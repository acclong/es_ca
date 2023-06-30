namespace ES.CA_ManagementUI
{
    partial class frmLocHeThongTichHop
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
            this.grbProg = new System.Windows.Forms.GroupBox();
            this.rlvProg = new Telerik.WinControls.UI.RadListView();
            this.rbarProg = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement2 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement2 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.cmmbarllbNhom = new Telerik.WinControls.UI.CommandBarLabel();
            this.drpProgGroup = new Telerik.WinControls.UI.CommandBarDropDownList();
            this.commandBarSeparator4 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.cmmbarlblLoc = new Telerik.WinControls.UI.CommandBarLabel();
            this.txtProgFilter = new Telerik.WinControls.UI.CommandBarTextBox();
            this.btnAddProg = new Telerik.WinControls.UI.RadButton();
            this.commandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.grbProg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvProg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarProg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddProg)).BeginInit();
            this.SuspendLayout();
            // 
            // grbProg
            // 
            this.grbProg.Controls.Add(this.btnAddProg);
            this.grbProg.Controls.Add(this.rlvProg);
            this.grbProg.Controls.Add(this.rbarProg);
            this.grbProg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbProg.Location = new System.Drawing.Point(0, 0);
            this.grbProg.Name = "grbProg";
            this.grbProg.Size = new System.Drawing.Size(531, 535);
            this.grbProg.TabIndex = 3;
            this.grbProg.TabStop = false;
            this.grbProg.Text = "Hệ thống tích hợp";
            // 
            // rlvProg
            // 
            this.rlvProg.AllowColumnReorder = false;
            this.rlvProg.AllowColumnResize = false;
            this.rlvProg.AllowEdit = false;
            this.rlvProg.AllowRemove = false;
            this.rlvProg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rlvProg.AutoScroll = true;
            this.rlvProg.Location = new System.Drawing.Point(3, 48);
            this.rlvProg.Name = "rlvProg";
            this.rlvProg.Size = new System.Drawing.Size(525, 484);
            this.rlvProg.TabIndex = 59;
            this.rlvProg.Text = "radListView1";
            this.rlvProg.DoubleClick += new System.EventHandler(this.frmLocHeThongTichHop_DoubleClick);
            // 
            // rbarProg
            // 
            this.rbarProg.Location = new System.Drawing.Point(3, 12);
            this.rbarProg.Name = "rbarProg";
            this.rbarProg.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement2});
            this.rbarProg.Size = new System.Drawing.Size(426, 30);
            this.rbarProg.TabIndex = 58;
            this.rbarProg.Text = "radCommandBar1";
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
            this.cmmbarllbNhom,
            this.drpProgGroup,
            this.commandBarSeparator4,
            this.cmmbarlblLoc,
            this.txtProgFilter});
            this.commandBarStripElement2.Name = "commandBarStripElement1";
            // 
            // 
            // 
            this.commandBarStripElement2.OverflowButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            this.commandBarStripElement2.StretchHorizontally = true;
            this.commandBarStripElement2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            ((Telerik.WinControls.UI.RadCommandBarOverflowButton)(this.commandBarStripElement2.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // cmmbarllbNhom
            // 
            this.cmmbarllbNhom.AccessibleDescription = "Nhóm:";
            this.cmmbarllbNhom.AccessibleName = "Nhóm:";
            this.cmmbarllbNhom.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.cmmbarllbNhom.DisplayName = "commandBarLabel1";
            this.cmmbarllbNhom.Name = "cmmbarllbNhom";
            this.cmmbarllbNhom.Text = "Nhóm:";
            this.cmmbarllbNhom.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // drpProgGroup
            // 
            this.drpProgGroup.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.drpProgGroup.DisplayName = "commandBarDropDownList1";
            this.drpProgGroup.DropDownAnimationEnabled = true;
            this.drpProgGroup.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpProgGroup.MaxDropDownItems = 0;
            this.drpProgGroup.Name = "drpProgGroup";
            this.drpProgGroup.Text = "";
            this.drpProgGroup.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.drpProgGroup.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.drpUnitGroup_SelectedIndexChanged);
            // 
            // commandBarSeparator4
            // 
            this.commandBarSeparator4.AccessibleDescription = "commandBarSeparator1";
            this.commandBarSeparator4.AccessibleName = "commandBarSeparator1";
            this.commandBarSeparator4.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarSeparator4.DisplayName = "commandBarSeparator1";
            this.commandBarSeparator4.Name = "commandBarSeparator4";
            this.commandBarSeparator4.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarSeparator4.VisibleInOverflowMenu = false;
            // 
            // cmmbarlblLoc
            // 
            this.cmmbarlblLoc.AccessibleDescription = "Lọc:";
            this.cmmbarlblLoc.AccessibleName = "Lọc:";
            this.cmmbarlblLoc.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.cmmbarlblLoc.DisplayName = "commandBarLabel2";
            this.cmmbarlblLoc.Name = "cmmbarlblLoc";
            this.cmmbarlblLoc.Text = "Lọc:";
            this.cmmbarlblLoc.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // txtProgFilter
            // 
            this.txtProgFilter.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.txtProgFilter.DisplayName = "commandBarTextBox1";
            this.txtProgFilter.Name = "txtProgFilter";
            this.txtProgFilter.StretchHorizontally = true;
            this.txtProgFilter.Text = "";
            this.txtProgFilter.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.txtProgFilter.TextChanged += new System.EventHandler(this.txtProgFilter_TextChanged);
            // 
            // btnAddProg
            // 
            this.btnAddProg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddProg.Location = new System.Drawing.Point(435, 14);
            this.btnAddProg.Name = "btnAddProg";
            this.btnAddProg.Size = new System.Drawing.Size(90, 28);
            this.btnAddProg.TabIndex = 60;
            this.btnAddProg.Text = "Thêm hệ thống";
            this.btnAddProg.Click += new System.EventHandler(this.btnAddProg_Click);
            // 
            // commandBarStripElement1
            // 
            this.commandBarStripElement1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement1.DisplayName = "commandBarStripElement1";
            this.commandBarStripElement1.Name = "commandBarStripElement1";
            this.commandBarStripElement1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // frmLocHeThongTichHop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 535);
            this.Controls.Add(this.grbProg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmLocHeThongTichHop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm kiếm hệ thống tích hợp";
            this.Load += new System.EventHandler(this.frmLocHeThongTichHop_Load);
            this.grbProg.ResumeLayout(false);
            this.grbProg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvProg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarProg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddProg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbProg;
        private Telerik.WinControls.UI.RadListView rlvProg;
        private Telerik.WinControls.UI.RadCommandBar rbarProg;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement2;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement2;
        private Telerik.WinControls.UI.CommandBarLabel cmmbarllbNhom;
        private Telerik.WinControls.UI.CommandBarDropDownList drpProgGroup;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator4;
        private Telerik.WinControls.UI.CommandBarLabel cmmbarlblLoc;
        private Telerik.WinControls.UI.CommandBarTextBox txtProgFilter;
        private Telerik.WinControls.UI.RadButton btnAddProg;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement1;
    }
}