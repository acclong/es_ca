namespace ES.CA_ManagementUI
{
    partial class frmHSMXemDanhSach
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHSMXemDanhSach));
            this.cfgData = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.cfgData)).BeginInit();
            this.SuspendLayout();
            // 
            // cfgData
            // 
            this.cfgData.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.cfgData.AllowEditing = false;
            this.cfgData.AllowFiltering = true;
            this.cfgData.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgData.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgData.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgData.ColumnInfo = resources.GetString("cfgData.ColumnInfo");
            this.cfgData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgData.ExtendLastCol = true;
            this.cfgData.Location = new System.Drawing.Point(0, 0);
            this.cfgData.Name = "cfgData";
            this.cfgData.Rows.DefaultSize = 19;
            this.cfgData.ShowCursor = true;
            this.cfgData.Size = new System.Drawing.Size(588, 261);
            this.cfgData.StyleInfo = resources.GetString("cfgData.StyleInfo");
            this.cfgData.TabIndex = 17;
            this.cfgData.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            // 
            // frmXemHSM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 261);
            this.Controls.Add(this.cfgData);
            this.Name = "frmXemHSM";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmXemHSM";
            this.Load += new System.EventHandler(this.frmXemHSM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cfgData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid cfgData;

    }
}