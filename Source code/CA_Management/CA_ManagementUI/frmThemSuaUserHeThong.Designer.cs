namespace ES.CA_ManagementUI
{
    partial class frmThemSuaUserHeThong
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
            this.commandBarStripElement3 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.pnlLienKet = new System.Windows.Forms.Panel();
            this.grbQuanLy = new System.Windows.Forms.GroupBox();
            this.cboUserProgName = new System.Windows.Forms.ComboBox();
            this.lblUserProg = new System.Windows.Forms.Label();
            this.chkVaildTo = new System.Windows.Forms.CheckBox();
            this.dpkValidTo = new System.Windows.Forms.DateTimePicker();
            this.dpkValidFrom = new System.Windows.Forms.DateTimePicker();
            this.lblValidTo = new System.Windows.Forms.Label();
            this.lblValidFrom = new System.Windows.Forms.Label();
            this.grbLienKet = new System.Windows.Forms.GroupBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtProg = new System.Windows.Forms.TextBox();
            this.btnSeachHeThong = new System.Windows.Forms.Button();
            this.btnTimUser = new System.Windows.Forms.Button();
            this.lblProg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtID_UserProg = new System.Windows.Forms.TextBox();
            this.lblUserProgID = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.commandBarSeparator2 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarStripElement5 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.commandBarLabel5 = new Telerik.WinControls.UI.CommandBarLabel();
            this.commandBarDropDownList1 = new Telerik.WinControls.UI.CommandBarDropDownList();
            this.commandBarSeparator3 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarLabel6 = new Telerik.WinControls.UI.CommandBarLabel();
            this.commandBarTextBox1 = new Telerik.WinControls.UI.CommandBarTextBox();
            this.pnlLienKet.SuspendLayout();
            this.grbQuanLy.SuspendLayout();
            this.grbLienKet.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // commandBarStripElement3
            // 
            this.commandBarStripElement3.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement3.DisplayName = "commandBarStripElement3";
            this.commandBarStripElement3.Name = "commandBarStripElement3";
            this.commandBarStripElement3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // pnlLienKet
            // 
            this.pnlLienKet.Controls.Add(this.grbQuanLy);
            this.pnlLienKet.Controls.Add(this.grbLienKet);
            this.pnlLienKet.Controls.Add(this.pnlFooter);
            this.pnlLienKet.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlLienKet.Location = new System.Drawing.Point(2, 0);
            this.pnlLienKet.Name = "pnlLienKet";
            this.pnlLienKet.Size = new System.Drawing.Size(373, 247);
            this.pnlLienKet.TabIndex = 56;
            // 
            // grbQuanLy
            // 
            this.grbQuanLy.Controls.Add(this.cboUserProgName);
            this.grbQuanLy.Controls.Add(this.lblUserProg);
            this.grbQuanLy.Controls.Add(this.chkVaildTo);
            this.grbQuanLy.Controls.Add(this.dpkValidTo);
            this.grbQuanLy.Controls.Add(this.dpkValidFrom);
            this.grbQuanLy.Controls.Add(this.lblValidTo);
            this.grbQuanLy.Controls.Add(this.lblValidFrom);
            this.grbQuanLy.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbQuanLy.Location = new System.Drawing.Point(0, 106);
            this.grbQuanLy.Name = "grbQuanLy";
            this.grbQuanLy.Size = new System.Drawing.Size(373, 101);
            this.grbQuanLy.TabIndex = 1;
            this.grbQuanLy.TabStop = false;
            this.grbQuanLy.Text = "Thông tin quản lý";
            // 
            // cboUserProgName
            // 
            this.cboUserProgName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboUserProgName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboUserProgName.FormattingEnabled = true;
            this.cboUserProgName.Location = new System.Drawing.Point(113, 19);
            this.cboUserProgName.Name = "cboUserProgName";
            this.cboUserProgName.Size = new System.Drawing.Size(253, 21);
            this.cboUserProgName.TabIndex = 0;
            // 
            // lblUserProg
            // 
            this.lblUserProg.AutoSize = true;
            this.lblUserProg.Location = new System.Drawing.Point(6, 22);
            this.lblUserProg.Name = "lblUserProg";
            this.lblUserProg.Size = new System.Drawing.Size(81, 13);
            this.lblUserProg.TabIndex = 52;
            this.lblUserProg.Text = "Tên đăng nhập";
            // 
            // chkVaildTo
            // 
            this.chkVaildTo.AutoSize = true;
            this.chkVaildTo.Location = new System.Drawing.Point(113, 77);
            this.chkVaildTo.Name = "chkVaildTo";
            this.chkVaildTo.Size = new System.Drawing.Size(15, 14);
            this.chkVaildTo.TabIndex = 5;
            this.chkVaildTo.UseVisualStyleBackColor = true;
            this.chkVaildTo.CheckedChanged += new System.EventHandler(this.chkVaildTo_CheckedChanged);
            // 
            // dpkValidTo
            // 
            this.dpkValidTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dpkValidTo.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dpkValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkValidTo.Location = new System.Drawing.Point(134, 74);
            this.dpkValidTo.Name = "dpkValidTo";
            this.dpkValidTo.Size = new System.Drawing.Size(232, 20);
            this.dpkValidTo.TabIndex = 6;
            this.dpkValidTo.Visible = false;
            // 
            // dpkValidFrom
            // 
            this.dpkValidFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dpkValidFrom.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dpkValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkValidFrom.Location = new System.Drawing.Point(113, 46);
            this.dpkValidFrom.Name = "dpkValidFrom";
            this.dpkValidFrom.Size = new System.Drawing.Size(253, 20);
            this.dpkValidFrom.TabIndex = 4;
            // 
            // lblValidTo
            // 
            this.lblValidTo.AutoSize = true;
            this.lblValidTo.Location = new System.Drawing.Point(6, 77);
            this.lblValidTo.Name = "lblValidTo";
            this.lblValidTo.Size = new System.Drawing.Size(90, 13);
            this.lblValidTo.TabIndex = 47;
            this.lblValidTo.Text = "Ngày hết hiệu lực";
            // 
            // lblValidFrom
            // 
            this.lblValidFrom.AutoSize = true;
            this.lblValidFrom.Location = new System.Drawing.Point(6, 50);
            this.lblValidFrom.Name = "lblValidFrom";
            this.lblValidFrom.Size = new System.Drawing.Size(94, 13);
            this.lblValidFrom.TabIndex = 46;
            this.lblValidFrom.Text = "Ngày có hiệu lực *";
            // 
            // grbLienKet
            // 
            this.grbLienKet.Controls.Add(this.txtUserName);
            this.grbLienKet.Controls.Add(this.txtProg);
            this.grbLienKet.Controls.Add(this.btnSeachHeThong);
            this.grbLienKet.Controls.Add(this.btnTimUser);
            this.grbLienKet.Controls.Add(this.lblProg);
            this.grbLienKet.Controls.Add(this.label1);
            this.grbLienKet.Controls.Add(this.txtID_UserProg);
            this.grbLienKet.Controls.Add(this.lblUserProgID);
            this.grbLienKet.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbLienKet.Location = new System.Drawing.Point(0, 0);
            this.grbLienKet.Name = "grbLienKet";
            this.grbLienKet.Size = new System.Drawing.Size(373, 106);
            this.grbLienKet.TabIndex = 0;
            this.grbLienKet.TabStop = false;
            this.grbLienKet.Text = "Thông tin liên kết";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(113, 73);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(174, 20);
            this.txtUserName.TabIndex = 2;
            // 
            // txtProg
            // 
            this.txtProg.Location = new System.Drawing.Point(113, 44);
            this.txtProg.Name = "txtProg";
            this.txtProg.ReadOnly = true;
            this.txtProg.Size = new System.Drawing.Size(174, 20);
            this.txtProg.TabIndex = 1;
            // 
            // btnSeachHeThong
            // 
            this.btnSeachHeThong.Location = new System.Drawing.Point(294, 41);
            this.btnSeachHeThong.Name = "btnSeachHeThong";
            this.btnSeachHeThong.Size = new System.Drawing.Size(72, 23);
            this.btnSeachHeThong.TabIndex = 3;
            this.btnSeachHeThong.Text = "Tìm";
            this.btnSeachHeThong.UseVisualStyleBackColor = true;
            this.btnSeachHeThong.Click += new System.EventHandler(this.btnSeachHeThong_Click);
            // 
            // btnTimUser
            // 
            this.btnTimUser.Location = new System.Drawing.Point(293, 73);
            this.btnTimUser.Name = "btnTimUser";
            this.btnTimUser.Size = new System.Drawing.Size(73, 23);
            this.btnTimUser.TabIndex = 4;
            this.btnTimUser.Text = "Tìm";
            this.btnTimUser.UseVisualStyleBackColor = true;
            this.btnTimUser.Click += new System.EventHandler(this.btnTimUser_Click);
            // 
            // lblProg
            // 
            this.lblProg.AutoSize = true;
            this.lblProg.Location = new System.Drawing.Point(6, 47);
            this.lblProg.Name = "lblProg";
            this.lblProg.Size = new System.Drawing.Size(101, 13);
            this.lblProg.TabIndex = 42;
            this.lblProg.Text = "Hệ thống tích hợp *";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Người dùng *";
            // 
            // txtID_UserProg
            // 
            this.txtID_UserProg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID_UserProg.Location = new System.Drawing.Point(113, 16);
            this.txtID_UserProg.Name = "txtID_UserProg";
            this.txtID_UserProg.ReadOnly = true;
            this.txtID_UserProg.Size = new System.Drawing.Size(93, 20);
            this.txtID_UserProg.TabIndex = 0;
            // 
            // lblUserProgID
            // 
            this.lblUserProgID.AutoSize = true;
            this.lblUserProgID.Location = new System.Drawing.Point(6, 19);
            this.lblUserProgID.Name = "lblUserProgID";
            this.lblUserProgID.Size = new System.Drawing.Size(18, 13);
            this.lblUserProgID.TabIndex = 28;
            this.lblUserProgID.Text = "ID";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnExit);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 207);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(373, 40);
            this.pnlFooter.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(190, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Đóng";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(107, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            // commandBarStripElement5
            // 
            this.commandBarStripElement5.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement5.DisplayName = "commandBarStripElement1";
            this.commandBarStripElement5.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.commandBarLabel5,
            this.commandBarDropDownList1,
            this.commandBarSeparator3,
            this.commandBarLabel6,
            this.commandBarTextBox1});
            this.commandBarStripElement5.Name = "commandBarStripElement1";
            // 
            // 
            // 
            this.commandBarStripElement5.OverflowButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            this.commandBarStripElement5.StretchHorizontally = true;
            this.commandBarStripElement5.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            ((Telerik.WinControls.UI.RadCommandBarOverflowButton)(this.commandBarStripElement5.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // commandBarLabel5
            // 
            this.commandBarLabel5.AccessibleDescription = "Nhóm:";
            this.commandBarLabel5.AccessibleName = "Nhóm:";
            this.commandBarLabel5.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarLabel5.DisplayName = "commandBarLabel1";
            this.commandBarLabel5.Name = "commandBarLabel5";
            this.commandBarLabel5.Text = "Nhóm:";
            this.commandBarLabel5.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // commandBarDropDownList1
            // 
            this.commandBarDropDownList1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarDropDownList1.DisplayName = "commandBarDropDownList1";
            this.commandBarDropDownList1.DropDownAnimationEnabled = true;
            this.commandBarDropDownList1.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.commandBarDropDownList1.MaxDropDownItems = 0;
            this.commandBarDropDownList1.Name = "commandBarDropDownList1";
            this.commandBarDropDownList1.Text = "";
            this.commandBarDropDownList1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // commandBarSeparator3
            // 
            this.commandBarSeparator3.AccessibleDescription = "commandBarSeparator1";
            this.commandBarSeparator3.AccessibleName = "commandBarSeparator1";
            this.commandBarSeparator3.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarSeparator3.DisplayName = "commandBarSeparator1";
            this.commandBarSeparator3.Name = "commandBarSeparator3";
            this.commandBarSeparator3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarSeparator3.VisibleInOverflowMenu = false;
            // 
            // commandBarLabel6
            // 
            this.commandBarLabel6.AccessibleDescription = "Lọc:";
            this.commandBarLabel6.AccessibleName = "Lọc:";
            this.commandBarLabel6.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarLabel6.DisplayName = "commandBarLabel2";
            this.commandBarLabel6.Name = "commandBarLabel6";
            this.commandBarLabel6.Text = "Lọc:";
            this.commandBarLabel6.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // commandBarTextBox1
            // 
            this.commandBarTextBox1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarTextBox1.DisplayName = "commandBarTextBox1";
            this.commandBarTextBox1.Name = "commandBarTextBox1";
            this.commandBarTextBox1.StretchHorizontally = true;
            this.commandBarTextBox1.Text = "";
            this.commandBarTextBox1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // frmThemSuaUserHeThong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 247);
            this.Controls.Add(this.pnlLienKet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmThemSuaUserHeThong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật người dùng - hệ thống";
            this.Load += new System.EventHandler(this.frmThemSuaUserDonVi_Load);
            this.pnlLienKet.ResumeLayout(false);
            this.grbQuanLy.ResumeLayout(false);
            this.grbQuanLy.PerformLayout();
            this.grbLienKet.ResumeLayout(false);
            this.grbLienKet.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement3;
        private System.Windows.Forms.Panel pnlLienKet;
        private System.Windows.Forms.GroupBox grbLienKet;
        private System.Windows.Forms.Label lblProg;
        private System.Windows.Forms.TextBox txtID_UserProg;
        private System.Windows.Forms.Label lblUserProgID;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator2;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement5;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel5;
        private Telerik.WinControls.UI.CommandBarDropDownList commandBarDropDownList1;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator3;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel6;
        private Telerik.WinControls.UI.CommandBarTextBox commandBarTextBox1;
        private System.Windows.Forms.GroupBox grbQuanLy;
        private System.Windows.Forms.CheckBox chkVaildTo;
        private System.Windows.Forms.DateTimePicker dpkValidTo;
        private System.Windows.Forms.DateTimePicker dpkValidFrom;
        private System.Windows.Forms.Label lblValidTo;
        private System.Windows.Forms.Label lblValidFrom;
        private System.Windows.Forms.Label lblUserProg;
        private System.Windows.Forms.ComboBox cboUserProgName;
        private System.Windows.Forms.Button btnTimUser;
        private System.Windows.Forms.Button btnSeachHeThong;
        private System.Windows.Forms.TextBox txtProg;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label1;


    }
}