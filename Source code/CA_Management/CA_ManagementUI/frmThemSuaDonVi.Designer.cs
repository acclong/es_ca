namespace ES.CA_ManagementUI
{
    partial class frmThemSuaDonVi
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
            this.txtNotation = new System.Windows.Forms.TextBox();
            this.txtUnitID = new System.Windows.Forms.TextBox();
            this.lblUnitName = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.lblNotation = new System.Windows.Forms.Label();
            this.grbInfUser = new System.Windows.Forms.GroupBox();
            this.lblValidFrom = new System.Windows.Forms.Label();
            this.chkVaildTo = new System.Windows.Forms.CheckBox();
            this.lblValidTo = new System.Windows.Forms.Label();
            this.dpkValidFrom = new System.Windows.Forms.DateTimePicker();
            this.dpkValidTo = new System.Windows.Forms.DateTimePicker();
            this.lblMaDv = new System.Windows.Forms.Label();
            this.txtMaDv = new System.Windows.Forms.TextBox();
            this.cboParent = new System.Windows.Forms.ComboBox();
            this.lblParent = new System.Windows.Forms.Label();
            this.cboTypeUnit = new System.Windows.Forms.ComboBox();
            this.lblTypeUnit = new System.Windows.Forms.Label();
            this.txtUnitName = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.grbInfUserCA = new System.Windows.Forms.GroupBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMien = new System.Windows.Forms.Label();
            this.cboMien = new System.Windows.Forms.ComboBox();
            this.lblTenTat = new System.Windows.Forms.Label();
            this.txtTenTat = new System.Windows.Forms.TextBox();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.grbInfUser.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.grbInfUserCA.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNotation
            // 
            this.txtNotation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotation.Location = new System.Drawing.Point(109, 15);
            this.txtNotation.Name = "txtNotation";
            this.txtNotation.Size = new System.Drawing.Size(270, 20);
            this.txtNotation.TabIndex = 0;
            // 
            // txtUnitID
            // 
            this.txtUnitID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUnitID.Location = new System.Drawing.Point(108, 17);
            this.txtUnitID.Name = "txtUnitID";
            this.txtUnitID.ReadOnly = true;
            this.txtUnitID.Size = new System.Drawing.Size(109, 20);
            this.txtUnitID.TabIndex = 0;
            // 
            // lblUnitName
            // 
            this.lblUnitName.AutoSize = true;
            this.lblUnitName.Location = new System.Drawing.Point(4, 99);
            this.lblUnitName.Name = "lblUnitName";
            this.lblUnitName.Size = new System.Drawing.Size(66, 13);
            this.lblUnitName.TabIndex = 7;
            this.lblUnitName.Text = "Tên đơn vị *";
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Location = new System.Drawing.Point(6, 20);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(18, 13);
            this.lblUserID.TabIndex = 6;
            this.lblUserID.Text = "ID";
            // 
            // lblNotation
            // 
            this.lblNotation.AutoSize = true;
            this.lblNotation.Location = new System.Drawing.Point(6, 18);
            this.lblNotation.Name = "lblNotation";
            this.lblNotation.Size = new System.Drawing.Size(49, 13);
            this.lblNotation.TabIndex = 15;
            this.lblNotation.Text = "Ký hiệu *";
            // 
            // grbInfUser
            // 
            this.grbInfUser.Controls.Add(this.lblValidFrom);
            this.grbInfUser.Controls.Add(this.chkVaildTo);
            this.grbInfUser.Controls.Add(this.lblValidTo);
            this.grbInfUser.Controls.Add(this.dpkValidFrom);
            this.grbInfUser.Controls.Add(this.cboStatus);
            this.grbInfUser.Controls.Add(this.lblStatus);
            this.grbInfUser.Controls.Add(this.dpkValidTo);
            this.grbInfUser.Controls.Add(this.lblNotation);
            this.grbInfUser.Controls.Add(this.txtNotation);
            this.grbInfUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbInfUser.Location = new System.Drawing.Point(0, 216);
            this.grbInfUser.Name = "grbInfUser";
            this.grbInfUser.Size = new System.Drawing.Size(393, 128);
            this.grbInfUser.TabIndex = 1;
            this.grbInfUser.TabStop = false;
            this.grbInfUser.Text = "Thông tin đơn vị";
            // 
            // lblValidFrom
            // 
            this.lblValidFrom.AutoSize = true;
            this.lblValidFrom.Location = new System.Drawing.Point(6, 74);
            this.lblValidFrom.Name = "lblValidFrom";
            this.lblValidFrom.Size = new System.Drawing.Size(94, 13);
            this.lblValidFrom.TabIndex = 10;
            this.lblValidFrom.Text = "Ngày có hiệu lực *";
            // 
            // chkVaildTo
            // 
            this.chkVaildTo.AutoSize = true;
            this.chkVaildTo.Location = new System.Drawing.Point(108, 101);
            this.chkVaildTo.Name = "chkVaildTo";
            this.chkVaildTo.Size = new System.Drawing.Size(15, 14);
            this.chkVaildTo.TabIndex = 3;
            this.chkVaildTo.UseVisualStyleBackColor = true;
            this.chkVaildTo.CheckedChanged += new System.EventHandler(this.chkVaildTo_CheckedChanged);
            // 
            // lblValidTo
            // 
            this.lblValidTo.AutoSize = true;
            this.lblValidTo.Location = new System.Drawing.Point(4, 100);
            this.lblValidTo.Name = "lblValidTo";
            this.lblValidTo.Size = new System.Drawing.Size(90, 13);
            this.lblValidTo.TabIndex = 11;
            this.lblValidTo.Text = "Ngày hết hiệu lực";
            // 
            // dpkValidFrom
            // 
            this.dpkValidFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dpkValidFrom.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dpkValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkValidFrom.Location = new System.Drawing.Point(108, 70);
            this.dpkValidFrom.Name = "dpkValidFrom";
            this.dpkValidFrom.Size = new System.Drawing.Size(271, 20);
            this.dpkValidFrom.TabIndex = 2;
            // 
            // dpkValidTo
            // 
            this.dpkValidTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dpkValidTo.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dpkValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkValidTo.Location = new System.Drawing.Point(129, 98);
            this.dpkValidTo.Name = "dpkValidTo";
            this.dpkValidTo.Size = new System.Drawing.Size(250, 20);
            this.dpkValidTo.TabIndex = 4;
            this.dpkValidTo.Visible = false;
            // 
            // lblMaDv
            // 
            this.lblMaDv.AutoSize = true;
            this.lblMaDv.Location = new System.Drawing.Point(6, 73);
            this.lblMaDv.Name = "lblMaDv";
            this.lblMaDv.Size = new System.Drawing.Size(62, 13);
            this.lblMaDv.TabIndex = 24;
            this.lblMaDv.Text = "Mã đơn vị *";
            // 
            // txtMaDv
            // 
            this.txtMaDv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaDv.Location = new System.Drawing.Point(109, 70);
            this.txtMaDv.Name = "txtMaDv";
            this.txtMaDv.ReadOnly = true;
            this.txtMaDv.Size = new System.Drawing.Size(269, 20);
            this.txtMaDv.TabIndex = 3;
            // 
            // cboParent
            // 
            this.cboParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParent.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboParent.FormattingEnabled = true;
            this.cboParent.Location = new System.Drawing.Point(109, 122);
            this.cboParent.Name = "cboParent";
            this.cboParent.Size = new System.Drawing.Size(271, 21);
            this.cboParent.TabIndex = 5;
            // 
            // lblParent
            // 
            this.lblParent.AutoSize = true;
            this.lblParent.Location = new System.Drawing.Point(6, 125);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(59, 13);
            this.lblParent.TabIndex = 22;
            this.lblParent.Text = "Đơn vị cha";
            // 
            // cboTypeUnit
            // 
            this.cboTypeUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTypeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTypeUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboTypeUnit.FormattingEnabled = true;
            this.cboTypeUnit.Location = new System.Drawing.Point(108, 43);
            this.cboTypeUnit.Name = "cboTypeUnit";
            this.cboTypeUnit.Size = new System.Drawing.Size(193, 21);
            this.cboTypeUnit.TabIndex = 1;
            this.cboTypeUnit.SelectedIndexChanged += new System.EventHandler(this.cboTypeUnit_SelectedIndexChanged);
            // 
            // lblTypeUnit
            // 
            this.lblTypeUnit.AutoSize = true;
            this.lblTypeUnit.Location = new System.Drawing.Point(6, 46);
            this.lblTypeUnit.Name = "lblTypeUnit";
            this.lblTypeUnit.Size = new System.Drawing.Size(67, 13);
            this.lblTypeUnit.TabIndex = 20;
            this.lblTypeUnit.Text = "Loại đơn vị *";
            // 
            // txtUnitName
            // 
            this.txtUnitName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUnitName.Location = new System.Drawing.Point(108, 96);
            this.txtUnitName.Name = "txtUnitName";
            this.txtUnitName.ReadOnly = true;
            this.txtUnitName.Size = new System.Drawing.Size(270, 20);
            this.txtUnitName.TabIndex = 4;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(205, 9);
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
            this.btnSave.Location = new System.Drawing.Point(122, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnExit);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 342);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(393, 40);
            this.pnlFooter.TabIndex = 2;
            // 
            // grbInfUserCA
            // 
            this.grbInfUserCA.Controls.Add(this.cboMien);
            this.grbInfUserCA.Controls.Add(this.lblMien);
            this.grbInfUserCA.Controls.Add(this.lblTenTat);
            this.grbInfUserCA.Controls.Add(this.txtTenTat);
            this.grbInfUserCA.Controls.Add(this.btnFind);
            this.grbInfUserCA.Controls.Add(this.lblMaDv);
            this.grbInfUserCA.Controls.Add(this.lblUserID);
            this.grbInfUserCA.Controls.Add(this.txtMaDv);
            this.grbInfUserCA.Controls.Add(this.txtUnitName);
            this.grbInfUserCA.Controls.Add(this.lblUnitName);
            this.grbInfUserCA.Controls.Add(this.cboParent);
            this.grbInfUserCA.Controls.Add(this.txtUnitID);
            this.grbInfUserCA.Controls.Add(this.lblParent);
            this.grbInfUserCA.Controls.Add(this.cboTypeUnit);
            this.grbInfUserCA.Controls.Add(this.lblTypeUnit);
            this.grbInfUserCA.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbInfUserCA.Location = new System.Drawing.Point(0, 0);
            this.grbInfUserCA.Name = "grbInfUserCA";
            this.grbInfUserCA.Size = new System.Drawing.Size(393, 216);
            this.grbInfUserCA.TabIndex = 0;
            this.grbInfUserCA.TabStop = false;
            this.grbInfUserCA.Text = "Thông tin quản lý đơn vị";
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(307, 41);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(71, 23);
            this.btnFind.TabIndex = 2;
            this.btnFind.Text = "Tìm";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grbInfUser);
            this.panel1.Controls.Add(this.grbInfUserCA);
            this.panel1.Controls.Add(this.pnlFooter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 382);
            this.panel1.TabIndex = 10;
            // 
            // lblMien
            // 
            this.lblMien.AutoSize = true;
            this.lblMien.Location = new System.Drawing.Point(6, 153);
            this.lblMien.Name = "lblMien";
            this.lblMien.Size = new System.Drawing.Size(37, 13);
            this.lblMien.TabIndex = 17;
            this.lblMien.Text = "Miền *";
            // 
            // cboMien
            // 
            this.cboMien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMien.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboMien.FormattingEnabled = true;
            this.cboMien.Location = new System.Drawing.Point(108, 150);
            this.cboMien.Name = "cboMien";
            this.cboMien.Size = new System.Drawing.Size(270, 21);
            this.cboMien.TabIndex = 20;
            // 
            // lblTenTat
            // 
            this.lblTenTat.AutoSize = true;
            this.lblTenTat.Location = new System.Drawing.Point(5, 180);
            this.lblTenTat.Name = "lblTenTat";
            this.lblTenTat.Size = new System.Drawing.Size(52, 13);
            this.lblTenTat.TabIndex = 28;
            this.lblTenTat.Text = "Tên Tắt *";
            // 
            // txtTenTat
            // 
            this.txtTenTat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenTat.Location = new System.Drawing.Point(108, 177);
            this.txtTenTat.Name = "txtTenTat";
            this.txtTenTat.Size = new System.Drawing.Size(270, 20);
            this.txtTenTat.TabIndex = 27;
            // 
            // cboStatus
            // 
            this.cboStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(109, 42);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(270, 21);
            this.cboStatus.TabIndex = 25;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(6, 47);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(62, 13);
            this.lblStatus.TabIndex = 26;
            this.lblStatus.Text = "Trạng thái *";
            // 
            // frmThemSuaDonVi
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 382);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmThemSuaDonVi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật đơn vị";
            this.Load += new System.EventHandler(this.frmThemSuaDonVi_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmThemSuaDonVi_KeyDown);
            this.grbInfUser.ResumeLayout(false);
            this.grbInfUser.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.grbInfUserCA.ResumeLayout(false);
            this.grbInfUserCA.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNotation;
        private System.Windows.Forms.TextBox txtUnitID;
        private System.Windows.Forms.Label lblUnitName;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label lblNotation;
        private System.Windows.Forms.GroupBox grbInfUser;
        private System.Windows.Forms.TextBox txtUnitName;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.GroupBox grbInfUserCA;
        private System.Windows.Forms.CheckBox chkVaildTo;
        private System.Windows.Forms.DateTimePicker dpkValidTo;
        private System.Windows.Forms.DateTimePicker dpkValidFrom;
        private System.Windows.Forms.Label lblValidFrom;
        private System.Windows.Forms.Label lblValidTo;
        private System.Windows.Forms.ComboBox cboParent;
        private System.Windows.Forms.Label lblParent;
        private System.Windows.Forms.ComboBox cboTypeUnit;
        private System.Windows.Forms.Label lblTypeUnit;
        private System.Windows.Forms.Label lblMaDv;
        private System.Windows.Forms.TextBox txtMaDv;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboMien;
        private System.Windows.Forms.Label lblMien;
        private System.Windows.Forms.Label lblTenTat;
        private System.Windows.Forms.TextBox txtTenTat;
    }
}