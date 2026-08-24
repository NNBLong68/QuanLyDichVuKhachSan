using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDichVuKhachSan
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Set màu cho từng nút tab
            SetTabButtonStyle(btnSoDoPhong);
            SetTabButtonStyle(btnDichVu);
            SetTabButtonStyle(btnKhachHang);
            SetTabButtonStyle(btnHoaDon);
            SetTabButtonStyle(btnThongKe);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pnlHeader.BackColor = ColorTranslator.FromHtml("#1B2A4A");
            LO_pnlTabBar.BackColor = ColorTranslator.FromHtml("#1B2A4A");

            btnDichVu.BackColor = ColorTranslator.FromHtml("#1B2A4A");
            btnHoaDon.BackColor = ColorTranslator.FromHtml("#1B2A4A");
            btnKhachHang.BackColor = ColorTranslator.FromHtml("#1B2A4A");
            btnSoDoPhong.BackColor = ColorTranslator.FromHtml("#1B2A4A");
            btnThongKe.BackColor = ColorTranslator.FromHtml("#1B2A4A");
        }
        private void SetTabButtonStyle(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#283A64");
            btn.FlatAppearance.MouseDownBackColor = ColorTranslator.FromHtml("#101A31");
        }

        //Nhúng form vào panel Content
        private Form currentChildForm;
        private void OpenChildForm(Form childForm, Button activeBtn)
        {
            //currentChildForm là biến lưu form con đang được mở
            currentChildForm?.Close(); //Đóng form đang mở
            // Dấu ?. nghĩa là nếu khác null thì gọi close()

            currentChildForm = childForm; //gán form mới vào biến hiện tại
            childForm.TopLevel = false; //hạ bậc form, không còn có thể nắm kéo đi hay điều chỉnh nữa -> Biến thành form con
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(childForm); //nhúng form con vào panel
            childForm.BringToFront(); //đưa form con lên trên cùng
            childForm.Show();

            SetActiveTab(activeBtn);
            this.ActiveControl = pnlContent; //Dời focus khỏi nút
        }

        private void SetActiveTab(Button btn)
        {
            foreach (Control ctrl in LO_pnlTabBar.Controls)
            {
                if (ctrl is Button button)
                {
                    button.BackColor = ColorTranslator.FromHtml("#1B2A4A");
                    button.ForeColor = SystemColors.Info;
                }
            }

            btn.ForeColor = ColorTranslator.FromHtml("#FFC857");
        }

        private void btnSoDoPhong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmPhong(), btnSoDoPhong);
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDichVu(), btnDichVu);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmKhachHang(), btnKhachHang);
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHoaDon(), btnHoaDon);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmThongKe(), btnThongKe);
        }
    }
}
