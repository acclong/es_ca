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
using ESLogin;

namespace ES.CA_ManagementUI
{
    public partial class frmHSMDoiPassAdmin : Form
    {
        #region Var
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtHSM = new DataTable();

        public int DeviceID
        {
            get { return Convert.ToInt32(cboHSM.SelectedValue); }
        }

        public HSMLoginRole Role
        {
            get
            {
                //Loại đăng nhập
                return rbSOPIN.Checked ? HSMLoginRole.SecurityOfficer : HSMLoginRole.User;
            }
        }

        public string OldPass
        {
            get { return txtOldPIN.Text; }
        }

        public string NewPass1
        {
            get { return txtNewPIN.Text; }
        }

        public string NewPass2
        {
            get { return txtRePIN.Text; }
        }
        #endregion

        public frmHSMDoiPassAdmin()
        {
            InitializeComponent();
        }

        private void frmHSMDoiPassAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                // lấy danh sách các thiết bị sẵn có, check button UserPin + uncheck SOPin
                _dtHSM = _bus.HSM_Device_SelectAll();
                cboHSMLoad();
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // kiểm tra pass đầu vào
                if (!clsShare.CheckStringHSM(OldPass, 4, 32) || !clsShare.CheckStringHSM(NewPass1, 4, 32))
                {
                    clsShare.Message_Error("Mã PIN chỉ chứa ký tự không dấu và độ dài từ 4 đến 32 ký tự.\nHãy kiểm tra lại!");
                    return;
                }
                if (!NewPass1.Equals(NewPass2))
                {
                    clsShare.Message_Error("Mã PIN và xác nhận Mã PIN phải trùng nhau.\nHãy kiểm tra lại!");
                    return;
                }

                //Đổi mật khẩu trên thiết bị
                using (HSMServiceProvider hsm = new HSMServiceProvider(clsShare.CRYPTOKI))
                {
                    HSMReturnValue eResultLogin = hsm.LoginAdmin(DeviceID, Role, OldPass);
                    //Login thành công
                    if (eResultLogin == HSMReturnValue.OK)
                    {
                        // thay đổi mật khẩu trên thiết bị HSM
                        HSMReturnValue eResult = hsm.ChangeAdminPIN(OldPass, NewPass1);

                        if (eResult == HSMReturnValue.OK)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                            throw new Exception(eResult.ToString());
                    }
                    else if (eResultLogin == HSMReturnValue.PIN_INCORRECT)
                    {
                        clsShare.Message_Error("Sai mã PIN cũ!");
                        return;
                    }
                    else
                        throw new Exception(eResultLogin.ToString());
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Lỗi khi thay đổi mật khẩu HSM!\n\n" + ex.Message);
            }
            finally
            {
                txtOldPIN.Select();
            }
        }
    }
}
