-- =========================================================
-- SYSTEM: KHÁCH SẠN MANAGEMENT DATABASE (QuanLyKhachSanDB)
-- ARCHITECTURE: EXACTLY 8 CORE STRONG ENTITIES (LOẠI BỎ DATPHONG)
-- =========================================================

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'QuanLyKhachSanDB')
BEGIN
    ALTER DATABASE QuanLyKhachSanDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanLyKhachSanDB;
END
GO

CREATE DATABASE QuanLyKhachSanDB;
GO

USE QuanLyKhachSanDB;
GO

-- =========================================================
-- 8 THỰC THỂ MẠNH CỐT LÕI
-- =========================================================

-- 1. NhanVien (Admin & Lễ tân - Tích hợp Ca trực)
CREATE TABLE NhanVien (
    MaNhanVien INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    CCCD VARCHAR(20) NOT NULL UNIQUE,
    SoDienThoai VARCHAR(15) NOT NULL,
    ChucVu NVARCHAR(20) NOT NULL CHECK (ChucVu IN (N'Admin', N'Lễ tân')),
    TenDangNhap VARCHAR(50) NOT NULL UNIQUE,
    MatKhau VARCHAR(255) NOT NULL,
    CaTruc NVARCHAR(30) NOT NULL DEFAULT N'Ca Sáng' CHECK (CaTruc IN (N'Ca Sáng', N'Ca Chiều', N'Ca Đêm')),
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Hoạt động' CHECK (TrangThai IN (N'Hoạt động', N'Nghỉ việc'))
);
GO

-- 2. KhachHang
CREATE TABLE KhachHang (
    MaKhachHang INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    CCCD VARCHAR(20) NULL,
    SoDienThoai VARCHAR(15) NULL,
    DiaChi NVARCHAR(255) NULL
);
GO

-- 3. Phong (Phòng ở khách sạn - Tích hợp thông tin đang ở nếu có)
CREATE TABLE Phong (
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    SoPhong VARCHAR(10) NOT NULL UNIQUE,
    LoaiPhong NVARCHAR(50) NOT NULL,
    GiaPhongTheoGio DECIMAL(18,2) NOT NULL DEFAULT 0,
    GiaPhongTheoNgay DECIMAL(18,2) NOT NULL DEFAULT 0,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Trống' CHECK (TrangThai IN (N'Trống', N'Đang sử dụng', N'Đang dọn dẹp', N'Bảo trì'))
);
GO

-- 4. DanhMucDichVu (Dịch vụ Ăn uống, Đồ khô, Cho thuê xe, Sảnh sự kiện, Bãi đỗ xe)
CREATE TABLE DanhMucDichVu (
    MaDichVu INT IDENTITY(1,1) PRIMARY KEY,
    TenDichVu NVARCHAR(100) NOT NULL,
    NhomDichVu NVARCHAR(50) NOT NULL CHECK (NhomDichVu IN (N'Ăn uống tươi', N'Thực phẩm khô', N'Cho thuê xe', N'Sảnh sự kiện', N'Bãi đỗ xe', N'Giặt ủi')),
    PhanLoaiDichVu NVARCHAR(20) NOT NULL CHECK (PhanLoaiDichVu IN (N'Tự túc', N'Ngoại')),
    DonGia DECIMAL(18,2) NOT NULL DEFAULT 0,
    SoLuongTon INT NOT NULL DEFAULT 0, -- Thực phẩm khô
    SuatAnTheoNgay INT NOT NULL DEFAULT 0, -- Suất ăn tươi/ngày
    ThongTinChiTiet NVARCHAR(MAX) NULL, -- Biển số xe, Sức chứa sảnh, Trang thiết bị, ĐVT đồ khô...
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Sẵn sàng' CHECK (TrangThai IN (N'Sẵn sàng', N'Đang cho thuê', N'Dọn dẹp', N'Bảo trì', N'Phục vụ', N'Tạm ngưng')),
    IsActive BIT NOT NULL DEFAULT 1 -- Admin bật/tắt dịch vụ
);
GO

-- 5. DoiTacGiatUi (Dịch vụ ngoài)
CREATE TABLE DoiTacGiatUi (
    MaDoiTac INT IDENTITY(1,1) PRIMARY KEY,
    TenDoiTac NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255) NULL,
    SoDienThoai VARCHAR(15) NULL,
    TyLeAnChiaKhachSan DECIMAL(5,2) NOT NULL DEFAULT 20.00 -- % Khách sạn hưởng
);
GO

-- 6. PhieuDichVu (Thực thể giao dịch duy nhất quản lý mọi lượt dùng dịch vụ của khách)
CREATE TABLE PhieuDichVu (
    MaPhieu INT IDENTITY(1,1) PRIMARY KEY,
    MaCodePhieu VARCHAR(30) NOT NULL UNIQUE,
    MaDichVu INT NOT NULL FOREIGN KEY REFERENCES DanhMucDichVu(MaDichVu),
    MaKhachHang INT NOT NULL FOREIGN KEY REFERENCES KhachHang(MaKhachHang),
    MaPhong INT NULL FOREIGN KEY REFERENCES Phong(MaPhong),
    MaNhanVien INT NOT NULL FOREIGN KEY REFERENCES NhanVien(MaNhanVien),
    MaDoiTac INT NULL FOREIGN KEY REFERENCES DoiTacGiatUi(MaDoiTac),
    NgayBatDau DATETIME NOT NULL DEFAULT GETDATE(),
    NgayKetThuc DATETIME NULL,
    SoLuong_Kg_Ngay DECIMAL(10,2) NOT NULL DEFAULT 1.0,
    DonGia DECIMAL(18,2) NOT NULL DEFAULT 0,
    TienCoc DECIMAL(18,2) NOT NULL DEFAULT 0,
    ThuThemPhatSinh DECIMAL(18,2) NOT NULL DEFAULT 0,
    TongTien DECIMAL(18,2) NOT NULL DEFAULT 0,
    TienKhachSanNhan DECIMAL(18,2) NULL,
    TienDoiTacNhan DECIMAL(18,2) NULL,
    GhiChuChiTiet NVARCHAR(MAX) NULL,
    HinhThucTinh NVARCHAR(30) NOT NULL DEFAULT N'Hóa đơn riêng' CHECK (HinhThucTinh IN (N'Hóa đơn riêng', N'Tính vào tiền phòng')),
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Đã hoàn tất' CHECK (TrangThai IN (N'Đang sử dụng', N'Đã hoàn tất', N'Đã hủy'))
);
GO

-- 7. LichSuNhapKho (Thực thể mạnh Quản lý Nhập kho thực phẩm do Admin)
CREATE TABLE LichSuNhapKho (
    MaNhapKho INT IDENTITY(1,1) PRIMARY KEY,
    MaDichVu INT NOT NULL FOREIGN KEY REFERENCES DanhMucDichVu(MaDichVu),
    MaNhanVienAdmin INT NOT NULL FOREIGN KEY REFERENCES NhanVien(MaNhanVien),
    NgayNhap DATETIME NOT NULL DEFAULT GETDATE(),
    SoLuongNhap INT NOT NULL CHECK (SoLuongNhap > 0),
    DonGiaNhap DECIMAL(18,2) NOT NULL CHECK (DonGiaNhap >= 0),
    TongTien AS (SoLuongNhap * DonGiaNhap),
    GhiChu NVARCHAR(255) NULL
);
GO

-- 8. HoaDon (Thực thể mạnh Quản lý Hóa đơn tổng hợp)
CREATE TABLE HoaDon (
    MaHoaDon INT IDENTITY(1,1) PRIMARY KEY,
    MaCodeHoaDon VARCHAR(30) NOT NULL UNIQUE,
    MaKhachHang INT NOT NULL FOREIGN KEY REFERENCES KhachHang(MaKhachHang),
    MaPhong INT NULL FOREIGN KEY REFERENCES Phong(MaPhong),
    MaNhanVien INT NOT NULL FOREIGN KEY REFERENCES NhanVien(MaNhanVien),
    MaPhieuDichVu INT NULL FOREIGN KEY REFERENCES PhieuDichVu(MaPhieu),
    LoaiHoaDon NVARCHAR(50) NOT NULL CHECK (LoaiHoaDon IN (N'Ăn uống', N'Sự kiện', N'Thuê xe', N'Bãi đỗ xe', N'Giặt ủi', N'Tổng hợp')),
    NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
    TongTien DECIMAL(18,2) NOT NULL DEFAULT 0,
    DaThanhToan DECIMAL(18,2) NOT NULL DEFAULT 0,
    PhuongThucThanhToan NVARCHAR(30) NOT NULL DEFAULT N'Tiền mặt' CHECK (PhuongThucThanhToan IN (N'Tiền mặt', N'Chuyển khoản', N'Thẻ')),
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Đã thanh toán' CHECK (TrangThai IN (N'Chưa thanh toán', N'Đã thanh toán', N'Đã hủy'))
);
GO

-- =========================================================
-- TRIGGERS TỰ ĐỘNG HÓA
-- =========================================================

-- Trigger 1: Tự động cộng tồn kho khi Admin nhập hàng kho mới
CREATE TRIGGER trg_CapNhatTonKho_KhiNhapMoi
ON LichSuNhapKho
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE DanhMucDichVu
    SET SoLuongTon = SoLuongTon + i.SoLuongNhap
    FROM DanhMucDichVu d
    INNER JOIN inserted i ON d.MaDichVu = i.MaDichVu;
END;
GO

-- Trigger 2: Tự động tính toán tổng tiền & trừ tồn kho đồ khô + ăn chia giặt ủi khi tạo Phiếu dịch vụ
CREATE TRIGGER trg_XuLyPhieuDichVu
ON PhieuDichVu
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO PhieuDichVu (
        MaCodePhieu, MaDichVu, MaKhachHang, MaPhong, MaNhanVien, MaDoiTac,
        NgayBatDau, NgayKetThuc, SoLuong_Kg_Ngay, DonGia, TienCoc, ThuThemPhatSinh,
        TongTien, TienKhachSanNhan, TienDoiTacNhan, GhiChuChiTiet, HinhThucTinh, TrangThai
    )
    SELECT 
        i.MaCodePhieu, i.MaDichVu, i.MaKhachHang, i.MaPhong, i.MaNhanVien, i.MaDoiTac,
        ISNULL(i.NgayBatDau, GETDATE()), i.NgayKetThuc, i.SoLuong_Kg_Ngay, i.DonGia,
        i.TienCoc, i.ThuThemPhatSinh,
        ROUND((i.SoLuong_Kg_Ngay * i.DonGia) + i.ThuThemPhatSinh, 2) AS TongTien,
        CASE WHEN dt.MaDoiTac IS NOT NULL THEN ROUND(((i.SoLuong_Kg_Ngay * i.DonGia) + i.ThuThemPhatSinh) * (dt.TyLeAnChiaKhachSan / 100.0), 2) ELSE NULL END AS TienKhachSanNhan,
        CASE WHEN dt.MaDoiTac IS NOT NULL THEN ROUND(((i.SoLuong_Kg_Ngay * i.DonGia) + i.ThuThemPhatSinh) * (1.0 - (dt.TyLeAnChiaKhachSan / 100.0)), 2) ELSE NULL END AS TienDoiTacNhan,
        i.GhiChuChiTiet, i.HinhThucTinh, ISNULL(i.TrangThai, N'Đã hoàn tất')
    FROM inserted i
    LEFT JOIN DoiTacGiatUi dt ON i.MaDoiTac = dt.MaDoiTac;

    UPDATE DanhMucDichVu
    SET SoLuongTon = SoLuongTon - CAST(i.SoLuong_Kg_Ngay AS INT)
    FROM DanhMucDichVu d
    INNER JOIN inserted i ON d.MaDichVu = i.MaDichVu
    WHERE d.NhomDichVu = N'Thực phẩm khô';
END;
GO

-- =========================================================
-- STORED PROCEDURES THỐNG KÊ (ADMIN REPORTING)
-- =========================================================

-- SP 1: Thống kê doanh thu chung
CREATE PROCEDURE sp_ThongKeDoanhThuChung
    @TuNgay DATETIME = NULL,
    @DenNgay DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET @TuNgay = ISNULL(@TuNgay, '2000-01-01');
    SET @DenNgay = ISNULL(@DenNgay, '2099-12-31');

    SELECT 
        LoaiHoaDon AS DichVu,
        COUNT(MaHoaDon) AS SoLuotSuDung,
        SUM(TongTien) AS TongDoanhThu
    FROM HoaDon
    WHERE NgayLap BETWEEN @TuNgay AND @DenNgay
      AND TrangThai = N'Đã thanh toán'
    GROUP BY LoaiHoaDon
    ORDER BY TongDoanhThu DESC;
END;
GO

-- SP 2: Thống kê Ăn uống & Kho thực phẩm
CREATE PROCEDURE sp_ThongKeAnUong
    @TuNgay DATETIME = NULL,
    @DenNgay DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET @TuNgay = ISNULL(@TuNgay, '2000-01-01');
    SET @DenNgay = ISNULL(@DenNgay, '2099-12-31');

    SELECT ISNULL(SUM(TongTien), 0) AS DoanhThuAnUong
    FROM PhieuDichVu p
    INNER JOIN DanhMucDichVu d ON p.MaDichVu = d.MaDichVu
    WHERE d.NhomDichVu IN (N'Ăn uống tươi', N'Thực phẩm khô')
      AND p.NgayBatDau BETWEEN @TuNgay AND @DenNgay;

    SELECT 
        d.TenDichVu AS TenMon,
        d.NhomDichVu,
        SUM(p.SoLuong_Kg_Ngay) AS TongSoLuongBan,
        SUM(p.TongTien) AS TongDoanhThu
    FROM PhieuDichVu p
    INNER JOIN DanhMucDichVu d ON p.MaDichVu = d.MaDichVu
    WHERE d.NhomDichVu IN (N'Ăn uống tươi', N'Thực phẩm khô')
      AND p.NgayBatDau BETWEEN @TuNgay AND @DenNgay
    GROUP BY d.TenDichVu, d.NhomDichVu
    ORDER BY TongSoLuongBan DESC;

    SELECT 
        d.TenDichVu AS TenHang,
        d.ThongTinChiTiet AS DVT,
        d.SoLuongTon,
        ISNULL(SUM(p.SoLuong_Kg_Ngay), 0) AS SoLuongDaBan
    FROM DanhMucDichVu d
    LEFT JOIN PhieuDichVu p ON d.MaDichVu = p.MaDichVu AND p.NgayBatDau BETWEEN @TuNgay AND @DenNgay
    WHERE d.NhomDichVu = N'Thực phẩm khô'
    GROUP BY d.TenDichVu, d.ThongTinChiTiet, d.SoLuongTon
    ORDER BY SoLuongDaBan DESC;
END;
GO

-- SP 3: Thống kê Thuê xe
CREATE PROCEDURE sp_ThongKeThueXe
    @TuNgay DATETIME = NULL,
    @DenNgay DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET @TuNgay = ISNULL(@TuNgay, '2000-01-01');
    SET @DenNgay = ISNULL(@DenNgay, '2099-12-31');

    SELECT 
        d.TenDichVu AS TenXe,
        d.ThongTinChiTiet AS BienSo,
        COUNT(p.MaPhieu) AS SoLuotThue,
        SUM(p.TongTien) AS DoanhThu
    FROM PhieuDichVu p
    INNER JOIN DanhMucDichVu d ON p.MaDichVu = d.MaDichVu
    WHERE d.NhomDichVu = N'Cho thuê xe'
      AND p.NgayBatDau BETWEEN @TuNgay AND @DenNgay
    GROUP BY d.TenDichVu, d.ThongTinChiTiet
    ORDER BY SoLuotThue DESC;

    SELECT d.TenDichVu AS TenXe, p.GhiChuChiTiet, p.NgayKetThuc
    FROM PhieuDichVu p
    INNER JOIN DanhMucDichVu d ON p.MaDichVu = d.MaDichVu
    WHERE d.NhomDichVu = N'Cho thuê xe' AND (p.GhiChuChiTiet LIKE N'%hư%' OR p.GhiChuChiTiet LIKE N'%mất%');
END;
GO

-- SP 4: Thống kê Sự kiện
CREATE PROCEDURE sp_ThongKeSuKien
    @TuNgay DATETIME = NULL,
    @DenNgay DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET @TuNgay = ISNULL(@TuNgay, '2000-01-01');
    SET @DenNgay = ISNULL(@DenNgay, '2099-12-31');

    SELECT 
        d.TenDichVu AS TenKhuVuc,
        COUNT(p.MaPhieu) AS SoLuotThue,
        SUM(p.TongTien) AS DoanhThu
    FROM PhieuDichVu p
    INNER JOIN DanhMucDichVu d ON p.MaDichVu = d.MaDichVu
    WHERE d.NhomDichVu = N'Sảnh sự kiện'
      AND p.NgayBatDau BETWEEN @TuNgay AND @DenNgay
    GROUP BY d.TenDichVu
    ORDER BY SoLuotThue DESC;

    SELECT d.TenDichVu AS TenKhuVuc, p.NgayBatDau, p.GhiChuChiTiet AS GhiChuHuHai
    FROM PhieuDichVu p
    INNER JOIN DanhMucDichVu d ON p.MaDichVu = d.MaDichVu
    WHERE d.NhomDichVu = N'Sảnh sự kiện' AND p.GhiChuChiTiet IS NOT NULL;
END;
GO

-- SP 5: Thống kê Bãi đỗ xe
CREATE PROCEDURE sp_ThongKeDoXe
    @TuNgay DATETIME = NULL,
    @DenNgay DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET @TuNgay = ISNULL(@TuNgay, '2000-01-01');
    SET @DenNgay = ISNULL(@DenNgay, '2099-12-31');

    SELECT 
        COUNT(p.MaPhieu) AS TongLuotGui,
        SUM(p.TongTien) AS TongDoanhThuDoXe
    FROM PhieuDichVu p
    INNER JOIN DanhMucDichVu d ON p.MaDichVu = d.MaDichVu
    WHERE d.NhomDichVu = N'Bãi đỗ xe'
      AND p.NgayBatDau BETWEEN @TuNgay AND @DenNgay;

    SELECT p.MaPhieu, p.GhiChuChiTiet AS GhiChuSuaChua, p.NgayBatDau
    FROM PhieuDichVu p
    INNER JOIN DanhMucDichVu d ON p.MaDichVu = d.MaDichVu
    WHERE d.NhomDichVu = N'Bãi đỗ xe' AND p.GhiChuChiTiet IS NOT NULL;
END;
GO

-- SP 6: Thống kê Giặt ủi theo đối tác
CREATE PROCEDURE sp_ThongKeGiatUi
    @TuNgay DATETIME = NULL,
    @DenNgay DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET @TuNgay = ISNULL(@TuNgay, '2000-01-01');
    SET @DenNgay = ISNULL(@DenNgay, '2099-12-31');

    SELECT 
        dt.TenDoiTac,
        dt.TyLeAnChiaKhachSan,
        COUNT(p.MaPhieu) AS SoPhieuGiat,
        SUM(p.SoLuong_Kg_Ngay) AS TongKg,
        SUM(p.TongTien) AS TongDoanhThuDichVu,
        SUM(p.TienKhachSanNhan) AS DoanhThuKhachSan,
        SUM(p.TienDoiTacNhan) AS TienTraDoiTac
    FROM PhieuDichVu p
    INNER JOIN DoiTacGiatUi dt ON p.MaDoiTac = dt.MaDoiTac
    WHERE p.NgayBatDau BETWEEN @TuNgay AND @DenNgay
    GROUP BY dt.TenDoiTac, dt.TyLeAnChiaKhachSan
    ORDER BY TongDoanhThuDichVu DESC;
END;
GO

-- =========================================================
-- SEED DATA (DỮ LIỆU MẪU)
-- =========================================================

-- 1. NhanVien
INSERT INTO NhanVien (HoTen, CCCD, SoDienThoai, ChucVu, TenDangNhap, MatKhau, CaTruc) VALUES
(N'Chủ Khách Sạn - Admin', '001099000001', '0901111111', N'Admin', 'admin', 'YourPassword123!', N'Ca Sáng'),
(N'Lễ Tân Nguyễn Văn A', '001099000002', '0902222222', N'Lễ tân', 'letana', '123456', N'Ca Sáng'),
(N'Lễ Tân Trần Thị B', '001099000003', '0903333333', N'Lễ tân', 'letanb', '123456', N'Ca Chiều');

-- 2. KhachHang
INSERT INTO KhachHang (HoTen, CCCD, SoDienThoai, DiaChi) VALUES
(N'Lê Văn Cường', '036095001111', '0987654321', N'Hà Nội'),
(N'Phạm Thị Mai', '036095002222', '0912345678', N'Đà Nẵng'),
(N'Nguyễn Hoàng Nam', '036095003333', '0978123456', N'TP. Hồ Chí Minh');

-- 3. Phong
INSERT INTO Phong (SoPhong, LoaiPhong, GiaPhongTheoGio, GiaPhongTheoNgay, TrangThai) VALUES
('101', N'Phòng Đơn Standard', 100000, 500000, N'Đang sử dụng'),
('102', N'Phòng Đôi VIP', 200000, 900000, N'Đang sử dụng'),
('201', N'Phòng Suite Luxe', 350000, 1500000, N'Trống');

-- 4. DanhMucDichVu
INSERT INTO DanhMucDichVu (TenDichVu, NhomDichVu, PhanLoaiDichVu, DonGia, SoLuongTon, SuatAnTheoNgay, ThongTinChiTiet, TrangThai) VALUES
(N'Phở Bò Đặc Biệt', N'Ăn uống tươi', N'Tự túc', 55000, 0, 50, N'Đơn vị: Tô', N'Phục vụ'),
(N'Cà Phê Sữa Đá', N'Ăn uống tươi', N'Tự túc', 30000, 0, 0, N'Đơn vị: Ly', N'Phục vụ'),
(N'Nước Ngọt Coca Cola', N'Thực phẩm khô', N'Tự túc', 15000, 100, 0, N'Lon', N'Sẵn sàng'),
(N'Mì Gói Hảo Hảo', N'Thực phẩm khô', N'Tự túc', 10000, 150, 0, N'Gói', N'Sẵn sàng'),
(N'Honda Vision 2023', N'Cho thuê xe', N'Tự túc', 150000, 0, 0, N'Biển số: 43-F1 123.45', N'Đang cho thuê'),
(N'Sảnh Hội Nghị Ballroom', N'Sảnh sự kiện', N'Tự túc', 10000000, 0, 0, N'Sức chứa 200 người. Đèn LED, âm thanh', N'Sẵn sàng'),
(N'Gửi Xe Máy Hầm', N'Bãi đỗ xe', N'Tự túc', 10000, 0, 0, N'Xe máy', N'Sẵn sàng'),
(N'Gửi Ô Tô Hầm', N'Bãi đỗ xe', N'Tự túc', 50000, 0, 0, N'Ô tô 4-7 chỗ', N'Sẵn sàng'),
(N'Dịch Vụ Giặt Sấy', N'Giặt ủi', N'Ngoại', 30000, 0, 0, N'Tính theo kg', N'Sẵn sàng');

-- 5. DoiTacGiatUi
INSERT INTO DoiTacGiatUi (TenDoiTac, DiaChi, SoDienThoai, TyLeAnChiaKhachSan) VALUES
(N'Giặt ủi Siêu Sạch Express', N'123 Nguyễn Văn Linh, Đà Nẵng', '0905123456', 25.00),
(N'Xưởng Giặt An Nhiên', N'45 Lê Duẩn, Đà Nẵng', '0905654321', 20.00);

-- 6. LichSuNhapKho (Admin nhập kho đồ khô)
INSERT INTO LichSuNhapKho (MaDichVu, MaNhanVienAdmin, NgayNhap, SoLuongNhap, DonGiaNhap, GhiChu) VALUES
(3, 1, GETDATE(), 50, 12000, N'Nhập 50 lon Coca Cola đợt 1');

-- 7. PhieuDichVu (Lễ tân ghi nhận các lượt dùng dịch vụ)
INSERT INTO PhieuDichVu (MaCodePhieu, MaDichVu, MaKhachHang, MaPhong, MaNhanVien, MaDoiTac, SoLuong_Kg_Ngay, DonGia, TienCoc, ThuThemPhatSinh, GhiChuChiTiet, HinhThucTinh) VALUES
('AU-001', 1, 1, 1, 2, NULL, 2, 55000, 0, 0, N'2 Tô phở bò giao tận phòng 101', N'Hóa đơn riêng'),
('AU-002', 3, 1, 1, 2, NULL, 2, 15000, 0, 0, N'2 Lon Coca Cola (Tồn kho tự giảm)', N'Hóa đơn riêng'),
('SK-001', 6, 3, NULL, 2, NULL, 1, 10000000, 3000000, 500000, N'Ghi chú hư hại: Vỡ 1 ly thủy tinh sân khấu', N'Hóa đơn riêng'),
('TX-001', 5, 1, 1, 2, NULL, 2, 150000, 500000, 0, N'Thuê xe Vision 2 ngày. Biển số 43-F1 123.45', N'Hóa đơn riêng'),
('DX-001', 8, 1, 1, 2, NULL, 1, 50000, 0, 0, N'Xe ô tô đỗ hầm 30F-999.88', N'Hóa đơn riêng'),
('GU-001', 9, 2, 2, 2, 1, 5, 30000, 0, 0, N'Phiếu lấy đồ: GU-1001. Giặt 5kg đồ thơm', N'Hóa đơn riêng');

-- 8. HoaDon (Hóa đơn tổng hợp)
INSERT INTO HoaDon (MaCodeHoaDon, MaKhachHang, MaPhong, MaNhanVien, MaPhieuDichVu, LoaiHoaDon, TongTien, DaThanhToan, PhuongThucThanhToan) VALUES
('HD-AU-001', 1, 1, 2, 1, N'Ăn uống', 110000, 110000, N'Tiền mặt'),
('HD-SK-001', 3, NULL, 2, 3, N'Sự kiện', 10500000, 10500000, N'Chuyển khoản'),
('HD-GU-001', 2, 2, 2, 6, N'Giặt ủi', 150000, 150000, N'Tiền mặt');
GO

PRINT N'=== KHỞI TẠO CSDL ĐÚNG 8 BẢNG THỰC THỂ MẠNH QuanLyKhachSanDB THÀNH CÔNG! ===';
GO
