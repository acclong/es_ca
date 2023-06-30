using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;

namespace ES.CA_ManagementDAL
{
    public partial class DALQuanTri
    {
        #region Private Members & Constructors
        /// <summary>
        /// SQL Connection
        /// </summary>
        private DAL_SqlConnector sc;

        /// <summary>
        /// Constructs new SqlDataProvider instance use default connection
        /// </summary>
        public DALQuanTri()
        {
            sc = new DAL_SqlConnector();
        }

        /// <summary>
        /// Constructs new SqlDataProvider instance with specific connection
        /// </summary>
        public DALQuanTri(string strConn)
        {
            sc = new DAL_SqlConnector(strConn);
        }

        /// <summary>
        /// Constructs new SqlDataProvider instance with specific connection
        /// </summary>
        public DALQuanTri(string sComputerName, string sDBName, string sUserName, string sPassword)
        {
            sc = new DAL_SqlConnector(sComputerName, sDBName, sUserName, sPassword);
        }
        #endregion

        private static bool isEmpty(object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString().Trim() == "")
                return true;
            else
                return false;
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        #region Methods
        #region Sample
        public DataTable SXKD_ThongSoVanHanh(string Ma_NM, DateTime NgayXem)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SXKD_ThongSoVanHanh";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("Ma_NM", Ma_NM);
                cmd.Parameters.AddWithValue("NgayXem", NgayXem);
                cmd.Parameters.Add("P_OUT", SqlDbType.VarChar).Direction = ParameterDirection.Output;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                string sOutput = cmd.Parameters["P_OUT"].Value.ToString();

                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
        }

        public DataSet SXKD_LuuLuongNuocTram_Ngang(string MaTram, DateTime StartDate, DateTime EndDate)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SXKD_LuuLuongNuocTram_Ngang";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("P_OUT", SqlDbType.Int, 0)).Direction = ParameterDirection.Input;
                cmd.Parameters.Add(new SqlParameter("P_ERROR", SqlDbType.VarChar, 500)).Direction = ParameterDirection.Output;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                connection.Close();

                return ds;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
        }

        public void SXKD_M_ThuyVan_Tram_Insert_Update(DataTable dtLLNuocTram, string user)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                for (int i = 0; i < dtLLNuocTram.Rows.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SXKD_M_ThuyVan_Tram_Insert_Update";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;

                    cmd.Parameters.Add(new SqlParameter("@MaTram", SqlDbType.NChar, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dtLLNuocTram.Rows[i]["Matram"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("@Ngay", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, (DateTime)dtLLNuocTram.Rows[i]["Gio"]));

                    if (dtLLNuocTram.Rows[i]["LuuLuong"] != DBNull.Value)
                        cmd.Parameters.Add(new SqlParameter("@LuuLuong", SqlDbType.Decimal, 18, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, (decimal)dtLLNuocTram.Rows[i]["LuuLuong"]));
                    else
                        cmd.Parameters.Add(new SqlParameter("@LuuLuong", SqlDbType.Decimal, 18, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));

                    if (user != string.Empty)
                        cmd.Parameters.Add(new SqlParameter("@NguoiNhap", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, user));
                    else
                        cmd.Parameters.Add(new SqlParameter("@NguoiNhap", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, string.Empty));
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                {
                    transaction.Rollback();
                    connection.Close();
                }
                throw ex;
            }
        }
        #endregion

        #region Q_Config

        public DataTable Q_CONFIG_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_CONFIG_SelectAll";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
        }

        public DataTable Q_CONFIG_SelectBy_ConfigID(int configID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_CONFIG_SelectBy_ConfigID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("ConfigID", configID);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
        }

        public void Q_CONFIG_Update(int configID, string Value)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_CONFIG_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@ConfigID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, configID));
                if(Value == null)
                    cmd.Parameters.Add(new SqlParameter("@Value", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@Value", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Value));

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
        }

        public DataTable Q_CONFIG_Select_Mail()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_CONFIG_Select_Mail";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 600;
                cmd.Connection = connection;


                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
        }
        #endregion

        public DataTable S_DonVi_SelectAll(string sp_Select)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sp_Select;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
        }

        public DataTable HT_HeThong_SelectUser(string tableUser, string colummUserID, string colummUserName)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT [" + colummUserID + "], [" + colummUserName + "] FROM [" + tableUser + "]";
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 600;
                cmd.Connection = connection;
                

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
        }

        public DataTable HT_HeThong_SelectUser(string sQueryUser)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sQueryUser;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 600;
                cmd.Connection = connection;


                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
        }

        public void HT_BiddingServer_CA_Unit_User_Update(DataTable dtUser)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand();

            try
            {
                //Cập nhật danh sách ánh xạ Đơn vị phát điện - Người dùng
                //Thêm
                DataRow[] rowAdd = dtUser.Select("", "", DataViewRowState.Added);
                foreach (DataRow row in rowAdd)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "INSERT INTO CA_Unit_User VALUES  ('" + row["Ma_DVPD"] + "','" + row["UserName"] + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;

                    cmd.ExecuteNonQuery();
                }
                //Sửa
                DataRow[] rowUpdate = dtUser.Select("", "", DataViewRowState.ModifiedCurrent);
                foreach (DataRow row in rowUpdate)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "UPDATE CA_Unit_User SET UserName = '" + row["UserName"] + "' WHERE Ma_DVPD = '" + row["Ma_DVPD"] + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;

                    cmd.ExecuteNonQuery();
                }
                //xóa văn bản thuộc hồ sơ
                DataRow[] rowDelete = dtUser.Select("", "", DataViewRowState.Deleted);
                foreach (DataRow row in rowDelete)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "DELETE FROM CA_Unit_User WHERE Ma_DVPD = '" + row["Ma_DVPD", DataRowVersion.Original] + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;

                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                {
                    transaction.Rollback();
                    connection.Close();
                }
                throw ex;
            }
        }
        #endregion
    }
}
