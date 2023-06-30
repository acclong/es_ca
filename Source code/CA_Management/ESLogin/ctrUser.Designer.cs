namespace ESLogin
{
    partial class ctrUser
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnDefaultLogin = new System.Windows.Forms.Button();
            this.btnUserDelete = new System.Windows.Forms.Button();
            this.btnUserEdit = new System.Windows.Forms.Button();
            this.btnUserAdd = new System.Windows.Forms.Button();
            this.plBottomRight = new System.Windows.Forms.Panel();
            this.btnRoleSave = new System.Windows.Forms.Button();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.trvModule = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grvUser = new System.Windows.Forms.DataGridView();
            this.panel4.SuspendLayout();
            this.plBottomRight.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvUser)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.btnDefaultLogin);
            this.panel4.Controls.Add(this.btnUserDelete);
            this.panel4.Controls.Add(this.btnUserEdit);
            this.panel4.Controls.Add(this.btnUserAdd);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(3, 531);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(738, 41);
            this.panel4.TabIndex = 40;
            // 
            // btnDefaultLogin
            // 
            this.btnDefaultLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefaultLogin.Image = global::ESLogin.Properties.Resources.config;
            this.btnDefaultLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDefaultLogin.Location = new System.Drawing.Point(557, 6);
            this.btnDefaultLogin.Name = "btnDefaultLogin";
            this.btnDefaultLogin.Size = new System.Drawing.Size(169, 23);
            this.btnDefaultLogin.TabIndex = 43;
            this.btnDefaultLogin.Text = "    Tạo đăng nhập ngầm định";
            this.btnDefaultLogin.UseVisualStyleBackColor = true;
            this.btnDefaultLogin.Click += new System.EventHandler(this.btnDefaultLogin_Click);
            // 
            // btnUserDelete
            // 
            this.btnUserDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUserDelete.Image = global::ESLogin.Properties.Resources.Delete;
            this.btnUserDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserDelete.Location = new System.Drawing.Point(482, 6);
            this.btnUserDelete.Name = "btnUserDelete";
            this.btnUserDelete.Size = new System.Drawing.Size(69, 23);
            this.btnUserDelete.TabIndex = 42;
            this.btnUserDelete.Text = "  Xóa";
            this.btnUserDelete.UseVisualStyleBackColor = true;
            this.btnUserDelete.Click += new System.EventHandler(this.btnUserDelete_Click);
            // 
            // btnUserEdit
            // 
            this.btnUserEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUserEdit.Image = global::ESLogin.Properties.Resources.Edit;
            this.btnUserEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserEdit.Location = new System.Drawing.Point(407, 7);
            this.btnUserEdit.Name = "btnUserEdit";
            this.btnUserEdit.Size = new System.Drawing.Size(69, 23);
            this.btnUserEdit.TabIndex = 41;
            this.btnUserEdit.Text = "  Sửa";
            this.btnUserEdit.UseVisualStyleBackColor = true;
            this.btnUserEdit.Click += new System.EventHandler(this.btnUserEdit_Click);
            // 
            // btnUserAdd
            // 
            this.btnUserAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUserAdd.Image = global::ESLogin.Properties.Resources.add;
            this.btnUserAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserAdd.Location = new System.Drawing.Point(334, 7);
            this.btnUserAdd.Name = "btnUserAdd";
            this.btnUserAdd.Size = new System.Drawing.Size(69, 23);
            this.btnUserAdd.TabIndex = 40;
            this.btnUserAdd.Text = "  Thêm";
            this.btnUserAdd.UseVisualStyleBackColor = true;
            this.btnUserAdd.Click += new System.EventHandler(this.btnUserAdd_Click);
            // 
            // plBottomRight
            // 
            this.plBottomRight.BackColor = System.Drawing.SystemColors.Control;
            this.plBottomRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.plBottomRight.Controls.Add(this.btnRoleSave);
            this.plBottomRight.Controls.Add(this.chkAll);
            this.plBottomRight.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plBottomRight.Location = new System.Drawing.Point(3, 531);
            this.plBottomRight.Name = "plBottomRight";
            this.plBottomRight.Size = new System.Drawing.Size(293, 41);
            this.plBottomRight.TabIndex = 21;
            // 
            // btnRoleSave
            // 
            this.btnRoleSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRoleSave.Image = global::ESLogin.Properties.Resources.Save;
            this.btnRoleSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRoleSave.Location = new System.Drawing.Point(217, 7);
            this.btnRoleSave.Name = "btnRoleSave";
            this.btnRoleSave.Size = new System.Drawing.Size(69, 23);
            this.btnRoleSave.TabIndex = 41;
            this.btnRoleSave.Text = "Ghi";
            this.btnRoleSave.UseVisualStyleBackColor = true;
            this.btnRoleSave.Click += new System.EventHandler(this.btnRoleSave_Click);
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
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1047, 579);
            this.panel1.TabIndex = 55;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.trvModule);
            this.groupBox3.Controls.Add(this.plBottomRight);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(744, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(299, 575);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Phân quyền chức năng sử dụng";
            // 
            // trvModule
            // 
            this.trvModule.BackColor = System.Drawing.SystemColors.Window;
            this.trvModule.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.trvModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvModule.Location = new System.Drawing.Point(3, 16);
            this.trvModule.Name = "trvModule";
            this.trvModule.Size = new System.Drawing.Size(293, 515);
            this.trvModule.TabIndex = 22;
            this.trvModule.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvModule_AfterCheck);
            this.trvModule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trvModule_MouseDown);
            this.trvModule.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trvModule_MouseUp);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.grvUser);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(744, 575);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Người dùng";
            // 
            // grvUser
            // 
            this.grvUser.AllowUserToAddRows = false;
            this.grvUser.AllowUserToDeleteRows = false;
            this.grvUser.AllowUserToOrderColumns = true;
            this.grvUser.AllowUserToResizeRows = false;
            this.grvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvUser.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grvUser.Location = new System.Drawing.Point(3, 16);
            this.grvUser.MultiSelect = false;
            this.grvUser.Name = "grvUser";
            this.grvUser.ReadOnly = true;
            this.grvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvUser.Size = new System.Drawing.Size(738, 515);
            this.grvUser.TabIndex = 41;
            this.grvUser.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.grvUser_RowStateChanged);
            // 
            // ctrUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panel1);
            this.Name = "ctrUser";
            this.Size = new System.Drawing.Size(1047, 579);
            this.Load += new System.EventHandler(this.ctrUser_Load);
            this.panel4.ResumeLayout(false);
            this.plBottomRight.ResumeLayout(false);
            this.plBottomRight.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel plBottomRight;
        internal System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TreeView trvModule;
        private System.Windows.Forms.Button btnUserAdd;
        private System.Windows.Forms.Button btnUserEdit;
        private System.Windows.Forms.Button btnUserDelete;
        private System.Windows.Forms.Button btnDefaultLogin;
        private System.Windows.Forms.Button btnRoleSave;
        private System.Windows.Forms.DataGridView grvUser;
    }
}
