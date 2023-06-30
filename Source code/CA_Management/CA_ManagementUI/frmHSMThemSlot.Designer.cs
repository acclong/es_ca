namespace ES.CA_ManagementUI
{
    partial class frmHSMThemSlot
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
            this.txtConfirmUserPin = new System.Windows.Forms.TextBox();
            this.lblConfirmUserPin = new System.Windows.Forms.Label();
            this.txtConfirmSOPin = new System.Windows.Forms.TextBox();
            this.lblConfirmSOPin = new System.Windows.Forms.Label();
            this.txtUserPIN = new System.Windows.Forms.TextBox();
            this.lblLabel = new System.Windows.Forms.Label();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.lblSOPIN = new System.Windows.Forms.Label();
            this.txtSOPIN = new System.Windows.Forms.TextBox();
            this.lblUserPIN = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grbLoaiMaThayDoi = new System.Windows.Forms.GroupBox();
            this.cboHSM = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlFooter.SuspendLayout();
            this.grbLoaiMaThayDoi.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtConfirmUserPin
            // 
            this.txtConfirmUserPin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfirmUserPin.Location = new System.Drawing.Point(117, 131);
            this.txtConfirmUserPin.Name = "txtConfirmUserPin";
            this.txtConfirmUserPin.PasswordChar = '*';
            this.txtConfirmUserPin.Size = new System.Drawing.Size(273, 20);
            this.txtConfirmUserPin.TabIndex = 18;
            this.toolTip.SetToolTip(this.txtConfirmUserPin, "Mã PIN chỉ chứa ký tự không dấu và độ dài từ 4 đến 32 ký tự");
            // 
            // lblConfirmUserPin
            // 
            this.lblConfirmUserPin.AutoSize = true;
            this.lblConfirmUserPin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblConfirmUserPin.Location = new System.Drawing.Point(12, 134);
            this.lblConfirmUserPin.Name = "lblConfirmUserPin";
            this.lblConfirmUserPin.Size = new System.Drawing.Size(103, 13);
            this.lblConfirmUserPin.TabIndex = 19;
            this.lblConfirmUserPin.Text = "Xác nhận User PIN*";
            // 
            // txtConfirmSOPin
            // 
            this.txtConfirmSOPin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfirmSOPin.Location = new System.Drawing.Point(117, 75);
            this.txtConfirmSOPin.Name = "txtConfirmSOPin";
            this.txtConfirmSOPin.PasswordChar = '*';
            this.txtConfirmSOPin.Size = new System.Drawing.Size(273, 20);
            this.txtConfirmSOPin.TabIndex = 16;
            this.toolTip.SetToolTip(this.txtConfirmSOPin, "Mã PIN chỉ chứa ký tự không dấu và độ dài từ 4 đến 32 ký tự");
            // 
            // lblConfirmSOPin
            // 
            this.lblConfirmSOPin.AutoSize = true;
            this.lblConfirmSOPin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblConfirmSOPin.Location = new System.Drawing.Point(12, 78);
            this.lblConfirmSOPin.Name = "lblConfirmSOPin";
            this.lblConfirmSOPin.Size = new System.Drawing.Size(96, 13);
            this.lblConfirmSOPin.TabIndex = 17;
            this.lblConfirmSOPin.Text = "Xác nhận SO PIN*";
            // 
            // txtUserPIN
            // 
            this.txtUserPIN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserPIN.Location = new System.Drawing.Point(117, 103);
            this.txtUserPIN.Name = "txtUserPIN";
            this.txtUserPIN.PasswordChar = '*';
            this.txtUserPIN.Size = new System.Drawing.Size(273, 20);
            this.txtUserPIN.TabIndex = 17;
            this.toolTip.SetToolTip(this.txtUserPIN, "Mã PIN chỉ chứa ký tự không dấu và độ dài từ 4 đến 32 ký tự");
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblLabel.Location = new System.Drawing.Point(12, 22);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(99, 13);
            this.lblLabel.TabIndex = 7;
            this.lblLabel.Text = "Tên (Token Label)*";
            // 
            // txtLabel
            // 
            this.txtLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLabel.Location = new System.Drawing.Point(117, 19);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(273, 20);
            this.txtLabel.TabIndex = 14;
            this.toolTip.SetToolTip(this.txtLabel, "Token Label chỉ chứa chữ cái, số và dấu gạch dưới");
            // 
            // lblSOPIN
            // 
            this.lblSOPIN.AutoSize = true;
            this.lblSOPIN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSOPIN.Location = new System.Drawing.Point(12, 50);
            this.lblSOPIN.Name = "lblSOPIN";
            this.lblSOPIN.Size = new System.Drawing.Size(47, 13);
            this.lblSOPIN.TabIndex = 8;
            this.lblSOPIN.Text = "SO PIN*";
            // 
            // txtSOPIN
            // 
            this.txtSOPIN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSOPIN.Location = new System.Drawing.Point(117, 47);
            this.txtSOPIN.Name = "txtSOPIN";
            this.txtSOPIN.PasswordChar = '*';
            this.txtSOPIN.Size = new System.Drawing.Size(273, 20);
            this.txtSOPIN.TabIndex = 15;
            this.toolTip.SetToolTip(this.txtSOPIN, "Mã PIN chỉ chứa ký tự không dấu và độ dài từ 4 đến 32 ký tự");
            // 
            // lblUserPIN
            // 
            this.lblUserPIN.AutoSize = true;
            this.lblUserPIN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUserPIN.Location = new System.Drawing.Point(12, 106);
            this.lblUserPIN.Name = "lblUserPIN";
            this.lblUserPIN.Size = new System.Drawing.Size(54, 13);
            this.lblUserPIN.TabIndex = 9;
            this.lblUserPIN.Text = "User PIN*";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnExit);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 221);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(402, 40);
            this.pnlFooter.TabIndex = 6;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(206, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 23);
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
            this.btnSave.Size = new System.Drawing.Size(74, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Đồng ý";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grbLoaiMaThayDoi
            // 
            this.grbLoaiMaThayDoi.Controls.Add(this.cboHSM);
            this.grbLoaiMaThayDoi.Controls.Add(this.lblStatus);
            this.grbLoaiMaThayDoi.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbLoaiMaThayDoi.Location = new System.Drawing.Point(0, 0);
            this.grbLoaiMaThayDoi.Name = "grbLoaiMaThayDoi";
            this.grbLoaiMaThayDoi.Size = new System.Drawing.Size(402, 52);
            this.grbLoaiMaThayDoi.TabIndex = 8;
            this.grbLoaiMaThayDoi.TabStop = false;
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
            this.cboHSM.Size = new System.Drawing.Size(191, 21);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtConfirmUserPin);
            this.groupBox1.Controls.Add(this.lblLabel);
            this.groupBox1.Controls.Add(this.lblConfirmUserPin);
            this.groupBox1.Controls.Add(this.lblUserPIN);
            this.groupBox1.Controls.Add(this.txtConfirmSOPin);
            this.groupBox1.Controls.Add(this.txtSOPIN);
            this.groupBox1.Controls.Add(this.lblConfirmSOPin);
            this.groupBox1.Controls.Add(this.lblSOPIN);
            this.groupBox1.Controls.Add(this.txtUserPIN);
            this.groupBox1.Controls.Add(this.txtLabel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(402, 169);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin khởi tạo slot";
            // 
            // frmThemSlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 261);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbLoaiMaThayDoi);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmThemSlot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm slot";
            this.Load += new System.EventHandler(this.frmThemSlot_Load);
            this.pnlFooter.ResumeLayout(false);
            this.grbLoaiMaThayDoi.ResumeLayout(false);
            this.grbLoaiMaThayDoi.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.Label lblSOPIN;
        private System.Windows.Forms.TextBox txtSOPIN;
        private System.Windows.Forms.Label lblUserPIN;
        private System.Windows.Forms.TextBox txtUserPIN;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtConfirmUserPin;
        private System.Windows.Forms.Label lblConfirmUserPin;
        private System.Windows.Forms.TextBox txtConfirmSOPin;
        private System.Windows.Forms.Label lblConfirmSOPin;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox grbLoaiMaThayDoi;
        private System.Windows.Forms.ComboBox cboHSM;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}