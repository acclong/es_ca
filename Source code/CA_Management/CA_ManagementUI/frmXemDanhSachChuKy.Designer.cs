namespace ES.CA_ManagementUI
{
    partial class frmXemDanhSachChuKy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmXemDanhSachChuKy));
            this.cfgVanBan = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cfgVanBan)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // cfgVanBan
            // 
            this.cfgVanBan.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.cfgVanBan.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgVanBan.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgVanBan.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgVanBan.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.cfgVanBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgVanBan.ExtendLastCol = true;
            this.cfgVanBan.Location = new System.Drawing.Point(0, 0);
            this.cfgVanBan.Name = "cfgVanBan";
            this.cfgVanBan.Rows.DefaultSize = 19;
            this.cfgVanBan.Size = new System.Drawing.Size(914, 278);
            this.cfgVanBan.StyleInfo = resources.GetString("cfgVanBan.StyleInfo");
            this.cfgVanBan.TabIndex = 21;
            this.cfgVanBan.Click += new System.EventHandler(this.cfgVanBan_Click);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnDel);
            this.pnlFooter.Controls.Add(this.btnExit);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 278);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(914, 40);
            this.pnlFooter.TabIndex = 20;
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.Location = new System.Drawing.Point(710, 9);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(94, 23);
            this.btnDel.TabIndex = 6;
            this.btnDel.Text = "Xóa chữ ký";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(810, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Đóng";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmDanhSachChungThuSo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 318);
            this.Controls.Add(this.cfgVanBan);
            this.Controls.Add(this.pnlFooter);
            this.Name = "frmDanhSachChungThuSo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách chữ ký số";
            this.Load += new System.EventHandler(this.frmDanhSachChungThuSo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cfgVanBan)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid cfgVanBan;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDel;
    }
}