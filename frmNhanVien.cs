using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace QuanLyDichVuKhachSan
{
    public partial class frmNhanVien : Form
    {
        private int selectedMaNhanVien = 0;

        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            FormatDataGridView();
            cboChucVu.SelectedIndex = 1; // Lễ tân
            cboCaTruc.SelectedIndex = 0; // Ca Sáng
            cboTrangThai.SelectedIndex = 0; // Hoạt động
            LoadData();
        }

        private void FormatDataGridView()
        {
            dgvNhanVien.EnableHeadersVisualStyles = false;
            dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 23, 42);
            dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(52, 211, 153);
            dgvNhanVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvNhanVien.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            dgvNhanVien.DefaultCellStyle.ForeColor = Color.White;
            dgvNhanVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(79, 70, 229);
            dgvNhanVien.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvNhanVien.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvNhanVien.GridColor = Color.FromArgb(51, 65, 85);
        }

        private void LoadData()
        {
            try
            {
                string sql = "SELECT MaNhanVien, HoTen AS [Họ Và Tên], CCCD AS [CCCD], SoDienThoai AS [SĐT], ChucVu AS [Chức Vụ], TenDangNhap AS [Tên Đăng Nhập], CaTruc AS [Ca Trực], TrangThai AS [Trạng Thái] FROM NhanVien ORDER BY ChucVu, HoTen";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                dgvNhanVien.DataSource = dt;

                if (dgvNhanVien.Columns["MaNhanVien"] != null)
                    dgvNhanVien.Columns["MaNhanVien"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvNhanVien.SelectedRows[0];
                selectedMaNhanVien = Convert.ToInt32(row.Cells["MaNhanVien"].Value);
                txtHoTen.Text = row.Cells["Họ Và Tên"].Value.ToString();
                txtCCCD.Text = row.Cells["CCCD"].Value.ToString();
                txtSDT.Text = row.Cells["SĐT"].Value.ToString();
                cboChucVu.SelectedItem = row.Cells["Chức Vụ"].Value.ToString();
                txtTenDN.Text = row.Cells["Tên Đăng Nhập"].Value.ToString();
                cboCaTruc.SelectedItem = row.Cells["Ca Trực"].Value.ToString();
                cboTrangThai.SelectedItem = row.Cells["Trạng Thái"].Value.ToString();
                txtMatKhau.Text = ""; // Keep password field clean
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtTenDN.Text) || string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ tên, Tên đăng nhập và Mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = "INSERT INTO NhanVien (HoTen, CCCD, SoDienThoai, ChucVu, TenDangNhap, MatKhau, CaTruc, TrangThai) VALUES (@HoTen, @CCCD, @SDT, @ChucVu, @TenDN, @MatKhau, @CaTruc, @TrangThai)";
                SqlParameter[] p = {
                    new SqlParameter("@HoTen", txtHoTen.Text.Trim()),
                    new SqlParameter("@CCCD", txtCCCD.Text.Trim()),
                    new SqlParameter("@SDT", txtSDT.Text.Trim()),
                    new SqlParameter("@ChucVu", cboChucVu.SelectedItem?.ToString() ?? "Lễ tân"),
                    new SqlParameter("@TenDN", txtTenDN.Text.Trim()),
                    new SqlParameter("@MatKhau", txtMatKhau.Text.Trim()),
                    new SqlParameter("@CaTruc", cboCaTruc.SelectedItem?.ToString() ?? "Ca Sáng"),
                    new SqlParameter("@TrangThai", cboTrangThai.SelectedItem?.ToString() ?? "Hoạt động")
                };

                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Thêm nhân viên mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaNhanVien == 0) return;

            try
            {
                string sql = "UPDATE NhanVien SET HoTen = @HoTen, CCCD = @CCCD, SoDienThoai = @SDT, ChucVu = @ChucVu, TenDangNhap = @TenDN, CaTruc = @CaTruc, TrangThai = @TrangThai";
                if (!string.IsNullOrWhiteSpace(txtMatKhau.Text))
                {
                    sql += ", MatKhau = @MatKhau";
                }
                sql += " WHERE MaNhanVien = @MaNV";

                var pList = new System.Collections.Generic.List<SqlParameter> {
                    new SqlParameter("@HoTen", txtHoTen.Text.Trim()),
                    new SqlParameter("@CCCD", txtCCCD.Text.Trim()),
                    new SqlParameter("@SDT", txtSDT.Text.Trim()),
                    new SqlParameter("@ChucVu", cboChucVu.SelectedItem?.ToString() ?? "Lễ tân"),
                    new SqlParameter("@TenDN", txtTenDN.Text.Trim()),
                    new SqlParameter("@CaTruc", cboCaTruc.SelectedItem?.ToString() ?? "Ca Sáng"),
                    new SqlParameter("@TrangThai", cboTrangThai.SelectedItem?.ToString() ?? "Hoạt động"),
                    new SqlParameter("@MaNV", selectedMaNhanVien)
                };

                if (!string.IsNullOrWhiteSpace(txtMatKhau.Text))
                {
                    pList.Add(new SqlParameter("@MatKhau", txtMatKhau.Text.Trim()));
                }

                DatabaseHelper.ExecuteNonQuery(sql, pList.ToArray());
                MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaNhanVien == 0) return;

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn chuyển tài khoản '{txtHoTen.Text}' sang trạng thái NGHỈ VIỆC?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                string sql = "UPDATE NhanVien SET TrangThai = N'Nghỉ việc' WHERE MaNhanVien = @MaNV";
                SqlParameter[] p = { new SqlParameter("@MaNV", selectedMaNhanVien) };
                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Đã chuyển tài khoản sang trạng thái Nghỉ việc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật trạng thái nghỉ việc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
