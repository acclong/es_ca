using ES.CA_ManagementBUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ES.CA_ManagementUI
{
    public partial class frmThemSuaHeThongTichHop : Form
    {
        #region Var
        
        private int iProgID;
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtProgram = new DataTable();

        public int _iProgID
        {
            set { iProgID = value; }
            get { return iProgID; }
        }

        #endregion

        public frmThemSuaHeThongTichHop()
        {
            InitializeComponent();
        }

        private void frmThemSuaHeThongTichHop_Load(object sender, EventArgs e)
        {
            try
            {
                InitcboStatus();
                if (_iProgID != -1)
                {
                    this.Text = "Sửa hệ thống tích hợp";

                    // lấy dữ liệu từ database load lên form
                    _dtProgram = _bus.CA_Program_SelectByProgID(_iProgID);

                    txtProgID.Text = _iProgID.ToString();
                    txtProgName.Text = _dtProgram.Rows[0]["ProgName"].ToString();
                    txtName.Text = _dtProgram.Rows[0]["Name"].ToString();
                    txtNotation.Text = _dtProgram.Rows[0]["Notation"].ToString();
                    cboStatus.SelectedIndex = Convert.ToInt32(_dtProgram.Rows[0]["Status"]);
                    txtServerName.Text = _dtProgram.Rows[0]["ServerName"].ToString();
                    txtDBName.Text = _dtProgram.Rows[0]["DBName"].ToString();
                    txtUserName.Text = _dtProgram.Rows[0]["UserDB"].ToString();
                    txtPass.Text = _dtProgram.Rows[0]["PasswordDB"].ToString();
                    txtQueryUser.Text = _dtProgram.Rows[0]["QueryUser"].ToString();
                }
                else
                {
                    this.Text = "Thêm hệ thống tích hợp chứng thư số";
                    cboStatus.SelectedIndex = 1;
                    txtProgID.Text = "";
                }
                this.ActiveControl = txtProgName;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region Init
        private void InitcboStatus()
        {
            string[] array = { "Không hiệu lực", "Hiệu lực" };
            cboStatus.Items.AddRange(array);
        }

        #endregion

        #region Controls
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị biến tạm thời
                string sProgName = txtProgName.Text.Trim();
                string sName = txtName.Text.Trim();
                string sNotation = txtNotation.Text.Trim();
                int iStatus = cboStatus.SelectedIndex;
                string sServerName = txtServerName.Text.Trim();
                string sDBName = txtDBName.Text.Trim();
                string sUserDB = txtUserName.Text.Trim();
                string sPassword = txtPass.Text.Trim();
                string sQueryUser = txtQueryUser.Text.Trim();
                string sUserModified = clsShare.sUserName;

                // Kiểm tra dữ liệu đầu vào
                if (sProgName == "" || sName == "" || sServerName == "" || sDBName == "" || sUserDB == "" || sPassword == "" || iStatus == -1 || sNotation == "")
                {
                    clsShare.Message_Error("Các trường có dấu * không được phép để trống!");
                    this.ActiveControl = txtProgName;
                    return;
                }

                // Xác nhận thông tin thu hồi hiệu lực hệ thống
                if (iStatus == 0)
                    if (clsShare.Message_WarningYN("Bạn có chắc chắn xác nhận KHÔNG HIỆU LỰC cho hệ thống này không?") == false)
                    {
                        this.ActiveControl = txtProgName;
                        return;
                    }

                // kiểm tra ProgName
                //Edited by Toantk on 23/4/2015
                //Chuyển hàm lấy mã để kiểm tra vào Business
                int progID = _bus.CA_Program_HasProgName(sProgName);
                if (_iProgID == -1 && progID != 0)
                {
                    clsShare.Message_Error("Mã hệ thống đã tồn tại. Vui lòng nhập lại Mã hệ thống!");
                    this.ActiveControl = txtProgName;
                    return;
                }
                else if (_iProgID != -1 && progID != 0)
                {
                    if (progID != _iProgID)
                    {
                        clsShare.Message_Error("Mã hệ thống đã tồn tại. Vui lòng nhập lại Mã hệ thống!");
                        this.ActiveControl = txtProgName;
                        return;
                    }
                }

                // kiểm tra Notation
                //Edited by Toantk on 23/4/2015
                //Chuyển hàm lấy mã để kiểm tra vào Business
                progID = _bus.CA_Program_HasNotation(sNotation);
                if (_iProgID == -1 && progID != 0)
                {
                    clsShare.Message_Error("Ký hiệu đã tồn tại. Vui lòng nhập lại Ký hiệu!");
                    this.ActiveControl = txtProgName;
                    return;
                }
                else if (_iProgID != -1 && progID != 0)
                {
                    if (progID != _iProgID)
                    {
                        clsShare.Message_Error("Ký hiệu đã tồn tại. Vui lòng nhập lại Ký hiệu!");
                        this.ActiveControl = txtProgName;
                        return;
                    }
                }

                // insert, update vào cơ sở dữ liệu
                _bus.CA_Program_InsertUpdate(_iProgID, sProgName, sName, sNotation, iStatus, sServerName, sDBName, sUserDB,
                    sPassword, sQueryUser, sUserModified);
                
                // Thông báo kết quả thành công
                clsShare.Message_Info("Cập nhật hệ thống thành công!");

                this.ActiveControl = txtProgName;


                if (iProgID == -1)
                {
                    txtProgName.Clear();
                    txtName.Clear();
                    txtNotation.Clear();
                    cboStatus.SelectedIndex = 0;
                    txtServerName.Clear();
                    txtDBName.Clear();
                    txtUserName.Clear();
                    txtPass.Clear();
                    txtQueryUser.Clear();
                }

            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Event
        
        private void frmThemSuaHeThongTichHop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnExit_Click(null, null);
            }
        }

        #endregion

    }
}
