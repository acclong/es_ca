using C1.Win.C1FlexGrid;
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
    public partial class frmThemSuaUserHeThongNew : Form
    {
        public frmThemSuaUserHeThongNew()
        {
            InitializeComponent();
        }

        private void frmThemSuaUserHeThongNew_Load(object sender, EventArgs e)
        {
            DataTable dtProg = _bus.CA_Program_SelectAll();

            if (dtProg != null)
            {
                int n = dtProg.Rows.Count;
                _progIDFull = new int[n];
                _progNameFull = new string[n];

                int i = 0;

                foreach (DataRow item in dtProg.Rows)
                {
                    _progNameFull[i] = item["Name"].ToString();
                    _progIDFull[i] = Convert.ToInt32(item["ProgID"]);
                    i++;
                }
            }

            cfgProg.DataSource = _bus.CA_UserProg_SelectBy_UserID(_userID);
            InitCfgProg();

            //LoadData();
            //LoadDataUnit(-1, "");

            //InitCfgProg();
            //InitCfgDonVi();
        }

        #region Var

        CA_ManagementBUS.BUSQuanTri _bus = new CA_ManagementBUS.BUSQuanTri();

        int _indexRowBefore = -1;
        int _indexRowNow = -1;

        int[] _progIDFull;
        string[] _progNameFull;

        private int _userID = -1;
        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        private List<int> _progID = new List<int>();
        private List<List<int>> _unitID = new List<List<int>>();
        private List<List<int>> _unitSign = new List<List<int>>();

        #endregion

        #region Init

        private void InitCfgProg()
        {
            string[] arrName = { "ID_UserProg", "UserID", "UserName", "ProgID", "ProgName", "UserProgName", "ValidFrom", "ValidTo" };
            string[] arrHeader = { "ID_UserProg", "UserID", "UserName", "ProgID", "Tên hệ thống", "Tên đăng nhập", "Ngày có hiệu lực", "Ngày hết hiệu lực" };

            try
            {
                cfgProg.Clear();

                #region C1

                for (int i = 0; i < arrHeader.Count(); i++)
                {
                    cfgProg.Cols[i + 1].Name = arrName[i];
                    cfgProg.Cols[i + 1].Caption = arrHeader[i];

                    // căn lề
                    switch (arrName[i])
                    {
                        case "ValidFrom":
                        case "ValidTo":
                            cfgProg.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                            break;
                        default:
                            cfgProg.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                            break;
                    }

                    // ẩn các cột
                    switch (arrName[i])
                    {
                        case "UserName":
                        case "ID_UserProg":
                        case "ProgID":
                        case "UserID":
                            cfgProg.Cols[i + 1].Visible = false;
                            break;
                    }

                    // tạo Filter và định dạng ngày tháng
                    switch (arrName[i])
                    {
                        case "ValidFrom":
                        case "ValidTo":
                            cfgProg.Cols[i + 1].AllowFiltering = AllowFiltering.ByCondition;
                            cfgProg.Cols[i + 1].Format = "dd/MM/yyyy";
                            break;
                        default:
                            cfgProg.Cols[i + 1].AllowFiltering = AllowFiltering.Default;
                            break;
                    }
                }

                #endregion

                cfgProg.Cols["ProgName"].Width = 200;
                cfgProg.Cols["UserProgName"].Width = 100;
                cfgProg.Cols["ValidFrom"].Width = 125;
                cfgProg.Cols["ValidTo"].Width = 125;

                DataTable dtProg = _bus.CA_Program_SelectAll();

                //// Format col Style cho Date
                //CellStyle cs = cfgProg.Styles.Add("dateFrom");
                //cs.DataType = typeof(DateTime);
                //cs.UserData = DateTime.Now;
                //cs.Format = "dd-MM-yyyy HH:mm:ss";

                //// Format col Style cho Date
                //cs = cfgProg.Styles.Add("dateTo");
                //cs.DataType = typeof(DateTime);
                //cs.Format = "dd-MM-yyyy HH:mm:ss";


                //cs = cfgProg.Styles.Add("Prog");
                //cs.DataType = typeof(string);
                //string progstr = "";

                //if (_progIDFull != null)
                //{
                //    for (int i = 0; i < _progIDFull.Count(); i++)
                //    {
                //        if (_progID.Count > 0)
                //        {
                //            if (_progID.IndexOf(_progIDFull[i]) != -1)
                //            {
                //                progstr += _progNameFull[i] + "|";
                //            }
                //        }
                //        else { progstr += _progNameFull[i] + "|"; }
                //    }
                //}
                //if (progstr.Length > 0) progstr.Remove(progstr.Length - 1);
                //cs.ComboList = progstr;

                //cfgProg.Cols["ProgName"].Style = cfgProg.Styles["Prog"];

                ////CellRange rg = cfgProg.GetCellRange(1, 5);
                ////rg.Style = cfgProg.Styles["Prog"];

                //cfgProg.Cols["ValidFrom"].Style = cfgProg.Styles["dateFrom"];
                //cfgProg.Cols["ValidTo"].Style = cfgProg.Styles["dateTo"];

                //cfgProg.Styles.Add("UserProgName");
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void InitCfgDonVi()
        {

            cfgDonVi.ExtendLastCol = true;
            cfgDonVi.Cols.Fixed = 1;
            cfgDonVi.Cols[0].Width = 25;

            string[] arrName = {"STT", "UnitType", "MaDV", "Name", "Notation", "Status", "StatusName", "ValidFrom",
                                   "ValidTo", "UnitTypeID", "UnitID", "ParentID", "ParentNotation", "Checkbox", "Sign" };
            string[] arrHeader = { "STT", "Loại đơn vị", "Mã đơn vị", "Tên đơn vị", "Ký hiệu", "Status", "Trạng thái", "Ngày có hiệu lực",
                                     "Ngày hết hiệu lực", "UnitTypeID", "UnitID", "ParentID", "Đơn vị cha","", "Ký lập" };

            cfgDonVi.AllowEditing = true;

            for (int j = 0; j < arrName.Length; j++)
            {

                int i = j + 1;
                #region C1
                // tên và header
                cfgDonVi.Cols[i].Name = arrName[j];
                cfgDonVi.Cols[i].Caption = arrHeader[j];



                // căn lề
                if (i == 1 || i == 3 || i == 7 || i == 8 || i == 9)
                    cfgDonVi.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                else
                    cfgDonVi.Cols[i].TextAlign = TextAlignEnum.LeftCenter;

                // kích thước cột
                switch (i)
                {
                    case 1: cfgDonVi.Cols[i].Width = 50; break;
                    case 3: cfgDonVi.Cols[i].Width = 100; break;
                    case 4: cfgDonVi.Cols[i].Width = 250; break;
                    case 7: cfgDonVi.Cols[i].Width = 100; break;
                    case 5: cfgDonVi.Cols[i].Width = 100; break;
                    case 8: cfgDonVi.Cols[i].Width = 100; break;
                    case 9: cfgDonVi.Cols[i].Width = 100; break;
                    case 13: cfgDonVi.Cols[i].Width = 100; break;
                    case 14: cfgDonVi.Cols[i].Width = 40; break;
                    case 15: cfgDonVi.Cols[i].Width = 70; break;
                    case 2: cfgDonVi.Cols[i].Width = 100; break;
                }
                // format cột
                if (i == 9 || i == 8)
                    cfgDonVi.Cols[i].Format = "dd/MM/yyyy";

                // ẩn các cột không cần thiết
                if (i == 11 || i == 6 || i == 10 || i == 13 || i == 12 || i == 1 || i == 8 || i == 9)
                    cfgDonVi.Cols[i].Visible = false;

                if (i == 8 || i == 9)
                    cfgDonVi.Cols[i].AllowFiltering = AllowFiltering.ByCondition;
                else
                    cfgDonVi.Cols[i].AllowFiltering = AllowFiltering.Default;


                cfgDonVi.Cols[i].AllowEditing = false; ;
                #endregion
            }

            cfgDonVi.Cols["Checkbox"].AllowEditing = true;
            cfgDonVi.Cols["Checkbox"].Move(1);

            cfgDonVi.Cols["Sign"].AllowEditing = true;
            cfgDonVi.Cols["Sign"].Move(7);

            // căn giừa hàng đầu
            cfgDonVi.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
            DataTable dts = (DataTable)cfgDonVi.DataSource;

            cfgDonVi.Cols["UnitType"].Visible = true;

            //CellStyle cs = cfgDonVi.Styles.Add("Checkbox");
            //cs.DataType = typeof(string);
            //cs.ComboList = "Ký xác nhận|Ký lập";

            //cs = cfgDonVi.Styles.Add("StatusName");
            //cs.DataType = typeof(string);
            //cs.ComboList = "Hiệu lực|Không hiệu lực";
            //cfgDonVi.Cols["StatusName"].AllowEditing = true;

        }

        #endregion

        #region Data

        private void LoadData()
        {
            cfgProg.DataSource = _bus.CA_UserProg_SelectBy_UserID(-1);
        }

        private void LoadDataUnit(int ID_UserProg, string Seach)
        {
            try
            {
                BUSQuanTri bus = new BUSQuanTri();
                //DataTable dt = bus.CA_Unit_SelectBy_UnitTypeID_Status(Convert.ToInt32(cboLoaiDonVi.SelectedValue), Convert.ToInt32(cboTrangThai.SelectedValue));

                DataTable dt = bus.CA_Unit_Select_Check(ID_UserProg, Seach);

                List<int> ListUnit = new List<int>();

                DataColumn selectionCol = new DataColumn("Checkbox", typeof(bool));
                dt.Columns.Add(selectionCol);

                //if (UnitID.Count() == 0)
                //{
                if (ID_UserProg != -1)
                {
                    foreach (DataRow item in dt.Rows)
                    {

                        if (item["Check0"].ToString() != "0")
                        {
                            item["Checkbox"] = true;
                        }
                        else
                        {
                            item["Checkbox"] = false;
                        }
                    }
                }
                else
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        item["Checkbox"] = false;
                    }
                }
                //}
                //else
                //{
                //    foreach (DataRow item in dt.Rows)
                //    {

                //        if (_unitID.IndexOf(Convert.ToInt32(item["UnitID"])) != -1)
                //        {
                //            item["Checkbox"] = true;
                //        }
                //        else
                //        {
                //            item["Checkbox"] = false;
                //        }

                //    }
                //}
                dt.Columns.Remove("Check0");

                selectionCol = new DataColumn("Sign", typeof(bool));
                selectionCol.DefaultValue = false;
                dt.Columns.Add(selectionCol);

                cfgDonVi.DataSource = dt;

            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void LoadDataUnit(int userID, int progID, string userProgName)
        {
            try
            {
                BUSQuanTri bus = new BUSQuanTri();

                DataTable dtUserProg = _bus.CA_UserProgram_SelectBy_UserProgName_ProgID_UserID(progID, userID, userProgName);
                int ID_UserProg = -1;
                if (dtUserProg.Rows.Count > 0)
                {
                    ID_UserProg = Convert.ToInt32(dtUserProg.Rows[0]["ID_UserProg"]);
                }

                DataTable dt = bus.CA_Unit_Select_Check(ID_UserProg, "");

                List<int> ListUnit = new List<int>();

                DataColumn selectionCol = new DataColumn("Checkbox", typeof(bool));
                dt.Columns.Add(selectionCol);

                selectionCol = new DataColumn("Sign", typeof(bool));
                dt.Columns.Add(selectionCol);

                List<int> UnitID = new List<int>();
                List<int> UnitSign = new List<int>();

                if (_progID.Count > 0)
                {
                    int x = _progID.IndexOf(progID);
                    if (x != -1)
                    {
                        UnitID = _unitID[x];
                        UnitSign = _unitSign[x];
                    }
                }

                if (UnitID.Count() == 0)
                {
                    if (ID_UserProg != -1)
                    {
                        dtUserProg = _bus.CA_UserProgram_SelectByUserProgID(ID_UserProg);

                        List<int> tamUnit = new List<int>();
                        List<int> tamSign = new List<int>();

                        if (dtUserProg != null)
                        {

                            foreach (DataRow item in dtUserProg.Rows)
                            {
                                if (item["UnitID"].ToString() != "")
                                {
                                    tamSign.Add(Convert.ToInt32(item["SignatureTypeID"]));
                                    tamUnit.Add(Convert.ToInt32(item["UnitID"]));
                                }
                            }
                        }
                        foreach (DataRow item in dt.Rows)
                        {
                            int x = UnitID.IndexOf(Convert.ToInt32(item["UnitID"]));
                            if (x != -1)
                            {
                                item["Checkbox"] = true;
                                if (UnitSign[x] != 1)
                                {
                                    item["Sign"] = true;
                                }
                                else
                                {
                                    item["Sign"] = false;
                                }
                            }
                            else
                            {
                                item["Checkbox"] = false;
                                item["Sign"] = false;
                            }

                            if (item["Check0"].ToString() != "0")
                            {
                                item["Checkbox"] = true;
                            }
                            else
                            {
                                item["Checkbox"] = false;
                            }
                        }
                    }
                    else
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            item["Checkbox"] = false;
                            item["Sign"] = false;
                        }
                    }
                }
                else
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        int x = UnitID.IndexOf(Convert.ToInt32(item["UnitID"]));
                        if (x != -1)
                        {
                            item["Checkbox"] = true;
                            if (UnitSign[x] != 1)
                            {
                                item["Sign"] = true;
                            }
                            else
                            {
                                item["Sign"] = false;
                            }
                        }
                        else
                        {
                            item["Checkbox"] = false;
                            item["Sign"] = false;
                        }

                    }
                }
                dt.Columns.Remove("Check0");
                cfgDonVi.DataSource = dt;

            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void LoadDataUnit_UserID_ProgID_UserProgName(int userID, int progID, string userProgName)
        {
            try
            {
                int ID_UserProg = -1;
                string Seach = "";
                if (userID != -1 && progID != -1 && userProgName != "")
                {
                    DataTable dtUserProg = _bus.CA_UserProgram_SelectBy_UserProgName_ProgID_UserID(progID, userID, userProgName);
                    if (dtUserProg != null)
                    {
                        ID_UserProg = Convert.ToInt32(dtUserProg.Rows[0]["ID_UserProg"]);
                    }
                }
                DataTable dt = _bus.CA_Unit_Select_Check(ID_UserProg, Seach);


                DataColumn selectionCol = new DataColumn("Checkbox", typeof(bool));
                dt.Columns.Add(selectionCol);

                int indexProg = -1;

                if (ID_UserProg == -1) // kiểm tra xem bản chi có trong db chua
                {
                    if (_progID.Count == 0) // danh sach tam khong co
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            item["Checkbox"] = false;
                        }
                    }
                    else
                    {
                        indexProg = _progID.IndexOf(progID);
                        if (indexProg == -1) // nếu chua có trong db va khong co trong danh sach tam thì
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                item["Checkbox"] = false;
                            }
                        }
                        else
                        {
                            List<int> unitprog = _unitID[indexProg];
                            if (unitprog.Count == 0) // co trong danh sach tam nhu chua co gi
                            {
                                foreach (DataRow item in dt.Rows)
                                {
                                    item["Checkbox"] = false;
                                }
                            }
                            else // danh sach tam c, neu co thi de true
                            {
                                foreach (DataRow item in dt.Rows)
                                {
                                    if (unitprog.IndexOf(Convert.ToInt32(item["UnitID"])) != -1)
                                    {
                                        item["Checkbox"] = true;
                                    }
                                    else
                                    {
                                        item["Checkbox"] = false;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {

                }

                //--------------------------------------------------------------

                if (_progID.Count == 0 & ID_UserProg == -1)
                {
                    indexProg = -1;
                    foreach (DataRow item in dt.Rows)
                    {
                        item["Checkbox"] = false;
                    }
                }
                else
                {
                    indexProg = _progID.IndexOf(progID);

                    if (indexProg == -1)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            item["Checkbox"] = false;
                        }
                    }

                    if (indexProg != -1 & _unitID[indexProg].Count > 0)
                    {
                        List<int> unitprog = _unitID[indexProg];

                        foreach (DataRow item in dt.Rows)
                        {

                            if (unitprog.IndexOf(Convert.ToInt32(item["UnitID"])) != -1)
                            {
                                item["Checkbox"] = true;
                            }
                            else
                            {
                                item["Checkbox"] = false;
                            }

                        }

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }

        }

        #endregion

        #region Controls

        private void btnSeachUser_Click(object sender, EventArgs e)
        {
            frmLocNguoiDung frm = new frmLocNguoiDung();
            frm.ShowDialog();
            if (frm.UserID != -1 && frm.UserName != "")
            {
                _userID = frm.UserID;
                txtUserName.Text = frm.UserName;
            }
        }

        #endregion

        #region Action

        private void cfgProg_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                string dau = cfgProg.Rows[cfgProg.Row][5].ToString();
                int index = Array.IndexOf(_progNameFull, dau);
                int prog = _progIDFull[index];

                #region ProgName

                if (cfgProg.Col == 5)
                {

                    CellStyle cs = cfgProg.Styles["UserProgName"];
                    cs.DataType = typeof(string);
                    try
                    {

                        DataTable dt = _bus.CA_Program_SelectByProgID(prog);
                        BUSQuanTri bus = new BUSQuanTri(dt.Rows[0]["ServerName"].ToString(), dt.Rows[0]["DBName"].ToString(), dt.Rows[0]["UserDB"].ToString(), dt.Rows[0]["PasswordDB"].ToString());
                        DataTable dtUserProg = bus.HT_HeThong_SelectUser(dt.Rows[0]["TableUser"].ToString(), dt.Rows[0]["ColummUserID"].ToString(), dt.Rows[0]["ColummUserName"].ToString());

                        if (dtUserProg.Columns[0].ColumnName == "IDUSER")
                        {
                            string progstr = "";
                            foreach (DataRow item in dtUserProg.Rows)
                            {
                                progstr += item["IDUSER"] + "|";
                            }
                            progstr.Remove(progstr.Length - 1);
                            cs.ComboList = progstr;

                        }
                        else
                        {
                            cs.ComboList = "";
                        }
                    }
                    catch (Exception)
                    {
                        cs.ComboList = "";
                    }

                    CellRange rg = cfgProg.GetCellRange(cfgProg.Row, 6);
                    rg.Style = cfgProg.Styles["UserProgName"];

                }
                #endregion

                #region Xử lý
                try
                {
                    string userprogname = cfgProg.Rows[cfgProg.Row][6].ToString();
                    if (prog != -1 && userprogname != "" && _userID != -1)
                    {
                        DataTable dt = _bus.CA_UserProgram_SelectBy_UserProgName_ProgID_UserID(prog, _userID, userprogname);
                        int ID_UserProg = -1;
                        if (dt.Rows.Count > 0)
                        {
                            ID_UserProg = Convert.ToInt32(dt.Rows[0]["ID_UserProg"]);
                        }

                        LoadDataUnit(_userID, prog, userprogname);
                        InitCfgDonVi();
                    }
                }
                catch (Exception ex)
                {
                }
                #endregion
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void cfgProg_Click(object sender, EventArgs e)
        {
            try
            {
                // Lay ID Prog truoc
                string dau = cfgProg.Rows[_indexRowBefore]["ProgName"].ToString();
                int index = Array.IndexOf(_progNameFull, dau);
                int prog = _progIDFull[index];

                // lay userprogname
                string userprogname = cfgProg.Rows[_indexRowBefore]["UserProgName"].ToString();

                // lay ID cua ban ghi(neu co)
                DataTable dt = _bus.CA_UserProgram_SelectBy_UserProgName_ProgID_UserID(prog, _userID, userprogname);
                int ID_UserProg = -1;
                if (dt.Rows.Count > 0)
                {
                    ID_UserProg = Convert.ToInt32(dt.Rows[0]["ID_UserProg"]);
                }

                // kiem tra co thay doi dong khong
                if (_indexRowBefore != cfgProg.Row)
                {
                    #region Insert

                    if (_userID != -1 & prog != -1 & userprogname != "")
                    {
                        // list tam chua danh sach Unit va Sign
                        List<int> unitID = new List<int>();
                        List<int> unitSign = new List<int>();
                        DataTable dttr = (DataTable)cfgDonVi.DataSource;
                        DataRow[] rowtrue = dttr.Select("Checkbox = true");
                        if (rowtrue.Count() > 0)
                        {
                            int x = _progID.IndexOf(prog);
                            if (x == -1)
                            {
                                _progID.Add(prog);
                                _unitID.Add(new List<int>());
                                _unitSign.Add(new List<int>());
                                // lay du lieu tu bang truoc
                                foreach (DataRow item in rowtrue)
                                {
                                    unitID.Add(Convert.ToInt32(item["UnitID"]));
                                    if (Convert.ToBoolean(item["Checkbox"]))
                                    {
                                        unitSign.Add(1);
                                    }
                                    else
                                    {
                                        unitSign.Add(2);
                                    }
                                }
                            }
                            else
                            {
                                unitID = _unitID[x];
                                unitSign = _unitSign[x];
                            }

                            // kiem tra dua vao danh sach tam
                            if (_progID.Count > 0)
                            {
                                if (x != -1)
                                {
                                    _unitID[x] = unitID;
                                    _unitSign[x] = unitSign;
                                }
                                else
                                {
                                    _unitSign.Add(unitSign);
                                    _unitID.Add(unitID);
                                }
                            }
                        }

                        LoadDataUnit(_userID, prog, userprogname);
                        //InitCfgProg();
                        InitCfgDonVi();

                        _indexRowBefore = cfgProg.Row;

                    }
                    #endregion

                    _indexRowBefore = cfgProg.Row;
                }

                dau = cfgProg.Rows[_indexRowBefore]["ProgName"].ToString();
                index = Array.IndexOf(_progNameFull, dau);
                prog = _progIDFull[index];

                // lay userprogname
                userprogname = cfgProg.Rows[_indexRowBefore]["UserProgName"].ToString();

                LoadDataUnit(_userID, prog, userprogname);
                InitCfgDonVi();
            }
            
            catch (Exception ex)
            {
                _indexRowBefore = cfgProg.Row;
            }
        }

        #endregion
    }
}
