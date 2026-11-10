/* =================================================================================
   HỆ THỐNG QUẢN LÝ DỊCH VỤ KHÁCH SẠN (DALAT HOTEL) - SQL SERVER
   FILE 5: Data.sql - DỮ LIỆU TĨNH HỆ THỐNG (MASTER DATA) & DỮ LIỆU MẪU (SAMPLE DATA)
   Thứ tự chạy: 1. Table.sql -> 2. Views.sql -> 3. Trigger.sql -> 4. Procedures.sql -> 5. Data.sql
   ================================================================================= */

USE QuanLyDichVuKhachSan;
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- =====================================================================================
-- PHẦN 6: DỮ LIỆU TĨNH & DANH MỤC HỆ THỐNG (SYSTEM & MASTER DATA)
-- =====================================================================================

-- 6.1 Tài khoản đăng nhập mặc định (Mật khẩu mặc định: 123)
INSERT INTO TaiKhoan (TenDangNhap, MatKhauHash, HoTen, VaiTro, SoDienThoai, Email) VALUES
('admin', 'ubJSwSb+V3dx+o3DsP1XwWiiBuvs6i40PTcsb1ziOsU=', N'Chủ Khách Sạn', N'Admin', '0948198812', 'dalathotel@gmail.com'),
('letan', 'ubJSwSb+V3dx+o3DsP1XwWiiBuvs6i40PTcsb1ziOsU=', N'Lễ Tân Trực Chính', N'LeTan', '0912345678', 'letan@dalathotel.vn');
GO

-- 6.2 Nhân viên
INSERT INTO NhanVien (MaNV, HoTen, CCCD, SoDienThoai, Email, ChucVu, LuongCoBan, TrangThai) VALUES
('NV001', N'Trần Thị Mai',  '001198000123', '0912345678', 'maitran@dalathotel.vn',   N'Lễ tân trưởng', 12000000, N'Đang làm việc'),
('NV002', N'Nguyễn Văn An',  '001199000456', '0987654321', 'annguyen@dalathotel.vn',  N'Nhân viên lễ tân',8500000, N'Đang làm việc'),
('NV003', N'Lê Hoàng Nam',   '001197000789', '0933112233', 'namle@dalathotel.vn',     N'Nhân viên lễ tân',8500000, N'Đang làm việc');
GO

-- 6.3 Ca trực
INSERT INTO CaTruc (TenCa, GioBatDau, GioKetThuc, GhiChu) VALUES
(N'Ca Sáng',  '06:00:00', '14:00:00', N'Tiếp nhận khách check-in sáng, kiểm tra mini-bar'),
(N'Ca Chiều', '14:00:00', '22:00:00', N'Đỉnh điểm check-in/out, bàn giao xe cho thuê'),
(N'Ca Đêm',   '22:00:00', '06:00:00', N'Trực đêm, bảo an bãi xe, chốt ca ngày');
GO

-- 6.4 Phân công ca trực trọn vẹn tháng 9/2026 (Từ 01/09 đến 30/09, mỗi ngày 3 ca cho 3 nhân viên luân phiên)
INSERT INTO PhanCongCaTruc (NhanVienId, CaTrucId, NgayTruc, GhiChu) VALUES
-- Ngày 01/09/2026
(1, 1, '2026-09-01', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-09-01', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-09-01', N'Lê Hoàng Nam ca đêm'),
-- Ngày 02/09/2026
(2, 1, '2026-09-02', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-09-02', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-09-02', N'Trần Thị Mai ca đêm'),
-- Ngày 03/09/2026
(3, 1, '2026-09-03', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-09-03', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-09-03', N'Nguyễn Văn An ca đêm'),
-- Ngày 04/09/2026
(1, 1, '2026-09-04', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-09-04', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-09-04', N'Lê Hoàng Nam ca đêm'),
-- Ngày 05/09/2026
(2, 1, '2026-09-05', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-09-05', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-09-05', N'Trần Thị Mai ca đêm'),
-- Ngày 06/09/2026
(3, 1, '2026-09-06', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-09-06', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-09-06', N'Nguyễn Văn An ca đêm'),
-- Ngày 07/09/2026
(1, 1, '2026-09-07', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-09-07', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-09-07', N'Lê Hoàng Nam ca đêm'),
-- Ngày 08/09/2026
(2, 1, '2026-09-08', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-09-08', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-09-08', N'Trần Thị Mai ca đêm'),
-- Ngày 09/09/2026
(3, 1, '2026-09-09', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-09-09', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-09-09', N'Nguyễn Văn An ca đêm'),
-- Ngày 10/09/2026
(1, 1, '2026-09-10', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-09-10', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-09-10', N'Lê Hoàng Nam ca đêm'),
-- Ngày 11/09/2026
(2, 1, '2026-09-11', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-09-11', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-09-11', N'Trần Thị Mai ca đêm'),
-- Ngày 12/09/2026
(3, 1, '2026-09-12', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-09-12', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-09-12', N'Nguyễn Văn An ca đêm'),
-- Ngày 13/09/2026
(1, 1, '2026-09-13', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-09-13', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-09-13', N'Lê Hoàng Nam ca đêm'),
-- Ngày 14/09/2026
(2, 1, '2026-09-14', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-09-14', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-09-14', N'Trần Thị Mai ca đêm'),
-- Ngày 15/09/2026
(3, 1, '2026-09-15', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-09-15', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-09-15', N'Nguyễn Văn An ca đêm'),
-- Ngày 16/09/2026
(1, 1, '2026-09-16', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-09-16', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-09-16', N'Lê Hoàng Nam ca đêm'),
-- Ngày 17/09/2026 - Hôm nay
(2, 1, '2026-09-17', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-09-17', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-09-17', N'Trần Thị Mai ca đêm'),
-- Ngày 18/09/2026
(3, 1, '2026-09-18', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-09-18', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-09-18', N'Nguyễn Văn An ca đêm'),
-- Ngày 19/09/2026
(1, 1, '2026-09-19', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-09-19', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-09-19', N'Lê Hoàng Nam ca đêm'),
-- Ngày 20/09/2026
(2, 1, '2026-09-20', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-09-20', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-09-20', N'Trần Thị Mai ca đêm'),
-- Ngày 21/09/2026
(3, 1, '2026-09-21', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-09-21', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-09-21', N'Nguyễn Văn An ca đêm'),
-- Ngày 22/09/2026
(1, 1, '2026-09-22', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-09-22', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-09-22', N'Lê Hoàng Nam ca đêm'),
-- Ngày 23/09/2026
(2, 1, '2026-09-23', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-09-23', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-09-23', N'Trần Thị Mai ca đêm'),
-- Ngày 24/09/2026
(3, 1, '2026-09-24', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-09-24', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-09-24', N'Nguyễn Văn An ca đêm'),
-- Ngày 25/09/2026
(1, 1, '2026-09-25', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-09-25', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-09-25', N'Lê Hoàng Nam ca đêm'),
-- Ngày 26/09/2026
(2, 1, '2026-09-26', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-09-26', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-09-26', N'Trần Thị Mai ca đêm'),
-- Ngày 27/09/2026
(3, 1, '2026-09-27', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-09-27', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-09-27', N'Nguyễn Văn An ca đêm'),
-- Ngày 28/09/2026
(1, 1, '2026-09-28', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-09-28', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-09-28', N'Lê Hoàng Nam ca đêm'),
-- Ngày 29/09/2026
(2, 1, '2026-09-29', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-09-29', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-09-29', N'Trần Thị Mai ca đêm'),
-- Ngày 30/09/2026
(3, 1, '2026-09-30', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-09-30', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-09-30', N'Nguyễn Văn An ca đêm');
GO

-- Phân công ca trực tháng 11/2026 (Xếp ca đầy đủ từ đầu tháng và từ hôm nay 09/11/2026 đến hết tháng 30/11/2026)
INSERT INTO PhanCongCaTruc (NhanVienId, CaTrucId, NgayTruc, GhiChu) VALUES
-- Ngày 01/11/2026
(1, 1, '2026-11-01', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-11-01', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-11-01', N'Lê Hoàng Nam ca đêm'),
-- Ngày 02/11/2026
(2, 1, '2026-11-02', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-11-02', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-11-02', N'Trần Thị Mai ca đêm'),
-- Ngày 03/11/2026
(3, 1, '2026-11-03', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-11-03', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-11-03', N'Nguyễn Văn An ca đêm'),
-- Ngày 04/11/2026
(1, 1, '2026-11-04', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-11-04', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-11-04', N'Lê Hoàng Nam ca đêm'),
-- Ngày 05/11/2026
(2, 1, '2026-11-05', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-11-05', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-11-05', N'Trần Thị Mai ca đêm'),
-- Ngày 06/11/2026
(3, 1, '2026-11-06', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-11-06', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-11-06', N'Nguyễn Văn An ca đêm'),
-- Ngày 07/11/2026
(1, 1, '2026-11-07', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-11-07', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-11-07', N'Lê Hoàng Nam ca đêm'),
-- Ngày 08/11/2026
(2, 1, '2026-11-08', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-11-08', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-11-08', N'Trần Thị Mai ca đêm'),
-- Ngày 09/11/2026 - Hôm nay
(3, 1, '2026-11-09', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-11-09', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-11-09', N'Nguyễn Văn An ca đêm'),
-- Ngày 10/11/2026
(1, 1, '2026-11-10', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-11-10', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-11-10', N'Lê Hoàng Nam ca đêm'),
-- Ngày 11/11/2026
(2, 1, '2026-11-11', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-11-11', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-11-11', N'Trần Thị Mai ca đêm'),
-- Ngày 12/11/2026
(3, 1, '2026-11-12', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-11-12', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-11-12', N'Nguyễn Văn An ca đêm'),
-- Ngày 13/11/2026
(1, 1, '2026-11-13', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-11-13', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-11-13', N'Lê Hoàng Nam ca đêm'),
-- Ngày 14/11/2026
(2, 1, '2026-11-14', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-11-14', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-11-14', N'Trần Thị Mai ca đêm'),
-- Ngày 15/11/2026
(3, 1, '2026-11-15', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-11-15', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-11-15', N'Nguyễn Văn An ca đêm'),
-- Ngày 16/11/2026
(1, 1, '2026-11-16', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-11-16', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-11-16', N'Lê Hoàng Nam ca đêm'),
-- Ngày 17/11/2026
(2, 1, '2026-11-17', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-11-17', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-11-17', N'Trần Thị Mai ca đêm'),
-- Ngày 18/11/2026
(3, 1, '2026-11-18', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-11-18', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-11-18', N'Nguyễn Văn An ca đêm'),
-- Ngày 19/11/2026
(1, 1, '2026-11-19', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-11-19', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-11-19', N'Lê Hoàng Nam ca đêm'),
-- Ngày 20/11/2026
(2, 1, '2026-11-20', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-11-20', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-11-20', N'Trần Thị Mai ca đêm'),
-- Ngày 21/11/2026
(3, 1, '2026-11-21', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-11-21', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-11-21', N'Nguyễn Văn An ca đêm'),
-- Ngày 22/11/2026
(1, 1, '2026-11-22', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-11-22', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-11-22', N'Lê Hoàng Nam ca đêm'),
-- Ngày 23/11/2026
(2, 1, '2026-11-23', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-11-23', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-11-23', N'Trần Thị Mai ca đêm'),
-- Ngày 24/11/2026
(3, 1, '2026-11-24', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-11-24', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-11-24', N'Nguyễn Văn An ca đêm'),
-- Ngày 25/11/2026
(1, 1, '2026-11-25', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-11-25', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-11-25', N'Lê Hoàng Nam ca đêm'),
-- Ngày 26/11/2026
(2, 1, '2026-11-26', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-11-26', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-11-26', N'Trần Thị Mai ca đêm'),
-- Ngày 27/11/2026
(3, 1, '2026-11-27', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-11-27', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-11-27', N'Nguyễn Văn An ca đêm'),
-- Ngày 28/11/2026
(1, 1, '2026-11-28', N'Trần Thị Mai ca sáng'),
(2, 2, '2026-11-28', N'Nguyễn Văn An ca chiều'),
(3, 3, '2026-11-28', N'Lê Hoàng Nam ca đêm'),
-- Ngày 29/11/2026
(2, 1, '2026-11-29', N'Nguyễn Văn An ca sáng'),
(3, 2, '2026-11-29', N'Lê Hoàng Nam ca chiều'),
(1, 3, '2026-11-29', N'Trần Thị Mai ca đêm'),
-- Ngày 30/11/2026
(3, 1, '2026-11-30', N'Lê Hoàng Nam ca sáng'),
(1, 2, '2026-11-30', N'Trần Thị Mai ca chiều'),
(2, 3, '2026-11-30', N'Nguyễn Văn An ca đêm');
GO

-- 6.5 Danh mục 5 dịch vụ hệ thống
INSERT INTO DichVuHeThong (MaDV, TenDV, LoaiDichVu, DangHoatDong, MoTa) VALUES
('AN_UONG', N'Ăn Uống',              N'Tự túc',        1, N'Bán lẻ hàng khô, nước giải khát, snack tại phòng & quầy lễ tân'),
('SU_KIEN', N'Sảnh & Sự Kiện',        N'Tự túc',        1, N'Cho thuê sảnh tiệc, phòng họp VIP, sân thượng Sky Lounge theo giờ/ngày'),
('THUE_XE', N'Cho Thuê Xe Máy',      N'Tự túc',        1, N'Cho thuê xe tay ga, xe số kèm mũ bảo hiểm'),
('DO_XE',   N'Bãi & Hầm Đỗ Xe',      N'Tự túc',        1, N'Quản lý trông giữ xe máy, ô tô có thu phí theo lượt/giờ/ngày'),
('GIAT_UI', N'Giặt Ủi',              N'Dịch vụ ngoài', 1, N'Liên kết đối tác giặt sấy, giặt hấp lấy hoa hồng 25-35%');
GO

-- 6.6 Cấu hình tham số hệ thống
INSERT INTO ThamSoHeThong (MaThamSo, TenThamSo, GiaTri, MoTa) VALUES
('CHECKIN_STANDARD_TIME',       '14:00',      N'14:00', N'Giờ nhận phòng tiêu chuẩn'),
('CHECKOUT_STANDARD_TIME',      '12:00',      N'12:00', N'Giờ trả phòng tiêu chuẩn'),
('EVENT_ADVANCE_ALERT_MINUTES', '15',         N'15',    N'Báo trước khi sự kiện sắp hết giờ (phút)'),
-- FIX (bổ sung theo yêu cầu): thời gian dọn dẹp mặc định giữa 2 đơn liên tiếp cùng khu vực
('EVENT_CLEANING_BUFFER_MINUTES','60',        N'60',    N'Thời gian dọn dẹp/kiểm tra tối thiểu giữa 2 đơn liên tiếp cùng 1 khu vực sự kiện (phút)'),
('VEHICLE_ADVANCE_ALERT_MINUTES','30',        N'30',    N'Báo trước khi xe sắp hết giờ thuê (phút)'),
('VEHICLE_CLEANING_BUFFER_MINUTES','30',      N'30',    N'Thời gian vệ sinh/kiểm tra xe tối thiểu giữa 2 lượt thuê liên tiếp cùng 1 xe (phút)');
GO

-- 6.7 Danh mục 20 phòng khách sạn (Tầng 1 đến Tầng 5)
-- 13 phòng đang ở (~2/3 tổng số phòng), thời gian lưu trú đầy đủ 2-3 ngày, 1-2 tuần từ hôm nay
INSERT INTO PhongKhachSan (SoPhong, Tang, LoaiPhong, TrangThai, TenKhach, SoDienThoai, CCCD, GhiChu, NgayNhanPhong, NgayTraPhong) VALUES
('101', 1, N'Phòng đơn', N'Đang ở',   N'Đoàn Minh Trí',     '0908112233', '079198001234', N'Khách ở 2 ngày từ hôm nay, thuê xe SH', GETDATE(), DATEADD(DAY,2,GETDATE())),
('102', 1, N'Phòng đơn', N'Đang ở',   N'Trịnh Hồng Quân',   '0909123456', '079192003344', N'Khách ở 3 ngày từ hôm nay, thuê xe Lead', GETDATE(), DATEADD(DAY,3,GETDATE())),
('103', 1, N'Phòng đôi', N'Đang ở',   N'Vũ Hoàng Nam',      '0912334455', '079193005566', N'Khách ở 2 tuần từ hôm nay, thuê xe Grande', GETDATE(), DATEADD(DAY,14,GETDATE())),
('104', 1, N'Phòng đôi', N'Đang dọn', NULL,                 NULL,         NULL,           N'Khách vừa trả phòng lúc sáng', NULL, NULL),
('201', 2, N'Phòng đơn', N'Đang ở',   N'Hoàng Mai Linh',    '0978112244', '079194007788', N'Khách ở 2 ngày từ hôm nay, thuê xe Future', GETDATE(), DATEADD(DAY,2,GETDATE())),
('202', 2, N'Phòng đôi', N'Đang ở',   N'Lê Tấn Phát',       '0969888999', '079201009876', N'Khách ở 1 tuần từ hôm nay, thuê xe AirBlade', GETDATE(), DATEADD(DAY,7,GETDATE())),
('203', 2, N'Phòng đôi', N'Đang ở',   N'Bùi Thanh Tùng',    '0938445566', '079196001122', N'Khách ở 2 tuần từ hôm nay, thuê xe Winner X', GETDATE(), DATEADD(DAY,14,GETDATE())),
('204', 2, N'Phòng đơn', N'Bảo trì',  NULL,                 NULL,         NULL,           N'Đang sửa vòi sen toilet', NULL, NULL),
('301', 3, N'Phòng đôi', N'Đang ở',   N'Nguyễn Anh Thư',    '0918777666', '079195003456', N'Khách ở 2 tuần từ hôm nay (honeymoon, thuê xe Vision)', GETDATE(), DATEADD(DAY,14,GETDATE())),
('302', 3, N'Phòng đơn', N'Đang ở',   N'Đặng Hải Yến',      '0982334411', '079197003355', N'Khách ở 3 ngày từ hôm nay, thuê xe Vespa', GETDATE(), DATEADD(DAY,3,GETDATE())),
('303', 3, N'Phòng đôi', N'Đang ở',   N'Phan Văn Đức',      '0945667788', '079199004466', N'Khách ở 2 tuần từ hôm nay, thuê xe NVX', GETDATE(), DATEADD(DAY,14,GETDATE())),
('304', 3, N'Phòng đơn', N'Trống',    NULL,                 NULL,         NULL,           NULL, NULL, NULL),
('401', 4, N'Phòng đôi', N'Đang ở',   N'Phạm Quốc Bảo',     '0933444555', '079188005678', N'Khách ở 2 tuần từ hôm nay, gửi xe ô tô 7 chỗ', GETDATE(), DATEADD(DAY,14,GETDATE())),
('402', 4, N'Phòng đơn', N'Đang ở',   N'Trần Thu Hà',       '0919223344', '079189006677', N'Khách ở 3 ngày từ hôm nay, thuê xe Liberty', GETDATE(), DATEADD(DAY,3,GETDATE())),
('403', 4, N'Phòng đôi', N'Đang ở',   N'Đỗ Minh Khang',     '0987556677', '079187008899', N'Khách ở 1 tuần từ hôm nay, thuê xe Exciter', GETDATE(), DATEADD(DAY,7,GETDATE())),
('404', 4, N'Phòng đơn', N'Trống',    NULL,                 NULL,         NULL,           NULL, NULL, NULL),
('501', 5, N'Phòng đôi', N'Đang ở',   N'Nguyễn Hoàng Long', '0903889900', '079186001234', N'Khách ở 2 tuần từ hôm nay, thuê xe PG-1', GETDATE(), DATEADD(DAY,14,GETDATE())),
('502', 5, N'Phòng đơn', N'Đang ở',   N'Lê Thị Ngọc Ánh',   '0979334455', '079185002345', N'Khách ở 1 tuần từ hôm nay, thuê xe Sirius', GETDATE(), DATEADD(DAY,7,GETDATE())),
('503', 5, N'Phòng đôi', N'Đang dọn', NULL,                 NULL,         NULL,           N'Đang thay ga trải giường', NULL, NULL),
('504', 5, N'Phòng đơn', N'Trống',    NULL,                 NULL,         NULL,           NULL, NULL, NULL);
GO
UPDATE PhongKhachSan
SET GiaPhong = CASE WHEN LoaiPhong = N'Phòng đôi' THEN 800000 ELSE 500000 END;
GO

-- 6.8 Nhà cung cấp
INSERT INTO NhaCungCap (TenNhaCungCap, NguoiLienHe, SoDienThoai, DiaChi, Email, GhiChu, TrangThai) VALUES
(N'Công ty CP Hàng tiêu dùng Masan', N'Nguyễn Văn Toàn', '02838221100', N'Tầng 12 Central Plaza, Q.1, TP.HCM', 'contact@masan.vn', N'Cung cấp nước khoáng, nước ngọt, mì ly ăn liền', 1),
(N'Công ty TNHH Nestlé Việt Nam',     N'Phạm Mai Lan',   '02839112233', N'KCN Biên Hòa 2, Đồng Nai',          'order@vn.nestle.com', N'Cung cấp cà phê G7, trà đóng chai, nước tăng lực', 1),
(N'Cơ sở Đặc sản Rượu Vang Đà Lạt',  N'Lê Hữu Thọ',     '02633822456', N'Phường 8, TP. Đà Lạt',             'sales@dalatwine.com', N'Cung cấp rượu vang, mứt dâu, trà Atiso cao cấp', 1);
GO

-- 6.9 Danh mục món ăn & mini-bar
INSERT INTO MonAn (TenMon, DanhMuc, DonViTinh, GiaBan, GiaVonBinhQuan, SoLuongTonKho, MucTonToiThieu, NhaCungCapId, GhiChu) VALUES
(N'Mì tôm ly Modern',               N'Đồ ăn nhẹ',       N'Ly',    20000, 11000, 150, 20, 1, N'Hạn dùng 8 tháng'),
(N'Nước khoáng Lavie 500ml',        N'Nước giải khát',  N'Chai',  15000,  6000, 240, 30, 1, N'Hạn dùng 12 tháng'),
(N'Coca Cola lon 330ml',            N'Nước giải khát',  N'Lon',   20000, 10000, 180, 25, 1, N'Hạn dùng 9 tháng'),
(N'Cà phê hòa tan G7 (hộp 20 gói)', N'Đồ uống pha',     N'Hộp',   45000, 22000,  80, 15, 2, N'Hạn dùng 18 tháng'),
(N'Trà Atiso túi lọc Đà Lạt',       N'Đồ uống pha',     N'Hộp',   60000, 30000,  60, 10, 3, N'Đặc sản Đà Lạt, HSD 12 tháng'),
(N'Snack khoai tây Lay''s',         N'Đồ ăn nhẹ',       N'Gói',   25000, 13000, 120, 20, 1, N'Hạn dùng 6 tháng'),
(N'Rượu vang đỏ Đà Lạt 750ml',      N'Rượu cao cấp',    N'Chai', 220000,120000,  40,  8, 3, N'Hạn dùng 36 tháng'),
(N'Hạt điều rang muối 200g',        N'Đồ ăn nhẹ',       N'Hũ',    85000, 45000,  50, 10, 3, N'Hạn dùng 12 tháng'),
(N'Nước tăng lực Red Bull lon',     N'Nước giải khát',  N'Lon',   25000, 12500,  90, 15, 2, N'Hạn dùng 12 tháng'),
(N'Socola thanh KitKat 4 finger',   N'Bánh kẹo',        N'Thanh', 22000, 11000,  70, 15, 2, N'Hạn dùng 10 tháng'),
(N'Trà xanh C2 hương chanh 455ml',  N'Nước giải khát',  N'Chai',  15000,  7000, 110, 20, 1, N'Hạn dùng 9 tháng'),
(N'Bia Heineken lon 330ml',         N'Đồ uống có cồn',  N'Lon',   35000, 18000, 100, 20, 1, N'Hạn dùng 12 tháng'),
(N'Mứt dâu tây Đà Lạt 250g',        N'Đặc sản',         N'Hũ',    55000, 28000,  45, 10, 3, N'Hạn dùng 6 tháng');
GO

-- 6.10 Sảnh & khu vực sự kiện (Chỉ cho thuê và tính tiền theo giờ)
-- FIX: TrangThai giờ chỉ còn 'Hoạt động'/'Ngừng khai thác' (xem giải thích ở CREATE TABLE)
INSERT INTO KhuVucSuKien (TenKhuVuc, LoaiKhuVuc, SucChua, GiaThueTheoGio, TrangThietBi, TrangThai) VALUES
(N'Sảnh Đại Tiệc Grand Diamond',    N'Sảnh lớn',             250, 1500000, N'Âm thanh Line Array, màn LED P3, ánh sáng Moving Head, 25 bàn tiệc tròn', N'Hoạt động'),
(N'Phòng Hội Nghị VIP Ruby',        N'Phòng họp kín',         40,  500000, N'Bàn hội nghị chữ U, 2 máy chiếu laser 4K, 8 mic không dây',              N'Hoạt động'),
(N'Sky Lounge Sân Thượng Sunset',   N'Sân thượng ngoài trời',120, 1200000, N'Quầy bar cocktail, loa Bose ngoài trời, đèn LED dây vintage',            N'Hoạt động'),
(N'Phòng Trà Zen Garden',           N'Phòng nhỏ trong nhà',   20,  300000, N'Bàn trà gỗ, sân vườn nhỏ, phù hợp họp mặt gia đình',                     N'Hoạt động');
GO

-- 6.11 Danh mục 20 xe máy cho thuê (18 xe Hoạt động/sẵn sàng khai thác, 2 xe Bảo trì)
-- Trạng thái động thực tế (Đang thuê, Đang vệ sinh kiểm tra...) được vw_TrangThaiXeHienTai tính toán theo giờ thực
INSERT INTO XeChoThue (BienSo, TenXe, LoaiXe, GiaThueNgay, TienCocQuyDinh, TrangThai, GhiChuTinhTrang) VALUES
('59-F2 888.68', N'Honda SH 150i ABS 2024',     N'Xe tay ga', 300000, 1000000, N'Hoạt động', N'Xe mới 99%, kèm 2 mũ bảo hiểm cao cấp'),
('59-F2 777.23', N'Honda AirBlade 160 2023',    N'Xe tay ga', 180000,  500000, N'Hoạt động', N'Xe sạch đẹp, máy êm'),
('59-F2 555.45', N'Honda Vision Smartkey 2024', N'Xe tay ga', 150000,  500000, N'Hoạt động', N'Xe sạch đẹp, tiết kiệm xăng'),
('59-F2 333.12', N'Yamaha Exciter 155 VVA',     N'Xe côn tay',170000,  500000, N'Hoạt động', N'Xích sên đã bôi trơn bảo dưỡng'),
('59-F2 111.89', N'Honda Wave Alpha 110',       N'Xe số',     100000,  300000, N'Bảo trì',   N'Đang thay nhớt & bugi định kỳ'),
('59-F2 222.34', N'Honda Lead 125 Smartkey 2024',N'Xe tay ga',160000,  500000, N'Hoạt động', N'Cốp rộng, xe êm ái'),
('59-F2 444.56', N'Yamaha Grande Hybrid 2024', N'Xe tay ga', 170000,  500000, N'Hoạt động', N'Động cơ BlueCore Hybrid cực êm'),
('59-F2 666.78', N'Honda Future 125 Fi',        N'Xe số',     120000,  300000, N'Hoạt động', N'Phun xăng điện tử tiết kiệm nhiên liệu'),
('59-F2 999.01', N'Honda Winner X 150 ABS',     N'Xe côn tay',170000,  500000, N'Hoạt động', N'Phanh ABS an toàn, lốp mới'),
('59-F2 123.77', N'Vespa Primavera 125 ABS',    N'Xe tay ga', 280000, 1000000, N'Hoạt động', N'Phong cách thanh lịch, xe cao cấp'),
('59-F2 234.88', N'Yamaha NVX 155 VVA',         N'Xe tay ga', 190000,  500000, N'Hoạt động', N'Dáng thể thao hầm hố, máy bốc'),
('59-F2 345.99', N'Honda Blade 110',           N'Xe số',     100000,  300000, N'Hoạt động', N'Xe gọn nhẹ, đi đường dốc tốt'),
('59-F2 456.11', N'Piaggio Liberty 125 ABS',    N'Xe tay ga', 220000,  800000, N'Hoạt động', N'Bánh lớn đầm chắc, tôn dáng'),
('59-F2 567.22', N'Yamaha PG-1 115 Phượt',      N'Xe côn tay',150000,  500000, N'Hoạt động', N'Lốp gai địa hình, thích hợp cắm trại'),
('59-F2 678.33', N'Yamaha Sirius Fi 115',       N'Xe số',     100000,  300000, N'Hoạt động', N'Máy bốc bền bỉ, tiết kiệm xăng'),
('59-F2 789.44', N'Honda AirBlade 125 2024',    N'Xe tay ga', 160000,  500000, N'Hoạt động', N'Khóa thông minh Smartkey'),
('59-F2 890.55', N'Suzuki Raider R150 Fi',      N'Xe côn tay',180000,  500000, N'Bảo trì',   N'Đang cân chỉnh nhông sên dĩa'),
('59-F2 901.66', N'Honda Vario 160 ABS',        N'Xe tay ga', 200000,  600000, N'Hoạt động', N'Xe mới sạch đẹp, kèm 2 mũ bảo hiểm'),
('59-F2 012.77', N'Honda Wave RSX Fi 110',      N'Xe số',     110000,  300000, N'Hoạt động', N'Tiết kiệm xăng, máy êm'),
('59-F2 123.88', N'Yamaha Exciter 150 GP',      N'Xe côn tay',160000,  500000, N'Hoạt động', N'Xe nguyên bản, phanh đĩa trước sau');
GO

-- 6.12 Bảng giá bán dịch vụ giặt ủi chung (Khóa giá bán thống nhất áp dụng cho mọi khách)
INSERT INTO LoaiDichVuGiatUi (MaLoai, TenLoai, DonGiaBanKg, DonViTinh, HeSoBoiThuongToiDa, DangKinhDoanh) VALUES
('GIAT_SAY', N'Giặt sấy thông thường', 30000, N'Kg', 10, 1),
('GIAT_HAP', N'Giặt hấp cao cấp',     80000, N'Kg', 10, 1),
('UI_PHANG', N'Ủi phẳng',              40000, N'Kg', 10, 1);
GO

-- 6.13 Đối tác giặt ủi ngoài
INSERT INTO DoiTacGiatUi (TenDoiTac, DiaChi, SoDienThoai, HanThanhToanCongNo, TyLeKhachSanHuong, TyLeDoiTacHuong, TrangThai) VALUES
(N'Tiệm Giặt Sấy Tốc Hành EcoClean',        N'Số 12 Đường Hoa Cúc, P.7, Đà Lạt',    '02633445566', N'Theo tháng', 30.00, 70.00, 1),
(N'Xưởng Giặt Hấp Cao Cấp Royal Laundry',   N'Số 88 Nguyễn Chí Thanh, Đà Lạt',      '02633112233', N'Theo tháng', 35.00, 65.00, 1),
(N'Tiệm Giặt Ủi Nhanh Sương Mai',           N'Số 45 Trần Hưng Đạo, Đà Lạt',         '02633998877', N'Theo tháng', 25.00, 75.00, 0);
GO

-- 6.14 Bảng giá vốn đối tác giặt ủi (Mỗi đối tác x mỗi loại dịch vụ = đơn giá vốn riêng, luôn NHỎ HƠN giá niêm yết)
INSERT INTO BangGiaDoiTacGiatUi (DoiTacId, LoaiDichVuId, DonGiaVonKg, GhiChu) VALUES
-- 1. Tiệm Giặt Sấy Tốc Hành EcoClean (Id = 1): Chuyên giặt sấy dân dụng
(1, 1, 21000, N'Giá vốn giặt sấy 21k/kg (Giá bán 30k -> KS lời 9k/kg ~ 30%)'),
(1, 2, 56000, N'Giá vốn giặt hấp 56k/kg (Giá bán 80k -> KS lời 24k/kg ~ 30%)'),
(1, 3, 28000, N'Giá vốn ủi phẳng 28k/kg (Giá bán 40k -> KS lời 12k/kg ~ 30%)'),

-- 2. Xưởng Giặt Hấp Cao Cấp Royal Laundry (Id = 2): Chuyên đồ vest, đầm lụa, chiết khấu hấp dẫn
(2, 1, 20000, N'Giá vốn giặt sấy 20k/kg (Giá bán 30k -> KS lời 10k/kg ~ 33.3%)'),
(2, 2, 52000, N'Giá vốn giặt hấp 52k/kg (Giá bán 80k -> KS lời 28k/kg ~ 35%)'),
(2, 3, 26000, N'Giá vốn ủi phẳng 26k/kg (Giá bán 40k -> KS lời 14k/kg ~ 35%)'),

-- 3. Tiệm Giặt Ủi Nhanh Sương Mai (Id = 3): Đã ngừng hợp tác (Lưu dữ liệu lịch sử đối soát)
(3, 1, 22500, N'Giá vốn giặt sấy cũ lúc còn hợp tác (22.5k/kg)'),
(3, 2, 60000, N'Giá vốn giặt hấp cũ lúc còn hợp tác (60k/kg)'),
(3, 3, 30000, N'Giá vốn ủi phẳng cũ lúc còn hợp tác (30k/kg)');
GO


-- =====================================================================================
-- PHẦN 7: DỮ LIỆU MẪU GIAO DỊCH PHÁT SINH (TRANSACTIONS & OPERATIONAL SAMPLE DATA)
-- =====================================================================================

-- 7.1 Khách hàng mẫu (14 khách lưu trú phòng + khách vãng lai)
INSERT INTO KhachHang (HoTen, CCCD, SoDienThoai, SoPhong, GhiChu, NgayTao) VALUES
(N'Đoàn Minh Trí',     '079198001234', '0908112233', '101', N'Khách lưu trú phòng 101 (2 ngày, thuê xe SH)', GETDATE()),
(N'Trịnh Hồng Quân',   '079192003344', '0909123456', '102', N'Khách lưu trú phòng 102 (3 ngày, thuê xe Lead)', GETDATE()),
(N'Vũ Hoàng Nam',      '079193005566', '0912334455', '103', N'Khách lưu trú phòng 103 (2 tuần, thuê xe Grande)', GETDATE()),
(N'Hoàng Mai Linh',    '079194007788', '0978112244', '201', N'Khách lưu trú phòng 201 (2 ngày, thuê xe Future)', GETDATE()),
(N'Lê Tấn Phát',       '079201009876', '0969888999', '202', N'Khách lưu trú phòng 202 (1 tuần, thuê xe AirBlade)', GETDATE()),
(N'Bùi Thanh Tùng',    '079196001122', '0938445566', '203', N'Khách lưu trú phòng 203 (2 tuần, thuê xe Winner X)', GETDATE()),
(N'Nguyễn Anh Thư',    '079195003456', '0918777666', '301', N'Khách lưu trú phòng 301 (2 tuần honeymoon, thuê xe Vision)', GETDATE()),
(N'Đặng Hải Yến',      '079197003355', '0982334411', '302', N'Khách lưu trú phòng 302 (3 ngày, thuê xe Vespa)', GETDATE()),
(N'Phan Văn Đức',      '079199004466', '0945667788', '303', N'Khách lưu trú phòng 303 (2 tuần, thuê xe NVX)', GETDATE()),
(N'Phạm Quốc Bảo',     '079188005678', '0933444555', '401', N'Khách lưu trú phòng 401 (2 tuần, gửi ô tô 7 chỗ)', GETDATE()),
(N'Trần Thu Hà',       '079189006677', '0919223344', '402', N'Khách lưu trú phòng 402 (3 ngày, thuê xe Liberty)', GETDATE()),
(N'Đỗ Minh Khang',     '079187008899', '0987556677', '403', N'Khách lưu trú phòng 403 (1 tuần, thuê xe Exciter)', GETDATE()),
(N'Nguyễn Hoàng Long', '079186001234', '0903889900', '501', N'Khách lưu trú phòng 501 (2 tuần, thuê xe PG-1)', GETDATE()),
(N'Lê Thị Ngọc Ánh',   '079185002345', '0979334455', '502', N'Khách lưu trú phòng 502 (1 tuần, thuê xe Sirius)', GETDATE()),
(N'Công ty TechCorp',  '0312999888',   '0903999888', NULL,  N'Khách vãng lai tổ chức sự kiện',   GETDATE()),
(N'Nguyễn Văn Khách',  '079190001111', '0988112233', NULL,  N'Khách vãng lai thuê xe',           GETDATE());
GO

-- 7.2 Phiếu nhập kho mẫu
INSERT INTO PhieuNhapKho (MaPhieuNhap, NhaCungCapId, NguoiNhapId, NgayNhap, GhiChu) VALUES
('PN-20260825-01', 1, 1, DATEADD(DAY,-6,GETDATE()), N'Nhập hàng định kỳ đầu tuần từ Masan'),
('PN-20260827-01', 1, 2, DATEADD(DAY,-4,GETDATE()), N'Nhập bổ sung nước khoáng và Coca Cola'),
('PN-20260829-01', 2, 1, DATEADD(DAY,-2,GETDATE()), N'Nhập cà phê G7 và socola từ Nestlé'),
('PN-20260830-01', 3, 3, DATEADD(DAY,-1,GETDATE()), N'Nhập rượu vang và đặc sản Đà Lạt');
GO

INSERT INTO ChiTietPhieuNhapKho (PhieuNhapId, MonAnId, DonViNhap, HeSoQuyDoi, SoLuongNhap, DonGiaNhap, SoLo, HanSuDung, GhiChu) VALUES
(1, 1, N'Thùng', 30, 5,  330000, N'MO-2608-A', DATEADD(MONTH,8,GETDATE()),  N'1 thùng = 30 gói'),
(1, 6, N'Thùng', 24, 3,  480000, N'LAY-2608-B', DATEADD(MONTH,6,GETDATE()), N'1 thùng = 24 gói'),
(2, 2, N'Thùng', 24, 10, 144000, N'LAV-2608-C', DATEADD(MONTH,12,GETDATE()),N'1 thùng = 24 chai 500ml'),
(2, 3, N'Thùng', 24, 8,  240000, N'COCA-2608-D',DATEADD(MONTH,9,GETDATE()), N'1 thùng = 24 lon'),
(3, 4, N'Hộp',   1,  20, 22000,  N'G7-2608-E',  DATEADD(MONTH,10,GETDATE()),N'Nhập lẻ theo hộp, không quy đổi'),
(4, 7, N'Thùng', 6,  5,  720000, N'VANG-2608-F',DATEADD(DAY,10,GETDATE()), N'CẢNH BÁO: còn 10 ngày là hết hạn, cần ưu tiên bán trước (FIFO)');
GO

-- 7.3 Đơn hàng ẩm thực & minibar mẫu (Các đơn quá khứ trước hôm nay đã Hoàn thành & Thanh toán hết; kèm đơn phát sinh mới Hôm nay & Ngày mai)
INSERT INTO DonHangMonAn (MaDon, KhachHangId, TenKhach, SoPhong, HinhThucThanhToan, TongTien, DaThanhToan, GhiChu, NguoiTaoId, NguoiThanhToanId, ThoiGianThanhToan, NgayTao, TrangThai) VALUES
-- Dữ liệu mẫu đã qua (Trước ngày hôm nay -> Đã giao & Đã thanh toán hết để thống kê)
('DH-AU-001', 1, N'Đoàn Minh Trí', '101', N'Ghi nợ vào phòng', 35000, 1, N'1 Coca + 1 Lavie (Đã quyết toán)', 1, 1, DATEADD(DAY,-10,GETDATE()), DATEADD(DAY,-10,GETDATE()), N'Đã giao'),
('DH-AU-002', 1, N'Đoàn Minh Trí', '101', N'Thanh toán trực tiếp', 45000, 1, N'1 Cà phê G7 (Khách trả tiền ngay)', 1, 1, DATEADD(DAY,-9,GETDATE()), DATEADD(DAY,-9,GETDATE()), N'Đã giao'),
('DH-AU-003', 2, N'Trịnh Hồng Quân', '102', N'Ghi nợ vào phòng', 60000, 1, N'2 Mì Modern + 1 Coca (Đã quyết toán)', 2, 2, DATEADD(DAY,-9,GETDATE()), DATEADD(DAY,-9,GETDATE()), N'Đã giao'),
('DH-AU-004', 3, N'Vũ Hoàng Nam', '103', N'Ghi nợ vào phòng', 220000, 1, N'1 Rượu vang đỏ Đà Lạt (Đã quyết toán)', 1, 1, DATEADD(DAY,-8,GETDATE()), DATEADD(DAY,-8,GETDATE()), N'Đã giao'),
('DH-AU-005', 4, N'Hoàng Mai Linh', '201', N'Thanh toán trực tiếp', 85000, 1, N'1 Hạt điều rang muối', 2, 2, DATEADD(DAY,-8,GETDATE()), DATEADD(DAY,-8,GETDATE()), N'Đã giao'),
('DH-AU-006', 5, N'Lê Tấn Phát', '202', N'Ghi nợ vào phòng', 110000, 1, N'3 Mì tôm ly + 2 Snack khoai tây (Đã quyết toán)', 2, 2, DATEADD(DAY,-7,GETDATE()), DATEADD(DAY,-7,GETDATE()), N'Đã giao'),
('DH-AU-007', 6, N'Bùi Thanh Tùng', '203', N'Ghi nợ vào phòng', 70000, 1, N'2 Bia Heineken ướp lạnh (Đã quyết toán)', 3, 3, DATEADD(DAY,-7,GETDATE()), DATEADD(DAY,-7,GETDATE()), N'Đã giao'),
('DH-AU-008', 7, N'Nguyễn Anh Thư', '301', N'Ghi nợ vào phòng', 275000, 1, N'1 Rượu vang + 1 Mứt dâu tây (Đã quyết toán)', 3, 3, DATEADD(DAY,-6,GETDATE()), DATEADD(DAY,-6,GETDATE()), N'Đã giao'),
('DH-AU-009', 8, N'Đặng Hải Yến', '302', N'Ghi nợ vào phòng', 90000, 1, N'2 Coca + 2 Snack khoai tây (Đã quyết toán)', 1, 1, DATEADD(DAY,-6,GETDATE()), DATEADD(DAY,-6,GETDATE()), N'Đã giao'),
('DH-AU-010', 9, N'Phan Văn Đức', '303', N'Ghi nợ vào phòng', 105000, 1, N'3 Bia Heineken lon (Đã quyết toán)', 2, 2, DATEADD(DAY,-5,GETDATE()), DATEADD(DAY,-5,GETDATE()), N'Đã giao'),
('DH-AU-011', 10, N'Phạm Quốc Bảo', '401', N'Ghi nợ vào phòng', 80000, 1, N'1 Trà Atiso + 1 Coca (Đã quyết toán)', 1, 1, DATEADD(DAY,-5,GETDATE()), DATEADD(DAY,-5,GETDATE()), N'Đã giao'),
('DH-AU-012', 11, N'Trần Thu Hà', '402', N'Ghi nợ vào phòng', 50000, 0, N'2 Nước tăng lực Red Bull (Khách hủy)', 2, NULL, NULL, DATEADD(DAY,-4,GETDATE()), N'Đã hủy'),
('DH-AU-013', 12, N'Đỗ Minh Khang', '403', N'Ghi nợ vào phòng', 60000, 1, N'1 Red Bull + 1 Mì + 1 Lavie (Đã quyết toán)', 2, 2, DATEADD(DAY,-4,GETDATE()), DATEADD(DAY,-4,GETDATE()), N'Đã giao'),
('DH-AU-014', 13, N'Nguyễn Hoàng Long', '501', N'Ghi nợ vào phòng', 220000, 1, N'1 Rượu vang đỏ Đà Lạt (Đã quyết toán)', 1, 1, DATEADD(DAY,-3,GETDATE()), DATEADD(DAY,-3,GETDATE()), N'Đã giao'),
('DH-AU-015', 14, N'Lê Thị Ngọc Ánh', '502', N'Ghi nợ vào phòng', 110000, 1, N'2 Hũ Mứt dâu tây Đà Lạt (Đã quyết toán)', 3, 3, DATEADD(DAY,-3,GETDATE()), DATEADD(DAY,-3,GETDATE()), N'Đã giao'),
('DH-AU-016', 1, N'Đoàn Minh Trí', '101', N'Ghi nợ vào phòng', 40000, 1, N'2 Mì Modern ăn sáng (Đã quyết toán)', 1, 1, DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-2,GETDATE()), N'Đã giao'),
('DH-AU-017', 4, N'Hoàng Mai Linh', '201', N'Ghi nợ vào phòng', 55000, 1, N'1 Mứt dâu tây ăn bánh mì (Đã quyết toán)', 2, 2, DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-2,GETDATE()), N'Đã giao'),
('DH-AU-018', 7, N'Nguyễn Anh Thư', '301', N'Thanh toán trực tiếp', 140000, 1, N'2 Bia Heineken + 1 Hạt điều', 1, 1, DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-2,GETDATE()), N'Đã giao'),
('DH-AU-019', 9, N'Phan Văn Đức', '303', N'Ghi nợ vào phòng', 60000, 1, N'1 Trà Atiso + 1 Snack Lay''s (Đã quyết toán)', 2, 2, DATEADD(DAY,-1,GETDATE()), DATEADD(DAY,-1,GETDATE()), N'Đã giao'),
('DH-AU-020', 10, N'Phạm Quốc Bảo', '401', N'Ghi nợ vào phòng', 35000, 1, N'1 Coca + 1 Nước khoáng Lavie (Đã quyết toán)', 1, 1, DATEADD(DAY,-1,GETDATE()), DATEADD(DAY,-1,GETDATE()), N'Đã giao'),
('DH-AU-021', 13, N'Nguyễn Hoàng Long', '501', N'Ghi nợ vào phòng', 45000, 1, N'1 Cà phê G7 họp online (Đã quyết toán)', 3, 3, DATEADD(DAY,-1,GETDATE()), DATEADD(DAY,-1,GETDATE()), N'Đã giao'),
-- Giao dịch phát sinh MỚI Hôm nay (GETDATE()) và Ngày mai (DATEADD(DAY,1,GETDATE()))
('DH-AU-022', 1, N'Đoàn Minh Trí', '101', N'Ghi nợ vào phòng', 55000, 0, N'1 Mì Modern + 1 Red Bull + 1 Lavie sáng nay', 1, NULL, NULL, DATEADD(HOUR,-2,GETDATE()), N'Đã giao'),
('DH-AU-023', 2, N'Trịnh Hồng Quân', '102', N'Thanh toán trực tiếp', 90000, 1, N'2 Cà phê G7 (Khách thanh toán tại quầy sáng nay)', 1, 1, DATEADD(HOUR,-2,GETDATE()), DATEADD(HOUR,-2,GETDATE()), N'Đã giao'),
('DH-AU-024', 3, N'Vũ Hoàng Nam', '103', N'Ghi nợ vào phòng', 290000, 0, N'1 Rượu vang đỏ Đà Lạt + 2 Bia Heineken hôm nay', 2, NULL, NULL, DATEADD(HOUR,-1,GETDATE()), N'Đã giao'),
('DH-AU-025', 5, N'Lê Tấn Phát', '202', N'Ghi nợ vào phòng', 85000, 0, N'1 Hạt điều rang muối 200g dùng tại phòng', 2, NULL, NULL, DATEADD(MINUTE,-40,GETDATE()), N'Đang chuẩn bị'),
('DH-AU-026', 7, N'Nguyễn Anh Thư', '301', N'Ghi nợ vào phòng', 115000, 0, N'1 Trà Atiso + 1 Mứt dâu tây Đà Lạt', 1, NULL, NULL, DATEADD(MINUTE,-25,GETDATE()), N'Đã giao'),
('DH-AU-027', 8, N'Đặng Hải Yến', '302', N'Thanh toán trực tiếp', 70000, 1, N'2 Snack khoai tây Lay''s + 1 Coca Cola', 2, 2, DATEADD(MINUTE,-20,GETDATE()), DATEADD(MINUTE,-20,GETDATE()), N'Đã giao'),
('DH-AU-028', 10, N'Phạm Quốc Bảo', '401', N'Ghi nợ vào phòng', 140000, 0, N'4 Bia Heineken ướp lạnh gọi lên phòng', 1, NULL, NULL, DATEADD(MINUTE,-10,GETDATE()), N'Chờ xử lý'),
('DH-AU-029', 12, N'Đỗ Minh Khang', '403', N'Ghi nợ vào phòng', 65000, 0, N'2 Mì tôm ly + 1 Nước tăng lực Red Bull', 3, NULL, NULL, DATEADD(MINUTE,-5,GETDATE()), N'Chờ xử lý'),
('DH-AU-030', 6, N'Bùi Thanh Tùng', '203', N'Ghi nợ vào phòng', 280000, 0, N'Đặt trước 1 Rượu vang đỏ + 1 Trà Atiso cho tiệc ngày mai', 1, NULL, NULL, DATEADD(DAY,1,GETDATE()), N'Chờ xử lý'),
('DH-AU-031', 13, N'Nguyễn Hoàng Long', '501', N'Thanh toán trực tiếp', 130000, 1, N'Đặt combo Cà phê G7 + Hạt điều cho sáng ngày mai', 2, 2, GETDATE(), DATEADD(DAY,1,GETDATE()), N'Đã giao');
GO


INSERT INTO ChiTietDonHangMonAn (DonHangId, MonAnId, SoLuong, DonGia, GiaVon, GhiChu) VALUES
(1, 3, 1, 20000, 10000, N'Coca Cola lon'),
(1, 2, 1, 15000, 6000, N'Nước khoáng Lavie'),
(2, 4, 1, 45000, 22000, N'Cà phê G7 hộp 20 gói'),
(3, 1, 2, 20000, 11000, N'Mì Modern ly'),
(3, 3, 1, 20000, 10000, N'Coca Cola lon'),
(4, 7, 1, 220000, 120000, N'Rượu vang đỏ Đà Lạt'),
(5, 8, 1, 85000, 45000, N'Hạt điều rang muối 200g'),
(6, 1, 3, 20000, 11000, N'Mì Modern ly'),
(6, 6, 2, 25000, 13000, N'Snack khoai tây Lay''s'),
(7, 12, 2, 35000, 18000, N'Bia Heineken lon'),
(8, 7, 1, 220000, 120000, N'Rượu vang đỏ Đà Lạt'),
(8, 13, 1, 55000, 28000, N'Mứt dâu tây 250g'),
(9, 3, 2, 20000, 10000, N'Coca Cola lon'),
(9, 6, 2, 25000, 13000, N'Snack khoai tây Lay''s'),
(10, 12, 3, 35000, 18000, N'Bia Heineken lon'),
(11, 5, 1, 60000, 30000, N'Trà Atiso túi lọc'),
(11, 3, 1, 20000, 10000, N'Coca Cola lon'),
(12, 9, 2, 25000, 12500, N'Nước tăng lực Red Bull'),
(13, 9, 1, 25000, 12500, N'Nước tăng lực Red Bull'),
(13, 1, 1, 20000, 11000, N'Mì Modern ly'),
(13, 2, 1, 15000, 6000, N'Nước khoáng Lavie'),
(14, 7, 1, 220000, 120000, N'Rượu vang đỏ Đà Lạt'),
(15, 13, 2, 55000, 28000, N'Mứt dâu tây 250g'),
(16, 1, 2, 20000, 11000, N'Mì Modern ly'),
(17, 13, 1, 55000, 28000, N'Mứt dâu tây 250g'),
(18, 12, 2, 35000, 18000, N'Bia Heineken lon'),
(18, 8, 1, 70000, 45000, N'Hạt điều rang muối'),
(19, 5, 1, 35000, 20000, N'Trà Atiso túi lọc'),
(19, 6, 1, 25000, 13000, N'Snack khoai tây Lay''s'),
(20, 3, 1, 20000, 10000, N'Coca Cola lon'),
(20, 2, 1, 15000, 6000, N'Nước khoáng Lavie'),
(21, 4, 1, 45000, 22000, N'Cà phê G7 hộp 20 gói'),
-- Chi tiết cho các đơn mới Hôm nay & Ngày mai (DonHangId 22 -> 31)
(22, 1, 1, 20000, 11000, N'Mì Modern ly'),
(22, 9, 1, 25000, 12500, N'Nước tăng lực Red Bull'),
(22, 2, 1, 10000, 6000, N'Nước khoáng Lavie'),
(23, 4, 2, 45000, 22000, N'Cà phê G7 hộp 20 gói'),
(24, 7, 1, 220000, 120000, N'Rượu vang đỏ Đà Lạt'),
(24, 12, 2, 35000, 18000, N'Bia Heineken lon'),
(25, 8, 1, 85000, 45000, N'Hạt điều rang muối 200g'),
(26, 5, 1, 60000, 30000, N'Trà Atiso túi lọc Đà Lạt'),
(26, 13, 1, 55000, 28000, N'Mứt dâu tây Đà Lạt 250g'),
(27, 6, 2, 25000, 13000, N'Snack khoai tây Lay''s'),
(27, 3, 1, 20000, 10000, N'Coca Cola lon'),
(28, 12, 4, 35000, 18000, N'Bia Heineken lon'),
(29, 1, 2, 20000, 11000, N'Mì Modern ly'),
(29, 9, 1, 25000, 12500, N'Nước tăng lực Red Bull'),
(30, 7, 1, 220000, 120000, N'Rượu vang đỏ Đà Lạt'),
(30, 5, 1, 60000, 30000, N'Trà Atiso túi lọc'),
(31, 4, 1, 45000, 22000, N'Cà phê G7 hộp 20 gói'),
(31, 8, 1, 85000, 45000, N'Hạt điều rang muối 200g');
GO

-- 7.4 Đơn đặt sảnh & sự kiện mẫu (Các sự kiện quá khứ đã Hoàn tất & quyết toán; Hôm nay có Đang sử dụng; Từ ngày mai đến hết tháng 9 là Đã đặt)
INSERT INTO DonDatSuKien (
    MaDon, KhuVucId, KhachHangId, TenKhach, SoDienThoai, SoPhong,
    ThoiGianBatDau, ThoiGianKetThuc, ThoiGianDonDepPhut, PhuThuRutNganDonDep, GhiChuRutNganDonDep,
    TienCoc, TongTienDuKien, ChiPhiPhatSinh, DaThanhToan, GhiChuHuHai,
    HinhThucThanhToanBanDau, TrangThaiDon,
    ThoiGianHuy, LyDoHuy, NguoiHuyId, SoTienHoanCoc,
    NguoiTaoId, NguoiThanhToanId, ThoiGianThanhToan, NgayTao
) VALUES
('SK-2026-001', 1, 7, N'Nguyễn Anh Thư', '0918777666', '301', DATEADD(HOUR,8,DATEADD(DAY,-35,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,13,DATEADD(DAY,-35,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2250000, 7500000, 0, 1, N'Đại tiệc kỷ niệm gia đình, phục vụ chu đáo', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,13,DATEADD(DAY,-35,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-38,GETDATE())),
('SK-2026-002', 2, 15, N'Công ty TechCorp', '0903999888', NULL, DATEADD(HOUR,13,DATEADD(DAY,-33,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,17,DATEADD(DAY,-33,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2000000, 2000000, 0, 1, N'Họp HĐQT quý 3 đã xong', N'Thanh toán toàn bộ', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,17,DATEADD(DAY,-33,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-36,GETDATE())),
('SK-2026-003', 3, 1, N'Đoàn Minh Trí', '0908112233', '101', DATEADD(HOUR,17,DATEADD(DAY,-30,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-30,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1800000, 6000000, 0, 1, N'Tiệc sinh nhật Acoustic hoàng hôn', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 2, 2, DATEADD(HOUR,22,DATEADD(DAY,-30,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-33,GETDATE())),
('SK-2026-004', 4, 8, N'Đặng Hải Yến', '0982334411', '302', DATEADD(HOUR,8,DATEADD(DAY,-28,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,11,DATEADD(DAY,-28,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 270000, 900000, 0, 1, N'Thưởng trà sáng', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,11,DATEADD(DAY,-28,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-30,GETDATE())),
('SK-2026-005', 1, 15, N'Tập đoàn Vingroup', '0909555666', NULL, DATEADD(HOUR,9,DATEADD(DAY,-25,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,16,DATEADD(DAY,-25,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 3150000, 10500000, 0, 0, N'Khách hoãn hội thảo sang quý 4', N'Đặt cọc trước', N'Đã hủy - Hoàn cọc', DATEADD(DAY,-28,CAST(CAST(GETDATE() AS DATE) AS DATETIME)), N'Khách thông báo hủy trước 3 ngày, hoàn cọc 100% theo quy định', 1, 3150000, 1, NULL, NULL, DATEADD(DAY,-31,GETDATE())),
('SK-2026-006', 2, 6, N'Bùi Thanh Tùng', '0938445566', '203', DATEADD(HOUR,8,DATEADD(DAY,-23,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,13,DATEADD(DAY,-23,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), 30, 250000, N'Dọn dẹp cấp tốc 30 phút phục vụ đoàn đại biểu', 750000, 2500000, 0, 1, N'Hội thảo khoa học công nghệ', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 2, 2, DATEADD(HOUR,13,DATEADD(DAY,-23,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-25,GETDATE())),
('SK-2026-007', 4, 15, N'CLB Thư Pháp Trà Đạo', '0914223355', NULL, DATEADD(HOUR,14,DATEADD(DAY,-21,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-21,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 450000, 1200000, 0, 0, N'Khách báo hủy đột xuất trước giờ bắt đầu', N'Đặt cọc trước', N'Đã hủy - Không hoàn cọc', DATEADD(HOUR,11,DATEADD(DAY,-21,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), N'Khách báo hủy đột xuất trước giờ bắt đầu 3 tiếng, không hoàn cọc theo quy chế', 2, 0, 2, NULL, NULL, DATEADD(DAY,-23,GETDATE())),
('SK-2026-008', 1, 15, N'Tập đoàn Hoa Sen Tri Ân', '0907123987', NULL, DATEADD(HOUR,17,DATEADD(DAY,-19,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,23,DATEADD(DAY,-19,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2700000, 9000000, 500000, 1, N'Khách làm vỡ 2 ly rượu pha lê cao cấp, đã bồi thường 500.000đ', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,23,DATEADD(DAY,-19,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-21,GETDATE())),
('SK-2026-009', 3, 15, N'Live Chill Rooftop', '0916334455', NULL, DATEADD(HOUR,16,DATEADD(DAY,-18,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-18,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2160000, 7200000, 0, 1, N'Live Chill Rooftop chào mừng lễ', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-18,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-20,GETDATE())),
('SK-2026-010', 2, 5, N'Lê Tấn Phát', '0969888999', '202', DATEADD(HOUR,8,DATEADD(DAY,-17,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,12,DATEADD(DAY,-17,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2000000, 2000000, 0, 1, N'Họp chiến lược kinh doanh tháng 9', N'Thanh toán toàn bộ', N'Hoàn tất', NULL, NULL, NULL, NULL, 2, 2, DATEADD(HOUR,12,DATEADD(DAY,-17,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-19,GETDATE())),
('SK-2026-011', 4, 10, N'Phạm Quốc Bảo', '0933444555', '401', DATEADD(HOUR,8,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,11,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 270000, 900000, 0, 1, N'Giao lưu trà đạo nghệ thuật', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,11,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-18,GETDATE())),
('SK-2026-012', 1, 15, N'Ngân hàng Vietcombank', '0912888999', NULL, DATEADD(HOUR,14,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1800000, 6000000, 0, 1, N'Tiệc tri ân đối tác đầu tháng', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-18,GETDATE())),
('SK-2026-013', 1, 15, N'Tập đoàn Vingroup', '0909555666', NULL, DATEADD(HOUR,8,DATEADD(DAY,-25,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,17,DATEADD(DAY,-25,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 4050000, 13500000, 0, 1, N'Hội thảo khách hàng toàn quốc', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,17,DATEADD(DAY,-25,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-27,GETDATE())),
('SK-2026-014', 2, 5, N'Lê Tấn Phát', '0969888999', '202', DATEADD(HOUR,13,DATEADD(DAY,-25,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-25,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 750000, 2500000, 0, 1, N'Ký kết hợp đồng đối tác', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-25,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-27,GETDATE())),
('SK-2026-015', 3, 15, N'Ngân hàng Vietcombank', '0912888999', NULL, DATEADD(HOUR,16,DATEADD(DAY,-25,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-25,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2160000, 7200000, 0, 1, N'Gala Dinner tri ân khách VIP', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-25,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-27,GETDATE())),
('SK-2026-016', 4, 10, N'Phạm Quốc Bảo', '0933444555', '401', DATEADD(HOUR,14,DATEADD(DAY,-25,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,17,DATEADD(DAY,-25,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 900000, 900000, 0, 1, N'Giao lưu trà đạo nghệ thuật', N'Thanh toán toàn bộ', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,17,DATEADD(DAY,-25,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-27,GETDATE())),
('SK-2026-017', 1, 15, N'Tiệc cưới Hoàng Gia', '0988777111', NULL, DATEADD(HOUR,9,DATEADD(DAY,-24,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,16,DATEADD(DAY,-24,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 3150000, 10500000, 0, 1, N'Tiệc cưới trọn gói cao cấp', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,16,DATEADD(DAY,-24,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-26,GETDATE())),
('SK-2026-018', 2, 6, N'Bùi Thanh Tùng', '0938445566', '203', DATEADD(HOUR,8,DATEADD(DAY,-24,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,13,DATEADD(DAY,-24,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 750000, 2500000, 0, 1, N'Họp chi nhánh công ty', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,13,DATEADD(DAY,-24,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-26,GETDATE())),
('SK-2026-019', 3, 15, N'CLB Doanh Nhân Trẻ', '0902334455', NULL, DATEADD(HOUR,17,DATEADD(DAY,-24,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,23,DATEADD(DAY,-24,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2160000, 7200000, 0, 1, N'Cocktail Networking Night', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,23,DATEADD(DAY,-24,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-26,GETDATE())),
('SK-2026-020', 4, 2, N'Trịnh Hồng Quân', '0909123456', '102', DATEADD(HOUR,9,DATEADD(DAY,-24,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,15,DATEADD(DAY,-24,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 540000, 1800000, 0, 1, N'Họp mặt trà đạo', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,15,DATEADD(DAY,-24,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-26,GETDATE())),
('SK-2026-021', 1, 15, N'Tiệc cưới Trọng Tấn', '0988777222', NULL, DATEADD(HOUR,10,DATEADD(DAY,-23,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,16,DATEADD(DAY,-23,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2700000, 9000000, 0, 1, N'Tiệc cưới phong cách Đà Lạt', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,16,DATEADD(DAY,-23,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-25,GETDATE())),
('SK-2026-022', 2, 3, N'Vũ Hoàng Nam', '0912334455', '103', DATEADD(HOUR,14,DATEADD(DAY,-23,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,19,DATEADD(DAY,-23,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), 30, 300000, N'Phụ thu dọn dẹp cấp tốc 30 phút phục vụ đoàn đại biểu', 750000, 2500000, 0, 1, N'Hội thảo nhóm chuyên gia', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,19,DATEADD(DAY,-23,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-25,GETDATE())),
('SK-2026-023', 3, 15, N'Sunset Acoustic Night', '0903112244', NULL, DATEADD(HOUR,16,DATEADD(DAY,-23,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-23,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2160000, 7200000, 0, 1, N'Đêm nhạc acoustic ngắm hoàng hôn', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-23,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-25,GETDATE())),
('SK-2026-024', 4, 15, N'CLB Thư Pháp Trà Đạo', '0914223355', NULL, DATEADD(HOUR,8,DATEADD(DAY,-23,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,13,DATEADD(DAY,-23,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1500000, 1500000, 0, 1, N'Giao lưu thư pháp cuối tuần', N'Thanh toán toàn bộ', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,13,DATEADD(DAY,-23,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-25,GETDATE())),
('SK-2026-025', 1, 15, N'FPT Software Đà Lạt', '0908112345', NULL, DATEADD(HOUR,8,DATEADD(DAY,-22,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-22,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 4500000, 15000000, 0, 1, N'Tech Summit & Triển lãm AI', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-22,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-24,GETDATE())),
('SK-2026-026', 2, 4, N'Hoàng Mai Linh', '0978112244', '201', DATEADD(HOUR,8,DATEADD(DAY,-22,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,12,DATEADD(DAY,-22,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 600000, 2000000, 0, 1, N'Phỏng vấn nhân sự dự án', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,12,DATEADD(DAY,-22,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-24,GETDATE())),
('SK-2026-027', 3, 13, N'Nguyễn Hoàng Long', '0903889900', '501', DATEADD(HOUR,17,DATEADD(DAY,-22,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-22,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1800000, 6000000, 0, 1, N'Tiệc mừng thọ gia đình', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-22,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-24,GETDATE())),
('SK-2026-028', 4, 12, N'Đỗ Minh Khang', '0987556677', '403', DATEADD(HOUR,13,DATEADD(DAY,-22,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-22,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 450000, 1500000, 0, 1, N'Họp mặt nhóm bạn học', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-22,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-24,GETDATE())),
('SK-2026-029', 1, 15, N'Gala Tri Ân Dược Hậu Giang', '0913998877', NULL, DATEADD(HOUR,16,DATEADD(DAY,-21,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-21,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2700000, 9000000, 0, 0, N'Khách chuyển địa điểm tổ chức ra Hà Nội', N'Đặt cọc trước', N'Đã hủy - Hoàn cọc', DATEADD(DAY,-21,GETDATE()), N'Khách báo hủy trước 5 ngày, hoàn 100% tiền cọc', 1, 2700000, 1, NULL, NULL, DATEADD(DAY,-23,GETDATE())),
('SK-2026-030', 2, 15, N'Hội thảo Y Khoa Lâm Đồng', '0914556677', NULL, DATEADD(HOUR,8,DATEADD(DAY,-21,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,17,DATEADD(DAY,-21,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1350000, 4500000, 0, 1, N'Hội thảo cập nhật kiến thức y khoa', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,17,DATEADD(DAY,-21,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-23,GETDATE())),
('SK-2026-031', 3, 15, N'Rooftop DJ Night', '0908778899', NULL, DATEADD(HOUR,18,DATEADD(DAY,-21,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,23,DATEADD(DAY,-21,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1800000, 6000000, 0, 1, N'Đêm nhạc EDM ngoài trời', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,23,DATEADD(DAY,-21,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-23,GETDATE())),
('SK-2026-032', 4, 14, N'Lê Thị Ngọc Ánh', '0979334455', '502', DATEADD(HOUR,9,DATEADD(DAY,-21,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,14,DATEADD(DAY,-21,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1500000, 1500000, 0, 1, N'Tiệc trà sinh nhật thân mật', N'Thanh toán toàn bộ', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,14,DATEADD(DAY,-21,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-23,GETDATE())),
('SK-2026-033', 1, 15, N'Diễn đàn Đổi mới Sáng tạo', '0903221199', NULL, DATEADD(HOUR,8,DATEADD(DAY,-20,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,16,DATEADD(DAY,-20,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 3600000, 12000000, 0, 1, N'Hội nghị xúc tiến khởi nghiệp xanh', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,16,DATEADD(DAY,-20,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-22,GETDATE())),
('SK-2026-034', 2, 9, N'Phan Văn Đức', '0945667788', '303', DATEADD(HOUR,13,DATEADD(DAY,-20,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-20,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 750000, 2500000, 0, 1, N'Họp bàn dự án kinh doanh', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-20,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-22,GETDATE())),
('SK-2026-035', 3, 15, N'Live Chill Rooftop', '0916334455', NULL, DATEADD(HOUR,16,DATEADD(DAY,-20,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-20,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2160000, 7200000, 0, 1, N'Đêm ngắm sao và thưởng thức đồ uống', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-20,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-22,GETDATE())),
('SK-2026-036', 4, 11, N'Trần Thu Hà', '0919223344', '402', DATEADD(HOUR,14,DATEADD(DAY,-20,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-20,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 360000, 1200000, 0, 1, N'Trà chiều cùng gia đình', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-20,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-22,GETDATE())),
('SK-2026-037', 1, 15, N'Hội nghị Xúc tiến Du lịch 2026', '0914667788', NULL, DATEADD(HOUR,8,DATEADD(DAY,-19,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,15,DATEADD(DAY,-19,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 4050000, 13500000, 0, 1, N'Hội nghị du lịch các tỉnh Tây Nguyên', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,15,DATEADD(DAY,-19,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-21,GETDATE())),
('SK-2026-038', 2, 15, N'Hiệp hội Du lịch Lâm Đồng', '0914667788', NULL, DATEADD(HOUR,9,DATEADD(DAY,-19,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,16,DATEADD(DAY,-19,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1050000, 3500000, 0, 1, N'Tập huấn tiêu chuẩn nghiệp vụ lễ tân', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,16,DATEADD(DAY,-19,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-21,GETDATE())),
('SK-2026-039', 3, 15, N'Tiệc Sinh Nhật VIP', '0908990011', NULL, DATEADD(HOUR,17,DATEADD(DAY,-19,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-19,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1800000, 6000000, 0, 1, N'Tiệc sinh nhật riêng tư trên sân thượng', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-19,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-21,GETDATE())),
('SK-2026-040', 4, 2, N'Trịnh Hồng Quân', '0909123456', '102', DATEADD(HOUR,8,DATEADD(DAY,-19,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,12,DATEADD(DAY,-19,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1200000, 1200000, 0, 1, N'Thưởng trà đàm đạo buổi sáng', N'Thanh toán toàn bộ', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,12,DATEADD(DAY,-19,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-21,GETDATE())),
('SK-2026-041', 1, 15, N'Xây dựng Hòa Bình Gala', '0907123456', NULL, DATEADD(HOUR,17,DATEADD(DAY,-18,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-18,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), 30, 500000, N'Dọn dẹp nhanh 30 phút để set up đại tiệc', 2250000, 7500000, 0, 1, N'Đại tiệc tổng kết công trình', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-18,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-20,GETDATE())),
('SK-2026-042', 2, 15, N'Đại hội Cổ đông BĐS', '0918223344', NULL, DATEADD(HOUR,8,DATEADD(DAY,-18,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,13,DATEADD(DAY,-18,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 750000, 2500000, 0, 1, N'Đại hội cổ đông thường niên', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,13,DATEADD(DAY,-18,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-20,GETDATE())),
('SK-2026-043', 3, 15, N'Barbecue Sunset Chill', '0903445566', NULL, DATEADD(HOUR,10,DATEADD(DAY,-18,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,14,DATEADD(DAY,-18,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2160000, 7200000, 0, 1, N'Tiệc nướng BBQ ngắm thung lũng', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,14,DATEADD(DAY,-18,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-20,GETDATE())),
('SK-2026-044', 4, 15, N'Trà thiền ngắm sương', '0915667788', NULL, DATEADD(HOUR,13,DATEADD(DAY,-18,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-18,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 450000, 1500000, 0, 1, N'Buổi trà thiền thư giãn cuối tuần', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-18,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-20,GETDATE())),
('SK-2026-045', 1, 15, N'Tiệc cưới Thanh Bình & Mai Hương', '0988112233', NULL, DATEADD(HOUR,9,DATEADD(DAY,-17,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,16,DATEADD(DAY,-17,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 3150000, 10500000, 0, 1, N'Tiệc cưới sang trọng', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,16,DATEADD(DAY,-17,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-19,GETDATE())),
('SK-2026-046', 2, 15, N'Workshop Nhiếp ảnh', '0909334455', NULL, DATEADD(HOUR,13,DATEADD(DAY,-17,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-17,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 750000, 2500000, 0, 1, N'Chia sẻ kỹ thuật chụp ảnh phong cảnh', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-17,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-19,GETDATE())),
('SK-2026-047', 3, 15, N'Weekend Sky Party', '0912445566', NULL, DATEADD(HOUR,17,DATEADD(DAY,-17,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,23,DATEADD(DAY,-17,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2160000, 7200000, 0, 1, N'Tiệc cocktail đón hoàng hôn', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,23,DATEADD(DAY,-17,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-19,GETDATE())),
('SK-2026-048', 4, 1, N'Đoàn Minh Trí', '0908112233', '101', DATEADD(HOUR,9,DATEADD(DAY,-17,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,14,DATEADD(DAY,-17,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 450000, 1500000, 0, 1, N'Trà đạo gặp gỡ bạn bè', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,14,DATEADD(DAY,-17,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-19,GETDATE())),
('SK-2026-049', 1, 15, N'Hội thảo Nông sản Sạch', '0908332211', NULL, DATEADD(HOUR,8,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,12,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2700000, 9000000, 0, 1, N'Xúc tiến tiêu thụ rau hoa Đà Lạt', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,12,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-18,GETDATE())),
('SK-2026-050', 2, 7, N'Nguyễn Anh Thư', '0918777666', '301', DATEADD(HOUR,8,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,12,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 600000, 2000000, 0, 1, N'Họp ban cố vấn', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,12,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-18,GETDATE())),
('SK-2026-051', 3, 15, N'Sky Club Members Night', '0903556677', NULL, DATEADD(HOUR,16,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2160000, 7200000, 0, 1, N'Đêm giao lưu thành viên', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-18,GETDATE())),
('SK-2026-052', 4, 8, N'Đặng Hải Yến', '0982334411', '302', DATEADD(HOUR,14,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,19,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 450000, 1500000, 0, 1, N'Trà chiều phong vị cao nguyên', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,19,DATEADD(DAY,-16,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-18,GETDATE())),
('SK-2026-053', 1, 15, N'Tập đoàn Hoa Sen Tri Ân', '0907123987', NULL, DATEADD(HOUR,8,DATEADD(DAY,-15,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,11,DATEADD(DAY,-15,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2700000, 9000000, 0, 1, N'Sáng nay đã hoàn tất tiệc tri ân', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,11,DATEADD(DAY,-15,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-17,GETDATE())),
('SK-2026-054', 2, 15, N'Hội thảo Pháp lý Doanh nghiệp', '0914889900', NULL, DATEADD(HOUR,9,DATEADD(DAY,-15,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,15,DATEADD(DAY,-15,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 900000, 3000000, 0, 1, N'Hội thảo đang diễn ra sôi nổi', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,15,DATEADD(DAY,-15,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-17,GETDATE())),
('SK-2026-055', 3, 6, N'Bùi Thanh Tùng', '0938445566', '203', DATEADD(HOUR,17,DATEADD(DAY,-15,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-15,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1800000, 6000000, 0, 1, N'Tiệc kỷ niệm ngày cưới gia đình tối nay', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-15,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-17,GETDATE())),
('SK-2026-056', 4, 15, N'Trà thất bạn hữu', '0917223344', NULL, DATEADD(HOUR,8,DATEADD(DAY,-15,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,14,DATEADD(DAY,-15,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 450000, 1500000, 0, 1, N'Giao lưu trà đạo nghệ nhân đang tiếp khách', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,14,DATEADD(DAY,-15,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-17,GETDATE())),
('SK-2026-057', 1, 15, N'Viettel Telecom Hội nghị', '0986112233', NULL, DATEADD(HOUR,8,DATEADD(DAY,-14,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-14,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 4500000, 15000000, 0, 1, N'Hội nghị triển khai kế hoạch kinh doanh', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-14,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-16,GETDATE())),
('SK-2026-058', 2, 12, N'Đỗ Minh Khang', '0987556677', '403', DATEADD(HOUR,13,DATEADD(DAY,-14,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-14,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 750000, 2500000, 0, 1, N'Họp dự án phần mềm', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-14,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-16,GETDATE())),
('SK-2026-059', 3, 15, N'Sunset Chill Out', '0908445566', NULL, DATEADD(HOUR,18,DATEADD(DAY,-14,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,23,DATEADD(DAY,-14,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1800000, 6000000, 0, 0, N'Khách thay đổi kế hoạch gia đình', N'Đặt cọc trước', N'Đã hủy - Hoàn cọc', DATEADD(DAY,-15,GETDATE()), N'Khách báo hủy trước 1 tuần, hoàn 100% tiền cọc', 1, 1800000, 1, NULL, NULL, DATEADD(DAY,-16,GETDATE())),
('SK-2026-060', 4, 15, N'Trà chiều thư giãn', '0913556677', NULL, DATEADD(HOUR,14,DATEADD(DAY,-14,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-14,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 360000, 1200000, 0, 1, N'Trà chiều ấm cúng', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-14,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-16,GETDATE())),
('SK-2026-061', 1, 15, N'Công ty TechCorp Demo AI', '0903999888', NULL, DATEADD(HOUR,8,DATEADD(DAY,-13,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,17,DATEADD(DAY,-13,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 4050000, 13500000, 0, 1, N'Hội thảo ra mắt nền tảng AI Khách sạn', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,17,DATEADD(DAY,-13,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-15,GETDATE())),
('SK-2026-062', 2, 5, N'Lê Tấn Phát', '0969888999', '202', DATEADD(HOUR,13,DATEADD(DAY,-13,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-13,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 750000, 2500000, 0, 1, N'Họp chiến lược kinh doanh', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-13,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-15,GETDATE())),
('SK-2026-063', 3, 15, N'Sunset Music Club', '0908119922', NULL, DATEADD(HOUR,16,DATEADD(DAY,-13,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-13,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2160000, 7200000, 0, 1, N'Đêm nhạc acoustic cuối tuần', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-13,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-15,GETDATE())),
('SK-2026-064', 4, 2, N'Trịnh Hồng Quân', '0909123456', '102', DATEADD(HOUR,8,DATEADD(DAY,-13,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,12,DATEADD(DAY,-13,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1200000, 1200000, 0, 1, N'Đàm đạo trà sáng bạn bè', N'Thanh toán toàn bộ', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,12,DATEADD(DAY,-13,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-15,GETDATE())),
('SK-2026-065', 1, 15, N'Tiệc cưới Minh Quang & Hà My', '0988223344', NULL, DATEADD(HOUR,9,DATEADD(DAY,-12,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,16,DATEADD(DAY,-12,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 3150000, 10500000, 0, 1, N'Tiệc cưới cao cấp', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,16,DATEADD(DAY,-12,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-14,GETDATE())),
('SK-2026-066', 2, 3, N'Vũ Hoàng Nam', '0912334455', '103', DATEADD(HOUR,8,DATEADD(DAY,-12,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,13,DATEADD(DAY,-12,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 750000, 2500000, 0, 1, N'Họp nhóm tư vấn tài chính', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,13,DATEADD(DAY,-12,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-14,GETDATE())),
('SK-2026-067', 3, 15, N'DJ Sunset Party Night', '0909445566', NULL, DATEADD(HOUR,17,DATEADD(DAY,-12,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,23,DATEADD(DAY,-12,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2160000, 7200000, 0, 1, N'Tiệc DJ ngắm cảnh toàn thành phố', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,23,DATEADD(DAY,-12,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-14,GETDATE())),
('SK-2026-068', 4, 4, N'Hoàng Mai Linh', '0978112244', '201', DATEADD(HOUR,13,DATEADD(DAY,-12,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,17,DATEADD(DAY,-12,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 360000, 1200000, 0, 1, N'Trà chiều gia đình', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,17,DATEADD(DAY,-12,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-14,GETDATE())),
('SK-2026-069', 1, 15, N'Hội nghị Công nghệ VinFast', '0915998877', NULL, DATEADD(HOUR,8,DATEADD(DAY,-11,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-11,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 4500000, 15000000, 0, 1, N'Hội nghị chiến lược phân phối xe điện', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-11,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-13,GETDATE())),
('SK-2026-070', 2, 6, N'Bùi Thanh Tùng', '0938445566', '203', DATEADD(HOUR,14,DATEADD(DAY,-11,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,19,DATEADD(DAY,-11,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 750000, 2500000, 0, 1, N'Họp ban quản lý dự án', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,19,DATEADD(DAY,-11,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-13,GETDATE())),
('SK-2026-071', 3, 15, N'Acoustic Night & Wine', '0903332211', NULL, DATEADD(HOUR,16,DATEADD(DAY,-11,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-11,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2160000, 7200000, 0, 1, N'Đêm nhạc acoustic thưởng rượu vang', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-11,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-13,GETDATE())),
('SK-2026-072', 4, 7, N'Nguyễn Anh Thư', '0918777666', '301', DATEADD(HOUR,8,DATEADD(DAY,-11,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,13,DATEADD(DAY,-11,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), 30, 200000, N'Yêu cầu dọn sảnh sớm 30 phút để đón khách', 450000, 1500000, 0, 1, N'Thưởng trà tĩnh tâm', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,13,DATEADD(DAY,-11,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-13,GETDATE())),
('SK-2026-073', 1, 15, N'Diễn đàn Bất động sản 2026', '0914112233', NULL, DATEADD(HOUR,9,DATEADD(DAY,-10,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,17,DATEADD(DAY,-10,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 3600000, 12000000, 0, 1, N'Tọa đàm bất động sản nghỉ dưỡng', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,17,DATEADD(DAY,-10,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-12,GETDATE())),
('SK-2026-074', 2, 8, N'Đặng Hải Yến', '0982334411', '302', DATEADD(HOUR,8,DATEADD(DAY,-10,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,12,DATEADD(DAY,-10,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 600000, 2000000, 0, 1, N'Hội thảo kỹ năng giao tiếp', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,12,DATEADD(DAY,-10,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-12,GETDATE())),
('SK-2026-075', 3, 9, N'Phan Văn Đức', '0945667788', '303', DATEADD(HOUR,17,DATEADD(DAY,-10,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-10,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1800000, 6000000, 0, 1, N'Tiệc sinh nhật ấm cúng trên cao', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-10,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-12,GETDATE())),
('SK-2026-076', 4, 10, N'Phạm Quốc Bảo', '0933444555', '401', DATEADD(HOUR,14,DATEADD(DAY,-10,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-10,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 360000, 1200000, 0, 1, N'Trà chiều kết nối đối tác', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-10,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-12,GETDATE())),
('SK-2026-077', 1, 15, N'Gala Tổng kết Quý 3 Vinatex', '0908556677', NULL, DATEADD(HOUR,16,DATEADD(DAY,-9,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-9,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2700000, 9000000, 0, 1, N'Gala Dinner tổng kết hoạt động quý 3', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-9,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-11,GETDATE())),
('SK-2026-078', 2, 15, N'Huấn luyện Quản lý Cấp cao', '0916778899', NULL, DATEADD(HOUR,8,DATEADD(DAY,-9,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,17,DATEADD(DAY,-9,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1350000, 4500000, 0, 1, N'Khóa đào tạo lãnh đạo chuyển dịch số', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,17,DATEADD(DAY,-9,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-11,GETDATE())),
('SK-2026-079', 3, 15, N'Chill Out Rooftop Sunset', '0903882233', NULL, DATEADD(HOUR,18,DATEADD(DAY,-9,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,23,DATEADD(DAY,-9,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1800000, 6000000, 0, 1, N'Đêm ngắm hoàng hôn và nghe nhạc', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,23,DATEADD(DAY,-9,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-11,GETDATE())),
('SK-2026-080', 4, 11, N'Trần Thu Hà', '0919223344', '402', DATEADD(HOUR,9,DATEADD(DAY,-9,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,14,DATEADD(DAY,-9,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1500000, 1500000, 0, 1, N'Thưởng trà sương sớm', N'Thanh toán toàn bộ', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,14,DATEADD(DAY,-9,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-11,GETDATE())),
('SK-2026-081', 1, 15, N'Hội nghị Khách hàng Petrolimex', '0909887766', NULL, DATEADD(HOUR,8,DATEADD(DAY,-8,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,16,DATEADD(DAY,-8,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 3600000, 12000000, 0, 1, N'Hội nghị đối tác phân phối dầu nhờn', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,16,DATEADD(DAY,-8,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-10,GETDATE())),
('SK-2026-082', 2, 12, N'Đỗ Minh Khang', '0987556677', '403', DATEADD(HOUR,13,DATEADD(DAY,-8,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-8,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 750000, 2500000, 0, 1, N'Họp đội ngũ lập trình viên', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-8,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-10,GETDATE())),
('SK-2026-083', 3, 13, N'Nguyễn Hoàng Long', '0903889900', '501', DATEADD(HOUR,16,DATEADD(DAY,-8,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-8,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2160000, 7200000, 0, 1, N'Tiệc tri ân nhân viên xuất sắc', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-8,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-10,GETDATE())),
('SK-2026-084', 4, 14, N'Lê Thị Ngọc Ánh', '0979334455', '502', DATEADD(HOUR,14,DATEADD(DAY,-8,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-8,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 360000, 1200000, 0, 1, N'Trà chiều cùng người thân', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-8,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-10,GETDATE())),
('SK-2026-085', 1, 15, N'Hội thảo Phát triển Bền vững', '0914228899', NULL, DATEADD(HOUR,8,DATEADD(DAY,-7,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,17,DATEADD(DAY,-7,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 4050000, 13500000, 0, 1, N'Hội thảo công nghiệp xanh Tây Nguyên', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,17,DATEADD(DAY,-7,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-9,GETDATE())),
('SK-2026-086', 2, 15, N'Họp Ban Giám Đốc BIDV', '0918334455', NULL, DATEADD(HOUR,9,DATEADD(DAY,-7,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,16,DATEADD(DAY,-7,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1050000, 3500000, 0, 1, N'Họp giao ban chi nhánh Lâm Đồng', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,16,DATEADD(DAY,-7,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-9,GETDATE())),
('SK-2026-087', 3, 1, N'Đoàn Minh Trí', '0908112233', '101', DATEADD(HOUR,17,DATEADD(DAY,-7,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-7,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1800000, 6000000, 0, 1, N'Tiệc mừng công đoàn công tác', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-7,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-9,GETDATE())),
('SK-2026-088', 4, 15, N'Trà đàm nghệ nhân xứ trà', '0915446688', NULL, DATEADD(HOUR,8,DATEADD(DAY,-7,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,12,DATEADD(DAY,-7,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 360000, 1200000, 0, 1, N'Gặp gỡ nghệ nhân chế tác trà Oolong', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,12,DATEADD(DAY,-7,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-9,GETDATE())),
('SK-2026-089', 1, 15, N'Đại tiệc Tri ân Vietjet Air', '0908123789', NULL, DATEADD(HOUR,17,DATEADD(DAY,-6,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-6,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2250000, 7500000, 0, 1, N'Dạ tiệc tri ân phòng vé khu vực', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-6,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-8,GETDATE())),
('SK-2026-090', 2, 15, N'Hội thảo Chuyển đổi số', '0916889900', NULL, DATEADD(HOUR,8,DATEADD(DAY,-6,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,13,DATEADD(DAY,-6,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 750000, 2500000, 0, 1, N'Ứng dụng ERP trong quản trị khách sạn', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,13,DATEADD(DAY,-6,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-8,GETDATE())),
('SK-2026-091', 3, 15, N'Sunset Cocktail VIP', '0903114477', NULL, DATEADD(HOUR,16,DATEADD(DAY,-6,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-6,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2160000, 7200000, 0, 1, N'Tiệc cocktail đón hoàng hôn cuối tuần', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-6,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-8,GETDATE())),
('SK-2026-092', 4, 15, N'Giao lưu trà Việt', '0917335577', NULL, DATEADD(HOUR,13,DATEADD(DAY,-6,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-6,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 450000, 1500000, 0, 1, N'Buổi nếm thử trà shan tuyết', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-6,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-8,GETDATE())),
('SK-2026-093', 1, 15, N'Tiệc cưới Văn Hùng & Thu Thảo', '0988665544', NULL, DATEADD(HOUR,9,DATEADD(DAY,-5,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,16,DATEADD(DAY,-5,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 3150000, 10500000, 0, 1, N'Tiệc cưới phong cách Châu Âu', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,16,DATEADD(DAY,-5,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-7,GETDATE())),
('SK-2026-094', 2, 15, N'Hội thảo Đầu tư Khởi nghiệp', '0909558833', NULL, DATEADD(HOUR,13,DATEADD(DAY,-5,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,-5,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 750000, 2500000, 0, 1, N'Gặp gỡ các quỹ đầu tư mạo hiểm', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,18,DATEADD(DAY,-5,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-7,GETDATE())),
('SK-2026-095', 3, 15, N'Weekend Acoustic Sky Club', '0914992211', NULL, DATEADD(HOUR,17,DATEADD(DAY,-5,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,23,DATEADD(DAY,-5,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2160000, 7200000, 0, 1, N'Đêm nhạc acoustic lãng mạn', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,23,DATEADD(DAY,-5,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-7,GETDATE())),
('SK-2026-096', 4, 15, N'Trà chiều thiền định', '0918113355', NULL, DATEADD(HOUR,9,DATEADD(DAY,-5,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,14,DATEADD(DAY,-5,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1500000, 1500000, 0, 1, N'Trà thiền ngắm hoa cẩm tú cầu', N'Thanh toán toàn bộ', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,14,DATEADD(DAY,-5,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-7,GETDATE())),
('SK-2026-097', 1, 15, N'Ngày hội Du lịch Đà Lạt', '0908771122', NULL, DATEADD(HOUR,10,DATEADD(DAY,-4,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,16,DATEADD(DAY,-4,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2700000, 9000000, 0, 1, N'Họp báo xúc tiến mùa lễ hội hoa', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,16,DATEADD(DAY,-4,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-6,GETDATE())),
('SK-2026-098', 2, 15, N'Họp Hội đồng Khoa học', '0912447788', NULL, DATEADD(HOUR,8,DATEADD(DAY,-4,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,12,DATEADD(DAY,-4,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 600000, 2000000, 0, 1, N'Nghiệm thu đề tài cấp tỉnh', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,12,DATEADD(DAY,-4,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-6,GETDATE())),
('SK-2026-099', 3, 15, N'Sky Lounge Networking', '0903668899', NULL, DATEADD(HOUR,16,DATEADD(DAY,-4,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-4,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2160000, 7200000, 0, 1, N'Giao lưu cộng đồng doanh nhân trẻ', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-4,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-6,GETDATE())),
('SK-2026-100', 4, 15, N'Buổi đàm đạo thư pháp', '0915224466', NULL, DATEADD(HOUR,14,DATEADD(DAY,-4,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,19,DATEADD(DAY,-4,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 450000, 1500000, 0, 1, N'Trình diễn nghệ thuật viết chữ', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,19,DATEADD(DAY,-4,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-6,GETDATE())),
('SK-2026-101', 1, 15, N'Đại hội Thường niên Agribank', '0909337711', NULL, DATEADD(HOUR,8,DATEADD(DAY,-3,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,17,DATEADD(DAY,-3,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 4050000, 13500000, 0, 1, N'Hội nghị sơ kết thi đua khu vực', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,17,DATEADD(DAY,-3,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-5,GETDATE())),
('SK-2026-102', 2, 15, N'Tập huấn An toàn thông tin', '0916113355', NULL, DATEADD(HOUR,9,DATEADD(DAY,-3,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,15,DATEADD(DAY,-3,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 900000, 3000000, 0, 1, N'Tập huấn an ninh mạng cơ quan nhà nước', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,15,DATEADD(DAY,-3,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-5,GETDATE())),
('SK-2026-103', 3, 15, N'Sunset Live Band Night', '0908225588', NULL, DATEADD(HOUR,17,DATEADD(DAY,-3,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-3,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1800000, 6000000, 0, 1, N'Đêm nhạc pop ballad nhẹ nhàng', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-3,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-5,GETDATE())),
('SK-2026-104', 4, 15, N'Trà sớm thung lũng sương', '0917882244', NULL, DATEADD(HOUR,8,DATEADD(DAY,-3,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,13,DATEADD(DAY,-3,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 450000, 1500000, 0, 1, N'Trà sớm ngắm đồi thông reo', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,13,DATEADD(DAY,-3,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-5,GETDATE())),
('SK-2026-105', 1, 15, N'Gala Tổng Kết Tháng 9 & Chào Tháng 10', '0908889911', NULL, DATEADD(HOUR,16,DATEADD(DAY,-2,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-2,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2700000, 9000000, 0, 1, N'Đại tiệc liên hoan cuối tháng', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-2,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-4,GETDATE())),
('SK-2026-106', 2, 15, N'Họp Tổng Kết Kinh Doanh Tháng 9', '0912993344', NULL, DATEADD(HOUR,8,DATEADD(DAY,-2,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,13,DATEADD(DAY,-2,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 750000, 2500000, 0, 1, N'Tổng kết chỉ tiêu quý 3', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,13,DATEADD(DAY,-2,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-4,GETDATE())),
('SK-2026-107', 3, 15, N'Sky Party Chốt Sổ Tháng 9', '0903558822', NULL, DATEADD(HOUR,17,DATEADD(DAY,-2,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,-2,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1800000, 6000000, 0, 1, N'Tiệc chúc mừng vượt KPI tháng 9', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,22,DATEADD(DAY,-2,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-4,GETDATE())),
('SK-2026-108', 4, 15, N'Trà chiều tri ân tháng 9', '0916228844', NULL, DATEADD(HOUR,9,DATEADD(DAY,-2,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,13,DATEADD(DAY,-2,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1200000, 1200000, 0, 1, N'Gặp mặt thân mật cuối tháng', N'Thanh toán toàn bộ', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,13,DATEADD(DAY,-2,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(DAY,-4,GETDATE())),
-- Giao dịch đặt sảnh & sự kiện MỚI Hôm nay (GETDATE()) và Ngày mai (DATEADD(DAY,1,GETDATE()))
('SK-2026-109', 1, 15, N'Tập đoàn Hoa Sen Tri Ân', '0907123987', NULL, DATEADD(HOUR,7,CAST(CAST(GETDATE() AS DATE) AS DATETIME)), DATEADD(HOUR,10,CAST(CAST(GETDATE() AS DATE) AS DATETIME)), NULL, 0, NULL, 2700000, 4500000, 0, 1, N'Sáng nay đã hoàn tất hội nghị tổng kết sớm', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(HOUR,10,CAST(CAST(GETDATE() AS DATE) AS DATETIME)), DATEADD(DAY,-1,GETDATE())),
('SK-2026-110', 2, 15, N'Hội thảo Pháp lý Doanh nghiệp', '0914889900', NULL, DATEADD(HOUR,8,CAST(CAST(GETDATE() AS DATE) AS DATETIME)), DATEADD(HOUR,15,CAST(CAST(GETDATE() AS DATE) AS DATETIME)), NULL, 0, NULL, 1050000, 3500000, 0, 0, N'Hội thảo đang diễn ra tại Phòng VIP Ruby hôm nay', N'Đặt cọc trước', N'Đang sử dụng', NULL, NULL, NULL, NULL, 1, NULL, NULL, DATEADD(DAY,-1,GETDATE())),
('SK-2026-111', 4, 10, N'Phạm Quốc Bảo', '0933444555', '401', DATEADD(HOUR,8,CAST(CAST(GETDATE() AS DATE) AS DATETIME)), DATEADD(HOUR,14,CAST(CAST(GETDATE() AS DATE) AS DATETIME)), NULL, 0, NULL, 540000, 1800000, 0, 0, N'Giao lưu trà đạo nghệ nhân hôm nay', N'Đặt cọc trước', N'Đang sử dụng', NULL, NULL, NULL, NULL, 1, NULL, NULL, DATEADD(DAY,-1,GETDATE())),
('SK-2026-112', 3, 6, N'Bùi Thanh Tùng', '0938445566', '203', DATEADD(HOUR,17,CAST(CAST(GETDATE() AS DATE) AS DATETIME)), DATEADD(HOUR,22,CAST(CAST(GETDATE() AS DATE) AS DATETIME)), NULL, 0, NULL, 1800000, 6000000, 0, 0, N'Tiệc kỷ niệm ngày cưới gia đình tối hôm nay', N'Đặt cọc trước', N'Đã đặt', NULL, NULL, NULL, NULL, 2, NULL, NULL, GETDATE()),
('SK-2026-113', 1, 15, N'Viettel Telecom Hội nghị', '0986112233', NULL, DATEADD(HOUR,8,DATEADD(DAY,1,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,17,DATEADD(DAY,1,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 4050000, 13500000, 0, 0, N'Hội nghị triển khai kế hoạch kinh doanh ngày mai', N'Đặt cọc trước', N'Đã đặt', NULL, NULL, NULL, NULL, 1, NULL, NULL, GETDATE()),
('SK-2026-114', 2, 12, N'Đỗ Minh Khang', '0987556677', '403', DATEADD(HOUR,9,DATEADD(DAY,1,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,14,DATEADD(DAY,1,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 2500000, 2500000, 0, 1, N'Họp dự án phần mềm ngày mai (Đã thanh toán toàn bộ)', N'Thanh toán toàn bộ', N'Đã đặt', NULL, NULL, NULL, NULL, 2, 2, GETDATE(), GETDATE()),
('SK-2026-115', 3, 7, N'Nguyễn Anh Thư', '0918777666', '301', DATEADD(HOUR,17,DATEADD(DAY,1,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,22,DATEADD(DAY,1,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 1800000, 6000000, 0, 0, N'Tiệc Acoustic Honeymoon tối ngày mai', N'Đặt cọc trước', N'Đã đặt', NULL, NULL, NULL, NULL, 1, NULL, NULL, GETDATE()),
('SK-2026-116', 4, 5, N'Lê Tấn Phát', '0969888999', '202', DATEADD(HOUR,14,DATEADD(DAY,1,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), DATEADD(HOUR,18,DATEADD(DAY,1,CAST(CAST(GETDATE() AS DATE) AS DATETIME))), NULL, 0, NULL, 360000, 1200000, 0, 0, N'Thưởng trà ký kết đối tác chiều ngày mai', N'Đặt cọc trước', N'Đã đặt', NULL, NULL, NULL, NULL, 2, NULL, NULL, GETDATE());
GO

-- 7.5 Đơn thuê xe mẫu (Toàn bộ các đơn mẫu quá khứ trước hôm nay đã Hoàn tất & Thanh toán hết; kèm các đơn phát sinh mới Hôm nay & Ngày mai)
INSERT INTO DonThueXe (
    MaDon, XeId, KhachHangId, TenKhach, CCCD, SoDienThoai, SoPhong,
    NgayThue, NgayTraDuKien, NgayTraThucTe,
    ThoiGianKiemTraXePhut, PhuThuRutNganBuffer, GhiChuRutNganBuffer,
    TienCoc, TienThue, ChiPhiPhatSinh, TongTienThanhToan, DaThanhToan,
    GhiChuKhiNhanXe, GhiChuHuHai,
    HinhThucThanhToanBanDau, TrangThaiDon,
    ThoiGianHuy, LyDoHuy, NguoiHuyId, SoTienHoanCoc,
    NguoiTaoId, NguoiThanhToanId, ThoiGianThanhToan, NgayTao
) VALUES
-- Dữ liệu mẫu đã qua (Trước ngày hôm nay -> Đã hoàn tất & thanh toán hết)
('TX-2026-001', 5, 16, N'Nguyễn Văn Khách', '079190001111', '0988112233', NULL, DATEADD(DAY,-30,GETDATE()), DATEADD(DAY,-27,GETDATE()), DATEADD(DAY,-27,GETDATE()), NULL, 0, NULL, 100000, 300000, 0, 300000, 1, N'Khách thuê Wave 3 ngày ngắm đồi thông', N'Xe sạch đẹp, trả đúng hạn', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-27,GETDATE()), DATEADD(DAY,-30,GETDATE())),
('TX-2026-002', 17, 16, N'Lê Hoàng Long', '079191002222', '0912445566', NULL, DATEADD(DAY,-27,GETDATE()), DATEADD(DAY,-24,GETDATE()), DATEADD(DAY,-24,GETDATE()), NULL, 0, NULL, 200000, 540000, 0, 540000, 1, N'Khách thuê Raider đi phượt', N'Xe hoạt động tốt', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 2, 2, DATEADD(DAY,-24,GETDATE()), DATEADD(DAY,-27,GETDATE())),
('TX-2026-003', 18, 16, N'Phạm Thu Thảo', '079192003333', '0938119900', NULL, DATEADD(DAY,-24,GETDATE()), DATEADD(DAY,-21,GETDATE()), DATEADD(DAY,-21,GETDATE()), NULL, 0, NULL, 300000, 600000, 0, 600000, 1, N'Khách thuê Vario 3 ngày', N'Xe hoàn hảo, đã rửa sạch', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-21,GETDATE()), DATEADD(DAY,-24,GETDATE())),
('TX-2026-004', 19, 16, N'Ngô Gia Huy', '079193004444', '0945223344', NULL, DATEADD(DAY,-22,GETDATE()), DATEADD(DAY,-19,GETDATE()), DATEADD(DAY,-19,GETDATE()), NULL, 0, NULL, 200000, 330000, 0, 330000, 1, N'Khách thuê Wave RSX đi công tác', N'Đổ đầy bình xăng khi trả', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 2, 2, DATEADD(DAY,-19,GETDATE()), DATEADD(DAY,-22,GETDATE())),
('TX-2026-005', 20, 16, N'Đặng Quốc Tuấn', '079194005555', '0979887766', NULL, DATEADD(DAY,-20,GETDATE()), DATEADD(DAY,-17,GETDATE()), DATEADD(DAY,-17,GETDATE()), NULL, 0, NULL, 300000, 480000, 0, 480000, 1, N'Khách thuê Exciter GP', N'Xe tốt, bảo dưỡng định kỳ', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-17,GETDATE()), DATEADD(DAY,-20,GETDATE())),
('TX-2026-006', 5, 16, N'Trần Minh Trí', '079195006666', '0908776655', NULL, DATEADD(DAY,-18,GETDATE()), DATEADD(DAY,-16,GETDATE()), DATEADD(DAY,-16,GETDATE()), NULL, 0, NULL, 100000, 200000, 0, 200000, 1, N'Khách thuê Wave 2 ngày', N'Đã hoàn tất thanh toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 2, 2, DATEADD(DAY,-16,GETDATE()), DATEADD(DAY,-18,GETDATE())),
('TX-2026-007', 17, 16, N'Võ Hoàng Khang', '079196007777', '0918332211', NULL, DATEADD(DAY,-17,GETDATE()), DATEADD(DAY,-14,GETDATE()), NULL, NULL, 0, NULL, 200000, 540000, 0, 0, 0, N'Khách đổi lịch bay, hủy thuê xe trước ngày nhận', N'Đã hoàn lại cọc cho khách', N'Đặt cọc trước', N'Đã hủy - Hoàn cọc', DATEADD(DAY,-17,GETDATE()), N'Khách báo hủy trước ngày nhận xe, hoàn 100% cọc', 1, 200000, 1, NULL, NULL, DATEADD(DAY,-17,GETDATE())),
('TX-2026-008', 1, 1, N'Đoàn Minh Trí', '079198001234', '0908112233', '101', DATEADD(DAY,-25,GETDATE()), DATEADD(DAY,-23,GETDATE()), DATEADD(DAY,-23,GETDATE()), NULL, 0, NULL, 300000, 600000, 0, 600000, 1, N'Khách thuê SH kèm 2 nón bảo hiểm cao cấp', N'Trả xe đúng hẹn, xe tốt', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-23,GETDATE()), DATEADD(DAY,-25,GETDATE())),
('TX-2026-009', 8, 4, N'Hoàng Mai Linh', '079194007788', '0978112244', '201', DATEADD(DAY,-23,GETDATE()), DATEADD(DAY,-21,GETDATE()), DATEADD(DAY,-21,GETDATE()), NULL, 0, NULL, 120000, 240000, 0, 240000, 1, N'Khách thuê Future leo dốc đồi chè', N'Xe sạch đẹp', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 2, 2, DATEADD(DAY,-21,GETDATE()), DATEADD(DAY,-23,GETDATE())),
('TX-2026-010', 6, 2, N'Trịnh Hồng Quân', '079192003344', '0909123456', '102', DATEADD(DAY,-22,GETDATE()), DATEADD(DAY,-19,GETDATE()), DATEADD(DAY,-19,GETDATE()), NULL, 0, NULL, 240000, 480000, 0, 480000, 1, N'Khách thuê Lead đi chợ mua đặc sản', N'Xe tốt, hoàn tất thanh toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 2, 2, DATEADD(DAY,-19,GETDATE()), DATEADD(DAY,-22,GETDATE())),
('TX-2026-011', 10, 8, N'Đặng Hải Yến', '079197003355', '0982334411', '302', DATEADD(DAY,-21,GETDATE()), DATEADD(DAY,-18,GETDATE()), DATEADD(DAY,-18,GETDATE()), NULL, 0, NULL, 420000, 840000, 0, 840000, 1, N'Khách thuê Vespa chụp ảnh check-in', N'Hoàn tất tốt đẹp', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-18,GETDATE()), DATEADD(DAY,-21,GETDATE())),
('TX-2026-012', 13, 11, N'Trần Thu Hà', '079189006677', '0919223344', '402', DATEADD(DAY,-20,GETDATE()), DATEADD(DAY,-17,GETDATE()), DATEADD(DAY,-17,GETDATE()), NULL, 0, NULL, 330000, 660000, 0, 660000, 1, N'Khách thuê Liberty dạo phố cổ', N'Đã trả xe', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 2, 2, DATEADD(DAY,-17,GETDATE()), DATEADD(DAY,-20,GETDATE())),
('TX-2026-013', 2, 5, N'Lê Tấn Phát', '079201009876', '0969888999', '202', DATEADD(DAY,-19,GETDATE()), DATEADD(DAY,-16,GETDATE()), DATEADD(DAY,-16,GETDATE()), NULL, 0, NULL, 378000, 540000, 0, 540000, 1, N'Khách lấy 2 nón bảo hiểm', N'Xe trả đúng hẹn', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 2, 2, DATEADD(DAY,-16,GETDATE()), DATEADD(DAY,-19,GETDATE())),
('TX-2026-014', 4, 12, N'Đỗ Minh Khang', '079187008899', '0987556677', '403', DATEADD(DAY,-18,GETDATE()), DATEADD(DAY,-16,GETDATE()), DATEADD(DAY,-16,GETDATE()), NULL, 0, NULL, 170000, 340000, 0, 340000, 1, N'Khách thuê Exciter đi phượt đèo', N'Đã kiểm tra xe an toàn', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 2, 2, DATEADD(DAY,-16,GETDATE()), DATEADD(DAY,-18,GETDATE())),
('TX-2026-015', 15, 14, N'Lê Thị Ngọc Ánh', '079185002345', '0979334455', '502', DATEADD(DAY,-17,GETDATE()), DATEADD(DAY,-16,GETDATE()), DATEADD(DAY,-16,GETDATE()), NULL, 0, NULL, 50000, 100000, 0, 100000, 1, N'Khách thuê Sirius ngắm hoàng hôn', N'Đã thanh toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 2, 2, DATEADD(DAY,-16,GETDATE()), DATEADD(DAY,-17,GETDATE())),
('TX-2026-016', 1, 1, N'Đoàn Minh Trí', '079198001234', '0908112233', '101', DATEADD(DAY,-15,GETDATE()), DATEADD(DAY,-13,GETDATE()), DATEADD(DAY,-13,GETDATE()), NULL, 0, NULL, 300000, 600000, 0, 600000, 1, N'Khách thuê SH cao cấp 2 ngày', N'Đã trả xe & quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-13,GETDATE()), DATEADD(DAY,-15,GETDATE())),
('TX-2026-017', 6, 2, N'Trịnh Hồng Quân', '079192003344', '0909123456', '102', DATEADD(DAY,-15,GETDATE()), DATEADD(DAY,-13,GETDATE()), DATEADD(DAY,-13,GETDATE()), NULL, 0, NULL, 240000, 480000, 0, 480000, 1, N'Khách thuê Lead 2 ngày', N'Đã trả xe & quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 2, 2, DATEADD(DAY,-13,GETDATE()), DATEADD(DAY,-15,GETDATE())),
('TX-2026-018', 7, 3, N'Vũ Hoàng Nam', '079193005566', '0912334455', '103', DATEADD(DAY,-15,GETDATE()), DATEADD(DAY,-10,GETDATE()), DATEADD(DAY,-10,GETDATE()), NULL, 0, NULL, 700000, 1190000, 0, 1190000, 1, N'Khách thuê Grande đi dạo hồ Tuyền Lâm', N'Đã trả xe & quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-10,GETDATE()), DATEADD(DAY,-15,GETDATE())),
('TX-2026-019', 10, 8, N'Đặng Hải Yến', '079197003355', '0982334411', '302', DATEADD(DAY,-15,GETDATE()), DATEADD(DAY,-12,GETDATE()), DATEADD(DAY,-12,GETDATE()), NULL, 0, NULL, 420000, 840000, 0, 840000, 1, N'Khách thuê Vespa dạo phố', N'Đã trả xe & quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 2, 2, DATEADD(DAY,-12,GETDATE()), DATEADD(DAY,-15,GETDATE())),
('TX-2026-020', 2, 5, N'Lê Tấn Phát', '079201009876', '0969888999', '202', DATEADD(DAY,-15,GETDATE()), DATEADD(DAY,-11,GETDATE()), DATEADD(DAY,-11,GETDATE()), NULL, 0, NULL, 380000, 720000, 0, 720000, 1, N'Khách thuê AirBlade công tác', N'Đã trả xe & quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-11,GETDATE()), DATEADD(DAY,-15,GETDATE())),
('TX-2026-021', 15, 14, N'Lê Thị Ngọc Ánh', '079185002345', '0979334455', '502', DATEADD(HOUR,-4,DATEADD(DAY,-15,GETDATE())), DATEADD(HOUR,-1,DATEADD(DAY,-15,GETDATE())), DATEADD(HOUR,-1,DATEADD(DAY,-15,GETDATE())), NULL, 0, NULL, 100000, 100000, 0, 100000, 1, N'Khách thuê Sirius dạo phố', N'Đã trả xe', N'Thanh toán toàn bộ', N'Hoàn tất', NULL, NULL, NULL, NULL, 2, 2, DATEADD(HOUR,-1,DATEADD(DAY,-15,GETDATE())), DATEADD(HOUR,-4,DATEADD(DAY,-15,GETDATE()))),
('TX-2026-022', 19, 16, N'Nguyễn Văn Khách', '079190001111', '0988112233', NULL, DATEADD(DAY,-16,GETDATE()), DATEADD(DAY,-15,GETDATE()), DATEADD(DAY,-15,GETDATE()), NULL, 0, NULL, 200000, 220000, 0, 220000, 1, N'Khách thuê xe Wave RSX', N'Đã hoàn tất trả xe', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-15,GETDATE()), DATEADD(DAY,-16,GETDATE())),
('TX-2026-023', 8, 4, N'Hoàng Mai Linh', '079194007788', '0978112244', '201', DATEADD(DAY,-14,GETDATE()), DATEADD(DAY,-12,GETDATE()), DATEADD(DAY,-12,GETDATE()), NULL, 0, NULL, 120000, 240000, 0, 240000, 1, N'Khách thuê Future leo dốc đồi chè', N'Xe sạch đẹp, đã quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-12,GETDATE()), DATEADD(DAY,-14,GETDATE())),
('TX-2026-024', 9, 6, N'Bùi Thanh Tùng', '079196001122', '0938445566', '203', DATEADD(DAY,-14,GETDATE()), DATEADD(DAY,-11,GETDATE()), DATEADD(DAY,-11,GETDATE()), NULL, 0, NULL, 250000, 510000, 0, 510000, 1, N'Khách thuê Winner X săn mây', N'Xe sạch đẹp, đã quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-11,GETDATE()), DATEADD(DAY,-14,GETDATE())),
('TX-2026-025', 13, 11, N'Trần Thu Hà', '079189006677', '0919223344', '402', DATEADD(DAY,-13,GETDATE()), DATEADD(DAY,-10,GETDATE()), DATEADD(DAY,-10,GETDATE()), NULL, 0, NULL, 330000, 660000, 0, 660000, 1, N'Khách thuê Liberty dạo phố cuối tuần', N'Xe sạch đẹp, đã quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-10,GETDATE()), DATEADD(DAY,-13,GETDATE())),
('TX-2026-026', 4, 12, N'Đỗ Minh Khang', '079187008899', '0987556677', '403', DATEADD(DAY,-13,GETDATE()), DATEADD(DAY,-9,GETDATE()), DATEADD(DAY,-9,GETDATE()), NULL, 0, NULL, 350000, 680000, 0, 680000, 1, N'Khách thuê Exciter đi phượt', N'Xe sạch đẹp, đã quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-9,GETDATE()), DATEADD(DAY,-13,GETDATE())),
('TX-2026-027', 18, 16, N'Trần Văn Bình', '079197008888', '0903112233', NULL, DATEADD(DAY,-12,GETDATE()), DATEADD(DAY,-9,GETDATE()), DATEADD(DAY,-9,GETDATE()), NULL, 0, NULL, 300000, 600000, 0, 600000, 1, N'Khách thuê Vario 160', N'Xe sạch đẹp, đã quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-9,GETDATE()), DATEADD(DAY,-12,GETDATE())),
('TX-2026-028', 17, 16, N'Lê Hoàng Long', '079191002222', '0912445566', NULL, DATEADD(DAY,-12,GETDATE()), DATEADD(DAY,-8,GETDATE()), DATEADD(DAY,-8,GETDATE()), NULL, 0, NULL, 200000, 720000, 0, 720000, 1, N'Khách thuê Raider phượt đèo', N'Xe sạch đẹp, đã quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-8,GETDATE()), DATEADD(DAY,-12,GETDATE())),
('TX-2026-029', 14, 13, N'Nguyễn Hoàng Long', '079186001234', '0903889900', '501', DATEADD(DAY,-11,GETDATE()), DATEADD(DAY,-7,GETDATE()), DATEADD(DAY,-7,GETDATE()), NULL, 0, NULL, 300000, 680000, 0, 680000, 1, N'Khách thuê PG-1 trải nghiệm dã ngoại', N'Xe sạch đẹp, đã quyết toán', N'Thanh toán toàn bộ', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-7,GETDATE()), DATEADD(DAY,-11,GETDATE())),
('TX-2026-030', 3, 7, N'Nguyễn Anh Thư', '079195003456', '0918777666', '301', DATEADD(DAY,-10,GETDATE()), DATEADD(DAY,-6,GETDATE()), DATEADD(DAY,-6,GETDATE()), NULL, 0, NULL, 300000, 600000, 0, 600000, 1, N'Khách thuê xe Vision đi café', N'Xe sạch đẹp, đã quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-6,GETDATE()), DATEADD(DAY,-10,GETDATE())),
('TX-2026-031', 5, 16, N'Phan Văn Hưng', '079198004455', '0915667788', NULL, DATEADD(DAY,-9,GETDATE()), DATEADD(DAY,-6,GETDATE()), DATEADD(DAY,-6,GETDATE()), NULL, 0, NULL, 100000, 300000, 0, 300000, 1, N'Khách vãng lai thuê Wave tiết kiệm', N'Xe sạch đẹp, đã quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-6,GETDATE()), DATEADD(DAY,-9,GETDATE())),
('TX-2026-032', 1, 16, N'Đặng Quốc Huy', '079199003322', '0908332211', NULL, DATEADD(DAY,-8,GETDATE()), DATEADD(DAY,-5,GETDATE()), DATEADD(DAY,-5,GETDATE()), NULL, 0, NULL, 450000, 900000, 0, 900000, 1, N'Khách thuê SH 150i cao cấp', N'Xe sạch đẹp, đã quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-5,GETDATE()), DATEADD(DAY,-8,GETDATE())),
('TX-2026-033', 6, 16, N'Vũ Thị Hạnh', '079192005544', '0937221144', NULL, DATEADD(DAY,-8,GETDATE()), DATEADD(DAY,-5,GETDATE()), DATEADD(DAY,-5,GETDATE()), NULL, 0, NULL, 240000, 480000, 0, 480000, 1, N'Khách thuê Lead cốp rộng', N'Xe sạch đẹp, đã quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-5,GETDATE()), DATEADD(DAY,-8,GETDATE())),
('TX-2026-034', 10, 16, N'Ngô Thanh Tùng', '079193006677', '0914889922', NULL, DATEADD(DAY,-7,GETDATE()), DATEADD(DAY,-3,GETDATE()), DATEADD(DAY,-3,GETDATE()), NULL, 0, NULL, 420000, 1120000, 0, 1120000, 1, N'Khách thuê Vespa du lịch dài ngày', N'Xe sạch đẹp, đã quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-3,GETDATE()), DATEADD(DAY,-7,GETDATE())),
('TX-2026-035', 2, 16, N'Trịnh Văn Nam', '079194008899', '0903554433', NULL, DATEADD(DAY,-6,GETDATE()), DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-2,GETDATE()), NULL, 0, NULL, 360000, 720000, 0, 720000, 1, N'Khách thuê AirBlade', N'Xe sạch đẹp, đã quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-6,GETDATE())),
('TX-2026-036', 8, 16, N'Lý Quốc Bảo', '079195007788', '0918443322', NULL, DATEADD(DAY,-5,GETDATE()), DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-2,GETDATE()), NULL, 0, NULL, 240000, 360000, 0, 360000, 1, N'Khách thuê Future đi công tác', N'Xe sạch đẹp, đã quyết toán', N'Đặt cọc trước', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-5,GETDATE())),
('TX-2026-037', 20, 16, N'Hồ Minh Tuấn', '079196009900', '0909665544', NULL, DATEADD(DAY,-4,GETDATE()), DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-2,GETDATE()), NULL, 0, NULL, 300000, 480000, 0, 480000, 1, N'Khách thuê Exciter', N'Xe sạch đẹp, đã quyết toán', N'Thanh toán toàn bộ', N'Hoàn tất', NULL, NULL, NULL, NULL, 1, 1, DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-4,GETDATE())),
('TX-2026-038', 19, 16, N'Võ Thị Thu Thảo', '079197001122', '0916332211', NULL, DATEADD(DAY,-13,GETDATE()), DATEADD(DAY,-10,GETDATE()), NULL, NULL, 0, NULL, 200000, 330000, 0, 0, 0, N'Khách đổi lịch bay, hủy trước 2 ngày', N'Xe sạch đẹp', N'Đặt cọc trước', N'Đã hủy - Hoàn cọc', DATEADD(DAY,-15,GETDATE()), N'Khách hủy trước 2 ngày, hoàn cọc 100%', 1, 200000, 1, NULL, NULL, DATEADD(DAY,-15,GETDATE())),
('TX-2026-039', 11, 16, N'Bùi Văn Hùng', '079198002233', '0908114455', NULL, DATEADD(DAY,-10,GETDATE()), DATEADD(DAY,-7,GETDATE()), NULL, NULL, 0, NULL, 250000, 500000, 0, 0, 0, N'Khách báo hủy đột xuất sát ngày', N'Xe sạch đẹp', N'Đặt cọc trước', N'Đã hủy - Không hoàn cọc', DATEADD(DAY,-10,GETDATE()), N'Hủy sát ngày, không hoàn cọc', 1, 0, 1, NULL, NULL, DATEADD(DAY,-11,GETDATE())),
-- Giao dịch thuê xe MỚI Hôm nay (GETDATE()) và Ngày mai (DATEADD(DAY,1,GETDATE()))
('TX-2026-040', 1, 1, N'Đoàn Minh Trí', '079198001234', '0908112233', '101', DATEADD(HOUR,-3,GETDATE()), DATEADD(DAY,1,GETDATE()), NULL, NULL, 0, NULL, 300000, 600000, 0, 600000, 0, N'Khách phòng 101 thuê SH 150i hôm nay đến ngày mai', N'Đang thuê tham quan Đà Lạt', N'Đặt cọc trước', N'Đang thuê', NULL, NULL, NULL, NULL, 1, NULL, NULL, DATEADD(HOUR,-3,GETDATE())),
('TX-2026-041', 6, 2, N'Trịnh Hồng Quân', '079192003344', '0909123456', '102', DATEADD(HOUR,-2,GETDATE()), DATEADD(DAY,2,GETDATE()), NULL, NULL, 0, NULL, 240000, 480000, 0, 480000, 0, N'Khách phòng 102 thuê Lead 125 hôm nay', N'Đang sử dụng xe', N'Đặt cọc trước', N'Đang thuê', NULL, NULL, NULL, NULL, 2, NULL, NULL, DATEADD(HOUR,-2,GETDATE())),
('TX-2026-042', 7, 3, N'Vũ Hoàng Nam', '079193005566', '0912334455', '103', DATEADD(HOUR,-4,GETDATE()), DATEADD(DAY,3,GETDATE()), NULL, NULL, 0, NULL, 300000, 680000, 0, 680000, 0, N'Khách phòng 103 thuê Grande Hybrid hôm nay', N'Đang thuê', N'Đặt cọc trước', N'Đang thuê', NULL, NULL, NULL, NULL, 1, NULL, NULL, DATEADD(HOUR,-4,GETDATE())),
('TX-2026-043', 10, 8, N'Đặng Hải Yến', '079197003355', '0982334411', '302', DATEADD(HOUR,-3,GETDATE()), DATEADD(DAY,2,GETDATE()), NULL, NULL, 0, NULL, 420000, 840000, 0, 840000, 0, N'Khách phòng 302 thuê Vespa Primavera hôm nay', N'Đang thuê', N'Đặt cọc trước', N'Đang thuê', NULL, NULL, NULL, NULL, 2, NULL, NULL, DATEADD(HOUR,-3,GETDATE())),
('TX-2026-044', 2, 5, N'Lê Tấn Phát', '079201009876', '0969888999', '202', DATEADD(HOUR,-1,GETDATE()), DATEADD(DAY,2,GETDATE()), NULL, NULL, 0, NULL, 200000, 540000, 0, 540000, 0, N'Khách phòng 202 thuê AirBlade 160 hôm nay', N'Đang thuê', N'Đặt cọc trước', N'Đang thuê', NULL, NULL, NULL, NULL, 1, NULL, NULL, DATEADD(HOUR,-1,GETDATE())),
('TX-2026-045', 15, 14, N'Lê Thị Ngọc Ánh', '079185002345', '0979334455', '502', DATEADD(HOUR,-5,GETDATE()), DATEADD(HOUR,-1,GETDATE()), DATEADD(HOUR,-1,GETDATE()), NULL, 0, NULL, 100000, 100000, 0, 100000, 1, N'Khách phòng 502 thuê Sirius sáng nay', N'Đã trả xe sáng nay', N'Thanh toán toàn bộ', N'Hoàn tất', NULL, NULL, NULL, NULL, 2, 2, DATEADD(HOUR,-1,GETDATE()), DATEADD(HOUR,-5,GETDATE())),
('TX-2026-046', 8, 4, N'Hoàng Mai Linh', '079194007788', '0978112244', '201', DATEADD(DAY,1,GETDATE()), DATEADD(DAY,3,GETDATE()), NULL, NULL, 0, NULL, 120000, 240000, 0, 240000, 0, N'Khách phòng 201 đặt thuê xe Future nhận ngày mai', N'Xe sạch đẹp', N'Đặt cọc trước', N'Đã đặt', NULL, NULL, NULL, NULL, 1, NULL, NULL, GETDATE()),
('TX-2026-047', 9, 6, N'Bùi Thanh Tùng', '079196001122', '0938445566', '203', DATEADD(DAY,1,GETDATE()), DATEADD(DAY,4,GETDATE()), NULL, NULL, 0, NULL, 250000, 510000, 0, 510000, 0, N'Khách phòng 203 đặt thuê Winner X nhận ngày mai', N'Xe sạch đẹp', N'Đặt cọc trước', N'Đã đặt', NULL, NULL, NULL, NULL, 1, NULL, NULL, GETDATE()),
('TX-2026-048', 3, 7, N'Nguyễn Anh Thư', '079195003456', '0918777666', '301', DATEADD(DAY,1,GETDATE()), DATEADD(DAY,3,GETDATE()), NULL, NULL, 0, NULL, 300000, 300000, 0, 300000, 1, N'Khách phòng 301 đặt thuê Vision ngày mai (Đã thanh toán toàn bộ)', N'Xe sạch đẹp', N'Thanh toán toàn bộ', N'Đã đặt', NULL, NULL, NULL, NULL, 1, 1, GETDATE(), GETDATE()),
('TX-2026-049', 13, 11, N'Trần Thu Hà', '079189006677', '0919223344', '402', DATEADD(DAY,1,GETDATE()), DATEADD(DAY,3,GETDATE()), NULL, NULL, 0, NULL, 220000, 440000, 0, 440000, 0, N'Khách phòng 402 đặt thuê Liberty nhận ngày mai', N'Xe sạch đẹp', N'Đặt cọc trước', N'Đã đặt', NULL, NULL, NULL, NULL, 2, NULL, NULL, GETDATE());
GO

-- 7.6 Bãi đỗ xe mẫu (Các lượt gửi xe quá khứ trước hôm nay đã thanh toán hết; kèm xe đang gửi & phát sinh mới Hôm nay & Ngày mai)
INSERT INTO BaiDoXe (MaVe, BienSoXe, LoaiXe, TenKhach, SoDienThoai, SoPhong, ThoiGianVao, ThoiGianRa, HinhThucGui, PhiGui, DaThanhToan, GhiChuSuaChua, NguoiTaoId, NguoiThanhToanId, ThoiGianThanhToan) VALUES
-- Dữ liệu mẫu đã qua (Trước ngày hôm nay -> Đã rời bãi & thanh toán hết)
('VE-DX-001', '49A-123.45', N'Ô tô',          N'Trần Văn An',        '0912345678', NULL,  DATEADD(DAY,-15,GETDATE()),DATEADD(DAY,-12,GETDATE()),N'Theo ngày',              150000,1, NULL, 1, 1, DATEADD(DAY,-12,GETDATE())),
('VE-DX-002', '59-X1 888.88', N'Xe máy',      N'Lê Thị Bình',        '0987654321', NULL,  DATEADD(DAY,-12,GETDATE()),DATEADD(DAY,-9,GETDATE()), N'Theo ngày',              45000, 1, NULL, 2, 2, DATEADD(DAY,-9,GETDATE())),
('VE-DX-003', '60B-456.78', N'Ô tô',          N'Nguyễn Văn Cường',   '0933112233', NULL,  DATEADD(DAY,-10,GETDATE()),DATEADD(DAY,-7,GETDATE()), N'Theo ngày',              150000,1, NULL, 1, 1, DATEADD(DAY,-7,GETDATE())),
('VE-DX-004', '49B-999.11', N'Xe máy',        N'Phạm Minh Đức',      '0908223344', NULL,  DATEADD(DAY,-8,GETDATE()), DATEADD(DAY,-6,GETDATE()), N'Theo ngày',              30000, 1, NULL, 3, 3, DATEADD(DAY,-6,GETDATE())),
('VE-DX-005', '51F-777.99', N'Ô tô',          N'Vũ Hoàng Khang',     '0919334455', NULL,  DATEADD(DAY,-6,GETDATE()), DATEADD(DAY,-4,GETDATE()), N'Theo ngày',              100000,1, NULL, 1, 1, DATEADD(DAY,-4,GETDATE())),
('VE-DX-006', '59-Z2 333.22', N'Xe máy',      N'Khách vãng lai A',   '0900112233', NULL,  DATEADD(DAY,-5,GETDATE()), DATEADD(DAY,-4,GETDATE()), N'Theo ngày',              15000, 1, NULL, 2, 2, DATEADD(DAY,-4,GETDATE())),
('VE-DX-007', '51G-111.22', N'Ô tô',          N'Nguyễn Thành Đạt',   '0933444111', NULL,  DATEADD(DAY,-4,GETDATE()), DATEADD(DAY,-3,GETDATE()), N'Theo ngày',              50000, 1, NULL, 2, 2, DATEADD(DAY,-3,GETDATE())),
('VE-DX-008', '49A-222.33', N'Ô tô',          N'Lê Quang Vinh',      '0912334111', NULL,  DATEADD(DAY,-4,GETDATE()), DATEADD(DAY,-2,GETDATE()), N'Theo ngày',              100000,1, NULL, 1, 1, DATEADD(DAY,-2,GETDATE())),
('VE-DX-009', '29B-333.44', N'Ô tô',          N'Hoàng Gia Bảo',      '0903889111', NULL,  DATEADD(DAY,-3,GETDATE()), DATEADD(DAY,-2,GETDATE()), N'Theo ngày',              50000, 1, NULL, 3, 3, DATEADD(DAY,-2,GETDATE())),
('VE-DX-010', '59-S3 444.11', N'Xe máy',      N'Phan Thanh Bình',    '0969888111', NULL,  DATEADD(DAY,-3,GETDATE()), DATEADD(DAY,-2,GETDATE()), N'Theo ngày',              15000, 1, NULL, 1, 1, DATEADD(DAY,-2,GETDATE())),
('VE-DX-011', '60A-998.22', N'Ô tô',          N'Hoàng Trọng Khôi',   '0908771122', NULL,  DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-1,GETDATE()), N'Theo tiếng',             100000,1, NULL, 2, 2, DATEADD(DAY,-1,GETDATE())),
('VE-DX-012', '49-M1 555.66', N'Xe máy',      N'Nguyễn Văn Khách',   '0988112233', NULL,  DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-1,GETDATE()), N'Theo lượt',              5000,  1, NULL, 1, 1, DATEADD(DAY,-1,GETDATE())),
('VE-DX-013', '59-A1 999.88', N'Xe máy',      N'Khách vãng lai B',   '0900111222', NULL,  DATEADD(DAY,-1,GETDATE()), DATEADD(HOUR,-12,GETDATE()),N'Theo lượt',             5000,  1, N'Xe ra an toàn', 2, 2, DATEADD(HOUR,-12,GETDATE())),
('VE-DX-014', '51H-234.56', N'Ô tô',          N'Trần Văn Nam',       '0938112299', NULL,  DATEADD(DAY,-1,GETDATE()), DATEADD(HOUR,-10,GETDATE()),N'Theo tiếng',            150000,1, NULL, 1, 1, DATEADD(HOUR,-10,GETDATE())),
-- Giao dịch bãi đỗ xe MỚI Hôm nay (GETDATE())
('VE-DX-015', '51G-123.45', N'Ô tô',          N'Phạm Quốc Bảo',      '0933444555', '401', DATEADD(HOUR,-5,GETDATE()), NULL,                      N'Khách phòng (Miễn phí)', 0,     1, NULL, 2, 2, DATEADD(HOUR,-5,GETDATE())),
('VE-DX-016', '49A-567.89', N'Ô tô',          N'Vũ Hoàng Nam',       '0912334455', '103', DATEADD(HOUR,-6,GETDATE()), NULL,                      N'Khách phòng (Miễn phí)', 0,     1, NULL, 1, 1, DATEADD(HOUR,-6,GETDATE())),
('VE-DX-017', '29B-888.99', N'Ô tô',          N'Nguyễn Hoàng Long',  '0903889900', '501', DATEADD(HOUR,-4,GETDATE()), NULL,                      N'Khách phòng (Miễn phí)', 0,     1, NULL, 3, 3, DATEADD(HOUR,-4,GETDATE())),
('VE-DX-018', '59-S3 888.11', N'Xe máy',      N'Lê Tấn Phát',        '0969888999', '202', DATEADD(HOUR,-3,GETDATE()), NULL,                      N'Khách phòng (Miễn phí)', 0,     1, NULL, 1, 1, DATEADD(HOUR,-3,GETDATE())),
('VE-DX-019', '60A-686.86', N'Ô tô',          N'Đinh Văn Toàn',      '0908667788', NULL,  DATEADD(HOUR,-2,GETDATE()), NULL,                      N'Theo tiếng',             100000,0, NULL, 2, NULL, NULL),
('VE-DX-020', '49-B1 666.88', N'Xe máy',      N'Lâm Chấn Huy',       '0988223344', NULL,  DATEADD(MINUTE,-75,GETDATE()),NULL,                    N'Theo lượt',              5000,  0, NULL, 1, NULL, NULL),
('VE-DX-021', '51K-999.68', N'Ô tô',          N'Trương Minh Phú',    '0918223399', NULL,  DATEADD(HOUR,-4,GETDATE()), DATEADD(HOUR,-1,GETDATE()),N'Theo tiếng',             150000,1, N'Khách dự hội thảo sáng nay', 1, 1, DATEADD(HOUR,-1,GETDATE()));
GO

-- 7.7 Đơn giặt ủi mẫu (Các đơn quá khứ trước hôm nay đã Hoàn tất & Thanh toán hết; kèm các đơn phát sinh mới Hôm nay & Ngày mai)
INSERT INTO DonGiatUi (MaDon, KhachHangId, TenKhach, SoDienThoai, SoPhong, DoiTacId, LoaiDichVu, KhoiLuongKg, DonGiaKg, TongTienThuKhach, TienKhachSanNhan, TienDoiTacNhan, TinhTrangQuanAoLucNhan, NgayNhan, NgayGiaoDoiTac, NgayHenTra, NgayTraThucTe, HinhThucThanhToan, TrangThai, DaThanhToan, NguoiTaoId, NguoiThanhToanId, ThoiGianThanhToan, CoDenBu, BenChiuTrachNhiem, SoTienDenBu, LyDoDenBu, NgayGhiNhanDenBu, NguoiGhiNhanDenBuId) VALUES
-- Dữ liệu mẫu đã qua (Trước ngày hôm nay -> Đã hoàn tất & thanh toán hết)
('DH-GU-001', 1, N'Đoàn Minh Trí',     '0908112233', '101', 1,    N'Giặt sấy thông thường', 4.0, 30000, 120000, 36000,  84000,  N'Đồ du lịch đợt 1',            DATEADD(DAY,-25,GETDATE()),DATEADD(DAY,-25,GETDATE()),DATEADD(DAY,-24,GETDATE()),DATEADD(DAY,-24,GETDATE()),N'Thanh toán trực tiếp', N'Hoàn tất', 1, 1, 1, DATEADD(DAY,-24,GETDATE()), 0, NULL, 0, NULL, NULL, NULL),
('DH-GU-002', 3, N'Vũ Hoàng Nam',      '0912334455', '103', 2,    N'Giặt hấp cao cấp',     2.5, 80000, 200000, 70000, 130000,  N'Áo măng tô dạ hội',           DATEADD(DAY,-22,GETDATE()),DATEADD(DAY,-22,GETDATE()),DATEADD(DAY,-21,GETDATE()),DATEADD(DAY,-21,GETDATE()),N'Ghi nợ vào phòng',   N'Hoàn tất', 1, 2, 2, DATEADD(DAY,-21,GETDATE()), 0, NULL, 0, NULL, NULL, NULL),
('DH-GU-003', 7, N'Nguyễn Anh Thư',    '0918777666', '301', 1,    N'Ủi phẳng',              3.0, 40000, 120000, 36000,  84000,  N'6 váy lụa nhẹ',               DATEADD(DAY,-20,GETDATE()),DATEADD(DAY,-20,GETDATE()),DATEADD(DAY,-19,GETDATE()),DATEADD(DAY,-19,GETDATE()),N'Thanh toán trực tiếp', N'Hoàn tất', 1, 1, 1, DATEADD(DAY,-19,GETDATE()), 0, NULL, 0, NULL, NULL, NULL),
('DH-GU-004', 5, N'Lê Tấn Phát',       '0969888999', '202', 1,    N'Giặt sấy thông thường', 5.0, 30000, 150000, 45000, 105000,  N'Quần áo thể thao (Có đền bù)',DATEADD(DAY,-18,GETDATE()),DATEADD(DAY,-18,GETDATE()),DATEADD(DAY,-17,GETDATE()),DATEADD(DAY,-17,GETDATE()),N'Ghi nợ vào phòng',   N'Hoàn tất', 1, 3, 3, DATEADD(DAY,-17,GETDATE()), 1, N'DoiTac', 100000, N'Tiệm giặt làm phai cúc áo, đã bồi thường 100k', DATEADD(DAY,-17,GETDATE()), 1),
('DH-GU-005', 4, N'Hoàng Mai Linh',    '0978112244', '201', NULL, N'Giặt sấy thông thường', 2.0, 30000,  60000,  60000,  0,      N'Khách đổi ý nhận lại tự giặt',DATEADD(DAY,-16,GETDATE()),NULL,                      DATEADD(DAY,-15,GETDATE()),NULL,                      N'Thanh toán trực tiếp', N'Đã hủy',   0, 1, NULL, NULL,                     0, NULL, 0, NULL, NULL, NULL),
('DH-GU-006', 9, N'Phan Văn Đức',      '0945667788', '303', 2,    N'Giặt hấp cao cấp',     3.0, 80000, 240000, 84000, 156000,  N'2 bộ vest dự lễ',             DATEADD(DAY,-15,GETDATE()),DATEADD(DAY,-15,GETDATE()),DATEADD(DAY,-14,GETDATE()),DATEADD(DAY,-14,GETDATE()),N'Ghi nợ vào phòng',   N'Hoàn tất', 1, 2, 2, DATEADD(DAY,-14,GETDATE()), 0, NULL, 0, NULL, NULL, NULL),
('DH-GU-007', 6, N'Bùi Thanh Tùng',    '0938445566', '203', 1,    N'Giặt sấy thông thường', 4.5, 30000, 135000, 40500,  94500,  N'Đồ cotton gia đình',          DATEADD(DAY,-14,GETDATE()),DATEADD(DAY,-14,GETDATE()),DATEADD(DAY,-13,GETDATE()),DATEADD(DAY,-13,GETDATE()),N'Thanh toán trực tiếp', N'Hoàn tất', 1, 1, 1, DATEADD(DAY,-13,GETDATE()), 0, NULL, 0, NULL, NULL, NULL),
('DH-GU-008', 8, N'Đặng Hải Yến',      '0982334411', '302', 2,    N'Giặt hấp cao cấp',     2.0, 80000, 160000, 56000, 104000,  N'Đầm lụa cao cấp',             DATEADD(DAY,-13,GETDATE()),DATEADD(DAY,-13,GETDATE()),DATEADD(DAY,-12,GETDATE()),DATEADD(DAY,-12,GETDATE()),N'Ghi nợ vào phòng',   N'Hoàn tất', 1, 2, 2, DATEADD(DAY,-12,GETDATE()), 0, NULL, 0, NULL, NULL, NULL),
('DH-GU-009', 11,N'Trần Thu Hà',       '0919223344', '402', 1,    N'Ủi phẳng',              2.5, 40000, 100000, 30000,  70000,  N'5 áo sơ mi văn phòng',        DATEADD(DAY,-12,GETDATE()),DATEADD(DAY,-12,GETDATE()),DATEADD(DAY,-11,GETDATE()),DATEADD(DAY,-11,GETDATE()),N'Thanh toán trực tiếp', N'Hoàn tất', 1, 1, 1, DATEADD(DAY,-11,GETDATE()), 0, NULL, 0, NULL, NULL, NULL),
('DH-GU-010', 12,N'Đỗ Minh Khang',     '0987556677', '403', 1,    N'Giặt sấy thông thường', 3.5, 30000, 105000, 31500,  73500,  N'Đồ phượt xe máy',             DATEADD(DAY,-11,GETDATE()),DATEADD(DAY,-11,GETDATE()),DATEADD(DAY,-10,GETDATE()),DATEADD(DAY,-10,GETDATE()),N'Ghi nợ vào phòng',   N'Hoàn tất', 1, 2, 2, DATEADD(DAY,-10,GETDATE()), 0, NULL, 0, NULL, NULL, NULL),
('DH-GU-011', 1, N'Đoàn Minh Trí',     '0908112233', '101', 2,    N'Giặt hấp cao cấp',     2.0, 80000, 160000, 56000, 104000,  N'Áo sơ mi lụa + quần tây',     DATEADD(DAY,-10,GETDATE()),DATEADD(DAY,-10,GETDATE()),DATEADD(DAY,-9,GETDATE()), DATEADD(DAY,-9,GETDATE()), N'Ghi nợ vào phòng',   N'Hoàn tất', 1, 1, 1, DATEADD(DAY,-9,GETDATE()),  0, NULL, 0, NULL, NULL, NULL),
('DH-GU-012', 5, N'Lê Tấn Phát',       '0969888999', '202', 1,    N'Giặt sấy thông thường', 3.5, 30000, 105000, 31500,  73500,  N'Đồ thể thao đi phượt',        DATEADD(DAY,-9,GETDATE()), DATEADD(DAY,-9,GETDATE()), DATEADD(DAY,-8,GETDATE()), DATEADD(DAY,-8,GETDATE()), N'Thanh toán trực tiếp', N'Hoàn tất', 1, 2, 2, DATEADD(DAY,-8,GETDATE()),  0, NULL, 0, NULL, NULL, NULL),
('DH-GU-013', 3, N'Vũ Hoàng Nam',      '0912334455', '103', 2,    N'Giặt hấp cao cấp',     2.5, 80000, 200000, 70000, 130000,  N'Áo măng tô dạ',               DATEADD(DAY,-8,GETDATE()), DATEADD(DAY,-8,GETDATE()), DATEADD(DAY,-7,GETDATE()), DATEADD(DAY,-7,GETDATE()), N'Ghi nợ vào phòng',   N'Hoàn tất', 1, 3, 3, DATEADD(DAY,-7,GETDATE()),  0, NULL, 0, NULL, NULL, NULL),
('DH-GU-014', 4, N'Hoàng Mai Linh',    '0978112244', '201', 1,    N'Giặt sấy thông thường', 3.0, 30000,  90000, 27000,  63000,  N'Váy hoa mùa hè',              DATEADD(DAY,-7,GETDATE()), DATEADD(DAY,-7,GETDATE()), DATEADD(DAY,-6,GETDATE()), DATEADD(DAY,-6,GETDATE()), N'Ghi nợ vào phòng',   N'Hoàn tất', 1, 1, 1, DATEADD(DAY,-6,GETDATE()),  0, NULL, 0, NULL, NULL, NULL),
('DH-GU-015', 6, N'Bùi Thanh Tùng',    '0938445566', '203', 1,    N'Giặt sấy thông thường', 4.0, 30000, 120000, 36000,  84000,  N'Quần áo ngủ + áo khoác gió',  DATEADD(DAY,-6,GETDATE()), DATEADD(DAY,-6,GETDATE()), DATEADD(DAY,-5,GETDATE()), DATEADD(DAY,-5,GETDATE()), N'Thanh toán trực tiếp', N'Hoàn tất', 1, 1, 1, DATEADD(DAY,-5,GETDATE()),  0, NULL, 0, NULL, NULL, NULL),
('DH-GU-016', 8, N'Đặng Hải Yến',      '0982334411', '302', 2,    N'Giặt hấp cao cấp',     1.5, 80000, 120000, 42000,  78000,  N'Đầm lụa tơ tằm',              DATEADD(DAY,-5,GETDATE()), DATEADD(DAY,-5,GETDATE()), DATEADD(DAY,-4,GETDATE()), DATEADD(DAY,-4,GETDATE()), N'Ghi nợ vào phòng',   N'Hoàn tất', 1, 2, 2, DATEADD(DAY,-4,GETDATE()),  0, NULL, 0, NULL, NULL, NULL),
('DH-GU-017', 9, N'Phan Văn Đức',      '0945667788', '303', 1,    N'Giặt sấy thông thường', 3.0, 30000,  90000, 27000,  63000,  N'Quần áo du lịch gia đình',    DATEADD(DAY,-5,GETDATE()), DATEADD(DAY,-5,GETDATE()), DATEADD(DAY,-4,GETDATE()), DATEADD(DAY,-4,GETDATE()), N'Thanh toán trực tiếp', N'Hoàn tất', 1, 1, 1, DATEADD(DAY,-4,GETDATE()),  0, NULL, 0, NULL, NULL, NULL),
('DH-GU-018', 13,N'Nguyễn Hoàng Long', '0903889900', '501', 1,    N'Ủi phẳng',              2.0, 40000,  80000, 24000,  56000,  N'Áo sơ mi họp sáng',           DATEADD(DAY,-4,GETDATE()), DATEADD(DAY,-4,GETDATE()), DATEADD(DAY,-3,GETDATE()), DATEADD(DAY,-3,GETDATE()), N'Ghi nợ vào phòng',   N'Hoàn tất', 1, 2, 2, DATEADD(DAY,-3,GETDATE()),  0, NULL, 0, NULL, NULL, NULL),
('DH-GU-019', 2, N'Trịnh Hồng Quân',   '0909123456', '102', 1,    N'Ủi phẳng',              2.5, 40000, 100000, 30000,  70000,  N'5 áo sơ mi công sở',          DATEADD(DAY,-4,GETDATE()), DATEADD(DAY,-4,GETDATE()), DATEADD(DAY,-3,GETDATE()), DATEADD(DAY,-3,GETDATE()), N'Ghi nợ vào phòng',   N'Hoàn tất', 1, 1, 1, DATEADD(DAY,-3,GETDATE()),  0, NULL, 0, NULL, NULL, NULL),
('DH-GU-020', 7, N'Nguyễn Anh Thư',    '0918777666', '301', 2,    N'Giặt hấp cao cấp',     2.0, 80000, 160000, 56000, 104000,  N'Váy dạ hội trắng honeymoon',  DATEADD(DAY,-3,GETDATE()), DATEADD(DAY,-3,GETDATE()), DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-2,GETDATE()), N'Ghi nợ vào phòng',   N'Hoàn tất', 1, 2, 2, DATEADD(DAY,-2,GETDATE()),  0, NULL, 0, NULL, NULL, NULL),
('DH-GU-021', 10,N'Phạm Quốc Bảo',     '0933444555', '401', 2,    N'Giặt hấp cao cấp',     3.0, 80000, 240000, 84000, 156000,  N'Vest cao cấp họp đối tác',    DATEADD(DAY,-3,GETDATE()), DATEADD(DAY,-3,GETDATE()), DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-2,GETDATE()), N'Thanh toán trực tiếp', N'Hoàn tất', 1, 1, 1, DATEADD(DAY,-2,GETDATE()),  0, NULL, 0, NULL, NULL, NULL),
('DH-GU-022', 14,N'Lê Thị Ngọc Ánh',   '0979334455', '502', 1,    N'Giặt sấy thông thường', 3.0, 30000,  90000, 27000,  63000,  N'Đồ len mỏng + khăn quàng',    DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-1,GETDATE()), DATEADD(DAY,-1,GETDATE()), N'Ghi nợ vào phòng',   N'Hoàn tất', 1, 2, 2, DATEADD(DAY,-1,GETDATE()),  0, NULL, 0, NULL, NULL, NULL),
('DH-GU-023', 11,N'Trần Thu Hà',       '0919223344', '402', 1,    N'Giặt sấy thông thường', 2.0, 30000,  60000, 18000,  42000,  N'Quần áo ngủ + áo khoác nhẹ',  DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-1,GETDATE()), DATEADD(DAY,-1,GETDATE()), N'Thanh toán trực tiếp', N'Hoàn tất', 1, 1, 1, DATEADD(DAY,-1,GETDATE()),  0, NULL, 0, NULL, NULL, NULL),
('DH-GU-024', 12,N'Đỗ Minh Khang',     '0987556677', '403', 1,    N'Giặt sấy thông thường', 3.0, 30000,  90000, 27000,  63000,  N'Quần áo công sở',             DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-2,GETDATE()), DATEADD(DAY,-1,GETDATE()), DATEADD(DAY,-1,GETDATE()), N'Ghi nợ vào phòng',   N'Hoàn tất', 1, 1, 1, DATEADD(DAY,-1,GETDATE()),  0, NULL, 0, NULL, NULL, NULL),
-- Giao dịch giặt ủi MỚI Hôm nay (GETDATE()) và Ngày mai (DATEADD(DAY,1,GETDATE()))
('DH-GU-025', 1, N'Đoàn Minh Trí',     '0908112233', '101', NULL, N'Giặt hấp cao cấp',     2.0, 80000, 160000, 160000, 0,      N'Áo sơ mi lụa + quần tây (Mới nhận hôm nay)', DATEADD(HOUR,-2,GETDATE()), NULL,                      DATEADD(DAY,1,GETDATE()),   NULL,                      N'Ghi nợ vào phòng',   N'Chờ giao đối tác', 0, 1, NULL, NULL, 0, NULL, 0, NULL, NULL, NULL),
('DH-GU-026', 5, N'Lê Tấn Phát',       '0969888999', '202', NULL, N'Giặt sấy thông thường', 3.5, 30000, 105000, 105000, 0,      N'Đồ thể thao đi phượt (Chờ gom tiệm hôm nay)', DATEADD(HOUR,-1,GETDATE()), NULL,                      DATEADD(DAY,1,GETDATE()),   NULL,                      N'Thanh toán trực tiếp', N'Chờ giao đối tác', 1, 2, 2, DATEADD(HOUR,-1,GETDATE()), 0, NULL, 0, NULL, NULL, NULL),
('DH-GU-027', 3, N'Vũ Hoàng Nam',      '0912334455', '103', 2,    N'Giặt hấp cao cấp',     2.5, 80000, 200000, 70000, 130000,  N'Áo măng tô dạ (Hẹn trả ngày mai)',            DATEADD(HOUR,-4,GETDATE()), DATEADD(HOUR,-3,GETDATE()), DATEADD(DAY,1,GETDATE()),   NULL,                      N'Ghi nợ vào phòng',   N'Đang giặt',        0, 3, NULL, NULL, 0, NULL, 0, NULL, NULL, NULL),
('DH-GU-028', 4, N'Hoàng Mai Linh',    '0978112244', '201', 1,    N'Giặt sấy thông thường', 3.0, 30000,  90000, 27000,  63000,  N'Váy hoa mùa hè (Đang giặt hôm nay)',          DATEADD(HOUR,-3,GETDATE()), DATEADD(HOUR,-2,GETDATE()), DATEADD(HOUR,5,GETDATE()),  NULL,                      N'Ghi nợ vào phòng',   N'Đang giặt',        0, 1, NULL, NULL, 0, NULL, 0, NULL, NULL, NULL),
('DH-GU-029', 6, N'Bùi Thanh Tùng',    '0938445566', '203', 1,    N'Giặt sấy thông thường', 4.0, 30000, 120000, 36000,  84000,  N'Quần áo ngủ + áo khoác gió (Đang giặt)',       DATEADD(HOUR,-5,GETDATE()), DATEADD(HOUR,-4,GETDATE()), DATEADD(HOUR,2,GETDATE()),  NULL,                         N'Thanh toán trực tiếp', N'Đang giặt',     1, 1, 1, DATEADD(HOUR,-5,GETDATE()), 0, NULL, 0, NULL, NULL, NULL),
('DH-GU-030', 8, N'Đặng Hải Yến',      '0982334411', '302', 2,    N'Giặt hấp cao cấp',     1.5, 80000, 120000, 42000,  78000,  N'Đầm lụa tơ tằm (Đang giặt chờ đối tác giao)',  DATEADD(HOUR,-4,GETDATE()), DATEADD(HOUR,-3,GETDATE()), DATEADD(HOUR,2,GETDATE()),  NULL,                         N'Ghi nợ vào phòng',   N'Đang giặt',     0, 2, NULL, NULL, 0, NULL, 0, NULL, NULL, NULL),
('DH-GU-031', 9, N'Phan Văn Đức',      '0945667788', '303', 1,    N'Giặt sấy thông thường', 3.0, 30000,  90000, 27000,  63000,  N'Quần áo du lịch gia đình (Hoàn tất hôm nay)',  DATEADD(HOUR,-6,GETDATE()), DATEADD(HOUR,-5,GETDATE()), DATEADD(HOUR,-1,GETDATE()), DATEADD(HOUR,-1,GETDATE()),  N'Thanh toán trực tiếp', N'Hoàn tất',      1, 1, 1, DATEADD(HOUR,-1,GETDATE()), 0, NULL, 0, NULL, NULL, NULL),
('DH-GU-032', 13,N'Nguyễn Hoàng Long', '0903889900', '501', NULL, N'Ủi phẳng',              2.5, 40000, 100000, 35000,  65000,  N'Bộ vest hội nghị ngày mai',                    DATEADD(DAY,1,GETDATE()),   NULL,                       DATEADD(DAY,2,GETDATE()),   NULL,                        N'Ghi nợ vào phòng',   N'Chờ giao đối tác', 0, 1, NULL, NULL, 0, NULL, 0, NULL, NULL, NULL);
GO

-- Tự động chạy thủ tục quyết toán dữ liệu quá khứ và đồng bộ ca trực
EXEC sp_TuDongQuyetToanDuLieuCu;
GO

