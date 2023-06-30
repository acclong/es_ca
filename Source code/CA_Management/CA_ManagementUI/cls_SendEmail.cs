using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace ES.CA_ManagementUI
{
    public class cls_SendEmail
    {
        public string From = string.Empty;
        public string To = string.Empty;
        public string User = string.Empty;
        public string Password = string.Empty;
        public string Subject = string.Empty;
        public string Body = string.Empty;
        public List<string> AttachmentPath = new List<string>();
        public string Host = "mail.nldc.evn.vn";//"127.0.0.1";
        public int Port = 25;
        public string CC = string.Empty;
        public string BCC = string.Empty;
        public bool IsHtml = false;
        public int SendUsing = 0;//0 = Network, 1 = PickupDirectory, 2 = SpecifiedPickupDirectory
        public bool UseSSL = true;
        public int AuthenticationMode = 1;//0 = No authentication, 1 = Plain Text, 2 = NTLM authentication

        public cls_SendEmail()
        {

        }
        public void SendEmail()
        {
            new Thread(new ThreadStart(SendMessage)).Start();
            //SendMessage();
        }
        /// <summary>
        /// Send Email Message method.
        /// </summary>
        private void SendMessage()
        {
            try
            {
                MailMessage oMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient(Host);

                oMessage.From = new MailAddress(From);
                oMessage.To.Add(To);
                oMessage.Subject = Subject;
                oMessage.IsBodyHtml = IsHtml;
                oMessage.Body = Body;

                if (CC != string.Empty)
                    oMessage.CC.Add(CC);

                try
                {
                    if (BCC != string.Empty)
                        oMessage.Bcc.Add(BCC);
                }
                catch
                {
                    //bỏ qua ko cần Bcc nếu lỗi
                }

                switch (SendUsing)
                {
                    case 0:
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        break;
                    case 1:
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                        break;
                    case 2:
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        break;
                    default:
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        break;

                }
                if (AuthenticationMode > 0)
                {
                    smtpClient.Credentials = new NetworkCredential(User, Password);
                }

                smtpClient.Port = Port;
                //smtpClient.EnableSsl = UseSSL;

                // Create and add the attachment
                for (int i = 0; i < AttachmentPath.Count; i++)
                {
                    if (AttachmentPath[i] != string.Empty)
                    {
                        string[] attach = AttachmentPath[i].Split(Convert.ToChar(";"));
                        if (attach.Length > 0)
                        {
                            for (int j = 0; j < attach.Length; j++)
                            {
                                oMessage.Attachments.Add(new Attachment(attach[j]));
                            }
                        }
                    }
                }

                try
                {
                    // Deliver the message
                    smtpClient.Send(oMessage);
                }

                catch (Exception ex)
                {
                    ex.ToString();
                }
                finally
                {
                    oMessage.Dispose();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
