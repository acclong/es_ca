namespace ES.CA_ManagementUI
{
    partial class ucHSMQuanlyWLDSlot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucHSMQuanlyWLDSlot));
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelSlot = new System.Windows.Forms.Button();
            this.grbWLDSlot = new System.Windows.Forms.GroupBox();
            this.cfgWLDSlot = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.grbWLDObject = new System.Windows.Forms.GroupBox();
            this.cfgWLDObject = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlFooter.SuspendLayout();
            this.grbWLDSlot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgWLDSlot)).BeginInit();
            this.grbWLDObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgWLDObject)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(797, 40);
            this.lblHeader.TabIndex = 14;
            this.lblHeader.Text = "QUẢN LÝ WLD SLOT";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnUpdate);
            this.pnlFooter.Controls.Add(this.btnDelSlot);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 490);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(797, 40);
            this.pnlFooter.TabIndex = 15;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(571, 8);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(122, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelSlot
            // 
            this.btnDelSlot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelSlot.Location = new System.Drawing.Point(699, 8);
            this.btnDelSlot.Name = "btnDelSlot";
            this.btnDelSlot.Size = new System.Drawing.Size(94, 23);
            this.btnDelSlot.TabIndex = 3;
            this.btnDelSlot.Text = "Xóa Slot";
            this.btnDelSlot.UseVisualStyleBackColor = true;
            this.btnDelSlot.Click += new System.EventHandler(this.btnDelSlot_Click);
            // 
            // grbWLDSlot
            // 
            this.grbWLDSlot.Controls.Add(this.cfgWLDSlot);
            this.grbWLDSlot.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbWLDSlot.Location = new System.Drawing.Point(0, 40);
            this.grbWLDSlot.Name = "grbWLDSlot";
            this.grbWLDSlot.Size = new System.Drawing.Size(797, 382);
            this.grbWLDSlot.TabIndex = 16;
            this.grbWLDSlot.TabStop = false;
            this.grbWLDSlot.Text = "WLD Slot";
            // 
            // cfgWLDSlot
            // 
            this.cfgWLDSlot.AllowEditing = false;
            this.cfgWLDSlot.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgWLDSlot.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgWLDSlot.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgWLDSlot.ColumnInfo = "10,1,0,0,0,95,Columns:";
            this.cfgWLDSlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgWLDSlot.ExtendLastCol = true;
            this.cfgWLDSlot.Location = new System.Drawing.Point(3, 16);
            this.cfgWLDSlot.Name = "cfgWLDSlot";
            this.cfgWLDSlot.Rows.DefaultSize = 19;
            this.cfgWLDSlot.ShowCursor = true;
            this.cfgWLDSlot.Size = new System.Drawing.Size(791, 363);
            this.cfgWLDSlot.StyleInfo = resources.GetString("cfgWLDSlot.StyleInfo");
            this.cfgWLDSlot.TabIndex = 21;
            this.cfgWLDSlot.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            this.cfgWLDSlot.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.cfgWLDSlot_AfterEdit);
            // 
            // grbWLDObject
            // 
            this.grbWLDObject.Controls.Add(this.cfgWLDObject);
            this.grbWLDObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbWLDObject.Location = new System.Drawing.Point(0, 422);
            this.grbWLDObject.Name = "grbWLDObject";
            this.grbWLDObject.Size = new System.Drawing.Size(797, 68);
            this.grbWLDObject.TabIndex = 17;
            this.grbWLDObject.TabStop = false;
            this.grbWLDObject.Text = "WLD Object";
            // 
            // cfgWLDObject
            // 
            this.cfgWLDObject.AllowEditing = false;
            this.cfgWLDObject.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgWLDObject.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgWLDObject.AutoGenerateColumns = false;
            this.cfgWLDObject.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgWLDObject.ColumnInfo = "14,1,0,0,0,95,Columns:";
            this.cfgWLDObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgWLDObject.ExtendLastCol = true;
            this.cfgWLDObject.Location = new System.Drawing.Point(3, 16);
            this.cfgWLDObject.Name = "cfgWLDObject";
            this.cfgWLDObject.Rows.DefaultSize = 19;
            this.cfgWLDObject.ShowCursor = true;
            this.cfgWLDObject.Size = new System.Drawing.Size(791, 49);
            this.cfgWLDObject.StyleInfo = resources.GetString("cfgWLDObject.StyleInfo");
            this.cfgWLDObject.TabIndex = 21;
            this.cfgWLDObject.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            // 
            // ucHSMQuanlyWLDSlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbWLDObject);
            this.Controls.Add(this.grbWLDSlot);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.pnlFooter);
            this.Name = "ucHSMQuanlyWLDSlot";
            this.Size = new System.Drawing.Size(797, 530);
            this.Load += new System.EventHandler(this.ucHSMQuanlyWLDSlot_Load);
            this.pnlFooter.ResumeLayout(false);
            this.grbWLDSlot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfgWLDSlot)).EndInit();
            this.grbWLDObject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfgWLDObject)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnDelSlot;
        private System.Windows.Forms.GroupBox grbWLDSlot;
        private System.Windows.Forms.GroupBox grbWLDObject;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgWLDSlot;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgWLDObject;
        private System.Windows.Forms.Button btnUpdate;
    }
}
