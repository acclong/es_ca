using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ES.CA_ManagementBUS;

namespace ES.CA_ManagementUI
{
    public partial class frmXemVBThayTheNew : Form
    {
        #region Var
        private BUSQuanTri _bus = new BUSQuanTri();
        private int _fileID = -1;
        public int FileID
        {
            get { return _fileID; }
            set { _fileID = value; }
        }
        #endregion

        public frmXemVBThayTheNew()
        {
            InitializeComponent();
        }

        private void frmXemVBThayTheNew_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitCfgFileRealese();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
                Close();
            }
        }

        #region Init
        private void InitCfgFileRealese()
        {
            // cấu hình radGrid
            cfgFileRelease.ExtendLastCol = true;
            cfgFileRelease.Cols.Fixed = 1;
            cfgFileRelease.Cols[0].Width = 25;
            cfgFileRelease.AllowFiltering = true;
            cfgFileRelease.AllowMerging = AllowMergingEnum.RestrictRows;

            string[] arrName = { "ID_FileRelation", "FileID_1", "FileNumber_1", "FileID_2", "FileNumber_2", "RelationTypeID", "RelationTypeName"
                                   , "UserModified", "DateModified", "Dau" };
            string[] arrHeader = { "ID_FileRelation", "FileID_1", "File A", "FileID_2", "File B", "RelationTypeID", "Loại liên kết"
                                     , "Người cập nhật", "Ngày cập nhật", "Dau" };

            #region For
            for (int i = 0; i < arrHeader.Length; i++)
            {
                // tên cột và header
                cfgFileRelease.Cols[i + 1].Name = arrName[i];
                cfgFileRelease.Cols[i + 1].Caption = arrHeader[i];
                cfgFileRelease.Cols[i + 1].TextAlignFixed = TextAlignEnum.CenterCenter;

                #region Magin
                // căn lề
                switch (arrName[i])
                {
                    case "UserModified":
                    case "DateModified":
                        cfgFileRelease.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    default:
                        cfgFileRelease.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                        break;
                }
                #endregion

                #region Hide
                // ẩn các cột
                switch (arrName[i])
                {
                    case "ID_FileRelation":
                    case "FileID_1":
                    case "FileID_2":
                    case "RelationTypeID":
                    case "Dau":
                        cfgFileRelease.Cols[i + 1].Visible = false;
                        break;
                }
                #endregion

                #region Format
                // tạo Filter và định dạng ngày tháng
                switch (arrName[i])
                {
                    case "DateModified":
                        cfgFileRelease.Cols[i + 1].AllowFiltering = AllowFiltering.ByCondition;
                        cfgFileRelease.Cols[i + 1].Format = "dd/MM/yyyy HH:mm:ss";
                        break;
                    default:
                        cfgFileRelease.Cols[i + 1].AllowFiltering = AllowFiltering.Default;
                        break;
                }
                #endregion
            }
            #endregion

            // To mau
            CellStyle cs = cfgFileRelease.Styles.Add("Color");
            //cs.BackColor = SystemColors.Info;
            cs.ForeColor = Color.Red;

            for (int i = 1; i < cfgFileRelease.Rows.Count; i++)
            {
                if(Convert.ToInt32(cfgFileRelease.Rows[i]["Dau"]) == 1)
                {
                    CellRange rg = cfgFileRelease.GetCellRange(i, 3);
                    rg.Style = cfgFileRelease.Styles["Color"];
                }
                else
                {
                    CellRange rg = cfgFileRelease.GetCellRange(i, 5);
                    rg.Style = cfgFileRelease.Styles["Color"];
                }
            }

            // kích thước cột
            cfgFileRelease.Cols["FileNumber_1"].Width = 250;
            cfgFileRelease.Cols["FileNumber_2"].Width = 250;
            cfgFileRelease.Cols["RelationTypeName"].Width = 200;
            cfgFileRelease.Cols["UserModified"].Width = 100;
            cfgFileRelease.Cols["DateModified"].Width = 130;
        }
        #endregion

        #region Data
        private void LoadData()
        {
            DataTable dt_Release = _bus.FL_FileRelation_SelectBy_FileID(_fileID);
            cfgFileRelease.DataSource = dt_Release;
        }
        #endregion

        #region Controls
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
