namespace ES.CA_ManagementUI
{
    partial class frmXemVBThayTheNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmXemVBThayTheNew));
            this.lblHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.cfgFileRelease = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgFileRelease)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(785, 40);
            this.lblHeader.TabIndex = 26;
            this.lblHeader.Text = "DANH SÁCH VĂN BẢN LIÊN QUAN";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 326);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(785, 36);
            this.panel1.TabIndex = 27;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(698, 7);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Đóng";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cfgFileRelease
            // 
            this.cfgFileRelease.AllowEditing = false;
            this.cfgFileRelease.AllowFiltering = true;
            this.cfgFileRelease.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgFileRelease.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgFileRelease.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgFileRelease.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.cfgFileRelease.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgFileRelease.ExtendLastCol = true;
            this.cfgFileRelease.Location = new System.Drawing.Point(0, 40);
            this.cfgFileRelease.Name = "cfgFileRelease";
            this.cfgFileRelease.Rows.DefaultSize = 19;
            this.cfgFileRelease.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
            this.cfgFileRelease.ShowCursor = true;
            this.cfgFileRelease.Size = new System.Drawing.Size(785, 286);
            this.cfgFileRelease.StyleInfo = resources.GetString("cfgFileRelease.StyleInfo");
            this.cfgFileRelease.TabIndex = 31;
            this.cfgFileRelease.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            // 
            // frmXemVBThayTheNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 362);
            this.Controls.Add(this.cfgFileRelease);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmXemVBThayTheNew";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmXemVBThayTheNew_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfgFileRelease)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panel1;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgFileRelease;
        private System.Windows.Forms.Button btnExit;
    }
}