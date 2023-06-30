using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

namespace ValidateCert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Lấy certificate
                X509Certificate2 cert = new X509Certificate2();
                if (!txtSeri.Text.Contains(':'))
                    cert = getCertBySNo(txtSeri.Text);
                else
                    cert = getCertByFile(txtSeri.Text);

                //MyX509Validator.Validate(cert);
                CertificateValidator validator = new CertificateValidator();
                validator.RevocationMode = CRevocationMode.CustomCRLFile;
                validator.CrlLink = "C:\\Users\\TranKim\\Desktop\\CA\\vnptca.crl";
                validator.VerificationTime = new DateTime(2015, 1, 20);
                validator.Build(cert);

                MessageBox.Show(validator.ValidationStatus.ToString(), "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
