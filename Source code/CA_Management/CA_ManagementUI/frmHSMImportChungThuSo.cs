using ES.CA_ManagementBUS;
using esDigitalSignature;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace ES.CA_ManagementUI
{
    public partial class frmHSMImportChungThuSo : Form
    {
        #region Var
        BUSQuanTri _bus = new BUSQuanTri();

        private DataTable _dtSlot, _dtUser;
        private byte[] _CKA_ID;
        private int _slotID = -1;
        private int _userA0 = -1;
        private X509Certificate2 _x509Cert;

        public int UserA0
        {
            get { return _userA0; }
            set { _userA0 = value; }
        }

        public int SlotID
        {
            get { return _slotID; }
            set { _slotID = value; }
        }

        public string Label_Certificate
        {
            get { return txtCertLabel.Text; }
        }

        public byte[] CKA_ID
        {
            get { return _CKA_ID; }
        }

        public X509Certificate2 X509Cert
        {
            get { return _x509Cert; }
        }

        public string Subject
        {
            get { return txtSubject.Text; }
        }
        #endregion

        public frmHSMImportChungThuSo()
        {
            InitializeComponent();
        }

        private void frmHSMImportChungThuSo_Load(object sender, EventArgs e)
        {
            try
            {
                InitCboUserA0();
                InitCboSlotID();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }


        #region Init
        private void InitCboUserA0()
        {
            _dtUser = _bus.CA_User_SelectByUnitID(1);

            DataRow dr = _dtUser.NewRow();
            dr["UserID"] = -1;
            dr["Name"] = "[Không có]";
            _dtUser.Rows.InsertAt(dr, 0);

            cboUserA0.DataSource = _dtUser;
            cboUserA0.DisplayMember = "Name";
            cboUserA0.ValueMember = "UserID";
            cboUserA0.SelectedValue = _userA0;
        }

        private void InitCboSlotID()
        {
            _dtSlot = _bus.HSM_Slot_SelectByInitToken(true);
            cboSlotID.DataSource = _dtSlot;
            cboSlotID.DisplayMember = "Label_SlotID";
            cboSlotID.ValueMember = "SlotID";
            if (_slotID != -1)
                cboSlotID.SelectedValue = _slotID;
            else
                cboSlotID.SelectedIndex = 0;
        }

        private void FillControlsFromCert(X509Certificate2 x509Cert)
        {
            // hiển thị thông tin của chứng thư
            txtName.Text = x509Cert.GetNameInfo(X509NameType.DnsName, false);
            txtSerial.Text = x509Cert.SerialNumber;
            txtSubject.Text = x509Cert.Subject.Replace(", ", "\r\n");
            txtIssuer.Text = x509Cert.Issuer.Replace(", ", "\r\n");
            txtValidFrom.Text = x509Cert.NotBefore.ToString("dd/MM/yyyy HH:mm:ss");
            txtValidTo.Text = x509Cert.NotAfter.ToString("dd/MM/yyyy HH:mm:ss");
            txtThumbPrint.Text = x509Cert.Thumbprint;
        }
        #endregion

        #region Controls

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                // load file để tạo mới
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Multiselect = false;

                    ofd.Filter = "Certificate Files (*.cer,*.crt,*.pem)|*.cer;*.crt;*.pem|All files (*.*)|*.*";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        _x509Cert = Common.GetCertificateByFile(ofd.FileName);
                        FillControlsFromCert(_x509Cert);
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
                // Lay du lieu
                _userA0 = Convert.ToInt32(cboUserA0.SelectedValue);
                _slotID = Convert.ToInt32(cboSlotID.SelectedValue);
                string slotSerial = ((DataRowView)cboSlotID.SelectedItem)["Serial"].ToString();
                if (Label_Certificate == "")
                {
                    clsShare.Message_Error("Các trường có dấu '*' không được để trống.");
                    return;
                }

                // kiểm tra đã nhập thông tin chứng thư số
                if (_x509Cert == null)
                {
                    clsShare.Message_Error("Không có thông tin của chứng thư!");
                    return;
                }

                using (HSMServiceProvider hsm = new HSMServiceProvider(Common.CRYPTOKI))
                {
                    //Mở session nhưng ko đăng nhập
                    hsm.Login(slotSerial, HSMLoginRole.User, "1");
                    // tạo cert trong HSM
                    _CKA_ID = hsm.ImportCertificateFromX509(Label_Certificate, _x509Cert);
                }

                // trả về
                this.DialogResult = DialogResult.OK;
                this.Close();
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

        #endregion
    }
}
