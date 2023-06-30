namespace A0ServiceDemo
{
    partial class SignByLink
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
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtDect = new System.Windows.Forms.TextBox();
            this.btnSign = new System.Windows.Forms.Button();
            this.lblSource = new System.Windows.Forms.Label();
            this.lblDest = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lblIDProg = new System.Windows.Forms.Label();
            this.lblUserProg = new System.Windows.Forms.Label();
            this.txtIDProg = new System.Windows.Forms.TextBox();
            this.txtUserProg = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(107, 12);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(231, 22);
            this.txtSource.TabIndex = 0;
            this.txtSource.Text = "~/FileBeforeSigned/Báo cáo thực tập hè.docx";
            // 
            // txtDect
            // 
            this.txtDect.Location = new System.Drawing.Point(107, 40);
            this.txtDect.Name = "txtDect";
            this.txtDect.Size = new System.Drawing.Size(231, 22);
            this.txtDect.TabIndex = 1;
            this.txtDect.Text = "~/FileAfterSigned/Báo cáo thực tập hè.docx";
            // 
            // btnSign
            // 
            this.btnSign.Location = new System.Drawing.Point(251, 123);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(87, 27);
            this.btnSign.TabIndex = 2;
            this.btnSign.Text = "Sign";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(13, 15);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(64, 15);
            this.lblSource.TabIndex = 4;
            this.lblSource.Text = "File cần ký";
            // 
            // lblDest
            // 
            this.lblDest.AutoSize = true;
            this.lblDest.Location = new System.Drawing.Point(13, 43);
            this.lblDest.Name = "lblDest";
            this.lblDest.Size = new System.Drawing.Size(86, 15);
            this.lblDest.TabIndex = 5;
            this.lblDest.Text = "Thư mục đã ký";
            // 
            // lblIDProg
            // 
            this.lblIDProg.AutoSize = true;
            this.lblIDProg.Location = new System.Drawing.Point(13, 98);
            this.lblIDProg.Name = "lblIDProg";
            this.lblIDProg.Size = new System.Drawing.Size(43, 15);
            this.lblIDProg.TabIndex = 9;
            this.lblIDProg.Text = "IdProg";
            // 
            // lblUserProg
            // 
            this.lblUserProg.AutoSize = true;
            this.lblUserProg.Location = new System.Drawing.Point(13, 70);
            this.lblUserProg.Name = "lblUserProg";
            this.lblUserProg.Size = new System.Drawing.Size(57, 15);
            this.lblUserProg.TabIndex = 8;
            this.lblUserProg.Text = "UserProg";
            // 
            // txtIDProg
            // 
            this.txtIDProg.Location = new System.Drawing.Point(107, 95);
            this.txtIDProg.Name = "txtIDProg";
            this.txtIDProg.Size = new System.Drawing.Size(231, 22);
            this.txtIDProg.TabIndex = 7;
            this.txtIDProg.Text = "1";
            // 
            // txtUserProg
            // 
            this.txtUserProg.Location = new System.Drawing.Point(107, 67);
            this.txtUserProg.Name = "txtUserProg";
            this.txtUserProg.Size = new System.Drawing.Size(231, 22);
            this.txtUserProg.TabIndex = 6;
            this.txtUserProg.Text = "ninhtq";
            // 
            // SignByLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 162);
            this.Controls.Add(this.lblIDProg);
            this.Controls.Add(this.lblUserProg);
            this.Controls.Add(this.txtIDProg);
            this.Controls.Add(this.txtUserProg);
            this.Controls.Add(this.lblDest);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.btnSign);
            this.Controls.Add(this.txtDect);
            this.Controls.Add(this.txtSource);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SignByLink";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "S‌ignByLink";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtDect;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.Label lblDest;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label lblIDProg;
        private System.Windows.Forms.Label lblUserProg;
        private System.Windows.Forms.TextBox txtIDProg;
        private System.Windows.Forms.TextBox txtUserProg;
    }
}