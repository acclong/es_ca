namespace ES.CA_ManagementUI
{
    partial class frmLocDonViMulti
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLocDonViMulti));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cfgDonVi = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtSeach = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAddUnit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgDonVi)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cfgDonVi);
            this.panel1.Location = new System.Drawing.Point(12, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(865, 333);
            this.panel1.TabIndex = 0;
            // 
            // cfgDonVi
            // 
            this.cfgDonVi.AllowEditing = false;
            this.cfgDonVi.AllowFiltering = true;
            this.cfgDonVi.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgDonVi.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgDonVi.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgDonVi.ColumnInfo = resources.GetString("cfgDonVi.ColumnInfo");
            this.cfgDonVi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgDonVi.ExtendLastCol = true;
            this.cfgDonVi.Location = new System.Drawing.Point(0, 0);
            this.cfgDonVi.Name = "cfgDonVi";
            this.cfgDonVi.Rows.DefaultSize = 19;
            this.cfgDonVi.ShowCursor = true;
            this.cfgDonVi.Size = new System.Drawing.Size(865, 333);
            this.cfgDonVi.StyleInfo = resources.GetString("cfgDonVi.StyleInfo");
            this.cfgDonVi.TabIndex = 1;
            this.cfgDonVi.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            // 
            // btnDone
            // 
            this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDone.Location = new System.Drawing.Point(721, 393);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 28);
            this.btnDone.TabIndex = 0;
            this.btnDone.Text = "Xong";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(802, 393);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 28);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnAddUnit);
            this.panel3.Controls.Add(this.btnRefresh);
            this.panel3.Controls.Add(this.txtSeach);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(865, 36);
            this.panel3.TabIndex = 2;
            // 
            // txtSeach
            // 
            this.txtSeach.Location = new System.Drawing.Point(61, 8);
            this.txtSeach.Name = "txtSeach";
            this.txtSeach.Size = new System.Drawing.Size(232, 20);
            this.txtSeach.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tìm kiếm";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(299, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "Xem";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAddUnit
            // 
            this.btnAddUnit.Location = new System.Drawing.Point(768, 6);
            this.btnAddUnit.Name = "btnAddUnit";
            this.btnAddUnit.Size = new System.Drawing.Size(94, 23);
            this.btnAddUnit.TabIndex = 11;
            this.btnAddUnit.Text = "Thêm đơn vị";
            this.btnAddUnit.UseVisualStyleBackColor = true;
            this.btnAddUnit.Click += new System.EventHandler(this.btnAddUnit_Click);
            // 
            // frmLocDonViMulti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 433);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmLocDonViMulti";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm kiếm đơn vị";
            this.Load += new System.EventHandler(this.frmLocDonViMulti_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfgDonVi)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgDonVi;
        private System.Windows.Forms.TextBox txtSeach;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnAddUnit;
    }
}