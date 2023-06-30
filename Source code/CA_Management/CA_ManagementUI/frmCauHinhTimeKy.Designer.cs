namespace ES.CA_ManagementUI
{
    partial class frmCauHinhTimeKy
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
            this.lblPathSave = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.nudNext = new System.Windows.Forms.NumericUpDown();
            this.nudSave = new System.Windows.Forms.NumericUpDown();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSave)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPathSave
            // 
            this.lblPathSave.AutoSize = true;
            this.lblPathSave.Location = new System.Drawing.Point(13, 9);
            this.lblPathSave.Name = "lblPathSave";
            this.lblPathSave.Size = new System.Drawing.Size(151, 13);
            this.lblPathSave.TabIndex = 8;
            this.lblPathSave.Text = "Thời gian chờ lần ký tiếp (giây)";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Location = new System.Drawing.Point(153, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(64, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Hủy";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(83, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnExit);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 51);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(300, 40);
            this.pnlFooter.TabIndex = 13;
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Location = new System.Drawing.Point(12, 32);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(152, 13);
            this.lblPeriod.TabIndex = 9;
            this.lblPeriod.Text = "Thời gian chờ lưu chữ ký (giây)";
            // 
            // nudNext
            // 
            this.nudNext.Location = new System.Drawing.Point(169, 7);
            this.nudNext.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudNext.Name = "nudNext";
            this.nudNext.Size = new System.Drawing.Size(120, 20);
            this.nudNext.TabIndex = 14;
            // 
            // nudSave
            // 
            this.nudSave.Location = new System.Drawing.Point(169, 30);
            this.nudSave.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudSave.Name = "nudSave";
            this.nudSave.Size = new System.Drawing.Size(120, 20);
            this.nudSave.TabIndex = 14;
            // 
            // frmCauHinhTimeKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 91);
            this.Controls.Add(this.nudSave);
            this.Controls.Add(this.nudNext);
            this.Controls.Add(this.lblPathSave);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.lblPeriod);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmCauHinhTimeKy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình thời gian ký";
            this.Load += new System.EventHandler(this.frmCauHinhTimeKy_Load);
            this.pnlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSave)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPathSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.NumericUpDown nudNext;
        private System.Windows.Forms.NumericUpDown nudSave;
    }
}