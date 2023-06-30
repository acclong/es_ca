namespace ES.CA_ManagementUI
{
    partial class ucXemVBServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucXemVBServer));
            this.lblHeader = new System.Windows.Forms.Label();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.dpkEnd = new System.Windows.Forms.DateTimePicker();
            this.dpkBegin = new System.Windows.Forms.DateTimePicker();
            this.cboLoaiVanBan = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cboDonVi = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnSign = new System.Windows.Forms.Button();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnDelFile = new System.Windows.Forms.Button();
            this.cfgVanBan1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgVanBan1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(873, 48);
            this.lblHeader.TabIndex = 16;
            this.lblHeader.Text = "DANH SÁCH VĂN BẢN";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radPanel1
            // 
            this.radPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radPanel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radPanel1.Controls.Add(this.dpkEnd);
            this.radPanel1.Controls.Add(this.dpkBegin);
            this.radPanel1.Controls.Add(this.cboLoaiVanBan);
            this.radPanel1.Controls.Add(this.label4);
            this.radPanel1.Controls.Add(this.btnRefresh);
            this.radPanel1.Controls.Add(this.label3);
            this.radPanel1.Controls.Add(this.cboDonVi);
            this.radPanel1.Controls.Add(this.label2);
            this.radPanel1.Controls.Add(this.label1);
            this.radPanel1.Location = new System.Drawing.Point(4, 51);
            this.radPanel1.Name = "radPanel1";
            // 
            // 
            // 
            this.radPanel1.RootElement.AccessibleDescription = null;
            this.radPanel1.RootElement.AccessibleName = null;
            this.radPanel1.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 200, 100);
            this.radPanel1.Size = new System.Drawing.Size(866, 36);
            this.radPanel1.TabIndex = 19;
            // 
            // dpkEnd
            // 
            this.dpkEnd.CustomFormat = "dd/MM/yyyy";
            this.dpkEnd.Location = new System.Drawing.Point(690, 8);
            this.dpkEnd.Name = "dpkEnd";
            this.dpkEnd.Size = new System.Drawing.Size(93, 20);
            this.dpkEnd.TabIndex = 8;
            // 
            // dpkBegin
            // 
            this.dpkBegin.CustomFormat = "dd/MM/yyyy";
            this.dpkBegin.Location = new System.Drawing.Point(535, 8);
            this.dpkBegin.Name = "dpkBegin";
            this.dpkBegin.Size = new System.Drawing.Size(93, 20);
            this.dpkBegin.TabIndex = 8;
            // 
            // cboLoaiVanBan
            // 
            this.cboLoaiVanBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiVanBan.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboLoaiVanBan.FormattingEnabled = true;
            this.cboLoaiVanBan.Location = new System.Drawing.Point(86, 8);
            this.cboLoaiVanBan.Name = "cboLoaiVanBan";
            this.cboLoaiVanBan.Size = new System.Drawing.Size(192, 21);
            this.cboLoaiVanBan.TabIndex = 7;
            this.cboLoaiVanBan.SelectedIndexChanged += new System.EventHandler(this.cboLoaiVanBan_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(634, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Tới ngày";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(788, 7);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(71, 23);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Xem";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(482, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Từ ngày";
            // 
            // cboDonVi
            // 
            this.cboDonVi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDonVi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboDonVi.FormattingEnabled = true;
            this.cboDonVi.Location = new System.Drawing.Point(330, 8);
            this.cboDonVi.Name = "cboDonVi";
            this.cboDonVi.Size = new System.Drawing.Size(146, 21);
            this.cboDonVi.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đơn vị";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loại văn bản";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnSign);
            this.pnlFooter.Controls.Add(this.btnCompare);
            this.pnlFooter.Controls.Add(this.btnDelFile);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 459);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(873, 40);
            this.pnlFooter.TabIndex = 18;
            // 
            // btnSign
            // 
            this.btnSign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSign.Location = new System.Drawing.Point(667, 9);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(94, 23);
            this.btnSign.TabIndex = 5;
            this.btnSign.Text = "Ký văn bản";
            this.btnSign.UseVisualStyleBackColor = true;
            // 
            // btnCompare
            // 
            this.btnCompare.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompare.Location = new System.Drawing.Point(567, 9);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(94, 23);
            this.btnCompare.TabIndex = 4;
            this.btnCompare.Text = "So sánh Hash";
            this.btnCompare.UseVisualStyleBackColor = true;
            // 
            // btnDelFile
            // 
            this.btnDelFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelFile.Location = new System.Drawing.Point(769, 9);
            this.btnDelFile.Name = "btnDelFile";
            this.btnDelFile.Size = new System.Drawing.Size(94, 23);
            this.btnDelFile.TabIndex = 3;
            this.btnDelFile.Text = "Hủy văn bản";
            this.btnDelFile.UseVisualStyleBackColor = true;
            // 
            // cfgVanBan1
            // 
            this.cfgVanBan1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.cfgVanBan1.AllowEditing = false;
            this.cfgVanBan1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgVanBan1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgVanBan1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cfgVanBan1.AutoGenerateColumns = false;
            this.cfgVanBan1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgVanBan1.ColumnInfo = "11,1,0,0,0,95,Columns:8{AllowSorting:False;AllowEditing:False;}\t";
            this.cfgVanBan1.ExtendLastCol = true;
            this.cfgVanBan1.Location = new System.Drawing.Point(8, 93);
            this.cfgVanBan1.Name = "cfgVanBan1";
            this.cfgVanBan1.Rows.DefaultSize = 19;
            this.cfgVanBan1.Size = new System.Drawing.Size(855, 360);
            this.cfgVanBan1.StyleInfo = resources.GetString("cfgVanBan1.StyleInfo");
            this.cfgVanBan1.TabIndex = 21;
            this.cfgVanBan1.Click += new System.EventHandler(this.cfgVanBan1_Click);
            // 
            // ucXemVBServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cfgVanBan1);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.lblHeader);
            this.Name = "ucXemVBServer";
            this.Size = new System.Drawing.Size(873, 499);
            this.Load += new System.EventHandler(this.ucXemVBServer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfgVanBan1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboDonVi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Button btnDelFile;
        private System.Windows.Forms.ComboBox cboLoaiVanBan;
        private System.Windows.Forms.DateTimePicker dpkBegin;
        private System.Windows.Forms.DateTimePicker dpkEnd;
        private System.Windows.Forms.Label label4;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgVanBan1;
    }
}
