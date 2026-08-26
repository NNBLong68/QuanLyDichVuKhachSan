namespace QuanLyDichVuKhachSan
{
    partial class frmPhieuDichVu
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dgvPhieu = new System.Windows.Forms.DataGridView();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.lblControlTitle = new System.Windows.Forms.Label();
            this.lblMaCode = new System.Windows.Forms.Label();
            this.txtMaCode = new System.Windows.Forms.TextBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.lblPhong = new System.Windows.Forms.Label();
            this.cboPhong = new System.Windows.Forms.ComboBox();
            this.lblDichVu = new System.Windows.Forms.Label();
            this.cboDichVu = new System.Windows.Forms.ComboBox();
            this.lblDoiTac = new System.Windows.Forms.Label();
            this.cboDoiTac = new System.Windows.Forms.ComboBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.lblTienCoc = new System.Windows.Forms.Label();
            this.txtTienCoc = new System.Windows.Forms.TextBox();
            this.lblThuThem = new System.Windows.Forms.Label();
            this.txtThuThem = new System.Windows.Forms.TextBox();
            this.lblHinhThuc = new System.Windows.Forms.Label();
            this.cboHinhThuc = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnTaoPhieu = new System.Windows.Forms.Button();
            this.btnHoanTat = new System.Windows.Forms.Button();
            this.btnHuyPhieu = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieu)).BeginInit();
            this.pnlControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.dgvPhieu);
            this.pnlMain.Controls.Add(this.pnlControl);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(15, 15);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1010, 650);
            this.pnlMain.TabIndex = 0;
            // 
            // dgvPhieu
            // 
            this.dgvPhieu.AllowUserToAddRows = false;
            this.dgvPhieu.AllowUserToDeleteRows = false;
            this.dgvPhieu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhieu.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dgvPhieu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPhieu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhieu.Location = new System.Drawing.Point(0, 0);
            this.dgvPhieu.MultiSelect = false;
            this.dgvPhieu.Name = "dgvPhieu";
            this.dgvPhieu.ReadOnly = true;
            this.dgvPhieu.RowHeadersVisible = false;
            this.dgvPhieu.RowTemplate.Height = 32;
            this.dgvPhieu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhieu.Size = new System.Drawing.Size(630, 650);
            this.dgvPhieu.TabIndex = 0;
            this.dgvPhieu.SelectionChanged += new System.EventHandler(this.dgvPhieu_SelectionChanged);
            // 
            // pnlControl
            // 
            this.pnlControl.AutoScroll = true;
            this.pnlControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlControl.Controls.Add(this.btnHuyPhieu);
            this.pnlControl.Controls.Add(this.btnHoanTat);
            this.pnlControl.Controls.Add(this.btnTaoPhieu);
            this.pnlControl.Controls.Add(this.txtGhiChu);
            this.pnlControl.Controls.Add(this.lblGhiChu);
            this.pnlControl.Controls.Add(this.cboTrangThai);
            this.pnlControl.Controls.Add(this.lblTrangThai);
            this.pnlControl.Controls.Add(this.cboHinhThuc);
            this.pnlControl.Controls.Add(this.lblHinhThuc);
            this.pnlControl.Controls.Add(this.txtThuThem);
            this.pnlControl.Controls.Add(this.lblThuThem);
            this.pnlControl.Controls.Add(this.txtTienCoc);
            this.pnlControl.Controls.Add(this.lblTienCoc);
            this.pnlControl.Controls.Add(this.txtDonGia);
            this.pnlControl.Controls.Add(this.lblDonGia);
            this.pnlControl.Controls.Add(this.txtSoLuong);
            this.pnlControl.Controls.Add(this.lblSoLuong);
            this.pnlControl.Controls.Add(this.cboDoiTac);
            this.pnlControl.Controls.Add(this.lblDoiTac);
            this.pnlControl.Controls.Add(this.cboDichVu);
            this.pnlControl.Controls.Add(this.lblDichVu);
            this.pnlControl.Controls.Add(this.cboPhong);
            this.pnlControl.Controls.Add(this.lblPhong);
            this.pnlControl.Controls.Add(this.cboKhachHang);
            this.pnlControl.Controls.Add(this.lblKhachHang);
            this.pnlControl.Controls.Add(this.txtMaCode);
            this.pnlControl.Controls.Add(this.lblMaCode);
            this.pnlControl.Controls.Add(this.lblControlTitle);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControl.Location = new System.Drawing.Point(640, 0);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(370, 650);
            this.pnlControl.TabIndex = 1;
            // 
            // lblControlTitle
            // 
            this.lblControlTitle.AutoSize = true;
            this.lblControlTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblControlTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(211)))), ((int)(((byte)(153)))));
            this.lblControlTitle.Location = new System.Drawing.Point(15, 10);
            this.lblControlTitle.Name = "lblControlTitle";
            this.lblControlTitle.Size = new System.Drawing.Size(183, 21);
            this.lblControlTitle.TabIndex = 0;
            this.lblControlTitle.Text = "LẬP PHIẾU DỊCH VỤ";
            // 
            // lblMaCode
            // 
            this.lblMaCode.AutoSize = true;
            this.lblMaCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMaCode.ForeColor = System.Drawing.Color.White;
            this.lblMaCode.Location = new System.Drawing.Point(15, 40);
            this.lblMaCode.Name = "lblMaCode";
            this.lblMaCode.Size = new System.Drawing.Size(92, 15);
            this.lblMaCode.TabIndex = 1;
            this.lblMaCode.Text = "Mã Phiếu Code:";
            // 
            // txtMaCode
            // 
            this.txtMaCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtMaCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaCode.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMaCode.ForeColor = System.Drawing.Color.White;
            this.txtMaCode.Location = new System.Drawing.Point(15, 58);
            this.txtMaCode.Name = "txtMaCode";
            this.txtMaCode.Size = new System.Drawing.Size(340, 24);
            this.txtMaCode.TabIndex = 2;
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblKhachHang.ForeColor = System.Drawing.Color.White;
            this.lblKhachHang.Location = new System.Drawing.Point(15, 88);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(77, 15);
            this.lblKhachHang.TabIndex = 3;
            this.lblKhachHang.Text = "Khách Hàng:";
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboKhachHang.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboKhachHang.ForeColor = System.Drawing.Color.White;
            this.cboKhachHang.FormattingEnabled = true;
            this.cboKhachHang.Location = new System.Drawing.Point(15, 106);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(340, 25);
            this.cboKhachHang.TabIndex = 4;
            // 
            // lblPhong
            // 
            this.lblPhong.AutoSize = true;
            this.lblPhong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPhong.ForeColor = System.Drawing.Color.White;
            this.lblPhong.Location = new System.Drawing.Point(15, 138);
            this.lblPhong.Name = "lblPhong";
            this.lblPhong.Size = new System.Drawing.Size(68, 15);
            this.lblPhong.TabIndex = 5;
            this.lblPhong.Text = "Phòng (nếu có):";
            // 
            // cboPhong
            // 
            this.cboPhong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPhong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboPhong.ForeColor = System.Drawing.Color.White;
            this.cboPhong.FormattingEnabled = true;
            this.cboPhong.Location = new System.Drawing.Point(15, 156);
            this.cboPhong.Name = "cboPhong";
            this.cboPhong.Size = new System.Drawing.Size(340, 25);
            this.cboPhong.TabIndex = 6;
            // 
            // lblDichVu
            // 
            this.lblDichVu.AutoSize = true;
            this.lblDichVu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDichVu.ForeColor = System.Drawing.Color.White;
            this.lblDichVu.Location = new System.Drawing.Point(15, 188);
            this.lblDichVu.Name = "lblDichVu";
            this.lblDichVu.Size = new System.Drawing.Size(89, 15);
            this.lblDichVu.TabIndex = 7;
            this.lblDichVu.Text = "Chọn Dịch Vụ:";
            // 
            // cboDichVu
            // 
            this.cboDichVu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboDichVu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDichVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDichVu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboDichVu.ForeColor = System.Drawing.Color.White;
            this.cboDichVu.FormattingEnabled = true;
            this.cboDichVu.Location = new System.Drawing.Point(15, 206);
            this.cboDichVu.Name = "cboDichVu";
            this.cboDichVu.Size = new System.Drawing.Size(340, 25);
            this.cboDichVu.TabIndex = 8;
            this.cboDichVu.SelectedIndexChanged += new System.EventHandler(this.cboDichVu_SelectedIndexChanged);
            // 
            // lblDoiTac
            // 
            this.lblDoiTac.AutoSize = true;
            this.lblDoiTac.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDoiTac.ForeColor = System.Drawing.Color.White;
            this.lblDoiTac.Location = new System.Drawing.Point(15, 238);
            this.lblDoiTac.Name = "lblDoiTac";
            this.lblDoiTac.Size = new System.Drawing.Size(127, 15);
            this.lblDoiTac.TabIndex = 9;
            this.lblDoiTac.Text = "Đối Tác Giặt Ủi (nếu có):";
            // 
            // cboDoiTac
            // 
            this.cboDoiTac.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboDoiTac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoiTac.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDoiTac.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboDoiTac.ForeColor = System.Drawing.Color.White;
            this.cboDoiTac.FormattingEnabled = true;
            this.cboDoiTac.Location = new System.Drawing.Point(15, 256);
            this.cboDoiTac.Name = "cboDoiTac";
            this.cboDoiTac.Size = new System.Drawing.Size(340, 25);
            this.cboDoiTac.TabIndex = 10;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSoLuong.ForeColor = System.Drawing.Color.White;
            this.lblSoLuong.Location = new System.Drawing.Point(15, 288);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(117, 15);
            this.lblSoLuong.TabIndex = 11;
            this.lblSoLuong.Text = "Số Lượng / Kg / Ngày:";
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtSoLuong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSoLuong.ForeColor = System.Drawing.Color.White;
            this.txtSoLuong.Location = new System.Drawing.Point(15, 306);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(160, 24);
            this.txtSoLuong.TabIndex = 12;
            this.txtSoLuong.Text = "1";
            // 
            // lblDonGia
            // 
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDonGia.ForeColor = System.Drawing.Color.White;
            this.lblDonGia.Location = new System.Drawing.Point(195, 288);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new System.Drawing.Size(86, 15);
            this.lblDonGia.TabIndex = 13;
            this.lblDonGia.Text = "Đơn Giá (VNĐ):";
            // 
            // txtDonGia
            // 
            this.txtDonGia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtDonGia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDonGia.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDonGia.ForeColor = System.Drawing.Color.White;
            this.txtDonGia.Location = new System.Drawing.Point(195, 306);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Size = new System.Drawing.Size(160, 24);
            this.txtDonGia.TabIndex = 14;
            // 
            // lblTienCoc
            // 
            this.lblTienCoc.AutoSize = true;
            this.lblTienCoc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTienCoc.ForeColor = System.Drawing.Color.White;
            this.lblTienCoc.Location = new System.Drawing.Point(15, 338);
            this.lblTienCoc.Name = "lblTienCoc";
            this.lblTienCoc.Size = new System.Drawing.Size(91, 15);
            this.lblTienCoc.TabIndex = 15;
            this.lblTienCoc.Text = "Tiền Cọc (VNĐ):";
            // 
            // txtTienCoc
            // 
            this.txtTienCoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtTienCoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTienCoc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtTienCoc.ForeColor = System.Drawing.Color.White;
            this.txtTienCoc.Location = new System.Drawing.Point(15, 356);
            this.txtTienCoc.Name = "txtTienCoc";
            this.txtTienCoc.Size = new System.Drawing.Size(160, 24);
            this.txtTienCoc.TabIndex = 16;
            this.txtTienCoc.Text = "0";
            // 
            // lblThuThem
            // 
            this.lblThuThem.AutoSize = true;
            this.lblThuThem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblThuThem.ForeColor = System.Drawing.Color.White;
            this.lblThuThem.Location = new System.Drawing.Point(195, 338);
            this.lblThuThem.Name = "lblThuThem";
            this.lblThuThem.Size = new System.Drawing.Size(117, 15);
            this.lblThuThem.TabIndex = 17;
            this.lblThuThem.Text = "Thu Thêm Phát Sinh:";
            // 
            // txtThuThem
            // 
            this.txtThuThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtThuThem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtThuThem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtThuThem.ForeColor = System.Drawing.Color.White;
            this.txtThuThem.Location = new System.Drawing.Point(195, 356);
            this.txtThuThem.Name = "txtThuThem";
            this.txtThuThem.Size = new System.Drawing.Size(160, 24);
            this.txtThuThem.TabIndex = 18;
            this.txtThuThem.Text = "0";
            // 
            // lblHinhThuc
            // 
            this.lblHinhThuc.AutoSize = true;
            this.lblHinhThuc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHinhThuc.ForeColor = System.Drawing.Color.White;
            this.lblHinhThuc.Location = new System.Drawing.Point(15, 388);
            this.lblHinhThuc.Name = "lblHinhThuc";
            this.lblHinhThuc.Size = new System.Drawing.Size(127, 15);
            this.lblHinhThuc.TabIndex = 19;
            this.lblHinhThuc.Text = "Hình Thức Tính Tiền:";
            // 
            // cboHinhThuc
            // 
            this.cboHinhThuc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboHinhThuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHinhThuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboHinhThuc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboHinhThuc.ForeColor = System.Drawing.Color.White;
            this.cboHinhThuc.FormattingEnabled = true;
            this.cboHinhThuc.Items.AddRange(new object[] {
            "Hóa đơn riêng",
            "Tính vào tiền phòng"});
            this.cboHinhThuc.Location = new System.Drawing.Point(15, 406);
            this.cboHinhThuc.Name = "cboHinhThuc";
            this.cboHinhThuc.Size = new System.Drawing.Size(160, 25);
            this.cboHinhThuc.TabIndex = 20;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTrangThai.ForeColor = System.Drawing.Color.White;
            this.lblTrangThai.Location = new System.Drawing.Point(195, 388);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(71, 15);
            this.lblTrangThai.TabIndex = 21;
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
            "Đang sử dụng",
            "Đã hoàn tất",
            "Đã hủy"});
            this.cboTrangThai.Location = new System.Drawing.Point(195, 406);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(160, 25);
            this.cboTrangThai.TabIndex = 22;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblGhiChu.ForeColor = System.Drawing.Color.White;
            this.lblGhiChu.Location = new System.Drawing.Point(15, 438);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(107, 15);
            this.lblGhiChu.TabIndex = 23;
            this.lblGhiChu.Text = "Ghi Chú Chi Tiết:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtGhiChu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtGhiChu.ForeColor = System.Drawing.Color.White;
            this.txtGhiChu.Location = new System.Drawing.Point(15, 456);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(340, 50);
            this.txtGhiChu.TabIndex = 24;
            // 
            // btnTaoPhieu
            // 
            this.btnTaoPhieu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnTaoPhieu.FlatAppearance.BorderSize = 0;
            this.btnTaoPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaoPhieu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTaoPhieu.ForeColor = System.Drawing.Color.White;
            this.btnTaoPhieu.Location = new System.Drawing.Point(15, 520);
            this.btnTaoPhieu.Name = "btnTaoPhieu";
            this.btnTaoPhieu.Size = new System.Drawing.Size(100, 35);
            this.btnTaoPhieu.TabIndex = 25;
            this.btnTaoPhieu.Text = "Tạo Phiếu";
            this.btnTaoPhieu.UseVisualStyleBackColor = false;
            this.btnTaoPhieu.Click += new System.EventHandler(this.btnTaoPhieu_Click);
            // 
            // btnHoanTat
            // 
            this.btnHoanTat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnHoanTat.FlatAppearance.BorderSize = 0;
            this.btnHoanTat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoanTat.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnHoanTat.ForeColor = System.Drawing.Color.White;
            this.btnHoanTat.Location = new System.Drawing.Point(130, 520);
            this.btnHoanTat.Name = "btnHoanTat";
            this.btnHoanTat.Size = new System.Drawing.Size(110, 35);
            this.btnHoanTat.TabIndex = 26;
            this.btnHoanTat.Text = "Cập Nhật";
            this.btnHoanTat.UseVisualStyleBackColor = false;
            this.btnHoanTat.Click += new System.EventHandler(this.btnHoanTat_Click);
            // 
            // btnHuyPhieu
            // 
            this.btnHuyPhieu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnHuyPhieu.FlatAppearance.BorderSize = 0;
            this.btnHuyPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuyPhieu.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnHuyPhieu.ForeColor = System.Drawing.Color.White;
            this.btnHuyPhieu.Location = new System.Drawing.Point(255, 520);
            this.btnHuyPhieu.Name = "btnHuyPhieu";
            this.btnHuyPhieu.Size = new System.Drawing.Size(100, 35);
            this.btnHuyPhieu.TabIndex = 27;
            this.btnHuyPhieu.Text = "Hủy Phiếu";
            this.btnHuyPhieu.UseVisualStyleBackColor = false;
            this.btnHuyPhieu.Click += new System.EventHandler(this.btnHuyPhieu_Click);
            // 
            // frmPhieuDichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(1040, 680);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPhieuDichVu";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Text = "Phiếu dịch vụ";
            this.Load += new System.EventHandler(this.frmPhieuDichVu_Load);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieu)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.DataGridView dgvPhieu;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Label lblControlTitle;
        private System.Windows.Forms.Label lblMaCode;
        private System.Windows.Forms.TextBox txtMaCode;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.Label lblPhong;
        private System.Windows.Forms.ComboBox cboPhong;
        private System.Windows.Forms.Label lblDichVu;
        private System.Windows.Forms.ComboBox cboDichVu;
        private System.Windows.Forms.Label lblDoiTac;
        private System.Windows.Forms.ComboBox cboDoiTac;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.Label lblTienCoc;
        private System.Windows.Forms.TextBox txtTienCoc;
        private System.Windows.Forms.Label lblThuThem;
        private System.Windows.Forms.TextBox txtThuThem;
        private System.Windows.Forms.Label lblHinhThuc;
        private System.Windows.Forms.ComboBox cboHinhThuc;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnTaoPhieu;
        private System.Windows.Forms.Button btnHoanTat;
        private System.Windows.Forms.Button btnHuyPhieu;
    }
}
