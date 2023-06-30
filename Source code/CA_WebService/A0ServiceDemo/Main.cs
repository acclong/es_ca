using A0ServiceDemo.A0SignatureService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace A0ServiceDemo
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnDemo_Click(object sender, EventArgs e)
        {
            if(rdbByBase64.Checked)
            {
                SignBase64 sb = new SignBase64();
                this.Visible = false;
                sb.ShowDialog();
                this.Visible = true;
            }
            else if (rdbByLink.Checked)
            {
                SignByLink sl = new SignByLink();
                this.Visible = false;
                sl.ShowDialog();
                this.Visible = true;
            }
            else if (rdbByArrayFile.Checked)
            {
                SignArrayFile saf = new SignArrayFile();
                this.Visible = false;
                saf.ShowDialog();
                this.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable x = new DataTable("RESULTS");
            string error ="";
            CAService ca = new CAService();
            ca.Timeout = 300000;

            string file = "";
            for (int i = 1500; i <= 1500; i++)
            {
                file += i.ToString() + ";";
            }

            DateTime start = DateTime.Now;
            bool ret = ca.SignFilesByID_ReturnDetail_NoApp(file, "TTTT_A0", "a0_kylap123", "123456", ref x, ref error);
            DateTime end = DateTime.Now;

            c1FlexGrid1.DataSource = x;
            MessageBox.Show((end - start).TotalSeconds.ToString() + "\n" + error);
        }
    }
}
