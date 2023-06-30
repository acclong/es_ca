using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ES.CA_ManagementBUS;
using ESLogin;
using Microsoft.VisualBasic;
using System.Net.Mail;
using System.Net;

namespace ES.CA_ManagementUI
{
    public partial class frmCauHinhEmail : Form
    {
        private BUSQuanTri _bus = new BUSQuanTri();

        public frmCauHinhEmail()
        {
            InitializeComponent();
        }

        private void frmCauHinhEmail_Load(object sender, EventArgs e)
        {
            try
            {
                GetConfig();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void GetConfig()
        {
            DataTable dtConfig = _bus.Q_CONFIG_SelectAll();

            txtEmail.Text = dtConfig.Rows[6]["Value"].ToString();
            txtPass.Text = StringCryptor.DecryptString(dtConfig.Rows[7]["Value"].ToString());
            txtServer.Text = dtConfig.Rows[8]["Value"].ToString();
            nudPort.Value = Convert.ToInt32(dtConfig.Rows[9]["Value"]);
            ckbSSL.Checked = Convert.ToBoolean(dtConfig.Rows[10]["Value"]);
        }

        private void UpdateConfig()
        {
            _bus.Q_CONFIG_Update(7, txtEmail.Text);
            _bus.Q_CONFIG_Update(8, StringCryptor.EncryptString(txtPass.Text));
            _bus.Q_CONFIG_Update(9, txtServer.Text);
            _bus.Q_CONFIG_Update(10, nudPort.Value.ToString());
            _bus.Q_CONFIG_Update(11, ckbSSL.Checked.ToString());
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                //Nhập địa chỉ nhận
                string email_to = Interaction.InputBox("Hãy nhập địa chỉ nhận e-mail kiểm tra:", "Gửi e-mal kiểm tra");
                if (email_to == "")
                    return;

                // gán giá trị thong tin mail
                string email_sender = txtEmail.Text;
                string email_password = txtPass.Text;
                string smtp_server = txtServer.Text;
                string smtp_port = nudPort.Value.ToString();
                string smtp_ssl = ckbSSL.Checked.ToString();
                                
                //Khởi tạo thông số
                SmtpClient client = new SmtpClient(smtp_server);
                client.Port = Convert.ToInt32(smtp_port);
                client.EnableSsl = Convert.ToBoolean(smtp_ssl);
                client.Timeout = 60000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(email_sender, email_password);
                //Soạn message
                MailMessage msg = new MailMessage();
                msg.IsBodyHtml = true;
                msg.From = new MailAddress(email_sender);
                msg.To.Add(email_to);
                msg.Subject = "[EVNNLDC] Kiểm tra e-mail hệ thống CA";

                msg.Body = String.Format(@"
                            <b><i>E-mail này được gửi từ hệ thống CA để kiểm tra thông số kết nối phục vụ gửi e-mail thông báo đến người dùng sau này.</i></b>");

                client.Send(msg);

                clsShare.Message_Info("Hoàn thành gửi e-mail kiểm tra.");
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Kiểm tra e-mail không thành công: " + ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateConfig();
                clsShare.Message_Info("Cập nhật dữ liệu thành công!");
                this.Close();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
