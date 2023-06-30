namespace ES.CA_ManagementUI
{
    partial class ucLienKetUserCert
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
            this.btnEdit = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.rgvUserCert = new Telerik.WinControls.UI.RadGridView();
            this.pnlBottom.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgvUserCert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvUserCert.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(893, 8);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnEdit);
            this.pnlBottom.Controls.Add(this.btnAdd);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 476);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(976, 40);
            this.pnlBottom.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(812, 8);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblHeader);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(976, 40);
            this.pnlTop.TabIndex = 4;
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(976, 40);
            this.lblHeader.TabIndex = 15;
            this.lblHeader.Text = "DANH MỤC PHẦN QUYỀN CHỨNG THƯ - NGƯỜI DÙNG";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rgvUserCert
            // 
            this.rgvUserCert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rgvUserCert.Location = new System.Drawing.Point(0, 40);
            // 
            // rgvUserCert
            // 
            this.rgvUserCert.MasterTemplate.AllowAddNewRow = false;
            this.rgvUserCert.MasterTemplate.AllowColumnReorder = false;
            this.rgvUserCert.MasterTemplate.AllowDeleteRow = false;
            this.rgvUserCert.MasterTemplate.AllowDragToGroup = false;
            this.rgvUserCert.MasterTemplate.AllowEditRow = false;
            this.rgvUserCert.MasterTemplate.AllowRowResize = false;
            this.rgvUserCert.Name = "rgvUserCert";
            this.rgvUserCert.Size = new System.Drawing.Size(976, 436);
            this.rgvUserCert.TabIndex = 6;
            this.rgvUserCert.Text = "radGridView1";
            this.rgvUserCert.GroupByChanged += new Telerik.WinControls.UI.GridViewCollectionChangedEventHandler(this.rgvUserCert_GroupByChanged);
            // 
            // ucLienKetUserCert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rgvUserCert);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Name = "ucLienKetUserCert";
            this.Size = new System.Drawing.Size(976, 516);
            this.Load += new System.EventHandler(this.ucPhanQuyenCertUser_Load);
            this.pnlBottom.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgvUserCert.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvUserCert)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel pnlTop;
        private Telerik.WinControls.UI.RadGridView rgvUserCert;
        private System.Windows.Forms.Label lblHeader;

    }
}
