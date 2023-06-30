namespace ESLogin
{
    partial class frmLoginFull
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoginFull));
            this.lbBuiltVersion = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnOption = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.chk_Qtri = new System.Windows.Forms.CheckBox();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.pnlAbout = new System.Windows.Forms.Panel();
            this.lbESOLUTIONS = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lbResult = new System.Windows.Forms.Label();
            this.pnlMayChu = new System.Windows.Forms.Panel();
            this.Panel1.SuspendLayout();
            this.pnlAbout.SuspendLayout();
            this.pnlMayChu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbBuiltVersion
            // 
            this.lbBuiltVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbBuiltVersion.AutoSize = true;
            this.lbBuiltVersion.BackColor = System.Drawing.Color.Transparent;
            this.lbBuiltVersion.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBuiltVersion.ForeColor = System.Drawing.SystemColors.Control;
            this.lbBuiltVersion.Location = new System.Drawing.Point(442, 221);
            this.lbBuiltVersion.Name = "lbBuiltVersion";
            this.lbBuiltVersion.Size = new System.Drawing.Size(94, 14);
            this.lbBuiltVersion.TabIndex = 36;
            this.lbBuiltVersion.Text = "Phiên bản N.N.N.N";
            this.lbBuiltVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Panel1.Controls.Add(this.label5);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(539, 69);
            this.Panel1.TabIndex = 54;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label5.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.LavenderBlush;
            this.label5.Location = new System.Drawing.Point(103, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(402, 29);
            this.label5.TabIndex = 10;
            this.label5.Text = "HỆ THỐNG QUẢN TRỊ CHỮ KÝ SỐ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.Control;
            this.Label2.Location = new System.Drawing.Point(91, 125);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(70, 16);
            this.Label2.TabIndex = 52;
            this.Label2.Text = "Mật khẩu:";
            // 
            // btnOption
            // 
            this.btnOption.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOption.Image = ((System.Drawing.Image)(resources.GetObject("btnOption.Image")));
            this.btnOption.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOption.Location = new System.Drawing.Point(343, 189);
            this.btnOption.Name = "btnOption";
            this.btnOption.Size = new System.Drawing.Size(83, 24);
            this.btnOption.TabIndex = 57;
            this.btnOption.Text = "     Tùy chọn";
            this.btnOption.UseVisualStyleBackColor = true;
            this.btnOption.Click += new System.EventHandler(this.btnOption_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnLogin.Image")));
            this.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogin.Location = new System.Drawing.Point(167, 189);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(87, 24);
            this.btnLogin.TabIndex = 55;
            this.btnLogin.Text = "    Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(262, 189);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 24);
            this.btnExit.TabIndex = 56;
            this.btnExit.Text = "Hủy";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.Control;
            this.Label1.Location = new System.Drawing.Point(71, 85);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(91, 16);
            this.Label1.TabIndex = 53;
            this.Label1.Text = "Tên truy cập:";
            // 
            // chk_Qtri
            // 
            this.chk_Qtri.BackColor = System.Drawing.Color.Transparent;
            this.chk_Qtri.ForeColor = System.Drawing.SystemColors.Control;
            this.chk_Qtri.Location = new System.Drawing.Point(167, 151);
            this.chk_Qtri.Name = "chk_Qtri";
            this.chk_Qtri.Size = new System.Drawing.Size(170, 24);
            this.chk_Qtri.TabIndex = 49;
            this.chk_Qtri.Text = "Quyền quản trị cơ sở dữ liệu";
            this.chk_Qtri.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Qtri.UseVisualStyleBackColor = false;
            // 
            // txtDBName
            // 
            this.txtDBName.AcceptsReturn = true;
            this.txtDBName.BackColor = System.Drawing.Color.SteelBlue;
            this.txtDBName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDBName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDBName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBName.ForeColor = System.Drawing.SystemColors.Window;
            this.txtDBName.Location = new System.Drawing.Point(167, 50);
            this.txtDBName.MaxLength = 0;
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDBName.Size = new System.Drawing.Size(259, 23);
            this.txtDBName.TabIndex = 49;
            this.txtDBName.WordWrap = false;
            // 
            // txtServerName
            // 
            this.txtServerName.AcceptsReturn = true;
            this.txtServerName.BackColor = System.Drawing.Color.SteelBlue;
            this.txtServerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServerName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerName.ForeColor = System.Drawing.SystemColors.Window;
            this.txtServerName.Location = new System.Drawing.Point(168, 13);
            this.txtServerName.MaxLength = 0;
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtServerName.Size = new System.Drawing.Size(259, 23);
            this.txtServerName.TabIndex = 48;
            this.txtServerName.WordWrap = false;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.ForeColor = System.Drawing.SystemColors.Control;
            this.Label4.Location = new System.Drawing.Point(63, 52);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(98, 16);
            this.Label4.TabIndex = 41;
            this.Label4.Text = "Cơ sở dữ liệu:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.SystemColors.Control;
            this.Label3.Location = new System.Drawing.Point(97, 15);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(65, 16);
            this.Label3.TabIndex = 41;
            this.Label3.Text = "Máy chủ:";
            // 
            // txtPassword
            // 
            this.txtPassword.AcceptsReturn = true;
            this.txtPassword.BackColor = System.Drawing.Color.SteelBlue;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.SystemColors.Window;
            this.txtPassword.Location = new System.Drawing.Point(167, 123);
            this.txtPassword.MaxLength = 0;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPassword.Size = new System.Drawing.Size(259, 23);
            this.txtPassword.TabIndex = 48;
            this.txtPassword.WordWrap = false;
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // pnlAbout
            // 
            this.pnlAbout.BackColor = System.Drawing.Color.Transparent;
            this.pnlAbout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAbout.Controls.Add(this.lbESOLUTIONS);
            this.pnlAbout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAbout.Location = new System.Drawing.Point(0, 323);
            this.pnlAbout.Name = "pnlAbout";
            this.pnlAbout.Size = new System.Drawing.Size(539, 30);
            this.pnlAbout.TabIndex = 50;
            // 
            // lbESOLUTIONS
            // 
            this.lbESOLUTIONS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbESOLUTIONS.AutoSize = true;
            this.lbESOLUTIONS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbESOLUTIONS.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbESOLUTIONS.ForeColor = System.Drawing.Color.GhostWhite;
            this.lbESOLUTIONS.Location = new System.Drawing.Point(88, 5);
            this.lbESOLUTIONS.Name = "lbESOLUTIONS";
            this.lbESOLUTIONS.Size = new System.Drawing.Size(367, 14);
            this.lbESOLUTIONS.TabIndex = 9;
            this.lbESOLUTIONS.Text = "Công ty Cổ phần Giải pháp Quản lý Năng lượng - www.e-solutions.com.vn";
            this.lbESOLUTIONS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbESOLUTIONS.Click += new System.EventHandler(this.lbESOLUTIONS_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.AcceptsReturn = true;
            this.txtUserName.BackColor = System.Drawing.Color.SteelBlue;
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.ForeColor = System.Drawing.SystemColors.Window;
            this.txtUserName.Location = new System.Drawing.Point(167, 83);
            this.txtUserName.MaxLength = 0;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUserName.Size = new System.Drawing.Size(259, 23);
            this.txtUserName.TabIndex = 47;
            this.txtUserName.WordWrap = false;
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            // 
            // lbResult
            // 
            this.lbResult.AutoSize = true;
            this.lbResult.BackColor = System.Drawing.Color.Transparent;
            this.lbResult.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbResult.ForeColor = System.Drawing.SystemColors.Control;
            this.lbResult.Location = new System.Drawing.Point(4, 221);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(88, 14);
            this.lbResult.TabIndex = 60;
            this.lbResult.Text = "Thông tin license";
            this.lbResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlMayChu
            // 
            this.pnlMayChu.BackColor = System.Drawing.Color.Transparent;
            this.pnlMayChu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMayChu.Controls.Add(this.txtDBName);
            this.pnlMayChu.Controls.Add(this.txtServerName);
            this.pnlMayChu.Controls.Add(this.Label4);
            this.pnlMayChu.Controls.Add(this.Label3);
            this.pnlMayChu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMayChu.Location = new System.Drawing.Point(0, 238);
            this.pnlMayChu.Name = "pnlMayChu";
            this.pnlMayChu.Size = new System.Drawing.Size(539, 85);
            this.pnlMayChu.TabIndex = 51;
            // 
            // frmLoginFull
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(539, 353);
            this.Controls.Add(this.lbBuiltVersion);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.btnOption);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.chk_Qtri);
            this.Controls.Add(this.pnlMayChu);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.pnlAbout);
            this.Controls.Add(this.txtUserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmLoginFull";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.Load += new System.EventHandler(this.frmLoginFull_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.pnlAbout.ResumeLayout(false);
            this.pnlAbout.PerformLayout();
            this.pnlMayChu.ResumeLayout(false);
            this.pnlMayChu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lbBuiltVersion;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btnOption;
        internal System.Windows.Forms.Button btnLogin;
        internal System.Windows.Forms.Button btnExit;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.CheckBox chk_Qtri;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        public System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.Panel pnlAbout;
        private System.Windows.Forms.Label lbESOLUTIONS;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtDBName;
        private System.Windows.Forms.TextBox txtServerName;
        internal System.Windows.Forms.Label lbResult;
        internal System.Windows.Forms.Panel pnlMayChu;
        private System.Windows.Forms.Label label5;

    }
}