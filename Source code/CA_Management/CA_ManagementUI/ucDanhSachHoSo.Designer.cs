namespace ES.CA_ManagementUI
{
    partial class ucDanhSachHoSo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDanhSachHoSo));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTypeUnit = new System.Windows.Forms.Label();
            this.cboTypeUnit = new System.Windows.Forms.ComboBox();
            this.lblDateType = new System.Windows.Forms.Label();
            this.cboDateType = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblUnit = new System.Windows.Forms.Label();
            this.cboUnit = new System.Windows.Forms.ComboBox();
            this.lblProfileType = new System.Windows.Forms.Label();
            this.cboProfileType = new System.Windows.Forms.ComboBox();
            this.dpkDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlTime = new System.Windows.Forms.Panel();
            this.lblYear = new System.Windows.Forms.Label();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.lblMonth = new System.Windows.Forms.Label();
            this.cboMonth = new System.Windows.Forms.ComboBox();
            this.pnlDownload = new System.Windows.Forms.Panel();
            this.lblDownLoad = new System.Windows.Forms.Label();
            this.cfgFile = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlHeader.SuspendLayout();
            this.pnlTime.SuspendLayout();
            this.pnlDownload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgFile)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.lblTypeUnit);
            this.pnlHeader.Controls.Add(this.cboTypeUnit);
            this.pnlHeader.Controls.Add(this.lblDateType);
            this.pnlHeader.Controls.Add(this.cboDateType);
            this.pnlHeader.Controls.Add(this.btnRefresh);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 40);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1023, 40);
            this.pnlHeader.TabIndex = 26;
            // 
            // lblTypeUnit
            // 
            this.lblTypeUnit.AutoSize = true;
            this.lblTypeUnit.Location = new System.Drawing.Point(6, 13);
            this.lblTypeUnit.Name = "lblTypeUnit";
            this.lblTypeUnit.Size = new System.Drawing.Size(60, 13);
            this.lblTypeUnit.TabIndex = 3;
            this.lblTypeUnit.Text = "Loại đơn vị";
            // 
            // cboTypeUnit
            // 
            this.cboTypeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTypeUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboTypeUnit.FormattingEnabled = true;
            this.cboTypeUnit.Location = new System.Drawing.Point(74, 9);
            this.cboTypeUnit.Name = "cboTypeUnit";
            this.cboTypeUnit.Size = new System.Drawing.Size(110, 21);
            this.cboTypeUnit.TabIndex = 4;
            // 
            // lblDateType
            // 
            this.lblDateType.AutoSize = true;
            this.lblDateType.Enabled = false;
            this.lblDateType.Location = new System.Drawing.Point(192, 13);
            this.lblDateType.Name = "lblDateType";
            this.lblDateType.Size = new System.Drawing.Size(70, 13);
            this.lblDateType.TabIndex = 13;
            this.lblDateType.Text = "Loại thời gian";
            // 
            // cboDateType
            // 
            this.cboDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDateType.Enabled = false;
            this.cboDateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboDateType.FormattingEnabled = true;
            this.cboDateType.Location = new System.Drawing.Point(270, 9);
            this.cboDateType.Name = "cboDateType";
            this.cboDateType.Size = new System.Drawing.Size(110, 21);
            this.cboDateType.TabIndex = 14;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(388, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Xem";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Enabled = false;
            this.lblUnit.Location = new System.Drawing.Point(6, 13);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(38, 13);
            this.lblUnit.TabIndex = 5;
            this.lblUnit.Text = "Đơn vị";
            // 
            // cboUnit
            // 
            this.cboUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnit.Enabled = false;
            this.cboUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboUnit.FormattingEnabled = true;
            this.cboUnit.Location = new System.Drawing.Point(52, 9);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Size = new System.Drawing.Size(149, 21);
            this.cboUnit.TabIndex = 6;
            // 
            // lblProfileType
            // 
            this.lblProfileType.AutoSize = true;
            this.lblProfileType.Enabled = false;
            this.lblProfileType.Location = new System.Drawing.Point(209, 13);
            this.lblProfileType.Name = "lblProfileType";
            this.lblProfileType.Size = new System.Drawing.Size(56, 13);
            this.lblProfileType.TabIndex = 11;
            this.lblProfileType.Text = "Loại hồ sơ";
            // 
            // cboProfileType
            // 
            this.cboProfileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProfileType.Enabled = false;
            this.cboProfileType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboProfileType.FormattingEnabled = true;
            this.cboProfileType.Location = new System.Drawing.Point(273, 9);
            this.cboProfileType.Name = "cboProfileType";
            this.cboProfileType.Size = new System.Drawing.Size(110, 21);
            this.cboProfileType.TabIndex = 12;
            // 
            // dpkDate
            // 
            this.dpkDate.CustomFormat = "dd/MM/yyyy";
            this.dpkDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkDate.Location = new System.Drawing.Point(790, 9);
            this.dpkDate.Name = "dpkDate";
            this.dpkDate.Size = new System.Drawing.Size(95, 20);
            this.dpkDate.TabIndex = 8;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(710, 13);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(72, 13);
            this.lblDate.TabIndex = 7;
            this.lblDate.Text = "Ngày hiệu lực";
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1023, 40);
            this.lblHeader.TabIndex = 25;
            this.lblHeader.Text = "DANH SÁCH HỒ SƠ";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTime
            // 
            this.pnlTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTime.Controls.Add(this.lblProfileType);
            this.pnlTime.Controls.Add(this.cboProfileType);
            this.pnlTime.Controls.Add(this.lblUnit);
            this.pnlTime.Controls.Add(this.cboUnit);
            this.pnlTime.Controls.Add(this.lblYear);
            this.pnlTime.Controls.Add(this.cboYear);
            this.pnlTime.Controls.Add(this.lblMonth);
            this.pnlTime.Controls.Add(this.cboMonth);
            this.pnlTime.Controls.Add(this.lblDate);
            this.pnlTime.Controls.Add(this.dpkDate);
            this.pnlTime.Controls.Add(this.pnlDownload);
            this.pnlTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTime.Location = new System.Drawing.Point(0, 80);
            this.pnlTime.Name = "pnlTime";
            this.pnlTime.Size = new System.Drawing.Size(1023, 40);
            this.pnlTime.TabIndex = 29;
            this.pnlTime.Visible = false;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(391, 13);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(29, 13);
            this.lblYear.TabIndex = 15;
            this.lblYear.Text = "Năm";
            // 
            // cboYear
            // 
            this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Location = new System.Drawing.Point(428, 9);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(110, 21);
            this.cboYear.TabIndex = 16;
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(546, 13);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(38, 13);
            this.lblMonth.TabIndex = 13;
            this.lblMonth.Text = "Tháng";
            // 
            // cboMonth
            // 
            this.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMonth.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboMonth.FormattingEnabled = true;
            this.cboMonth.Location = new System.Drawing.Point(592, 9);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(110, 21);
            this.cboMonth.TabIndex = 14;
            // 
            // pnlDownload
            // 
            this.pnlDownload.Controls.Add(this.lblDownLoad);
            this.pnlDownload.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDownload.Location = new System.Drawing.Point(932, 0);
            this.pnlDownload.Name = "pnlDownload";
            this.pnlDownload.Size = new System.Drawing.Size(89, 38);
            this.pnlDownload.TabIndex = 17;
            // 
            // lblDownLoad
            // 
            this.lblDownLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDownLoad.AutoSize = true;
            this.lblDownLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDownLoad.Location = new System.Drawing.Point(17, 13);
            this.lblDownLoad.Name = "lblDownLoad";
            this.lblDownLoad.Size = new System.Drawing.Size(64, 13);
            this.lblDownLoad.TabIndex = 17;
            this.lblDownLoad.Text = "Lấy tất cả";
            this.lblDownLoad.Click += new System.EventHandler(this.lblDownLoad_Click);
            // 
            // cfgFile
            // 
            this.cfgFile.AllowEditing = false;
            this.cfgFile.AllowFiltering = true;
            this.cfgFile.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgFile.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgFile.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgFile.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.cfgFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgFile.Location = new System.Drawing.Point(0, 120);
            this.cfgFile.Name = "cfgFile";
            this.cfgFile.Rows.DefaultSize = 19;
            this.cfgFile.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
            this.cfgFile.ShowCursor = true;
            this.cfgFile.Size = new System.Drawing.Size(1023, 415);
            this.cfgFile.StyleInfo = resources.GetString("cfgFile.StyleInfo");
            this.cfgFile.TabIndex = 30;
            this.cfgFile.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            // 
            // ucDanhSachHoSo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cfgFile);
            this.Controls.Add(this.pnlTime);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblHeader);
            this.Name = "ucDanhSachHoSo";
            this.Size = new System.Drawing.Size(1023, 535);
            this.Load += new System.EventHandler(this.ucDanhSachHoSo_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlTime.ResumeLayout(false);
            this.pnlTime.PerformLayout();
            this.pnlDownload.ResumeLayout(false);
            this.pnlDownload.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgFile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.DateTimePicker dpkDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.ComboBox cboUnit;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.ComboBox cboTypeUnit;
        private System.Windows.Forms.Label lblTypeUnit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.ComboBox cboProfileType;
        private System.Windows.Forms.Label lblProfileType;
        private System.Windows.Forms.Panel pnlTime;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgFile;
        private System.Windows.Forms.ComboBox cboYear;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.ComboBox cboMonth;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblDateType;
        private System.Windows.Forms.ComboBox cboDateType;
        private System.Windows.Forms.Panel pnlDownload;
        private System.Windows.Forms.Label lblDownLoad;
    }
}
