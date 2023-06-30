namespace ES.CA_ManagementUI
{
    partial class ucHSMQuanLyObjects
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucHSMQuanLyObjects));
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnXem = new System.Windows.Forms.Button();
            this.cboSelectToken = new System.Windows.Forms.ComboBox();
            this.lblSelectToken = new System.Windows.Forms.Label();
            this.cfgObject = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlFooter.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgObject)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(657, 40);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "QUẢN LÝ ĐỐI TƯỢNG";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnUpdate);
            this.pnlFooter.Controls.Add(this.btnDelete);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 455);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(657, 40);
            this.pnlFooter.TabIndex = 3;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(473, 5);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 23);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(579, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.btnXem);
            this.pnlHeader.Controls.Add(this.cboSelectToken);
            this.pnlHeader.Controls.Add(this.lblSelectToken);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 40);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(657, 40);
            this.pnlHeader.TabIndex = 2;
            // 
            // btnXem
            // 
            this.btnXem.Location = new System.Drawing.Point(256, 7);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(75, 23);
            this.btnXem.TabIndex = 9;
            this.btnXem.Text = "Xem";
            this.btnXem.UseVisualStyleBackColor = true;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // cboSelectToken
            // 
            this.cboSelectToken.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSelectToken.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboSelectToken.FormattingEnabled = true;
            this.cboSelectToken.Location = new System.Drawing.Point(64, 9);
            this.cboSelectToken.Name = "cboSelectToken";
            this.cboSelectToken.Size = new System.Drawing.Size(185, 21);
            this.cboSelectToken.TabIndex = 7;
            // 
            // lblSelectToken
            // 
            this.lblSelectToken.AutoSize = true;
            this.lblSelectToken.Location = new System.Drawing.Point(3, 12);
            this.lblSelectToken.Name = "lblSelectToken";
            this.lblSelectToken.Size = new System.Drawing.Size(53, 13);
            this.lblSelectToken.TabIndex = 5;
            this.lblSelectToken.Text = "Chọn Slot";
            // 
            // cfgObject
            // 
            this.cfgObject.AllowEditing = false;
            this.cfgObject.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgObject.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgObject.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgObject.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.cfgObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgObject.ExtendLastCol = true;
            this.cfgObject.Location = new System.Drawing.Point(0, 80);
            this.cfgObject.Name = "cfgObject";
            this.cfgObject.Rows.DefaultSize = 19;
            this.cfgObject.ShowCursor = true;
            this.cfgObject.Size = new System.Drawing.Size(657, 375);
            this.cfgObject.StyleInfo = resources.GetString("cfgObject.StyleInfo");
            this.cfgObject.TabIndex = 20;
            this.cfgObject.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            this.cfgObject.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.cfgObject_AfterEdit);
            // 
            // ucHSMQuanLyObjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cfgObject);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.pnlFooter);
            this.Name = "ucHSMQuanLyObjects";
            this.Size = new System.Drawing.Size(657, 495);
            this.Load += new System.EventHandler(this.ucHSMQuanLyObjects_Load);
            this.pnlFooter.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgObject)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.ComboBox cboSelectToken;
        private System.Windows.Forms.Label lblSelectToken;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgObject;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnXem;

    }
}
