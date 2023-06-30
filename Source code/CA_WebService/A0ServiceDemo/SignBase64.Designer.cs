namespace A0ServiceDemo
{
    partial class SignBase64
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
            this.rbFileLocal = new System.Windows.Forms.RadioButton();
            this.rbFileServer = new System.Windows.Forms.RadioButton();
            this.btnVerify = new System.Windows.Forms.Button();
            this.txtSeri = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSign = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbFileLocal
            // 
            this.rbFileLocal.AutoSize = true;
            this.rbFileLocal.Enabled = false;
            this.rbFileLocal.Location = new System.Drawing.Point(106, 12);
            this.rbFileLocal.Name = "rbFileLocal";
            this.rbFileLocal.Size = new System.Drawing.Size(78, 17);
            this.rbFileLocal.TabIndex = 23;
            this.rbFileLocal.Text = "File at local";
            this.rbFileLocal.UseVisualStyleBackColor = true;
            // 
            // rbFileServer
            // 
            this.rbFileServer.AutoSize = true;
            this.rbFileServer.Checked = true;
            this.rbFileServer.Location = new System.Drawing.Point(12, 12);
            this.rbFileServer.Name = "rbFileServer";
            this.rbFileServer.Size = new System.Drawing.Size(85, 17);
            this.rbFileServer.TabIndex = 22;
            this.rbFileServer.TabStop = true;
            this.rbFileServer.Text = "File at server";
            this.rbFileServer.UseVisualStyleBackColor = true;
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(93, 117);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(75, 23);
            this.btnVerify.TabIndex = 20;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // txtSeri
            // 
            this.txtSeri.Location = new System.Drawing.Point(12, 52);
            this.txtSeri.Name = "txtSeri";
            this.txtSeri.Size = new System.Drawing.Size(302, 20);
            this.txtSeri.TabIndex = 18;
            this.txtSeri.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSeri_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Đường dẫn file";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Serial Number of Certificate";
            // 
            // btnSign
            // 
            this.btnSign.Location = new System.Drawing.Point(12, 117);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(75, 23);
            this.btnSign.TabIndex = 15;
            this.btnSign.Text = "Ký file";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(321, 91);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(29, 20);
            this.btnFind.TabIndex = 14;
            this.btnFind.Text = "...";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(12, 91);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(302, 20);
            this.txtPath.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(239, 117);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SignBase64
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 148);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rbFileLocal);
            this.Controls.Add(this.rbFileServer);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.txtSeri);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSign);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.txtPath);
            this.Name = "SignBase64";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SignBase64";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbFileLocal;
        private System.Windows.Forms.RadioButton rbFileServer;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.TextBox txtSeri;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button button1;
    }
}

