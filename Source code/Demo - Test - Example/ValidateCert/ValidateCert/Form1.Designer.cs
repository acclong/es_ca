namespace ValidateCert
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnFileCRT = new System.Windows.Forms.Button();
            this.txtSeri = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Validate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnFileCRT
            // 
            this.btnFileCRT.Location = new System.Drawing.Point(320, 25);
            this.btnFileCRT.Name = "btnFileCRT";
            this.btnFileCRT.Size = new System.Drawing.Size(29, 20);
            this.btnFileCRT.TabIndex = 11;
            this.btnFileCRT.Text = "...";
            this.btnFileCRT.UseVisualStyleBackColor = true;
            this.btnFileCRT.Click += new System.EventHandler(this.btnFileCRT_Click);
            // 
            // txtSeri
            // 
            this.txtSeri.Location = new System.Drawing.Point(11, 25);
            this.txtSeri.Name = "txtSeri";
            this.txtSeri.Size = new System.Drawing.Size(302, 20);
            this.txtSeri.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Seri Number Usb Token/File CRT";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 81);
            this.Controls.Add(this.btnFileCRT);
            this.Controls.Add(this.txtSeri);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Validate Certificate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnFileCRT;
        private System.Windows.Forms.TextBox txtSeri;
        private System.Windows.Forms.Label label1;
    }
}

