namespace ES.CA_ManagementUI
{
    partial class frmHSMNhanBanSlot
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbbHSMNguon = new System.Windows.Forms.ComboBox();
            this.cbbSlotNguon = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbHSMDich = new System.Windows.Forms.ComboBox();
            this.cbbSlotDich = new System.Windows.Forms.ComboBox();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnReplication = new System.Windows.Forms.Button();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Slot nguồn";
            // 
            // cbbHSMNguon
            // 
            this.cbbHSMNguon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbHSMNguon.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbHSMNguon.FormattingEnabled = true;
            this.cbbHSMNguon.Location = new System.Drawing.Point(83, 10);
            this.cbbHSMNguon.Name = "cbbHSMNguon";
            this.cbbHSMNguon.Size = new System.Drawing.Size(180, 21);
            this.cbbHSMNguon.TabIndex = 1;
            // 
            // cbbSlotNguon
            // 
            this.cbbSlotNguon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSlotNguon.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbSlotNguon.FormattingEnabled = true;
            this.cbbSlotNguon.Location = new System.Drawing.Point(270, 10);
            this.cbbSlotNguon.Name = "cbbSlotNguon";
            this.cbbSlotNguon.Size = new System.Drawing.Size(148, 21);
            this.cbbSlotNguon.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Slot đích";
            // 
            // cbbHSMDich
            // 
            this.cbbHSMDich.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbHSMDich.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbHSMDich.FormattingEnabled = true;
            this.cbbHSMDich.Location = new System.Drawing.Point(83, 41);
            this.cbbHSMDich.Name = "cbbHSMDich";
            this.cbbHSMDich.Size = new System.Drawing.Size(180, 21);
            this.cbbHSMDich.TabIndex = 1;
            // 
            // cbbSlotDich
            // 
            this.cbbSlotDich.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSlotDich.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbSlotDich.FormattingEnabled = true;
            this.cbbSlotDich.Location = new System.Drawing.Point(270, 41);
            this.cbbSlotDich.Name = "cbbSlotDich";
            this.cbbSlotDich.Size = new System.Drawing.Size(148, 21);
            this.cbbSlotDich.TabIndex = 1;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnExit);
            this.pnlFooter.Controls.Add(this.btnReplication);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 67);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(430, 40);
            this.pnlFooter.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(219, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Hủy";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReplication
            // 
            this.btnReplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReplication.Location = new System.Drawing.Point(136, 9);
            this.btnReplication.Name = "btnReplication";
            this.btnReplication.Size = new System.Drawing.Size(75, 23);
            this.btnReplication.TabIndex = 2;
            this.btnReplication.Text = "Đồng ý";
            this.btnReplication.UseVisualStyleBackColor = true;
            this.btnReplication.Click += new System.EventHandler(this.btnReplication_Click);
            // 
            // frmNhanBanSlotNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 107);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.cbbSlotDich);
            this.Controls.Add(this.cbbSlotNguon);
            this.Controls.Add(this.cbbHSMDich);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbbHSMNguon);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmNhanBanSlotNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhân bản HSM Slot";
            this.Load += new System.EventHandler(this.frmNhanBanSlotNew_Load);
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbHSMNguon;
        private System.Windows.Forms.ComboBox cbbSlotNguon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbHSMDich;
        private System.Windows.Forms.ComboBox cbbSlotDich;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnReplication;
    }
}