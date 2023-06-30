using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using A0ServiceDemo.A0SignatureService;
using System.Security.Cryptography.X509Certificates;
using X509Certificate2 = System.Security.Cryptography.X509Certificates.X509Certificate2;
using System.IO;
using esDigitalSignature;

namespace A0ServiceDemo
{
    public partial class SignArrayFile : Form
    {
        public SignArrayFile()
        {
            InitializeComponent();
            txtSource.Text = "~/FileBeforeSigned/Báo cáo thực tập hè.docx\n~/FileBeforeSigned/Ninhtq_Lịch.xlsx";
            txtDect.Text = "~/FileAfterSigned/Báo cáo thực tập hè.docx\n~/FileBeforeSigned/Báo cáo thực tập hè.docx";
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            try
            {
                //Tham số
                string[] aSource = txtSource.Text.Split('\n');
                string[] aDectiantion = txtDect.Text.Split('\n');
                string sUserProg = txtUserProg.Text;
                int iIDProg = Convert.ToInt32(txtIDProg.Text);

                //kiểm tra số file đầu vào và đầu ra
                if (aSource.Length > aDectiantion.Length)
                {
                    MessageBox.Show("Số file đầu vào nhiều hơn " + (aSource.Length - aDectiantion.Length) + "file so với số file đầu ra");
                    return;
                }
                else if (aSource.Length < aDectiantion.Length)
                {
                    MessageBox.Show("Số file đầu ra nhiều hơn " + (aDectiantion.Length - aSource.Length) + "file so với số file đầu vào");
                    return;
                }

                //Gọi hàm ký server
                //CAServiceSoapClient sc = new CAServiceSoapClient();
                //string sSignArrayFile = sc.SignFilesByLink(aSource, aDectiantion, sUserProg, iIDProg);
                //if (sSignArrayFile == "")
                //    MessageBox.Show("Ký file thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //else
                //    MessageBox.Show("Ký file thất bại!\n"+sSignArrayFile, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ký file thất bại \n\n" + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
