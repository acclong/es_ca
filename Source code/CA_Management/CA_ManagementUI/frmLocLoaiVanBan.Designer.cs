namespace ES.CA_ManagementUI
{
    partial class frmLocLoaiVanBan
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
            this.grbListFileType = new System.Windows.Forms.GroupBox();
            this.rlvFileType = new Telerik.WinControls.UI.RadListView();
            this.rbarFileType = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement2 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement2 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.commandBarLabel3 = new Telerik.WinControls.UI.CommandBarLabel();
            this.drpFileTypeGroup = new Telerik.WinControls.UI.CommandBarDropDownList();
            this.commandBarSeparator2 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarLabel4 = new Telerik.WinControls.UI.CommandBarLabel();
            this.tbFileTypeFilter = new Telerik.WinControls.UI.CommandBarTextBox();
            this.grbListFileType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvFileType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarFileType)).BeginInit();
            this.SuspendLayout();
            // 
            // grbListFileType
            // 
            this.grbListFileType.Controls.Add(this.rlvFileType);
            this.grbListFileType.Controls.Add(this.rbarFileType);
            this.grbListFileType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbListFileType.Location = new System.Drawing.Point(0, 0);
            this.grbListFileType.Name = "grbListFileType";
            this.grbListFileType.Size = new System.Drawing.Size(509, 498);
            this.grbListFileType.TabIndex = 13;
            this.grbListFileType.TabStop = false;
            this.grbListFileType.Text = "Chọn đơn vị";
            // 
            // rlvFileType
            // 
            this.rlvFileType.AllowColumnReorder = false;
            this.rlvFileType.AllowColumnResize = false;
            this.rlvFileType.AllowEdit = false;
            this.rlvFileType.AllowRemove = false;
            this.rlvFileType.AutoScroll = true;
            this.rlvFileType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rlvFileType.Location = new System.Drawing.Point(3, 46);
            this.rlvFileType.Name = "rlvFileType";
            this.rlvFileType.Size = new System.Drawing.Size(503, 449);
            this.rlvFileType.TabIndex = 2;
            this.rlvFileType.Text = "radListView1";
            // 
            // rbarFileType
            // 
            this.rbarFileType.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbarFileType.Location = new System.Drawing.Point(3, 16);
            this.rbarFileType.Name = "rbarFileType";
            this.rbarFileType.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement2});
            this.rbarFileType.Size = new System.Drawing.Size(503, 30);
            this.rbarFileType.TabIndex = 1;
            this.rbarFileType.Text = "radCommandBar1";
            // 
            // commandBarRowElement2
            // 
            this.commandBarRowElement2.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarRowElement2.MinSize = new System.Drawing.Size(25, 25);
            this.commandBarRowElement2.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.commandBarStripElement2});
            this.commandBarRowElement2.Text = "";
            this.commandBarRowElement2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // commandBarStripElement2
            // 
            this.commandBarStripElement2.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement2.DisplayName = "commandBarStripElement1";
            this.commandBarStripElement2.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.commandBarLabel3,
            this.drpFileTypeGroup,
            this.commandBarSeparator2,
            this.commandBarLabel4,
            this.tbFileTypeFilter});
            this.commandBarStripElement2.Name = "commandBarStripElement1";
            // 
            // 
            // 
            this.commandBarStripElement2.OverflowButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            this.commandBarStripElement2.StretchHorizontally = true;
            this.commandBarStripElement2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            ((Telerik.WinControls.UI.RadCommandBarOverflowButton)(this.commandBarStripElement2.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // commandBarLabel3
            // 
            this.commandBarLabel3.AccessibleDescription = "Nhóm:";
            this.commandBarLabel3.AccessibleName = "Nhóm:";
            this.commandBarLabel3.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarLabel3.DisplayName = "commandBarLabel1";
            this.commandBarLabel3.Name = "commandBarLabel3";
            this.commandBarLabel3.Text = "Nhóm:";
            this.commandBarLabel3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // drpFileTypeGroup
            // 
            this.drpFileTypeGroup.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.drpFileTypeGroup.DisplayName = "commandBarDropDownList1";
            this.drpFileTypeGroup.DropDownAnimationEnabled = true;
            this.drpFileTypeGroup.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpFileTypeGroup.MaxDropDownItems = 0;
            this.drpFileTypeGroup.Name = "drpFileTypeGroup";
            this.drpFileTypeGroup.Text = "";
            this.drpFileTypeGroup.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // commandBarSeparator2
            // 
            this.commandBarSeparator2.AccessibleDescription = "commandBarSeparator1";
            this.commandBarSeparator2.AccessibleName = "commandBarSeparator1";
            this.commandBarSeparator2.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarSeparator2.DisplayName = "commandBarSeparator1";
            this.commandBarSeparator2.Name = "commandBarSeparator2";
            this.commandBarSeparator2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarSeparator2.VisibleInOverflowMenu = false;
            // 
            // commandBarLabel4
            // 
            this.commandBarLabel4.AccessibleDescription = "Lọc:";
            this.commandBarLabel4.AccessibleName = "Lọc:";
            this.commandBarLabel4.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarLabel4.DisplayName = "commandBarLabel2";
            this.commandBarLabel4.Name = "commandBarLabel4";
            this.commandBarLabel4.Text = "Lọc:";
            this.commandBarLabel4.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // tbFileTypeFilter
            // 
            this.tbFileTypeFilter.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.tbFileTypeFilter.DisplayName = "commandBarTextBox1";
            this.tbFileTypeFilter.Name = "tbFileTypeFilter";
            this.tbFileTypeFilter.StretchHorizontally = true;
            this.tbFileTypeFilter.Text = "";
            this.tbFileTypeFilter.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // frmLocLoaiVanBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 498);
            this.Controls.Add(this.grbListFileType);
            this.Name = "frmLocLoaiVanBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm kiếm loại văn bản";
            this.Load += new System.EventHandler(this.frmLocLoaiVanBan_Load);
            this.grbListFileType.ResumeLayout(false);
            this.grbListFileType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvFileType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarFileType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbListFileType;
        private Telerik.WinControls.UI.RadListView rlvFileType;
        private Telerik.WinControls.UI.RadCommandBar rbarFileType;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement2;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement2;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel3;
        private Telerik.WinControls.UI.CommandBarDropDownList drpFileTypeGroup;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator2;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel4;
        private Telerik.WinControls.UI.CommandBarTextBox tbFileTypeFilter;
    }
}