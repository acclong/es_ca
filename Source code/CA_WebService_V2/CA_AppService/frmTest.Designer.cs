namespace CA_AppService
{
    partial class frmTest
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTest));
            this.tmrTimeout = new System.Windows.Forms.Timer(this.components);
            this.txtThread = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSign = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.imgLoading = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrTimeout
            // 
            this.tmrTimeout.Interval = 1000;
            this.tmrTimeout.Tick += new System.EventHandler(this.tmrTimeout_Tick);
            // 
            // txtThread
            // 
            this.txtThread.Location = new System.Drawing.Point(161, 12);
            this.txtThread.Name = "txtThread";
            this.txtThread.Size = new System.Drawing.Size(75, 20);
            this.txtThread.TabIndex = 0;
            this.txtThread.Text = "20";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Max Thread";
            // 
            // btnSign
            // 
            this.btnSign.Location = new System.Drawing.Point(64, 50);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(75, 23);
            this.btnSign.TabIndex = 7;
            this.btnSign.Text = "Ký";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(161, 50);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 8;
            this.btnRemove.Text = "Bỏ chữ ký";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // imgLoading
            // 
            this.imgLoading.Image = ((System.Drawing.Image)(resources.GetObject("imgLoading.Image")));
            this.imgLoading.InitialImage = ((System.Drawing.Image)(resources.GetObject("imgLoading.InitialImage")));
            this.imgLoading.Location = new System.Drawing.Point(66, 80);
            this.imgLoading.Name = "imgLoading";
            this.imgLoading.Size = new System.Drawing.Size(170, 150);
            this.imgLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLoading.TabIndex = 9;
            this.imgLoading.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button1.Location = new System.Drawing.Point(8, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 57);
            this.button1.TabIndex = 10;
            this.button1.Text = "XXX";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 241);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.imgLoading);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnSign);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtThread);
            this.Name = "frmTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTest";
            ((System.ComponentModel.ISupportInitialize)(this.imgLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrTimeout;
        private System.Windows.Forms.TextBox txtThread;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.PictureBox imgLoading;
        private System.Windows.Forms.Button button1;
    }
}