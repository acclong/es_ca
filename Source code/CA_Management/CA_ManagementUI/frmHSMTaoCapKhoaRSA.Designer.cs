namespace ES.CA_ManagementUI
{
    partial class frmHSMTaoCapKhoaRSA
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
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboKeyType = new System.Windows.Forms.ComboBox();
            this.txtPKU = new System.Windows.Forms.TextBox();
            this.txtPKI = new System.Windows.Forms.TextBox();
            this.txtRequest = new System.Windows.Forms.TextBox();
            this.cboSlotID = new System.Windows.Forms.ComboBox();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnExit);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 167);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(376, 40);
            this.pnlFooter.TabIndex = 5;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(192, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Đóng";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(109, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Đồng ý";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Slot ID";
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(144, 66);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(220, 20);
            this.txtSubject.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Key type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Private key label *";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Subject *";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Certificate Request label *";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Public key label *";
            // 
            // cboKeyType
            // 
            this.cboKeyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKeyType.Enabled = false;
            this.cboKeyType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboKeyType.FormattingEnabled = true;
            this.cboKeyType.Location = new System.Drawing.Point(144, 39);
            this.cboKeyType.Name = "cboKeyType";
            this.cboKeyType.Size = new System.Drawing.Size(220, 21);
            this.cboKeyType.TabIndex = 1;
            // 
            // txtPKU
            // 
            this.txtPKU.Location = new System.Drawing.Point(144, 92);
            this.txtPKU.Name = "txtPKU";
            this.txtPKU.Size = new System.Drawing.Size(220, 20);
            this.txtPKU.TabIndex = 3;
            // 
            // txtPKI
            // 
            this.txtPKI.Location = new System.Drawing.Point(144, 118);
            this.txtPKI.Name = "txtPKI";
            this.txtPKI.Size = new System.Drawing.Size(220, 20);
            this.txtPKI.TabIndex = 4;
            // 
            // txtRequest
            // 
            this.txtRequest.Location = new System.Drawing.Point(144, 144);
            this.txtRequest.Name = "txtRequest";
            this.txtRequest.Size = new System.Drawing.Size(220, 20);
            this.txtRequest.TabIndex = 5;
            // 
            // cboSlotID
            // 
            this.cboSlotID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSlotID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboSlotID.FormattingEnabled = true;
            this.cboSlotID.Location = new System.Drawing.Point(144, 12);
            this.cboSlotID.Name = "cboSlotID";
            this.cboSlotID.Size = new System.Drawing.Size(139, 21);
            this.cboSlotID.TabIndex = 0;
            // 
            // frmTaoCapKhoaRSA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 207);
            this.Controls.Add(this.txtRequest);
            this.Controls.Add(this.txtPKI);
            this.Controls.Add(this.txtPKU);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.cboKeyType);
            this.Controls.Add(this.cboSlotID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmTaoCapKhoaRSA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo cặp khóa RSA";
            this.Load += new System.EventHandler(this.frmTaoCapKhoaRSA_Load);
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboKeyType;
        private System.Windows.Forms.TextBox txtPKU;
        private System.Windows.Forms.TextBox txtPKI;
        private System.Windows.Forms.TextBox txtRequest;
        private System.Windows.Forms.ComboBox cboSlotID;
    }
}