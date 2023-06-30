namespace AppUpdate
{
    partial class frmUpdate
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
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.lblShow = new System.Windows.Forms.Label();
            this.prgUpdate = new System.Windows.Forms.ProgressBar();
            this.chkContinue = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // lblShow
            // 
            this.lblShow.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShow.Location = new System.Drawing.Point(13, 10);
            this.lblShow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblShow.Name = "lblShow";
            this.lblShow.Padding = new System.Windows.Forms.Padding(4);
            this.lblShow.Size = new System.Drawing.Size(401, 30);
            this.lblShow.TabIndex = 0;
            this.lblShow.Text = "Updating...";
            // 
            // prgUpdate
            // 
            this.prgUpdate.Location = new System.Drawing.Point(13, 43);
            this.prgUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.prgUpdate.Name = "prgUpdate";
            this.prgUpdate.Size = new System.Drawing.Size(401, 34);
            this.prgUpdate.TabIndex = 1;
            // 
            // chkContinue
            // 
            this.chkContinue.AutoSize = true;
            this.chkContinue.Checked = true;
            this.chkContinue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkContinue.Enabled = false;
            this.chkContinue.Location = new System.Drawing.Point(17, 84);
            this.chkContinue.Name = "chkContinue";
            this.chkContinue.Size = new System.Drawing.Size(183, 23);
            this.chkContinue.TabIndex = 2;
            this.chkContinue.Text = "Thực hiện tiếp tục việc ký";
            this.chkContinue.UseVisualStyleBackColor = true;
            // 
            // frmUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 115);
            this.ControlBox = false;
            this.Controls.Add(this.chkContinue);
            this.Controls.Add(this.prgUpdate);
            this.Controls.Add(this.lblShow);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmUpdate";
            this.Text = "Ứng dụng ký";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Label lblShow;
        private System.Windows.Forms.ProgressBar prgUpdate;
        private System.Windows.Forms.CheckBox chkContinue;
    }
}

