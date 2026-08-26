namespace QuanLyDichVuKhachSan
{
    partial class frmThongKe
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
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnTatCa = new System.Windows.Forms.Button();
            this.btnThangNay = new System.Windows.Forms.Button();
            this.btnHomNay = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tabReports = new System.Windows.Forms.TabControl();
            this.tabDoanhThuChung = new System.Windows.Forms.TabPage();
            this.dgvDoanhThuChung = new System.Windows.Forms.DataGridView();
            this.tabAnUong = new System.Windows.Forms.TabPage();
            this.dgvAnUongMon = new System.Windows.Forms.DataGridView();
            this.dgvAnUongTon = new System.Windows.Forms.DataGridView();
            this.lblAnUongTon = new System.Windows.Forms.Label();
            this.lblAnUongMon = new System.Windows.Forms.Label();
            this.tabThueXe = new System.Windows.Forms.TabPage();
            this.dgvThueXe = new System.Windows.Forms.DataGridView();
            this.tabSuKien = new System.Windows.Forms.TabPage();
            this.dgvSuKien = new System.Windows.Forms.DataGridView();
            this.tabDoXe = new System.Windows.Forms.TabPage();
            this.dgvDoXe = new System.Windows.Forms.DataGridView();
            this.tabGiatUi = new System.Windows.Forms.TabPage();
            this.dgvGiatUi = new System.Windows.Forms.DataGridView();
            this.pnlTopBar.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tabReports.SuspendLayout();
            this.tabDoanhThuChung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThuChung)).BeginInit();
            this.tabAnUong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnUongMon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnUongTon)).BeginInit();
            this.tabThueXe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThueXe)).BeginInit();
            this.tabSuKien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuKien)).BeginInit();
            this.tabDoXe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoXe)).BeginInit();
            this.tabGiatUi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiatUi)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlTopBar.Controls.Add(this.btnHomNay);
            this.pnlTopBar.Controls.Add(this.btnThangNay);
            this.pnlTopBar.Controls.Add(this.btnTatCa);
            this.pnlTopBar.Controls.Add(this.btnLoc);
            this.pnlTopBar.Controls.Add(this.dtpDenNgay);
            this.pnlTopBar.Controls.Add(this.lblDenNgay);
            this.pnlTopBar.Controls.Add(this.dtpTuNgay);
            this.pnlTopBar.Controls.Add(this.lblTuNgay);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Location = new System.Drawing.Point(15, 15);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Size = new System.Drawing.Size(1010, 55);
            this.pnlTopBar.TabIndex = 0;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTuNgay.ForeColor = System.Drawing.Color.White;
            this.lblTuNgay.Location = new System.Drawing.Point(15, 18);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(63, 17);
            this.lblTuNgay.TabIndex = 0;
            this.lblTuNgay.Text = "Từ Ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.dtpTuNgay.Location = new System.Drawing.Point(85, 15);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(120, 23);
            this.dtpTuNgay.TabIndex = 1;
            this.dtpTuNgay.Value = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDenNgay.ForeColor = System.Drawing.Color.White;
            this.lblDenNgay.Location = new System.Drawing.Point(220, 18);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(72, 17);
            this.lblDenNgay.TabIndex = 2;
            this.lblDenNgay.Text = "Đến Ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.dtpDenNgay.Location = new System.Drawing.Point(295, 15);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(120, 23);
            this.dtpDenNgay.TabIndex = 3;
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnLoc.FlatAppearance.BorderSize = 0;
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(435, 12);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(100, 30);
            this.btnLoc.TabIndex = 4;
            this.btnLoc.Text = "📊 Lọc Báo Cáo";
            this.btnLoc.UseVisualStyleBackColor = false;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // btnTatCa
            // 
            this.btnTatCa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnTatCa.FlatAppearance.BorderSize = 0;
            this.btnTatCa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTatCa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnTatCa.ForeColor = System.Drawing.Color.White;
            this.btnTatCa.Location = new System.Drawing.Point(550, 12);
            this.btnTatCa.Name = "btnTatCa";
            this.btnTatCa.Size = new System.Drawing.Size(80, 30);
            this.btnTatCa.TabIndex = 5;
            this.btnTatCa.Text = "Tất cả";
            this.btnTatCa.UseVisualStyleBackColor = false;
            this.btnTatCa.Click += new System.EventHandler(this.btnTatCa_Click);
            // 
            // btnThangNay
            // 
            this.btnThangNay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnThangNay.FlatAppearance.BorderSize = 0;
            this.btnThangNay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThangNay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnThangNay.ForeColor = System.Drawing.Color.White;
            this.btnThangNay.Location = new System.Drawing.Point(640, 12);
            this.btnThangNay.Name = "btnThangNay";
            this.btnThangNay.Size = new System.Drawing.Size(90, 30);
            this.btnThangNay.TabIndex = 6;
            this.btnThangNay.Text = "Tháng này";
            this.btnThangNay.UseVisualStyleBackColor = false;
            this.btnThangNay.Click += new System.EventHandler(this.btnThangNay_Click);
            // 
            // btnHomNay
            // 
            this.btnHomNay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnHomNay.FlatAppearance.BorderSize = 0;
            this.btnHomNay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHomNay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnHomNay.ForeColor = System.Drawing.Color.White;
            this.btnHomNay.Location = new System.Drawing.Point(740, 12);
            this.btnHomNay.Name = "btnHomNay";
            this.btnHomNay.Size = new System.Drawing.Size(80, 30);
            this.btnHomNay.TabIndex = 7;
            this.btnHomNay.Text = "Hôm nay";
            this.btnHomNay.UseVisualStyleBackColor = false;
            this.btnHomNay.Click += new System.EventHandler(this.btnHomNay_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tabReports);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(15, 70);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlMain.Size = new System.Drawing.Size(1010, 595);
            this.pnlMain.TabIndex = 1;
            // 
            // tabReports
            // 
            this.tabReports.Controls.Add(this.tabDoanhThuChung);
            this.tabReports.Controls.Add(this.tabAnUong);
            this.tabReports.Controls.Add(this.tabThueXe);
            this.tabReports.Controls.Add(this.tabSuKien);
            this.tabReports.Controls.Add(this.tabDoXe);
            this.tabReports.Controls.Add(this.tabGiatUi);
            this.tabReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabReports.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tabReports.Location = new System.Drawing.Point(0, 10);
            this.tabReports.Name = "tabReports";
            this.tabReports.SelectedIndex = 0;
            this.tabReports.Size = new System.Drawing.Size(1010, 585);
            this.tabReports.TabIndex = 0;
            this.tabReports.SelectedIndexChanged += new System.EventHandler(this.tabReports_SelectedIndexChanged);
            // 
            // tabDoanhThuChung
            // 
            this.tabDoanhThuChung.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.tabDoanhThuChung.Controls.Add(this.dgvDoanhThuChung);
            this.tabDoanhThuChung.Location = new System.Drawing.Point(4, 26);
            this.tabDoanhThuChung.Name = "tabDoanhThuChung";
            this.tabDoanhThuChung.Padding = new System.Windows.Forms.Padding(10);
            this.tabDoanhThuChung.Size = new System.Drawing.Size(1002, 555);
            this.tabDoanhThuChung.TabIndex = 0;
            this.tabDoanhThuChung.Text = "📊 Doanh Thu Chung";
            // 
            // dgvDoanhThuChung
            // 
            this.dgvDoanhThuChung.AllowUserToAddRows = false;
            this.dgvDoanhThuChung.AllowUserToDeleteRows = false;
            this.dgvDoanhThuChung.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoanhThuChung.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dgvDoanhThuChung.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDoanhThuChung.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoanhThuChung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDoanhThuChung.Location = new System.Drawing.Point(10, 10);
            this.dgvDoanhThuChung.MultiSelect = false;
            this.dgvDoanhThuChung.Name = "dgvDoanhThuChung";
            this.dgvDoanhThuChung.ReadOnly = true;
            this.dgvDoanhThuChung.RowHeadersVisible = false;
            this.dgvDoanhThuChung.RowTemplate.Height = 32;
            this.dgvDoanhThuChung.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoanhThuChung.Size = new System.Drawing.Size(982, 535);
            this.dgvDoanhThuChung.TabIndex = 0;
            // 
            // tabAnUong
            // 
            this.tabAnUong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.tabAnUong.Controls.Add(this.dgvAnUongTon);
            this.tabAnUong.Controls.Add(this.lblAnUongTon);
            this.tabAnUong.Controls.Add(this.dgvAnUongMon);
            this.tabAnUong.Controls.Add(this.lblAnUongMon);
            this.tabAnUong.Location = new System.Drawing.Point(4, 26);
            this.tabAnUong.Name = "tabAnUong";
            this.tabAnUong.Padding = new System.Windows.Forms.Padding(10);
            this.tabAnUong.Size = new System.Drawing.Size(1002, 555);
            this.tabAnUong.TabIndex = 1;
            this.tabAnUong.Text = "🍜 Báo Cáo Ăn Uống & Kho";
            // 
            // lblAnUongMon
            // 
            this.lblAnUongMon.AutoSize = true;
            this.lblAnUongMon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(211)))), ((int)(((byte)(153)))));
            this.lblAnUongMon.Location = new System.Drawing.Point(10, 10);
            this.lblAnUongMon.Name = "lblAnUongMon";
            this.lblAnUongMon.Size = new System.Drawing.Size(258, 17);
            this.lblAnUongMon.TabIndex = 0;
            this.lblAnUongMon.Text = "TOP MÓN ĂN / ĐỒ UỐNG BÁN CHẠY:";
            // 
            // dgvAnUongMon
            // 
            this.dgvAnUongMon.AllowUserToAddRows = false;
            this.dgvAnUongMon.AllowUserToDeleteRows = false;
            this.dgvAnUongMon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAnUongMon.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dgvAnUongMon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAnUongMon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnUongMon.Location = new System.Drawing.Point(10, 35);
            this.dgvAnUongMon.MultiSelect = false;
            this.dgvAnUongMon.Name = "dgvAnUongMon";
            this.dgvAnUongMon.ReadOnly = true;
            this.dgvAnUongMon.RowHeadersVisible = false;
            this.dgvAnUongMon.RowTemplate.Height = 28;
            this.dgvAnUongMon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAnUongMon.Size = new System.Drawing.Size(982, 230);
            this.dgvAnUongMon.TabIndex = 1;
            // 
            // lblAnUongTon
            // 
            this.lblAnUongTon.AutoSize = true;
            this.lblAnUongTon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(191)))), ((int)(((byte)(36)))));
            this.lblAnUongTon.Location = new System.Drawing.Point(10, 275);
            this.lblAnUongTon.Name = "lblAnUongTon";
            this.lblAnUongTon.Size = new System.Drawing.Size(288, 17);
            this.lblAnUongTon.TabIndex = 2;
            this.lblAnUongTon.Text = "TỒN KHO THỰC PHẨM KHÔ & ĐÃ TIÊU THỤ:";
            // 
            // dgvAnUongTon
            // 
            this.dgvAnUongTon.AllowUserToAddRows = false;
            this.dgvAnUongTon.AllowUserToDeleteRows = false;
            this.dgvAnUongTon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAnUongTon.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dgvAnUongTon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAnUongTon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnUongTon.Location = new System.Drawing.Point(10, 300);
            this.dgvAnUongTon.MultiSelect = false;
            this.dgvAnUongTon.Name = "dgvAnUongTon";
            this.dgvAnUongTon.ReadOnly = true;
            this.dgvAnUongTon.RowHeadersVisible = false;
            this.dgvAnUongTon.RowTemplate.Height = 28;
            this.dgvAnUongTon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAnUongTon.Size = new System.Drawing.Size(982, 245);
            this.dgvAnUongTon.TabIndex = 3;
            // 
            // tabThueXe
            // 
            this.tabThueXe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.tabThueXe.Controls.Add(this.dgvThueXe);
            this.tabThueXe.Location = new System.Drawing.Point(4, 26);
            this.tabThueXe.Name = "tabThueXe";
            this.tabThueXe.Padding = new System.Windows.Forms.Padding(10);
            this.tabThueXe.Size = new System.Drawing.Size(1002, 555);
            this.tabThueXe.TabIndex = 2;
            this.tabThueXe.Text = "🚗 Báo Cáo Thuê Xe";
            // 
            // dgvThueXe
            // 
            this.dgvThueXe.AllowUserToAddRows = false;
            this.dgvThueXe.AllowUserToDeleteRows = false;
            this.dgvThueXe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvThueXe.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dgvThueXe.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvThueXe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThueXe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvThueXe.Location = new System.Drawing.Point(10, 10);
            this.dgvThueXe.MultiSelect = false;
            this.dgvThueXe.Name = "dgvThueXe";
            this.dgvThueXe.ReadOnly = true;
            this.dgvThueXe.RowHeadersVisible = false;
            this.dgvThueXe.RowTemplate.Height = 32;
            this.dgvThueXe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThueXe.Size = new System.Drawing.Size(982, 535);
            this.dgvThueXe.TabIndex = 0;
            // 
            // tabSuKien
            // 
            this.tabSuKien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.tabSuKien.Controls.Add(this.dgvSuKien);
            this.tabSuKien.Location = new System.Drawing.Point(4, 26);
            this.tabSuKien.Name = "tabSuKien";
            this.tabSuKien.Padding = new System.Windows.Forms.Padding(10);
            this.tabSuKien.Size = new System.Drawing.Size(1002, 555);
            this.tabSuKien.TabIndex = 3;
            this.tabSuKien.Text = "🏛️ Báo Cáo Sự Kiện";
            // 
            // dgvSuKien
            // 
            this.dgvSuKien.AllowUserToAddRows = false;
            this.dgvSuKien.AllowUserToDeleteRows = false;
            this.dgvSuKien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSuKien.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dgvSuKien.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSuKien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSuKien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSuKien.Location = new System.Drawing.Point(10, 10);
            this.dgvSuKien.MultiSelect = false;
            this.dgvSuKien.Name = "dgvSuKien";
            this.dgvSuKien.ReadOnly = true;
            this.dgvSuKien.RowHeadersVisible = false;
            this.dgvSuKien.RowTemplate.Height = 32;
            this.dgvSuKien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSuKien.Size = new System.Drawing.Size(982, 535);
            this.dgvSuKien.TabIndex = 0;
            // 
            // tabDoXe
            // 
            this.tabDoXe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.tabDoXe.Controls.Add(this.dgvDoXe);
            this.tabDoXe.Location = new System.Drawing.Point(4, 26);
            this.tabDoXe.Name = "tabDoXe";
            this.tabDoXe.Padding = new System.Windows.Forms.Padding(10);
            this.tabDoXe.Size = new System.Drawing.Size(1002, 555);
            this.tabDoXe.TabIndex = 4;
            this.tabDoXe.Text = "🅿️ Báo Cáo Bãi Đỗ Xe";
            // 
            // dgvDoXe
            // 
            this.dgvDoXe.AllowUserToAddRows = false;
            this.dgvDoXe.AllowUserToDeleteRows = false;
            this.dgvDoXe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoXe.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dgvDoXe.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDoXe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoXe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDoXe.Location = new System.Drawing.Point(10, 10);
            this.dgvDoXe.MultiSelect = false;
            this.dgvDoXe.Name = "dgvDoXe";
            this.dgvDoXe.ReadOnly = true;
            this.dgvDoXe.RowHeadersVisible = false;
            this.dgvDoXe.RowTemplate.Height = 32;
            this.dgvDoXe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoXe.Size = new System.Drawing.Size(982, 535);
            this.dgvDoXe.TabIndex = 0;
            // 
            // tabGiatUi
            // 
            this.tabGiatUi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.tabGiatUi.Controls.Add(this.dgvGiatUi);
            this.tabGiatUi.Location = new System.Drawing.Point(4, 26);
            this.tabGiatUi.Name = "tabGiatUi";
            this.tabGiatUi.Padding = new System.Windows.Forms.Padding(10);
            this.tabGiatUi.Size = new System.Drawing.Size(1002, 555);
            this.tabGiatUi.TabIndex = 5;
            this.tabGiatUi.Text = "🧺 Báo Cáo Giặt Ủi & Đối Tác";
            // 
            // dgvGiatUi
            // 
            this.dgvGiatUi.AllowUserToAddRows = false;
            this.dgvGiatUi.AllowUserToDeleteRows = false;
            this.dgvGiatUi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGiatUi.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dgvGiatUi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGiatUi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGiatUi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGiatUi.Location = new System.Drawing.Point(10, 10);
            this.dgvGiatUi.MultiSelect = false;
            this.dgvGiatUi.Name = "dgvGiatUi";
            this.dgvGiatUi.ReadOnly = true;
            this.dgvGiatUi.RowHeadersVisible = false;
            this.dgvGiatUi.RowTemplate.Height = 32;
            this.dgvGiatUi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGiatUi.Size = new System.Drawing.Size(982, 535);
            this.dgvGiatUi.TabIndex = 0;
            // 
            // frmThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(1040, 680);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTopBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmThongKe";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Text = "Báo cáo thống kê";
            this.Load += new System.EventHandler(this.frmThongKe_Load);
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.tabReports.ResumeLayout(false);
            this.tabDoanhThuChung.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThuChung)).EndInit();
            this.tabAnUong.ResumeLayout(false);
            this.tabAnUong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnUongMon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnUongTon)).EndInit();
            this.tabThueXe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThueXe)).EndInit();
            this.tabSuKien.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuKien)).EndInit();
            this.tabDoXe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoXe)).EndInit();
            this.tabGiatUi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiatUi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Button btnTatCa;
        private System.Windows.Forms.Button btnThangNay;
        private System.Windows.Forms.Button btnHomNay;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TabControl tabReports;
        private System.Windows.Forms.TabPage tabDoanhThuChung;
        private System.Windows.Forms.DataGridView dgvDoanhThuChung;
        private System.Windows.Forms.TabPage tabAnUong;
        private System.Windows.Forms.Label lblAnUongMon;
        private System.Windows.Forms.DataGridView dgvAnUongMon;
        private System.Windows.Forms.Label lblAnUongTon;
        private System.Windows.Forms.DataGridView dgvAnUongTon;
        private System.Windows.Forms.TabPage tabThueXe;
        private System.Windows.Forms.DataGridView dgvThueXe;
        private System.Windows.Forms.TabPage tabSuKien;
        private System.Windows.Forms.DataGridView dgvSuKien;
        private System.Windows.Forms.TabPage tabDoXe;
        private System.Windows.Forms.DataGridView dgvDoXe;
        private System.Windows.Forms.TabPage tabGiatUi;
        private System.Windows.Forms.DataGridView dgvGiatUi;
    }
}