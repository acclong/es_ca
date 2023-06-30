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
    public partial class frmLocDonViMulti : Form
    {
        private int id_UserProg;

        public int ID_UserProg
        {
            get { return id_UserProg; }
            set { id_UserProg = value; }
        }


        public frmLocDonViMulti()
        {
            InitializeComponent();
        }

        private void frmLocDonViMulti_Load(object sender, EventArgs e)
        {
            LoadData(id_UserProg, "");

            InitrgvDonVi();
        }

        #region Var

        private DataTable _dt = new DataTable();

        private List<int> _unitID = new List<int>();
        private List<string> _unitName = new List<string>();
        private List<string> _unitMaDV = new List<string>();
        private List<string> _unitNotation = new List<string>();
        private List<bool> _unitEnable = new List<bool>();
        private List<int> _unitSign = new List<int>();

        public List<int> UnitID
        {
            get { return _unitID; }
            set { _unitID = value; }
        }
        public List<string> UnitName
        {
            get { return _unitName; }
            set { _unitName = value; }
        }
        public List<string> UnitMaDV
        {
            get { return _unitMaDV; }
            set { _unitMaDV = value; }
        }
        public List<string> UnitNotation
        {
            get { return _unitNotation; }
            set { _unitNotation = value; }
        }
        public List<bool> UnitEnable
        {
            get { return _unitEnable; }
            set { _unitEnable = value; }
        }
        public List<int> UnitSign
        {
            get { return _unitSign; }
            set { _unitSign = value; }
        }

        #endregion

        #region Init

        private void InitrgvDonVi()
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
                if (i == 11 || i == 6 || i == 10 || i == 13 || i == 12 || i == 1)
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
            //cfgDonVi.Rows[0].Style.Font = new Font("Times New Roman", 11, FontStyle.Bold);

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

        private void LoadData(int ID_UserProg, string Seach)
        {
            try
            {
                BUSQuanTri bus = new BUSQuanTri();
                //DataTable dt = bus.CA_Unit_SelectBy_UnitTypeID_Status(Convert.ToInt32(cboLoaiDonVi.SelectedValue), Convert.ToInt32(cboTrangThai.SelectedValue));

                DataTable dt = bus.CA_Unit_Select_Check(ID_UserProg, Seach);



                DataColumn selectionCol = new DataColumn("Checkbox", typeof(bool));
                dt.Columns.Add(selectionCol);

                selectionCol = new DataColumn("Sign", typeof(bool));
                dt.Columns.Add(selectionCol);

                if (UnitID.Count() == 0)
                {
                    if (id_UserProg != -1)
                    {
                        DataTable dtUserProg = bus.CA_UserProgram_SelectByUserProgID(ID_UserProg);

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
                                if (UnitSign[x] == 1)
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

                            //if (item["Check0"].ToString() != "0")
                            //{
                            //    item["Checkbox"] = true;
                            //}
                            //else
                            //{
                            //    item["Checkbox"] = false;
                            //    item["Sign"] = false;
                            //}
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
                        int index = _unitID.IndexOf(Convert.ToInt32(item["UnitID"]));
                        if (index != -1)
                        {
                            item["Checkbox"] = true;
                            if (_unitSign[index] == 1)
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

                //selectionCol = new DataColumn("Sign", typeof(bool));
                //selectionCol.DefaultValue = false;
                //dt.Columns.Add(selectionCol);

                cfgDonVi.DataSource = dt;

                _dt = dt;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        #endregion

        #region Controls

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData(id_UserProg, txtSeach.Text);
                InitrgvDonVi();
            }
            catch (Exception ex)
            { }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            _unitID = new List<int>();
            _unitMaDV = new List<string>();
            _unitName = new List<string>();
            _unitNotation = new List<string>();
            _unitSign = new List<int>();
            DataRow[] rowtrue = _dt.Select("Checkbox = true");
            if (rowtrue.Count() > 0)
            {
                foreach (DataRow item in rowtrue)
                {
                    _unitID.Add(Convert.ToInt32(item["UnitID"]));
                    _unitMaDV.Add(item["MaDV"].ToString());
                    _unitName.Add(item["Name"].ToString());
                    _unitNotation.Add(item["Notation"].ToString());
                    if (Convert.ToBoolean(item["Sign"]))
                        _unitSign.Add(1);
                    else
                        _unitSign.Add(2);

                }
            }
            Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        private void btnAddUnit_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemSuaDonVi frm = new frmThemSuaDonVi();
                frm.UnitID = -1;
                frm.ShowDialog();

                // load lại dữ liệu
                LoadData(id_UserProg, "");
                InitrgvDonVi();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region Event

        #endregion
    }

    class SeachUnit
    {
        public SeachUnit() { }
        public SeachUnit(int ID, string madv, string notation, string name)
        {
            UnitID = ID;
            UnitMaDV = madv;
            UnitName = name;
            UnitNotation = notation;
        }

        public int UnitID { get; set; }

        public string UnitMaDV { get; set; }

        public string UnitNotation { get; set; }

        public string UnitName { get; set; }

        public void Swap(ref SeachUnit A, ref SeachUnit B)
        {
            SeachUnit C = A;
            A = B;
            B = C;
        }
    }
}
