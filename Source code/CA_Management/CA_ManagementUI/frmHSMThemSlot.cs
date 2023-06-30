using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using esDigitalSignature;
using ES.CA_ManagementBUS;
using ESLogin;

namespace ES.CA_ManagementUI
{
    public partial class frmHSMThemSlot : Form
    {
        #region var
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtHSM = new DataTable();

        private string _SlotSerial;

        public int DeviceID
        {
            get { return Convert.ToInt32(cboHSM.SelectedValue); }
        }

        public string SlotSerial
        {
            get { return _SlotSerial; }
        }

        public string TokenLabel
        {
            get { return txtLabel.Text; }
        }

        public string SOPIN
        {
            get { return txtSOPIN.Text; }
        }

        public string UserPIN
        {
            get { return txtUserPIN.Text; }
        }
        #endregion

        public frmHSMThemSlot()
        {
            InitializeComponent();
        }

        private void frmThemSlot_Load(object sender, EventArgs e)
        {
            try
            {
                // lấy danh sách các thiết bị sẵn có, check button UserPin + uncheck SOPin
                _dtHSM = _bus.HSM_Device_SelectAll();
                cboHSMLoad();
                //Disable trường mã PIN
                txtSOPIN.Enabled = false;
                txtConfirmSOPin.Enabled = false;
                txtUserPIN.Enabled = false;
                txtConfirmUserPin.Enabled = false;
                // Dùng mã pin mặc định
                //string tmp = StringCryptor.EncryptString("abc@123");
                string defaultPIN = StringCryptor.DecryptString(_bus.Q_CONFIG_GetPINDefault());
                txtSOPIN.Text = defaultPIN;
                txtConfirmSOPin.Text = defaultPIN;
                txtUserPIN.Text = defaultPIN;
                txtConfirmUserPin.Text = defaultPIN;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cboHSMLoad()
        {
            cboHSM.DataSource = _dtHSM;
            cboHSM.ValueMember = "DeviceID";
            cboHSM.DisplayMember = "Name";
            cboHSM.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Edited by Ninhtq on 5/7/2015
                // khai báo biến tạm thời
                string tokenLabel = txtLabel.Text;
                string soPin = txtSOPIN.Text;
                string confirmSoPin = txtConfirmSOPin.Text;
                string userPin = txtUserPIN.Text;
                string confirmUserPin = txtConfirmUserPin.Text;

                // kiểm tra đúng chuẩn ký tự
                if (!clsShare.CheckStringHSM(tokenLabel, 1, 50))
                {
                    clsShare.Message_Error("Tên Token Label chỉ chứa ký tự không dấu và độ dài từ 1 đến 50 ký tự.\nHãy kiểm tra lại!");
                    return;
                }

                //Toantk 23/8/2015: Bỏ kiểm tra trùng tên slot
                //// Thêm kiểm tra tên token label khi thêm mới
                //DataTable dt = _bus.HSM_Slot_SelectByTokenLabel(tokenLabel);
                //if(dt.Rows.Count != 0)
                //{
                //    clsShare.Message_Error("Tên Token Label đã tồn tại.\nHãy kiểm tra lại!");
                //    return;
                //}

                // Kiểm tra Mã PIN
                if (!clsShare.CheckStringHSM(soPin, 4, 32) || !clsShare.CheckStringHSM(userPin, 4, 32))
                {
                    clsShare.Message_Error("Mã PIN chỉ chứa ký tự không dấu và độ dài từ 4 đến 32 ký tự.\nHãy kiểm tra lại!");
                    return;
                }
                if (soPin != confirmSoPin || userPin != confirmUserPin)
                {
                    clsShare.Message_Error("Mã PIN và xác nhận Mã PIN phải trùng nhau.\nHãy kiểm tra lại!");
                    return;
                }

                //Gọi form nhập pass
                frmHSMInputPassword frm = new frmHSMInputPassword("Hãy nhập mã User PIN của thiết bị " + cboHSM.Text);
                if (frm.ShowDialog() == DialogResult.Cancel)
                    return;
                string pin = frm.InputPassword;

                //Thêm vào thiết bị HSM
                using (HSMServiceProvider hsm = new HSMServiceProvider(Common.CRYPTOKI))
                {
                    //Login Admin
                    HSMReturnValue eResultLogin = hsm.LoginAdmin(DeviceID, HSMLoginRole.User, pin);
                    if (eResultLogin == HSMReturnValue.OK)
                    {
                        //Tạo slot và lấy serial
                        _SlotSerial = hsm.CreateSlot(tokenLabel);
                    }
                    else if (eResultLogin == HSMReturnValue.PIN_INCORRECT)
                    {
                        clsShare.Message_Error("Sai mã PIN thiết bị!");
                        return;
                    }
                    else
                        throw new Exception(eResultLogin.ToString());
                }
                using (HSMServiceProvider hsm = new HSMServiceProvider(Common.CRYPTOKI))
                {
                    //Login Admin
                    hsm.LoginAdmin(DeviceID, HSMLoginRole.User, pin);
                    //Khởi tạo token
                    hsm.InitToken(_SlotSerial, tokenLabel, soPin, userPin);
                    //
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Lỗi khi tạo Slot trên HSM!\n\n" + ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
