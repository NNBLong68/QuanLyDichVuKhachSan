using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyDichVuKhachSan
{
    public partial class MainForm : Form
    {
        private Form activeChildForm = null;
        private Button activeNavBtn = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Set User Badge info
            if (SessionManager.CurrentUser != null)
            {
                lblUserInfo.Text = "👤 " + SessionManager.CurrentUser.HoTen;
                lblUserRole.Text = $"{SessionManager.CurrentUser.ChucVu} | {SessionManager.CurrentUser.CaTruc}";

                // Role-based visibility
                if (!SessionManager.CurrentUser.IsAdmin)
                {
                    btnNhapKho.Visible = false;
                    btnNhanVien.Visible = false;
                }
            }

            // Open Sơ đồ phòng as default home form
            OpenChildForm(new frmPhong(), btnSoDoPhong, "🚪 SƠ ĐỒ PHÒNG KHÁCH SẠN");
        }

        public void OpenChildForm(Form childForm, Button navBtn, string pageTitle)
        {
            if (activeChildForm != null)
            {
                activeChildForm.Close();
            }

            activeChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

            lblPageTitle.Text = pageTitle;
            HighlightNavButton(navBtn);
        }

        private void HighlightNavButton(Button btn)
        {
            if (activeNavBtn != null)
            {
                activeNavBtn.BackColor = Color.FromArgb(15, 23, 42);
                activeNavBtn.ForeColor = Color.FromArgb(226, 232, 240);
            }

            activeNavBtn = btn;
            if (activeNavBtn != null)
            {
                activeNavBtn.BackColor = Color.FromArgb(30, 41, 59);
                activeNavBtn.ForeColor = Color.FromArgb(52, 211, 153);
            }
        }

        private void btnSoDoPhong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmPhong(), btnSoDoPhong, "🚪 SƠ ĐỒ PHÒNG KHÁCH SẠN");
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDichVu(), btnDichVu, "🍽️ DANH MỤC DỊCH VỤ");
        }

        private void btnPhieuDichVu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmPhieuDichVu(), btnPhieuDichVu, "📋 LẬP PHIẾU DỊCH VỤ");
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmKhachHang(), btnKhachHang, "👥 QUẢN LÝ KHÁCH HÀNG");
        }

        private void btnDoiTac_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDoiTac(), btnDoiTac, "🧺 QUẢN LÝ ĐỐI TÁC GIẶT ỦI");
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmNhapKho(), btnNhapKho, "📦 LỊCH SỬ NHẬP KHO THỰC PHẨM");
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHoaDon(), btnHoaDon, "💳 QUẢN LÝ HÓA ĐƠN & THANH TOÁN");
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmThongKe(), btnThongKe, "📊 BÁO CÁO THỐNG KÊ DOANH THU");
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmNhanVien(), btnNhanVien, "👤 QUẢN LÝ NHÂN VIÊN");
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                SessionManager.CurrentUser = null;
                this.Hide();
                frmDangNhap login = new frmDangNhap();
                login.Show();
            }
        }
    }
}
