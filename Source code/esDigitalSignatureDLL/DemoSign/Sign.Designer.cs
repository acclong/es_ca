namespace DemoSign
{
    partial class Sign
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
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnSign = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSeri = new System.Windows.Forms.TextBox();
            this.btnFileCRT = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.rbXml = new System.Windows.Forms.RadioButton();
            this.rbOffice = new System.Windows.Forms.RadioButton();
            this.rbPdf = new System.Windows.Forms.RadioButton();
            this.rbString = new System.Windows.Forms.RadioButton();
            this.btnSelCert = new System.Windows.Forms.Button();
            this.btnGetHash = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSlotSerial = new System.Windows.Forms.TextBox();
            this.txtSlotPIN = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtKeyLabel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(12, 91);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(302, 20);
            this.txtPath.TabIndex = 2;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(321, 91);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(29, 20);
            this.btnFind.TabIndex = 3;
            this.btnFind.Text = "...";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnSign
            // 
            this.btnSign.Location = new System.Drawing.Point(12, 117);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(75, 23);
            this.btnSign.TabIndex = 4;
            this.btnSign.Text = "Ký file";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Seri Number Usb Token/File CRT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Đường dẫn file";
            // 
            // txtSeri
            // 
            this.txtSeri.Location = new System.Drawing.Point(12, 52);
            this.txtSeri.Name = "txtSeri";
            this.txtSeri.Size = new System.Drawing.Size(302, 20);
            this.txtSeri.TabIndex = 7;
            this.txtSeri.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSeri_KeyDown);
            // 
            // btnFileCRT
            // 
            this.btnFileCRT.Location = new System.Drawing.Point(321, 52);
            this.btnFileCRT.Name = "btnFileCRT";
            this.btnFileCRT.Size = new System.Drawing.Size(29, 20);
            this.btnFileCRT.TabIndex = 8;
            this.btnFileCRT.Text = "...";
            this.btnFileCRT.UseVisualStyleBackColor = true;
            this.btnFileCRT.Click += new System.EventHandler(this.btnFileCRT_Click);
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(93, 117);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(75, 23);
            this.btnVerify.TabIndex = 9;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // rbXml
            // 
            this.rbXml.AutoSize = true;
            this.rbXml.Location = new System.Drawing.Point(185, 12);
            this.rbXml.Name = "rbXml";
            this.rbXml.Size = new System.Drawing.Size(63, 17);
            this.rbXml.TabIndex = 10;
            this.rbXml.Text = "file XML";
            this.rbXml.UseVisualStyleBackColor = true;
            this.rbXml.CheckedChanged += new System.EventHandler(this.rbXml_CheckedChanged);
            // 
            // rbOffice
            // 
            this.rbOffice.AutoSize = true;
            this.rbOffice.Checked = true;
            this.rbOffice.Location = new System.Drawing.Point(12, 12);
            this.rbOffice.Name = "rbOffice";
            this.rbOffice.Size = new System.Drawing.Size(99, 17);
            this.rbOffice.TabIndex = 11;
            this.rbOffice.TabStop = true;
            this.rbOffice.Text = "file Word, Excel";
            this.rbOffice.UseVisualStyleBackColor = true;
            this.rbOffice.CheckedChanged += new System.EventHandler(this.rbOffice_CheckedChanged);
            // 
            // rbPdf
            // 
            this.rbPdf.AutoSize = true;
            this.rbPdf.Location = new System.Drawing.Point(117, 12);
            this.rbPdf.Name = "rbPdf";
            this.rbPdf.Size = new System.Drawing.Size(62, 17);
            this.rbPdf.TabIndex = 12;
            this.rbPdf.Text = "file PDF";
            this.rbPdf.UseVisualStyleBackColor = true;
            this.rbPdf.CheckedChanged += new System.EventHandler(this.rbPdf_CheckedChanged);
            // 
            // rbString
            // 
            this.rbString.AutoSize = true;
            this.rbString.Location = new System.Drawing.Point(255, 13);
            this.rbString.Name = "rbString";
            this.rbString.Size = new System.Drawing.Size(52, 17);
            this.rbString.TabIndex = 13;
            this.rbString.TabStop = true;
            this.rbString.Text = "String";
            this.rbString.UseVisualStyleBackColor = true;
            this.rbString.CheckedChanged += new System.EventHandler(this.rbString_CheckedChanged);
            // 
            // btnSelCert
            // 
            this.btnSelCert.Location = new System.Drawing.Point(175, 116);
            this.btnSelCert.Name = "btnSelCert";
            this.btnSelCert.Size = new System.Drawing.Size(75, 23);
            this.btnSelCert.TabIndex = 14;
            this.btnSelCert.Text = "Select Cert";
            this.btnSelCert.UseVisualStyleBackColor = true;
            this.btnSelCert.Click += new System.EventHandler(this.btnSelCert_Click);
            // 
            // btnGetHash
            // 
            this.btnGetHash.Location = new System.Drawing.Point(256, 117);
            this.btnGetHash.Name = "btnGetHash";
            this.btnGetHash.Size = new System.Drawing.Size(75, 23);
            this.btnGetHash.TabIndex = 15;
            this.btnGetHash.Text = "Get Hash";
            this.btnGetHash.UseVisualStyleBackColor = true;
            this.btnGetHash.Click += new System.EventHandler(this.btnGetHash_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 238);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(411, 110);
            this.textBox1.TabIndex = 16;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 209);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "asd";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Slot Serial";
            // 
            // txtSlotSerial
            // 
            this.txtSlotSerial.Location = new System.Drawing.Point(73, 163);
            this.txtSlotSerial.Name = "txtSlotSerial";
            this.txtSlotSerial.Size = new System.Drawing.Size(95, 20);
            this.txtSlotSerial.TabIndex = 19;
            // 
            // txtSlotPIN
            // 
            this.txtSlotPIN.Location = new System.Drawing.Point(256, 166);
            this.txtSlotPIN.Name = "txtSlotPIN";
            this.txtSlotPIN.Size = new System.Drawing.Size(95, 20);
            this.txtSlotPIN.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(176, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Slot Password";
            // 
            // txtKeyLabel
            // 
            this.txtKeyLabel.Location = new System.Drawing.Point(407, 166);
            this.txtKeyLabel.Name = "txtKeyLabel";
            this.txtKeyLabel.Size = new System.Drawing.Size(95, 20);
            this.txtKeyLabel.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(357, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Prv Key";
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(352, 117);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 24;
            this.btnEncrypt.Text = "Mã hóa";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(433, 117);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 23);
            this.btnDecrypt.TabIndex = 25;
            this.btnDecrypt.Text = "Giải mã";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // Sign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 379);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.txtKeyLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSlotPIN);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSlotSerial);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnGetHash);
            this.Controls.Add(this.btnSelCert);
            this.Controls.Add(this.rbString);
            this.Controls.Add(this.rbPdf);
            this.Controls.Add(this.rbOffice);
            this.Controls.Add(this.rbXml);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.btnFileCRT);
            this.Controls.Add(this.txtSeri);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSign);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.txtPath);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Sign";
            this.Text = "Sign file";
            this.Load += new System.EventHandler(this.Sign_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSeri;
        private System.Windows.Forms.Button btnFileCRT;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.RadioButton rbXml;
        private System.Windows.Forms.RadioButton rbOffice;
        private System.Windows.Forms.RadioButton rbPdf;
        private System.Windows.Forms.RadioButton rbString;
        private System.Windows.Forms.Button btnSelCert;
        private System.Windows.Forms.Button btnGetHash;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSlotSerial;
        private System.Windows.Forms.TextBox txtSlotPIN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtKeyLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;

    }
}

