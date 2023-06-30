using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ES.CA_WebServiceDAL
{
    public partial class DALThanhToan
    {
        #region Private Members & Constructors
        /// <summary>
        /// SQL Connection
        /// </summary>
        private DAL_SqlConnector sc;

        /// <summary>
        /// Constructs new SqlDataProvider instance use default connection
        /// </summary>
        public DALThanhToan()
        {
            sc = new DAL_SqlConnector();
        }

        /// <summary>
        /// Constructs new SqlDataProvider instance with specific connection
        /// </summary>
        public DALThanhToan(string strConn)
        {
            sc = new DAL_SqlConnector(strConn);
        }

        /// <summary>
        /// Constructs new SqlDataProvider instance with specific connection
        /// </summary>
        public DALThanhToan(string sComputerName, string sDBName, string sUserName, string sPassword)
        {
            sc = new DAL_SqlConnector(sComputerName, sDBName, sUserName, sPassword);
        }
        #endregion
        
        #region Methods
        public DataTable TT_XacThucThongKe_TuNgayDenNgay_EPTC(string Ma_NM, DateTime TuNgay, DateTime DenNgay)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "TT_XacThucThongKe_TuNgayDenNgay_EPTC";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@MaDV", Ma_NM);
                cmd.Parameters.AddWithValue("@TuNgay", TuNgay);
                cmd.Parameters.AddWithValue("@DenNgay", DenNgay);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable(cmd.CommandText);
                sda.Fill(dt);

                sqlcon.Close();
                return dt;
            }
            catch (Exception ex)
            {
                if (sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();
                throw ex;
            }
        }

        public DataTable TT_XacThucThongKe_Ngay_EPTC(string Ma_NM, DateTime Ngay)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "TT_XacThucThongKe_Ngay_EPTC";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@MaDV", Ma_NM);
                cmd.Parameters.AddWithValue("@Ngay", Ngay);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable(cmd.CommandText);
                sda.Fill(dt);

                sqlcon.Close();
                return dt;
            }
            catch (Exception ex)
            {
                if (sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();
                throw ex;
            }
        }

        public bool InsertXacThucBangKe_EPTC(string ma_NM, DateTime ngay, int lanXacThuc, bool XacThuc, string LyDo, string NguoiXacThuc, DateTime NgayXacThuc)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "TT_XacThucBangKeAdd_Nhieu_EPTC";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@Ma_NM", ma_NM);
                cmd.Parameters.AddWithValue("@Ngay", ngay);
                cmd.Parameters.AddWithValue("@LanXacThuc", lanXacThuc);
                cmd.Parameters.AddWithValue("@EPTC_XacThuc", XacThuc);
                cmd.Parameters.AddWithValue("@EPTC_LyDo", LyDo);
                cmd.Parameters.AddWithValue("@EPTC_NguoiXacThuc", NguoiXacThuc);
                cmd.Parameters.AddWithValue("@EPTC_NgayXacThuc", NgayXacThuc);
                
                cmd.ExecuteNonQuery();
                sqlcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();
                string error = ex.Message;
                return false;
            }
        }
        #endregion
    }
}
