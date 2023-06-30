namespace ES.CA_ManagementUI
{
    partial class ucHSMQuanLySlots
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucHSMQuanLySlots));
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnXem = new System.Windows.Forms.Button();
            this.cboHSM = new System.Windows.Forms.ComboBox();
            this.lblSelectToken = new System.Windows.Forms.Label();
            this.cfgSlot = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlFooter.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgSlot)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(776, 40);
            this.lblHeader.TabIndex = 10;
            this.lblHeader.Text = "QUẢN LÝ SLOT";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnUpdate);
            this.pnlFooter.Controls.Add(this.btnDelete);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 453);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(776, 40);
            this.pnlFooter.TabIndex = 11;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(585, 6);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(107, 23);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(698, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.btnXem);
            this.pnlHeader.Controls.Add(this.cboHSM);
            this.pnlHeader.Controls.Add(this.lblSelectToken);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 40);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(776, 40);
            this.pnlHeader.TabIndex = 12;
            // 
            // btnXem
            // 
            this.btnXem.Location = new System.Drawing.Point(269, 8);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(75, 23);
            this.btnXem.TabIndex = 7;
            this.btnXem.Text = "Xem";
            this.btnXem.UseVisualStyleBackColor = true;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // cboHSM
            // 
            this.cboHSM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHSM.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboHSM.FormattingEnabled = true;
            this.cboHSM.Location = new System.Drawing.Point(78, 9);
            this.cboHSM.Name = "cboHSM";
            this.cboHSM.Size = new System.Drawing.Size(185, 21);
            this.cboHSM.TabIndex = 7;
            // 
            // lblSelectToken
            // 
            this.lblSelectToken.AutoSize = true;
            this.lblSelectToken.Location = new System.Drawing.Point(3, 12);
            this.lblSelectToken.Name = "lblSelectToken";
            this.lblSelectToken.Size = new System.Drawing.Size(69, 13);
            this.lblSelectToken.TabIndex = 5;
            this.lblSelectToken.Text = "Thiết bị HSM";
            // 
            // cfgSlot
            // 
            this.cfgSlot.AllowEditing = false;
            this.cfgSlot.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgSlot.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgSlot.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgSlot.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.cfgSlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgSlot.ExtendLastCol = true;
            this.cfgSlot.Location = new System.Drawing.Point(0, 80);
            this.cfgSlot.Name = "cfgSlot";
            this.cfgSlot.Rows.DefaultSize = 19;
            this.cfgSlot.ShowCursor = true;
            this.cfgSlot.Size = new System.Drawing.Size(776, 373);
            this.cfgSlot.StyleInfo = resources.GetString("cfgSlot.StyleInfo");
            this.cfgSlot.TabIndex = 19;
            this.cfgSlot.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            this.cfgSlot.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.cfgSlot_AfterEdit);
            // 
            // ucHSMQuanLySlots
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cfgSlot);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.pnlFooter);
            this.Name = "ucHSMQuanLySlots";
            this.Size = new System.Drawing.Size(776, 493);
            this.Load += new System.EventHandler(this.ucHSMQuanLySlots_Load);
            this.pnlFooter.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgSlot)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.ComboBox cboHSM;
        private System.Windows.Forms.Label lblSelectToken;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgSlot;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnXem;
    }
}
