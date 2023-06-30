namespace ES.CA_ManagementUI
{
    partial class frmHSMDoiPassAdmin
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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grbLoaiMaThayDoi = new System.Windows.Forms.GroupBox();
            this.rbSOPIN = new System.Windows.Forms.RadioButton();
            this.rbUserPIN = new System.Windows.Forms.RadioButton();
            this.cboHSM = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.grbMaPin = new System.Windows.Forms.GroupBox();
            this.txtRePIN = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtOldPIN = new System.Windows.Forms.TextBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.txtNewPIN = new System.Windows.Forms.TextBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grbLoaiMaThayDoi.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.grbMaPin.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(206, 9);
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
            this.btnSave.Location = new System.Drawing.Point(123, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Đồng ý";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grbLoaiMaThayDoi
            // 
            this.grbLoaiMaThayDoi.Controls.Add(this.rbSOPIN);
            this.grbLoaiMaThayDoi.Controls.Add(this.rbUserPIN);
            this.grbLoaiMaThayDoi.Controls.Add(this.cboHSM);
            this.grbLoaiMaThayDoi.Controls.Add(this.lblStatus);
            this.grbLoaiMaThayDoi.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbLoaiMaThayDoi.Location = new System.Drawing.Point(0, 0);
            this.grbLoaiMaThayDoi.Name = "grbLoaiMaThayDoi";
            this.grbLoaiMaThayDoi.Size = new System.Drawing.Size(404, 99);
            this.grbLoaiMaThayDoi.TabIndex = 7;
            this.grbLoaiMaThayDoi.TabStop = false;
            // 
            // rbSOPIN
            // 
            this.rbSOPIN.AutoSize = true;
            this.rbSOPIN.Location = new System.Drawing.Point(126, 49);
            this.rbSOPIN.Name = "rbSOPIN";
            this.rbSOPIN.Size = new System.Drawing.Size(124, 17);
            this.rbSOPIN.TabIndex = 1;
            this.rbSOPIN.TabStop = true;
            this.rbSOPIN.Text = "Administrator SO PIN";
            this.rbSOPIN.UseVisualStyleBackColor = true;
            // 
            // rbUserPIN
            // 
            this.rbUserPIN.AutoSize = true;
            this.rbUserPIN.Checked = true;
            this.rbUserPIN.Location = new System.Drawing.Point(126, 72);
            this.rbUserPIN.Name = "rbUserPIN";
            this.rbUserPIN.Size = new System.Drawing.Size(131, 17);
            this.rbUserPIN.TabIndex = 0;
            this.rbUserPIN.TabStop = true;
            this.rbUserPIN.Text = "Administrator User PIN";
            this.rbUserPIN.UseVisualStyleBackColor = true;
            // 
            // cboHSM
            // 
            this.cboHSM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboHSM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHSM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboHSM.FormattingEnabled = true;
            this.cboHSM.Location = new System.Drawing.Point(126, 19);
            this.cboHSM.Name = "cboHSM";
            this.cboHSM.Size = new System.Drawing.Size(193, 21);
            this.cboHSM.TabIndex = 105;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(51, 22);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(69, 13);
            this.lblStatus.TabIndex = 104;
            this.lblStatus.Text = "Thiết bị HSM";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnExit);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 207);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(404, 40);
            this.pnlFooter.TabIndex = 6;
            // 
            // grbMaPin
            // 
            this.grbMaPin.Controls.Add(this.txtRePIN);
            this.grbMaPin.Controls.Add(this.lblUserName);
            this.grbMaPin.Controls.Add(this.txtOldPIN);
            this.grbMaPin.Controls.Add(this.lblDepartment);
            this.grbMaPin.Controls.Add(this.txtNewPIN);
            this.grbMaPin.Controls.Add(this.lblUnit);
            this.grbMaPin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbMaPin.Location = new System.Drawing.Point(0, 99);
            this.grbMaPin.Name = "grbMaPin";
            this.grbMaPin.Size = new System.Drawing.Size(404, 108);
            this.grbMaPin.TabIndex = 8;
            this.grbMaPin.TabStop = false;
            this.grbMaPin.Text = "Thông tin thay đổi";
            // 
            // txtRePIN
            // 
            this.txtRePIN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRePIN.Location = new System.Drawing.Point(126, 77);
            this.txtRePIN.Name = "txtRePIN";
            this.txtRePIN.PasswordChar = '*';
            this.txtRePIN.Size = new System.Drawing.Size(267, 20);
            this.txtRePIN.TabIndex = 22;
            this.toolTip.SetToolTip(this.txtRePIN, "Mã pin không chứa ký tự có dấu và có độ dài từ 4 đến 32 ký tự");
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(13, 24);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(62, 13);
            this.lblUserName.TabIndex = 17;
            this.lblUserName.Text = "Mã PIN cũ*";
            // 
            // txtOldPIN
            // 
            this.txtOldPIN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOldPIN.Location = new System.Drawing.Point(126, 21);
            this.txtOldPIN.Name = "txtOldPIN";
            this.txtOldPIN.PasswordChar = '*';
            this.txtOldPIN.Size = new System.Drawing.Size(267, 20);
            this.txtOldPIN.TabIndex = 20;
            this.toolTip.SetToolTip(this.txtOldPIN, "Mã pin không chứa ký tự có dấu và có độ dài từ 4 đến 32 ký tự");
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Location = new System.Drawing.Point(13, 52);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(66, 13);
            this.lblDepartment.TabIndex = 18;
            this.lblDepartment.Text = "Mã PIN mới*";
            // 
            // txtNewPIN
            // 
            this.txtNewPIN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewPIN.Location = new System.Drawing.Point(126, 49);
            this.txtNewPIN.Name = "txtNewPIN";
            this.txtNewPIN.PasswordChar = '*';
            this.txtNewPIN.Size = new System.Drawing.Size(267, 20);
            this.txtNewPIN.TabIndex = 21;
            this.toolTip.SetToolTip(this.txtNewPIN, "Mã pin không chứa ký tự có dấu và có độ dài từ 4 đến 32 ký tự");
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new System.Drawing.Point(13, 80);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(107, 13);
            this.lblUnit.TabIndex = 19;
            this.lblUnit.Text = "Nhập lại mã PIN mới*";
            // 
            // frmHSMDoiPassAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 247);
            this.Controls.Add(this.grbMaPin);
            this.Controls.Add(this.grbLoaiMaThayDoi);
            this.Controls.Add(this.pnlFooter);
            this.Name = "frmHSMDoiPassAdmin";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thay đổi mật khẩu quản trị HSM";
            this.Load += new System.EventHandler(this.frmHSMDoiPassAdmin_Load);
            this.grbLoaiMaThayDoi.ResumeLayout(false);
            this.grbLoaiMaThayDoi.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.grbMaPin.ResumeLayout(false);
            this.grbMaPin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grbLoaiMaThayDoi;
        private System.Windows.Forms.RadioButton rbSOPIN;
        private System.Windows.Forms.RadioButton rbUserPIN;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.GroupBox grbMaPin;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtOldPIN;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.TextBox txtNewPIN;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.TextBox txtRePIN;
        private System.Windows.Forms.ComboBox cboHSM;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ToolTip toolTip;

    }
}