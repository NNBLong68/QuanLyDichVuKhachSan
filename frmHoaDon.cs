using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace QuanLyDichVuKhachSan
{
    public partial class frmHoaDon : Form
    {
        private int selectedMaHoaDon = 0;

        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            FormatDataGridView();
            LoadComboboxData();
            GenerateNewCode();
            LoadData();
        }

        private void FormatDataGridView()
        {
            dgvHoaDon.EnableHeadersVisualStyles = false;
            dgvHoaDon.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 23, 42);
            dgvHoaDon.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(52, 211, 153);
            dgvHoaDon.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvHoaDon.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            dgvHoaDon.DefaultCellStyle.ForeColor = Color.White;
            dgvHoaDon.DefaultCellStyle.SelectionBackColor = Color.FromArgb(79, 70, 229);
            dgvHoaDon.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvHoaDon.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvHoaDon.GridColor = Color.FromArgb(51, 65, 85);
        }

        private void GenerateNewCode()
        {
            txtMaCode.Text = "HD-" + DateTime.Now.ToString("yyMMdd-HHmmss");
        }

        private void LoadComboboxData()
        {
            try
            {
                // KhachHang
                DataTable dtKH = DatabaseHelper.ExecuteQuery("SELECT MaKhachHang, HoTen + ' (' + ISNULL(SoDienThoai, '') + ')' AS DisplayText FROM KhachHang");
                cboKhachHang.DataSource = dtKH;
                cboKhachHang.DisplayMember = "DisplayText";
                cboKhachHang.ValueMember = "MaKhachHang";

                // Phong
                DataTable dtP = DatabaseHelper.ExecuteQuery("SELECT MaPhong, 'Phòng ' + SoPhong + ' (' + LoaiPhong + ')' AS DisplayText FROM Phong");
                DataRow emptyP = dtP.NewRow();
                emptyP["MaPhong"] = DBNull.Value;
                emptyP["DisplayText"] = "-- Không chọn phòng --";
                dtP.Rows.InsertAt(emptyP, 0);
                cboPhong.DataSource = dtP;
                cboPhong.DisplayMember = "DisplayText";
                cboPhong.ValueMember = "MaPhong";

                // PhieuDichVu
                DataTable dtPDV = DatabaseHelper.ExecuteQuery("SELECT MaPhieu, MaCodePhieu + ' [' + CAST(CAST(TongTien AS INT) AS VARCHAR) + 'đ]' AS DisplayText, TongTien FROM PhieuDichVu WHERE TrangThai != N'Đã hủy'");
                DataRow emptyPDV = dtPDV.NewRow();
                emptyPDV["MaPhieu"] = DBNull.Value;
                emptyPDV["DisplayText"] = "-- Không gộp phiếu --";
                dtPDV.Rows.InsertAt(emptyPDV, 0);
                cboPhieu.DataSource = dtPDV;
                cboPhieu.DisplayMember = "DisplayText";
                cboPhieu.ValueMember = "MaPhieu";

                cboLoaiHD.SelectedIndex = 5; // Tổng hợp
                cboPhuongThuc.SelectedIndex = 0; // Tiền mặt
                cboTrangThai.SelectedIndex = 1; // Đã thanh toán
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục combobox hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                string sql = @"
                SELECT 
                    h.MaHoaDon,
                    h.MaCodeHoaDon AS [Mã Hóa Đơn],
                    kh.HoTen AS [Khách Hàng],
                    ISNULL(ph.SoPhong, N'Không') AS [Phòng],
                    ISNULL(pdv.MaCodePhieu, N'Không') AS [Mã Phiếu],
                    h.LoaiHoaDon AS [Loại HD],
                    h.NgayLap AS [Ngày Lập],
                    h.TongTien AS [Tổng Tiền (đ)],
                    h.DaThanhToan AS [Đã Thanh Toán (đ)],
                    h.PhuongThucThanhToan AS [Phương Thức],
                    h.TrangThai AS [Trạng Thái]
                FROM HoaDon h
                INNER JOIN KhachHang kh ON h.MaKhachHang = kh.MaKhachHang
                LEFT JOIN Phong ph ON h.MaPhong = ph.MaPhong
                LEFT JOIN PhieuDichVu pdv ON h.MaPhieuDichVu = pdv.MaPhieu
                ORDER BY h.NgayLap DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                dgvHoaDon.DataSource = dt;

                if (dgvHoaDon.Columns["MaHoaDon"] != null)
                    dgvHoaDon.Columns["MaHoaDon"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboPhieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPhieu.SelectedValue != null && cboPhieu.SelectedValue != DBNull.Value && cboPhieu.SelectedValue is int maPhieu)
            {
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT TongTien FROM PhieuDichVu WHERE MaPhieu = " + maPhieu);
                if (dt.Rows.Count > 0)
                {
                    decimal tt = Convert.ToDecimal(dt.Rows[0]["TongTien"]);
                    txtTongTien.Text = tt.ToString("N0");
                    txtDaThanhToan.Text = tt.ToString("N0");
                }
            }
        }

        private void dgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvHoaDon.SelectedRows[0];
                selectedMaHoaDon = Convert.ToInt32(row.Cells["MaHoaDon"].Value);
                txtMaCode.Text = row.Cells["Mã Hóa Đơn"].Value.ToString();
                cboLoaiHD.SelectedItem = row.Cells["Loại HD"].Value.ToString();
                txtTongTien.Text = Convert.ToDecimal(row.Cells["Tổng Tiền (đ)"].Value).ToString("N0");
                txtDaThanhToan.Text = Convert.ToDecimal(row.Cells["Đã Thanh Toán (đ)"].Value).ToString("N0");
                cboPhuongThuc.SelectedItem = row.Cells["Phương Thức"].Value.ToString();
                cboTrangThai.SelectedItem = row.Cells["Trạng Thái"].Value.ToString();
            }
        }

        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maNV = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNhanVien : 1;
            decimal tongTien = decimal.TryParse(txtTongTien.Text.Replace(",", ""), out decimal tt) ? tt : 0;
            decimal daThanhToan = decimal.TryParse(txtDaThanhToan.Text.Replace(",", ""), out decimal dtt) ? dtt : tongTien;

            try
            {
                string sql = @"
                INSERT INTO HoaDon (
                    MaCodeHoaDon, MaKhachHang, MaPhong, MaNhanVien, MaPhieuDichVu,
                    LoaiHoaDon, NgayLap, TongTien, DaThanhToan, PhuongThucThanhToan, TrangThai
                ) VALUES (
                    @MaCode, @MaKH, @MaPhong, @MaNV, @MaPhieu,
                    @LoaiHD, GETDATE(), @TongTien, @DaThanhToan, @PhuongThuc, @TrangThai
                )";

                SqlParameter[] p = {
                    new SqlParameter("@MaCode", string.IsNullOrWhiteSpace(txtMaCode.Text) ? "HD-" + DateTime.Now.Ticks : txtMaCode.Text.Trim()),
                    new SqlParameter("@MaKH", (int)cboKhachHang.SelectedValue),
                    new SqlParameter("@MaPhong", cboPhong.SelectedValue != DBNull.Value && cboPhong.SelectedValue != null ? (object)cboPhong.SelectedValue : DBNull.Value),
                    new SqlParameter("@MaNV", maNV),
                    new SqlParameter("@MaPhieu", cboPhieu.SelectedValue != DBNull.Value && cboPhieu.SelectedValue != null ? (object)cboPhieu.SelectedValue : DBNull.Value),
                    new SqlParameter("@LoaiHD", cboLoaiHD.SelectedItem?.ToString() ?? "Tổng hợp"),
                    new SqlParameter("@TongTien", tongTien),
                    new SqlParameter("@DaThanhToan", daThanhToan),
                    new SqlParameter("@PhuongThuc", cboPhuongThuc.SelectedItem?.ToString() ?? "Tiền mặt"),
                    new SqlParameter("@TrangThai", cboTrangThai.SelectedItem?.ToString() ?? "Đã thanh toán")
                };

                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Lập hóa đơn thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GenerateNewCode();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lập hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (selectedMaHoaDon == 0) return;

            try
            {
                string sql = "UPDATE HoaDon SET TrangThai = N'Đã thanh toán', DaThanhToan = TongTien, PhuongThucThanhToan = @PhuongThuc WHERE MaHoaDon = @MaHD";
                SqlParameter[] p = {
                    new SqlParameter("@PhuongThuc", cboPhuongThuc.SelectedItem?.ToString() ?? "Tiền mặt"),
                    new SqlParameter("@MaHD", selectedMaHoaDon)
                };

                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Xác nhận thanh toán hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyHD_Click(object sender, EventArgs e)
        {
            if (selectedMaHoaDon == 0) return;

            var confirm = MessageBox.Show("Bạn có chắc chắn muốn HỦY hóa đơn này?", "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                string sql = "UPDATE HoaDon SET TrangThai = N'Đã hủy' WHERE MaHoaDon = @MaHD";
                SqlParameter[] p = { new SqlParameter("@MaHD", selectedMaHoaDon) };
                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Đã hủy hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hủy hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
