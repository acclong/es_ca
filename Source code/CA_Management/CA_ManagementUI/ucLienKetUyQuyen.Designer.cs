namespace ES.CA_ManagementUI
{
    partial class ucLienKetUyQuyen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucLienKetUyQuyen));
            this.lblHeader = new System.Windows.Forms.Label();
            this.panGrid = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cfgUyQuyen = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panTreeView = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cfgUnit = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cboNguoiDung = new System.Windows.Forms.ComboBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.panGrid.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgUyQuyen)).BeginInit();
            this.panTreeView.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgUnit)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(786, 40);
            this.lblHeader.TabIndex = 15;
            this.lblHeader.Text = "LIÊN KẾT ỦY QUYỀN";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panGrid
            // 
            this.panGrid.Controls.Add(this.groupBox1);
            this.panGrid.Dock = System.Windows.Forms.DockStyle.Left;
            this.panGrid.Location = new System.Drawing.Point(0, 0);
            this.panGrid.Name = "panGrid";
            this.panGrid.Size = new System.Drawing.Size(609, 375);
            this.panGrid.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cfgUyQuyen);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(609, 375);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách ủy quyền";
            // 
            // cfgUyQuyen
            // 
            this.cfgUyQuyen.AllowEditing = false;
            this.cfgUyQuyen.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgUyQuyen.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgUyQuyen.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgUyQuyen.ColumnInfo = resources.GetString("cfgUyQuyen.ColumnInfo");
            this.cfgUyQuyen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgUyQuyen.ExtendLastCol = true;
            this.cfgUyQuyen.Location = new System.Drawing.Point(3, 16);
            this.cfgUyQuyen.Name = "cfgUyQuyen";
            this.cfgUyQuyen.Rows.DefaultSize = 19;
            this.cfgUyQuyen.ShowCursor = true;
            this.cfgUyQuyen.Size = new System.Drawing.Size(603, 356);
            this.cfgUyQuyen.StyleInfo = resources.GetString("cfgUyQuyen.StyleInfo");
            this.cfgUyQuyen.TabIndex = 1;
            this.cfgUyQuyen.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            this.cfgUyQuyen.DoubleClick += new System.EventHandler(this.cfgUyQuyen_DoubleClick);
            // 
            // panTreeView
            // 
            this.panTreeView.Controls.Add(this.groupBox2);
            this.panTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panTreeView.Location = new System.Drawing.Point(614, 0);
            this.panTreeView.Name = "panTreeView";
            this.panTreeView.Size = new System.Drawing.Size(172, 375);
            this.panTreeView.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cfgUnit);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(172, 375);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách đơn vị được ủy quyền";
            // 
            // cfgUnit
            // 
            this.cfgUnit.AllowDelete = true;
            this.cfgUnit.AllowEditing = false;
            this.cfgUnit.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgUnit.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgUnit.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgUnit.ColumnInfo = resources.GetString("cfgUnit.ColumnInfo");
            this.cfgUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgUnit.ExtendLastCol = true;
            this.cfgUnit.Location = new System.Drawing.Point(3, 16);
            this.cfgUnit.Name = "cfgUnit";
            this.cfgUnit.Rows.DefaultSize = 19;
            this.cfgUnit.ShowCursor = true;
            this.cfgUnit.Size = new System.Drawing.Size(166, 356);
            this.cfgUnit.StyleInfo = resources.GetString("cfgUnit.StyleInfo");
            this.cfgUnit.TabIndex = 2;
            this.cfgUnit.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            this.cfgUnit.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.cfgUnit_AfterEdit);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panTreeView);
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.panGrid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(786, 375);
            this.panel2.TabIndex = 18;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(609, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 375);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(533, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Xem";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cboNguoiDung
            // 
            this.cboNguoiDung.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNguoiDung.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboNguoiDung.FormattingEnabled = true;
            this.cboNguoiDung.Location = new System.Drawing.Point(97, 9);
            this.cboNguoiDung.Name = "cboNguoiDung";
            this.cboNguoiDung.Size = new System.Drawing.Size(198, 21);
            this.cboNguoiDung.TabIndex = 0;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(356, 9);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(171, 20);
            this.txtTimKiem.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tìm kiếm";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Người ủy quyền";
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(701, 9);
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
            this.btnAdd.Location = new System.Drawing.Point(620, 9);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.cboNguoiDung);
            this.panel1.Controls.Add(this.txtTimKiem);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 40);
            this.panel1.TabIndex = 16;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnEdit);
            this.pnlFooter.Controls.Add(this.btnAdd);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 455);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(786, 40);
            this.pnlFooter.TabIndex = 17;
            // 
            // ucLienKetUyQuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.lblHeader);
            this.Name = "ucLienKetUyQuyen";
            this.Size = new System.Drawing.Size(786, 495);
            this.Load += new System.EventHandler(this.ucLienKetUyQuyen_Load);
            this.panGrid.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfgUyQuyen)).EndInit();
            this.panTreeView.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfgUnit)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panGrid;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgUyQuyen;
        private System.Windows.Forms.Panel panTreeView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cboNguoiDung;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgUnit;
    }
}
