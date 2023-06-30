using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using esDigitalSignature;
using ES.CA_ManagementBUS;
using System.IO;

namespace ES.CA_ManagementUI
{
    public partial class frmThemSuaNhaCungCapCA : Form
    {
        #region Var
        
        BUSQuanTri _bus = new BUSQuanTri();
        int _CertAuthID;
        X509Certificate2 _x509Cert;
        byte[] _fileCert;

        public int CertAuthID
        {
            get { return _CertAuthID; }
            set { _CertAuthID = value; }
        }

        #endregion

        public frmThemSuaNhaCungCapCA()
        {
            InitializeComponent();
        }

        private void frmThemNhaCungCapCA_Load(object sender, EventArgs e)
        {
            try
            {
                if (CertAuthID == 0)
                {
                    btnLoadFile.Text = "Load file";
                    chkShowRevoked.Enabled = false;
                }
                else
                {
                    btnLoadFile.Visible = false;
                    btnLoadFile.Text = "Get file";
                    chkShowRevoked.Enabled = true;

                    DataTable dtCert = _bus.CA_CertificationAuthority_SelectByID(CertAuthID);

                    _x509Cert = new X509Certificate2((byte[])(dtCert.Rows[0]["RawData"]));
                    //_fileCert = (byte[])(dtCert.Rows[0]["FileCert"]);
                    FillControlsFromCert();
                    txtCRL.Text = dtCert.Rows[0]["CRL_URL"].ToString();
                    if (dtCert.Rows[0]["RevokedFrom"] != DBNull.Value)
                    {
                        chkShowRevoked.Checked = true;
                        dpkRevoked.Value = Convert.ToDateTime(dtCert.Rows[0]["RevokedFrom"]);
                    }
                }

            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region Controls
        
        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (CertAuthID == 0)
                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        ofd.Multiselect = false;
                        ofd.Filter = "Certificate Files (*.cer,*.crt,*.pem)|*.cer;*.crt;*.pem|All files (*.*)|*.*";
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            _x509Cert = Common.GetCertificateByFile(ofd.FileName);
                            _fileCert = File.ReadAllBytes(ofd.FileName);
                            FillControlsFromCert();
                        }
                    }
                else
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.FileName = txtName.Text + ".cer";
                        sfd.Filter = "Certificate Files (*.cer,*.crt)|*.cer;*.crt|All files (*.*)|*.*";

                        if (sfd.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(sfd.FileName))
                        {
                            File.WriteAllBytes(sfd.FileName, _fileCert);
                        }
                    }                
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Lỗi khi load chứng thư số!\n\n" + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_x509Cert == null)
                {
                    clsShare.Message_Info("Không có thông tin chứng thư của nhà cung cấp!");
                    return;
                }

                if (chkShowRevoked.Checked)
                    if (clsShare.Message_WarningYN("Bạn có chắc chắn lưu thông tin thu hồi nhà cung cấp CA?") == false)
                        return;

                if (CertAuthID == 0)
                    _bus.CA_CertificationAuthority_Insert(CertAuthID, _x509Cert, txtCRL.Text, clsShare.sUserName);
                else
                    _bus.CA_CertificationAuthority_Update(CertAuthID, txtCRL.Text, chkShowRevoked.Checked ? dpkRevoked.Value : DateTime.MaxValue, clsShare.sUserName);

                clsShare.Message_Info("Cập nhật nhà cung cấp CA thành công!");
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkShowRevoked_CheckedChanged(object sender, EventArgs e)
        {
            dpkRevoked.Visible = chkShowRevoked.Checked;
        }

        #endregion

        #region Event
        
        private void FillControlsFromCert()
        {
            txtName.Text = _x509Cert.GetNameInfo(X509NameType.DnsName, false);
            txtSerial.Text = _x509Cert.SerialNumber;
            txtSubject.Text = _x509Cert.Subject.Replace(", ", "\r\n");
            txtIssuer.Text = _x509Cert.Issuer.Replace(", ", "\r\n");
            txtValidFrom.Text = _x509Cert.NotBefore.ToString("dd/MM/yyyy HH:mm:ss");
            txtValidTo.Text = _x509Cert.NotAfter.ToString("dd/MM/yyyy HH:mm:ss");
            txtThumbPrint.Text = _x509Cert.Thumbprint;
        }

        private void frmThemSuaNhaCungCapCA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnCancel_Click(null, null);
            }
        }

        #endregion
    }
}
