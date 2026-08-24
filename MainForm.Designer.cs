namespace QuanLyDichVuKhachSan
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSub = new System.Windows.Forms.Label();
            this.lblTenKS = new System.Windows.Forms.Label();
            this.LO_pnlTabBar = new System.Windows.Forms.TableLayoutPanel();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnSoDoPhong = new System.Windows.Forms.Button();
            this.btnHoaDon = new System.Windows.Forms.Button();
            this.btnDichVu = new System.Windows.Forms.Button();
            this.btnKhachHang = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.LO_pnlTabBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.SystemColors.HotTrack;
            this.pnlHeader.Controls.Add(this.lblSub);
            this.pnlHeader.Controls.Add(this.lblTenKS);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1396, 117);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSub.ForeColor = System.Drawing.SystemColors.Info;
            this.lblSub.Location = new System.Drawing.Point(15, 60);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(164, 24);
            this.lblSub.TabIndex = 0;
            this.lblSub.Text = "Quản lý dịch vụ ";
            // 
            // lblTenKS
            // 
            this.lblTenKS.AutoSize = true;
            this.lblTenKS.Font = new System.Drawing.Font("Arial", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenKS.ForeColor = System.Drawing.SystemColors.Info;
            this.lblTenKS.Location = new System.Drawing.Point(12, 18);
            this.lblTenKS.Name = "lblTenKS";
            this.lblTenKS.Size = new System.Drawing.Size(367, 38);
            this.lblTenKS.TabIndex = 0;
            this.lblTenKS.Text = "Khách sạn Campuchia";
            // 
            // LO_pnlTabBar
            // 
            this.LO_pnlTabBar.ColumnCount = 5;
            this.LO_pnlTabBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.LO_pnlTabBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.LO_pnlTabBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.LO_pnlTabBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.LO_pnlTabBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.LO_pnlTabBar.Controls.Add(this.btnThongKe, 4, 0);
            this.LO_pnlTabBar.Controls.Add(this.btnSoDoPhong, 0, 0);
            this.LO_pnlTabBar.Controls.Add(this.btnHoaDon, 3, 0);
            this.LO_pnlTabBar.Controls.Add(this.btnDichVu, 1, 0);
            this.LO_pnlTabBar.Controls.Add(this.btnKhachHang, 2, 0);
            this.LO_pnlTabBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.LO_pnlTabBar.Location = new System.Drawing.Point(0, 117);
            this.LO_pnlTabBar.Name = "LO_pnlTabBar";
            this.LO_pnlTabBar.RowCount = 1;
            this.LO_pnlTabBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LO_pnlTabBar.Size = new System.Drawing.Size(1396, 64);
            this.LO_pnlTabBar.TabIndex = 2;
            // 
            // btnThongKe
            // 
            this.btnThongKe.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnThongKe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThongKe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnThongKe.FlatAppearance.BorderSize = 0;
            this.btnThongKe.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.btnThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKe.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKe.ForeColor = System.Drawing.SystemColors.Info;
            this.btnThongKe.Location = new System.Drawing.Point(1119, 3);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(274, 58);
            this.btnThongKe.TabIndex = 0;
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = false;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // btnSoDoPhong
            // 
            this.btnSoDoPhong.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSoDoPhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSoDoPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSoDoPhong.FlatAppearance.BorderSize = 0;
            this.btnSoDoPhong.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSoDoPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSoDoPhong.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSoDoPhong.ForeColor = System.Drawing.SystemColors.Info;
            this.btnSoDoPhong.Location = new System.Drawing.Point(3, 3);
            this.btnSoDoPhong.Name = "btnSoDoPhong";
            this.btnSoDoPhong.Size = new System.Drawing.Size(273, 58);
            this.btnSoDoPhong.TabIndex = 0;
            this.btnSoDoPhong.Text = "Phòng";
            this.btnSoDoPhong.UseVisualStyleBackColor = false;
            this.btnSoDoPhong.Click += new System.EventHandler(this.btnSoDoPhong_Click);
            // 
            // btnHoaDon
            // 
            this.btnHoaDon.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHoaDon.FlatAppearance.BorderSize = 0;
            this.btnHoaDon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.btnHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoaDon.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHoaDon.ForeColor = System.Drawing.SystemColors.Info;
            this.btnHoaDon.Location = new System.Drawing.Point(840, 3);
            this.btnHoaDon.Name = "btnHoaDon";
            this.btnHoaDon.Size = new System.Drawing.Size(273, 58);
            this.btnHoaDon.TabIndex = 0;
            this.btnHoaDon.Text = "Hóa đơn";
            this.btnHoaDon.UseVisualStyleBackColor = false;
            this.btnHoaDon.Click += new System.EventHandler(this.btnHoaDon_Click);
            // 
            // btnDichVu
            // 
            this.btnDichVu.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnDichVu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDichVu.FlatAppearance.BorderSize = 0;
            this.btnDichVu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.btnDichVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDichVu.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDichVu.ForeColor = System.Drawing.SystemColors.Info;
            this.btnDichVu.Location = new System.Drawing.Point(282, 3);
            this.btnDichVu.Name = "btnDichVu";
            this.btnDichVu.Size = new System.Drawing.Size(273, 58);
            this.btnDichVu.TabIndex = 0;
            this.btnDichVu.Text = "Dịch vụ";
            this.btnDichVu.UseVisualStyleBackColor = false;
            this.btnDichVu.Click += new System.EventHandler(this.btnDichVu_Click);
            // 
            // btnKhachHang
            // 
            this.btnKhachHang.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnKhachHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnKhachHang.FlatAppearance.BorderSize = 0;
            this.btnKhachHang.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.btnKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKhachHang.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKhachHang.ForeColor = System.Drawing.SystemColors.Info;
            this.btnKhachHang.Location = new System.Drawing.Point(561, 3);
            this.btnKhachHang.Name = "btnKhachHang";
            this.btnKhachHang.Size = new System.Drawing.Size(273, 58);
            this.btnKhachHang.TabIndex = 0;
            this.btnKhachHang.Text = "Khách hàng";
            this.btnKhachHang.UseVisualStyleBackColor = false;
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 181);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1396, 516);
            this.pnlContent.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1396, 697);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.LO_pnlTabBar);
            this.Controls.Add(this.pnlHeader);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Khách sạn Campuchia - Quản lý dịch vụ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.LO_pnlTabBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Label lblTenKS;
        private System.Windows.Forms.TableLayoutPanel LO_pnlTabBar;
        private System.Windows.Forms.Button btnSoDoPhong;
        private System.Windows.Forms.Button btnDichVu;
        private System.Windows.Forms.Button btnKhachHang;
        private System.Windows.Forms.Button btnHoaDon;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Panel pnlContent;
    }
}

