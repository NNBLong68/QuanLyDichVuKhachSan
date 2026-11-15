using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyDichVuKhachSan
{
    public partial class LaundryServiceView : UserControl
    {
        private ObservableCollection<DonGiatUiHienThi> _dsGiatUi;

        private readonly string[] _cacBuocTienDo =
        {
            "Đã tiếp nhận",
            "Đang giặt",
            "Đang sấy",
            "Đang ủi",
            "Hoàn thành",
            "Đã giao khách"
        };

        public LaundryServiceView()
        {
            InitializeComponent();
            TaiDuLieuGiatUi();
        }

        private void TaiDuLieuGiatUi()
        {
            if (_dsGiatUi == null)
            {
                _dsGiatUi = new ObservableCollection<DonGiatUiHienThi>
                {
                    new DonGiatUiHienThi("DH001", "Nguyễn Hằng (P.101)", "0234563 9806", "Giặt sấy thường (Theo kg)", "Đang giặt", "50.000"),
                    new DonGiatUiHienThi("DH002", "Nguyễn Nganh (P.205)", "0234563 9809", "Giặt ủi cao cấp (Theo bộ)", "Đang sấy", "85.000"),
                    new DonGiatUiHienThi("DH003", "Nguyễn Hằng (P.101)", "0234559 9809", "Giặt khô / Hấp Vest - Áo dài", "Đã tiếp nhận", "120.000"),
                    new DonGiatUiHienThi("DH004", "Lê Minh Tâm (P.302)", "0234564 9808", "Giặt sấy thường (Theo kg)", "Đã tiếp nhận", "45.000"),
                    new DonGiatUiHienThi("DH005", "Trần Minh Quân (P.201)", "0234554 9809", "Giặt ủi cao cấp (Theo bộ)", "Đang giặt", "85.000"),
                    new DonGiatUiHienThi("DH006", "Lê Thu Thảo (P.402)", "0334558 9809", "Giặt chăn ga / Đồ vải khách sạn", "Hoàn thành", "150.000"),
                    new DonGiatUiHienThi("DH007", "Phạm Hoàng Nam (P.104)", "09749 272037", "Giặt sấy thường (Theo kg)", "Đang ủi", "50.000"),
                    new DonGiatUiHienThi("DH008", "Võ Gia Huy (P.305)", "09749 278038", "Giặt ủi cao cấp (Theo bộ)", "Đã giao khách", "95.000")
                };
            }

            CapNhatThongKeGiatUi(_dsGiatUi);
            TaoMaDonTiepTheo();
        }

        private void CapNhatThongKeGiatUi(IEnumerable<DonGiatUiHienThi> danhSachHienThi)
        {
            if (dgDonGiatUi != null)
                dgDonGiatUi.ItemsSource = danhSachHienThi.ToList();

            if (_dsGiatUi == null) return;

            if (txtTongDonGiatUi != null)
                txtTongDonGiatUi.Text = $"{_dsGiatUi.Count:00} đơn";

            if (txtDangXuLyGiatUi != null)
            {
                int dangXuLy = _dsGiatUi.Count(x => x.TrangThai == "Đang giặt" || x.TrangThai == "Đang sấy" || x.TrangThai == "Đang ủi");
                txtDangXuLyGiatUi.Text = $"{dangXuLy:00} đơn";
            }

            if (txtChoGiaoGiatUi != null)
            {
                int choGiao = _dsGiatUi.Count(x => x.TrangThai == "Hoàn thành");
                txtChoGiaoGiatUi.Text = $"{choGiao} đơn";
            }
        }

        private void TaoMaDonTiepTheo()
        {
            if (txtMaDonGui != null && _dsGiatUi != null)
            {
                txtMaDonGui.Text = $"DH{_dsGiatUi.Count + 1:000}";
            }
        }

        private void BtnCapNhatTienDo_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is DonGiatUiHienThi don)
            {
                int idx = Array.IndexOf(_cacBuocTienDo, don.TrangThai);
                if (idx >= 0 && idx < _cacBuocTienDo.Length - 1)
                {
                    string buocMoi = _cacBuocTienDo[idx + 1];
                    don.CapNhatTrangThai(buocMoi);
                    CapNhatThongKeGiatUi(_dsGiatUi);
                }
                else
                {
                    MessageBox.Show($"Đơn {don.MaDon} của khách [{don.TenKhachHang}] đã ở trạng thái cuối cùng (Đã giao khách)!",
                                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void BtnLichSuGiatUi_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is DonGiatUiHienThi don)
            {
                string lichSu = string.Join("\n", don.LichSuTienDo.Select(x => "  • " + x));
                MessageBox.Show(
                    $"MÃ ĐƠN: {don.MaDon} - KHÁCH: {don.TenKhachHang}\n" +
                    $"Dịch vụ: {don.DichVu} | Đơn giá: {don.DonGia} VNĐ\n" +
                    $"Trạng thái hiện tại: {don.TrangThai} (Tiến độ: {don.TienDo}%)\n" +
                    $"----------------------------------------------------\n" +
                    $"NHẬT KÝ TIẾN ĐỘ 6 BƯỚC:\n{lichSu}",
                    "Lịch Sử Tiến Độ Giặt Ủi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnTiepNhanDon_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKhachGui.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên khách hàng hoặc Số phòng!", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTenKhachGui.Focus();
                return;
            }

            string ma = string.IsNullOrWhiteSpace(txtMaDonGui.Text) ? $"DH{_dsGiatUi.Count + 1:000}" : txtMaDonGui.Text.Trim().ToUpper();
            string ten = txtTenKhachGui.Text.Trim();
            string sdt = string.IsNullOrWhiteSpace(txtSdtGui.Text) ? "0900 000 000" : txtSdtGui.Text.Trim();
            string dichVu = (cboLoaiGiatUi.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Giặt sấy thường (Theo kg)";
            string donGia = string.IsNullOrWhiteSpace(txtDonGiaGui.Text) ? "50.000" : txtDonGiaGui.Text.Trim();

            var donTonTai = _dsGiatUi.FirstOrDefault(x => x.MaDon.Equals(ma, StringComparison.OrdinalIgnoreCase));
            if (donTonTai != null)
            {
                donTonTai.TenKhachHang = ten;
                donTonTai.SoDienThoai = sdt;
                donTonTai.DichVu = dichVu;
                donTonTai.DonGia = donGia;
                donTonTai.LichSuTienDo.Add($"[{DateTime.Now:HH:mm dd/MM/yyyy}] Cập nhật thông tin đơn ({dichVu} - {donGia} đ)");
                CapNhatThongKeGiatUi(_dsGiatUi);
                MessageBox.Show($"Đã cập nhật thông tin đơn [{ma}] thành công!", "Cập nhật", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var donMoi = new DonGiatUiHienThi(ma, ten, sdt, dichVu, "Đã tiếp nhận", donGia);
                _dsGiatUi.Insert(0, donMoi);
                CapNhatThongKeGiatUi(_dsGiatUi);
                MessageBox.Show($"Đã tiếp nhận đơn giặt ủi mới [{ma}] cho khách {ten}!", "Tiếp nhận thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            BtnLamMoiFormGiatUi_Click(null, null);
        }

        private void BtnXacNhanGiaoHang_Click(object sender, RoutedEventArgs e)
        {
            if (dgDonGiatUi.SelectedItem is DonGiatUiHienThi don)
            {
                if (don.TrangThai == "Đã giao khách")
                {
                    MessageBox.Show($"Đơn {don.MaDon} đã được giao cho khách trước đó!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                don.CapNhatTrangThai("Đã giao khách");
                CapNhatThongKeGiatUi(_dsGiatUi);
                MessageBox.Show($"Đã xác nhận bàn giao đồ giặt ủi đơn [{don.MaDon}] cho khách {don.TenKhachHang}!",
                                "Xác nhận giao khách", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn trong bảng để xác nhận giao khách!", "Chưa chọn đơn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnChiTietDon_Click(object sender, RoutedEventArgs e)
        {
            if (dgDonGiatUi.SelectedItem is DonGiatUiHienThi don)
            {
                string lichSu = string.Join("\n", don.LichSuTienDo.Select(x => "  • " + x));
                MessageBox.Show(
                    $"THÔNG TIN CHI TIẾT ĐƠN GIẶT ỦI\n" +
                    $"====================================\n" +
                    $"Mã đơn:         {don.MaDon}\n" +
                    $"Khách hàng:     {don.TenKhachHang}\n" +
                    $"Số điện thoại:  {don.SoDienThoai}\n" +
                    $"Loại dịch vụ:   {don.DichVu}\n" +
                    $"Đơn giá:        {don.DonGia} VNĐ\n" +
                    $"Trạng thái:     {don.TrangThai} ({don.TienDo}%)\n\n" +
                    $"LỊCH SỬ XỬ LÝ:\n{lichSu}",
                    "Chi Tiết Đơn Giặt Ủi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng bấm chọn 1 đơn trong bảng danh sách bên trái để xem chi tiết!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DgDonGiatUi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDonGiatUi.SelectedItem is DonGiatUiHienThi don)
            {
                txtMaDonGui.Text = don.MaDon;
                txtTenKhachGui.Text = don.TenKhachHang;
                txtSdtGui.Text = don.SoDienThoai;
                txtDonGiaGui.Text = don.DonGia;

                foreach (ComboBoxItem item in cboLoaiGiatUi.Items)
                {
                    if (item.Content?.ToString() == don.DichVu)
                    {
                        cboLoaiGiatUi.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private void CboLoaiGiatUi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboLoaiGiatUi.SelectedItem is ComboBoxItem item && item.Tag != null && txtDonGiaGui != null)
            {
                if (dgDonGiatUi == null || dgDonGiatUi.SelectedItem == null)
                {
                    txtDonGiaGui.Text = item.Tag.ToString();
                }
            }
        }

        private void BtnLamMoiFormGiatUi_Click(object sender, RoutedEventArgs e)
        {
            if (dgDonGiatUi != null) dgDonGiatUi.SelectedItem = null;
            TaoMaDonTiepTheo();
            txtTenKhachGui?.Clear();
            txtSdtGui?.Clear();
            if (cboLoaiGiatUi != null) cboLoaiGiatUi.SelectedIndex = 0;
            if (txtDonGiaGui != null) txtDonGiaGui.Text = "50.000";
        }

        private void BtnLamMoiGiatUi_Click(object sender, RoutedEventArgs e)
        {
            txtTimKiemGiatUi?.Clear();
            if (txtTrangThaiLoc != null) txtTrangThaiLoc.Text = "Hiển thị: Tất cả các bước";
            CapNhatThongKeGiatUi(_dsGiatUi);
            BtnLamMoiFormGiatUi_Click(null, null);
        }

        private void TxtTimKiemGiatUi_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_dsGiatUi == null) return;
            string kw = txtTimKiemGiatUi.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(kw))
            {
                if (txtTrangThaiLoc != null) txtTrangThaiLoc.Text = "Hiển thị: Tất cả các bước";
                CapNhatThongKeGiatUi(_dsGiatUi);
            }
            else
            {
                if (txtTrangThaiLoc != null) txtTrangThaiLoc.Text = $"Tìm kiếm: \"{txtTimKiemGiatUi.Text}\"";
                dgDonGiatUi.ItemsSource = _dsGiatUi.Where(x =>
                    x.MaDon.ToLower().Contains(kw) ||
                    x.TenKhachHang.ToLower().Contains(kw) ||
                    x.SoDienThoai.ToLower().Contains(kw) ||
                    x.DichVu.ToLower().Contains(kw) ||
                    x.TrangThai.ToLower().Contains(kw)).ToList();
            }
        }

        private void CardLocTatCa_Click(object sender, MouseButtonEventArgs e)
        {
            if (txtTrangThaiLoc != null) txtTrangThaiLoc.Text = "Hiển thị: Tất cả các bước";
            CapNhatThongKeGiatUi(_dsGiatUi);
        }

        private void CardLocDangXuLy_Click(object sender, MouseButtonEventArgs e)
        {
            if (txtTrangThaiLoc != null) txtTrangThaiLoc.Text = "Đang lọc: Đơn đang xử lý (Giặt / Sấy / Ủi)";
            dgDonGiatUi.ItemsSource = _dsGiatUi.Where(x =>
                x.TrangThai == "Đang giặt" || x.TrangThai == "Đang sấy" || x.TrangThai == "Đang ủi").ToList();
        }

        private void CardLocChoGiao_Click(object sender, MouseButtonEventArgs e)
        {
            if (txtTrangThaiLoc != null) txtTrangThaiLoc.Text = "Đang lọc: Đơn đã hoàn thành chờ giao khách";
            dgDonGiatUi.ItemsSource = _dsGiatUi.Where(x => x.TrangThai == "Hoàn thành").ToList();
        }

        private void StepFilter_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement el && el.Tag is string buoc)
            {
                if (txtTrangThaiLoc != null) txtTrangThaiLoc.Text = $"Đang lọc theo bước: {buoc}";
                dgDonGiatUi.ItemsSource = _dsGiatUi.Where(x => x.TrangThai == buoc).ToList();
            }
        }
    }

    public class DonGiatUiHienThi : INotifyPropertyChanged
    {
        private string _maDon;
        public string MaDon
        {
            get => _maDon;
            set { _maDon = value; OnPropertyChanged(nameof(MaDon)); }
        }

        private string _tenKhachHang;
        public string TenKhachHang
        {
            get => _tenKhachHang;
            set { _tenKhachHang = value; OnPropertyChanged(nameof(TenKhachHang)); }
        }

        private string _soDienThoai;
        public string SoDienThoai
        {
            get => _soDienThoai;
            set { _soDienThoai = value; OnPropertyChanged(nameof(SoDienThoai)); }
        }

        private string _dichVu;
        public string DichVu
        {
            get => _dichVu;
            set { _dichVu = value; OnPropertyChanged(nameof(DichVu)); }
        }

        private string _donGia;
        public string DonGia
        {
            get => _donGia;
            set { _donGia = value; OnPropertyChanged(nameof(DonGia)); }
        }

        private string _trangThai;
        public string TrangThai
        {
            get => _trangThai;
            set { _trangThai = value; OnPropertyChanged(nameof(TrangThai)); }
        }

        private int _tienDo;
        public int TienDo
        {
            get => _tienDo;
            set { _tienDo = value; OnPropertyChanged(nameof(TienDo)); }
        }

        private Brush _mauNen;
        public Brush MauNen
        {
            get => _mauNen;
            set { _mauNen = value; OnPropertyChanged(nameof(MauNen)); }
        }

        private Brush _mauChu;
        public Brush MauChu
        {
            get => _mauChu;
            set { _mauChu = value; OnPropertyChanged(nameof(MauChu)); }
        }

        public List<string> LichSuTienDo { get; set; } = new List<string>();

        public DonGiatUiHienThi(string ma, string ten, string sdt, string dv, string tt, string gia)
        {
            MaDon = ma;
            TenKhachHang = ten;
            SoDienThoai = sdt;
            DichVu = dv;
            DonGia = gia;
            LichSuTienDo.Add($"[{DateTime.Now.AddMinutes(-30):HH:mm dd/MM/yyyy}] Khởi tạo đơn giặt ủi ({dv})");
            CapNhatTrangThai(tt);
        }

        public void CapNhatTrangThai(string trangThaiMoi)
        {
            TrangThai = trangThaiMoi;
            var bc = new BrushConverter();

            switch (trangThaiMoi)
            {
                case "Đã tiếp nhận":
                    TienDo = 16;
                    MauNen = (Brush)bc.ConvertFrom("#DCFCE7");
                    MauChu = (Brush)bc.ConvertFrom("#15803D");
                    break;
                case "Đang giặt":
                    TienDo = 35;
                    MauNen = (Brush)bc.ConvertFrom("#DBEAFE");
                    MauChu = (Brush)bc.ConvertFrom("#1D4ED8");
                    break;
                case "Đang sấy":
                    TienDo = 55;
                    MauNen = (Brush)bc.ConvertFrom("#FEF9C3");
                    MauChu = (Brush)bc.ConvertFrom("#A16207");
                    break;
                case "Đang ủi":
                    TienDo = 75;
                    MauNen = (Brush)bc.ConvertFrom("#E0E7FF");
                    MauChu = (Brush)bc.ConvertFrom("#4338CA");
                    break;
                case "Hoàn thành":
                    TienDo = 90;
                    MauNen = (Brush)bc.ConvertFrom("#FEE2E2");
                    MauChu = (Brush)bc.ConvertFrom("#B91C1C");
                    break;
                case "Đã giao khách":
                    TienDo = 100;
                    MauNen = (Brush)bc.ConvertFrom("#E2E8F0");
                    MauChu = (Brush)bc.ConvertFrom("#334155");
                    break;
            }

            LichSuTienDo.Add($"[{DateTime.Now:HH:mm dd/MM/yyyy}] Chuyển trạng thái -> {trangThaiMoi} ({TienDo}%)");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}