using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SignApp
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void lblEVN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.nldc.evn.vn/");
        }

        private void lbllCtyES_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://e-solutions.com.vn/");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://e-solutions.com.vn/");
        }

        private void picIcon_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.nldc.evn.vn/");
        }
    }
}
