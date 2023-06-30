using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using ES.CA_ManagementDAL;

namespace ES.CA_ManagementBUS
{
    public partial class BUSQuanTri
    {
        #region Constructor
        private DALQuanTri _dal;
        /// <summary>
        /// Khởi tạo dùng kết nối mặc định
        /// </summary>
        public BUSQuanTri()
        {
            _dal = new DALQuanTri();
        }

        /// <summary>
        /// Khởi tạo dùng kết nối tạm (đến database khác)
        /// </summary>
        /// <param name="strConn"></param>
        public BUSQuanTri(string strConn)
        {
            _dal = new DALQuanTri(strConn);
        }

        /// <summary>
        /// Khởi tạo dùng kết nối tạm (đến database khác)
        /// </summary>
        /// <param name="sComputerName"></param>
        /// <param name="sDBName"></param>
        /// <param name="sUserName"></param>
        /// <param name="sPassword"></param>
        public BUSQuanTri(string sComputerName, string sDBName, string sUserName, string sPassword)
        {
            _dal = new DALQuanTri(sComputerName, sDBName, sUserName, sPassword);
        }
        #endregion

        #region Methods
        #region Sample
        public DataTable SXKD_ThongSoVanHanh(string Ma_NM, DateTime NgayXem)
        {
            return _dal.SXKD_ThongSoVanHanh(Ma_NM, NgayXem);
        }

        public DataSet SXKD_LuuLuongNuocTram_Ngang(string MaTram, DateTime StartDate, DateTime EndDate)
        {
            return _dal.SXKD_LuuLuongNuocTram_Ngang(MaTram, StartDate, EndDate);
        }

        public void SXKD_M_ThuyVan_Tram_Insert_Update(DataTable dtLLNuocTram, string user)
        {
            _dal.SXKD_M_ThuyVan_Tram_Insert_Update(dtLLNuocTram, user);
        }
        #endregion

        #region Q_Config

        /// <summary>
        /// Toantk: Lấy danh sách các giá trị config
        /// </summary>
        /// <returns></returns>
        public DataTable Q_CONFIG_SelectAll()
        {
            return _dal.Q_CONFIG_SelectAll();
        }

        /// <summary>
        /// Toantk: Lấy giá trị config theo ID
        /// </summary>
        /// <param name="configID"></param>
        /// <returns></returns>
        public DataTable Q_CONFIG_SelectBy_ConfigID(int configID)
        {
            return _dal.Q_CONFIG_SelectBy_ConfigID(configID);
        }

        /// <summary>
        /// Hieutm: Thông tin thư mục chứa máy chủ
        /// </summary>
        /// <returns></returns>
        public string Q_CONFIG_GetRootFile()
        {
            DataTable dt = _dal.Q_CONFIG_SelectAll();
            return dt.Rows[2]["Value"].ToString();
        }

        /// <summary>
        /// Toantk: Lấy mã PIN mặc định (chưa giải mã)
        /// </summary>
        /// <returns></returns>
        public string Q_CONFIG_GetPINDefault()
        {
            DataTable dt = _dal.Q_CONFIG_SelectBy_ConfigID(6);
            return dt.Rows[0]["Value"].ToString();
        }

        /// <summary>
        /// Toantk: Cập nhật từng giá trị config
        /// </summary>
        /// <param name="configID"></param>
        /// <param name="value"></param>
        public void Q_CONFIG_Update(int configID, string value)
        {
            _dal.Q_CONFIG_Update(configID, value);
        }

        /// <summary>
        /// HieuTM: Lấy thông tin gửi mail tự động
        /// </summary>
        /// <returns></returns>
        public DataTable Q_CONFIG_Select_Mail()
        {
            return _dal.Q_CONFIG_Select_Mail();
        }
        #endregion

        /// <summary>
        /// Toantk: Lấy danh mục đơn vị từ các bảng S_ trong CSDL_Chung
        /// </summary>
        /// <param name="sp_Select">Store procedure tương ứng với loại đơn vị</param>
        /// <returns></returns>
        public DataTable S_DonVi_SelectAll(string sp_Select)
        {
            return _dal.S_DonVi_SelectAll(sp_Select);
        }

        /// <summary>
        /// Toantk: Lấy bảng người dùng từ csdl của hệ thống tích hợp
        /// </summary>
        /// <param name="tableUser"></param>
        /// <param name="colummUserID"></param>
        /// <param name="colummUserName"></param>
        /// <returns></returns>
        public DataTable HT_HeThong_SelectUser(string tableUser, string colummUserID, string colummUserName)
        {
            return _dal.HT_HeThong_SelectUser(tableUser, colummUserID, colummUserName);
        }

        /// <summary>
        /// HieuTM: Lấy bảng người dùng từ csdl của hệ thống tích hợp qua SQL Query
        /// <para>Chú ý: Bảng trả về phải có trường Login_ID và Login_Name</para>
        /// </summary>
        /// <param name="tableUser"></param>
        /// <param name="colummUserID"></param>
        /// <param name="colummUserName"></param>
        /// <returns></returns>
        public DataTable HT_HeThong_SelectUser(string sQueryUser)
        {
            return _dal.HT_HeThong_SelectUser(sQueryUser);
        }

        /// <summary>
        /// Toantk: Thêm cột ngày vào datatable để nhóm theo Date
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable AddDateCol(DataTable dt)
        {
            DataColumn dc = new DataColumn("ValidFromDate", typeof(DateTime));
            dt.Columns.Add(dc);
            dc = new DataColumn("ValidToDate", typeof(DateTime));
            dt.Columns.Add(dc);

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ValidFrom"] != DBNull.Value)
                    dr["ValidFromDate"] = Convert.ToDateTime(dr["ValidFrom"]).Date;
                if (dr["ValidTo"] != DBNull.Value)
                    dr["ValidToDate"] = Convert.ToDateTime(dr["ValidTo"]).Date;
            }

            return dt;
        }

        /// <summary>
        /// Cập nhật danh sách ánh xạ Đơn vị phát điện - Người dùng cho db Bidding Server
        /// </summary>
        /// <param name="dtUser"></param>
        public void HT_BiddingServer_CA_Unit_User_Update(DataTable dtUser)
        {
            _dal.HT_BiddingServer_CA_Unit_User_Update(dtUser);
        }
        #endregion
    }
}
