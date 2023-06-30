namespace ES.CA_ManagementUI
{
    partial class ucDanhMucNhaCungCap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDanhMucNhaCungCap));
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlFill = new System.Windows.Forms.Panel();
            this.cfgCertificates = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlBottom.SuspendLayout();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgCertificates)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnDelete);
            this.pnlBottom.Controls.Add(this.btnEdit);
            this.pnlBottom.Controls.Add(this.btnAdd);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 510);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(780, 40);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(702, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(621, 6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(540, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // pnlFill
            // 
            this.pnlFill.Controls.Add(this.cfgCertificates);
            this.pnlFill.Controls.Add(this.lblHeader);
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(0, 0);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(780, 510);
            this.pnlFill.TabIndex = 2;
            // 
            // cfgCertificates
            // 
            this.cfgCertificates.AllowEditing = false;
            this.cfgCertificates.AllowFiltering = true;
            this.cfgCertificates.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgCertificates.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgCertificates.AutoGenerateColumns = false;
            this.cfgCertificates.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgCertificates.ColumnInfo = resources.GetString("cfgCertificates.ColumnInfo");
            this.cfgCertificates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgCertificates.ExtendLastCol = true;
            this.cfgCertificates.Location = new System.Drawing.Point(0, 40);
            this.cfgCertificates.Name = "cfgCertificates";
            this.cfgCertificates.Rows.DefaultSize = 19;
            this.cfgCertificates.ShowCursor = true;
            this.cfgCertificates.Size = new System.Drawing.Size(780, 470);
            this.cfgCertificates.StyleInfo = resources.GetString("cfgCertificates.StyleInfo");
            this.cfgCertificates.TabIndex = 0;
            this.cfgCertificates.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            this.cfgCertificates.DoubleClick += new System.EventHandler(this.cfgCertificates_DoubleClick);
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(780, 40);
            this.lblHeader.TabIndex = 17;
            this.lblHeader.Text = "DANH MỤC NHÀ CUNG CẤP CHỨNG THƯ SỐ";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucDanhMucNhaCungCap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.pnlBottom);
            this.Name = "ucDanhMucNhaCungCap";
            this.Size = new System.Drawing.Size(780, 550);
            this.Load += new System.EventHandler(this.ucCauHinhNhaCungCap_Load);
            this.pnlBottom.ResumeLayout(false);
            this.pnlFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfgCertificates)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Panel pnlFill;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnDelete;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgCertificates;
    }
}
