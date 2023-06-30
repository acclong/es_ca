namespace ES.CA_ManagementUI
{
    partial class frmThemSuaUserCert
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
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlLienKet = new System.Windows.Forms.Panel();
            this.grbInfUser = new System.Windows.Forms.GroupBox();
            this.chkVaildTo = new System.Windows.Forms.CheckBox();
            this.dpkValidTo = new System.Windows.Forms.DateTimePicker();
            this.dpkValidFrom = new System.Windows.Forms.DateTimePicker();
            this.cboCertType = new System.Windows.Forms.ComboBox();
            this.lblValidFrom = new System.Windows.Forms.Label();
            this.lblValidTo = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.grbInfUserCert = new System.Windows.Forms.GroupBox();
            this.txtID_UserCert = new System.Windows.Forms.TextBox();
            this.lblUserCertID = new System.Windows.Forms.Label();
            this.cboCert = new System.Windows.Forms.ComboBox();
            this.cboUserName = new System.Windows.Forms.ComboBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblCert = new System.Windows.Forms.Label();
            this.splCertUser = new System.Windows.Forms.SplitContainer();
            this.grbUser = new System.Windows.Forms.GroupBox();
            this.rlvUser = new Telerik.WinControls.UI.RadListView();
            this.rbarUser = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement1 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.commandBarLabel1 = new Telerik.WinControls.UI.CommandBarLabel();
            this.drpUserGroup = new Telerik.WinControls.UI.CommandBarDropDownList();
            this.commandBarSeparator1 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarLabel2 = new Telerik.WinControls.UI.CommandBarLabel();
            this.tbUserFilter = new Telerik.WinControls.UI.CommandBarTextBox();
            this.grbCert = new System.Windows.Forms.GroupBox();
            this.rlvCert = new Telerik.WinControls.UI.RadListView();
            this.rbarCert = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement2 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement2 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.cmmbarllbNhom = new Telerik.WinControls.UI.CommandBarLabel();
            this.drpCertGroup = new Telerik.WinControls.UI.CommandBarDropDownList();
            this.commandBarSeparator4 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.cmmbarlblLoc = new Telerik.WinControls.UI.CommandBarLabel();
            this.txtCertFilter = new Telerik.WinControls.UI.CommandBarTextBox();
            this.pnlFooter.SuspendLayout();
            this.pnlLienKet.SuspendLayout();
            this.grbInfUser.SuspendLayout();
            this.grbInfUserCert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splCertUser)).BeginInit();
            this.splCertUser.Panel1.SuspendLayout();
            this.splCertUser.Panel2.SuspendLayout();
            this.splCertUser.SuspendLayout();
            this.grbUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarUser)).BeginInit();
            this.grbCert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvCert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarCert)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnExit);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 526);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(301, 40);
            this.pnlFooter.TabIndex = 11;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(154, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 36;
            this.btnExit.Text = "Đóng";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(71, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 35;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlLienKet
            // 
            this.pnlLienKet.Controls.Add(this.grbInfUser);
            this.pnlLienKet.Controls.Add(this.pnlFooter);
            this.pnlLienKet.Controls.Add(this.grbInfUserCert);
            this.pnlLienKet.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlLienKet.Location = new System.Drawing.Point(683, 0);
            this.pnlLienKet.Name = "pnlLienKet";
            this.pnlLienKet.Size = new System.Drawing.Size(301, 566);
            this.pnlLienKet.TabIndex = 57;
            // 
            // grbInfUser
            // 
            this.grbInfUser.Controls.Add(this.chkVaildTo);
            this.grbInfUser.Controls.Add(this.dpkValidTo);
            this.grbInfUser.Controls.Add(this.dpkValidFrom);
            this.grbInfUser.Controls.Add(this.cboCertType);
            this.grbInfUser.Controls.Add(this.lblValidFrom);
            this.grbInfUser.Controls.Add(this.lblValidTo);
            this.grbInfUser.Controls.Add(this.lblType);
            this.grbInfUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbInfUser.Location = new System.Drawing.Point(0, 104);
            this.grbInfUser.Name = "grbInfUser";
            this.grbInfUser.Size = new System.Drawing.Size(301, 109);
            this.grbInfUser.TabIndex = 13;
            this.grbInfUser.TabStop = false;
            this.grbInfUser.Text = "Thông tin quản lý";
            // 
            // chkVaildTo
            // 
            this.chkVaildTo.AutoSize = true;
            this.chkVaildTo.Location = new System.Drawing.Point(119, 80);
            this.chkVaildTo.Name = "chkVaildTo";
            this.chkVaildTo.Size = new System.Drawing.Size(15, 14);
            this.chkVaildTo.TabIndex = 51;
            this.chkVaildTo.UseVisualStyleBackColor = true;
            this.chkVaildTo.CheckedChanged += new System.EventHandler(this.chkVaildTo_CheckedChanged);
            // 
            // dpkValidTo
            // 
            this.dpkValidTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dpkValidTo.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dpkValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkValidTo.Location = new System.Drawing.Point(144, 76);
            this.dpkValidTo.Name = "dpkValidTo";
            this.dpkValidTo.Size = new System.Drawing.Size(145, 20);
            this.dpkValidTo.TabIndex = 36;
            this.dpkValidTo.Visible = false;
            // 
            // dpkValidFrom
            // 
            this.dpkValidFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dpkValidFrom.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dpkValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkValidFrom.Location = new System.Drawing.Point(120, 48);
            this.dpkValidFrom.Name = "dpkValidFrom";
            this.dpkValidFrom.Size = new System.Drawing.Size(169, 20);
            this.dpkValidFrom.TabIndex = 35;
            // 
            // cboCertType
            // 
            this.cboCertType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCertType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCertType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboCertType.FormattingEnabled = true;
            this.cboCertType.Location = new System.Drawing.Point(119, 19);
            this.cboCertType.Name = "cboCertType";
            this.cboCertType.Size = new System.Drawing.Size(169, 21);
            this.cboCertType.TabIndex = 37;
            // 
            // lblValidFrom
            // 
            this.lblValidFrom.AutoSize = true;
            this.lblValidFrom.Location = new System.Drawing.Point(13, 52);
            this.lblValidFrom.Name = "lblValidFrom";
            this.lblValidFrom.Size = new System.Drawing.Size(87, 13);
            this.lblValidFrom.TabIndex = 32;
            this.lblValidFrom.Text = "Ngày có hiệu lực";
            // 
            // lblValidTo
            // 
            this.lblValidTo.AutoSize = true;
            this.lblValidTo.Location = new System.Drawing.Point(13, 80);
            this.lblValidTo.Name = "lblValidTo";
            this.lblValidTo.Size = new System.Drawing.Size(90, 13);
            this.lblValidTo.TabIndex = 33;
            this.lblValidTo.Text = "Ngày hết hiệu lực";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(13, 23);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(78, 13);
            this.lblType.TabIndex = 34;
            this.lblType.Text = "Loại chứng thư";
            // 
            // grbInfUserCert
            // 
            this.grbInfUserCert.Controls.Add(this.txtID_UserCert);
            this.grbInfUserCert.Controls.Add(this.lblUserCertID);
            this.grbInfUserCert.Controls.Add(this.cboCert);
            this.grbInfUserCert.Controls.Add(this.cboUserName);
            this.grbInfUserCert.Controls.Add(this.lblUserName);
            this.grbInfUserCert.Controls.Add(this.lblCert);
            this.grbInfUserCert.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbInfUserCert.Location = new System.Drawing.Point(0, 0);
            this.grbInfUserCert.Name = "grbInfUserCert";
            this.grbInfUserCert.Size = new System.Drawing.Size(301, 104);
            this.grbInfUserCert.TabIndex = 11;
            this.grbInfUserCert.TabStop = false;
            this.grbInfUserCert.Text = "Thông tin liên kết";
            // 
            // txtID_UserCert
            // 
            this.txtID_UserCert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID_UserCert.Location = new System.Drawing.Point(119, 17);
            this.txtID_UserCert.Name = "txtID_UserCert";
            this.txtID_UserCert.ReadOnly = true;
            this.txtID_UserCert.Size = new System.Drawing.Size(105, 20);
            this.txtID_UserCert.TabIndex = 42;
            // 
            // lblUserCertID
            // 
            this.lblUserCertID.AutoSize = true;
            this.lblUserCertID.Location = new System.Drawing.Point(13, 20);
            this.lblUserCertID.Name = "lblUserCertID";
            this.lblUserCertID.Size = new System.Drawing.Size(18, 13);
            this.lblUserCertID.TabIndex = 41;
            this.lblUserCertID.Text = "ID";
            // 
            // cboCert
            // 
            this.cboCert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCert.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboCert.FormattingEnabled = true;
            this.cboCert.Location = new System.Drawing.Point(119, 70);
            this.cboCert.Name = "cboCert";
            this.cboCert.Size = new System.Drawing.Size(169, 21);
            this.cboCert.TabIndex = 39;
            // 
            // cboUserName
            // 
            this.cboUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboUserName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUserName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboUserName.FormattingEnabled = true;
            this.cboUserName.Location = new System.Drawing.Point(119, 43);
            this.cboUserName.Name = "cboUserName";
            this.cboUserName.Size = new System.Drawing.Size(169, 21);
            this.cboUserName.TabIndex = 40;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(13, 73);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(56, 13);
            this.lblUserName.TabIndex = 36;
            this.lblUserName.Text = "Chứng thư";
            // 
            // lblCert
            // 
            this.lblCert.AutoSize = true;
            this.lblCert.Location = new System.Drawing.Point(13, 46);
            this.lblCert.Name = "lblCert";
            this.lblCert.Size = new System.Drawing.Size(62, 13);
            this.lblCert.TabIndex = 37;
            this.lblCert.Text = "Người dùng";
            // 
            // splCertUser
            // 
            this.splCertUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splCertUser.Location = new System.Drawing.Point(0, 0);
            this.splCertUser.Name = "splCertUser";
            // 
            // splCertUser.Panel1
            // 
            this.splCertUser.Panel1.Controls.Add(this.grbUser);
            this.splCertUser.Panel1MinSize = 50;
            // 
            // splCertUser.Panel2
            // 
            this.splCertUser.Panel2.Controls.Add(this.grbCert);
            this.splCertUser.Panel2MinSize = 50;
            this.splCertUser.Size = new System.Drawing.Size(683, 566);
            this.splCertUser.SplitterDistance = 340;
            this.splCertUser.TabIndex = 58;
            // 
            // grbUser
            // 
            this.grbUser.Controls.Add(this.rlvUser);
            this.grbUser.Controls.Add(this.rbarUser);
            this.grbUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbUser.Location = new System.Drawing.Point(0, 0);
            this.grbUser.Name = "grbUser";
            this.grbUser.Size = new System.Drawing.Size(340, 566);
            this.grbUser.TabIndex = 54;
            this.grbUser.TabStop = false;
            this.grbUser.Text = "Người dùng chứng thư số";
            // 
            // rlvUser
            // 
            this.rlvUser.AllowColumnReorder = false;
            this.rlvUser.AllowColumnResize = false;
            this.rlvUser.AllowEdit = false;
            this.rlvUser.AllowRemove = false;
            this.rlvUser.AutoScroll = true;
            this.rlvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rlvUser.Location = new System.Drawing.Point(3, 46);
            this.rlvUser.Name = "rlvUser";
            this.rlvUser.Size = new System.Drawing.Size(334, 517);
            this.rlvUser.TabIndex = 1;
            this.rlvUser.Text = "radListView1";
            // 
            // rbarUser
            // 
            this.rbarUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbarUser.Location = new System.Drawing.Point(3, 16);
            this.rbarUser.Name = "rbarUser";
            this.rbarUser.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement1});
            this.rbarUser.Size = new System.Drawing.Size(334, 30);
            this.rbarUser.TabIndex = 0;
            this.rbarUser.Text = "radCommandBar1";
            // 
            // commandBarRowElement1
            // 
            this.commandBarRowElement1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarRowElement1.MinSize = new System.Drawing.Size(25, 25);
            this.commandBarRowElement1.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.commandBarStripElement1});
            this.commandBarRowElement1.Text = "";
            this.commandBarRowElement1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // commandBarStripElement1
            // 
            this.commandBarStripElement1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement1.DisplayName = "commandBarStripElement1";
            this.commandBarStripElement1.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.commandBarLabel1,
            this.drpUserGroup,
            this.commandBarSeparator1,
            this.commandBarLabel2,
            this.tbUserFilter});
            this.commandBarStripElement1.Name = "commandBarStripElement1";
            // 
            // 
            // 
            this.commandBarStripElement1.OverflowButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            this.commandBarStripElement1.StretchHorizontally = true;
            this.commandBarStripElement1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            ((Telerik.WinControls.UI.RadCommandBarOverflowButton)(this.commandBarStripElement1.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // commandBarLabel1
            // 
            this.commandBarLabel1.AccessibleDescription = "Nhóm:";
            this.commandBarLabel1.AccessibleName = "Nhóm:";
            this.commandBarLabel1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarLabel1.DisplayName = "commandBarLabel1";
            this.commandBarLabel1.Name = "commandBarLabel1";
            this.commandBarLabel1.Text = "Nhóm:";
            this.commandBarLabel1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // drpUserGroup
            // 
            this.drpUserGroup.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.drpUserGroup.DisplayName = "commandBarDropDownList1";
            this.drpUserGroup.DropDownAnimationEnabled = true;
            this.drpUserGroup.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpUserGroup.MaxDropDownItems = 0;
            this.drpUserGroup.Name = "drpUserGroup";
            this.drpUserGroup.Text = "";
            this.drpUserGroup.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // commandBarSeparator1
            // 
            this.commandBarSeparator1.AccessibleDescription = "commandBarSeparator1";
            this.commandBarSeparator1.AccessibleName = "commandBarSeparator1";
            this.commandBarSeparator1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarSeparator1.DisplayName = "commandBarSeparator1";
            this.commandBarSeparator1.Name = "commandBarSeparator1";
            this.commandBarSeparator1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarSeparator1.VisibleInOverflowMenu = false;
            // 
            // commandBarLabel2
            // 
            this.commandBarLabel2.AccessibleDescription = "Lọc:";
            this.commandBarLabel2.AccessibleName = "Lọc:";
            this.commandBarLabel2.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarLabel2.DisplayName = "commandBarLabel2";
            this.commandBarLabel2.Name = "commandBarLabel2";
            this.commandBarLabel2.Text = "Lọc:";
            this.commandBarLabel2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // tbUserFilter
            // 
            this.tbUserFilter.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.tbUserFilter.DisplayName = "commandBarTextBox1";
            this.tbUserFilter.Name = "tbUserFilter";
            this.tbUserFilter.StretchHorizontally = true;
            this.tbUserFilter.Text = "";
            this.tbUserFilter.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // grbCert
            // 
            this.grbCert.Controls.Add(this.rlvCert);
            this.grbCert.Controls.Add(this.rbarCert);
            this.grbCert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbCert.Location = new System.Drawing.Point(0, 0);
            this.grbCert.Name = "grbCert";
            this.grbCert.Size = new System.Drawing.Size(339, 566);
            this.grbCert.TabIndex = 2;
            this.grbCert.TabStop = false;
            this.grbCert.Text = "Chứng thư số";
            // 
            // rlvCert
            // 
            this.rlvCert.AllowColumnReorder = false;
            this.rlvCert.AllowColumnResize = false;
            this.rlvCert.AllowEdit = false;
            this.rlvCert.AllowRemove = false;
            this.rlvCert.AutoScroll = true;
            this.rlvCert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rlvCert.Location = new System.Drawing.Point(3, 46);
            this.rlvCert.Name = "rlvCert";
            this.rlvCert.Size = new System.Drawing.Size(333, 517);
            this.rlvCert.TabIndex = 59;
            this.rlvCert.Text = "radListView1";
            // 
            // rbarCert
            // 
            this.rbarCert.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbarCert.Location = new System.Drawing.Point(3, 16);
            this.rbarCert.Name = "rbarCert";
            this.rbarCert.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement2});
            this.rbarCert.Size = new System.Drawing.Size(333, 30);
            this.rbarCert.TabIndex = 58;
            this.rbarCert.Text = "radCommandBar1";
            // 
            // commandBarRowElement2
            // 
            this.commandBarRowElement2.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarRowElement2.MinSize = new System.Drawing.Size(25, 25);
            this.commandBarRowElement2.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.commandBarStripElement2});
            this.commandBarRowElement2.Text = "";
            this.commandBarRowElement2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // commandBarStripElement2
            // 
            this.commandBarStripElement2.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement2.DisplayName = "commandBarStripElement1";
            this.commandBarStripElement2.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.cmmbarllbNhom,
            this.drpCertGroup,
            this.commandBarSeparator4,
            this.cmmbarlblLoc,
            this.txtCertFilter});
            this.commandBarStripElement2.Name = "commandBarStripElement1";
            // 
            // 
            // 
            this.commandBarStripElement2.OverflowButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            this.commandBarStripElement2.StretchHorizontally = true;
            this.commandBarStripElement2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            ((Telerik.WinControls.UI.RadCommandBarOverflowButton)(this.commandBarStripElement2.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // cmmbarllbNhom
            // 
            this.cmmbarllbNhom.AccessibleDescription = "Nhóm:";
            this.cmmbarllbNhom.AccessibleName = "Nhóm:";
            this.cmmbarllbNhom.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.cmmbarllbNhom.DisplayName = "commandBarLabel1";
            this.cmmbarllbNhom.Name = "cmmbarllbNhom";
            this.cmmbarllbNhom.Text = "Nhóm:";
            this.cmmbarllbNhom.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // drpCertGroup
            // 
            this.drpCertGroup.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.drpCertGroup.DisplayName = "commandBarDropDownList1";
            this.drpCertGroup.DropDownAnimationEnabled = true;
            this.drpCertGroup.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpCertGroup.MaxDropDownItems = 0;
            this.drpCertGroup.Name = "drpCertGroup";
            this.drpCertGroup.Text = "";
            this.drpCertGroup.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // commandBarSeparator4
            // 
            this.commandBarSeparator4.AccessibleDescription = "commandBarSeparator1";
            this.commandBarSeparator4.AccessibleName = "commandBarSeparator1";
            this.commandBarSeparator4.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarSeparator4.DisplayName = "commandBarSeparator1";
            this.commandBarSeparator4.Name = "commandBarSeparator4";
            this.commandBarSeparator4.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarSeparator4.VisibleInOverflowMenu = false;
            // 
            // cmmbarlblLoc
            // 
            this.cmmbarlblLoc.AccessibleDescription = "Lọc:";
            this.cmmbarlblLoc.AccessibleName = "Lọc:";
            this.cmmbarlblLoc.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.cmmbarlblLoc.DisplayName = "commandBarLabel2";
            this.cmmbarlblLoc.Name = "cmmbarlblLoc";
            this.cmmbarlblLoc.Text = "Lọc:";
            this.cmmbarlblLoc.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // txtCertFilter
            // 
            this.txtCertFilter.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.txtCertFilter.DisplayName = "commandBarTextBox1";
            this.txtCertFilter.Name = "txtCertFilter";
            this.txtCertFilter.StretchHorizontally = true;
            this.txtCertFilter.Text = "";
            this.txtCertFilter.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // frmThemSuaUserCert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 566);
            this.Controls.Add(this.splCertUser);
            this.Controls.Add(this.pnlLienKet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmThemSuaUserCert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật liên kết người dùng - Certificate";
            this.Load += new System.EventHandler(this.frmThemSuaUserDonVi_Load);
            this.pnlFooter.ResumeLayout(false);
            this.pnlLienKet.ResumeLayout(false);
            this.grbInfUser.ResumeLayout(false);
            this.grbInfUser.PerformLayout();
            this.grbInfUserCert.ResumeLayout(false);
            this.grbInfUserCert.PerformLayout();
            this.splCertUser.Panel1.ResumeLayout(false);
            this.splCertUser.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splCertUser)).EndInit();
            this.splCertUser.ResumeLayout(false);
            this.grbUser.ResumeLayout(false);
            this.grbUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarUser)).EndInit();
            this.grbCert.ResumeLayout(false);
            this.grbCert.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvCert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarCert)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlLienKet;
        private System.Windows.Forms.GroupBox grbInfUser;
        private System.Windows.Forms.DateTimePicker dpkValidTo;
        private System.Windows.Forms.DateTimePicker dpkValidFrom;
        private System.Windows.Forms.ComboBox cboCertType;
        private System.Windows.Forms.Label lblValidFrom;
        private System.Windows.Forms.Label lblValidTo;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.GroupBox grbInfUserCert;
        private System.Windows.Forms.ComboBox cboCert;
        private System.Windows.Forms.ComboBox cboUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblCert;
        private System.Windows.Forms.SplitContainer splCertUser;
        private System.Windows.Forms.GroupBox grbCert;
        private Telerik.WinControls.UI.RadListView rlvCert;
        private Telerik.WinControls.UI.RadCommandBar rbarCert;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement2;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement2;
        private Telerik.WinControls.UI.CommandBarLabel cmmbarllbNhom;
        private Telerik.WinControls.UI.CommandBarDropDownList drpCertGroup;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator4;
        private Telerik.WinControls.UI.CommandBarLabel cmmbarlblLoc;
        private Telerik.WinControls.UI.CommandBarTextBox txtCertFilter;
        private System.Windows.Forms.GroupBox grbUser;
        private Telerik.WinControls.UI.RadListView rlvUser;
        private Telerik.WinControls.UI.RadCommandBar rbarUser;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement1;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement1;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel1;
        private Telerik.WinControls.UI.CommandBarDropDownList drpUserGroup;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator1;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel2;
        private Telerik.WinControls.UI.CommandBarTextBox tbUserFilter;
        private System.Windows.Forms.TextBox txtID_UserCert;
        private System.Windows.Forms.Label lblUserCertID;
        private System.Windows.Forms.CheckBox chkVaildTo;

    }
}