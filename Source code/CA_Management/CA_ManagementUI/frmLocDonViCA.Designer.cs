namespace ES.CA_ManagementUI
{
    partial class frmLocDonViCA
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
            this.grbUnit = new System.Windows.Forms.GroupBox();
            this.rlvUnit = new Telerik.WinControls.UI.RadListView();
            this.rbarUnit = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement2 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement2 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.commandBarLabel3 = new Telerik.WinControls.UI.CommandBarLabel();
            this.drpUnitGroup = new Telerik.WinControls.UI.CommandBarDropDownList();
            this.commandBarSeparator2 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarLabel4 = new Telerik.WinControls.UI.CommandBarLabel();
            this.tbUnitFilter = new Telerik.WinControls.UI.CommandBarTextBox();
            this.grbUnit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarUnit)).BeginInit();
            this.SuspendLayout();
            // 
            // grbUnit
            // 
            this.grbUnit.Controls.Add(this.rlvUnit);
            this.grbUnit.Controls.Add(this.rbarUnit);
            this.grbUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbUnit.Location = new System.Drawing.Point(0, 0);
            this.grbUnit.Name = "grbUnit";
            this.grbUnit.Size = new System.Drawing.Size(428, 524);
            this.grbUnit.TabIndex = 2;
            this.grbUnit.TabStop = false;
            this.grbUnit.Text = "Đơn vị";
            // 
            // rlvUnit
            // 
            this.rlvUnit.AllowColumnReorder = false;
            this.rlvUnit.AllowColumnResize = false;
            this.rlvUnit.AllowEdit = false;
            this.rlvUnit.AllowRemove = false;
            this.rlvUnit.AutoScroll = true;
            this.rlvUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rlvUnit.Location = new System.Drawing.Point(3, 71);
            this.rlvUnit.Name = "rlvUnit";
            this.rlvUnit.Size = new System.Drawing.Size(422, 450);
            this.rlvUnit.TabIndex = 2;
            this.rlvUnit.Text = "radListView1";
            this.rlvUnit.VisualItemFormatting += new Telerik.WinControls.UI.ListViewVisualItemEventHandler(this.rlvUnit_VisualItemFormatting);
            this.rlvUnit.VisualItemCreating += new Telerik.WinControls.UI.ListViewVisualItemCreatingEventHandler(this.rlvUnit_VisualItemCreating);
            this.rlvUnit.DoubleClick += new System.EventHandler(this.rlvUnit_DoubleClick);
            // 
            // rbarUnit
            // 
            this.rbarUnit.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbarUnit.Location = new System.Drawing.Point(3, 16);
            this.rbarUnit.Name = "rbarUnit";
            this.rbarUnit.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement2});
            this.rbarUnit.Size = new System.Drawing.Size(422, 55);
            this.rbarUnit.TabIndex = 1;
            this.rbarUnit.Text = "radCommandBar1";
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
            this.drpUnitGroup,
            this.commandBarSeparator2,
            this.commandBarLabel4,
            this.tbUnitFilter});
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
            // drpUnitGroup
            // 
            this.drpUnitGroup.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.drpUnitGroup.DisplayName = "commandBarDropDownList1";
            this.drpUnitGroup.DropDownAnimationEnabled = true;
            this.drpUnitGroup.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpUnitGroup.MaxDropDownItems = 0;
            this.drpUnitGroup.Name = "drpUnitGroup";
            this.drpUnitGroup.Text = "";
            this.drpUnitGroup.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.drpUnitGroup.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.drpUnitGroup_SelectedIndexChanged);
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
            // tbUnitFilter
            // 
            this.tbUnitFilter.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.tbUnitFilter.DisplayName = "commandBarTextBox1";
            this.tbUnitFilter.Name = "tbUnitFilter";
            this.tbUnitFilter.StretchHorizontally = true;
            this.tbUnitFilter.Text = "";
            this.tbUnitFilter.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.tbUnitFilter.TextChanged += new System.EventHandler(this.tbUnitFilter_TextChanged);
            // 
            // frmLocDonViCA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 524);
            this.Controls.Add(this.grbUnit);
            this.Name = "frmLocDonViCA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm kiếm đơn vị CA";
            this.Load += new System.EventHandler(this.frmLocDonViCA_Load);
            this.grbUnit.ResumeLayout(false);
            this.grbUnit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarUnit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbUnit;
        private Telerik.WinControls.UI.RadListView rlvUnit;
        private Telerik.WinControls.UI.RadCommandBar rbarUnit;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement2;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement2;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel3;
        private Telerik.WinControls.UI.CommandBarDropDownList drpUnitGroup;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator2;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel4;
        private Telerik.WinControls.UI.CommandBarTextBox tbUnitFilter;
    }
}