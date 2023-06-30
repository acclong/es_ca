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
using ES.CA_ManagementBUS;
using C1.Win.C1FlexGrid;

namespace ES.CA_ManagementUI
{
    public partial class frmThemSuaUyQuyen : Form
    {
        #region Var
        BUSQuanTri _bus = new BUSQuanTri();

        public bool bInsertUpdate = true;
        public int _id_UyQuyen = -1;
        public string _str_UserUyQuyen = "";
        public string _str_UserDuocUyQuyen = "";
        int _id_UserProg = -1;
        int _userID_UyQuyen = -1;
        int _userID_NhanUyQuyen = -1;
        int _unitID = -1;
        int _progID = -1;
        string _userProgName = "";
        DataTable _dtProg = new DataTable();
        DataTable _dtUserProg = new DataTable();
        DataTable _dtCfgUyQuyen = new DataTable();

        private List<int> UnitID = new List<int>();
        private List<string> UnitName = new List<string>();
        private List<string> UnitMaDV = new List<string>();
        private List<string> UnitNotation = new List<string>();
        private List<bool> UnitEnable = new List<bool>();
        private List<int> UnitSign = new List<int>();

        public int ID_UserProg
        {
            set { _id_UserProg = value; }
            get { return _id_UserProg; }
        }

        #endregion
        public frmThemSuaUyQuyen()
        {
            InitializeComponent();
        }

        private void frmThemSuaUyQuyen_Load(object sender, EventArgs e)
        {
            try
            {
                if (_id_UyQuyen == -1)
                {
                    cfgUyQuyen.Visible = false;
                }
                else
                {
                    cfgUyQuyen.Visible = true;
                    txtUyQuyen.Text = _str_UserUyQuyen;
                    txtNhanUyQuyen.Text = _str_UserDuocUyQuyen;
                    loadCfgDataUyQuyen();
                    InitcfgUyQuyen();
                }
                if (!bInsertUpdate)
                {
                    btnUyQuyen.Enabled = false;
                    btnNhanUyQuyen.Enabled = false;
                }
                else
                {
                    btnUyQuyen.Enabled = true;
                    btnNhanUyQuyen.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                //
            }
            finally
            {
                //
            }
        }

        private void btnUyQuyen_Click(object sender, EventArgs e)
        {
            frmLocNguoiDung frm = new frmLocNguoiDung(this);
            frm.bFrmUyQuyen = true;
            frm.bUyQuyen = true;
            frm.bNhanUyQuyen = false;
            frm.ShowDialog();
        }

        private void btnNhanUyQuyen_Click(object sender, EventArgs e)
        {
            frmLocNguoiDung frm = new frmLocNguoiDung(this);
            frm.bFrmUyQuyen = true;
            frm.bUyQuyen = false;
            frm.bNhanUyQuyen = true;
            frm.ShowDialog();
        }

        #region Active

        public void CapNhatDuLieuUserUyQuyen(string username, int userID)
        {
            txtUyQuyen.Text = username;
            _userID_UyQuyen = userID;
            //Lấy dữ liệu
            loadCfgDataUyQuyen();
        }

        public void CapNhatDuLieuUserNhanUyQuyen(string username, int userID)
        {
            txtNhanUyQuyen.Text = username;
            _userID_NhanUyQuyen = userID;
            if (!bInsertUpdate)
            {
                txtID_UyQuyen.Text = _id_UyQuyen.ToString();
            }
            loadCfgDataUyQuyen();
        }

        public void CapNhatDuLieuUnit(string unitname, int unitID)
        {
            _unitID = unitID;
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Lấy bảng chứa dữ liệu
            DataTable dtSave = new DataTable();
            List<int> lstErr = new List<int>();
            dtSave.Columns.Add("Checkbox", typeof(Boolean));
            dtSave.Columns.Add("UserProgName", typeof(string));
            dtSave.Columns.Add("ValidFrom", typeof(DateTime));
            dtSave.Columns.Add("ValidTo", typeof(DateTime));
            dtSave.Columns.Add("ProgID", typeof(int));
            for (int iCfg = 1; iCfg < cfgUyQuyen.Rows.Count; iCfg++)
            {
                DataRow dr = dtSave.NewRow();
                dr["Checkbox"] = cfgUyQuyen.Rows[iCfg]["Checkbox"];
                dr["UserProgName"] = cfgUyQuyen.Rows[iCfg]["UserProgName"];
                dr["ValidFrom"] = cfgUyQuyen.Rows[iCfg]["ValidFrom"];
                dr["ValidTo"] = cfgUyQuyen.Rows[iCfg]["ValidTo"];
                dr["ProgID"] = cfgUyQuyen.Rows[iCfg]["ProgID"];
                dtSave.Rows.Add(dr);
                dtSave.AcceptChanges();
            }
            if (_bus.CA_UyQuyen_InsertUpdate(bInsertUpdate, _id_UyQuyen, dtSave, _userID_UyQuyen, _userID_NhanUyQuyen, clsShare.sUserName, ref lstErr))
            {
                clsShare.Message_Info("Cập nhật thành công");
                this.Close();
            }
            else
            {
                clsShare.Message_Error("Người nhận đã được ủy quyền trong khoảng thời gian này");
                //Giữ form, focus rows sai để chỉnh sửa
                cfgUyQuyen.Clear(ClearFlags.Style);
                for (int iLst = 0; iLst < lstErr.Count; iLst++)
                {
                    cfgUyQuyen.Rows[iLst + 1].StyleNew.BackColor = Color.Red;
                }
                InitcfgUyQuyen();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboHeThong_Click(object sender, EventArgs e)
        {
            try
            {
                //Dữ liệu
                //Bảng danh sách hệ thống
                if (_userID_UyQuyen == -1)
                {
                    return;
                }
                _dtProg = _bus.CA_Program_SelectBy_UserID(_userID_UyQuyen);
            }
            catch (Exception ex)
            {

            }
        }

        private void cboUserProgName_Click(object sender, EventArgs e)
        {
            if (_userID_NhanUyQuyen == -1)
            {
                return;
            }
        }

        private void InitcfgUyQuyen()
        {
            try
            {
                // cấu hình cho cfg
                cfgUyQuyen.Cols.Fixed = 0;
                string[] arrName = new string[] { "CheckBox", "ProgName", "UserProgName", "ValidFrom", "ValidTo", "ProgID", "DuocUyQuyen_ID", "UyQuyen_ID" };
                string[] arrCaption = new string[] { "", "Hệ thống", "Tên đăng nhập", "Hiệu lực từ", "Hiệu lực đến", "ProgID", "DuocUyQuyen_ID", "UyQuyen_ID" };

                if (_id_UyQuyen != -1)
                {
                    cfgUyQuyen.Cols[0].Visible = false;
                }
                cfgUyQuyen.AllowEditing = true;
                for (int i = 0; i < arrName.Length; i++)
                {
                    cfgUyQuyen.Cols[i].Name = arrName[i];
                    cfgUyQuyen.Cols[i].Caption = arrCaption[i];
                    // căn lề
                    if (i == 0)
                    {
                        cfgUyQuyen.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                        cfgUyQuyen.Cols[i].AllowEditing = true;
                    }
                    else
                    {
                        if (i == 1)
                        {
                            cfgUyQuyen.Cols[1].AllowEditing = false;
                        }
                        else
                        {
                            cfgUyQuyen.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                            cfgUyQuyen.Cols[i].AllowEditing = true;
                            //
                            cfgUyQuyen.Cols[i].AllowFiltering = AllowFiltering.ByValue;
                            if (i > 4)
                            {
                                cfgUyQuyen.Cols[i].Visible = false;
                            }
                        }
                    }
                }
                // kích thước cột
                cfgUyQuyen.Cols[0].Width = 30;
                cfgUyQuyen.Cols[0].DataType = typeof(Boolean);
                cfgUyQuyen.Cols[1].Width = 100;
                cfgUyQuyen.Cols[2].Width = 100;
                cfgUyQuyen.Cols[3].Width = 160;
                cfgUyQuyen.Cols[3].DataType = typeof(DateTime);
                cfgUyQuyen.Cols[3].Format = "dd/MM/yyyy HH:mm:ss";
                cfgUyQuyen.Cols[4].Width = 160;
                cfgUyQuyen.Cols[4].DataType = typeof(DateTime);
                cfgUyQuyen.Cols[4].Format = "dd/MM/yyyy HH:mm:ss";
                // căn giừa hàng đầu
                cfgUyQuyen.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Lỗi trong quá trình lấy dữ liệu:\r\n" + ex.Message);
            }
        }

        private void loadCfgDataUyQuyen()
        {
            if (_id_UyQuyen == -1)
            {
                //Lấy dữ liệu
                if (_userID_UyQuyen == -1 || _userID_NhanUyQuyen == -1)
                {
                    cfgUyQuyen.Visible = false;
                }
                else
                {
                    cfgUyQuyen.Visible = true;
                    //Đổ dữ liệu
                    _dtCfgUyQuyen = _bus.CA_UserProgram_SelectBy_IDUyQuyen_IDDuocUyQuyen(_id_UyQuyen, _userID_UyQuyen, _userID_NhanUyQuyen);
                    if (_dtCfgUyQuyen.Rows.Count > 0)
                    {
                        cfgUyQuyen.DataSource = _dtCfgUyQuyen;
                    }
                    InitcfgUyQuyen();
                }
            }
            else
            {
                cfgUyQuyen.Visible = true;
                //Đổ dữ liệu
                _dtCfgUyQuyen = _bus.CA_UserProgram_SelectBy_IDUyQuyen_IDDuocUyQuyen(_id_UyQuyen, _userID_UyQuyen, _userID_NhanUyQuyen);
                if (_dtCfgUyQuyen.Rows.Count > 0)
                {
                    cfgUyQuyen.DataSource = _dtCfgUyQuyen;
                    _userID_UyQuyen = int.Parse(_dtCfgUyQuyen.Rows[0]["UyQuyen_ID"].ToString());
                    _userID_NhanUyQuyen = int.Parse(_dtCfgUyQuyen.Rows[0]["DuocUyQuyen_ID"].ToString());
                }
                InitcfgUyQuyen();
            }
        }
    }
}
