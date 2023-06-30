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
    public partial class frmXemLogDanhMuc : Form
    {
        #region Var

        BUSQuanTri _bus = new BUSQuanTri();

        private TypeLog _log = TypeLog.User;

        public TypeLog Log
        {
            get { return _log; }
            set { _log = value; }
        }

        private int _userID;

        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private int _unitID;

        public int UnitID
        {
            get { return _unitID; }
            set { _unitID = value; }
        }

        private int _progID;

        public int ProgID
        {
            get { return _progID; }
            set { _progID = value; }
        }

        private int _certID;

        public int CertID
        {
            get { return _certID; }
            set { _certID = value; }
        }

        private int _id_UnitProg;

        public int ID_UnitProg
        {
            get { return _id_UnitProg; }
            set { _id_UnitProg = value; }
        }

        private int _id_UserProg;

        public int ID_UserProg
        {
            get { return _id_UserProg; }
            set { _id_UserProg = value; }
        }


        public enum TypeLog
        {
            User,
            UserProg,
            Unit,
            UnitProg,
            Program,
            Cert
        };

        #endregion

        public frmXemLogDanhMuc()
        {
            InitializeComponent();
        }

        private void frmXemLogDanhMuc_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitCfgType();
            }
            catch (Exception ex)
            {

            }
        }

        #region Init

        public void InitCfgType()
        {
            switch (_log)
            {
                case TypeLog.User:
                    InitCfgType_User();
                    break;
                case TypeLog.Unit:
                    InitCfgType_Unit();
                    break;
                case TypeLog.Program:
                    InitCfgType_Prog();
                    break;
                case TypeLog.Cert:
                    InitCfgType_Cert();
                    break;
                case TypeLog.UnitProg:
                    InitCfgType_UnitProg();
                    break;
                case TypeLog.UserProg:
                    InitCfgType_UserProg();
                    break;
            }
        }

        public void InitCfgType_User()
        {

            cfgType.MergedRanges.Clear();

            // cấu hình radGrid
            cfgType.ExtendLastCol = true;
            cfgType.Cols.Fixed = 1;
            cfgType.Cols[0].Width = 25;
            cfgType.AllowFiltering = true;

            string[] arrName = {"STT", "ID_Log", "UserID", "UserName","UnitID","UnitName","UnitNotation" 
                                   ,"Status", "StatusName","ValidFrom","ValidTo","CertID","CertNameCN","Description", "UserModified", "DateModified" };
            string[] arrHeader = {"STT", "ID_Log", "UserID", "Họ và tên","UnitID","Tên đơn vị","Đơn vị"
                                     , "Type", "Trạng thái","Ngày có hiệu lực","Ngày hết hiệu lực","CertID","Tên chứng thư - Ngày hết hạn","Mô tả", "Người sửa", "Thời gian sửa" };

            #region For

            for (int i = 0; i < arrHeader.Length; i++)
            {
                #region C1

                // tên cột và header
                cfgType.Cols[i + 1].Name = arrName[i];
                cfgType.Cols[i + 1].Caption = arrHeader[i];

                // căn lề
                switch (arrName[i])
                {
                    case "STT":
                    case "UserModified":
                    case "DateModified":
                    case "StatusName":
                    case "ValidFrom":
                    case "ValidTo":
                        cfgType.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    default:
                        cfgType.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                        break;
                }

                // ẩn các cột
                switch (arrName[i])
                {
                    case "ID_Log":
                    case "UserID":
                    case "UnitID":
                    case "CertID":
                    case "UnitName":
                    case "Status":
                        cfgType.Cols[i + 1].Visible = false;
                        break;
                }

                // tạo Filter và định dạng ngày tháng
                switch (arrName[i])
                {
                    case "DateModified":
                    case "ValidFrom":
                    case "ValidTo":
                        cfgType.Cols[i + 1].AllowFiltering = AllowFiltering.ByCondition;
                        cfgType.Cols[i + 1].Format = "dd/MM/yyyy HH:mm:ss ";
                        break;
                    default:
                        cfgType.Cols[i + 1].AllowFiltering = AllowFiltering.ByValue;
                        break;
                }

                #endregion

            }

            #endregion

            // kích thước cột
            cfgType.Cols["STT"].Width = 50;
            cfgType.Cols["UserName"].Width = 150;
            cfgType.Cols["UnitNotation"].Width = 100;
            cfgType.Cols["StatusName"].Width = 100;
            cfgType.Cols["ValidFrom"].Width = 150;
            cfgType.Cols["ValidTo"].Width = 150;
            cfgType.Cols["CertNameCN"].Width = 250;
            cfgType.Cols["UserModified"].Width = 100;
            cfgType.Cols["DateModified"].Width = 150;
            cfgType.Cols["Description"].Width = 200;

            // căn giừa hàng đầu
            cfgType.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
        }

        public void InitCfgType_Unit()
        {
            cfgType.MergedRanges.Clear();

            // cấu hình radGrid
            cfgType.ExtendLastCol = true;
            cfgType.Cols.Fixed = 1;
            cfgType.Cols[0].Width = 25;
            cfgType.AllowFiltering = true;

            string[] arrName = {"STT", "ID_Log", "UnitID", "UnitMaDV","UnitName","UnitNotation" 
                                   ,"Status", "StatusName","ValidFrom","ValidTo","UnitTypeID","UnitTypeName","ParentID", "UserModified", "DateModified" };
            string[] arrHeader = {"STT", "ID_Log", "UnitID", "Mã đơn vị","Tên đơn vị","Đơn vị"
                                     , "Type", "Trạng thái","Ngày có hiệu lực","Ngày hết hiệu lực","UnitTypeID","Loại đơn vị","ParentID", "Người sửa", "Thời gian sửa"};

            #region For

            for (int i = 0; i < arrHeader.Length; i++)
            {
                #region C1

                // tên cột và header
                cfgType.Cols[i + 1].Name = arrName[i];
                cfgType.Cols[i + 1].Caption = arrHeader[i];

                // căn lề
                switch (arrName[i])
                {
                    case "STT":
                    case "UserModified":
                    case "DateModified":
                    case "StatusName":
                    case "ValidFrom":
                    case "ValidTo":
                        cfgType.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    default:
                        cfgType.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                        break;
                }

                // ẩn các cột
                switch (arrName[i])
                {
                    case "ID_Log":
                    case "UnitID":
                    //case "UnitName":
                    case "ParentID":
                    case "UnitTypeID":
                    case "Status":
                        cfgType.Cols[i + 1].Visible = false;
                        break;
                }

                // tạo Filter và định dạng ngày tháng
                switch (arrName[i])
                {
                    case "DateModified":
                    case "ValidFrom":
                    case "ValidTo":
                        cfgType.Cols[i + 1].AllowFiltering = AllowFiltering.ByCondition;
                        cfgType.Cols[i + 1].Format = "dd/MM/yyyy HH:mm:ss ";
                        break;
                    default:
                        cfgType.Cols[i + 1].AllowFiltering = AllowFiltering.ByValue;
                        break;
                }

                #endregion

            }

            #endregion

            // kích thước cột
            cfgType.Cols["STT"].Width = 50;
            cfgType.Cols["UnitMaDV"].Width = 100;
            cfgType.Cols["UnitName"].Width = 200;
            cfgType.Cols["UnitNotation"].Width = 100;
            cfgType.Cols["StatusName"].Width = 100;
            cfgType.Cols["ValidFrom"].Width = 150;
            cfgType.Cols["ValidTo"].Width = 150;
            cfgType.Cols["UnitTypeName"].Width = 100;
            cfgType.Cols["UserModified"].Width = 100;
            cfgType.Cols["DateModified"].Width = 150;

            // căn giừa hàng đầu
            cfgType.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
        }

        public void InitCfgType_Prog()
        {
            cfgType.MergedRanges.Clear();

            // cấu hình radGrid
            cfgType.ExtendLastCol = true;
            cfgType.Cols.Fixed = 1;
            cfgType.Cols[0].Width = 25;
            cfgType.AllowFiltering = true;

            string[] arrName = {"STT", "ID_Log", "ProgID", "ProgName","Name","Notation" 
                                   ,"Status", "StatusName","ServerName","DBName","UserDB", "UserModified", "DateModified" };
            string[] arrHeader = {"STT", "ID_Log", "ProgID", "Mã hệ thống","Tên hệ thống","Ký hiệu"
                                     , "Type", "Trạng thái","ServerName","Database Name","UserDB", "Người sửa", "Thời gian sửa"};

            #region For

            for (int i = 0; i < arrHeader.Length; i++)
            {
                #region C1

                // tên cột và header
                cfgType.Cols[i + 1].Name = arrName[i];
                cfgType.Cols[i + 1].Caption = arrHeader[i];

                // căn lề
                switch (arrName[i])
                {
                    case "STT":
                    case "UserModified":
                    case "DateModified":
                    case "StatusName":
                        cfgType.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    default:
                        cfgType.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                        break;
                }

                // ẩn các cột
                switch (arrName[i])
                {
                    case "ID_Log":
                    case "ProgID":
                    case "Status":
                        cfgType.Cols[i + 1].Visible = false;
                        break;
                }

                // tạo Filter và định dạng ngày tháng
                switch (arrName[i])
                {
                    case "DateModified":
                    case "ValidFrom":
                    case "ValidTo":
                        cfgType.Cols[i + 1].AllowFiltering = AllowFiltering.ByCondition;
                        cfgType.Cols[i + 1].Format = "dd/MM/yyyy HH:mm:ss ";
                        break;
                    default:
                        cfgType.Cols[i + 1].AllowFiltering = AllowFiltering.ByValue;
                        break;
                }

                #endregion

            }

            #endregion

            // kích thước cột
            cfgType.Cols["STT"].Width = 50;
            cfgType.Cols["ProgName"].Width = 100;
            cfgType.Cols["Name"].Width = 200;
            cfgType.Cols["Notation"].Width = 100;
            cfgType.Cols["StatusName"].Width = 100;
            cfgType.Cols["ServerName"].Width = 150;
            cfgType.Cols["DBName"].Width = 150;
            cfgType.Cols["UserDB"].Width = 100;
            cfgType.Cols["UserModified"].Width = 100;
            cfgType.Cols["DateModified"].Width = 150;

            // căn giừa hàng đầu
            cfgType.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
        }

        public void InitCfgType_Cert()
        {
            cfgType.MergedRanges.Clear();

            // cấu hình radGrid
            cfgType.ExtendLastCol = true;
            cfgType.Cols.Fixed = 1;
            cfgType.Cols[0].Width = 25;
            cfgType.AllowFiltering = true;

            string[] arrName = { "STT", "ID_Log", "CertID", "CertNameCN", "Serial", "Status", "StatusName", "CertType", "CertTypeName", "IssuerID", "IssuerNameCN", "ValidFrom", "ValidTo", "UserModified", "DateModified" };
            string[] arrHeader = { "STT", "ID_Log", "CertID", "Tên chứng thư", "Số Serial", "Type", "Trạng thái", "CertType", "Loại", "IssuerID", "Nhà cung cấp", "Ngày có hiệu lực", "Ngày hết hiệu lực", "Người sửa", "Thời gian sửa" };

            #region For

            for (int i = 0; i < arrHeader.Length; i++)
            {
                #region C1

                // tên cột và header
                cfgType.Cols[i + 1].Name = arrName[i];
                cfgType.Cols[i + 1].Caption = arrHeader[i];

                // căn lề
                switch (arrName[i])
                {
                    case "STT":
                    case "UserModified":
                    case "DateModified":
                    case "ValidFrom":
                    case "ValidTo":
                    case "StatusName":
                    case "CertTypeName":
                        cfgType.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    default:
                        cfgType.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                        break;
                }

                // ẩn các cột
                switch (arrName[i])
                {
                    case "ID_Log":
                    case "CertID":
                    case "IssuerID":
                    case "Status":
                    case "CertType":
                        cfgType.Cols[i + 1].Visible = false;
                        break;
                }

                // tạo Filter và định dạng ngày tháng
                switch (arrName[i])
                {
                    case "DateModified":
                    case "ValidFrom":
                    case "ValidTo":
                        cfgType.Cols[i + 1].AllowFiltering = AllowFiltering.ByCondition;
                        cfgType.Cols[i + 1].Format = "dd/MM/yyyy HH:mm:ss ";
                        break;
                    default:
                        cfgType.Cols[i + 1].AllowFiltering = AllowFiltering.ByValue;
                        break;
                }

                #endregion

            }

            #endregion

            // kích thước cột
            cfgType.Cols["STT"].Width = 50;
            cfgType.Cols["CertNameCN"].Width = 300;
            cfgType.Cols["Serial"].Width = 250;
            cfgType.Cols["StatusName"].Width = 100;
            cfgType.Cols["CertTypeName"].Width = 100;
            cfgType.Cols["IssuerNameCN"].Width = 150;
            cfgType.Cols["ValidFrom"].Width = 150;
            cfgType.Cols["ValidTo"].Width = 150;
            cfgType.Cols["UserModified"].Width = 100;
            cfgType.Cols["DateModified"].Width = 170;

            // căn giừa hàng đầu
            cfgType.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
        }

        public void InitCfgType_UnitProg()
        {
            cfgType.MergedRanges.Clear();

            // cấu hình radGrid
            cfgType.ExtendLastCol = true;
            cfgType.Cols.Fixed = 1;
            cfgType.Cols[0].Width = 25;
            cfgType.AllowFiltering = true;

            string[] arrName = {"STT", "ID_Log", "ID_UnitProgram", "ProgID","ProgName", "ProgNotation", "UnitID","UnitMaDV","UnitName","UnitNotation"
                                   ,"Status", "StatusName", "UserModified", "DateModified" };
            string[] arrHeader = {"STT", "ID_Log", "ID_UnitProgram","ProgID","Hệ thống", "Ký hiệu hệ thống","UnitID","Mã đơn vị","UnitName","Đơn vị"
                                     , "Type", "Trạng thái", "Người sửa", "Thời gian sửa"};

            #region For

            for (int i = 0; i < arrHeader.Length; i++)
            {
                #region C1

                // tên cột và header
                cfgType.Cols[i + 1].Name = arrName[i];
                cfgType.Cols[i + 1].Caption = arrHeader[i];

                // căn lề
                switch (arrName[i])
                {
                    case "STT":
                    case "UserModified":
                    case "DateModified":
                    case "StatusName":
                        cfgType.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    default:
                        cfgType.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                        break;
                }

                // ẩn các cột
                switch (arrName[i])
                {
                    case "ID_Log":
                    case "ID_UnitProgram":
                    case "ProgID":
                    case "UnitID":
                    case "UnitName":
                    case "Status":
                        cfgType.Cols[i + 1].Visible = false;
                        break;
                }

                // tạo Filter và định dạng ngày tháng
                switch (arrName[i])
                {
                    case "DateModified":
                    case "ValidFrom":
                    case "ValidTo":
                        cfgType.Cols[i + 1].AllowFiltering = AllowFiltering.ByCondition;
                        cfgType.Cols[i + 1].Format = "dd/MM/yyyy HH:mm:ss ";
                        break;
                    default:
                        cfgType.Cols[i + 1].AllowFiltering = AllowFiltering.ByValue;
                        break;
                }

                #endregion
            }

            #endregion

            // kích thước cột
            cfgType.Cols["STT"].Width = 50;
            cfgType.Cols["ProgName"].Width = 200;
            cfgType.Cols["ProgNotation"].Width = 100;
            cfgType.Cols["UnitMaDV"].Width = 100;
            cfgType.Cols["UnitNotation"].Width = 150;
            cfgType.Cols["StatusName"].Width = 100;
            cfgType.Cols["UserModified"].Width = 100;
            cfgType.Cols["DateModified"].Width = 170;

            // căn giừa hàng đầu
            cfgType.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
        }

        public void InitCfgType_UserProg()
        {
            cfgType.MergedRanges.Clear();

            // cấu hình radGrid
            cfgType.ExtendLastCol = true;
            cfgType.Cols.Fixed = 1;
            cfgType.Cols[0].Width = 25;
            cfgType.AllowFiltering = true;

            string[] arrName = { "STT", "ID_Log", "ID_UserProg", "ProgID", "ProgName", "ProgNotation", "UserID", "UserName", "UserProgName", "UserModified", "DateModified" };
            string[] arrHeader = { "STT", "ID_Log", "ID_UserProg", "ProgID", "Tên hệ thống", "Ký hiệu hệ thống", "UserID", "Người dùng", "Tên đăng nhập", "Người sửa", "Thời gian sửa" };

            #region For
            for (int i = 0; i < arrHeader.Length; i++)
            {
                // tên cột và header
                cfgType.Cols[i + 1].Name = arrName[i];
                cfgType.Cols[i + 1].Caption = arrHeader[i];
                cfgType.Cols[i + 1].TextAlignFixed = TextAlignEnum.CenterCenter;

                // căn lề
                switch (arrName[i])
                {
                    case "STT":
                    case "UserModified":
                    case "DateModified":
                        cfgType.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    default:
                        cfgType.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                        break;
                }

                // ẩn các cột
                switch (arrName[i])
                {
                    case "ID_Log":
                    case "ID_UserProg":
                    case "ProgID":
                    case "UserID":
                        cfgType.Cols[i + 1].Visible = false;
                        break;
                }

                // tạo Filter và định dạng ngày tháng
                switch (arrName[i])
                {
                    case "DateModified":
                    case "ValidFrom":
                    case "ValidTo":
                        cfgType.Cols[i + 1].AllowFiltering = AllowFiltering.ByCondition;
                        cfgType.Cols[i + 1].Format = "dd/MM/yyyy HH:mm:ss ";
                        break;
                    default:
                        cfgType.Cols[i + 1].AllowFiltering = AllowFiltering.ByValue;
                        break;
                }
            }
            #endregion

            // kích thước cột
            cfgType.Cols["STT"].Width = 50;
            cfgType.Cols["ProgName"].Width = 150;
            cfgType.Cols["ProgNotation"].Width = 100;
            cfgType.Cols["UserName"].Width = 150;
            cfgType.Cols["UserProgName"].Width = 100;
            cfgType.Cols["UserModified"].Width = 100;
            cfgType.Cols["DateModified"].Width = 170;
        }

        #endregion

        #region Data
        public void LoadData()
        {
            DataTable dt = null;

            switch (_log)
            {
                case TypeLog.User:
                    dt = _bus.CA_User_Log_SelectBy_UserID(_userID);
                    break;
                case TypeLog.Unit:
                    dt = _bus.CA_Unit_Log_SelectBy_UnitID(_unitID);
                    break;
                case TypeLog.Program:
                    dt = _bus.CA_Program_Log_SelectBy_ProgID(_progID);
                    break;
                case TypeLog.Cert:
                    dt = _bus.CA_Certificate_Log_SelectBy_CertID(_certID);
                    break;
                case TypeLog.UnitProg:
                    dt = _bus.CA_UnitProgram_Log_SelectBy_IDUnitProgram(_id_UnitProg);
                    break;
                case TypeLog.UserProg:
                    dt = _bus.CA_UserProgram_Log_SelectBy_IDUserProgram(_id_UserProg);
                    break;
            }
            cfgType.DataSource = dt;
        }
        #endregion

        #region Controls

        #endregion

        #region Event

        #endregion
    }
}
