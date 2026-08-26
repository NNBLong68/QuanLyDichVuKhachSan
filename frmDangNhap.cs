using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace QuanLyDichVuKhachSan
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            // Verify connection to SQL Server
            if (DatabaseHelper.TestConnection(out string err))
            {
                lblStatus.ForeColor = System.Drawing.Color.FromArgb(52, 211, 153);
                lblStatus.Text = "✅ Đã kết nối CSDL SQL Server (QuanLyKhachSanDB)";
            }
            else
            {
                lblStatus.ForeColor = System.Drawing.Color.FromArgb(248, 113, 113);
                lblStatus.Text = "❌ Lỗi kết nối CSDL: " + err;
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = "SELECT * FROM NhanVien WHERE TenDangNhap = @User AND MatKhau = @Pass AND TrangThai = N'Hoạt động'";
                SqlParameter[] p = {
                    new SqlParameter("@User", username),
                    new SqlParameter("@Pass", password)
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, p);
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    SessionManager.CurrentUser = new NhanVienModel
                    {
                        MaNhanVien = Convert.ToInt32(r["MaNhanVien"]),
                        HoTen = r["HoTen"].ToString(),
                        CCCD = r["CCCD"].ToString(),
                        SoDienThoai = r["SoDienThoai"].ToString(),
                        ChucVu = r["ChucVu"].ToString(),
                        TenDangNhap = r["TenDangNhap"].ToString(),
                        CaTruc = r["CaTruc"].ToString(),
                        TrangThai = r["TrangThai"].ToString()
                    };

                    this.Hide();
                    MainForm main = new MainForm();
                    main.FormClosed += (s, args) => this.Close();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng, hoặc tài khoản đã ngừng hoạt động!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi kết nối CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuickAdmin_Click(object sender, EventArgs e)
        {
            txtTenDangNhap.Text = "admin";
            txtMatKhau.Text = "YourPassword123!";
            btnDangNhap_Click(sender, e);
        }

        private void btnQuickLetan_Click(object sender, EventArgs e)
        {
            txtTenDangNhap.Text = "letana";
            txtMatKhau.Text = "123456";
            btnDangNhap_Click(sender, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
