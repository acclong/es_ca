namespace ESLogin
{
    partial class ctrUserGroup
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrUserGroup));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnGroupDelete = new System.Windows.Forms.Button();
            this.btnGroupEdit = new System.Windows.Forms.Button();
            this.btnRoleAdd = new System.Windows.Forms.Button();
            this.plBottomRight = new System.Windows.Forms.Panel();
            this.btnModuleSave = new System.Windows.Forms.Button();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.pnlTopRight = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.cboModule = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnRoleUserUpdate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cfgRoleGroupUser = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.trvModule = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cfgRoleGroup = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel2.SuspendLayout();
            this.plBottomRight.SuspendLayout();
            this.pnlTopRight.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgRoleGroupUser)).BeginInit();
            this.panel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgRoleGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnGroupDelete);
            this.panel2.Controls.Add(this.btnGroupEdit);
            this.panel2.Controls.Add(this.btnRoleAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 278);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(444, 41);
            this.panel2.TabIndex = 1;
            // 
            // btnGroupDelete
            // 
            this.btnGroupDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGroupDelete.Image = global::ESLogin.Properties.Resources.Delete;
            this.btnGroupDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGroupDelete.Location = new System.Drawing.Point(365, 7);
            this.btnGroupDelete.Name = "btnGroupDelete";
            this.btnGroupDelete.Size = new System.Drawing.Size(69, 23);
            this.btnGroupDelete.TabIndex = 43;
            this.btnGroupDelete.Text = "  Xóa";
            this.btnGroupDelete.UseVisualStyleBackColor = true;
            this.btnGroupDelete.Click += new System.EventHandler(this.btnGroupDelete_Click);
            // 
            // btnGroupEdit
            // 
            this.btnGroupEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGroupEdit.Image = global::ESLogin.Properties.Resources.Edit;
            this.btnGroupEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGroupEdit.Location = new System.Drawing.Point(290, 7);
            this.btnGroupEdit.Name = "btnGroupEdit";
            this.btnGroupEdit.Size = new System.Drawing.Size(69, 23);
            this.btnGroupEdit.TabIndex = 42;
            this.btnGroupEdit.Text = "  Sửa";
            this.btnGroupEdit.UseVisualStyleBackColor = true;
            this.btnGroupEdit.Click += new System.EventHandler(this.btnGroupEdit_Click);
            // 
            // btnRoleAdd
            // 
            this.btnRoleAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRoleAdd.Image = global::ESLogin.Properties.Resources.add;
            this.btnRoleAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRoleAdd.Location = new System.Drawing.Point(215, 7);
            this.btnRoleAdd.Name = "btnRoleAdd";
            this.btnRoleAdd.Size = new System.Drawing.Size(69, 23);
            this.btnRoleAdd.TabIndex = 41;
            this.btnRoleAdd.Text = "  Thêm";
            this.btnRoleAdd.UseVisualStyleBackColor = true;
            this.btnRoleAdd.Click += new System.EventHandler(this.btnRoleAdd_Click);
            // 
            // plBottomRight
            // 
            this.plBottomRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plBottomRight.Controls.Add(this.btnModuleSave);
            this.plBottomRight.Controls.Add(this.chkAll);
            this.plBottomRight.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBottomRight.Location = new System.Drawing.Point(3, 278);
            this.plBottomRight.Name = "plBottomRight";
            this.plBottomRight.Size = new System.Drawing.Size(313, 41);
            this.plBottomRight.TabIndex = 21;
            // 
            // btnModuleSave
            // 
            this.btnModuleSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModuleSave.Image = global::ESLogin.Properties.Resources.Save;
            this.btnModuleSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModuleSave.Location = new System.Drawing.Point(239, 7);
            this.btnModuleSave.Name = "btnModuleSave";
            this.btnModuleSave.Size = new System.Drawing.Size(69, 23);
            this.btnModuleSave.TabIndex = 42;
            this.btnModuleSave.Text = "  Ghi";
            this.btnModuleSave.UseVisualStyleBackColor = true;
            this.btnModuleSave.Click += new System.EventHandler(this.btnModuleSave_Click);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(8, 11);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(99, 17);
            this.chkAll.TabIndex = 14;
            this.chkAll.Text = "Chọn/Bỏ tất cả";
            this.chkAll.UseVisualStyleBackColor = true;
            // 
            // pnlTopRight
            // 
            this.pnlTopRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTopRight.Controls.Add(this.Label1);
            this.pnlTopRight.Controls.Add(this.cboModule);
            this.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopRight.Location = new System.Drawing.Point(3, 16);
            this.pnlTopRight.Name = "pnlTopRight";
            this.pnlTopRight.Size = new System.Drawing.Size(313, 44);
            this.pnlTopRight.TabIndex = 20;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(5, 15);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(107, 13);
            this.Label1.TabIndex = 5;
            this.Label1.Text = "Module chương trình:";
            // 
            // cboModule
            // 
            this.cboModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModule.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboModule.FormattingEnabled = true;
            this.cboModule.Location = new System.Drawing.Point(118, 12);
            this.cboModule.Name = "cboModule";
            this.cboModule.Size = new System.Drawing.Size(158, 21);
            this.cboModule.TabIndex = 6;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Control;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.btnRoleUserUpdate);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(3, 144);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(763, 35);
            this.panel6.TabIndex = 1;
            // 
            // btnRoleUserUpdate
            // 
            this.btnRoleUserUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRoleUserUpdate.Image = global::ESLogin.Properties.Resources.Save;
            this.btnRoleUserUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRoleUserUpdate.Location = new System.Drawing.Point(685, 6);
            this.btnRoleUserUpdate.Name = "btnRoleUserUpdate";
            this.btnRoleUserUpdate.Size = new System.Drawing.Size(69, 23);
            this.btnRoleUserUpdate.TabIndex = 43;
            this.btnRoleUserUpdate.Text = "  Ghi";
            this.btnRoleUserUpdate.UseVisualStyleBackColor = true;
            this.btnRoleUserUpdate.Click += new System.EventHandler(this.btnRoleUserUpdate_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(773, 511);
            this.panel1.TabIndex = 55;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.cfgRoleGroupUser);
            this.groupBox2.Controls.Add(this.panel6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 325);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(769, 182);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Phân nhóm quyền cho người dùng";
            // 
            // cfgRoleGroupUser
            // 
            this.cfgRoleGroupUser.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.cfgRoleGroupUser.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgRoleGroupUser.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgRoleGroupUser.ColumnInfo = "10,1,0,0,0,105,Columns:";
            this.cfgRoleGroupUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgRoleGroupUser.ExtendLastCol = true;
            this.cfgRoleGroupUser.Location = new System.Drawing.Point(3, 16);
            this.cfgRoleGroupUser.Name = "cfgRoleGroupUser";
            this.cfgRoleGroupUser.Rows.Count = 1;
            this.cfgRoleGroupUser.Rows.DefaultSize = 21;
            this.cfgRoleGroupUser.Size = new System.Drawing.Size(763, 128);
            this.cfgRoleGroupUser.StyleInfo = resources.GetString("cfgRoleGroupUser.StyleInfo");
            this.cfgRoleGroupUser.TabIndex = 18;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 322);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(769, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(769, 322);
            this.panel4.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.trvModule);
            this.groupBox3.Controls.Add(this.pnlTopRight);
            this.groupBox3.Controls.Add(this.plBottomRight);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(450, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(319, 322);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Phân quyền";
            // 
            // trvModule
            // 
            this.trvModule.BackColor = System.Drawing.SystemColors.Window;
            this.trvModule.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trvModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvModule.Location = new System.Drawing.Point(3, 60);
            this.trvModule.Name = "trvModule";
            this.trvModule.Size = new System.Drawing.Size(313, 218);
            this.trvModule.TabIndex = 23;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.cfgRoleGroup);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 322);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nhóm quyền";
            // 
            // cfgRoleGroup
            // 
            this.cfgRoleGroup.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgRoleGroup.ColumnInfo = "1,1,0,0,0,105,Columns:";
            this.cfgRoleGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgRoleGroup.ExtendLastCol = true;
            this.cfgRoleGroup.Location = new System.Drawing.Point(3, 16);
            this.cfgRoleGroup.Name = "cfgRoleGroup";
            this.cfgRoleGroup.Rows.Count = 1;
            this.cfgRoleGroup.Rows.DefaultSize = 21;
            this.cfgRoleGroup.Size = new System.Drawing.Size(444, 262);
            this.cfgRoleGroup.StyleInfo = resources.GetString("cfgRoleGroup.StyleInfo");
            this.cfgRoleGroup.TabIndex = 19;
            // 
            // ctrUserGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panel1);
            this.Name = "ctrUserGroup";
            this.Size = new System.Drawing.Size(773, 511);
            this.Load += new System.EventHandler(this.ucRoleGroup_Load);
            this.panel2.ResumeLayout(false);
            this.plBottomRight.ResumeLayout(false);
            this.plBottomRight.PerformLayout();
            this.pnlTopRight.ResumeLayout(false);
            this.pnlTopRight.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfgRoleGroupUser)).EndInit();
            this.panel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfgRoleGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Panel plBottomRight;
        internal System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Panel pnlTopRight;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        internal C1.Win.C1FlexGrid.C1FlexGrid cfgRoleGroup;
        private System.Windows.Forms.GroupBox groupBox2;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgRoleGroupUser;
        private System.Windows.Forms.TreeView trvModule;
        private System.Windows.Forms.ComboBox cboModule;
        private System.Windows.Forms.Button btnRoleAdd;
        private System.Windows.Forms.Button btnGroupEdit;
        private System.Windows.Forms.Button btnGroupDelete;
        private System.Windows.Forms.Button btnModuleSave;
        private System.Windows.Forms.Button btnRoleUserUpdate;
    }
}
