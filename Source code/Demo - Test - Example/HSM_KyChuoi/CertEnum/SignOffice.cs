using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

namespace CertEnum
{
    public partial class SignOffice : Form
    {
        public SignOffice()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog
            {
                Filter = @"Tập tin (*.xlsx;*.xls;*.doc;*.docx)|*.xlsx;*.xls;*.doc;*.docx",
                Multiselect = false,
                Title = @"Chọn tập tin"
            };
            if (OFD.ShowDialog() == DialogResult.Cancel)
                return;
            else
                textBox1.Text = OFD.FileName;
            OFD.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                clsSignWordExcel SignOffice = new clsSignWordExcel();
                //X509Certificate2 cert = getCertBySNo("01");
                X509Certificate2 cert = SignOffice.DisplayCertificates();
                SignOffice.signOfficeFileUsingPDSM(textBox1.Text, cert);
                MessageBox.Show("Ký file thành công!!!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ký file thất bại \n\n" + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
