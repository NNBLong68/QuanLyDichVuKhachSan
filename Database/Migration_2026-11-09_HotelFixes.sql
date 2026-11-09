/* =====================================================================================
   MIGRATION KHÔNG MẤT DỮ LIỆU — Quản Lý Dịch Vụ Khách Sạn
   Áp dụng cho CSDL đã tạo từ phiên bản cũ (không DROP DATABASE).

   Cách chạy:
     sqlcmd -S . -E -d QuanLyDichVuKhachSan -i Database\Migration_2026-11-09_HotelFixes.sql -f 65001 -b

   Nội dung:
     1. Bổ sung cột giá phòng PhongKhachSan.GiaPhong.
     2. Tạo bảng hóa đơn HoaDon / ChiTietHoaDon.
     3. Sửa view công nợ phòng: loại đơn hủy và đơn sự kiện/thuê xe chưa tới giờ.
     4. Sửa view doanh thu: loại đơn hủy.
     5. Sửa stored procedure xác nhận thanh toán phòng theo cùng quy tắc.
   ===================================================================================== */
USE [QuanLyDichVuKhachSan];
GO

/* 1. Giá phòng ---------------------------------------------------------------- */
IF COL_LENGTH('PhongKhachSan', 'GiaPhong') IS NULL
BEGIN
    ALTER TABLE PhongKhachSan
        ADD GiaPhong DECIMAL(18,2) NOT NULL
            CONSTRAINT DF_PhongKhachSan_GiaPhong DEFAULT 500000;

    UPDATE PhongKhachSan
    SET GiaPhong = CASE WHEN LoaiPhong = N'Phòng đôi' THEN 800000 ELSE 500000 END;
END
GO

/* 2. Hóa đơn quyết toán phòng ------------------------------------------------ */
IF OBJECT_ID('HoaDon', 'U') IS NULL
BEGIN
    CREATE TABLE HoaDon (
        Id              INT IDENTITY(1,1) PRIMARY KEY,
        MaHoaDon        NVARCHAR(50)  NOT NULL UNIQUE,
        SoPhong         NVARCHAR(20)  NOT NULL,
        TenKhach        NVARCHAR(100) NULL,
        SoDienThoai     NVARCHAR(20)  NULL,
        CCCD            NVARCHAR(20)  NULL,
        NgayLap         DATETIME      NOT NULL DEFAULT GETDATE(),
        NgayNhan        DATETIME      NULL,
        NgayTra         DATETIME      NULL,
        SoDem           INT           NOT NULL DEFAULT 0,
        GiaPhong        DECIMAL(18,2) NOT NULL DEFAULT 0,
        TienPhong       DECIMAL(18,2) NOT NULL DEFAULT 0,
        TienDichVu      DECIMAL(18,2) NOT NULL DEFAULT 0,
        TongTien        DECIMAL(18,2) NOT NULL DEFAULT 0,
        NguoiLap        NVARCHAR(100) NULL,
        NguoiThu        NVARCHAR(100) NULL
    );
    CREATE INDEX IX_HoaDon_SoPhong_NgayLap ON HoaDon(SoPhong, NgayLap);
END
GO
IF OBJECT_ID('ChiTietHoaDon', 'U') IS NULL
BEGIN
    CREATE TABLE ChiTietHoaDon (
        Id              INT IDENTITY(1,1) PRIMARY KEY,
        HoaDonId        INT           NOT NULL FOREIGN KEY REFERENCES HoaDon(Id) ON DELETE CASCADE,
        LoaiDichVu      NVARCHAR(50)  NULL,
        TenDichVu       NVARCHAR(200) NULL,
        ThoiGian        DATETIME      NULL,
        SoTien          DECIMAL(18,2) NOT NULL DEFAULT 0
    );
END
GO

/* 3. View công nợ phòng ------------------------------------------------------- */
CREATE OR ALTER VIEW vw_DichVuChuaThanhToanTheoPhong AS
SELECT SoPhong, N'Ẩm thực' AS LoaiDichVu, MaDon, TongTien AS SoTien, NgayTao AS ThoiGian
FROM DonHangMonAn
WHERE DaThanhToan = 0 AND TrangThai <> N'Đã hủy'
  AND (HinhThucThanhToan = N'Ghi nợ vào phòng' OR HinhThucThanhToan = N'Tính chung tiền phòng')
UNION ALL
SELECT SoPhong, N'Sự kiện', MaDon, (TongTienDuKien + ChiPhiPhatSinh - TienCoc), NgayTao
FROM DonDatSuKien
WHERE DaThanhToan = 0 AND SoPhong IS NOT NULL
  AND TrangThaiDon NOT IN (N'Đã hủy - Hoàn cọc', N'Đã hủy - Không hoàn cọc')
  AND ThoiGianBatDau <= GETDATE()
UNION ALL
SELECT SoPhong, N'Thuê xe', MaDon, (TienThue + ChiPhiPhatSinh - TienCoc), NgayTao
FROM DonThueXe
WHERE DaThanhToan = 0 AND SoPhong IS NOT NULL
  AND TrangThaiDon NOT IN (N'Đã hủy - Hoàn cọc', N'Đã hủy - Không hoàn cọc')
  AND NgayThue <= GETDATE()
UNION ALL
SELECT SoPhong, N'Bãi đỗ xe', MaVe, PhiGui, ThoiGianVao
FROM BaiDoXe
WHERE DaThanhToan = 0 AND SoPhong IS NOT NULL AND PhiGui > 0
UNION ALL
SELECT SoPhong, N'Giặt ủi', MaDon, TongTienThuKhach, NgayNhan
FROM DonGiatUi
WHERE DaThanhToan = 0 AND SoPhong IS NOT NULL AND TrangThai != N'Đã hủy';
GO

/* 4. View doanh thu ----------------------------------------------------------- */
CREATE OR ALTER VIEW vw_DoanhThuTheoDichVu AS
SELECT N'Ăn Uống' AS TenDichVu, COUNT(Id) AS SoGiaoDich, ISNULL(SUM(TongTien),0) AS DoanhThu, NgayTao AS Ngay
FROM DonHangMonAn WHERE DaThanhToan = 1 AND TrangThai <> N'Đã hủy' GROUP BY NgayTao
UNION ALL
SELECT N'Sảnh & Sự Kiện', COUNT(Id), ISNULL(SUM(TongTienDuKien + ChiPhiPhatSinh),0), NgayTao
FROM DonDatSuKien WHERE DaThanhToan = 1 AND TrangThaiDon NOT LIKE N'Đã hủy%' GROUP BY NgayTao
UNION ALL
SELECT N'Cho Thuê Xe Máy', COUNT(Id), ISNULL(SUM(TienThue + ChiPhiPhatSinh),0), NgayTao
FROM DonThueXe WHERE DaThanhToan = 1 AND TrangThaiDon NOT LIKE N'Đã hủy%' GROUP BY NgayTao
UNION ALL
SELECT N'Bãi & Hầm Đỗ Xe', COUNT(Id), ISNULL(SUM(PhiGui),0), ThoiGianVao
FROM BaiDoXe WHERE DaThanhToan = 1 AND PhiGui > 0 GROUP BY ThoiGianVao
UNION ALL
SELECT N'Giặt Ủi' AS TenDichVu, COUNT(Id) AS SoGiaoDich, ISNULL(SUM(TongTienThuKhach),0) AS DoanhThu, NgayNhan AS Ngay
FROM DonGiatUi WHERE DaThanhToan = 1 AND TrangThai != N'Đã hủy' GROUP BY NgayNhan;
GO

/* 5. Stored procedure xác nhận thanh toán dịch vụ theo phòng ------------------ */
CREATE OR ALTER PROCEDURE sp_XacNhanThanhToanDichVuTheoPhong
    @SoPhong NVARCHAR(20),
    @NhanVienThanhToanId INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE Id = @NhanVienThanhToanId AND TrangThai = N'Đang làm việc')
    BEGIN
        RAISERROR(N'Nhân viên xác nhận thanh toán không hợp lệ hoặc đã nghỉ việc.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Now DATETIME = GETDATE();

        UPDATE DonHangMonAn SET DaThanhToan = 1, NguoiThanhToanId = @NhanVienThanhToanId, ThoiGianThanhToan = @Now
        WHERE SoPhong = @SoPhong AND DaThanhToan = 0 AND TrangThai <> N'Đã hủy'
          AND (HinhThucThanhToan = N'Ghi nợ vào phòng' OR HinhThucThanhToan = N'Tính chung tiền phòng');

        UPDATE DonDatSuKien SET DaThanhToan = 1, NguoiThanhToanId = @NhanVienThanhToanId, ThoiGianThanhToan = @Now
        WHERE SoPhong = @SoPhong AND DaThanhToan = 0
          AND TrangThaiDon NOT IN (N'Đã hủy - Hoàn cọc', N'Đã hủy - Không hoàn cọc')
          AND ThoiGianBatDau <= @Now;

        UPDATE DonThueXe SET DaThanhToan = 1, NguoiThanhToanId = @NhanVienThanhToanId, ThoiGianThanhToan = @Now
        WHERE SoPhong = @SoPhong AND DaThanhToan = 0
          AND TrangThaiDon NOT IN (N'Đã hủy - Hoàn cọc', N'Đã hủy - Không hoàn cọc')
          AND NgayThue <= @Now;

        UPDATE BaiDoXe SET DaThanhToan = 1, NguoiThanhToanId = @NhanVienThanhToanId, ThoiGianThanhToan = @Now
        WHERE SoPhong = @SoPhong AND DaThanhToan = 0;

        UPDATE DonGiatUi SET DaThanhToan = 1, NguoiThanhToanId = @NhanVienThanhToanId, ThoiGianThanhToan = @Now
        WHERE SoPhong = @SoPhong AND DaThanhToan = 0 AND TrangThai != N'Đã hủy';

        COMMIT TRANSACTION;

        SELECT N'Đã xác nhận thanh toán toàn bộ dịch vụ của phòng ' + @SoPhong
               + N' bởi nhân viên #' + CAST(@NhanVienThanhToanId AS NVARCHAR(10)) AS KetQua;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

PRINT N'Migration hoàn tất: GiaPhong, HoaDon, ChiTietHoaDon, view công nợ/doanh thu và SP thanh toán đã được cập nhật.';
GO
