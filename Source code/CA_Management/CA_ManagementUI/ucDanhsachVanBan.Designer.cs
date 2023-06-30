namespace ES.CA_ManagementUI
{
    partial class ucDanhsachVanBan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDanhsachVanBan));
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.dpkToDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dpkFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.cboUnit = new System.Windows.Forms.ComboBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.cboTypeUnit = new System.Windows.Forms.ComboBox();
            this.lblTypeUnit = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cfgFile = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgFile)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(878, 40);
            this.lblHeader.TabIndex = 18;
            this.lblHeader.Text = "DANH SÁCH VĂN BẢN";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.dpkToDate);
            this.pnlHeader.Controls.Add(this.lblToDate);
            this.pnlHeader.Controls.Add(this.dpkFromDate);
            this.pnlHeader.Controls.Add(this.lblFromDate);
            this.pnlHeader.Controls.Add(this.cboUnit);
            this.pnlHeader.Controls.Add(this.lblUnit);
            this.pnlHeader.Controls.Add(this.cboTypeUnit);
            this.pnlHeader.Controls.Add(this.lblTypeUnit);
            this.pnlHeader.Controls.Add(this.btnRefresh);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 40);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(878, 40);
            this.pnlHeader.TabIndex = 23;
            // 
            // dpkToDate
            // 
            this.dpkToDate.CustomFormat = "dd/MM/yyyy";
            this.dpkToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkToDate.Location = new System.Drawing.Point(652, 9);
            this.dpkToDate.Name = "dpkToDate";
            this.dpkToDate.Size = new System.Drawing.Size(95, 20);
            this.dpkToDate.TabIndex = 10;
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(591, 13);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(53, 13);
            this.lblToDate.TabIndex = 9;
            this.lblToDate.Text = "Đến ngày";
            // 
            // dpkFromDate
            // 
            this.dpkFromDate.CustomFormat = "dd/MM/yyyy";
            this.dpkFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkFromDate.Location = new System.Drawing.Point(488, 9);
            this.dpkFromDate.Name = "dpkFromDate";
            this.dpkFromDate.Size = new System.Drawing.Size(95, 20);
            this.dpkFromDate.TabIndex = 8;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(434, 13);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(46, 13);
            this.lblFromDate.TabIndex = 7;
            this.lblFromDate.Text = "Từ ngày";
            // 
            // cboUnit
            // 
            this.cboUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboUnit.FormattingEnabled = true;
            this.cboUnit.Location = new System.Drawing.Point(238, 9);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Size = new System.Drawing.Size(188, 21);
            this.cboUnit.TabIndex = 6;
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new System.Drawing.Point(192, 13);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(38, 13);
            this.lblUnit.TabIndex = 5;
            this.lblUnit.Text = "Đơn vị";
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
            // lblTypeUnit
            // 
            this.lblTypeUnit.AutoSize = true;
            this.lblTypeUnit.Location = new System.Drawing.Point(6, 13);
            this.lblTypeUnit.Name = "lblTypeUnit";
            this.lblTypeUnit.Size = new System.Drawing.Size(60, 13);
            this.lblTypeUnit.TabIndex = 3;
            this.lblTypeUnit.Text = "Loại đơn vị";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(755, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Xem";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
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
            this.cfgFile.Location = new System.Drawing.Point(0, 80);
            this.cfgFile.Name = "cfgFile";
            this.cfgFile.Rows.DefaultSize = 19;
            this.cfgFile.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.cfgFile.ShowCursor = true;
            this.cfgFile.Size = new System.Drawing.Size(878, 437);
            this.cfgFile.StyleInfo = resources.GetString("cfgFile.StyleInfo");
            this.cfgFile.TabIndex = 24;
            this.cfgFile.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            // 
            // ucDanhsachVanBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cfgFile);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblHeader);
            this.Name = "ucDanhsachVanBan";
            this.Size = new System.Drawing.Size(878, 517);
            this.Load += new System.EventHandler(this.ucDanhSachVanBan_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgFile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnRefresh;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgFile;
        private System.Windows.Forms.ComboBox cboTypeUnit;
        private System.Windows.Forms.Label lblTypeUnit;
        private System.Windows.Forms.DateTimePicker dpkToDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dpkFromDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.ComboBox cboUnit;
        private System.Windows.Forms.Label lblUnit;
    }
}
