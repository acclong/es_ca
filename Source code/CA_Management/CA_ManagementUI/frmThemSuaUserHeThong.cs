using ES.CA_ManagementBUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
using Telerik.WinControls.Layouts;
using Telerik.WinControls;
using esDigitalSignature.Library;

namespace ES.CA_ManagementUI
{
    public partial class frmThemSuaUserHeThong : Form
    {
        #region Var
        BUSQuanTri _bus = new BUSQuanTri();

        int _id_UserProg = -1;
        int _userID = -1;
        int _unitID = -1;
        int _progID = -1;
        string _userProgName = "";

        public int ID_UserProg
        {
            set { _id_UserProg = value; }
            get { return _id_UserProg; }
        }

        #endregion

        public frmThemSuaUserHeThong()
        {
            InitializeComponent();
        }

        private void frmThemSuaUserDonVi_Load(object sender, EventArgs e)
        {
            try
            {
                // Nếu sửa
                if (ID_UserProg != -1)
                {
                    this.Text = "Sửa liên kết người dùng - hệ thống";
                    //Lấy dữ liệu và đổ vào control
                    LoadData();
                    //Không cho sửa
                    btnSeachHeThong.Visible = false;
                    btnTimUser.Visible = false;
                }
                else
                {
                    this.Text = "Thêm liên kết người dùng - hệ thống";
                }

                this.ActiveControl = btnSeachHeThong;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region Init
        private void InitCboUserName()
        {
            if (ID_UserProg != -1)
            {
                DataTable dt = _bus.CA_UserProgram_SelectByUserProgID(ID_UserProg);

                _progID = Convert.ToInt32(dt.Rows[0]["ProgID"]);
                LoadUserDataFromProgram(_progID, "");
            }
        }
        #endregion

        #region Data
        private void LoadData()
        {
            // lấy dữ liệu User-Program từ database
            DataTable dtUserProgram = _bus.CA_UserProgram_Unit_SelectBy_IDUserProg(ID_UserProg);

            // Lưu thông tin vào biến
            _userID = Convert.ToInt32(dtUserProgram.Rows[0]["UserID"]);
            _progID = Convert.ToInt32(dtUserProgram.Rows[0]["ProgID"]);

            // Fill dữ liệu lên controls
            txtID_UserProg.Text = ID_UserProg.ToString();
            txtProg.Text = _bus.CA_Program_SelectByProgID(_progID).Rows[0]["Name"].ToString();
            txtUserName.Text = _bus.CA_User_SelectByUserID(_userID).Rows[0]["Name"].ToString();
            LoadUserDataFromProgram(_progID, dtUserProgram.Rows[0]["UserProgName"].ToString());

            // Ngày hiệu lực
            dpkValidFrom.Value = Convert.ToDateTime(dtUserProgram.Rows[0]["ValidFrom"].ToString());
            if (dtUserProgram.Rows[0]["ValidTo"] == DBNull.Value)
                chkVaildTo.Checked = false;
            else
            {
                dpkValidTo.Value = Convert.ToDateTime(dtUserProgram.Rows[0]["ValidTo"].ToString());
                chkVaildTo.Checked = true;
            }
        }

        //Lấy bảng User của Hệ thống tích hợp
        private void LoadUserDataFromProgram(int progID, string userProgNameNow)
        {
            //Tạo bảng User login
            DataTable dtUserProgName = new DataTable();
            DataColumn dc0 = new DataColumn("Login_ID", typeof(string));
            dtUserProgName.Columns.Add(dc0);
            DataColumn dc1 = new DataColumn("Login_Name", typeof(string));
            dtUserProgName.Columns.Add(dc1);

            //Lấy dữ liệu
            try
            {
                //Thông tin truy cập hệ thống
                DataTable dtProg = _bus.CA_Program_SelectByProgID(progID);
                string serverName = dtProg.Rows[0]["ServerName"].ToString();
                string dbName = dtProg.Rows[0]["DBName"].ToString();
                string userDB = dtProg.Rows[0]["UserDB"].ToString();
                string passwordDB = dtProg.Rows[0]["PasswordDB"].ToString();
                string queryUser = dtProg.Rows[0]["QueryUser"].ToString();

                if (serverName != "" && dbName != "" && userDB != "" && queryUser != "")
                {
                    //Lấy bảng dữ liệu
                    BUSQuanTri busProg = new BUSQuanTri(serverName, dbName, userDB, passwordDB);
                    dtUserProgName = busProg.HT_HeThong_SelectUser(queryUser);

                    //chèn tên cũ vào datatable
                    if (userProgNameNow != "")
                    {
                        bool isInList = false;
                        for (int i = 0; i < dtUserProgName.Rows.Count; i++)
                        {
                            if (dtUserProgName.Rows[i]["Login_ID"].ToString() == userProgNameNow)
                            {
                                cboUserProgName.DataSource = dtUserProgName;
                                cboUserProgName.DisplayMember = "Login_ID";
                                cboUserProgName.SelectedIndex = i;
                                isInList = true;
                                break;
                            }
                        }
                        if (!isInList)
                        {
                            DataRow dr = dtUserProgName.NewRow();
                            dr["Login_ID"] = userProgNameNow;
                            dr["Login_Name"] = userProgNameNow;

                            dtUserProgName.Rows.InsertAt(dr, 0);
                        }
                    }
                }
                else
                    throw new Exception("Không đủ thông tin kết nối.");
            }
            catch (Exception ex)
            {
                clsShare.Message_Warning("Không thể truy cập dữ liệu người dùng của hệ thống. Hãy kiểm tra lại thông tin kết nối!\n\n" + ex.Message);

                if (userProgNameNow != "")
                {
                    DataRow dr = dtUserProgName.NewRow();
                    dr["Login_ID"] = userProgNameNow;
                    dr["Login_Name"] = userProgNameNow;
                    dtUserProgName.Rows.InsertAt(dr, 0);
                }
            }

            //Đổ vào combo box
            //Toantk 19/8/2015: sửa lấy ID theo ValueMember
            cboUserProgName.DataSource = dtUserProgName;
            cboUserProgName.DisplayMember = "Login_Name";
            cboUserProgName.ValueMember = "Login_ID";
        }
        #endregion

        #region Controls
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region lấy giá trị biến tạm thời, kiểm tra dữ liệu đầu vào
                if (_userID == -1 || _progID == -1)
                {
                    clsShare.Message_Error("Các trường có dấu * không được phép để trống!");
                    this.ActiveControl = btnSeachHeThong;
                    return;
                }

                _userProgName = cboUserProgName.SelectedValue == null ? cboUserProgName.Text : cboUserProgName.SelectedValue.ToString();
                DateTime validFrom = dpkValidFrom.Value;
                DateTime validTo = chkVaildTo.Checked ? dpkValidTo.Value : DateTime.MaxValue;

                if (chkVaildTo.Checked && dpkValidFrom.Value != null)
                    if (DateTime.Compare(validTo, validFrom) == -1)
                    {
                        clsShare.Message_Error("Ngày hết hiệu lực không được nhỏ hơn Ngày có hiệu lực!");
                        this.ActiveControl = btnSeachHeThong;
                        return;
                    }
                #endregion

                #region kiểm tra ràng buộc
                // Kiểm tra người dùng đã liên kết với hệ thống hay chưa
                DataTable dtUserProgram = _bus.CA_UserProgram_SelectByUserID_ProgID(_userID, _progID);
                if (ID_UserProg == -1 & dtUserProgram.Rows.Count > 0)
                {
                    clsShare.Message_Error("Người dùng đã đăng kí tài khoản liên kết với hệ thống!");
                    return;
                }

                // Kiểm tra ràng buộc với userProg
                // Lấy đơn vị của người dùng
                DataTable dt = _bus.CA_User_SelectByUserID(_userID);
                int userunit = Convert.ToInt32(dt.Rows[0]["UnitID"]);

                if (userunit == 1)  // Check người dùng A0
                {
                    DataTable dt_userprog = _bus.CA_UserProgram_SelectBy_UserProgName_ProgID(_userProgName, _progID);
                    if (dt_userprog.Rows.Count > 0)     //Nếu đã có bản ghi của userprog
                    {
                        if (ID_UserProg == -1)      //Thêm
                        {
                            clsShare.Message_Error("Tài khoản đăng nhập đã được sử dụng trong hệ thống!");
                            return;
                        }
                        //Nếu sửa thì check user của bản ghi đã có khác với user đang sửa ko?
                        else if (Convert.ToInt32(dt_userprog.Rows[0]["UserID"]) != _userID)
                        {
                            clsShare.Message_Error("Tài khoản đăng nhập đã được sử dụng trong hệ thống!");
                            return;
                        }
                    }
                }
                #endregion

                // lưu vào update DB
                if (_bus.CA_UserProgram_InsertUpdate(ID_UserProg, _userID, _progID, _userProgName, validFrom, validTo, clsShare.sUserName) != -1)
                {
                    _bus.CA_UyQuyen_QuyenUnit_ResetBy_UserUyQuyenID_ProgID(_userID, _progID);
                    clsShare.Message_Info("Cập nhật liên kết người dùng - hệ thống thành công!");
                }
                else
                    clsShare.Message_Error("Cập nhật liên kết người dùng - hệ thống thất bại!");

                //Định dạng lại các controls
                if (ID_UserProg == -1)
                {
                    txtID_UserProg.Clear();
                    txtProg.Clear();
                    txtUserName.Clear();

                    if (cboUserProgName.Items.Count > 0) cboUserProgName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnTimUser_Click(object sender, EventArgs e)
        {
            frmLocNguoiDung frm = new frmLocNguoiDung(this);
            frm.ShowDialog();
        }

        private void btnSeachHeThong_Click(object sender, EventArgs e)
        {
            try
            {
                frmLocHeThongTichHop frm = new frmLocHeThongTichHop(this);
                frm.ShowDialog();
                LoadUserDataFromProgram(_progID, "");
            }
            catch
            {
            }
        }

        private void chkVaildTo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVaildTo.Checked)
                dpkValidTo.Visible = true;
            else
                dpkValidTo.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Active
        public void CapNhatDuLieuHeThong(string proName, int proID)
        {
            txtProg.Text = proName;
            _progID = proID;
        }

        public void CapNhatDuLieuUser(string username, int userID)
        {
            txtUserName.Text = username;
            _userID = userID;
        }

        public void CapNhatDuLieuUnit(string unitname, int unitID)
        {
            _unitID = unitID;
        }
        #endregion

    }
}
