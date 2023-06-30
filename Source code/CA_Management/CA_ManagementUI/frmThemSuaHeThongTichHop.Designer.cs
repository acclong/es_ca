namespace ES.CA_ManagementUI
{
    partial class frmThemSuaHeThongTichHop
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
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblProgID = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.grbInfSystem = new System.Windows.Forms.GroupBox();
            this.lblNotation = new System.Windows.Forms.Label();
            this.txtNotation = new System.Windows.Forms.TextBox();
            this.txtProgID = new System.Windows.Forms.TextBox();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblProgName = new System.Windows.Forms.Label();
            this.txtProgName = new System.Windows.Forms.TextBox();
            this.grbInfTableUser = new System.Windows.Forms.GroupBox();
            this.lblTableUser = new System.Windows.Forms.Label();
            this.txtQueryUser = new System.Windows.Forms.TextBox();
            this.grbDatabase = new System.Windows.Forms.GroupBox();
            this.lblServerName = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.lblDBName = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.pnlFooter.SuspendLayout();
            this.grbInfSystem.SuspendLayout();
            this.grbInfTableUser.SuspendLayout();
            this.grbDatabase.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnExit);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 393);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(384, 40);
            this.pnlFooter.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(199, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Đóng";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(116, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblProgID
            // 
            this.lblProgID.AutoSize = true;
            this.lblProgID.Location = new System.Drawing.Point(13, 21);
            this.lblProgID.Name = "lblProgID";
            this.lblProgID.Size = new System.Drawing.Size(18, 13);
            this.lblProgID.TabIndex = 12;
            this.lblProgID.Text = "ID";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(115, 74);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(257, 20);
            this.txtName.TabIndex = 2;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(13, 77);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(78, 13);
            this.lblName.TabIndex = 14;
            this.lblName.Text = "Tên hệ thống *";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(13, 133);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(62, 13);
            this.lblStatus.TabIndex = 16;
            this.lblStatus.Text = "Trạng thái *";
            // 
            // grbInfSystem
            // 
            this.grbInfSystem.Controls.Add(this.lblNotation);
            this.grbInfSystem.Controls.Add(this.txtNotation);
            this.grbInfSystem.Controls.Add(this.txtProgID);
            this.grbInfSystem.Controls.Add(this.cboStatus);
            this.grbInfSystem.Controls.Add(this.lblProgName);
            this.grbInfSystem.Controls.Add(this.txtProgName);
            this.grbInfSystem.Controls.Add(this.lblProgID);
            this.grbInfSystem.Controls.Add(this.lblName);
            this.grbInfSystem.Controls.Add(this.txtName);
            this.grbInfSystem.Controls.Add(this.lblStatus);
            this.grbInfSystem.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbInfSystem.Location = new System.Drawing.Point(0, 0);
            this.grbInfSystem.Name = "grbInfSystem";
            this.grbInfSystem.Size = new System.Drawing.Size(384, 158);
            this.grbInfSystem.TabIndex = 0;
            this.grbInfSystem.TabStop = false;
            this.grbInfSystem.Text = "Thông tin hệ thống";
            // 
            // lblNotation
            // 
            this.lblNotation.AutoSize = true;
            this.lblNotation.Location = new System.Drawing.Point(13, 105);
            this.lblNotation.Name = "lblNotation";
            this.lblNotation.Size = new System.Drawing.Size(49, 13);
            this.lblNotation.TabIndex = 104;
            this.lblNotation.Text = "Ký hiệu *";
            // 
            // txtNotation
            // 
            this.txtNotation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotation.Location = new System.Drawing.Point(115, 102);
            this.txtNotation.Name = "txtNotation";
            this.txtNotation.Size = new System.Drawing.Size(257, 20);
            this.txtNotation.TabIndex = 3;
            // 
            // txtProgID
            // 
            this.txtProgID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProgID.Location = new System.Drawing.Point(115, 18);
            this.txtProgID.Name = "txtProgID";
            this.txtProgID.ReadOnly = true;
            this.txtProgID.Size = new System.Drawing.Size(107, 20);
            this.txtProgID.TabIndex = 0;
            // 
            // cboStatus
            // 
            this.cboStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(115, 130);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(257, 21);
            this.cboStatus.TabIndex = 4;
            // 
            // lblProgName
            // 
            this.lblProgName.AutoSize = true;
            this.lblProgName.Location = new System.Drawing.Point(13, 49);
            this.lblProgName.Name = "lblProgName";
            this.lblProgName.Size = new System.Drawing.Size(74, 13);
            this.lblProgName.TabIndex = 34;
            this.lblProgName.Text = "Mã hệ thống *";
            // 
            // txtProgName
            // 
            this.txtProgName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProgName.Location = new System.Drawing.Point(115, 46);
            this.txtProgName.Name = "txtProgName";
            this.txtProgName.Size = new System.Drawing.Size(257, 20);
            this.txtProgName.TabIndex = 1;
            // 
            // grbInfTableUser
            // 
            this.grbInfTableUser.Controls.Add(this.lblTableUser);
            this.grbInfTableUser.Controls.Add(this.txtQueryUser);
            this.grbInfTableUser.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grbInfTableUser.Location = new System.Drawing.Point(0, 290);
            this.grbInfTableUser.Name = "grbInfTableUser";
            this.grbInfTableUser.Size = new System.Drawing.Size(384, 103);
            this.grbInfTableUser.TabIndex = 2;
            this.grbInfTableUser.TabStop = false;
            this.grbInfTableUser.Text = "Thông tin bảng người dùng";
            // 
            // lblTableUser
            // 
            this.lblTableUser.AutoSize = true;
            this.lblTableUser.Location = new System.Drawing.Point(13, 22);
            this.lblTableUser.Name = "lblTableUser";
            this.lblTableUser.Size = new System.Drawing.Size(58, 13);
            this.lblTableUser.TabIndex = 26;
            this.lblTableUser.Text = "Query user";
            // 
            // txtQueryUser
            // 
            this.txtQueryUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQueryUser.Location = new System.Drawing.Point(115, 19);
            this.txtQueryUser.Multiline = true;
            this.txtQueryUser.Name = "txtQueryUser";
            this.txtQueryUser.Size = new System.Drawing.Size(257, 78);
            this.txtQueryUser.TabIndex = 0;
            this.txtQueryUser.Tag = "";
            // 
            // grbDatabase
            // 
            this.grbDatabase.Controls.Add(this.lblServerName);
            this.grbDatabase.Controls.Add(this.txtServerName);
            this.grbDatabase.Controls.Add(this.lblDBName);
            this.grbDatabase.Controls.Add(this.txtPass);
            this.grbDatabase.Controls.Add(this.txtDBName);
            this.grbDatabase.Controls.Add(this.lblPass);
            this.grbDatabase.Controls.Add(this.lblUserName);
            this.grbDatabase.Controls.Add(this.txtUserName);
            this.grbDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbDatabase.Location = new System.Drawing.Point(0, 158);
            this.grbDatabase.Name = "grbDatabase";
            this.grbDatabase.Size = new System.Drawing.Size(384, 132);
            this.grbDatabase.TabIndex = 1;
            this.grbDatabase.TabStop = false;
            this.grbDatabase.Text = "Thông tin cơ sở dữ liệu";
            // 
            // lblServerName
            // 
            this.lblServerName.AutoSize = true;
            this.lblServerName.Location = new System.Drawing.Point(13, 22);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(55, 13);
            this.lblServerName.TabIndex = 18;
            this.lblServerName.Text = "Máy chủ *";
            // 
            // txtServerName
            // 
            this.txtServerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerName.Location = new System.Drawing.Point(115, 19);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(257, 20);
            this.txtServerName.TabIndex = 0;
            // 
            // lblDBName
            // 
            this.lblDBName.AutoSize = true;
            this.lblDBName.Location = new System.Drawing.Point(13, 50);
            this.lblDBName.Name = "lblDBName";
            this.lblDBName.Size = new System.Drawing.Size(75, 13);
            this.lblDBName.TabIndex = 20;
            this.lblDBName.Text = "Cơ sở dữ liệu *";
            // 
            // txtPass
            // 
            this.txtPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPass.Location = new System.Drawing.Point(115, 103);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(257, 20);
            this.txtPass.TabIndex = 3;
            // 
            // txtDBName
            // 
            this.txtDBName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDBName.Location = new System.Drawing.Point(115, 47);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(257, 20);
            this.txtDBName.TabIndex = 1;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(13, 106);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(59, 13);
            this.lblPass.TabIndex = 24;
            this.lblPass.Text = "Mật khẩu *";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(13, 78);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(88, 13);
            this.lblUserName.TabIndex = 22;
            this.lblUserName.Text = "Tên đăng nhập *";
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Location = new System.Drawing.Point(115, 75);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(257, 20);
            this.txtUserName.TabIndex = 2;
            // 
            // frmThemSuaHeThongTichHop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 433);
            this.Controls.Add(this.grbDatabase);
            this.Controls.Add(this.grbInfTableUser);
            this.Controls.Add(this.grbInfSystem);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmThemSuaHeThongTichHop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống tích hợp chứng thư số";
            this.Load += new System.EventHandler(this.frmThemSuaHeThongTichHop_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmThemSuaHeThongTichHop_KeyDown);
            this.pnlFooter.ResumeLayout(false);
            this.grbInfSystem.ResumeLayout(false);
            this.grbInfSystem.PerformLayout();
            this.grbInfTableUser.ResumeLayout(false);
            this.grbInfTableUser.PerformLayout();
            this.grbDatabase.ResumeLayout(false);
            this.grbDatabase.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblProgID;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox grbInfSystem;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblProgName;
        private System.Windows.Forms.GroupBox grbInfTableUser;
        private System.Windows.Forms.Label lblTableUser;
        private System.Windows.Forms.TextBox txtQueryUser;
        private System.Windows.Forms.TextBox txtProgID;
        private System.Windows.Forms.TextBox txtProgName;
        private System.Windows.Forms.GroupBox grbDatabase;
        private System.Windows.Forms.Label lblServerName;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label lblDBName;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtDBName;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblNotation;
        private System.Windows.Forms.TextBox txtNotation;
    }
}