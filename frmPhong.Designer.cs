namespace QuanLyDichVuKhachSan
{
    partial class frmPhong
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblStatTotal = new System.Windows.Forms.Label();
            this.lblStatOccupied = new System.Windows.Forms.Label();
            this.lblStatAvailable = new System.Windows.Forms.Label();
            this.lblStatCleaning = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dgvPhong = new System.Windows.Forms.DataGridView();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.lblControlTitle = new System.Windows.Forms.Label();
            this.lblSoPhong = new System.Windows.Forms.Label();
            this.txtSoPhong = new System.Windows.Forms.TextBox();
            this.lblLoaiPhong = new System.Windows.Forms.Label();
            this.txtLoaiPhong = new System.Windows.Forms.TextBox();
            this.lblGiaGio = new System.Windows.Forms.Label();
            this.txtGiaGio = new System.Windows.Forms.TextBox();
            this.lblGiaNgay = new System.Windows.Forms.Label();
            this.txtGiaNgay = new System.Windows.Forms.TextBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnCapNhatTrangThai = new System.Windows.Forms.Button();
            this.btnLapPhieu = new System.Windows.Forms.Button();
            this.pnlSummary.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit();
            this.pnlControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlSummary.Controls.Add(this.lblStatCleaning);
            this.pnlSummary.Controls.Add(this.lblStatAvailable);
            this.pnlSummary.Controls.Add(this.lblStatOccupied);
            this.pnlSummary.Controls.Add(this.lblStatTotal);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummary.Location = new System.Drawing.Point(15, 15);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(1010, 60);
            this.pnlSummary.TabIndex = 0;
            // 
            // lblStatTotal
            // 
            this.lblStatTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatTotal.ForeColor = System.Drawing.Color.White;
            this.lblStatTotal.Location = new System.Drawing.Point(15, 15);
            this.lblStatTotal.Name = "lblStatTotal";
            this.lblStatTotal.Size = new System.Drawing.Size(200, 30);
            this.lblStatTotal.TabIndex = 0;
            this.lblStatTotal.Text = "🏨 Tổng số phòng: 0";
            this.lblStatTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatOccupied
            // 
            this.lblStatOccupied.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatOccupied.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(165)))), ((int)(((byte)(250)))));
            this.lblStatOccupied.Location = new System.Drawing.Point(230, 15);
            this.lblStatOccupied.Name = "lblStatOccupied";
            this.lblStatOccupied.Size = new System.Drawing.Size(220, 30);
            this.lblStatOccupied.TabIndex = 1;
            this.lblStatOccupied.Text = "🔵 Đang sử dụng: 0";
            this.lblStatOccupied.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatAvailable
            // 
            this.lblStatAvailable.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatAvailable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(211)))), ((int)(((byte)(153)))));
            this.lblStatAvailable.Location = new System.Drawing.Point(470, 15);
            this.lblStatAvailable.Name = "lblStatAvailable";
            this.lblStatAvailable.Size = new System.Drawing.Size(200, 30);
            this.lblStatAvailable.TabIndex = 2;
            this.lblStatAvailable.Text = "🟢 Phòng trống: 0";
            this.lblStatAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatCleaning
            // 
            this.lblStatCleaning.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatCleaning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(191)))), ((int)(((byte)(36)))));
            this.lblStatCleaning.Location = new System.Drawing.Point(700, 15);
            this.lblStatCleaning.Name = "lblStatCleaning";
            this.lblStatCleaning.Size = new System.Drawing.Size(280, 30);
            this.lblStatCleaning.TabIndex = 3;
            this.lblStatCleaning.Text = "🟠 Dọn dẹp/Bảo trì: 0";
            this.lblStatCleaning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.dgvPhong);
            this.pnlMain.Controls.Add(this.pnlControl);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(15, 75);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.pnlMain.Size = new System.Drawing.Size(1010, 590);
            this.pnlMain.TabIndex = 1;
            // 
            // dgvPhong
            // 
            this.dgvPhong.AllowUserToAddRows = false;
            this.dgvPhong.AllowUserToDeleteRows = false;
            this.dgvPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhong.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dgvPhong.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhong.Location = new System.Drawing.Point(0, 15);
            this.dgvPhong.MultiSelect = false;
            this.dgvPhong.Name = "dgvPhong";
            this.dgvPhong.ReadOnly = true;
            this.dgvPhong.RowHeadersVisible = false;
            this.dgvPhong.RowTemplate.Height = 32;
            this.dgvPhong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhong.Size = new System.Drawing.Size(650, 575);
            this.dgvPhong.TabIndex = 0;
            this.dgvPhong.SelectionChanged += new System.EventHandler(this.dgvPhong_SelectionChanged);
            // 
            // pnlControl
            // 
            this.pnlControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlControl.Controls.Add(this.btnLapPhieu);
            this.pnlControl.Controls.Add(this.btnCapNhatTrangThai);
            this.pnlControl.Controls.Add(this.btnXoa);
            this.pnlControl.Controls.Add(this.btnSua);
            this.pnlControl.Controls.Add(this.btnThem);
            this.pnlControl.Controls.Add(this.cboTrangThai);
            this.pnlControl.Controls.Add(this.lblTrangThai);
            this.pnlControl.Controls.Add(this.txtGiaNgay);
            this.pnlControl.Controls.Add(this.lblGiaNgay);
            this.pnlControl.Controls.Add(this.txtGiaGio);
            this.pnlControl.Controls.Add(this.lblGiaGio);
            this.pnlControl.Controls.Add(this.txtLoaiPhong);
            this.pnlControl.Controls.Add(this.lblLoaiPhong);
            this.pnlControl.Controls.Add(this.txtSoPhong);
            this.pnlControl.Controls.Add(this.lblSoPhong);
            this.pnlControl.Controls.Add(this.lblControlTitle);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControl.Location = new System.Drawing.Point(660, 15);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(350, 575);
            this.pnlControl.TabIndex = 1;
            // 
            // lblControlTitle
            // 
            this.lblControlTitle.AutoSize = true;
            this.lblControlTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblControlTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(211)))), ((int)(((byte)(153)))));
            this.lblControlTitle.Location = new System.Drawing.Point(20, 15);
            this.lblControlTitle.Name = "lblControlTitle";
            this.lblControlTitle.Size = new System.Drawing.Size(182, 21);
            this.lblControlTitle.TabIndex = 0;
            this.lblControlTitle.Text = "THÔNG TIN CHI TIẾT";
            // 
            // lblSoPhong
            // 
            this.lblSoPhong.AutoSize = true;
            this.lblSoPhong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSoPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblSoPhong.Location = new System.Drawing.Point(20, 55);
            this.lblSoPhong.Name = "lblSoPhong";
            this.lblSoPhong.Size = new System.Drawing.Size(71, 17);
            this.lblSoPhong.TabIndex = 1;
            this.lblSoPhong.Text = "Số Phòng:";
            // 
            // txtSoPhong
            // 
            this.txtSoPhong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtSoPhong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoPhong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSoPhong.ForeColor = System.Drawing.Color.White;
            this.txtSoPhong.Location = new System.Drawing.Point(20, 75);
            this.txtSoPhong.Name = "txtSoPhong";
            this.txtSoPhong.Size = new System.Drawing.Size(310, 25);
            this.txtSoPhong.TabIndex = 2;
            // 
            // lblLoaiPhong
            // 
            this.lblLoaiPhong.AutoSize = true;
            this.lblLoaiPhong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLoaiPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblLoaiPhong.Location = new System.Drawing.Point(20, 115);
            this.lblLoaiPhong.Name = "lblLoaiPhong";
            this.lblLoaiPhong.Size = new System.Drawing.Size(81, 17);
            this.lblLoaiPhong.TabIndex = 3;
            this.lblLoaiPhong.Text = "Loại Phòng:";
            // 
            // txtLoaiPhong
            // 
            this.txtLoaiPhong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtLoaiPhong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLoaiPhong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtLoaiPhong.ForeColor = System.Drawing.Color.White;
            this.txtLoaiPhong.Location = new System.Drawing.Point(20, 135);
            this.txtLoaiPhong.Name = "txtLoaiPhong";
            this.txtLoaiPhong.Size = new System.Drawing.Size(310, 25);
            this.txtLoaiPhong.TabIndex = 4;
            // 
            // lblGiaGio
            // 
            this.lblGiaGio.AutoSize = true;
            this.lblGiaGio.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblGiaGio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblGiaGio.Location = new System.Drawing.Point(20, 175);
            this.lblGiaGio.Name = "lblGiaGio";
            this.lblGiaGio.Size = new System.Drawing.Size(126, 17);
            this.lblGiaGio.TabIndex = 5;
            this.lblGiaGio.Text = "Giá Giờ (VNĐ/Giờ):";
            // 
            // txtGiaGio
            // 
            this.txtGiaGio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtGiaGio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGiaGio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtGiaGio.ForeColor = System.Drawing.Color.White;
            this.txtGiaGio.Location = new System.Drawing.Point(20, 195);
            this.txtGiaGio.Name = "txtGiaGio";
            this.txtGiaGio.Size = new System.Drawing.Size(310, 25);
            this.txtGiaGio.TabIndex = 6;
            // 
            // lblGiaNgay
            // 
            this.lblGiaNgay.AutoSize = true;
            this.lblGiaNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblGiaNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblGiaNgay.Location = new System.Drawing.Point(20, 235);
            this.lblGiaNgay.Name = "lblGiaNgay";
            this.lblGiaNgay.Size = new System.Drawing.Size(137, 17);
            this.lblGiaNgay.TabIndex = 7;
            this.lblGiaNgay.Text = "Giá Ngày (VNĐ/Ngày):";
            // 
            // txtGiaNgay
            // 
            this.txtGiaNgay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtGiaNgay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGiaNgay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtGiaNgay.ForeColor = System.Drawing.Color.White;
            this.txtGiaNgay.Location = new System.Drawing.Point(20, 255);
            this.txtGiaNgay.Name = "txtGiaNgay";
            this.txtGiaNgay.Size = new System.Drawing.Size(310, 25);
            this.txtGiaNgay.TabIndex = 8;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblTrangThai.Location = new System.Drawing.Point(20, 295);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(78, 17);
            this.lblTrangThai.TabIndex = 9;
            this.lblTrangThai.Text = "Trạng Thái:";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboTrangThai.ForeColor = System.Drawing.Color.White;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Trống",
            "Đang sử dụng",
            "Đang dọn dẹp",
            "Bảo trì"});
            this.cboTrangThai.Location = new System.Drawing.Point(20, 315);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(310, 25);
            this.cboTrangThai.TabIndex = 10;
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(20, 360);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(95, 35);
            this.btnThem.TabIndex = 11;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(125, 360);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(95, 35);
            this.btnSua.TabIndex = 12;
            this.btnSua.Text = "Cập nhật";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(230, 360);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 35);
            this.btnXoa.TabIndex = 13;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnCapNhatTrangThai
            // 
            this.btnCapNhatTrangThai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnCapNhatTrangThai.FlatAppearance.BorderSize = 0;
            this.btnCapNhatTrangThai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCapNhatTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCapNhatTrangThai.ForeColor = System.Drawing.Color.White;
            this.btnCapNhatTrangThai.Location = new System.Drawing.Point(20, 410);
            this.btnCapNhatTrangThai.Name = "btnCapNhatTrangThai";
            this.btnCapNhatTrangThai.Size = new System.Drawing.Size(310, 35);
            this.btnCapNhatTrangThai.TabIndex = 14;
            this.btnCapNhatTrangThai.Text = "⚡ Đổi trạng thái nhanh";
            this.btnCapNhatTrangThai.UseVisualStyleBackColor = false;
            this.btnCapNhatTrangThai.Click += new System.EventHandler(this.btnCapNhatTrangThai_Click);
            // 
            // btnLapPhieu
            // 
            this.btnLapPhieu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnLapPhieu.FlatAppearance.BorderSize = 0;
            this.btnLapPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLapPhieu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLapPhieu.ForeColor = System.Drawing.Color.White;
            this.btnLapPhieu.Location = new System.Drawing.Point(20, 455);
            this.btnLapPhieu.Name = "btnLapPhieu";
            this.btnLapPhieu.Size = new System.Drawing.Size(310, 40);
            this.btnLapPhieu.TabIndex = 15;
            this.btnLapPhieu.Text = "📋 Lập dịch vụ cho phòng này";
            this.btnLapPhieu.UseVisualStyleBackColor = false;
            this.btnLapPhieu.Click += new System.EventHandler(this.btnLapPhieu_Click);
            // 
            // frmPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(1040, 680);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlSummary);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPhong";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Text = "Quản lý phòng";
            this.Load += new System.EventHandler(this.frmPhong_Load);
            this.pnlSummary.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblStatTotal;
        private System.Windows.Forms.Label lblStatOccupied;
        private System.Windows.Forms.Label lblStatAvailable;
        private System.Windows.Forms.Label lblStatCleaning;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.DataGridView dgvPhong;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Label lblControlTitle;
        private System.Windows.Forms.Label lblSoPhong;
        private System.Windows.Forms.TextBox txtSoPhong;
        private System.Windows.Forms.Label lblLoaiPhong;
        private System.Windows.Forms.TextBox txtLoaiPhong;
        private System.Windows.Forms.Label lblGiaGio;
        private System.Windows.Forms.TextBox txtGiaGio;
        private System.Windows.Forms.Label lblGiaNgay;
        private System.Windows.Forms.TextBox txtGiaNgay;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnCapNhatTrangThai;
        private System.Windows.Forms.Button btnLapPhieu;
    }
}