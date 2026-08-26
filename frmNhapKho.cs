using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace QuanLyDichVuKhachSan
{
    public partial class frmNhapKho : Form
    {
        public frmNhapKho()
        {
            InitializeComponent();
        }

        private void frmNhapKho_Load(object sender, EventArgs e)
        {
            FormatDataGridView();
            LoadComboboxData();
            LoadData();
        }

        private void FormatDataGridView()
        {
            dgvNhapKho.EnableHeadersVisualStyles = false;
            dgvNhapKho.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 23, 42);
            dgvNhapKho.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(52, 211, 153);
            dgvNhapKho.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvNhapKho.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            dgvNhapKho.DefaultCellStyle.ForeColor = Color.White;
            dgvNhapKho.DefaultCellStyle.SelectionBackColor = Color.FromArgb(79, 70, 229);
            dgvNhapKho.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvNhapKho.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvNhapKho.GridColor = Color.FromArgb(51, 65, 85);
        }

        private void LoadComboboxData()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery("SELECT MaDichVu, TenDichVu + ' (Tồn hiện tại: ' + CAST(SoLuongTon AS VARCHAR) + ')' AS DisplayText FROM DanhMucDichVu WHERE NhomDichVu = N'Thực phẩm khô' AND IsActive = 1");
                cboDichVu.DataSource = dt;
                cboDichVu.DisplayMember = "DisplayText";
                cboDichVu.ValueMember = "MaDichVu";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục thực phẩm khô: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                string sql = @"
                SELECT 
                    nk.MaNhapKho,
                    dv.TenDichVu AS [Tên Sản Phẩm Đồ Khô],
                    nv.HoTen AS [Admin Nhập Kho],
                    nk.NgayNhap AS [Ngày Nhập],
                    nk.SoLuongNhap AS [Số Lượng Nhập],
                    nk.DonGiaNhap AS [Đơn Giá Nhập (đ)],
                    nk.TongTien AS [Tổng Tiền (đ)],
                    nk.GhiChu AS [Ghi Chú]
                FROM LichSuNhapKho nk
                INNER JOIN DanhMucDichVu dv ON nk.MaDichVu = dv.MaDichVu
                INNER JOIN NhanVien nv ON nk.MaNhanVienAdmin = nv.MaNhanVien
                ORDER BY nk.NgayNhap DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                dgvNhapKho.DataSource = dt;

                if (dgvNhapKho.Columns["MaNhapKho"] != null)
                    dgvNhapKho.Columns["MaNhapKho"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử nhập kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            if (cboDichVu.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm thực phẩm khô để nhập kho!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtSoLuong.Text, out int sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng nhập phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int adminId = SessionManager.CurrentUser != null ? SessionManager.CurrentUser.MaNhanVien : 1;

            try
            {
                string sql = @"INSERT INTO LichSuNhapKho (MaDichVu, MaNhanVienAdmin, NgayNhap, SoLuongNhap, DonGiaNhap, GhiChu) 
                               VALUES (@MaDV, @AdminId, GETDATE(), @SoLuong, @DonGia, @GhiChu)";

                SqlParameter[] p = {
                    new SqlParameter("@MaDV", (int)cboDichVu.SelectedValue),
                    new SqlParameter("@AdminId", adminId),
                    new SqlParameter("@SoLuong", sl),
                    new SqlParameter("@DonGia", decimal.TryParse(txtDonGia.Text.Replace(",", ""), out decimal dg) ? dg : 0),
                    new SqlParameter("@GhiChu", txtGhiChu.Text.Trim())
                };

                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show($"Nhập kho thành công {sl} đơn vị sản phẩm! Trigger SQL Server đã tự động cập nhật số lượng tồn kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadComboboxData();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực hiện nhập kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
