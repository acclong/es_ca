namespace ES.CA_ManagementUI
{
    partial class frmXemLogDanhMuc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmXemLogDanhMuc));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cfgType = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgType)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cfgType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(956, 377);
            this.panel1.TabIndex = 19;
            // 
            // cfgType
            // 
            this.cfgType.AllowEditing = false;
            this.cfgType.AllowFiltering = true;
            this.cfgType.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgType.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgType.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgType.ColumnInfo = resources.GetString("cfgType.ColumnInfo");
            this.cfgType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgType.ExtendLastCol = true;
            this.cfgType.Location = new System.Drawing.Point(0, 0);
            this.cfgType.Name = "cfgType";
            this.cfgType.Rows.DefaultSize = 19;
            this.cfgType.ShowCursor = true;
            this.cfgType.Size = new System.Drawing.Size(956, 377);
            this.cfgType.StyleInfo = resources.GetString("cfgType.StyleInfo");
            this.cfgType.TabIndex = 20;
            this.cfgType.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            // 
            // frmXemLogDanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 377);
            this.Controls.Add(this.panel1);
            this.Name = "frmXemLogDanhMuc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmXemLogDanhMuc";
            this.Load += new System.EventHandler(this.frmXemLogDanhMuc_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfgType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgType;
    }
}