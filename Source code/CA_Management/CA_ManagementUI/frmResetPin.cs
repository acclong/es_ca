using ES.CA_ManagementBUS;
using esDigitalSignature;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESLogin;

namespace ES.CA_ManagementUI
{
    public partial class frmResetPin : Form
    {
        private BUSQuanTri _bus = new BUSQuanTri();
        private HSMReturnValue _eResult = HSMReturnValue.FUNCTION_FAILED;
        public HSMServiceProvider _serviceProvider;
        public HSMLoginRole _role;
        public int _iDeviceID;
        public string _strSerial;

        public frmResetPin(HSMLoginRole roleLogin, int deviceID, string serial)
        {
            InitializeComponent();
            _iDeviceID = deviceID;
            _role = roleLogin;
            _strSerial = serial;
            chkDB.Checked = true;
            chkHSM.Checked = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // khai báo biến tạm thời                
                string userPin = txtUserPin.Text;
                string confirmUserPin = txtConfirmUserPin.Text;
                string userPinCry = StringCryptor.EncryptString(userPin);

                DataTable dt = _bus.HSM_Slot_SelectBySlotSerial_DeviceID(_strSerial, _iDeviceID);
                string userPinOld = StringCryptor.DecryptString(dt.Rows[0]["User_PIN"].ToString());

                // kiểm tra nơi lưu trữ
                if(!chkDB.Checked && !chkHSM.Checked)
                {
                    clsShare.Message_Error("Cần phải chọn ít nhất 1 nơi lưu trữ!");
                    return;
                }

                // Kiểm tra Mã PIN
                string[] arrayString = {userPin};
                if (!CheckPass(arrayString))
                {
                    clsShare.Message_Error("Mã PIN chỉ chứa ký tự không dấu và độ dài từ 4 đến 32 ký tự.\nHãy kiểm tra lại!");
                    return;
                }
                if (userPin != confirmUserPin)
                {
                    clsShare.Message_Error("Mã PIN và xác nhận Mã PIN phải trùng nhau.\nHãy kiểm tra lại!");
                    return;
                }
                
                // Thiết lập lại mã PIN mới trong HSM
                if (chkHSM.Checked)
                {
                    _serviceProvider = new HSMServiceProvider(clsShare.CRYPTOKI);
                    _serviceProvider.Login(_strSerial, _role, userPinOld);
                    _eResult = _serviceProvider.ChangeSlotPIN(userPinOld, userPin);

                    if (_eResult == HSMReturnValue.OK)
                        clsShare.Message_Info("Thiết lập lại mã PIN trong HSM thành công!");
                    else
                    {
                        clsShare.Message_Error("Thiết lập lại mã PIN không thành công!");
                        return;
                    }
                }
                if (chkDB.Checked)
                {
                    try
                    {
                        int slotID = Convert.ToInt32(dt.Rows[0]["SlotID"]);
                        int type = Convert.ToInt32(dt.Rows[0]["Type"]);
                        bool initToken = Convert.ToBoolean(dt.Rows[0]["InitToken"]);
                        string tokenLabel = dt.Rows[0]["TokenLabel"].ToString();
                        string soPin = dt.Rows[0]["SO_PIN"].ToString();
                        // HieuTM: comment tạm thời do thêm trường SlotIndex trong DB
                        //_bus.HSM_Slot_InsertUpdate(slotID, _iDeviceID, _strSerial, type, initToken, tokenLabel, soPin, userPinCry, clsShare.sUserName);
                        clsShare.Message_Info("Thiết lập lại mã PIN trong cơ sở dữ liệu thành công!");
                    }
                    catch
                    {
                        clsShare.Message_Error("Thiết lập lại mã PIN trong cơ sở dữ liệu không thành công!");
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Lỗi khi thiết lập lại mã PIN!\n\n" + ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool CheckPass(string[] arrayPin)
        {
            char[] Pin;
            for (int i = 0; i < arrayPin.Length; i++)
            {
                if (arrayPin[i].Length < 4)
                    return false;
                Pin = arrayPin[i].ToCharArray();
                for (int j = 0; j < Pin.Length; j++)
                {
                    if (Pin[j] <= 127)
                        continue;
                    else
                        return false;
                }
            }
            return true;
        }
    }
}
