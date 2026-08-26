using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace QuanLyDichVuKhachSan
{
    public partial class frmPhieuDichVu : Form
    {
        private int selectedMaPhieu = 0;
        private int preselectedMaPhong = 0;

        public frmPhieuDichVu(int maPhong = 0)
        {
            InitializeComponent();
            preselectedMaPhong = maPhong;
        }

        private void frmPhieuDichVu_Load(object sender, EventArgs e)
        {
            FormatDataGridView();
            LoadComboboxData();
            GenerateNewCode();
            LoadData();

            if (preselectedMaPhong > 0)
            {
                SelectPhongInCombo(preselectedMaPhong);
            }
        }

        private void FormatDataGridView()
        {
            dgvPhieu.EnableHeadersVisualStyles = false;
            dgvPhieu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 23, 42);
            dgvPhieu.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(52, 211, 153);
            dgvPhieu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvPhieu.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            dgvPhieu.DefaultCellStyle.ForeColor = Color.White;
            dgvPhieu.DefaultCellStyle.SelectionBackColor = Color.FromArgb(79, 70, 229);
            dgvPhieu.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvPhieu.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvPhieu.GridColor = Color.FromArgb(51, 65, 85);
        }

        private void GenerateNewCode()
        {
            txtMaCode.Text = "PDV-" + DateTime.Now.ToString("yyMMdd-HHmmss");
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

                // DichVu
                DataTable dtDV = DatabaseHelper.ExecuteQuery("SELECT MaDichVu, TenDichVu + ' [' + NhomDichVu + ' - ' + CAST(CAST(DonGia AS INT) AS VARCHAR) + 'đ]' AS DisplayText, DonGia FROM DanhMucDichVu WHERE IsActive = 1");
                cboDichVu.DataSource = dtDV;
                cboDichVu.DisplayMember = "DisplayText";
                cboDichVu.ValueMember = "MaDichVu";

                // DoiTac
                DataTable dtDT = DatabaseHelper.ExecuteQuery("SELECT MaDoiTac, TenDoiTac + ' (' + CAST(CAST(TyLeAnChiaKhachSan AS INT) AS VARCHAR) + '% ăn chia)' AS DisplayText FROM DoiTacGiatUi");
                DataRow emptyDT = dtDT.NewRow();
                emptyDT["MaDoiTac"] = DBNull.Value;
                emptyDT["DisplayText"] = "-- Không qua đối tác --";
                dtDT.Rows.InsertAt(emptyDT, 0);
                cboDoiTac.DataSource = dtDT;
                cboDoiTac.DisplayMember = "DisplayText";
                cboDoiTac.ValueMember = "MaDoiTac";

                cboHinhThuc.SelectedIndex = 0;
                cboTrangThai.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục combobox: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectPhongInCombo(int maPhong)
        {
            cboPhong.SelectedValue = maPhong;
        }

        private void LoadData()
        {
            try
            {
                string sql = @"
                SELECT 
                    p.MaPhieu,
                    p.MaCodePhieu AS [Mã Phiếu],
                    kh.HoTen AS [Khách Hàng],
                    ISNULL(ph.SoPhong, N'Không') AS [Phòng],
                    dv.TenDichVu AS [Dịch Vụ],
                    ISNULL(dt.TenDoiTac, N'Không') AS [Đối Tác],
                    p.SoLuong_Kg_Ngay AS [Số Lượng/Kg],
                    p.DonGia AS [Đơn Giá (đ)],
                    p.ThuThemPhatSinh AS [Phát Sinh (đ)],
                    p.TongTien AS [Tổng Tiền (đ)],
                    ISNULL(p.TienKhachSanNhan, 0) AS [Tiền KS Nhận (đ)],
                    ISNULL(p.TienDoiTacNhan, 0) AS [Tiền ĐT Nhận (đ)],
                    p.HinhThucTinh AS [Hình Thức],
                    p.TrangThai AS [Trạng Thái]
                FROM PhieuDichVu p
                INNER JOIN KhachHang kh ON p.MaKhachHang = kh.MaKhachHang
                INNER JOIN DanhMucDichVu dv ON p.MaDichVu = dv.MaDichVu
                LEFT JOIN Phong ph ON p.MaPhong = ph.MaPhong
                LEFT JOIN DoiTacGiatUi dt ON p.MaDoiTac = dt.MaDoiTac
                ORDER BY p.NgayBatDau DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                dgvPhieu.DataSource = dt;

                if (dgvPhieu.Columns["MaPhieu"] != null)
                    dgvPhieu.Columns["MaPhieu"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phiếu dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDichVu.SelectedItem != null && cboDichVu.SelectedValue is int maDV)
            {
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT DonGia, NhomDichVu FROM DanhMucDichVu WHERE MaDichVu = " + maDV);
                if (dt.Rows.Count > 0)
                {
                    txtDonGia.Text = Convert.ToDecimal(dt.Rows[0]["DonGia"]).ToString("N0");
                    string nhom = dt.Rows[0]["NhomDichVu"].ToString();
                    if (nhom == "Giặt ủi")
                    {
                        cboHinhThuc.SelectedItem = "Hóa đơn riêng";
                    }
                }
            }
        }

        private void dgvPhieu_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhieu.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvPhieu.SelectedRows[0];
                selectedMaPhieu = Convert.ToInt32(row.Cells["MaPhieu"].Value);
                txtMaCode.Text = row.Cells["Mã Phiếu"].Value.ToString();
                txtSoLuong.Text = Convert.ToDecimal(row.Cells["Số Lượng/Kg"].Value).ToString("G");
                txtDonGia.Text = Convert.ToDecimal(row.Cells["Đơn Giá (đ)"].Value).ToString("N0");
                txtThuThem.Text = Convert.ToDecimal(row.Cells["Phát Sinh (đ)"].Value).ToString("N0");
                cboHinhThuc.SelectedItem = row.Cells["Hình Thức"].Value.ToString();
                cboTrangThai.SelectedItem = row.Cells["Trạng Thái"].Value.ToString();
            }
        }

        private void btnTaoPhieu_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedValue == null || cboDichVu.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng và Dịch vụ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maNV = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNhanVien : 1;

            try
            {
                string sql = @"
                INSERT INTO PhieuDichVu (
                    MaCodePhieu, MaDichVu, MaKhachHang, MaPhong, MaNhanVien, MaDoiTac,
                    NgayBatDau, SoLuong_Kg_Ngay, DonGia, TienCoc, ThuThemPhatSinh,
                    GhiChuChiTiet, HinhThucTinh, TrangThai
                ) VALUES (
                    @MaCode, @MaDV, @MaKH, @MaPhong, @MaNV, @MaDoiTac,
                    GETDATE(), @SoLuong, @DonGia, @TienCoc, @ThuThem,
                    @GhiChu, @HinhThuc, @TrangThai
                )";

                SqlParameter[] p = {
                    new SqlParameter("@MaCode", string.IsNullOrWhiteSpace(txtMaCode.Text) ? "PDV-" + DateTime.Now.Ticks : txtMaCode.Text.Trim()),
                    new SqlParameter("@MaDV", (int)cboDichVu.SelectedValue),
                    new SqlParameter("@MaKH", (int)cboKhachHang.SelectedValue),
                    new SqlParameter("@MaPhong", cboPhong.SelectedValue != DBNull.Value && cboPhong.SelectedValue != null ? (object)cboPhong.SelectedValue : DBNull.Value),
                    new SqlParameter("@MaNV", maNV),
                    new SqlParameter("@MaDoiTac", cboDoiTac.SelectedValue != DBNull.Value && cboDoiTac.SelectedValue != null ? (object)cboDoiTac.SelectedValue : DBNull.Value),
                    new SqlParameter("@SoLuong", decimal.TryParse(txtSoLuong.Text, out decimal sl) ? sl : 1),
                    new SqlParameter("@DonGia", decimal.TryParse(txtDonGia.Text.Replace(",", ""), out decimal dg) ? dg : 0),
                    new SqlParameter("@TienCoc", decimal.TryParse(txtTienCoc.Text.Replace(",", ""), out decimal coc) ? coc : 0),
                    new SqlParameter("@ThuThem", decimal.TryParse(txtThuThem.Text.Replace(",", ""), out decimal tt) ? tt : 0),
                    new SqlParameter("@GhiChu", txtGhiChu.Text.Trim()),
                    new SqlParameter("@HinhThuc", cboHinhThuc.SelectedItem?.ToString() ?? "Hóa đơn riêng"),
                    new SqlParameter("@TrangThai", cboTrangThai.SelectedItem?.ToString() ?? "Đã hoàn tất")
                };

                // Executing insert triggers INSTEAD OF INSERT trigger in SQL Server automatically!
                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Tạo phiếu dịch vụ thành công! Hệ thống đã tự động tính tổng tiền, khấu trừ tồn kho & phân chia doanh thu đối tác nếu có.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GenerateNewCode();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo phiếu dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHoanTat_Click(object sender, EventArgs e)
        {
            if (selectedMaPhieu == 0) return;

            try
            {
                string sql = "UPDATE PhieuDichVu SET TrangThai = @TrangThai, GhiChuChiTiet = @GhiChu WHERE MaPhieu = @MaPhieu";
                SqlParameter[] p = {
                    new SqlParameter("@TrangThai", cboTrangThai.SelectedItem?.ToString() ?? "Đã hoàn tất"),
                    new SqlParameter("@GhiChu", txtGhiChu.Text.Trim()),
                    new SqlParameter("@MaPhieu", selectedMaPhieu)
                };

                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Cập nhật trạng thái phiếu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật phiếu dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyPhieu_Click(object sender, EventArgs e)
        {
            if (selectedMaPhieu == 0) return;

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn HỦY phiếu dịch vụ này?", "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                string sql = "UPDATE PhieuDichVu SET TrangThai = N'Đã hủy' WHERE MaPhieu = @MaPhieu";
                SqlParameter[] p = { new SqlParameter("@MaPhieu", selectedMaPhieu) };
                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Đã hủy phiếu dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hủy phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
