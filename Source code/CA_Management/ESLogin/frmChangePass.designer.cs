namespace ESLogin
{
    partial class frmChangePass
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
            this.txtNewPwd2 = new System.Windows.Forms.TextBox();
            this.txtNewPwd1 = new System.Windows.Forms.TextBox();
            this.txtOldPwd = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.grbUser = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.grbUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNewPwd2
            // 
            this.txtNewPwd2.AcceptsReturn = true;
            this.txtNewPwd2.BackColor = System.Drawing.SystemColors.Window;
            this.txtNewPwd2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNewPwd2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPwd2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNewPwd2.Location = new System.Drawing.Point(140, 67);
            this.txtNewPwd2.MaxLength = 0;
            this.txtNewPwd2.Name = "txtNewPwd2";
            this.txtNewPwd2.PasswordChar = '*';
            this.txtNewPwd2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNewPwd2.Size = new System.Drawing.Size(161, 20);
            this.txtNewPwd2.TabIndex = 6;
            // 
            // txtNewPwd1
            // 
            this.txtNewPwd1.AcceptsReturn = true;
            this.txtNewPwd1.BackColor = System.Drawing.SystemColors.Window;
            this.txtNewPwd1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNewPwd1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPwd1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNewPwd1.Location = new System.Drawing.Point(140, 43);
            this.txtNewPwd1.MaxLength = 0;
            this.txtNewPwd1.Name = "txtNewPwd1";
            this.txtNewPwd1.PasswordChar = '*';
            this.txtNewPwd1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNewPwd1.Size = new System.Drawing.Size(161, 20);
            this.txtNewPwd1.TabIndex = 4;
            // 
            // txtOldPwd
            // 
            this.txtOldPwd.AcceptsReturn = true;
            this.txtOldPwd.BackColor = System.Drawing.SystemColors.Window;
            this.txtOldPwd.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtOldPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOldPwd.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtOldPwd.Location = new System.Drawing.Point(140, 19);
            this.txtOldPwd.MaxLength = 0;
            this.txtOldPwd.Name = "txtOldPwd";
            this.txtOldPwd.PasswordChar = '*';
            this.txtOldPwd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtOldPwd.Size = new System.Drawing.Size(161, 20);
            this.txtOldPwd.TabIndex = 2;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.SystemColors.Control;
            this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label3.Location = new System.Drawing.Point(6, 70);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label3.Size = new System.Drawing.Size(128, 17);
            this.Label3.TabIndex = 5;
            this.Label3.Text = "Nhập lại mật khẩu mới :";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.SystemColors.Control;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.Location = new System.Drawing.Point(6, 46);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(89, 25);
            this.Label2.TabIndex = 3;
            this.Label2.Text = "Mật khẩu mới :";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(6, 22);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(73, 17);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Mật khẩu cũ :";
            // 
            // grbUser
            // 
            this.grbUser.BackColor = System.Drawing.SystemColors.Control;
            this.grbUser.Controls.Add(this.txtNewPwd2);
            this.grbUser.Controls.Add(this.txtNewPwd1);
            this.grbUser.Controls.Add(this.txtOldPwd);
            this.grbUser.Controls.Add(this.Label3);
            this.grbUser.Controls.Add(this.Label2);
            this.grbUser.Controls.Add(this.Label1);
            this.grbUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbUser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grbUser.Location = new System.Drawing.Point(0, 0);
            this.grbUser.Name = "grbUser";
            this.grbUser.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grbUser.Size = new System.Drawing.Size(327, 97);
            this.grbUser.TabIndex = 12;
            this.grbUser.TabStop = false;
            this.grbUser.Text = "abc";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::ESLogin.Properties.Resources.Save;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(85, 103);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Image = global::ESLogin.Properties.Resources.Close;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(166, 103);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 16;
            this.btnExit.Text = "Đóng";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmChangePass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 132);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grbUser);
            this.Name = "frmChangePass";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thay đổi mật khẩu";
            this.Load += new System.EventHandler(this.frmChangePass_Load);
            this.grbUser.ResumeLayout(false);
            this.grbUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TextBox txtNewPwd2;
        public System.Windows.Forms.TextBox txtNewPwd1;
        public System.Windows.Forms.TextBox txtOldPwd;
        public System.Windows.Forms.Label Label3;
        public System.Windows.Forms.Label Label2;
        public System.Windows.Forms.Label Label1;
        public System.Windows.Forms.GroupBox grbUser;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
    }
}