using ES.CA_ManagementBUS;
using esDigitalSignature.Library;
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
    public partial class frmBidding_CapNhatUser : Form
    {
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable dtUser = new DataTable();

        public frmBidding_CapNhatUser()
        {
            InitializeComponent();
        }

        private void frmBidding_CapNhatUser_Load(object sender, EventArgs e)
        {
            cfgData.AllowAddNew = true;
            cfgData.AllowEditing = true;
            LoadUserDataFromProgram((int)ProgName.BiddingServer);
        }

        //Lấy bảng User của Hệ thống tích hợp
        private void LoadUserDataFromProgram(int progID)
        {
            //Lấy dữ liệu
            try
            {
                //Thông tin truy cập hệ thống
                DataTable dtProg = _bus.CA_Program_SelectByProgID(progID);
                string serverName = dtProg.Rows[0]["ServerName"].ToString();
                string dbName = dtProg.Rows[0]["DBName"].ToString();
                string userDB = dtProg.Rows[0]["UserDB"].ToString();
                string passwordDB = dtProg.Rows[0]["PasswordDB"].ToString();
                string queryUser = "SELECT Ma_DVPD, UserName FROM CA_Unit_User ORDER BY Ma_DVPD";//dtProg.Rows[0]["QueryUser"].ToString();

                if (serverName != "" && dbName != "" && userDB != "" && queryUser != "")
                {
                    //Lấy bảng dữ liệu
                    _bus = new BUSQuanTri(serverName, dbName, userDB, passwordDB);
                    dtUser = _bus.HT_HeThong_SelectUser(queryUser);
                    //Đổ vào grid
                    cfgData.DataSource = dtUser;
                }
                else
                    throw new Exception("Không đủ thông tin kết nối.");
            }
            catch (Exception ex)
            {
                clsShare.Message_Warning("Không thể truy cập dữ liệu người dùng của hệ thống. Hãy kiểm tra lại thông tin kết nối!\n\n" + ex.Message);
            }            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _bus.HT_BiddingServer_CA_Unit_User_Update(dtUser);
                clsShare.Message_Info("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                clsShare.Message_Warning("Lỗi cập nhật!\n\n" + ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
