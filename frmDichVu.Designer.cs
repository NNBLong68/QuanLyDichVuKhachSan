namespace QuanLyDichVuKhachSan
{
    partial class frmDichVu
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
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblFilter = new System.Windows.Forms.Label();
            this.cboFilterNhom = new System.Windows.Forms.ComboBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dgvDichVu = new System.Windows.Forms.DataGridView();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.lblControlTitle = new System.Windows.Forms.Label();
            this.lblTenDV = new System.Windows.Forms.Label();
            this.txtTenDV = new System.Windows.Forms.TextBox();
            this.lblNhomDV = new System.Windows.Forms.Label();
            this.cboNhomDV = new System.Windows.Forms.ComboBox();
            this.lblPhanLoai = new System.Windows.Forms.Label();
            this.cboPhanLoai = new System.Windows.Forms.ComboBox();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.lblSoLuongTon = new System.Windows.Forms.Label();
            this.txtSoLuongTon = new System.Windows.Forms.TextBox();
            this.lblSuatAn = new System.Windows.Forms.Label();
            this.txtSuatAn = new System.Windows.Forms.TextBox();
            this.lblChiTiet = new System.Windows.Forms.Label();
            this.txtChiTiet = new System.Windows.Forms.TextBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.pnlFilter.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
            this.pnlControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlFilter.Controls.Add(this.txtSearch);
            this.pnlFilter.Controls.Add(this.lblSearch);
            this.pnlFilter.Controls.Add(this.cboFilterNhom);
            this.pnlFilter.Controls.Add(this.lblFilter);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(15, 15);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1010, 50);
            this.pnlFilter.TabIndex = 0;
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFilter.ForeColor = System.Drawing.Color.White;
            this.lblFilter.Location = new System.Drawing.Point(15, 16);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(127, 17);
            this.lblFilter.TabIndex = 0;
            this.lblFilter.Text = "Lọc nhóm dịch vụ:";
            // 
            // cboFilterNhom
            // 
            this.cboFilterNhom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboFilterNhom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterNhom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboFilterNhom.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboFilterNhom.ForeColor = System.Drawing.Color.White;
            this.cboFilterNhom.FormattingEnabled = true;
            this.cboFilterNhom.Items.AddRange(new object[] {
            "Tất cả",
            "Ăn uống tươi",
            "Thực phẩm khô",
            "Cho thuê xe",
            "Sảnh sự kiện",
            "Bãi đỗ xe",
            "Giặt ủi"});
            this.cboFilterNhom.Location = new System.Drawing.Point(145, 13);
            this.cboFilterNhom.Name = "cboFilterNhom";
            this.cboFilterNhom.Size = new System.Drawing.Size(200, 25);
            this.cboFilterNhom.TabIndex = 1;
            this.cboFilterNhom.SelectedIndexChanged += new System.EventHandler(this.cboFilterNhom_SelectedIndexChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSearch.ForeColor = System.Drawing.Color.White;
            this.lblSearch.Location = new System.Drawing.Point(380, 16);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(70, 17);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Tìm kiếm:";
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(455, 13);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(250, 24);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.dgvDichVu);
            this.pnlMain.Controls.Add(this.pnlControl);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(15, 65);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.pnlMain.Size = new System.Drawing.Size(1010, 600);
            this.pnlMain.TabIndex = 1;
            // 
            // dgvDichVu
            // 
            this.dgvDichVu.AllowUserToAddRows = false;
            this.dgvDichVu.AllowUserToDeleteRows = false;
            this.dgvDichVu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDichVu.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dgvDichVu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDichVu.Location = new System.Drawing.Point(0, 15);
            this.dgvDichVu.MultiSelect = false;
            this.dgvDichVu.Name = "dgvDichVu";
            this.dgvDichVu.ReadOnly = true;
            this.dgvDichVu.RowHeadersVisible = false;
            this.dgvDichVu.RowTemplate.Height = 32;
            this.dgvDichVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDichVu.Size = new System.Drawing.Size(630, 585);
            this.dgvDichVu.TabIndex = 0;
            this.dgvDichVu.SelectionChanged += new System.EventHandler(this.dgvDichVu_SelectionChanged);
            // 
            // pnlControl
            // 
            this.pnlControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlControl.Controls.Add(this.btnXoa);
            this.pnlControl.Controls.Add(this.btnSua);
            this.pnlControl.Controls.Add(this.btnThem);
            this.pnlControl.Controls.Add(this.chkIsActive);
            this.pnlControl.Controls.Add(this.cboTrangThai);
            this.pnlControl.Controls.Add(this.lblTrangThai);
            this.pnlControl.Controls.Add(this.txtChiTiet);
            this.pnlControl.Controls.Add(this.lblChiTiet);
            this.pnlControl.Controls.Add(this.txtSuatAn);
            this.pnlControl.Controls.Add(this.lblSuatAn);
            this.pnlControl.Controls.Add(this.txtSoLuongTon);
            this.pnlControl.Controls.Add(this.lblSoLuongTon);
            this.pnlControl.Controls.Add(this.txtDonGia);
            this.pnlControl.Controls.Add(this.lblDonGia);
            this.pnlControl.Controls.Add(this.cboPhanLoai);
            this.pnlControl.Controls.Add(this.lblPhanLoai);
            this.pnlControl.Controls.Add(this.cboNhomDV);
            this.pnlControl.Controls.Add(this.lblNhomDV);
            this.pnlControl.Controls.Add(this.txtTenDV);
            this.pnlControl.Controls.Add(this.lblTenDV);
            this.pnlControl.Controls.Add(this.lblControlTitle);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControl.Location = new System.Drawing.Point(640, 15);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(370, 585);
            this.pnlControl.TabIndex = 1;
            // 
            // lblControlTitle
            // 
            this.lblControlTitle.AutoSize = true;
            this.lblControlTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblControlTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(211)))), ((int)(((byte)(153)))));
            this.lblControlTitle.Location = new System.Drawing.Point(15, 10);
            this.lblControlTitle.Name = "lblControlTitle";
            this.lblControlTitle.Size = new System.Drawing.Size(181, 21);
            this.lblControlTitle.TabIndex = 0;
            this.lblControlTitle.Text = "CHI TIẾT DỊCH VỤ";
            // 
            // lblTenDV
            // 
            this.lblTenDV.AutoSize = true;
            this.lblTenDV.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTenDV.ForeColor = System.Drawing.Color.White;
            this.lblTenDV.Location = new System.Drawing.Point(15, 40);
            this.lblTenDV.Name = "lblTenDV";
            this.lblTenDV.Size = new System.Drawing.Size(75, 15);
            this.lblTenDV.TabIndex = 1;
            this.lblTenDV.Text = "Tên Dịch Vụ:";
            // 
            // txtTenDV
            // 
            this.txtTenDV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtTenDV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenDV.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtTenDV.ForeColor = System.Drawing.Color.White;
            this.txtTenDV.Location = new System.Drawing.Point(15, 58);
            this.txtTenDV.Name = "txtTenDV";
            this.txtTenDV.Size = new System.Drawing.Size(340, 24);
            this.txtTenDV.TabIndex = 2;
            // 
            // lblNhomDV
            // 
            this.lblNhomDV.AutoSize = true;
            this.lblNhomDV.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNhomDV.ForeColor = System.Drawing.Color.White;
            this.lblNhomDV.Location = new System.Drawing.Point(15, 90);
            this.lblNhomDV.Name = "lblNhomDV";
            this.lblNhomDV.Size = new System.Drawing.Size(89, 15);
            this.lblNhomDV.TabIndex = 3;
            this.lblNhomDV.Text = "Nhóm Dịch Vụ:";
            // 
            // cboNhomDV
            // 
            this.cboNhomDV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboNhomDV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhomDV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboNhomDV.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboNhomDV.ForeColor = System.Drawing.Color.White;
            this.cboNhomDV.FormattingEnabled = true;
            this.cboNhomDV.Items.AddRange(new object[] {
            "Ăn uống tươi",
            "Thực phẩm khô",
            "Cho thuê xe",
            "Sảnh sự kiện",
            "Bãi đỗ xe",
            "Giặt ủi"});
            this.cboNhomDV.Location = new System.Drawing.Point(15, 108);
            this.cboNhomDV.Name = "cboNhomDV";
            this.cboNhomDV.Size = new System.Drawing.Size(160, 25);
            this.cboNhomDV.TabIndex = 4;
            // 
            // lblPhanLoai
            // 
            this.lblPhanLoai.AutoSize = true;
            this.lblPhanLoai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPhanLoai.ForeColor = System.Drawing.Color.White;
            this.lblPhanLoai.Location = new System.Drawing.Point(195, 90);
            this.lblPhanLoai.Name = "lblPhanLoai";
            this.lblPhanLoai.Size = new System.Drawing.Size(62, 15);
            this.lblPhanLoai.TabIndex = 5;
            this.lblPhanLoai.Text = "Phân Loại:";
            // 
            // cboPhanLoai
            // 
            this.cboPhanLoai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboPhanLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhanLoai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPhanLoai.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboPhanLoai.ForeColor = System.Drawing.Color.White;
            this.cboPhanLoai.FormattingEnabled = true;
            this.cboPhanLoai.Items.AddRange(new object[] {
            "Tự túc",
            "Ngoại"});
            this.cboPhanLoai.Location = new System.Drawing.Point(195, 108);
            this.cboPhanLoai.Name = "cboPhanLoai";
            this.cboPhanLoai.Size = new System.Drawing.Size(160, 25);
            this.cboPhanLoai.TabIndex = 6;
            // 
            // lblDonGia
            // 
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDonGia.ForeColor = System.Drawing.Color.White;
            this.lblDonGia.Location = new System.Drawing.Point(15, 140);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new System.Drawing.Size(86, 15);
            this.lblDonGia.TabIndex = 7;
            this.lblDonGia.Text = "Đơn Giá (VNĐ):";
            // 
            // txtDonGia
            // 
            this.txtDonGia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtDonGia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDonGia.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDonGia.ForeColor = System.Drawing.Color.White;
            this.txtDonGia.Location = new System.Drawing.Point(15, 158);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Size = new System.Drawing.Size(340, 24);
            this.txtDonGia.TabIndex = 8;
            // 
            // lblSoLuongTon
            // 
            this.lblSoLuongTon.AutoSize = true;
            this.lblSoLuongTon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSoLuongTon.ForeColor = System.Drawing.Color.White;
            this.lblSoLuongTon.Location = new System.Drawing.Point(15, 190);
            this.lblSoLuongTon.Name = "lblSoLuongTon";
            this.lblSoLuongTon.Size = new System.Drawing.Size(86, 15);
            this.lblSoLuongTon.TabIndex = 9;
            this.lblSoLuongTon.Text = "Số Lượng Tồn:";
            // 
            // txtSoLuongTon
            // 
            this.txtSoLuongTon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtSoLuongTon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoLuongTon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSoLuongTon.ForeColor = System.Drawing.Color.White;
            this.txtSoLuongTon.Location = new System.Drawing.Point(15, 208);
            this.txtSoLuongTon.Name = "txtSoLuongTon";
            this.txtSoLuongTon.Size = new System.Drawing.Size(160, 24);
            this.txtSoLuongTon.TabIndex = 10;
            // 
            // lblSuatAn
            // 
            this.lblSuatAn.AutoSize = true;
            this.lblSuatAn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSuatAn.ForeColor = System.Drawing.Color.White;
            this.lblSuatAn.Location = new System.Drawing.Point(195, 190);
            this.lblSuatAn.Name = "lblSuatAn";
            this.lblSuatAn.Size = new System.Drawing.Size(95, 15);
            this.lblSuatAn.TabIndex = 11;
            this.lblSuatAn.Text = "Suất Ăn/Ngày:";
            // 
            // txtSuatAn
            // 
            this.txtSuatAn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtSuatAn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSuatAn.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSuatAn.ForeColor = System.Drawing.Color.White;
            this.txtSuatAn.Location = new System.Drawing.Point(195, 208);
            this.txtSuatAn.Name = "txtSuatAn";
            this.txtSuatAn.Size = new System.Drawing.Size(160, 24);
            this.txtSuatAn.TabIndex = 12;
            // 
            // lblChiTiet
            // 
            this.lblChiTiet.AutoSize = true;
            this.lblChiTiet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblChiTiet.ForeColor = System.Drawing.Color.White;
            this.lblChiTiet.Location = new System.Drawing.Point(15, 240);
            this.lblChiTiet.Name = "lblChiTiet";
            this.lblChiTiet.Size = new System.Drawing.Size(117, 15);
            this.lblChiTiet.TabIndex = 13;
            this.lblChiTiet.Text = "Thông Tin Chi Tiết:";
            // 
            // txtChiTiet
            // 
            this.txtChiTiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtChiTiet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChiTiet.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtChiTiet.ForeColor = System.Drawing.Color.White;
            this.txtChiTiet.Location = new System.Drawing.Point(15, 258);
            this.txtChiTiet.Multiline = true;
            this.txtChiTiet.Name = "txtChiTiet";
            this.txtChiTiet.Size = new System.Drawing.Size(340, 50);
            this.txtChiTiet.TabIndex = 14;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTrangThai.ForeColor = System.Drawing.Color.White;
            this.lblTrangThai.Location = new System.Drawing.Point(15, 315);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(71, 15);
            this.lblTrangThai.TabIndex = 15;
            this.lblTrangThai.Text = "Trạng Thái:";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboTrangThai.ForeColor = System.Drawing.Color.White;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Sẵn sàng",
            "Đang cho thuê",
            "Dọn dẹp",
            "Bảo trì",
            "Phục vụ",
            "Tạm ngưng"});
            this.cboTrangThai.Location = new System.Drawing.Point(15, 333);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(180, 25);
            this.cboTrangThai.TabIndex = 16;
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Checked = true;
            this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chkIsActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(211)))), ((int)(((byte)(153)))));
            this.chkIsActive.Location = new System.Drawing.Point(215, 335);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(140, 21);
            this.chkIsActive.TabIndex = 17;
            this.chkIsActive.Text = "Kích hoạt dịch vụ";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(15, 380);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 35);
            this.btnThem.TabIndex = 18;
            this.btnThem.Text = "Thêm Dịch Vụ";
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
            this.btnSua.Location = new System.Drawing.Point(135, 380);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 35);
            this.btnSua.TabIndex = 19;
            this.btnSua.Text = "Cập Nhật";
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
            this.btnXoa.Location = new System.Drawing.Point(255, 380);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 35);
            this.btnXoa.TabIndex = 20;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // frmDichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(1040, 680);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDichVu";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Text = "Danh mục dịch vụ";
            this.Load += new System.EventHandler(this.frmDichVu_Load);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ComboBox cboFilterNhom;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.DataGridView dgvDichVu;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Label lblControlTitle;
        private System.Windows.Forms.Label lblTenDV;
        private System.Windows.Forms.TextBox txtTenDV;
        private System.Windows.Forms.Label lblNhomDV;
        private System.Windows.Forms.ComboBox cboNhomDV;
        private System.Windows.Forms.Label lblPhanLoai;
        private System.Windows.Forms.ComboBox cboPhanLoai;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.Label lblSoLuongTon;
        private System.Windows.Forms.TextBox txtSoLuongTon;
        private System.Windows.Forms.Label lblSuatAn;
        private System.Windows.Forms.TextBox txtSuatAn;
        private System.Windows.Forms.Label lblChiTiet;
        private System.Windows.Forms.TextBox txtChiTiet;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
    }
}