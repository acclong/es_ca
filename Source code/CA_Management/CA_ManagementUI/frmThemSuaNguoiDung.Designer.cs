namespace ES.CA_ManagementUI
{
    partial class frmThemSuaNguoiDung
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
            this.grbInfUserCA = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboChungThuSo = new System.Windows.Forms.ComboBox();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chkVaildTo = new System.Windows.Forms.CheckBox();
            this.btnSeach = new System.Windows.Forms.Button();
            this.dpkValidTo = new System.Windows.Forms.DateTimePicker();
            this.txtUnitName = new System.Windows.Forms.TextBox();
            this.dpkValidFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblValidFrom = new System.Windows.Forms.Label();
            this.lblValidTo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grbInfUser = new System.Windows.Forms.GroupBox();
            this.txtUnitID = new System.Windows.Forms.TextBox();
            this.lblCMND = new System.Windows.Forms.Label();
            this.txtCMND = new System.Windows.Forms.TextBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserID = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlFooter.SuspendLayout();
            this.grbInfUserCA.SuspendLayout();
            this.grbInfUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnExit);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 362);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(384, 40);
            this.pnlFooter.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(198, 9);
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
            this.btnSave.Location = new System.Drawing.Point(115, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grbInfUserCA
            // 
            this.grbInfUserCA.Controls.Add(this.txtEmail);
            this.grbInfUserCA.Controls.Add(this.label3);
            this.grbInfUserCA.Controls.Add(this.cboChungThuSo);
            this.grbInfUserCA.Controls.Add(this.cboStatus);
            this.grbInfUserCA.Controls.Add(this.txtDescription);
            this.grbInfUserCA.Controls.Add(this.lblStatus);
            this.grbInfUserCA.Controls.Add(this.chkVaildTo);
            this.grbInfUserCA.Controls.Add(this.btnSeach);
            this.grbInfUserCA.Controls.Add(this.dpkValidTo);
            this.grbInfUserCA.Controls.Add(this.txtUnitName);
            this.grbInfUserCA.Controls.Add(this.dpkValidFrom);
            this.grbInfUserCA.Controls.Add(this.label1);
            this.grbInfUserCA.Controls.Add(this.lblValidFrom);
            this.grbInfUserCA.Controls.Add(this.label4);
            this.grbInfUserCA.Controls.Add(this.lblValidTo);
            this.grbInfUserCA.Controls.Add(this.label2);
            this.grbInfUserCA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbInfUserCA.Location = new System.Drawing.Point(0, 99);
            this.grbInfUserCA.Name = "grbInfUserCA";
            this.grbInfUserCA.Size = new System.Drawing.Size(384, 263);
            this.grbInfUserCA.TabIndex = 1;
            this.grbInfUserCA.TabStop = false;
            this.grbInfUserCA.Text = "Thông tin quản lý người dùng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Chứng thư sô";
            // 
            // cboChungThuSo
            // 
            this.cboChungThuSo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChungThuSo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboChungThuSo.FormattingEnabled = true;
            this.cboChungThuSo.Location = new System.Drawing.Point(110, 39);
            this.cboChungThuSo.Name = "cboChungThuSo";
            this.cboChungThuSo.Size = new System.Drawing.Size(263, 21);
            this.cboChungThuSo.TabIndex = 5;
            // 
            // cboStatus
            // 
            this.cboStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(110, 66);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(262, 21);
            this.cboStatus.TabIndex = 6;
            this.cboStatus.SelectedIndexChanged += new System.EventHandler(this.cboStatus_SelectedIndexChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(111, 171);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(261, 87);
            this.txtDescription.TabIndex = 7;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(14, 69);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(62, 13);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Trạng thái *";
            // 
            // chkVaildTo
            // 
            this.chkVaildTo.AutoSize = true;
            this.chkVaildTo.Location = new System.Drawing.Point(111, 122);
            this.chkVaildTo.Name = "chkVaildTo";
            this.chkVaildTo.Size = new System.Drawing.Size(15, 14);
            this.chkVaildTo.TabIndex = 3;
            this.chkVaildTo.UseVisualStyleBackColor = true;
            this.chkVaildTo.CheckedChanged += new System.EventHandler(this.chkVaildTo_CheckedChanged);
            // 
            // btnSeach
            // 
            this.btnSeach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeach.Location = new System.Drawing.Point(297, 11);
            this.btnSeach.Name = "btnSeach";
            this.btnSeach.Size = new System.Drawing.Size(75, 22);
            this.btnSeach.TabIndex = 1;
            this.btnSeach.Text = "Tìm";
            this.btnSeach.UseVisualStyleBackColor = true;
            this.btnSeach.Click += new System.EventHandler(this.btnSeach_Click);
            // 
            // dpkValidTo
            // 
            this.dpkValidTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dpkValidTo.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dpkValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkValidTo.Location = new System.Drawing.Point(132, 119);
            this.dpkValidTo.Name = "dpkValidTo";
            this.dpkValidTo.Size = new System.Drawing.Size(241, 20);
            this.dpkValidTo.TabIndex = 4;
            this.dpkValidTo.Visible = false;
            // 
            // txtUnitName
            // 
            this.txtUnitName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUnitName.Location = new System.Drawing.Point(111, 13);
            this.txtUnitName.Name = "txtUnitName";
            this.txtUnitName.ReadOnly = true;
            this.txtUnitName.Size = new System.Drawing.Size(180, 20);
            this.txtUnitName.TabIndex = 0;
            // 
            // dpkValidFrom
            // 
            this.dpkValidFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dpkValidFrom.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dpkValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkValidFrom.Location = new System.Drawing.Point(111, 93);
            this.dpkValidFrom.Name = "dpkValidFrom";
            this.dpkValidFrom.Size = new System.Drawing.Size(262, 20);
            this.dpkValidFrom.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Đơn vị *";
            // 
            // lblValidFrom
            // 
            this.lblValidFrom.AutoSize = true;
            this.lblValidFrom.Location = new System.Drawing.Point(14, 96);
            this.lblValidFrom.Name = "lblValidFrom";
            this.lblValidFrom.Size = new System.Drawing.Size(94, 13);
            this.lblValidFrom.TabIndex = 10;
            this.lblValidFrom.Text = "Ngày có hiệu lực *";
            // 
            // lblValidTo
            // 
            this.lblValidTo.AutoSize = true;
            this.lblValidTo.Location = new System.Drawing.Point(14, 122);
            this.lblValidTo.Name = "lblValidTo";
            this.lblValidTo.Size = new System.Drawing.Size(90, 13);
            this.lblValidTo.TabIndex = 11;
            this.lblValidTo.Text = "Ngày hết hiệu lực";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Mô tả";
            // 
            // grbInfUser
            // 
            this.grbInfUser.Controls.Add(this.txtUnitID);
            this.grbInfUser.Controls.Add(this.lblCMND);
            this.grbInfUser.Controls.Add(this.txtCMND);
            this.grbInfUser.Controls.Add(this.txtUserID);
            this.grbInfUser.Controls.Add(this.lblUserName);
            this.grbInfUser.Controls.Add(this.txtUserName);
            this.grbInfUser.Controls.Add(this.lblUserID);
            this.grbInfUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbInfUser.Location = new System.Drawing.Point(0, 0);
            this.grbInfUser.Name = "grbInfUser";
            this.grbInfUser.Size = new System.Drawing.Size(384, 99);
            this.grbInfUser.TabIndex = 0;
            this.grbInfUser.TabStop = false;
            this.grbInfUser.Text = "Thông tin người dùng";
            // 
            // txtUnitID
            // 
            this.txtUnitID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUnitID.Location = new System.Drawing.Point(314, 21);
            this.txtUnitID.Name = "txtUnitID";
            this.txtUnitID.ReadOnly = true;
            this.txtUnitID.Size = new System.Drawing.Size(57, 20);
            this.txtUnitID.TabIndex = 1;
            this.txtUnitID.Text = "-1";
            this.txtUnitID.Visible = false;
            // 
            // lblCMND
            // 
            this.lblCMND.AutoSize = true;
            this.lblCMND.Location = new System.Drawing.Point(14, 76);
            this.lblCMND.Name = "lblCMND";
            this.lblCMND.Size = new System.Drawing.Size(62, 13);
            this.lblCMND.TabIndex = 15;
            this.lblCMND.Text = "Số CMND *";
            // 
            // txtCMND
            // 
            this.txtCMND.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCMND.Location = new System.Drawing.Point(110, 73);
            this.txtCMND.Name = "txtCMND";
            this.txtCMND.Size = new System.Drawing.Size(261, 20);
            this.txtCMND.TabIndex = 3;
            // 
            // txtUserID
            // 
            this.txtUserID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserID.Location = new System.Drawing.Point(110, 21);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.ReadOnly = true;
            this.txtUserID.Size = new System.Drawing.Size(100, 20);
            this.txtUserID.TabIndex = 0;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(14, 50);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(61, 13);
            this.lblUserName.TabIndex = 7;
            this.lblUserName.Text = "Họ và tên *";
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Location = new System.Drawing.Point(110, 47);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(261, 20);
            this.txtUserName.TabIndex = 2;
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Location = new System.Drawing.Point(14, 24);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(18, 13);
            this.lblUserID.TabIndex = 6;
            this.lblUserID.Text = "ID";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(111, 145);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(260, 20);
            this.txtEmail.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Email";
            // 
            // frmThemSuaNguoiDung
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 402);
            this.Controls.Add(this.grbInfUserCA);
            this.Controls.Add(this.grbInfUser);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmThemSuaNguoiDung";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật người dùng";
            this.Load += new System.EventHandler(this.frmThemSuaNguoiDung_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmThemSuaNguoiDung_KeyDown);
            this.pnlFooter.ResumeLayout(false);
            this.grbInfUserCA.ResumeLayout(false);
            this.grbInfUserCA.PerformLayout();
            this.grbInfUser.ResumeLayout(false);
            this.grbInfUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grbInfUserCA;
        private System.Windows.Forms.Label lblValidFrom;
        private System.Windows.Forms.Label lblValidTo;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox grbInfUser;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.DateTimePicker dpkValidTo;
        private System.Windows.Forms.DateTimePicker dpkValidFrom;
        private System.Windows.Forms.CheckBox chkVaildTo;
        private System.Windows.Forms.Label lblCMND;
        private System.Windows.Forms.TextBox txtCMND;
        private System.Windows.Forms.Button btnSeach;
        private System.Windows.Forms.TextBox txtUnitName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUnitID;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboChungThuSo;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label4;
    }
}