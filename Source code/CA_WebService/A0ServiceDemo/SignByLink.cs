using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using X509Certificate2 = System.Security.Cryptography.X509Certificates.X509Certificate2;
using System.IO;
using esDigitalSignature;
using A0ServiceDemo.A0SignatureService;

namespace A0ServiceDemo
{
    public partial class SignByLink : Form
    {
        public SignByLink()
        {
            InitializeComponent();
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (txtSource.Text == "")
            {
                MessageBox.Show("Chưa có file để ký!");
                return;
            }
            try
            {
                //Tham số
                string sSource = txtSource.Text;
                string sDectiantion = txtDect.Text;
                string sUserProg = txtUserProg.Text;
                int iIDProg = Convert.ToInt32(txtIDProg.Text);

                //Gọi hàm ký server
                CAService sc = new CAService();
                bool bSignLink = sc.SignFileByLink(sSource, iIDProg, sUserProg, "123456");

                if (bSignLink)
                    MessageBox.Show("Ký file thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Ký file thất bại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ký file thất bại \n\n" + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
