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
            this.chkOffice = new System.Windows.Forms.CheckBox();
            this.chkPdf = new System.Windows.Forms.CheckBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnSign = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSeri = new System.Windows.Forms.TextBox();
            this.btnFileCRT = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkOffice
            // 
            this.chkOffice.AutoSize = true;
            this.chkOffice.Location = new System.Drawing.Point(12, 12);
            this.chkOffice.Name = "chkOffice";
            this.chkOffice.Size = new System.Drawing.Size(100, 17);
            this.chkOffice.TabIndex = 0;
            this.chkOffice.Text = "file Word, Excel";
            this.chkOffice.UseVisualStyleBackColor = true;
            this.chkOffice.CheckedChanged += new System.EventHandler(this.chkOffice_CheckedChanged);
            // 
            // chkPdf
            // 
            this.chkPdf.AutoSize = true;
            this.chkPdf.Location = new System.Drawing.Point(118, 12);
            this.chkPdf.Name = "chkPdf";
            this.chkPdf.Size = new System.Drawing.Size(63, 17);
            this.chkPdf.TabIndex = 1;
            this.chkPdf.Text = "file PDF";
            this.chkPdf.UseVisualStyleBackColor = true;
            this.chkPdf.CheckedChanged += new System.EventHandler(this.chkPdf_CheckedChanged);
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
            // Sign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 149);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.btnFileCRT);
            this.Controls.Add(this.txtSeri);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSign);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.chkPdf);
            this.Controls.Add(this.chkOffice);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Sign";
            this.Text = "Sign file";
            this.Load += new System.EventHandler(this.Sign_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkOffice;
        private System.Windows.Forms.CheckBox chkPdf;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSeri;
        private System.Windows.Forms.Button btnFileCRT;
        private System.Windows.Forms.Button btnVerify;

    }
}

