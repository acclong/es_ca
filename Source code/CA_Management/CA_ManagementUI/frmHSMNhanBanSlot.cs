using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ES.CA_ManagementBUS;
using esDigitalSignature;

namespace ES.CA_ManagementUI
{
    public partial class frmHSMNhanBanSlot : Form
    {
        #region Var
        private BUSQuanTri _bus = new BUSQuanTri();

        private int _slotID_A = -1;
        private int _deviceID_A = -1;

        private DataTable _dtSlot, _dtDevice;

        public int DeviceID_Nguon
        {
            get { return Convert.ToInt32(cbbHSMNguon.SelectedValue); }
            set { _deviceID_A = value; }
        }

        public int SlotID_Nguon
        {
            get { return Convert.ToInt32(cbbSlotNguon.SelectedValue); }
            set { _slotID_A = value; }
            //get { return _slotID_A; }
            //set
            //{
            //    _slotID_A = value;
            //    DataTable dt_device = _bus.HSM_Slot_SelectBySlotID(_slotID_A);
            //    if (dt_device.Rows.Count > 0)
            //        _deviceID_A = Convert.ToInt32(dt_device.Rows[0]["DeviceID"]);
            //    else
            //        _deviceID_A = -1;
            //}
        }

        public int DeviceID_Dich
        {
            get { return Convert.ToInt32(cbbHSMDich.SelectedValue); }
        }

        public int SlotID_Dich
        {
            get { return Convert.ToInt32(cbbSlotDich.SelectedValue); }
        }

        #endregion

        public frmHSMNhanBanSlot()
        {
            InitializeComponent();
        }

        private void frmNhanBanSlotNew_Load(object sender, EventArgs e)
        {
            try
            {
                _dtSlot = _bus.HSM_Slot_SelectAll();

                InitCbbHSMNguonDich();
                InitCbbSlotNguon(DeviceID_Nguon);
                InitCbbSlotDich(DeviceID_Dich);

                cbbHSMNguon.SelectedIndexChanged += cbbHSMNguon_SelectedIndexChanged;
                cbbHSMDich.SelectedIndexChanged += cbbHSMDich_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        #region Init
        private void InitCbbHSMNguonDich()
        {
            _dtDevice = _bus.HSM_Device_SelectAll();

            cbbHSMNguon.DataSource = _dtDevice.Copy();
            cbbHSMNguon.DisplayMember = "Name";
            cbbHSMNguon.ValueMember = "DeviceID";
            cbbHSMNguon.SelectedValue = _deviceID_A;

            cbbHSMDich.DataSource = _dtDevice.Copy();
            cbbHSMDich.DisplayMember = "Name";
            cbbHSMDich.ValueMember = "DeviceID";
            cbbHSMDich.SelectedIndex = 0;
        }

        private void InitCbbSlotNguon(int deviceID)
        {
            _dtSlot.DefaultView.RowFilter = "DeviceID = " + deviceID.ToString();
            cbbSlotNguon.DataSource = _dtSlot.DefaultView.ToTable();
            cbbSlotNguon.DisplayMember = "TokenLabel";
            cbbSlotNguon.ValueMember = "SlotID";
            if (_slotID_A > 0)
                cbbSlotNguon.SelectedValue = _slotID_A;
            else
                cbbSlotNguon.SelectedIndex = 0;
        }

        private void InitCbbSlotDich(int deviceID)
        {
            _dtSlot.DefaultView.RowFilter = "DeviceID = " + deviceID.ToString();
            cbbSlotDich.DataSource = _dtSlot.DefaultView.ToTable();
            cbbSlotDich.DisplayMember = "TokenLabel";
            cbbSlotDich.ValueMember = "SlotID";
            cbbSlotDich.SelectedIndex = 0;
        }
        #endregion

        #region Controls
        private void cbbHSMNguon_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                InitCbbSlotNguon(DeviceID_Nguon);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void cbbHSMDich_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                InitCbbSlotDich(DeviceID_Dich);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnReplication_Click(object sender, EventArgs e)
        {
            try
            {
                //Lấy thông tin
                DataRow drSlotNguon = _dtSlot.Select("SlotID = " + SlotID_Nguon)[0];
                DataRow drSlotDich = _dtSlot.Select("SlotID = " + SlotID_Dich)[0];
                DataRow drDeviceDich = _dtDevice.Select("DeviceID = " + DeviceID_Dich)[0];

                // kiem tra SOPIN va USERPIN khác nhau => đã su dung
                if (drSlotNguon["User_PIN_V"].ToString() != drSlotNguon["SO_PIN_V"].ToString())
                {
                    clsShare.Message_Error("Không thể nhân bản do Slot đã được người dùng sử dụng!");
                    return;
                }

                // kiem tra khac HSM
                if (cbbHSMDich.Text == cbbHSMNguon.Text)
                {
                    clsShare.Message_Error("Không thể nhân bản Slot trên cùng HSM!");
                    return;
                }
                
                // kiem tra token label giong nhau
                if (cbbSlotNguon.Text != cbbSlotDich.Text)
                {
                    clsShare.Message_Error("Slot nguồn và slot đích phải có token label giống nhau. Hãy kiểm tra lại!");
                    return;
                }

                // kiem tra A, B trung pass
                if (drSlotNguon["User_PIN_V"].ToString() != drSlotDich["User_PIN_V"].ToString()
                    || drSlotNguon["SO_PIN_V"].ToString() != drSlotDich["SO_PIN_V"].ToString())
                {
                    clsShare.Message_Error("Slot nguồn và slot đích phải có mã PIN giống nhau. Hãy kiểm tra lại!");
                    return;
                }

                // kiem tra B da co object chua
                DataTable dt_object_B = _bus.HSM_Object_SelectBySlotID(SlotID_Dich);
                if (dt_object_B.Rows.Count > 0)
                {
                    clsShare.Message_Info("Slot đích phải rỗng. Hãy kiểm tra lại!");
                    return;
                }

                //Xử lý điều khiển HSM ở đây
                using (HSMServiceProvider hsm = new HSMServiceProvider(Common.CRYPTOKI))
                {
                    string srcSlotSerial = drSlotNguon["Serial"].ToString();
                    string destSlotSerial = drSlotDich["Serial"].ToString();
                    string userPin = ESLogin.StringCryptor.DecryptString(drSlotDich["User_PIN_V"].ToString());
                    string destHsmSerial = drDeviceDich["Serial"].ToString();

                    hsm.ReplicateToken(srcSlotSerial, destSlotSerial, userPin, destHsmSerial);
                }

                //Trả về thành công
                this.DialogResult = DialogResult.OK;
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
        #endregion

    }
}