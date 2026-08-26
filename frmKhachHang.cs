using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace QuanLyDichVuKhachSan
{
    public partial class frmKhachHang : Form
    {
        private int selectedMaKhachHang = 0;

        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            FormatDataGridView();
            LoadData();
        }

        private void FormatDataGridView()
        {
            dgvKhachHang.EnableHeadersVisualStyles = false;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 23, 42);
            dgvKhachHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(52, 211, 153);
            dgvKhachHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvKhachHang.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            dgvKhachHang.DefaultCellStyle.ForeColor = Color.White;
            dgvKhachHang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(79, 70, 229);
            dgvKhachHang.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvKhachHang.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvKhachHang.GridColor = Color.FromArgb(51, 65, 85);
        }

        private void LoadData()
        {
            try
            {
                string sql = "SELECT MaKhachHang, HoTen AS [Họ Và Tên], CCCD AS [Số CCCD], SoDienThoai AS [Số Điện Thoại], DiaChi AS [Địa Chỉ] FROM KhachHang WHERE 1=1";

                string kw = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(kw))
                {
                    sql += " AND (HoTen LIKE N'%" + kw + "%' OR CCCD LIKE '%" + kw + "%' OR SoDienThoai LIKE '%" + kw + "%')";
                }

                sql += " ORDER BY HoTen";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                dgvKhachHang.DataSource = dt;

                if (dgvKhachHang.Columns["MaKhachHang"] != null)
                    dgvKhachHang.Columns["MaKhachHang"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKhachHang.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvKhachHang.SelectedRows[0];
                selectedMaKhachHang = Convert.ToInt32(row.Cells["MaKhachHang"].Value);
                txtHoTen.Text = row.Cells["Họ Và Tên"].Value.ToString();
                txtCCCD.Text = row.Cells["Số CCCD"].Value?.ToString() ?? "";
                txtSDT.Text = row.Cells["Số Điện Thoại"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["Địa Chỉ"].Value?.ToString() ?? "";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ tên khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = "INSERT INTO KhachHang (HoTen, CCCD, SoDienThoai, DiaChi) VALUES (@HoTen, @CCCD, @SDT, @DiaChi)";
                SqlParameter[] p = {
                    new SqlParameter("@HoTen", txtHoTen.Text.Trim()),
                    new SqlParameter("@CCCD", string.IsNullOrWhiteSpace(txtCCCD.Text) ? (object)DBNull.Value : txtCCCD.Text.Trim()),
                    new SqlParameter("@SDT", string.IsNullOrWhiteSpace(txtSDT.Text) ? (object)DBNull.Value : txtSDT.Text.Trim()),
                    new SqlParameter("@DiaChi", string.IsNullOrWhiteSpace(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text.Trim())
                };

                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaKhachHang == 0) return;

            try
            {
                string sql = "UPDATE KhachHang SET HoTen = @HoTen, CCCD = @CCCD, SoDienThoai = @SDT, DiaChi = @DiaChi WHERE MaKhachHang = @MaKH";
                SqlParameter[] p = {
                    new SqlParameter("@HoTen", txtHoTen.Text.Trim()),
                    new SqlParameter("@CCCD", string.IsNullOrWhiteSpace(txtCCCD.Text) ? (object)DBNull.Value : txtCCCD.Text.Trim()),
                    new SqlParameter("@SDT", string.IsNullOrWhiteSpace(txtSDT.Text) ? (object)DBNull.Value : txtSDT.Text.Trim()),
                    new SqlParameter("@DiaChi", string.IsNullOrWhiteSpace(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text.Trim()),
                    new SqlParameter("@MaKH", selectedMaKhachHang)
                };

                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaKhachHang == 0) return;

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng '{txtHoTen.Text}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                string sql = "DELETE FROM KhachHang WHERE MaKhachHang = @MaKH";
                SqlParameter[] p = { new SqlParameter("@MaKH", selectedMaKhachHang) };
                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa khách hàng (khách hàng có liên kết dữ liệu trong các phiếu dịch vụ/hóa đơn): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
