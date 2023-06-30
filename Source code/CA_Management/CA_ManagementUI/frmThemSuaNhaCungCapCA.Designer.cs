namespace ES.CA_ManagementUI
{
    partial class frmThemSuaNhaCungCapCA
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
            this.grbCA = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtThumbPrint = new System.Windows.Forms.TextBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.txtIssuer = new System.Windows.Forms.TextBox();
            this.txtValidFrom = new System.Windows.Forms.TextBox();
            this.txtValidTo = new System.Windows.Forms.TextBox();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grbInfo = new System.Windows.Forms.GroupBox();
            this.chkShowRevoked = new System.Windows.Forms.CheckBox();
            this.dpkRevoked = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCRL = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grbCA.SuspendLayout();
            this.grbInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbCA
            // 
            this.grbCA.Controls.Add(this.txtName);
            this.grbCA.Controls.Add(this.label9);
            this.grbCA.Controls.Add(this.txtThumbPrint);
            this.grbCA.Controls.Add(this.txtSubject);
            this.grbCA.Controls.Add(this.txtIssuer);
            this.grbCA.Controls.Add(this.txtValidFrom);
            this.grbCA.Controls.Add(this.txtValidTo);
            this.grbCA.Controls.Add(this.txtSerial);
            this.grbCA.Controls.Add(this.label6);
            this.grbCA.Controls.Add(this.label5);
            this.grbCA.Controls.Add(this.label4);
            this.grbCA.Controls.Add(this.label3);
            this.grbCA.Controls.Add(this.label2);
            this.grbCA.Controls.Add(this.label1);
            this.grbCA.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbCA.Location = new System.Drawing.Point(0, 0);
            this.grbCA.Name = "grbCA";
            this.grbCA.Size = new System.Drawing.Size(362, 359);
            this.grbCA.TabIndex = 1;
            this.grbCA.TabStop = false;
            this.grbCA.Text = "Certification Authority";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(87, 19);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(263, 20);
            this.txtName.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Tên";
            // 
            // txtThumbPrint
            // 
            this.txtThumbPrint.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThumbPrint.Location = new System.Drawing.Point(87, 299);
            this.txtThumbPrint.Multiline = true;
            this.txtThumbPrint.Name = "txtThumbPrint";
            this.txtThumbPrint.ReadOnly = true;
            this.txtThumbPrint.Size = new System.Drawing.Size(263, 53);
            this.txtThumbPrint.TabIndex = 11;
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(87, 71);
            this.txtSubject.Multiline = true;
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.ReadOnly = true;
            this.txtSubject.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSubject.Size = new System.Drawing.Size(263, 82);
            this.txtSubject.TabIndex = 10;
            // 
            // txtIssuer
            // 
            this.txtIssuer.Location = new System.Drawing.Point(87, 159);
            this.txtIssuer.Multiline = true;
            this.txtIssuer.Name = "txtIssuer";
            this.txtIssuer.ReadOnly = true;
            this.txtIssuer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIssuer.Size = new System.Drawing.Size(263, 82);
            this.txtIssuer.TabIndex = 9;
            // 
            // txtValidFrom
            // 
            this.txtValidFrom.Location = new System.Drawing.Point(87, 247);
            this.txtValidFrom.Name = "txtValidFrom";
            this.txtValidFrom.ReadOnly = true;
            this.txtValidFrom.Size = new System.Drawing.Size(263, 20);
            this.txtValidFrom.TabIndex = 8;
            // 
            // txtValidTo
            // 
            this.txtValidTo.Location = new System.Drawing.Point(87, 273);
            this.txtValidTo.Name = "txtValidTo";
            this.txtValidTo.ReadOnly = true;
            this.txtValidTo.Size = new System.Drawing.Size(263, 20);
            this.txtValidTo.TabIndex = 7;
            // 
            // txtSerial
            // 
            this.txtSerial.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerial.Location = new System.Drawing.Point(87, 45);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.ReadOnly = true;
            this.txtSerial.Size = new System.Drawing.Size(263, 20);
            this.txtSerial.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 302);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "ThumbPrint";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Hiệu lực đến";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Hiệu lực từ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Đơn vị cấp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên CA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Serial";
            // 
            // grbInfo
            // 
            this.grbInfo.Controls.Add(this.chkShowRevoked);
            this.grbInfo.Controls.Add(this.dpkRevoked);
            this.grbInfo.Controls.Add(this.label8);
            this.grbInfo.Controls.Add(this.txtCRL);
            this.grbInfo.Controls.Add(this.label7);
            this.grbInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbInfo.Location = new System.Drawing.Point(0, 359);
            this.grbInfo.Name = "grbInfo";
            this.grbInfo.Size = new System.Drawing.Size(362, 71);
            this.grbInfo.TabIndex = 2;
            this.grbInfo.TabStop = false;
            this.grbInfo.Text = "Thông tin";
            // 
            // chkShowRevoked
            // 
            this.chkShowRevoked.AutoSize = true;
            this.chkShowRevoked.Location = new System.Drawing.Point(103, 46);
            this.chkShowRevoked.Name = "chkShowRevoked";
            this.chkShowRevoked.Size = new System.Drawing.Size(15, 14);
            this.chkShowRevoked.TabIndex = 0;
            this.chkShowRevoked.UseVisualStyleBackColor = true;
            this.chkShowRevoked.CheckedChanged += new System.EventHandler(this.chkShowRevoked_CheckedChanged);
            // 
            // dpkRevoked
            // 
            this.dpkRevoked.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dpkRevoked.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkRevoked.Location = new System.Drawing.Point(132, 43);
            this.dpkRevoked.Name = "dpkRevoked";
            this.dpkRevoked.Size = new System.Drawing.Size(218, 20);
            this.dpkRevoked.TabIndex = 1;
            this.dpkRevoked.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Thu hồi từ";
            // 
            // txtCRL
            // 
            this.txtCRL.Location = new System.Drawing.Point(103, 17);
            this.txtCRL.Name = "txtCRL";
            this.txtCRL.Size = new System.Drawing.Size(247, 20);
            this.txtCRL.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Đường dẫn CRL";
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(63, 437);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFile.TabIndex = 0;
            this.btnLoadFile.Text = "Load file";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(144, 437);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(225, 437);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmThemSuaNhaCungCapCA
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 467);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.grbInfo);
            this.Controls.Add(this.grbCA);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmThemSuaNhaCungCapCA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm/Sửa nhà cung cấp CA";
            this.Load += new System.EventHandler(this.frmThemNhaCungCapCA_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmThemSuaNhaCungCapCA_KeyDown);
            this.grbCA.ResumeLayout(false);
            this.grbCA.PerformLayout();
            this.grbInfo.ResumeLayout(false);
            this.grbInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbCA;
        private System.Windows.Forms.GroupBox grbInfo;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtThumbPrint;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtIssuer;
        private System.Windows.Forms.TextBox txtValidFrom;
        private System.Windows.Forms.TextBox txtValidTo;
        private System.Windows.Forms.TextBox txtSerial;
        private System.Windows.Forms.DateTimePicker dpkRevoked;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCRL;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkShowRevoked;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label9;
    }
}