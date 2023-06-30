using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using esDigitalSignature;

namespace ClassDAL
{
    public partial class DALFile
    {
        #region Private Members & Constructors
        /// <summary>
        /// SQL Connection
        /// </summary>
        private DAL_SqlConnector sc;

        /// <summary>
        /// Constructs new SqlDataProvider instance use default connection
        /// </summary>
        public DALFile()
        {
            sc = new DAL_SqlConnector();
        }

        /// <summary>
        /// Constructs new SqlDataProvider instance with specific connection
        /// </summary>
        public DALFile(string strConn)
        {
            sc = new DAL_SqlConnector(strConn);
        }

        /// <summary>
        /// Constructs new SqlDataProvider instance with specific connection
        /// </summary>
        public DALFile(string sComputerName, string sDBName, string sUserName, string sPassword)
        {
            sc = new DAL_SqlConnector(sComputerName, sDBName, sUserName, sPassword);
        }
        #endregion

        public bool UpdateInfoToSign(DataTable dtInputSign, DataTable dtInputDB, string key, string userName, string program,ref string strErr)
        {
            SqlConnection sqlconn = sc.GetConnection();
            sqlconn.Open();
            SqlTransaction transaction = sqlconn.BeginTransaction();
            try
            {
                for (int i = 0; i < dtInputSign.Rows.Count; i++)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.CommandText = "[dbo].[CA_DataSign_Insert_OutDataSignID]";
                    sqlcomm.CommandType = CommandType.StoredProcedure;
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.Transaction = transaction;
                    sqlcomm.Parameters.Clear();
                    sqlcomm.Parameters.AddWithValue("@CA_DataSignID", -1).Direction = ParameterDirection.InputOutput;
                    sqlcomm.Parameters.AddWithValue("@Key", key);
                    sqlcomm.Parameters.AddWithValue("@UserName", userName);
                    sqlcomm.Parameters.AddWithValue("@Program", program);

                    sqlcomm.Parameters.AddWithValue("@FileData", dtInputSign.Rows[i]["FileData"]).SqlDbType = SqlDbType.Image;
                    sqlcomm.Parameters.AddWithValue("@FileID", dtInputSign.Rows[i]["FileID"]);
                    sqlcomm.Parameters.AddWithValue("@FilePath", dtInputSign.Rows[i]["FilePath"]);

                    sqlcomm.ExecuteNonQuery();
                    int iDataSignID = Convert.ToInt32(sqlcomm.Parameters["@CA_DataSignID"].Value);
                    DataRow[] arrdr = dtInputDB.Select("Obj = '" + dtInputSign.Rows[i]["Obj"] + "'");
                    for (int j = 0; j < arrdr.Length; j++)
                    {
                        SqlCommand comm = new SqlCommand();
                        comm.CommandText = "[dbo].[CA_DataSignForDB_Insert]";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = sqlconn;
                        comm.Transaction = transaction;
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("@CA_DataSignID", iDataSignID);
                        comm.Parameters.AddWithValue("@ColumnName", arrdr[j]["ColumnName"]);
                        comm.Parameters.AddWithValue("@ColumnValue", arrdr[j]["ColumnValue"]);
                        comm.Parameters.AddWithValue("@ColumnType", arrdr[j]["ColumnType"]);
                        comm.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
                sqlconn.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (sqlconn != null && sqlconn.State == ConnectionState.Open)
                {
                    transaction.Rollback();
                    sqlconn.Close();
                }
                strErr = ex.Message;
                return false;
            }
        }
    }
}
