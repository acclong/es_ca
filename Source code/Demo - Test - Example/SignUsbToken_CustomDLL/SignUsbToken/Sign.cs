using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace SignUsbToken
{
    public partial class Sign : Form
    {
        bool bSignOffice = true;

        public Sign()
        {
            InitializeComponent();
        }

        private void Sign_Load(object sender, EventArgs e)
        {
            chkOffice.Checked = true;
            txtSeri.Text = "‎5401DBF72B8A7CCBE6FA70BDD7293D10";
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

        private void chkOffice_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPdf.Checked)
            {
                chkPdf.Checked = false;
                bSignOffice = true;
            }
            else
                chkOffice.Checked = true;
        }

        private void chkPdf_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOffice.Checked)
            {
                chkOffice.Checked = false;
                bSignOffice = false;
            }
            else
                chkPdf.Checked = true;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (bSignOffice)
            {
                //office
                OpenFileDialog OFD = new OpenFileDialog
                {
                    Filter = @"Office document (*.xlsx;*.xls;*.doc;*.docx)|*.xlsx;*.xls;*.doc;*.docx",
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
                    Filter = @"PDF file (*.pdf)|*.pdf",
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

        private void btnFileCRT_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog
            {
                Filter = @"Tập tin (*.pem;*.crt;*.cer)|*.pem;*.crt;*.cer",
                Multiselect = false,
                Title = @"Chọn tập tin"
            };
            if (OFD.ShowDialog() == DialogResult.Cancel)
                return;
            else
                txtSeri.Text = OFD.FileName;
            OFD.Dispose();
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (txtSeri.Text == "")
            {
                MessageBox.Show("Chưa có certificate!");
                return;
            }

            //Lấy certificate
            X509Certificate2 cert = new X509Certificate2();
            if (!txtSeri.Text.Contains(':'))
                cert = getCertBySNo(txtSeri.Text);
            else
                cert = getCertByFile(txtSeri.Text);

            if (txtPath.Text == "")
            {
                MessageBox.Show("Chưa có file để ký!");
                return;
            }

            try
            {
                string sTypeFile = Path.GetExtension(txtPath.Text);
                if (sTypeFile == ".pdf")
                {
                    //Ký pdf
                    string sNameFile = Path.GetFileNameWithoutExtension(txtPath.Text);
                    string sPathDestination = Path.GetDirectoryName(txtPath.Text) + "\\" + sNameFile + "_signed.pdf";

                    clsSignPdf SignPdf = new clsSignPdf();
                    if (SignPdf.SignPdfFile(txtPath.Text, sPathDestination, cert, null, null, null))
                        MessageBox.Show("Ký file thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Ký file thất bại \n\n", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (sTypeFile == ".doc" || sTypeFile == ".docx" || sTypeFile == ".xls" || sTypeFile == ".xlsx")
                {
                    // Ký file Office
                    clsSignWordExcel SignOffice = new clsSignWordExcel();
                    SignOffice.signOfficeFileUsingPDSM(txtPath.Text, cert);
                    MessageBox.Show("Ký file thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Không hỗ trợ định dạng file!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ký file thất bại \n\n" + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (txtPath.Text == "")
            {
                MessageBox.Show("Chưa có file để ký!");
                return;
            }

            try
            {
                string sTypeFile = Path.GetExtension(txtPath.Text);
                if (sTypeFile == ".pdf")
                {
                    string err = "";

                    clsSignPdf SignPdf = new clsSignPdf();
                    if (!SignPdf.VerifyAllSignatures(txtPath.Text, ref err))
                        MessageBox.Show("Result: " + err, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Result: " + "Success", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (sTypeFile == ".doc" || sTypeFile == ".docx" || sTypeFile == ".xls" || sTypeFile == ".xlsx")
                {
                    // Check file Office
                    clsSignWordExcel SignOffice = new clsSignWordExcel();
                    string s = SignOffice.VerifyAllSignatures(txtPath.Text);
                    MessageBox.Show("Result: " + s, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Không hỗ trợ định dạng file!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verify thất bại \n\n" + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private X509Certificate2 getCertByFile(string crtPath)
        {
            X509Certificate2 cert = new X509Certificate2(crtPath);
            return cert;
        }
    }
}
