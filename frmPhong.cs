using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace QuanLyDichVuKhachSan
{
    public partial class frmPhong : Form
    {
        private int selectedMaPhong = 0;

        public frmPhong()
        {
            InitializeComponent();
        }

        private void frmPhong_Load(object sender, EventArgs e)
        {
            FormatDataGridView();
            LoadData();
        }

        private void FormatDataGridView()
        {
            dgvPhong.EnableHeadersVisualStyles = false;
            dgvPhong.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 23, 42);
            dgvPhong.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(52, 211, 153);
            dgvPhong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvPhong.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            dgvPhong.DefaultCellStyle.ForeColor = Color.White;
            dgvPhong.DefaultCellStyle.SelectionBackColor = Color.FromArgb(79, 70, 229);
            dgvPhong.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvPhong.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvPhong.GridColor = Color.FromArgb(51, 65, 85);
        }

        private void LoadData()
        {
            try
            {
                string sql = "SELECT MaPhong, SoPhong AS [Số Phòng], LoaiPhong AS [Loại Phòng], GiaPhongTheoGio AS [Giá Giờ (đ)], GiaPhongTheoNgay AS [Giá Ngày (đ)], TrangThai AS [Trạng Thái] FROM Phong ORDER BY SoPhong";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                dgvPhong.DataSource = dt;

                if (dgvPhong.Columns["MaPhong"] != null)
                    dgvPhong.Columns["MaPhong"].Visible = false;

                UpdateSummaryStats(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSummaryStats(DataTable dt)
        {
            int total = dt.Rows.Count;
            int occupied = 0;
            int available = 0;
            int cleaning = 0;

            foreach (DataRow r in dt.Rows)
            {
                string st = r["Trạng Thái"].ToString();
                if (st == "Đang sử dụng") occupied++;
                else if (st == "Trống") available++;
                else cleaning++;
            }

            lblStatTotal.Text = $"🏨 Tổng số phòng: {total}";
            lblStatOccupied.Text = $"🔵 Đang sử dụng: {occupied}";
            lblStatAvailable.Text = $"🟢 Phòng trống: {available}";
            lblStatCleaning.Text = $"🟠 Dọn dẹp/Bảo trì: {cleaning}";
        }

        private void dgvPhong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhong.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvPhong.SelectedRows[0];
                selectedMaPhong = Convert.ToInt32(row.Cells["MaPhong"].Value);
                txtSoPhong.Text = row.Cells["Số Phòng"].Value.ToString();
                txtLoaiPhong.Text = row.Cells["Loại Phòng"].Value.ToString();
                txtGiaGio.Text = Convert.ToDecimal(row.Cells["Giá Giờ (đ)"].Value).ToString("N0");
                txtGiaNgay.Text = Convert.ToDecimal(row.Cells["Giá Ngày (đ)"].Value).ToString("N0");
                cboTrangThai.SelectedItem = row.Cells["Trạng Thái"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoPhong.Text) || string.IsNullOrWhiteSpace(txtLoaiPhong.Text))
            {
                MessageBox.Show("Vui lòng nhập Số phòng và Loại phòng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = "INSERT INTO Phong (SoPhong, LoaiPhong, GiaPhongTheoGio, GiaPhongTheoNgay, TrangThai) VALUES (@SoPhong, @LoaiPhong, @GiaGio, @GiaNgay, @TrangThai)";
                SqlParameter[] p = {
                    new SqlParameter("@SoPhong", txtSoPhong.Text.Trim()),
                    new SqlParameter("@LoaiPhong", txtLoaiPhong.Text.Trim()),
                    new SqlParameter("@GiaGio", decimal.TryParse(txtGiaGio.Text.Replace(",", ""), out decimal gGio) ? gGio : 0),
                    new SqlParameter("@GiaNgay", decimal.TryParse(txtGiaNgay.Text.Replace(",", ""), out decimal gNgay) ? gNgay : 0),
                    new SqlParameter("@TrangThai", cboTrangThai.SelectedItem?.ToString() ?? "Trống")
                };

                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Thêm phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaPhong == 0) return;

            try
            {
                string sql = "UPDATE Phong SET SoPhong = @SoPhong, LoaiPhong = @LoaiPhong, GiaPhongTheoGio = @GiaGio, GiaPhongTheoNgay = @GiaNgay, TrangThai = @TrangThai WHERE MaPhong = @MaPhong";
                SqlParameter[] p = {
                    new SqlParameter("@SoPhong", txtSoPhong.Text.Trim()),
                    new SqlParameter("@LoaiPhong", txtLoaiPhong.Text.Trim()),
                    new SqlParameter("@GiaGio", decimal.TryParse(txtGiaGio.Text.Replace(",", ""), out decimal gGio) ? gGio : 0),
                    new SqlParameter("@GiaNgay", decimal.TryParse(txtGiaNgay.Text.Replace(",", ""), out decimal gNgay) ? gNgay : 0),
                    new SqlParameter("@TrangThai", cboTrangThai.SelectedItem?.ToString() ?? "Trống"),
                    new SqlParameter("@MaPhong", selectedMaPhong)
                };

                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Cập nhật phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaPhong == 0) return;

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa phòng {txtSoPhong.Text}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                string sql = "DELETE FROM Phong WHERE MaPhong = @MaPhong";
                SqlParameter[] p = { new SqlParameter("@MaPhong", selectedMaPhong) };
                DatabaseHelper.ExecuteNonQuery(sql, p);
                MessageBox.Show("Xóa phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa phòng (có thể phòng đang liên kết với phiếu dịch vụ/hóa đơn): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapNhatTrangThai_Click(object sender, EventArgs e)
        {
            btnSua_Click(sender, e);
        }

        private void btnLapPhieu_Click(object sender, EventArgs e)
        {
            if (selectedMaPhong == 0) return;
            // Switch main window to PhieuDichVu form
            if (this.ParentForm is MainForm main)
            {
                main.OpenChildForm(new frmPhieuDichVu(selectedMaPhong), null, "📋 LẬP PHIẾU DỊCH VỤ");
            }
        }
    }
}
