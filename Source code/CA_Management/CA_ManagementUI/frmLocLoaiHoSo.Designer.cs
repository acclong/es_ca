namespace ES.CA_ManagementUI
{
    partial class frmLocLoaiHoSo
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
            this.grbListProfileType = new System.Windows.Forms.GroupBox();
            this.rlvProfileType = new Telerik.WinControls.UI.RadListView();
            this.rbarProfileType = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement2 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement2 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.commandBarLabel3 = new Telerik.WinControls.UI.CommandBarLabel();
            this.drpProfileTypeGroup = new Telerik.WinControls.UI.CommandBarDropDownList();
            this.commandBarSeparator2 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarLabel4 = new Telerik.WinControls.UI.CommandBarLabel();
            this.txtProfileTypeFilter = new Telerik.WinControls.UI.CommandBarTextBox();
            this.grbListProfileType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvProfileType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarProfileType)).BeginInit();
            this.SuspendLayout();
            // 
            // grbListProfileType
            // 
            this.grbListProfileType.Controls.Add(this.rlvProfileType);
            this.grbListProfileType.Controls.Add(this.rbarProfileType);
            this.grbListProfileType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbListProfileType.Location = new System.Drawing.Point(0, 0);
            this.grbListProfileType.Name = "grbListProfileType";
            this.grbListProfileType.Size = new System.Drawing.Size(509, 498);
            this.grbListProfileType.TabIndex = 14;
            this.grbListProfileType.TabStop = false;
            this.grbListProfileType.Text = "Chọn đơn vị";
            // 
            // rlvProfileType
            // 
            this.rlvProfileType.AllowColumnReorder = false;
            this.rlvProfileType.AllowColumnResize = false;
            this.rlvProfileType.AllowEdit = false;
            this.rlvProfileType.AllowRemove = false;
            this.rlvProfileType.AutoScroll = true;
            this.rlvProfileType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rlvProfileType.Location = new System.Drawing.Point(3, 71);
            this.rlvProfileType.Name = "rlvProfileType";
            this.rlvProfileType.Size = new System.Drawing.Size(503, 424);
            this.rlvProfileType.TabIndex = 2;
            this.rlvProfileType.Text = "radListView1";
            // 
            // rbarProfileType
            // 
            this.rbarProfileType.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbarProfileType.Location = new System.Drawing.Point(3, 16);
            this.rbarProfileType.Name = "rbarProfileType";
            this.rbarProfileType.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement2});
            this.rbarProfileType.Size = new System.Drawing.Size(503, 55);
            this.rbarProfileType.TabIndex = 1;
            this.rbarProfileType.Text = "radCommandBar1";
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
            this.drpProfileTypeGroup,
            this.commandBarSeparator2,
            this.commandBarLabel4,
            this.txtProfileTypeFilter});
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
            // drpProfileTypeGroup
            // 
            this.drpProfileTypeGroup.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.drpProfileTypeGroup.DisplayName = "commandBarDropDownList1";
            this.drpProfileTypeGroup.DropDownAnimationEnabled = true;
            this.drpProfileTypeGroup.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpProfileTypeGroup.MaxDropDownItems = 0;
            this.drpProfileTypeGroup.Name = "drpProfileTypeGroup";
            this.drpProfileTypeGroup.Text = "";
            this.drpProfileTypeGroup.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
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
            // txtProfileTypeFilter
            // 
            this.txtProfileTypeFilter.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.txtProfileTypeFilter.DisplayName = "commandBarTextBox1";
            this.txtProfileTypeFilter.Name = "txtProfileTypeFilter";
            this.txtProfileTypeFilter.StretchHorizontally = true;
            this.txtProfileTypeFilter.Text = "";
            this.txtProfileTypeFilter.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // frmLocLoaiHoSo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 498);
            this.Controls.Add(this.grbListProfileType);
            this.Name = "frmLocLoaiHoSo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm kiếm loại hồ sơ";
            this.Load += new System.EventHandler(this.frmLocLoaiHoSo_Load);
            this.grbListProfileType.ResumeLayout(false);
            this.grbListProfileType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvProfileType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarProfileType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbListProfileType;
        private Telerik.WinControls.UI.RadListView rlvProfileType;
        private Telerik.WinControls.UI.RadCommandBar rbarProfileType;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement2;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement2;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel3;
        private Telerik.WinControls.UI.CommandBarDropDownList drpProfileTypeGroup;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator2;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel4;
        private Telerik.WinControls.UI.CommandBarTextBox txtProfileTypeFilter;
    }
}