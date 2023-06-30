namespace SignApp
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnlChucNang = new System.Windows.Forms.Panel();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnSign = new System.Windows.Forms.Button();
            this.rdbFolder = new System.Windows.Forms.RadioButton();
            this.rdbOnlyFile = new System.Windows.Forms.RadioButton();
            this.btnOpen = new System.Windows.Forms.Button();
            this.pnlButtom = new System.Windows.Forms.Panel();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.cfgFile = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlChucNang.SuspendLayout();
            this.pnlButtom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgFile)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlChucNang
            // 
            this.pnlChucNang.Controls.Add(this.btnHelp);
            this.pnlChucNang.Controls.Add(this.btnAbout);
            this.pnlChucNang.Controls.Add(this.btnReload);
            this.pnlChucNang.Controls.Add(this.btnSign);
            this.pnlChucNang.Controls.Add(this.rdbFolder);
            this.pnlChucNang.Controls.Add(this.rdbOnlyFile);
            this.pnlChucNang.Controls.Add(this.btnOpen);
            this.pnlChucNang.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChucNang.Location = new System.Drawing.Point(0, 0);
            this.pnlChucNang.Name = "pnlChucNang";
            this.pnlChucNang.Size = new System.Drawing.Size(1170, 68);
            this.pnlChucNang.TabIndex = 0;
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.BackColor = System.Drawing.Color.Transparent;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.ForeColor = System.Drawing.Color.Black;
            this.btnHelp.Image = global::SignApp.Properties.Resources.help;
            this.btnHelp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnHelp.Location = new System.Drawing.Point(1064, 9);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(46, 51);
            this.btnHelp.TabIndex = 10;
            this.btnHelp.Text = "Help";
            this.btnHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbout.BackColor = System.Drawing.Color.Transparent;
            this.btnAbout.FlatAppearance.BorderSize = 0;
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbout.ForeColor = System.Drawing.Color.Black;
            this.btnAbout.Image = global::SignApp.Properties.Resources.document_properties;
            this.btnAbout.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAbout.Location = new System.Drawing.Point(1112, 9);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(50, 51);
            this.btnAbout.TabIndex = 9;
            this.btnAbout.Text = "About";
            this.btnAbout.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnReload
            // 
            this.btnReload.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnReload.FlatAppearance.BorderSize = 0;
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReload.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReload.ForeColor = System.Drawing.Color.White;
            this.btnReload.Image = global::SignApp.Properties.Resources._01_refresh;
            this.btnReload.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReload.Location = new System.Drawing.Point(188, 9);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(60, 51);
            this.btnReload.TabIndex = 6;
            this.btnReload.Text = "Refresh";
            this.btnReload.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReload.UseVisualStyleBackColor = false;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnSign
            // 
            this.btnSign.BackColor = System.Drawing.Color.Olive;
            this.btnSign.FlatAppearance.BorderSize = 0;
            this.btnSign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSign.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSign.ForeColor = System.Drawing.Color.White;
            this.btnSign.Image = global::SignApp.Properties.Resources.edit_file;
            this.btnSign.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSign.Location = new System.Drawing.Point(252, 9);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(60, 51);
            this.btnSign.TabIndex = 4;
            this.btnSign.Text = "Sign";
            this.btnSign.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSign.UseVisualStyleBackColor = false;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // rdbFolder
            // 
            this.rdbFolder.AutoSize = true;
            this.rdbFolder.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbFolder.Location = new System.Drawing.Point(75, 39);
            this.rdbFolder.Name = "rdbFolder";
            this.rdbFolder.Size = new System.Drawing.Size(106, 20);
            this.rdbFolder.TabIndex = 3;
            this.rdbFolder.TabStop = true;
            this.rdbFolder.Text = "Chọn thư mục";
            this.rdbFolder.UseVisualStyleBackColor = true;
            // 
            // rdbOnlyFile
            // 
            this.rdbOnlyFile.AutoSize = true;
            this.rdbOnlyFile.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbOnlyFile.Location = new System.Drawing.Point(75, 11);
            this.rdbOnlyFile.Name = "rdbOnlyFile";
            this.rdbOnlyFile.Size = new System.Drawing.Size(104, 20);
            this.rdbOnlyFile.TabIndex = 1;
            this.rdbOnlyFile.TabStop = true;
            this.rdbOnlyFile.Text = "Chọn văn bản";
            this.rdbOnlyFile.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.Color.DarkOrange;
            this.btnOpen.FlatAppearance.BorderSize = 0;
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.ForeColor = System.Drawing.Color.White;
            this.btnOpen.Image = global::SignApp.Properties.Resources.opened;
            this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpen.Location = new System.Drawing.Point(12, 9);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(60, 51);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // pnlButtom
            // 
            this.pnlButtom.Controls.Add(this.chkAll);
            this.pnlButtom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtom.Location = new System.Drawing.Point(0, 594);
            this.pnlButtom.Name = "pnlButtom";
            this.pnlButtom.Size = new System.Drawing.Size(1170, 34);
            this.pnlButtom.TabIndex = 1;
            // 
            // chkAll
            // 
            this.chkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(10, 6);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(108, 23);
            this.chkAll.TabIndex = 2;
            this.chkAll.Text = "Chọn tất cả";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // cfgFile
            // 
            this.cfgFile.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.cfgFile.BackColor = System.Drawing.Color.AliceBlue;
            this.cfgFile.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            this.cfgFile.ColumnInfo = "10,0,0,0,0,120,Columns:0{Width:30;}\t";
            this.cfgFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfgFile.ExtendLastCol = true;
            this.cfgFile.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cfgFile.Location = new System.Drawing.Point(0, 68);
            this.cfgFile.Name = "cfgFile";
            this.cfgFile.Rows.DefaultSize = 24;
            this.cfgFile.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
            this.cfgFile.Size = new System.Drawing.Size(1170, 526);
            this.cfgFile.StyleInfo = resources.GetString("cfgFile.StyleInfo");
            this.cfgFile.TabIndex = 2;
            this.cfgFile.DoubleClick += new System.EventHandler(this.cfgFile_DoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1170, 628);
            this.Controls.Add(this.cfgFile);
            this.Controls.Add(this.pnlButtom);
            this.Controls.Add(this.pnlChucNang);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chương trình hỗ trợ ký số file văn bản";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.pnlChucNang.ResumeLayout(false);
            this.pnlChucNang.PerformLayout();
            this.pnlButtom.ResumeLayout(false);
            this.pnlButtom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfgFile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlChucNang;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.RadioButton rdbFolder;
        private System.Windows.Forms.RadioButton rdbOnlyFile;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Panel pnlButtom;
        private C1.Win.C1FlexGrid.C1FlexGrid cfgFile;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnAbout;

    }
}

