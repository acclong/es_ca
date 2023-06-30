using esDigitalSignature;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ES.CA_ManagementBUS;

namespace ES.CA_ManagementUI
{
    public partial class frmHSMTaoCapKhoaRSA : Form
    {
        #region Var
        BUSQuanTri _bus = new BUSQuanTri();

        private DataTable _dtSlot;
        private byte[] _CKA_ID;
        private int _slotID = -1;

        public int SlotID
        {
            get { return _slotID; }
            set { _slotID = value; }
        }

        public string Subject
        {
            get { return txtSubject.Text; }
            set { txtSubject.Text = value; }
        }

        public string Label_KeyPUB
        {
            get { return txtPKU.Text; }
            set { txtPKU.Text = value; }
        }

        public string Label_KeyPRV
        {
            get { return txtPKI.Text; }
            set { txtPKI.Text = value; }
        }

        public string Label_CertRequest
        {
            get { return txtRequest.Text; }
            set { txtRequest.Text = value; }
        }

        public byte[] CKA_ID
        {
            get { return _CKA_ID; }
            set { _CKA_ID = value; }
        }
        #endregion

        public frmHSMTaoCapKhoaRSA()
        {
            InitializeComponent();
        }

        private void frmTaoCapKhoaRSA_Load(object sender, EventArgs e)
        {
            InitCboKeyType();
            InitCboSlotID();
        }

        #region Init
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

        private void InitCboKeyType()
        {
            clsShare.ComboboxItem item1 = new clsShare.ComboboxItem();
            item1.Text = HSMKeyPairType.RSA.ToString();
            item1.Value = HSMKeyPairType.RSA;
            cboKeyType.Items.Add(item1);
            clsShare.ComboboxItem item2 = new clsShare.ComboboxItem();
            item2.Text = HSMKeyPairType.DSA.ToString();
            item2.Value = HSMKeyPairType.DSA;
            cboKeyType.Items.Add(item2);

            cboKeyType.SelectedIndex = 0;
        }
        #endregion

        #region Controls
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Lay du lieu
                _slotID = Convert.ToInt32(cboSlotID.SelectedValue);
                string slotSerial = ((DataRowView)cboSlotID.SelectedItem)["Serial"].ToString();
                int keyType = Convert.ToInt32(cboKeyType.SelectedValue);

                if (Label_KeyPUB == "" || Label_KeyPRV == "" || Subject == "" || Label_CertRequest == "")
                {
                    clsShare.Message_Error("Các trường có dấu '*' không được để trống.");
                    return;
                }

                // Xử lý HSM trả về CKA_ID ở đây
                //Gọi form nhập pass
                frmHSMInputPassword frm = new frmHSMInputPassword("Hãy nhập mã User PIN của slot " + cboSlotID.Text);
                frm.DefaultPassword = ((DataRowView)cboSlotID.SelectedItem)["SO_PIN"].ToString();
                if (frm.ShowDialog() == DialogResult.Cancel)
                    return;
                string pin = frm.InputPassword;

                //Thêm vào thiết bị HSM
                using (HSMServiceProvider hsm = new HSMServiceProvider(Common.CRYPTOKI))
                {
                    //Login Admin
                    HSMReturnValue eResultLogin = hsm.Login(slotSerial, HSMLoginRole.User, pin);
                    if (eResultLogin == HSMReturnValue.OK)
                    {
                        //Tạo slot và lấy serial
                        CKA_ID = hsm.GenerateKeyPairAndRequest(HSMKeyPairType.RSA, Subject, Label_KeyPUB, Label_KeyPRV, Label_CertRequest);
                    }
                    else if (eResultLogin == HSMReturnValue.PIN_INCORRECT)
                    {
                        clsShare.Message_Error("Sai mã PIN thiết bị!");
                        return;
                    }
                    else
                        throw new Exception(eResultLogin.ToString());
                }

                // trả về
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
