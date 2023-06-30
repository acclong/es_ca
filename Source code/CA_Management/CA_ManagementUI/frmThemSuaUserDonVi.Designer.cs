namespace ES.CA_ManagementUI
{
    partial class frmThemSuaUserDonVi
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
            this.grbUnit = new System.Windows.Forms.GroupBox();
            this.rlvUnit = new Telerik.WinControls.UI.RadListView();
            this.rbarUnit = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement2 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement2 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.commandBarLabel3 = new Telerik.WinControls.UI.CommandBarLabel();
            this.drpUnitGroup = new Telerik.WinControls.UI.CommandBarDropDownList();
            this.commandBarSeparator2 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarLabel4 = new Telerik.WinControls.UI.CommandBarLabel();
            this.tbUnitFilter = new Telerik.WinControls.UI.CommandBarTextBox();
            this.commandBarStripElement3 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cboUnit = new System.Windows.Forms.ComboBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.cboUserName = new System.Windows.Forms.ComboBox();
            this.txtID_UserUnit = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblUserUnitID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.chkVaildTo = new System.Windows.Forms.CheckBox();
            this.dpkValidTo = new System.Windows.Forms.DateTimePicker();
            this.dpkValidFrom = new System.Windows.Forms.DateTimePicker();
            this.lblValidFrom = new System.Windows.Forms.Label();
            this.lblValidTo = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlLienKet = new System.Windows.Forms.Panel();
            this.splUnitUser = new System.Windows.Forms.SplitContainer();
            this.grbUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarUser)).BeginInit();
            this.grbUnit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarUnit)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlLienKet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splUnitUser)).BeginInit();
            this.splUnitUser.Panel1.SuspendLayout();
            this.splUnitUser.Panel2.SuspendLayout();
            this.splUnitUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbUser
            // 
            this.grbUser.Controls.Add(this.rlvUser);
            this.grbUser.Controls.Add(this.rbarUser);
            this.grbUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbUser.Location = new System.Drawing.Point(0, 0);
            this.grbUser.Name = "grbUser";
            this.grbUser.Size = new System.Drawing.Size(339, 566);
            this.grbUser.TabIndex = 0;
            this.grbUser.TabStop = false;
            this.grbUser.Text = "Người dùng CA";
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
            this.rlvUser.Size = new System.Drawing.Size(333, 517);
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
            this.rbarUser.Size = new System.Drawing.Size(333, 30);
            this.rbarUser.TabIndex = 0;
            this.rbarUser.Text = "radCommandBar1";
            // 
            // commandBarRowElement1
            // 
            this.commandBarRowElement1.MinSize = new System.Drawing.Size(25, 25);
            this.commandBarRowElement1.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.commandBarStripElement1});
            // 
            // commandBarStripElement1
            // 
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
            ((Telerik.WinControls.UI.RadCommandBarOverflowButton)(this.commandBarStripElement1.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // commandBarLabel1
            // 
            this.commandBarLabel1.AccessibleDescription = "Nhóm:";
            this.commandBarLabel1.AccessibleName = "Nhóm:";
            this.commandBarLabel1.DisplayName = "commandBarLabel1";
            this.commandBarLabel1.Name = "commandBarLabel1";
            this.commandBarLabel1.Text = "Nhóm:";
            // 
            // drpUserGroup
            // 
            this.drpUserGroup.DisplayName = "commandBarDropDownList1";
            this.drpUserGroup.DropDownAnimationEnabled = true;
            this.drpUserGroup.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpUserGroup.MaxDropDownItems = 0;
            this.drpUserGroup.Name = "drpUserGroup";
            this.drpUserGroup.Text = "";
            // 
            // commandBarSeparator1
            // 
            this.commandBarSeparator1.AccessibleDescription = "commandBarSeparator1";
            this.commandBarSeparator1.AccessibleName = "commandBarSeparator1";
            this.commandBarSeparator1.DisplayName = "commandBarSeparator1";
            this.commandBarSeparator1.Name = "commandBarSeparator1";
            this.commandBarSeparator1.VisibleInOverflowMenu = false;
            // 
            // commandBarLabel2
            // 
            this.commandBarLabel2.AccessibleDescription = "Lọc:";
            this.commandBarLabel2.AccessibleName = "Lọc:";
            this.commandBarLabel2.DisplayName = "commandBarLabel2";
            this.commandBarLabel2.Name = "commandBarLabel2";
            this.commandBarLabel2.Text = "Lọc:";
            // 
            // tbUserFilter
            // 
            this.tbUserFilter.DisplayName = "commandBarTextBox1";
            this.tbUserFilter.Name = "tbUserFilter";
            this.tbUserFilter.StretchHorizontally = true;
            this.tbUserFilter.Text = "";
            // 
            // grbUnit
            // 
            this.grbUnit.Controls.Add(this.rlvUnit);
            this.grbUnit.Controls.Add(this.rbarUnit);
            this.grbUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbUnit.Location = new System.Drawing.Point(0, 0);
            this.grbUnit.Name = "grbUnit";
            this.grbUnit.Size = new System.Drawing.Size(340, 566);
            this.grbUnit.TabIndex = 1;
            this.grbUnit.TabStop = false;
            this.grbUnit.Text = "Đơn vị";
            // 
            // rlvUnit
            // 
            this.rlvUnit.AllowColumnReorder = false;
            this.rlvUnit.AllowColumnResize = false;
            this.rlvUnit.AllowEdit = false;
            this.rlvUnit.AllowRemove = false;
            this.rlvUnit.AutoScroll = true;
            this.rlvUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rlvUnit.Location = new System.Drawing.Point(3, 71);
            this.rlvUnit.Name = "rlvUnit";
            this.rlvUnit.Size = new System.Drawing.Size(334, 492);
            this.rlvUnit.TabIndex = 2;
            this.rlvUnit.Text = "radListView1";
            // 
            // rbarUnit
            // 
            this.rbarUnit.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbarUnit.Location = new System.Drawing.Point(3, 16);
            this.rbarUnit.Name = "rbarUnit";
            this.rbarUnit.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement2});
            this.rbarUnit.Size = new System.Drawing.Size(334, 55);
            this.rbarUnit.TabIndex = 1;
            this.rbarUnit.Text = "radCommandBar1";
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
            this.commandBarLabel3,
            this.drpUnitGroup,
            this.commandBarSeparator2,
            this.commandBarLabel4,
            this.tbUnitFilter});
            this.commandBarStripElement2.Name = "commandBarStripElement1";
            // 
            // 
            // 
            this.commandBarStripElement2.OverflowButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            this.commandBarStripElement2.StretchHorizontally = true;
            this.commandBarStripElement2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            ((Telerik.WinControls.UI.RadCommandBarOverflowButton)(this.commandBarStripElement2.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // commandBarLabel3
            // 
            this.commandBarLabel3.AccessibleDescription = "Nhóm:";
            this.commandBarLabel3.AccessibleName = "Nhóm:";
            this.commandBarLabel3.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarLabel3.DisplayName = "commandBarLabel1";
            this.commandBarLabel3.Name = "commandBarLabel3";
            this.commandBarLabel3.Text = "Nhóm:";
            this.commandBarLabel3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // drpUnitGroup
            // 
            this.drpUnitGroup.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.drpUnitGroup.DisplayName = "commandBarDropDownList1";
            this.drpUnitGroup.DropDownAnimationEnabled = true;
            this.drpUnitGroup.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpUnitGroup.MaxDropDownItems = 0;
            this.drpUnitGroup.Name = "drpUnitGroup";
            this.drpUnitGroup.Text = "";
            this.drpUnitGroup.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // commandBarSeparator2
            // 
            this.commandBarSeparator2.AccessibleDescription = "commandBarSeparator1";
            this.commandBarSeparator2.AccessibleName = "commandBarSeparator1";
            this.commandBarSeparator2.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarSeparator2.DisplayName = "commandBarSeparator1";
            this.commandBarSeparator2.Name = "commandBarSeparator2";
            this.commandBarSeparator2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarSeparator2.VisibleInOverflowMenu = false;
            // 
            // commandBarLabel4
            // 
            this.commandBarLabel4.AccessibleDescription = "Lọc:";
            this.commandBarLabel4.AccessibleName = "Lọc:";
            this.commandBarLabel4.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarLabel4.DisplayName = "commandBarLabel2";
            this.commandBarLabel4.Name = "commandBarLabel4";
            this.commandBarLabel4.Text = "Lọc:";
            this.commandBarLabel4.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // tbUnitFilter
            // 
            this.tbUnitFilter.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.tbUnitFilter.DisplayName = "commandBarTextBox1";
            this.tbUnitFilter.Name = "tbUnitFilter";
            this.tbUnitFilter.StretchHorizontally = true;
            this.tbUnitFilter.Text = "";
            this.tbUnitFilter.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // commandBarStripElement3
            // 
            this.commandBarStripElement3.DisplayName = "commandBarStripElement3";
            this.commandBarStripElement3.Name = "commandBarStripElement3";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cboUnit);
            this.groupBox3.Controls.Add(this.lblUnit);
            this.groupBox3.Controls.Add(this.cboUserName);
            this.groupBox3.Controls.Add(this.txtID_UserUnit);
            this.groupBox3.Controls.Add(this.lblUserName);
            this.groupBox3.Controls.Add(this.lblUserUnitID);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(301, 98);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông tin liên kết";
            // 
            // cboUnit
            // 
            this.cboUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboUnit.FormattingEnabled = true;
            this.cboUnit.Location = new System.Drawing.Point(78, 41);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Size = new System.Drawing.Size(216, 21);
            this.cboUnit.TabIndex = 43;
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new System.Drawing.Point(6, 44);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(42, 13);
            this.lblUnit.TabIndex = 42;
            this.lblUnit.Text = "Đơn vị*";
            // 
            // cboUserName
            // 
            this.cboUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboUserName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUserName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboUserName.FormattingEnabled = true;
            this.cboUserName.Location = new System.Drawing.Point(78, 68);
            this.cboUserName.Name = "cboUserName";
            this.cboUserName.Size = new System.Drawing.Size(216, 21);
            this.cboUserName.TabIndex = 41;
            // 
            // txtID_UserUnit
            // 
            this.txtID_UserUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID_UserUnit.Location = new System.Drawing.Point(78, 15);
            this.txtID_UserUnit.Name = "txtID_UserUnit";
            this.txtID_UserUnit.ReadOnly = true;
            this.txtID_UserUnit.Size = new System.Drawing.Size(105, 20);
            this.txtID_UserUnit.TabIndex = 29;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(6, 71);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(66, 13);
            this.lblUserName.TabIndex = 40;
            this.lblUserName.Text = "Người dùng*";
            // 
            // lblUserUnitID
            // 
            this.lblUserUnitID.AutoSize = true;
            this.lblUserUnitID.Location = new System.Drawing.Point(6, 18);
            this.lblUserUnitID.Name = "lblUserUnitID";
            this.lblUserUnitID.Size = new System.Drawing.Size(18, 13);
            this.lblUserUnitID.TabIndex = 28;
            this.lblUserUnitID.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Bộ phận";
            // 
            // txtDepartment
            // 
            this.txtDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDepartment.Location = new System.Drawing.Point(113, 16);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(181, 20);
            this.txtDepartment.TabIndex = 45;
            // 
            // chkVaildTo
            // 
            this.chkVaildTo.AutoSize = true;
            this.chkVaildTo.Location = new System.Drawing.Point(113, 73);
            this.chkVaildTo.Name = "chkVaildTo";
            this.chkVaildTo.Size = new System.Drawing.Size(15, 14);
            this.chkVaildTo.TabIndex = 50;
            this.chkVaildTo.UseVisualStyleBackColor = true;
            this.chkVaildTo.CheckedChanged += new System.EventHandler(this.chkVaildTo_CheckedChanged);
            // 
            // dpkValidTo
            // 
            this.dpkValidTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dpkValidTo.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dpkValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkValidTo.Location = new System.Drawing.Point(134, 70);
            this.dpkValidTo.Name = "dpkValidTo";
            this.dpkValidTo.Size = new System.Drawing.Size(160, 20);
            this.dpkValidTo.TabIndex = 49;
            this.dpkValidTo.Visible = false;
            // 
            // dpkValidFrom
            // 
            this.dpkValidFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dpkValidFrom.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dpkValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkValidFrom.Location = new System.Drawing.Point(113, 42);
            this.dpkValidFrom.Name = "dpkValidFrom";
            this.dpkValidFrom.Size = new System.Drawing.Size(181, 20);
            this.dpkValidFrom.TabIndex = 48;
            // 
            // lblValidFrom
            // 
            this.lblValidFrom.AutoSize = true;
            this.lblValidFrom.Location = new System.Drawing.Point(6, 46);
            this.lblValidFrom.Name = "lblValidFrom";
            this.lblValidFrom.Size = new System.Drawing.Size(91, 13);
            this.lblValidFrom.TabIndex = 46;
            this.lblValidFrom.Text = "Ngày có hiệu lực*";
            // 
            // lblValidTo
            // 
            this.lblValidTo.AutoSize = true;
            this.lblValidTo.Location = new System.Drawing.Point(6, 73);
            this.lblValidTo.Name = "lblValidTo";
            this.lblValidTo.Size = new System.Drawing.Size(90, 13);
            this.lblValidTo.TabIndex = 47;
            this.lblValidTo.Text = "Ngày hết hiệu lực";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkVaildTo);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.dpkValidTo);
            this.groupBox4.Controls.Add(this.txtDepartment);
            this.groupBox4.Controls.Add(this.dpkValidFrom);
            this.groupBox4.Controls.Add(this.lblValidTo);
            this.groupBox4.Controls.Add(this.lblValidFrom);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 98);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(301, 100);
            this.groupBox4.TabIndex = 51;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Thông tin quản lý";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnExit);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 526);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(301, 40);
            this.pnlFooter.TabIndex = 52;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(150, 9);
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
            this.btnSave.Location = new System.Drawing.Point(67, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 35;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlLienKet
            // 
            this.pnlLienKet.Controls.Add(this.pnlFooter);
            this.pnlLienKet.Controls.Add(this.groupBox4);
            this.pnlLienKet.Controls.Add(this.groupBox3);
            this.pnlLienKet.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlLienKet.Location = new System.Drawing.Point(683, 0);
            this.pnlLienKet.Name = "pnlLienKet";
            this.pnlLienKet.Size = new System.Drawing.Size(301, 566);
            this.pnlLienKet.TabIndex = 54;
            // 
            // splUnitUser
            // 
            this.splUnitUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splUnitUser.Location = new System.Drawing.Point(0, 0);
            this.splUnitUser.Name = "splUnitUser";
            // 
            // splUnitUser.Panel1
            // 
            this.splUnitUser.Panel1.Controls.Add(this.grbUnit);
            // 
            // splUnitUser.Panel2
            // 
            this.splUnitUser.Panel2.Controls.Add(this.grbUser);
            this.splUnitUser.Size = new System.Drawing.Size(683, 566);
            this.splUnitUser.SplitterDistance = 340;
            this.splUnitUser.TabIndex = 55;
            // 
            // frmThemSuaUserDonVi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 566);
            this.Controls.Add(this.splUnitUser);
            this.Controls.Add(this.pnlLienKet);
            this.Name = "frmThemSuaUserDonVi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật liên kết người dùng - đơn vị";
            this.Load += new System.EventHandler(this.frmThemSuaUserDonVi_Load);
            this.grbUser.ResumeLayout(false);
            this.grbUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarUser)).EndInit();
            this.grbUnit.ResumeLayout(false);
            this.grbUnit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarUnit)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlLienKet.ResumeLayout(false);
            this.splUnitUser.Panel1.ResumeLayout(false);
            this.splUnitUser.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splUnitUser)).EndInit();
            this.splUnitUser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbUser;
        private System.Windows.Forms.GroupBox grbUnit;
        private Telerik.WinControls.UI.RadListView rlvUser;
        private Telerik.WinControls.UI.RadCommandBar rbarUser;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement1;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement1;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel1;
        private Telerik.WinControls.UI.CommandBarDropDownList drpUserGroup;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator1;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel2;
        private Telerik.WinControls.UI.CommandBarTextBox tbUserFilter;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement3;
        private Telerik.WinControls.UI.RadCommandBar rbarUnit;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement2;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement2;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel3;
        private Telerik.WinControls.UI.CommandBarDropDownList drpUnitGroup;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator2;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel4;
        private Telerik.WinControls.UI.CommandBarTextBox tbUnitFilter;
        private Telerik.WinControls.UI.RadListView rlvUnit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtID_UserUnit;
        private System.Windows.Forms.Label lblUserUnitID;
        private System.Windows.Forms.ComboBox cboUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.ComboBox cboUnit;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.CheckBox chkVaildTo;
        private System.Windows.Forms.DateTimePicker dpkValidTo;
        private System.Windows.Forms.DateTimePicker dpkValidFrom;
        private System.Windows.Forms.Label lblValidFrom;
        private System.Windows.Forms.Label lblValidTo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlLienKet;
        private System.Windows.Forms.SplitContainer splUnitUser;

    }
}