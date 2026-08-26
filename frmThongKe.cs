using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace QuanLyDichVuKhachSan
{
    public partial class frmThongKe : Form
    {
        public frmThongKe()
        {
            InitializeComponent();
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            FormatGrid(dgvDoanhThuChung);
            FormatGrid(dgvAnUongMon);
            FormatGrid(dgvAnUongTon);
            FormatGrid(dgvThueXe);
            FormatGrid(dgvSuKien);
            FormatGrid(dgvDoXe);
            FormatGrid(dgvGiatUi);

            btnTatCa_Click(sender, e);
        }

        private void FormatGrid(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 23, 42);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(52, 211, 153);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(79, 70, 229);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgv.GridColor = Color.FromArgb(51, 65, 85);
        }

        private void LoadCurrentTabReport()
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

            SqlParameter[] p = {
                new SqlParameter("@TuNgay", tuNgay),
                new SqlParameter("@DenNgay", denNgay)
            };

            try
            {
                int index = tabReports.SelectedIndex;
                switch (index)
                {
                    case 0: // Doanh thu chung
                        DataSet dsChung = DatabaseHelper.ExecuteStoredProcedure("sp_ThongKeDoanhThuChung", p);
                        if (dsChung.Tables.Count > 0)
                            dgvDoanhThuChung.DataSource = dsChung.Tables[0];
                        break;

                    case 1: // Ăn uống & Kho
                        DataSet dsAU = DatabaseHelper.ExecuteStoredProcedure("sp_ThongKeAnUong", p);
                        if (dsAU.Tables.Count >= 3)
                        {
                            dgvAnUongMon.DataSource = dsAU.Tables[1];
                            dgvAnUongTon.DataSource = dsAU.Tables[2];
                        }
                        break;

                    case 2: // Thuê xe
                        DataSet dsXe = DatabaseHelper.ExecuteStoredProcedure("sp_ThongKeThueXe", p);
                        if (dsXe.Tables.Count > 0)
                            dgvThueXe.DataSource = dsXe.Tables[0];
                        break;

                    case 3: // Sự kiện
                        DataSet dsSK = DatabaseHelper.ExecuteStoredProcedure("sp_ThongKeSuKien", p);
                        if (dsSK.Tables.Count > 0)
                            dgvSuKien.DataSource = dsSK.Tables[0];
                        break;

                    case 4: // Bãi đỗ xe
                        DataSet dsDX = DatabaseHelper.ExecuteStoredProcedure("sp_ThongKeDoXe", p);
                        if (dsDX.Tables.Count > 0)
                            dgvDoXe.DataSource = dsDX.Tables[0];
                        break;

                    case 5: // Giặt ủi
                        DataSet dsGU = DatabaseHelper.ExecuteStoredProcedure("sp_ThongKeGiatUi", p);
                        if (dsGU.Tables.Count > 0)
                            dgvGiatUi.DataSource = dsGU.Tables[0];
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chạy Stored Procedure thống kê: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadCurrentTabReport();
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(2000, 1, 1);
            dtpDenNgay.Value = DateTime.Now;
            LoadCurrentTabReport();
        }

        private void btnThangNay_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = now;
            LoadCurrentTabReport();
        }

        private void btnHomNay_Click(object sender, EventArgs e)
        {
            dtpTuNgay.Value = DateTime.Now.Date;
            dtpDenNgay.Value = DateTime.Now.Date;
            LoadCurrentTabReport();
        }

        private void tabReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCurrentTabReport();
        }
    }
}
