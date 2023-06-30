using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using esDigitalSignature.eToken;
using esDigitalSignature;

namespace DemoSign
{
    public partial class CertAdmin : Form
    {
        HSMServiceProvider _hsm;
        byte[] id;

        public CertAdmin()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbLog.Clear();
            HSMServiceProvider hsm = new HSMServiceProvider();
            hsm.Dispose();
        }

        private void btnLoginAdmin_Click(object sender, EventArgs e)
        {
            try
            {
                _hsm = new HSMServiceProvider("cryptoki.dll");
                HSMReturnValue rv = _hsm.LoginAdmin(Int32.Parse(txtDeviceID.Text), HSMLoginRole.User, txtHSMPass.Text);
                if (rv != HSMReturnValue.OK)
                    MessageBox.Show(rv.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                _hsm.Logout();
                _hsm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnListSlot_Click(object sender, EventArgs e)
        {
            try
            {
                string text = "";

                PKCS11.Slot[] slots = PKCS11.GetSlotList(true);
                foreach (PKCS11.Slot slot in slots)
                {
                    text += "Slot ID: " + slot.Id.ToString() + "; Label: " + slot.GetTokenInfo().label + "\r\n";
                }

                rtbLog.Text = text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreateSlot_Click(object sender, EventArgs e)
        {
            try
            {
                txtSlotSerial.Text = _hsm.CreateSlot(txtLabel.Text);

                btnLogout_Click(null, null);
                btnLoginAdmin_Click(null, null);

                _hsm.InitToken(txtSlotSerial.Text, txtLabel.Text, txtSO_PIN.Text, txtUserPIN.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteSlot_Click(object sender, EventArgs e)
        {
            try
            {
                _hsm.DestroySlot(txtSlotSerial.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetPIN_Click(object sender, EventArgs e)
        {
            try
            {
                _hsm.ChangeSlotPIN(txtUserPIN.Text, txtNew.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLoginSlot_Click(object sender, EventArgs e)
        {
            try
            {
                _hsm.Logout();
                HSMReturnValue rv = _hsm.Login(txtSlotSerial.Text, HSMLoginRole.User, txtUserPIN.Text);
                int i = (int)rv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGenKeys_Click(object sender, EventArgs e)
        {
            try
            {
                id = _hsm.GenerateKeyPairAndRequest(HSMKeyPairType.RSA, txtKeySubject.Text, txtKeyLabelPUB.Text, txtKeyLabelPRV.Text, txtKeyLabelREQ.Text);
                txtKeyID.Text = Common.ConvertBytesToHex(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnToString_Click(object sender, EventArgs e)
        {
            try
            {
                id = Common.ConvertHexToByte(txtKeyID.Text);
                rtbLog.Text = _hsm.ExportCertificateRequestToPEM(id);//GenerateCertificateRequest(id, txtKeyLabelREQ.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnToCert_Click(object sender, EventArgs e)
        {
            try
            {
                id = Common.ConvertHexToByte(txtKeyID.Text);
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    X509Certificate2 cert = new X509Certificate2(ofd.FileName);
                    _hsm.ImportCertificateFromX509(txtKeyCertLabel.Text, cert);
                }

                //_hsm.SetObjectStatus("ToantkPUB", HSMObjectClass.PUBLIC_KEY, id, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                _hsm = new HSMServiceProvider(txtSlotSerial.Text, HSMLoginRole.User, txtUserPIN.Text);
                _hsm.DeleteObject(txtKeyCertLabel.Text, HSMObjectClass.CERTIFICATE, Common.ConvertHexToByte(txtKeyID.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                HSMServiceProvider hsm = new HSMServiceProvider(Common.CRYPTOKI);
                hsm.ReplicateToken(txtSlotSerial.Text, txtNew.Text, txtUserPIN.Text, txtDeviceID.Text);
                HSMServiceProvider.Finalize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnWrap_Click(object sender, EventArgs e)
        {
            try
            {
                HSMServiceProvider.Initialize(Common.CRYPTOKI);
                HSMServiceProvider hsm = new HSMServiceProvider();
                byte[] wrappedData = hsm.Wrap(txtSlotSerial.Text, txtUserPIN.Text, txtDeviceID.Text);

                rtbLog.Text = Common.ConvertBytesToHex(wrappedData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUnwrap_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] wrappedData = Common.ConvertHexToByte(rtbLog.Text);

                HSMServiceProvider hsm = new HSMServiceProvider();
                hsm.Unwrap(txtNew.Text, txtUserPIN.Text, wrappedData);
                HSMServiceProvider.Finalize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelWLD_Click(object sender, EventArgs e)
        {
            //Xóa WLD slot
            using (HSMServiceProvider hsm = new HSMServiceProvider())
            {
                hsm.DeleteWLDSlot(7);
            }
        }
    }
}
