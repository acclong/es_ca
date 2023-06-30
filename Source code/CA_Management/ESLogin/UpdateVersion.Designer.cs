namespace ESLogin
{
    partial class UpdateVersion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateVersion));
            this.progressBarUpdate = new System.Windows.Forms.ProgressBar();
            this.backgroundWorkerDownload = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.lbdownPrecent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBarUpdate
            // 
            this.progressBarUpdate.Location = new System.Drawing.Point(32, 101);
            this.progressBarUpdate.Name = "progressBarUpdate";
            this.progressBarUpdate.Size = new System.Drawing.Size(323, 23);
            this.progressBarUpdate.TabIndex = 1;
            // 
            // backgroundWorkerDownload
            // 
            this.backgroundWorkerDownload.WorkerReportsProgress = true;
            this.backgroundWorkerDownload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDownload_DoWork);
            this.backgroundWorkerDownload.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerDownload_ProgressChanged);
            this.backgroundWorkerDownload.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerDownload_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(38, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Đang tải phiên bản cập nhật . . .";
            // 
            // lbdownPrecent
            // 
            this.lbdownPrecent.AutoSize = true;
            this.lbdownPrecent.BackColor = System.Drawing.Color.Transparent;
            this.lbdownPrecent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbdownPrecent.Location = new System.Drawing.Point(259, 85);
            this.lbdownPrecent.Name = "lbdownPrecent";
            this.lbdownPrecent.Size = new System.Drawing.Size(0, 13);
            this.lbdownPrecent.TabIndex = 3;
            // 
            // UpdateVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(375, 149);
            this.Controls.Add(this.lbdownPrecent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBarUpdate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateVersion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật phiên bản";
            this.Load += new System.EventHandler(this.UpdateVersion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarUpdate;
        private System.ComponentModel.BackgroundWorker backgroundWorkerDownload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbdownPrecent;
    }
}