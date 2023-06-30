namespace ESLogin
{
    partial class UpdateLicense
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateLicense));
            this.btnCheck = new System.Windows.Forms.Button();
            this.GBInfoLic = new System.Windows.Forms.GroupBox();
            this.lbInfoLicense = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGet = new System.Windows.Forms.Button();
            this.lbInfo = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.timerLoading = new System.Windows.Forms.Timer(this.components);
            this.GBInfoLic.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCheck
            // 
            this.btnCheck.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.Location = new System.Drawing.Point(164, 64);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(120, 30);
            this.btnCheck.TabIndex = 0;
            this.btnCheck.Text = "Check License";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // GBInfoLic
            // 
            this.GBInfoLic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBInfoLic.BackColor = System.Drawing.Color.Transparent;
            this.GBInfoLic.Controls.Add(this.lbInfoLicense);
            this.GBInfoLic.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBInfoLic.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.GBInfoLic.Location = new System.Drawing.Point(12, 100);
            this.GBInfoLic.Name = "GBInfoLic";
            this.GBInfoLic.Size = new System.Drawing.Size(424, 52);
            this.GBInfoLic.TabIndex = 2;
            this.GBInfoLic.TabStop = false;
            this.GBInfoLic.Text = "Thông tin license";
            // 
            // lbInfoLicense
            // 
            this.lbInfoLicense.AutoSize = true;
            this.lbInfoLicense.Location = new System.Drawing.Point(17, 26);
            this.lbInfoLicense.Name = "lbInfoLicense";
            this.lbInfoLicense.Size = new System.Drawing.Size(45, 19);
            this.lbInfoLicense.TabIndex = 1;
            this.lbInfoLicense.Text = "label2";
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(290, 64);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(120, 30);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Visible = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnGet);
            this.panel1.Controls.Add(this.lbInfo);
            this.panel1.Controls.Add(this.btnImport);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 161);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(448, 158);
            this.panel1.TabIndex = 12;
            this.panel1.Visible = false;
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(99, 116);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(120, 30);
            this.btnGet.TabIndex = 10;
            this.btnGet.Text = "Get config.xml";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbInfo.Location = new System.Drawing.Point(9, 8);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(434, 95);
            this.lbInfo.TabIndex = 9;
            this.lbInfo.Text = resources.GetString("lbInfo.Text");
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(229, 116);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(120, 30);
            this.btnImport.TabIndex = 7;
            this.btnImport.Text = "Import license";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // timerLoading
            // 
            this.timerLoading.Tick += new System.EventHandler(this.timerLoading_Tick);
            // 
            // UpdateLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(448, 319);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.GBInfoLic);
            this.Controls.Add(this.btnCheck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kiểm tra license";
            this.Load += new System.EventHandler(this.UpdateLicense_Load);
            this.GBInfoLic.ResumeLayout(false);
            this.GBInfoLic.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.GroupBox GBInfoLic;
        private System.Windows.Forms.Label lbInfoLicense;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Timer timerLoading;
    }
}