namespace ES.CA_ManagementUI
{
    partial class frmXemLogKy
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.cfgVanBan1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.cfgVanBan1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(895, 48);
            this.lblHeader.TabIndex = 19;
            this.lblHeader.Text = "DANH SÁCH LOG KÝ";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cfgVanBan1
            // 
            this.cfgVanBan1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.cfgVanBan1.AllowEditing = false;
            this.cfgVanBan1.AutoResize = true;
            this.cfgVanBan1.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.cfgVanBan1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgVanBan1.ExtendLastCol = true;
            this.cfgVanBan1.Location = new System.Drawing.Point(0, 48);
            this.cfgVanBan1.Name = "cfgVanBan1";
            this.cfgVanBan1.Rows.DefaultSize = 19;
            this.cfgVanBan1.Size = new System.Drawing.Size(895, 336);
            this.cfgVanBan1.TabIndex = 20;
            this.cfgVanBan1.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            // 
            // frmXemLogKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 384);
            this.Controls.Add(this.cfgVanBan1);
            this.Controls.Add(this.lblHeader);
            this.Name = "frmXemLogKy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmXemLogKy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cfgVanBan1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgVanBan1;
    }
}