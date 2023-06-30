namespace ES.CA_ManagementUI
{
    partial class frmThemSuaLoaiHoSo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThemSuaLoaiHoSo));
            this.grbThongTin = new System.Windows.Forms.GroupBox();
            this.cboDateType = new System.Windows.Forms.ComboBox();
            this.cboUnitType = new System.Windows.Forms.ComboBox();
            this.lblUnitType = new System.Windows.Forms.Label();
            this.lblDateType = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtIdProfileType = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblIdProfileType = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grbQuanLy = new System.Windows.Forms.GroupBox();
            this.chkDateEnd = new System.Windows.Forms.CheckBox();
            this.dpkDateEnd = new System.Windows.Forms.DateTimePicker();
            this.dpkDateStart = new System.Windows.Forms.DateTimePicker();
            this.lblDateEnd = new System.Windows.Forms.Label();
            this.lblDateStart = new System.Windows.Forms.Label();
            this.grbVanBan = new System.Windows.Forms.GroupBox();
            this.cfgFileProfile = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.grbThongTin.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.grbQuanLy.SuspendLayout();
            this.grbVanBan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgFileProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // grbThongTin
            // 
            this.grbThongTin.Controls.Add(this.cboDateType);
            this.grbThongTin.Controls.Add(this.cboUnitType);
            this.grbThongTin.Controls.Add(this.lblUnitType);
            this.grbThongTin.Controls.Add(this.lblDateType);
            this.grbThongTin.Controls.Add(this.txtName);
            this.grbThongTin.Controls.Add(this.txtIdProfileType);
            this.grbThongTin.Controls.Add(this.lblName);
            this.grbThongTin.Controls.Add(this.lblIdProfileType);
            this.grbThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbThongTin.Location = new System.Drawing.Point(0, 0);
            this.grbThongTin.Name = "grbThongTin";
            this.grbThongTin.Size = new System.Drawing.Size(515, 136);
            this.grbThongTin.TabIndex = 4;
            this.grbThongTin.TabStop = false;
            this.grbThongTin.Text = "Thông tin loại hồ sơ";
            // 
            // cboDateType
            // 
            this.cboDateType.FormattingEnabled = true;
            this.cboDateType.Location = new System.Drawing.Point(113, 102);
            this.cboDateType.Name = "cboDateType";
            this.cboDateType.Size = new System.Drawing.Size(253, 21);
            this.cboDateType.TabIndex = 12;
            // 
            // cboUnitType
            // 
            this.cboUnitType.FormattingEnabled = true;
            this.cboUnitType.Location = new System.Drawing.Point(113, 73);
            this.cboUnitType.Name = "cboUnitType";
            this.cboUnitType.Size = new System.Drawing.Size(253, 21);
            this.cboUnitType.TabIndex = 11;
            // 
            // lblUnitType
            // 
            this.lblUnitType.AutoSize = true;
            this.lblUnitType.Location = new System.Drawing.Point(13, 76);
            this.lblUnitType.Name = "lblUnitType";
            this.lblUnitType.Size = new System.Drawing.Size(64, 13);
            this.lblUnitType.TabIndex = 10;
            this.lblUnitType.Text = "Loại đơn vị*";
            // 
            // lblDateType
            // 
            this.lblDateType.AutoSize = true;
            this.lblDateType.Location = new System.Drawing.Point(13, 105);
            this.lblDateType.Name = "lblDateType";
            this.lblDateType.Size = new System.Drawing.Size(57, 13);
            this.lblDateType.TabIndex = 9;
            this.lblDateType.Text = "Loại ngày*";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(113, 47);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(253, 20);
            this.txtName.TabIndex = 3;
            // 
            // txtIdProfileType
            // 
            this.txtIdProfileType.Location = new System.Drawing.Point(113, 19);
            this.txtIdProfileType.Name = "txtIdProfileType";
            this.txtIdProfileType.ReadOnly = true;
            this.txtIdProfileType.Size = new System.Drawing.Size(130, 20);
            this.txtIdProfileType.TabIndex = 2;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(13, 50);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(78, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Tên loại hồ sơ*";
            // 
            // lblIdProfileType
            // 
            this.lblIdProfileType.AutoSize = true;
            this.lblIdProfileType.Location = new System.Drawing.Point(13, 22);
            this.lblIdProfileType.Name = "lblIdProfileType";
            this.lblIdProfileType.Size = new System.Drawing.Size(18, 13);
            this.lblIdProfileType.TabIndex = 0;
            this.lblIdProfileType.Text = "ID";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.label1);
            this.pnlFooter.Controls.Add(this.btnExit);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 426);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(515, 55);
            this.pnlFooter.TabIndex = 5;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnExit.Location = new System.Drawing.Point(261, 24);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Đóng";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Location = new System.Drawing.Point(180, 24);
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
            this.grbQuanLy.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbQuanLy.Location = new System.Drawing.Point(0, 136);
            this.grbQuanLy.Name = "grbQuanLy";
            this.grbQuanLy.Size = new System.Drawing.Size(515, 80);
            this.grbQuanLy.TabIndex = 7;
            this.grbQuanLy.TabStop = false;
            this.grbQuanLy.Text = "Thông tin quản lý";
            // 
            // chkDateEnd
            // 
            this.chkDateEnd.AutoSize = true;
            this.chkDateEnd.Location = new System.Drawing.Point(113, 50);
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
            this.dpkDateEnd.Size = new System.Drawing.Size(231, 20);
            this.dpkDateEnd.TabIndex = 5;
            this.dpkDateEnd.Visible = false;
            // 
            // dpkDateStart
            // 
            this.dpkDateStart.CustomFormat = "dd/MM/yyyy";
            this.dpkDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkDateStart.Location = new System.Drawing.Point(113, 19);
            this.dpkDateStart.Name = "dpkDateStart";
            this.dpkDateStart.Size = new System.Drawing.Size(253, 20);
            this.dpkDateStart.TabIndex = 3;
            // 
            // lblDateEnd
            // 
            this.lblDateEnd.AutoSize = true;
            this.lblDateEnd.Location = new System.Drawing.Point(13, 51);
            this.lblDateEnd.Name = "lblDateEnd";
            this.lblDateEnd.Size = new System.Drawing.Size(74, 13);
            this.lblDateEnd.TabIndex = 2;
            this.lblDateEnd.Text = "Ngày kết thúc";
            // 
            // lblDateStart
            // 
            this.lblDateStart.AutoSize = true;
            this.lblDateStart.Location = new System.Drawing.Point(13, 23);
            this.lblDateStart.Name = "lblDateStart";
            this.lblDateStart.Size = new System.Drawing.Size(78, 13);
            this.lblDateStart.TabIndex = 1;
            this.lblDateStart.Text = "Ngày áp dụng*";
            // 
            // grbVanBan
            // 
            this.grbVanBan.Controls.Add(this.cfgFileProfile);
            this.grbVanBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbVanBan.Location = new System.Drawing.Point(0, 216);
            this.grbVanBan.Name = "grbVanBan";
            this.grbVanBan.Size = new System.Drawing.Size(515, 210);
            this.grbVanBan.TabIndex = 8;
            this.grbVanBan.TabStop = false;
            this.grbVanBan.Text = "Danh sách văn bản *";
            // 
            // cfgFileProfile
            // 
            this.cfgFileProfile.AllowEditing = false;
            this.cfgFileProfile.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgFileProfile.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgFileProfile.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgFileProfile.ColumnInfo = resources.GetString("cfgFileProfile.ColumnInfo");
            this.cfgFileProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgFileProfile.ExtendLastCol = true;
            this.cfgFileProfile.Location = new System.Drawing.Point(3, 16);
            this.cfgFileProfile.Name = "cfgFileProfile";
            this.cfgFileProfile.Rows.DefaultSize = 19;
            this.cfgFileProfile.ShowCursor = true;
            this.cfgFileProfile.Size = new System.Drawing.Size(509, 191);
            this.cfgFileProfile.StyleInfo = resources.GetString("cfgFileProfile.StyleInfo");
            this.cfgFileProfile.TabIndex = 3;
            this.cfgFileProfile.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            this.cfgFileProfile.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.cfgFileProfile_AfterEdit);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(4, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "* Lưu ý: Một loại văn bản có thể có nhiều giai đoạn áp dụng";
            // 
            // frmThemSuaLoaiHoSo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 481);
            this.Controls.Add(this.grbVanBan);
            this.Controls.Add(this.grbQuanLy);
            this.Controls.Add(this.grbThongTin);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmThemSuaLoaiHoSo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật Loại hồ sơ";
            this.Load += new System.EventHandler(this.frmThemSuaLoaiHoSo_Load);
            this.grbThongTin.ResumeLayout(false);
            this.grbThongTin.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.grbQuanLy.ResumeLayout(false);
            this.grbQuanLy.PerformLayout();
            this.grbVanBan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfgFileProfile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbThongTin;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtIdProfileType;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblIdProfileType;
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
        private System.Windows.Forms.GroupBox grbVanBan;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgFileProfile;
        private System.Windows.Forms.Label label1;
    }
}