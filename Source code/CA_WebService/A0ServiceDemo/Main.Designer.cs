namespace A0ServiceDemo
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.rdbByBase64 = new System.Windows.Forms.RadioButton();
            this.rdbByLink = new System.Windows.Forms.RadioButton();
            this.rdbByArrayFile = new System.Windows.Forms.RadioButton();
            this.btnDemo = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // rdbByBase64
            // 
            this.rdbByBase64.AutoSize = true;
            this.rdbByBase64.Location = new System.Drawing.Point(14, 14);
            this.rdbByBase64.Name = "rdbByBase64";
            this.rdbByBase64.Size = new System.Drawing.Size(78, 19);
            this.rdbByBase64.TabIndex = 0;
            this.rdbByBase64.TabStop = true;
            this.rdbByBase64.Text = "ByBase64\r\n";
            this.rdbByBase64.UseVisualStyleBackColor = true;
            // 
            // rdbByLink
            // 
            this.rdbByLink.AutoSize = true;
            this.rdbByLink.Location = new System.Drawing.Point(14, 52);
            this.rdbByLink.Name = "rdbByLink";
            this.rdbByLink.Size = new System.Drawing.Size(63, 19);
            this.rdbByLink.TabIndex = 1;
            this.rdbByLink.TabStop = true;
            this.rdbByLink.Text = "ByLink";
            this.rdbByLink.UseVisualStyleBackColor = true;
            // 
            // rdbByArrayFile
            // 
            this.rdbByArrayFile.AutoSize = true;
            this.rdbByArrayFile.Location = new System.Drawing.Point(14, 90);
            this.rdbByArrayFile.Name = "rdbByArrayFile";
            this.rdbByArrayFile.Size = new System.Drawing.Size(90, 19);
            this.rdbByArrayFile.TabIndex = 2;
            this.rdbByArrayFile.TabStop = true;
            this.rdbByArrayFile.Text = "ByArrayFile";
            this.rdbByArrayFile.UseVisualStyleBackColor = true;
            // 
            // btnDemo
            // 
            this.btnDemo.Location = new System.Drawing.Point(184, 12);
            this.btnDemo.Name = "btnDemo";
            this.btnDemo.Size = new System.Drawing.Size(87, 58);
            this.btnDemo.TabIndex = 3;
            this.btnDemo.Text = "Demo";
            this.btnDemo.UseVisualStyleBackColor = true;
            this.btnDemo.Click += new System.EventHandler(this.btnDemo_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(184, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 58);
            this.button1.TabIndex = 4;
            this.button1.Text = "TEST";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.ColumnInfo = "10,1,0,0,0,105,Columns:";
            this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c1FlexGrid1.Location = new System.Drawing.Point(0, 178);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.DefaultSize = 21;
            this.c1FlexGrid1.Size = new System.Drawing.Size(684, 231);
            this.c1FlexGrid1.StyleInfo = resources.GetString("c1FlexGrid1.StyleInfo");
            this.c1FlexGrid1.TabIndex = 5;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 409);
            this.Controls.Add(this.c1FlexGrid1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDemo);
            this.Controls.Add(this.rdbByArrayFile);
            this.Controls.Add(this.rdbByLink);
            this.Controls.Add(this.rdbByBase64);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbByBase64;
        private System.Windows.Forms.RadioButton rdbByLink;
        private System.Windows.Forms.RadioButton rdbByArrayFile;
        private System.Windows.Forms.Button btnDemo;
        private System.Windows.Forms.Button button1;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
    }
}