using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace QuanLyDichVuKhachSan
{
    public partial class frmDoiTac : Form
    {
        private int selectedMaDoiTac = 0;

        public frmDoiTac()
        {
            InitializeComponent();
        }

        private void frmDoiTac_Load(object sender, EventArgs e)
        {
            FormatDataGridView();
            LoadData();
        }

        private void FormatDataGridView()
        {
            dgvDoiTac.EnableHeadersVisualStyles = false;
            dgvDoiTac.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 23, 42);
            dgvDoiTac.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(52, 211, 153);
            dgvDoiTac.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvDoiTac.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            dgvDoiTac.DefaultCellStyle.ForeColor = Color.White;
            dgvDoiTac.DefaultCellStyle.SelectionBackColor = Color.FromArgb(79, 70, 229);
            dgvDoiTac.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvDoiTac.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvDoiTac.GridColor = Color.FromArgb(51, 65, 85);
        }

        private void LoadData()
        {
            try
            {
                string sql = "SELECT MaDoiTac, TenDoiTac AS [Tên Đối Tác], DiaChi AS [Địa Chỉ], SoDienThoai AS [Số Điện Thoại], TyLeAnChiaKhachSan AS [Tỷ Lệ Ăn Chia Khách Sạn (%)] FROM DoiTacGiatUi ORDER BY TenDoiTac";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                dgvDoiTac.DataSource = dt;

                if (dgvDoiTac.Columns["MaDoiTac"] != null)
                    dgvDoiTac.Columns["MaDoiTac"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách đối tác giặt ủi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDoiTac_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDoiTac.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvDoiTac.SelectedRows[0];
                selectedMaDoiTac = Convert.ToInt32(row.Cells["MaDoiTac"].Value);
                txtTenDT.Text = row.Cells["Tên Đối Tác"].Value.ToString();
                txtDiaChi.Text = row.Cells["Địa Chỉ"].Value?.ToString() ?? "";
                txtSDT.Text = row.Cells["Số Điện Thoại"].Value?.ToString() ?? "";
                txtTyLe.Text = Convert.ToDecimal(row.Cells["Tỷ Lệ Ăn Chia Khách Sạn (%)"].Value).ToString("F2");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDT.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên đối tác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = "INSERT INTO DoiTacGiatUi (TenDoiTac, DiaChi, SoDienThoai, TyLeAnChiaKhachSan) VALUES (@Ten, @DiaChi, @SDT, @TyLe)";
                SqlParameter[] p = {
                    new SqlParameter("@Ten", txtTenDT.Text.Trim()),
                    new SqlParameter("@DiaChi", string.IsNullOrWhiteSpace(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text.Trim()),
                    new SqlParameter("@SDT", string.IsNullOrWhiteSpace(txtSDT.Text) ? (object)DBNull.Value : txtSDT.Text.Trim()),
                    new SqlParameter("@TyLe", decimal.TryParse(txtTyLe.Text, out decimal tl) ? tl : 20.00m)
                };

                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Thêm đối tác giặt ủi mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm đối tác: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaDoiTac == 0) return;

            try
            {
                string sql = "UPDATE DoiTacGiatUi SET TenDoiTac = @Ten, DiaChi = @DiaChi, SoDienThoai = @SDT, TyLeAnChiaKhachSan = @TyLe WHERE MaDoiTac = @MaDT";
                SqlParameter[] p = {
                    new SqlParameter("@Ten", txtTenDT.Text.Trim()),
                    new SqlParameter("@DiaChi", string.IsNullOrWhiteSpace(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text.Trim()),
                    new SqlParameter("@SDT", string.IsNullOrWhiteSpace(txtSDT.Text) ? (object)DBNull.Value : txtSDT.Text.Trim()),
                    new SqlParameter("@TyLe", decimal.TryParse(txtTyLe.Text, out decimal tl) ? tl : 20.00m),
                    new SqlParameter("@MaDT", selectedMaDoiTac)
                };

                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Cập nhật thông tin đối tác thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật đối tác: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaDoiTac == 0) return;

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa đối tác '{txtTenDT.Text}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                string sql = "DELETE FROM DoiTacGiatUi WHERE MaDoiTac = @MaDT";
                SqlParameter[] p = { new SqlParameter("@MaDT", selectedMaDoiTac) };
                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Xóa đối tác thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa đối tác (đối tác đang được tham chiếu trong các phiếu dịch vụ): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
