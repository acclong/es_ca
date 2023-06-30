namespace ES.CA_ManagementUI
{
    partial class frmResetPin
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
            this.components = new System.ComponentModel.Container();
            this.txtUserPin = new System.Windows.Forms.TextBox();
            this.lblUserPin = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtConfirmUserPin = new System.Windows.Forms.TextBox();
            this.lblConfirmUserPin = new System.Windows.Forms.Label();
            this.chkHSM = new System.Windows.Forms.CheckBox();
            this.chkDB = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUserPin
            // 
            this.txtUserPin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserPin.Location = new System.Drawing.Point(140, 6);
            this.txtUserPin.Name = "txtUserPin";
            this.txtUserPin.PasswordChar = '*';
            this.txtUserPin.Size = new System.Drawing.Size(202, 20);
            this.txtUserPin.TabIndex = 20;
            this.toolTip.SetToolTip(this.txtUserPin, "Mã PIN chỉ chứa ký tự không dấu và độ dài từ 4 đến 32 ký tự");
            // 
            // lblUserPin
            // 
            this.lblUserPin.AutoSize = true;
            this.lblUserPin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUserPin.Location = new System.Drawing.Point(12, 9);
            this.lblUserPin.Name = "lblUserPin";
            this.lblUserPin.Size = new System.Drawing.Size(73, 13);
            this.lblUserPin.TabIndex = 21;
            this.lblUserPin.Text = "User PIN mới*";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnExit);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 78);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(354, 40);
            this.pnlFooter.TabIndex = 22;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(181, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Hủy";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(98, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtConfirmUserPin
            // 
            this.txtConfirmUserPin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfirmUserPin.Location = new System.Drawing.Point(140, 32);
            this.txtConfirmUserPin.Name = "txtConfirmUserPin";
            this.txtConfirmUserPin.PasswordChar = '*';
            this.txtConfirmUserPin.Size = new System.Drawing.Size(202, 20);
            this.txtConfirmUserPin.TabIndex = 23;
            this.toolTip.SetToolTip(this.txtConfirmUserPin, "Mã PIN chỉ chứa ký tự không dấu và độ dài từ 4 đến 32 ký tự");
            // 
            // lblConfirmUserPin
            // 
            this.lblConfirmUserPin.AutoSize = true;
            this.lblConfirmUserPin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblConfirmUserPin.Location = new System.Drawing.Point(12, 35);
            this.lblConfirmUserPin.Name = "lblConfirmUserPin";
            this.lblConfirmUserPin.Size = new System.Drawing.Size(122, 13);
            this.lblConfirmUserPin.TabIndex = 24;
            this.lblConfirmUserPin.Text = "Xác nhận User PIN mới*";
            // 
            // chkHSM
            // 
            this.chkHSM.AutoSize = true;
            this.chkHSM.Location = new System.Drawing.Point(86, 58);
            this.chkHSM.Name = "chkHSM";
            this.chkHSM.Size = new System.Drawing.Size(88, 17);
            this.chkHSM.TabIndex = 30;
            this.chkHSM.Text = "Thiết bị HSM";
            this.chkHSM.UseVisualStyleBackColor = true;
            // 
            // chkDB
            // 
            this.chkDB.AutoSize = true;
            this.chkDB.Enabled = false;
            this.chkDB.Location = new System.Drawing.Point(182, 58);
            this.chkDB.Name = "chkDB";
            this.chkDB.Size = new System.Drawing.Size(87, 17);
            this.chkDB.TabIndex = 29;
            this.chkDB.Text = "Cơ sở dữ liệu";
            this.chkDB.UseVisualStyleBackColor = true;
            // 
            // frmResetPin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 118);
            this.ControlBox = false;
            this.Controls.Add(this.chkHSM);
            this.Controls.Add(this.chkDB);
            this.Controls.Add(this.txtConfirmUserPin);
            this.Controls.Add(this.lblConfirmUserPin);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.txtUserPin);
            this.Controls.Add(this.lblUserPin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmResetPin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xác lập lại mã PIN";
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUserPin;
        private System.Windows.Forms.Label lblUserPin;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtConfirmUserPin;
        private System.Windows.Forms.Label lblConfirmUserPin;
        private System.Windows.Forms.CheckBox chkHSM;
        private System.Windows.Forms.CheckBox chkDB;
        private System.Windows.Forms.ToolTip toolTip;

    }
}