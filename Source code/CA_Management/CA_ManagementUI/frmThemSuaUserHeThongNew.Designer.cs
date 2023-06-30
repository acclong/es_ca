namespace ES.CA_ManagementUI
{
    partial class frmThemSuaUserHeThongNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThemSuaUserHeThongNew));
            this.cfgDonVi = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.cfgProg = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.btnSeachUser = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cfgDonVi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfgProg)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cfgDonVi
            // 
            this.cfgDonVi.AllowEditing = false;
            this.cfgDonVi.AllowFiltering = true;
            this.cfgDonVi.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgDonVi.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgDonVi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cfgDonVi.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgDonVi.ColumnInfo = resources.GetString("cfgDonVi.ColumnInfo");
            this.cfgDonVi.ExtendLastCol = true;
            this.cfgDonVi.Location = new System.Drawing.Point(6, 44);
            this.cfgDonVi.Name = "cfgDonVi";
            this.cfgDonVi.Rows.DefaultSize = 19;
            this.cfgDonVi.ShowCursor = true;
            this.cfgDonVi.Size = new System.Drawing.Size(451, 405);
            this.cfgDonVi.StyleInfo = resources.GetString("cfgDonVi.StyleInfo");
            this.cfgDonVi.TabIndex = 1;
            this.cfgDonVi.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            // 
            // cfgProg
            // 
            this.cfgProg.AllowAddNew = true;
            this.cfgProg.AllowFiltering = true;
            this.cfgProg.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            this.cfgProg.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgProg.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgProg.ColumnInfo = "5,1,0,0,0,95,Columns:0{AllowSorting:False;AllowDragging:False;AllowResizing:False" +
    ";AllowEditing:False;}\t1{AllowFiltering:None;}\t";
            this.cfgProg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgProg.ExtendLastCol = true;
            this.cfgProg.Location = new System.Drawing.Point(3, 16);
            this.cfgProg.Name = "cfgProg";
            this.cfgProg.Rows.DefaultSize = 19;
            this.cfgProg.ShowCursor = true;
            this.cfgProg.Size = new System.Drawing.Size(579, 436);
            this.cfgProg.StyleInfo = resources.GetString("cfgProg.StyleInfo");
            this.cfgProg.TabIndex = 3;
            this.cfgProg.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue;
            this.cfgProg.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.cfgProg_AfterEdit);
            this.cfgProg.Click += new System.EventHandler(this.cfgProg_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(461, 493);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Đóng";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(380, 493);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Lưu";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Người dùng *";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(87, 6);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(188, 20);
            this.txtUserName.TabIndex = 6;
            // 
            // btnSeachUser
            // 
            this.btnSeachUser.Location = new System.Drawing.Point(281, 5);
            this.btnSeachUser.Name = "btnSeachUser";
            this.btnSeachUser.Size = new System.Drawing.Size(75, 23);
            this.btnSeachUser.TabIndex = 7;
            this.btnSeachUser.Text = "Tìm";
            this.btnSeachUser.UseVisualStyleBackColor = true;
            this.btnSeachUser.Click += new System.EventHandler(this.btnSeachUser_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cfgProg);
            this.groupBox1.Location = new System.Drawing.Point(12, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(585, 455);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chọn hệ thống";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRefresh);
            this.groupBox2.Controls.Add(this.cfgDonVi);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(603, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(467, 455);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chọn đơn vị";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(269, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Xem";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(62, 17);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(201, 20);
            this.textBox2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tìm kiếm";
            // 
            // frmThemSuaUserHeThongNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 528);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSeachUser);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmThemSuaUserHeThongNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmThemSuaUserHeThongNew";
            this.Load += new System.EventHandler(this.frmThemSuaUserHeThongNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cfgDonVi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfgProg)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid cfgDonVi;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgProg;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Button btnSeachUser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRefresh;

    }
}