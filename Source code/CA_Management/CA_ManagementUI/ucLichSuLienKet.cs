using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace ES.CA_ManagementUI
{
    public partial class ucLichSuLienKet : UserControl
    {
        #region Var

        CA_ManagementBUS.BUSQuanTri _bus = new CA_ManagementBUS.BUSQuanTri();

        #endregion

        public ucLichSuLienKet()
        {
            InitializeComponent();
        }

        private void ucLichSuLienKet_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitDpkDate();
                InitCfgHistoryLink();

                cfgHistoryLink.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region Init
        private void InitDpkDate()
        {
            dpkDate.Value = DateTime.Now;
            dpkDate.Format = DateTimePickerFormat.Custom;
            dpkDate.CustomFormat = "dd/MM/yyyy HH:mm:ss";
        }

        private void InitCfgHistoryLink()
        {
            //cấu hình cột
            cfgHistoryLink.ExtendLastCol = true;
            cfgHistoryLink.Cols.Fixed = 1;
            cfgHistoryLink.Cols[0].Width = 20;
            cfgHistoryLink.AllowMerging = AllowMergingEnum.RestrictRows;
            cfgHistoryLink.AllowSorting = AllowSortingEnum.SingleColumn;

            //Edited by Toantk on 16/4/2015
            //Thêm trường STT và ẩn cột ID
            string[] arrName = new string[] { "STT", 
                "ProgID","ProgName","ProgNotation","ProgStatus","ProgStatusName",
                "UserID","UserName", "UserStatus","UserStatusName","UserValidFrom","UserValidTo",
                "ID_UserProg","UserProgName","UPValidFrom","UPValidTo",
                "CertID","CertNameCN", "CertStatus","CertStatusName", "CertValidFrom","CertValidTo"
                };
            string[] arrHeader = new string[] { "STT", 
                "ProgID","Tên hệ thống","Ký hiệu hệ thống","ProgStatus","Trạng thái hệ thống",
                "UserID","Người dùng", "UserStatus","Trạng thái người dùng","Ngày có hiệu lực","Ngày hết hiệu lực",
                "ID_UserProg","Tên đăng nhập","Ngày có hiệu lực","Ngày hết hiệu lực",
                "CertID","Tên chứng thư", "CertStatus","Trạng thái chứng thư", "Ngày có hiệu lực","Ngày hết hiệu lực"
            };

            #region For
            for (int i = 0; i < arrHeader.Length; i++)
            {
                // tên cột và header
                cfgHistoryLink.Cols[i + 1].Name = arrName[i];
                cfgHistoryLink.Cols[i + 1].Caption = arrHeader[i];

                #region Căn lề

                // căn lề
                switch (arrName[i])
                {
                    case "STT":
                    case "ProgStatusName":

                    case "UserStatusName":
                    case "UserValidFrom":
                    case "UserValidTo":

                    case "UPValidFrom":
                    case "UPValidTo":

                    case "CertStatusName":
                    case "CertValidFrom":
                    case "CertValidTo":
                        cfgHistoryLink.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    default:
                        cfgHistoryLink.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                        break;
                }
                #endregion

                #region Gộp dòng
                //// Gộp dòng
                //switch (arrName[i])
                //{
                //    case "ProgName":
                //        case "ProgNotation":
                //        case "ProgStatusName":
                //        cfgHistoryLink.Cols[i + 1].AllowMerging = true;
                //        break;
                //    default:
                //        cfgHistoryLink.Cols[i + 1].AllowMerging = false;
                //        break;
                //}
                #endregion

                #region Ẩn cột
                // ẩn các cột
                switch (arrName[i])
                {
                    case "ProgID":
                    case "ProgStatus":

                    case "UserID":
                    case "UserStatus":

                    case "ID_UserProg":

                    case "CertID":
                    case "CertStatus":

                    case "Status":
                        cfgHistoryLink.Cols[i + 1].Visible = false;
                        break;
                }
                #endregion

                #region Filter và định dạng ngày tháng

                // tạo Filter và định dạng ngày tháng
                switch (arrName[i])
                {
                    case "UserValidFrom":
                    case "UserValidTo":
                    case "UPValidFrom":
                    case "UPValidTo":
                    case "CertValidFrom":
                    case "CertValidTo":
                        cfgHistoryLink.Cols[i + 1].AllowFiltering = AllowFiltering.ByCondition;
                        cfgHistoryLink.Cols[i + 1].Format = "dd/MM/yyyy HH:mm:ss ";
                        break;
                    default:
                        cfgHistoryLink.Cols[i + 1].AllowFiltering = AllowFiltering.ByValue;
                        break;
                }

                #endregion
            }
            #endregion

            #region Định dạng chiều ngang cột
            // kích thước cột
            cfgHistoryLink.Cols["STT"].Width = 50;

            cfgHistoryLink.Cols["ProgName"].Width = 200;
            cfgHistoryLink.Cols["ProgNotation"].Width = 100;
            cfgHistoryLink.Cols["ProgStatusName"].Width = 100;

            cfgHistoryLink.Cols["UserName"].Width = 150;
            cfgHistoryLink.Cols["UserStatusName"].Width = 100;
            cfgHistoryLink.Cols["UserValidFrom"].Width = 150;
            cfgHistoryLink.Cols["UserValidTo"].Width = 150;

            cfgHistoryLink.Cols["UserProgName"].Width = 100;
            cfgHistoryLink.Cols["UPValidFrom"].Width = 150;
            cfgHistoryLink.Cols["UPValidTo"].Width = 150;

            cfgHistoryLink.Cols["CertNameCN"].Width = 200;
            cfgHistoryLink.Cols["CertStatusName"].Width = 100;
            cfgHistoryLink.Cols["CertValidFrom"].Width = 150;
            cfgHistoryLink.Cols["CertValidTo"].Width = 150;
            #endregion

            // căn giừa hàng đầu
            cfgHistoryLink.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
            //cfgHistoryLink.Rows[0].Style.Font = new Font("Times New Roman", 11, FontStyle.Bold);

        }
        #endregion

        #region Data
        public void LoadData()
        {
            DateTime date = DateTime.Now;
            DataTable dt = _bus.CA_LichSuLienKet(date);
            cfgHistoryLink.DataSource = dt;
        }
        #endregion

        #region Controls
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date = dpkDate.Value;
                DataTable dt = _bus.CA_LichSuLienKet(date);
                cfgHistoryLink.DataSource = dt;
                InitCfgHistoryLink();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }
        #endregion

        #region Event

        #endregion
    }
}
