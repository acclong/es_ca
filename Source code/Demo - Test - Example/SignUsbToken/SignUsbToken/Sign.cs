using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

namespace SignUsbToken
{
    public partial class Sign : Form
    {
        public Sign()
        {
            InitializeComponent();
        }

        bool bSignOffice = true;

        private void chkOffice_CheckedChanged(object sender, EventArgs e)
        {
            chkPdf.Checked = false;
            bSignOffice = true;
        }

        private void chkPdf_CheckedChanged(object sender, EventArgs e)
        {
            chkOffice.Checked = false;
            bSignOffice = false;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (bSignOffice)
            {
                //office
                OpenFileDialog OFD = new OpenFileDialog
                {
                    Filter = @"Tập tin (*.xlsx;*.xls;*.doc;*.docx)|*.xlsx;*.xls;*.doc;*.docx",
                    Multiselect = false,
                    Title = @"Chọn tập tin"
                };
                if (OFD.ShowDialog() == DialogResult.Cancel)
                    return;
                else
                    txtPath.Text = OFD.FileName;
                OFD.Dispose();
            }
            else
            {
                //Ký pdf
                OpenFileDialog OFD = new OpenFileDialog
                {
                    Filter = @"Tập tin (*.pdf)|*.pdf",
                    Multiselect = false,
                    Title = @"Chọn tập tin"
                };
                if (OFD.ShowDialog() == DialogResult.Cancel)
                    return;
                else
                    txtPath.Text = OFD.FileName;
                OFD.Dispose();
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (txtPath.Text == "")
            {
                MessageBox.Show("Chưa nhập file để ký!");
                return;
            }
            string sTypeFile = txtPath.Text.Trim().Split('\\').Last().Split('.').Last();
            if (sTypeFile == "pdf")
            {
                //Ký pdf
                try
                {
                    string sNameFile = txtPath.Text.Split('\\').Last();
                    string sPathDestination = txtPath.Text.Replace(sNameFile,sNameFile.Split('.').First()+"_Signed.pdf");
                    clsSignPdf SignPdf = new clsSignPdf();
                    X509Certificate2 cert = getCertBySNo(txtSeri.Text);
                    SignPdf.signPdfFile(txtPath.Text,sPathDestination,cert,null,null,null);
                    MessageBox.Show("Ký file thành công!!!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ký file thất bại \n\n" + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (sTypeFile == "doc" || sTypeFile == "docx" || sTypeFile == "xls" || sTypeFile == "xlsx")
            {
                // Ký file Office
                try
                {
                    clsSignWordExcel SignOffice = new clsSignWordExcel();
                    X509Certificate2 cert = getCertBySNo(txtSeri.Text);
                    SignOffice.signOfficeFileUsingPDSM(txtPath.Text, cert);
                    MessageBox.Show("Ký file thành công!!!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ký file thất bại \n\n" + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Nhập file sai định dạng!");
        }

        private void Sign_Load(object sender, EventArgs e)
        {
            chkOffice.Checked = true;
        }

        private void txtSeri_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSeri.Text == "")
                    return;
                char[] aPath = txtSeri.Text.Trim().ToUpper().ToCharArray();
                string seri = "";
                for (int i = 0; i < aPath.Length; i++)
                {
                    if (aPath[i] == ' ')
                        continue;
                    else
                        seri += aPath[i];
                }
                txtSeri.Text = seri;
            }
        }

        private X509Certificate2 getCertBySNo(string SerialNo)
        {
            // Access Personal (MY) certificate store of current user
            X509Store my = new X509Store();
            my.Open(OpenFlags.ReadOnly);

            // Find the certificate we'll use to sign            
            foreach (X509Certificate2 cert in my.Certificates)
            {
                if (cert.SerialNumber.Equals(SerialNo, StringComparison.InvariantCultureIgnoreCase))
                {                         
                    return cert;
                }

            }
            return null;
        }
    }
}
