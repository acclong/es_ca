namespace ES.CA_ManagementUI
{
    partial class ucLienKetUserDonVi
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.rgvUserDonVi = new Telerik.WinControls.UI.RadGridView();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgvUserDonVi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvUserDonVi.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(975, 40);
            this.lblHeader.TabIndex = 18;
            this.lblHeader.Text = "LIÊN KẾT NGƯỜI DÙNG - ĐƠN VỊ";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnEdit);
            this.pnlFooter.Controls.Add(this.btnAdd);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 488);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(975, 40);
            this.pnlFooter.TabIndex = 19;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(890, 9);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(809, 9);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // rgvUserDonVi
            // 
            this.rgvUserDonVi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rgvUserDonVi.Location = new System.Drawing.Point(0, 40);
            // 
            // rgvUserDonVi
            // 
            this.rgvUserDonVi.MasterTemplate.AllowAddNewRow = false;
            this.rgvUserDonVi.MasterTemplate.AllowColumnReorder = false;
            this.rgvUserDonVi.MasterTemplate.AllowDeleteRow = false;
            this.rgvUserDonVi.MasterTemplate.AllowDragToGroup = false;
            this.rgvUserDonVi.MasterTemplate.AllowEditRow = false;
            this.rgvUserDonVi.MasterTemplate.AllowRowResize = false;
            this.rgvUserDonVi.Name = "rgvUserDonVi";
            this.rgvUserDonVi.ShowGroupPanel = false;
            this.rgvUserDonVi.Size = new System.Drawing.Size(975, 448);
            this.rgvUserDonVi.TabIndex = 20;
            this.rgvUserDonVi.Text = "radGridView1";
            this.rgvUserDonVi.GroupByChanged += new Telerik.WinControls.UI.GridViewCollectionChangedEventHandler(this.rgvUserDonVi_GroupByChanged);
            this.rgvUserDonVi.DoubleClick += new System.EventHandler(this.rgvUserDonVi_DoubleClick);
            // 
            // ucLienKetUserDonVi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rgvUserDonVi);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.pnlFooter);
            this.Name = "ucLienKetUserDonVi";
            this.Size = new System.Drawing.Size(975, 528);
            this.Load += new System.EventHandler(this.ucLienKetUserDonVi_Load);
            this.pnlFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgvUserDonVi.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvUserDonVi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private Telerik.WinControls.UI.RadGridView rgvUserDonVi;
    }
}
