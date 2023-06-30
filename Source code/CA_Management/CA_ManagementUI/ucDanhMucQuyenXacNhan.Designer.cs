namespace ES.CA_ManagementUI
{
    partial class ucDanhMucQuyenXacNhan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDanhMucQuyenXacNhan));
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.cboLoaiVB = new System.Windows.Forms.ComboBox();
            this.lblLoaiVB = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cfgQuyen = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlFooter.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgQuyen)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnEdit);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 397);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(739, 40);
            this.pnlFooter.TabIndex = 30;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(627, 9);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(104, 23);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Cập nhật";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(739, 40);
            this.lblHeader.TabIndex = 29;
            this.lblHeader.Text = "DANH MỤC CẤU HÌNH XÁC NHẬN VĂN BẢN";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.cboLoaiVB);
            this.pnlHeader.Controls.Add(this.lblLoaiVB);
            this.pnlHeader.Controls.Add(this.btnRefresh);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 40);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(739, 40);
            this.pnlHeader.TabIndex = 31;
            // 
            // cboLoaiVB
            // 
            this.cboLoaiVB.FormattingEnabled = true;
            this.cboLoaiVB.Location = new System.Drawing.Point(80, 10);
            this.cboLoaiVB.Name = "cboLoaiVB";
            this.cboLoaiVB.Size = new System.Drawing.Size(219, 21);
            this.cboLoaiVB.TabIndex = 8;
            // 
            // lblLoaiVB
            // 
            this.lblLoaiVB.AutoSize = true;
            this.lblLoaiVB.Location = new System.Drawing.Point(5, 13);
            this.lblLoaiVB.Name = "lblLoaiVB";
            this.lblLoaiVB.Size = new System.Drawing.Size(69, 13);
            this.lblLoaiVB.TabIndex = 7;
            this.lblLoaiVB.Text = "Loại văn bản";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(305, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Xem";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cfgQuyen
            // 
            this.cfgQuyen.AllowEditing = false;
            this.cfgQuyen.AllowFiltering = true;
            this.cfgQuyen.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgQuyen.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgQuyen.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgQuyen.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.cfgQuyen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgQuyen.ExtendLastCol = true;
            this.cfgQuyen.Location = new System.Drawing.Point(0, 80);
            this.cfgQuyen.Name = "cfgQuyen";
            this.cfgQuyen.Rows.DefaultSize = 19;
            this.cfgQuyen.ShowCursor = true;
            this.cfgQuyen.Size = new System.Drawing.Size(739, 317);
            this.cfgQuyen.StyleInfo = resources.GetString("cfgQuyen.StyleInfo");
            this.cfgQuyen.TabIndex = 32;
            this.cfgQuyen.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            // 
            // ucDanhMucQuyenXacNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cfgQuyen);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.lblHeader);
            this.Name = "ucDanhMucQuyenXacNhan";
            this.Size = new System.Drawing.Size(739, 437);
            this.Load += new System.EventHandler(this.ucDanhMucQuyenXacNhan_Load);
            this.pnlFooter.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgQuyen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblLoaiVB;
        private System.Windows.Forms.Button btnRefresh;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgQuyen;
        private System.Windows.Forms.ComboBox cboLoaiVB;
    }
}
