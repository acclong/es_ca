namespace A0ServiceDemo
{
    partial class SignArrayFile
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
            this.lblIDProg = new System.Windows.Forms.Label();
            this.lblUserProg = new System.Windows.Forms.Label();
            this.txtIDProg = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.txtUserProg = new System.Windows.Forms.TextBox();
            this.lblDest = new System.Windows.Forms.Label();
            this.lblSource = new System.Windows.Forms.Label();
            this.btnSign = new System.Windows.Forms.Button();
            this.txtDect = new System.Windows.Forms.TextBox();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblIDProg
            // 
            this.lblIDProg.AutoSize = true;
            this.lblIDProg.Location = new System.Drawing.Point(12, 262);
            this.lblIDProg.Name = "lblIDProg";
            this.lblIDProg.Size = new System.Drawing.Size(43, 15);
            this.lblIDProg.TabIndex = 18;
            this.lblIDProg.Text = "IdProg";
            // 
            // lblUserProg
            // 
            this.lblUserProg.AutoSize = true;
            this.lblUserProg.Location = new System.Drawing.Point(12, 234);
            this.lblUserProg.Name = "lblUserProg";
            this.lblUserProg.Size = new System.Drawing.Size(57, 15);
            this.lblUserProg.TabIndex = 17;
            this.lblUserProg.Text = "UserProg";
            // 
            // txtIDProg
            // 
            this.txtIDProg.Location = new System.Drawing.Point(106, 259);
            this.txtIDProg.Name = "txtIDProg";
            this.txtIDProg.Size = new System.Drawing.Size(231, 22);
            this.txtIDProg.TabIndex = 16;
            this.txtIDProg.Text = "1";
            // 
            // txtUserProg
            // 
            this.txtUserProg.Location = new System.Drawing.Point(106, 231);
            this.txtUserProg.Name = "txtUserProg";
            this.txtUserProg.Size = new System.Drawing.Size(231, 22);
            this.txtUserProg.TabIndex = 15;
            this.txtUserProg.Text = "ninhtq";
            // 
            // lblDest
            // 
            this.lblDest.AutoSize = true;
            this.lblDest.Location = new System.Drawing.Point(3, 9);
            this.lblDest.Name = "lblDest";
            this.lblDest.Size = new System.Drawing.Size(86, 15);
            this.lblDest.TabIndex = 14;
            this.lblDest.Text = "Thư mục đã ký";
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(12, 9);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(64, 15);
            this.lblSource.TabIndex = 13;
            this.lblSource.Text = "File cần ký";
            // 
            // btnSign
            // 
            this.btnSign.Location = new System.Drawing.Point(250, 287);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(87, 27);
            this.btnSign.TabIndex = 12;
            this.btnSign.Text = "Sign";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // txtDect
            // 
            this.txtDect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtDect.Location = new System.Drawing.Point(0, 27);
            this.txtDect.Multiline = true;
            this.txtDect.Name = "txtDect";
            this.txtDect.Size = new System.Drawing.Size(172, 198);
            this.txtDect.TabIndex = 11;
            // 
            // txtSource
            // 
            this.txtSource.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtSource.Location = new System.Drawing.Point(0, 27);
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(176, 198);
            this.txtSource.TabIndex = 10;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblSource);
            this.splitContainer1.Panel1.Controls.Add(this.txtSource);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblDest);
            this.splitContainer1.Panel2.Controls.Add(this.txtDect);
            this.splitContainer1.Size = new System.Drawing.Size(352, 225);
            this.splitContainer1.SplitterDistance = 176;
            this.splitContainer1.TabIndex = 19;
            // 
            // SignArrayFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 321);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lblIDProg);
            this.Controls.Add(this.lblUserProg);
            this.Controls.Add(this.txtIDProg);
            this.Controls.Add(this.txtUserProg);
            this.Controls.Add(this.btnSign);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SignArrayFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SignArrayFile";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIDProg;
        private System.Windows.Forms.Label lblUserProg;
        private System.Windows.Forms.TextBox txtIDProg;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox txtUserProg;
        private System.Windows.Forms.Label lblDest;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.TextBox txtDect;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}