namespace ES.CA_ManagementUI
{
    partial class ucLienKetUserHeThong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucLienKetUserHeThong));
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblUpdateBiddingUser = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cboHeThong = new System.Windows.Forms.ComboBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cfgUserProg = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panTreeView = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cfgUnit = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.trvUnit = new System.Windows.Forms.TreeView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panGrid = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.c1ContextMenu1 = new C1.Win.C1Command.C1ContextMenu();
            this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            this.pnlFooter.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgUserProg)).BeginInit();
            this.panel2.SuspendLayout();
            this.panTreeView.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgUnit)).BeginInit();
            this.panGrid.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(789, 40);
            this.lblHeader.TabIndex = 14;
            this.lblHeader.Text = "PHÂN QUYỀN NGƯỜI DÙNG - HỆ THỐNG";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnEdit);
            this.pnlFooter.Controls.Add(this.btnAdd);
            this.pnlFooter.Controls.Add(this.lblUpdateBiddingUser);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 455);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(789, 40);
            this.pnlFooter.TabIndex = 2;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(704, 9);
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
            this.btnAdd.Location = new System.Drawing.Point(623, 9);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblUpdateBiddingUser
            // 
            this.lblUpdateBiddingUser.AutoSize = true;
            this.lblUpdateBiddingUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUpdateBiddingUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateBiddingUser.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblUpdateBiddingUser.Location = new System.Drawing.Point(4, 14);
            this.lblUpdateBiddingUser.Name = "lblUpdateBiddingUser";
            this.lblUpdateBiddingUser.Size = new System.Drawing.Size(178, 13);
            this.lblUpdateBiddingUser.TabIndex = 0;
            this.lblUpdateBiddingUser.Text = "Cập nhật người dùng Bidding Server";
            this.lblUpdateBiddingUser.Click += new System.EventHandler(this.lblUpdateBiddingUser_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.cboHeThong);
            this.panel1.Controls.Add(this.txtTimKiem);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(789, 40);
            this.panel1.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(509, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Xem";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cboHeThong
            // 
            this.cboHeThong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHeThong.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboHeThong.FormattingEnabled = true;
            this.cboHeThong.Location = new System.Drawing.Point(60, 9);
            this.cboHeThong.Name = "cboHeThong";
            this.cboHeThong.Size = new System.Drawing.Size(211, 21);
            this.cboHeThong.TabIndex = 0;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(332, 9);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(171, 20);
            this.txtTimKiem.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 13);
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
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hệ thống";
            // 
            // cfgUserProg
            // 
            this.cfgUserProg.AllowEditing = false;
            this.cfgUserProg.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgUserProg.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgUserProg.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgUserProg.ColumnInfo = resources.GetString("cfgUserProg.ColumnInfo");
            this.cfgUserProg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgUserProg.ExtendLastCol = true;
            this.cfgUserProg.Location = new System.Drawing.Point(3, 16);
            this.cfgUserProg.Name = "cfgUserProg";
            this.cfgUserProg.Rows.DefaultSize = 19;
            this.cfgUserProg.ShowCursor = true;
            this.cfgUserProg.Size = new System.Drawing.Size(500, 356);
            this.cfgUserProg.StyleInfo = resources.GetString("cfgUserProg.StyleInfo");
            this.cfgUserProg.TabIndex = 1;
            this.cfgUserProg.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            this.cfgUserProg.DoubleClick += new System.EventHandler(this.cfgUserProg_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panTreeView);
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.panGrid);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(789, 375);
            this.panel2.TabIndex = 15;
            // 
            // panTreeView
            // 
            this.panTreeView.Controls.Add(this.groupBox2);
            this.panTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panTreeView.Location = new System.Drawing.Point(511, 0);
            this.panTreeView.Name = "panTreeView";
            this.panTreeView.Size = new System.Drawing.Size(278, 375);
            this.panTreeView.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cfgUnit);
            this.groupBox2.Controls.Add(this.trvUnit);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 375);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Quyền đơn vị";
            // 
            // cfgUnit
            // 
            this.cfgUnit.AllowAddNew = true;
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
            this.cfgUnit.Size = new System.Drawing.Size(272, 356);
            this.cfgUnit.StyleInfo = resources.GetString("cfgUnit.StyleInfo");
            this.cfgUnit.TabIndex = 2;
            this.cfgUnit.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            this.cfgUnit.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.cfgUnit_AfterEdit);
            // 
            // trvUnit
            // 
            this.trvUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvUnit.Location = new System.Drawing.Point(3, 16);
            this.trvUnit.Name = "trvUnit";
            this.trvUnit.Size = new System.Drawing.Size(272, 356);
            this.trvUnit.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(506, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 375);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // panGrid
            // 
            this.panGrid.Controls.Add(this.groupBox1);
            this.panGrid.Dock = System.Windows.Forms.DockStyle.Left;
            this.panGrid.Location = new System.Drawing.Point(0, 0);
            this.panGrid.Name = "panGrid";
            this.panGrid.Size = new System.Drawing.Size(506, 375);
            this.panGrid.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cfgUserProg);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(506, 375);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Người dùng - hệ thống";
            // 
            // c1ContextMenu1
            // 
            this.c1ContextMenu1.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink1});
            this.c1ContextMenu1.Name = "c1ContextMenu1";
            this.c1ContextMenu1.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Text = "New Command";
            // 
            // c1CommandHolder1
            // 
            this.c1CommandHolder1.Commands.Add(this.c1ContextMenu1);
            this.c1CommandHolder1.Owner = this;
            // 
            // ucLienKetUserHeThong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.pnlFooter);
            this.Name = "ucLienKetUserHeThong";
            this.Size = new System.Drawing.Size(789, 495);
            this.Load += new System.EventHandler(this.ucPhanQuyenUserHeThong_Load);
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgUserProg)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panTreeView.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfgUnit)).EndInit();
            this.panGrid.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cboHeThong;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lblUpdateBiddingUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgUserProg;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panTreeView;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panGrid;
        private System.Windows.Forms.TreeView trvUnit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgUnit;
        private C1.Win.C1Command.C1ContextMenu c1ContextMenu1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink1;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
    }
}
