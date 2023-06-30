namespace ES.CA_ManagementUI
{
    partial class frmLocNguoiDung
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
            this.grbUser = new System.Windows.Forms.GroupBox();
            this.rlvUser = new Telerik.WinControls.UI.RadListView();
            this.commandBarRowElement1 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.commandBarLabel1 = new Telerik.WinControls.UI.CommandBarLabel();
            this.drpUserGroup = new Telerik.WinControls.UI.CommandBarDropDownList();
            this.commandBarSeparator1 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarLabel2 = new Telerik.WinControls.UI.CommandBarLabel();
            this.tbUserFilter = new Telerik.WinControls.UI.CommandBarTextBox();
            this.rbarUser = new Telerik.WinControls.UI.RadCommandBar();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.grbUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarUser)).BeginInit();
            this.SuspendLayout();
            // 
            // grbUser
            // 
            this.grbUser.Controls.Add(this.btnAddUser);
            this.grbUser.Controls.Add(this.rlvUser);
            this.grbUser.Controls.Add(this.rbarUser);
            this.grbUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbUser.Location = new System.Drawing.Point(0, 0);
            this.grbUser.Name = "grbUser";
            this.grbUser.Size = new System.Drawing.Size(452, 529);
            this.grbUser.TabIndex = 55;
            this.grbUser.TabStop = false;
            this.grbUser.Text = "Người dùng CA";
            // 
            // rlvUser
            // 
            this.rlvUser.AllowColumnReorder = false;
            this.rlvUser.AllowColumnResize = false;
            this.rlvUser.AllowEdit = false;
            this.rlvUser.AllowRemove = false;
            this.rlvUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rlvUser.AutoScroll = true;
            this.rlvUser.Location = new System.Drawing.Point(3, 48);
            this.rlvUser.Name = "rlvUser";
            this.rlvUser.Size = new System.Drawing.Size(446, 478);
            this.rlvUser.TabIndex = 1;
            this.rlvUser.Text = "radListView1";
            this.rlvUser.DoubleClick += new System.EventHandler(this.rlvUser_DoubleClick);
            // 
            // commandBarRowElement1
            // 
            this.commandBarRowElement1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarRowElement1.MinSize = new System.Drawing.Size(25, 25);
            this.commandBarRowElement1.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.commandBarStripElement1});
            this.commandBarRowElement1.Text = "";
            this.commandBarRowElement1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // commandBarStripElement1
            // 
            this.commandBarStripElement1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement1.DisplayName = "commandBarStripElement1";
            this.commandBarStripElement1.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.commandBarLabel1,
            this.drpUserGroup,
            this.commandBarSeparator1,
            this.commandBarLabel2,
            this.tbUserFilter});
            this.commandBarStripElement1.Name = "commandBarStripElement1";
            // 
            // 
            // 
            this.commandBarStripElement1.OverflowButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            this.commandBarStripElement1.StretchHorizontally = true;
            this.commandBarStripElement1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            ((Telerik.WinControls.UI.RadCommandBarOverflowButton)(this.commandBarStripElement1.GetChildAt(2))).Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // commandBarLabel1
            // 
            this.commandBarLabel1.AccessibleDescription = "Nhóm:";
            this.commandBarLabel1.AccessibleName = "Nhóm:";
            this.commandBarLabel1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarLabel1.DisplayName = "commandBarLabel1";
            this.commandBarLabel1.Name = "commandBarLabel1";
            this.commandBarLabel1.Text = "Nhóm:";
            this.commandBarLabel1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // drpUserGroup
            // 
            this.drpUserGroup.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.drpUserGroup.DisplayName = "commandBarDropDownList1";
            this.drpUserGroup.DropDownAnimationEnabled = true;
            this.drpUserGroup.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpUserGroup.MaxDropDownItems = 0;
            this.drpUserGroup.Name = "drpUserGroup";
            this.drpUserGroup.Text = "";
            this.drpUserGroup.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // commandBarSeparator1
            // 
            this.commandBarSeparator1.AccessibleDescription = "commandBarSeparator1";
            this.commandBarSeparator1.AccessibleName = "commandBarSeparator1";
            this.commandBarSeparator1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarSeparator1.DisplayName = "commandBarSeparator1";
            this.commandBarSeparator1.Name = "commandBarSeparator1";
            this.commandBarSeparator1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarSeparator1.VisibleInOverflowMenu = false;
            // 
            // commandBarLabel2
            // 
            this.commandBarLabel2.AccessibleDescription = "Lọc:";
            this.commandBarLabel2.AccessibleName = "Lọc:";
            this.commandBarLabel2.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarLabel2.DisplayName = "commandBarLabel2";
            this.commandBarLabel2.Name = "commandBarLabel2";
            this.commandBarLabel2.Text = "Lọc:";
            this.commandBarLabel2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // tbUserFilter
            // 
            this.tbUserFilter.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.tbUserFilter.DisplayName = "commandBarTextBox1";
            this.tbUserFilter.Name = "tbUserFilter";
            this.tbUserFilter.StretchHorizontally = true;
            this.tbUserFilter.Text = "";
            this.tbUserFilter.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.tbUserFilter.Click += new System.EventHandler(this.tbUserFilter_Click);
            // 
            // rbarUser
            // 
            this.rbarUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbarUser.Location = new System.Drawing.Point(6, 12);
            this.rbarUser.Name = "rbarUser";
            this.rbarUser.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement1});
            this.rbarUser.Size = new System.Drawing.Size(331, 30);
            this.rbarUser.TabIndex = 0;
            this.rbarUser.Text = "radCommandBar1";
            this.rbarUser.ThemeName = "ControlDefault";
            // 
            // btnAddUser
            // 
            this.btnAddUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddUser.Location = new System.Drawing.Point(343, 12);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(103, 30);
            this.btnAddUser.TabIndex = 2;
            this.btnAddUser.Text = "Thêm người dùng";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // frmLocNguoiDung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 529);
            this.Controls.Add(this.grbUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmLocNguoiDung";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm kiếm người dùng CA";
            this.Load += new System.EventHandler(this.frmLocNguoiDung_Load);
            this.grbUser.ResumeLayout(false);
            this.grbUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbarUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbUser;
        private Telerik.WinControls.UI.RadListView rlvUser;
        private Telerik.WinControls.UI.RadCommandBar rbarUser;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement1;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement1;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel1;
        private Telerik.WinControls.UI.CommandBarDropDownList drpUserGroup;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator1;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel2;
        private Telerik.WinControls.UI.CommandBarTextBox tbUserFilter;
        private System.Windows.Forms.Button btnAddUser;
    }
}