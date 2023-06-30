namespace ES.CA_ManagementUI
{
    partial class ucXemVBClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucXemVBClient));
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnSign = new System.Windows.Forms.Button();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnDelFile = new System.Windows.Forms.Button();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.cfgVanBan = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgVanBan)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(770, 40);
            this.lblHeader.TabIndex = 14;
            this.lblHeader.Text = "THÔNG TIN VĂN BẢN";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnSign);
            this.pnlFooter.Controls.Add(this.btnCompare);
            this.pnlFooter.Controls.Add(this.btnDelFile);
            this.pnlFooter.Controls.Add(this.btnAddFile);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 452);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(770, 40);
            this.pnlFooter.TabIndex = 15;
            // 
            // btnSign
            // 
            this.btnSign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSign.Location = new System.Drawing.Point(666, 9);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(94, 23);
            this.btnSign.TabIndex = 5;
            this.btnSign.Text = "Ký văn bản";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // btnCompare
            // 
            this.btnCompare.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompare.Location = new System.Drawing.Point(564, 9);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(94, 23);
            this.btnCompare.TabIndex = 4;
            this.btnCompare.Text = "So sánh Hash";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // btnDelFile
            // 
            this.btnDelFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelFile.Location = new System.Drawing.Point(462, 9);
            this.btnDelFile.Name = "btnDelFile";
            this.btnDelFile.Size = new System.Drawing.Size(94, 23);
            this.btnDelFile.TabIndex = 3;
            this.btnDelFile.Text = "Bỏ văn bản";
            this.btnDelFile.UseVisualStyleBackColor = true;
            this.btnDelFile.Click += new System.EventHandler(this.btnDelFile_Click);
            // 
            // btnAddFile
            // 
            this.btnAddFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFile.Location = new System.Drawing.Point(360, 9);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(94, 23);
            this.btnAddFile.TabIndex = 2;
            this.btnAddFile.Text = "Thêm văn bản";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // cfgVanBan
            // 
            this.cfgVanBan.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.cfgVanBan.AllowFiltering = true;
            this.cfgVanBan.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgVanBan.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgVanBan.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgVanBan.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.cfgVanBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgVanBan.ExtendLastCol = true;
            this.cfgVanBan.Location = new System.Drawing.Point(0, 40);
            this.cfgVanBan.Name = "cfgVanBan";
            this.cfgVanBan.Rows.DefaultSize = 19;
            this.cfgVanBan.Size = new System.Drawing.Size(770, 412);
            this.cfgVanBan.StyleInfo = resources.GetString("cfgVanBan.StyleInfo");
            this.cfgVanBan.TabIndex = 18;
            this.cfgVanBan.Click += new System.EventHandler(this.cfgVanBan_Click);
            // 
            // ucXemVBClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cfgVanBan);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.pnlFooter);
            this.Name = "ucXemVBClient";
            this.Size = new System.Drawing.Size(770, 492);
            this.Load += new System.EventHandler(this.ucXemVBClient_Load);
            this.pnlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfgVanBan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Button btnDelFile;
        private System.Windows.Forms.Button btnAddFile;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgVanBan;
        private System.Windows.Forms.Button btnSign;
    }
}
