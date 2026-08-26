using System;

namespace QuanLyDichVuKhachSan
{
    public class NhanVienModel
    {
        public int MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public string SoDienThoai { get; set; }
        public string ChucVu { get; set; } // Admin, Lễ tân
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string CaTruc { get; set; } // Ca Sáng, Ca Chiều, Ca Đêm
        public string TrangThai { get; set; } // Hoạt động, Nghỉ việc

        public bool IsAdmin => string.Equals(ChucVu, "Admin", StringComparison.OrdinalIgnoreCase);
    }

    public class KhachHangModel
    {
        public int MaKhachHang { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
    }

    public class PhongModel
    {
        public int MaPhong { get; set; }
        public string SoPhong { get; set; }
        public string LoaiPhong { get; set; }
        public decimal GiaPhongTheoGio { get; set; }
        public decimal GiaPhongTheoNgay { get; set; }
        public string TrangThai { get; set; } // Trống, Đang sử dụng, Đang dọn dẹp, Bảo trì
    }

    public class DanhMucDichVuModel
    {
        public int MaDichVu { get; set; }
        public string TenDichVu { get; set; }
        public string NhomDichVu { get; set; } // Ăn uống tươi, Thực phẩm khô, Cho thuê xe, Sảnh sự kiện, Bãi đỗ xe, Giặt ủi
        public string PhanLoaiDichVu { get; set; } // Tự túc, Ngoại
        public decimal DonGia { get; set; }
        public int SoLuongTon { get; set; }
        public int SuatAnTheoNgay { get; set; }
        public string ThongTinChiTiet { get; set; }
        public string TrangThai { get; set; }
        public bool IsActive { get; set; }
    }

    public class DoiTacGiatUiModel
    {
        public int MaDoiTac { get; set; }
        public string TenDoiTac { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public decimal TyLeAnChiaKhachSan { get; set; }
    }

    public class PhieuDichVuModel
    {
        public int MaPhieu { get; set; }
        public string MaCodePhieu { get; set; }
        public int MaDichVu { get; set; }
        public int MaKhachHang { get; set; }
        public int? MaPhong { get; set; }
        public int MaNhanVien { get; set; }
        public int? MaDoiTac { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public decimal SoLuong_Kg_Ngay { get; set; }
        public decimal DonGia { get; set; }
        public decimal TienCoc { get; set; }
        public decimal ThuThemPhatSinh { get; set; }
        public decimal TongTien { get; set; }
        public decimal? TienKhachSanNhan { get; set; }
        public decimal? TienDoiTacNhan { get; set; }
        public string GhiChuChiTiet { get; set; }
        public string HinhThucTinh { get; set; } // Hóa đơn riêng, Tính vào tiền phòng
        public string TrangThai { get; set; } // Đang sử dụng, Đã hoàn tất, Đã hủy
    }

    public class LichSuNhapKhoModel
    {
        public int MaNhapKho { get; set; }
        public int MaDichVu { get; set; }
        public int MaNhanVienAdmin { get; set; }
        public DateTime NgayNhap { get; set; }
        public int SoLuongNhap { get; set; }
        public decimal DonGiaNhap { get; set; }
        public decimal TongTien { get; set; }
        public string GhiChu { get; set; }
    }

    public class HoaDonModel
    {
        public int MaHoaDon { get; set; }
        public string MaCodeHoaDon { get; set; }
        public int MaKhachHang { get; set; }
        public int? MaPhong { get; set; }
        public int MaNhanVien { get; set; }
        public int? MaPhieuDichVu { get; set; }
        public string LoaiHoaDon { get; set; } // Ăn uống, Sự kiện, Thuê xe, Bãi đỗ xe, Giặt ủi, Tổng hợp
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public decimal DaThanhToan { get; set; }
        public string PhuongThucThanhToan { get; set; } // Tiền mặt, Chuyển khoản, Thẻ
        public string TrangThai { get; set; } // Chưa thanh toán, Đã thanh toán, Đã hủy
    }

    public static class SessionManager
    {
        public static NhanVienModel CurrentUser { get; set; }
    }
}
