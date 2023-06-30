namespace ES.CA_ManagementUI
{
    partial class frmThemSuaLoaiVanBan
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
            this.grbThongTin = new System.Windows.Forms.GroupBox();
            this.txtNotation = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtIdFileType = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblNotation = new System.Windows.Forms.Label();
            this.lblIdFileType = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grbQuanLy = new System.Windows.Forms.GroupBox();
            this.chkDateEnd = new System.Windows.Forms.CheckBox();
            this.dpkDateEnd = new System.Windows.Forms.DateTimePicker();
            this.dpkDateStart = new System.Windows.Forms.DateTimePicker();
            this.lblDateEnd = new System.Windows.Forms.Label();
            this.lblDateStart = new System.Windows.Forms.Label();
            this.lblUnitType = new System.Windows.Forms.Label();
            this.lblDateType = new System.Windows.Forms.Label();
            this.cboUnitType = new System.Windows.Forms.ComboBox();
            this.cboDateType = new System.Windows.Forms.ComboBox();
            this.grbThongTin.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.grbQuanLy.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbThongTin
            // 
            this.grbThongTin.Controls.Add(this.cboDateType);
            this.grbThongTin.Controls.Add(this.cboUnitType);
            this.grbThongTin.Controls.Add(this.lblUnitType);
            this.grbThongTin.Controls.Add(this.lblDateType);
            this.grbThongTin.Controls.Add(this.txtNotation);
            this.grbThongTin.Controls.Add(this.txtName);
            this.grbThongTin.Controls.Add(this.txtIdFileType);
            this.grbThongTin.Controls.Add(this.lblName);
            this.grbThongTin.Controls.Add(this.lblNotation);
            this.grbThongTin.Controls.Add(this.lblIdFileType);
            this.grbThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbThongTin.Location = new System.Drawing.Point(0, 0);
            this.grbThongTin.Name = "grbThongTin";
            this.grbThongTin.Size = new System.Drawing.Size(384, 162);
            this.grbThongTin.TabIndex = 0;
            this.grbThongTin.TabStop = false;
            this.grbThongTin.Text = "Thông tin Loại văn bản";
            // 
            // txtNotation
            // 
            this.txtNotation.Location = new System.Drawing.Point(112, 75);
            this.txtNotation.Name = "txtNotation";
            this.txtNotation.Size = new System.Drawing.Size(253, 20);
            this.txtNotation.TabIndex = 4;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(112, 47);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(253, 20);
            this.txtName.TabIndex = 3;
            // 
            // txtIdFileType
            // 
            this.txtIdFileType.Location = new System.Drawing.Point(112, 19);
            this.txtIdFileType.Name = "txtIdFileType";
            this.txtIdFileType.Size = new System.Drawing.Size(130, 20);
            this.txtIdFileType.TabIndex = 2;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 50);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(91, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Tên loại văn bản*";
            // 
            // lblNotation
            // 
            this.lblNotation.AutoSize = true;
            this.lblNotation.Location = new System.Drawing.Point(12, 78);
            this.lblNotation.Name = "lblNotation";
            this.lblNotation.Size = new System.Drawing.Size(46, 13);
            this.lblNotation.TabIndex = 0;
            this.lblNotation.Text = "Ký hiệu*";
            // 
            // lblIdFileType
            // 
            this.lblIdFileType.AutoSize = true;
            this.lblIdFileType.Location = new System.Drawing.Point(12, 22);
            this.lblIdFileType.Name = "lblIdFileType";
            this.lblIdFileType.Size = new System.Drawing.Size(18, 13);
            this.lblIdFileType.TabIndex = 0;
            this.lblIdFileType.Text = "ID";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnExit);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 243);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(384, 40);
            this.pnlFooter.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(196, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Đóng";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(113, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grbQuanLy
            // 
            this.grbQuanLy.Controls.Add(this.chkDateEnd);
            this.grbQuanLy.Controls.Add(this.dpkDateEnd);
            this.grbQuanLy.Controls.Add(this.dpkDateStart);
            this.grbQuanLy.Controls.Add(this.lblDateEnd);
            this.grbQuanLy.Controls.Add(this.lblDateStart);
            this.grbQuanLy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbQuanLy.Location = new System.Drawing.Point(0, 162);
            this.grbQuanLy.Name = "grbQuanLy";
            this.grbQuanLy.Size = new System.Drawing.Size(384, 81);
            this.grbQuanLy.TabIndex = 3;
            this.grbQuanLy.TabStop = false;
            this.grbQuanLy.Text = "Thông tin quản lý";
            // 
            // chkDateEnd
            // 
            this.chkDateEnd.AutoSize = true;
            this.chkDateEnd.Location = new System.Drawing.Point(112, 50);
            this.chkDateEnd.Name = "chkDateEnd";
            this.chkDateEnd.Size = new System.Drawing.Size(15, 14);
            this.chkDateEnd.TabIndex = 4;
            this.chkDateEnd.UseVisualStyleBackColor = true;
            this.chkDateEnd.CheckedChanged += new System.EventHandler(this.chkDateEnd_CheckedChanged);
            // 
            // dpkDateEnd
            // 
            this.dpkDateEnd.CustomFormat = "dd/MM/yyyy";
            this.dpkDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkDateEnd.Location = new System.Drawing.Point(133, 47);
            this.dpkDateEnd.Name = "dpkDateEnd";
            this.dpkDateEnd.Size = new System.Drawing.Size(232, 20);
            this.dpkDateEnd.TabIndex = 5;
            this.dpkDateEnd.Visible = false;
            // 
            // dpkDateStart
            // 
            this.dpkDateStart.CustomFormat = "dd/MM/yyyy";
            this.dpkDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkDateStart.Location = new System.Drawing.Point(112, 19);
            this.dpkDateStart.Name = "dpkDateStart";
            this.dpkDateStart.Size = new System.Drawing.Size(253, 20);
            this.dpkDateStart.TabIndex = 3;
            // 
            // lblDateEnd
            // 
            this.lblDateEnd.AutoSize = true;
            this.lblDateEnd.Location = new System.Drawing.Point(12, 51);
            this.lblDateEnd.Name = "lblDateEnd";
            this.lblDateEnd.Size = new System.Drawing.Size(74, 13);
            this.lblDateEnd.TabIndex = 2;
            this.lblDateEnd.Text = "Ngày kết thúc";
            // 
            // lblDateStart
            // 
            this.lblDateStart.AutoSize = true;
            this.lblDateStart.Location = new System.Drawing.Point(12, 23);
            this.lblDateStart.Name = "lblDateStart";
            this.lblDateStart.Size = new System.Drawing.Size(78, 13);
            this.lblDateStart.TabIndex = 1;
            this.lblDateStart.Text = "Ngày áp dụng*";
            // 
            // lblUnitType
            // 
            this.lblUnitType.AutoSize = true;
            this.lblUnitType.Location = new System.Drawing.Point(12, 106);
            this.lblUnitType.Name = "lblUnitType";
            this.lblUnitType.Size = new System.Drawing.Size(64, 13);
            this.lblUnitType.TabIndex = 6;
            this.lblUnitType.Text = "Loại đơn vị*";
            // 
            // lblDateType
            // 
            this.lblDateType.AutoSize = true;
            this.lblDateType.Location = new System.Drawing.Point(12, 135);
            this.lblDateType.Name = "lblDateType";
            this.lblDateType.Size = new System.Drawing.Size(57, 13);
            this.lblDateType.TabIndex = 5;
            this.lblDateType.Text = "Loại ngày*";
            // 
            // cboUnitType
            // 
            this.cboUnitType.FormattingEnabled = true;
            this.cboUnitType.Location = new System.Drawing.Point(112, 103);
            this.cboUnitType.Name = "cboUnitType";
            this.cboUnitType.Size = new System.Drawing.Size(253, 21);
            this.cboUnitType.TabIndex = 7;
            // 
            // cboDateType
            // 
            this.cboDateType.FormattingEnabled = true;
            this.cboDateType.Location = new System.Drawing.Point(112, 132);
            this.cboDateType.Name = "cboDateType";
            this.cboDateType.Size = new System.Drawing.Size(253, 21);
            this.cboDateType.TabIndex = 8;
            // 
            // frmThemSuaLoaiVanBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 283);
            this.Controls.Add(this.grbQuanLy);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.grbThongTin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmThemSuaLoaiVanBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật Loại văn bản";
            this.Load += new System.EventHandler(this.frmThemSuaLoaiVanBan_Load);
            this.grbThongTin.ResumeLayout(false);
            this.grbThongTin.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.grbQuanLy.ResumeLayout(false);
            this.grbQuanLy.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbThongTin;
        private System.Windows.Forms.TextBox txtNotation;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtIdFileType;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblNotation;
        private System.Windows.Forms.Label lblIdFileType;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grbQuanLy;
        private System.Windows.Forms.CheckBox chkDateEnd;
        private System.Windows.Forms.DateTimePicker dpkDateEnd;
        private System.Windows.Forms.DateTimePicker dpkDateStart;
        private System.Windows.Forms.Label lblDateEnd;
        private System.Windows.Forms.Label lblDateStart;
        private System.Windows.Forms.ComboBox cboDateType;
        private System.Windows.Forms.ComboBox cboUnitType;
        private System.Windows.Forms.Label lblUnitType;
        private System.Windows.Forms.Label lblDateType;
    }
}