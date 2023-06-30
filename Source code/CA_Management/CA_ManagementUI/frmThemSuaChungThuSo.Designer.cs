namespace ES.CA_ManagementUI
{
    partial class frmThemSuaChungThuSo
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.grbQuanLy = new System.Windows.Forms.GroupBox();
            this.txtCertID = new System.Windows.Forms.TextBox();
            this.lblCertID = new System.Windows.Forms.Label();
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
            this.cboCertType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.grbQuanLy.SuspendLayout();
            this.grbCA.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(225, 469);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(144, 469);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadFile.Location = new System.Drawing.Point(63, 469);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFile.TabIndex = 0;
            this.btnLoadFile.Text = "Load file";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(87, 47);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(263, 21);
            this.cboStatus.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Trạng thái";
            // 
            // grbQuanLy
            // 
            this.grbQuanLy.Controls.Add(this.cboCertType);
            this.grbQuanLy.Controls.Add(this.label8);
            this.grbQuanLy.Controls.Add(this.txtCertID);
            this.grbQuanLy.Controls.Add(this.lblCertID);
            this.grbQuanLy.Controls.Add(this.cboStatus);
            this.grbQuanLy.Controls.Add(this.label7);
            this.grbQuanLy.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbQuanLy.Location = new System.Drawing.Point(0, 0);
            this.grbQuanLy.Name = "grbQuanLy";
            this.grbQuanLy.Size = new System.Drawing.Size(363, 103);
            this.grbQuanLy.TabIndex = 1;
            this.grbQuanLy.TabStop = false;
            this.grbQuanLy.Text = "Thông tin quản lý";
            // 
            // txtCertID
            // 
            this.txtCertID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCertID.Location = new System.Drawing.Point(87, 19);
            this.txtCertID.Name = "txtCertID";
            this.txtCertID.ReadOnly = true;
            this.txtCertID.Size = new System.Drawing.Size(263, 20);
            this.txtCertID.TabIndex = 19;
            // 
            // lblCertID
            // 
            this.lblCertID.AutoSize = true;
            this.lblCertID.Location = new System.Drawing.Point(12, 22);
            this.lblCertID.Name = "lblCertID";
            this.lblCertID.Size = new System.Drawing.Size(18, 13);
            this.lblCertID.TabIndex = 18;
            this.lblCertID.Text = "ID";
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
            this.grbCA.Location = new System.Drawing.Point(0, 103);
            this.grbCA.Name = "grbCA";
            this.grbCA.Size = new System.Drawing.Size(363, 361);
            this.grbCA.TabIndex = 19;
            this.grbCA.TabStop = false;
            this.grbCA.Text = "Certificate";
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
            this.label9.Location = new System.Drawing.Point(12, 22);
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
            this.label6.Location = new System.Drawing.Point(12, 302);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "ThumbPrint";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Hiệu lực đến";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Hiệu lực từ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Đơn vị cấp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên CA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Serial";
            // 
            // cboCertType
            // 
            this.cboCertType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCertType.FormattingEnabled = true;
            this.cboCertType.Location = new System.Drawing.Point(87, 76);
            this.cboCertType.Name = "cboCertType";
            this.cboCertType.Size = new System.Drawing.Size(263, 21);
            this.cboCertType.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Loại";
            // 
            // frmThemSuaChungThuSo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 497);
            this.Controls.Add(this.grbCA);
            this.Controls.Add(this.grbQuanLy);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoadFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmThemSuaChungThuSo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmThemSuaChungThuSo";
            this.Load += new System.EventHandler(this.frmThemSuaChungThuSo_Load);
            this.grbQuanLy.ResumeLayout(false);
            this.grbQuanLy.PerformLayout();
            this.grbCA.ResumeLayout(false);
            this.grbCA.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox grbQuanLy;
        private System.Windows.Forms.TextBox txtCertID;
        private System.Windows.Forms.Label lblCertID;
        private System.Windows.Forms.GroupBox grbCA;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtThumbPrint;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtIssuer;
        private System.Windows.Forms.TextBox txtValidFrom;
        private System.Windows.Forms.TextBox txtValidTo;
        private System.Windows.Forms.TextBox txtSerial;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboCertType;
        private System.Windows.Forms.Label label8;
    }
}