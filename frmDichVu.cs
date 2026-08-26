using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace QuanLyDichVuKhachSan
{
    public partial class frmDichVu : Form
    {
        private int selectedMaDichVu = 0;

        public frmDichVu()
        {
            InitializeComponent();
        }

        private void frmDichVu_Load(object sender, EventArgs e)
        {
            cboFilterNhom.SelectedIndex = 0;
            FormatDataGridView();
            LoadData();
        }

        private void FormatDataGridView()
        {
            dgvDichVu.EnableHeadersVisualStyles = false;
            dgvDichVu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 23, 42);
            dgvDichVu.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(52, 211, 153);
            dgvDichVu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvDichVu.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            dgvDichVu.DefaultCellStyle.ForeColor = Color.White;
            dgvDichVu.DefaultCellStyle.SelectionBackColor = Color.FromArgb(79, 70, 229);
            dgvDichVu.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvDichVu.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvDichVu.GridColor = Color.FromArgb(51, 65, 85);
        }

        private void LoadData()
        {
            try
            {
                string sql = "SELECT MaDichVu, TenDichVu AS [Tên Dịch Vụ], NhomDichVu AS [Nhóm Dịch Vụ], PhanLoaiDichVu AS [Phân Loại], DonGia AS [Đơn Giá (đ)], SoLuongTon AS [Tồn Kho], SuatAnTheoNgay AS [Suất/Ngày], ThongTinChiTiet AS [Chi Tiết], TrangThai AS [Trạng Thái], IsActive AS [Hoạt Động] FROM DanhMucDichVu WHERE 1=1";

                string selectedNhom = cboFilterNhom.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedNhom) && selectedNhom != "Tất cả")
                {
                    sql += " AND NhomDichVu = N'" + selectedNhom + "'";
                }

                string kw = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(kw))
                {
                    sql += " AND (TenDichVu LIKE N'%" + kw + "%' OR ThongTinChiTiet LIKE N'%" + kw + "%')";
                }

                sql += " ORDER BY NhomDichVu, TenDichVu";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                dgvDichVu.DataSource = dt;

                if (dgvDichVu.Columns["MaDichVu"] != null)
                    dgvDichVu.Columns["MaDichVu"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboFilterNhom_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvDichVu_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDichVu.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvDichVu.SelectedRows[0];
                selectedMaDichVu = Convert.ToInt32(row.Cells["MaDichVu"].Value);
                txtTenDV.Text = row.Cells["Tên Dịch Vụ"].Value.ToString();
                cboNhomDV.SelectedItem = row.Cells["Nhóm Dịch Vụ"].Value.ToString();
                cboPhanLoai.SelectedItem = row.Cells["Phân Loại"].Value.ToString();
                txtDonGia.Text = Convert.ToDecimal(row.Cells["Đơn Giá (đ)"].Value).ToString("N0");
                txtSoLuongTon.Text = row.Cells["Tồn Kho"].Value.ToString();
                txtSuatAn.Text = row.Cells["Suất/Ngày"].Value.ToString();
                txtChiTiet.Text = row.Cells["Chi Tiết"].Value?.ToString() ?? "";
                cboTrangThai.SelectedItem = row.Cells["Trạng Thái"].Value.ToString();
                chkIsActive.Checked = Convert.ToBoolean(row.Cells["Hoạt Động"].Value);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDV.Text) || cboNhomDV.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập Tên dịch vụ và Nhóm dịch vụ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = "INSERT INTO DanhMucDichVu (TenDichVu, NhomDichVu, PhanLoaiDichVu, DonGia, SoLuongTon, SuatAnTheoNgay, ThongTinChiTiet, TrangThai, IsActive) VALUES (@TenDV, @NhomDV, @PhanLoai, @DonGia, @SoLuongTon, @SuatAn, @ChiTiet, @TrangThai, @IsActive)";
                SqlParameter[] p = {
                    new SqlParameter("@TenDV", txtTenDV.Text.Trim()),
                    new SqlParameter("@NhomDV", cboNhomDV.SelectedItem.ToString()),
                    new SqlParameter("@PhanLoai", cboPhanLoai.SelectedItem?.ToString() ?? "Tự túc"),
                    new SqlParameter("@DonGia", decimal.TryParse(txtDonGia.Text.Replace(",", ""), out decimal dg) ? dg : 0),
                    new SqlParameter("@SoLuongTon", int.TryParse(txtSoLuongTon.Text, out int ton) ? ton : 0),
                    new SqlParameter("@SuatAn", int.TryParse(txtSuatAn.Text, out int suat) ? suat : 0),
                    new SqlParameter("@ChiTiet", txtChiTiet.Text.Trim()),
                    new SqlParameter("@TrangThai", cboTrangThai.SelectedItem?.ToString() ?? "Sẵn sàng"),
                    new SqlParameter("@IsActive", chkIsActive.Checked)
                };

                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Thêm dịch vụ mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaDichVu == 0) return;

            try
            {
                string sql = "UPDATE DanhMucDichVu SET TenDichVu = @TenDV, NhomDichVu = @NhomDV, PhanLoaiDichVu = @PhanLoai, DonGia = @DonGia, SoLuongTon = @SoLuongTon, SuatAnTheoNgay = @SuatAn, ThongTinChiTiet = @ChiTiet, TrangThai = @TrangThai, IsActive = @IsActive WHERE MaDichVu = @MaDV";
                SqlParameter[] p = {
                    new SqlParameter("@TenDV", txtTenDV.Text.Trim()),
                    new SqlParameter("@NhomDV", cboNhomDV.SelectedItem.ToString()),
                    new SqlParameter("@PhanLoai", cboPhanLoai.SelectedItem?.ToString() ?? "Tự túc"),
                    new SqlParameter("@DonGia", decimal.TryParse(txtDonGia.Text.Replace(",", ""), out decimal dg) ? dg : 0),
                    new SqlParameter("@SoLuongTon", int.TryParse(txtSoLuongTon.Text, out int ton) ? ton : 0),
                    new SqlParameter("@SuatAn", int.TryParse(txtSuatAn.Text, out int suat) ? suat : 0),
                    new SqlParameter("@ChiTiet", txtChiTiet.Text.Trim()),
                    new SqlParameter("@TrangThai", cboTrangThai.SelectedItem?.ToString() ?? "Sẵn sàng"),
                    new SqlParameter("@IsActive", chkIsActive.Checked),
                    new SqlParameter("@MaDV", selectedMaDichVu)
                };

                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Cập nhật dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaDichVu == 0) return;

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa dịch vụ '{txtTenDV.Text}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                string sql = "DELETE FROM DanhMucDichVu WHERE MaDichVu = @MaDV";
                SqlParameter[] p = { new SqlParameter("@MaDV", selectedMaDichVu) };
                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Xóa dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa dịch vụ (có thể dịch vụ đã được sử dụng trong các phiếu dịch vụ): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
