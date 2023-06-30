using System;
using System.Data.SqlClient;

namespace ES.CA_ManagementDAL
{
    public class DAL_SqlConnector
    {
        private static string _connectionString;
        private SqlConnection sqlConnection = null;

        /// <summary>
        /// Gets and sets the connection string
        /// </summary>
        public static string ConnectionString
        {
            set
            {
                _connectionString = value;
            }
            get
            {
                return _connectionString;
            }
        }

        public DAL_SqlConnector()
        {
            try
            {
                sqlConnection = new SqlConnection(_connectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DAL_SqlConnector(string strConn)
        {
            try
            {
                sqlConnection = new SqlConnection(strConn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DAL_SqlConnector(string sComputerName, string sDBName, string sUserName, string sPassword)
        {

            try
            {
                string strConn = "Data Source= " + sComputerName +
                                    ";User ID=" + sUserName +
                                    ";Password=" + sPassword + 
                                    ";Persist Security Info=TRUE" + 
                                    "; Initial Catalog=" + sDBName;
                sqlConnection = new SqlConnection(strConn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlConnection GetConnection()
        {
            if (sqlConnection == null)
            {
                try
                {
                    sqlConnection = new SqlConnection(_connectionString);
                    return sqlConnection;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi DAL_SqlConnector.GetConnection(): Kết nối không tồn tại!\n\n" + ex.Message);
                }
            }
            else
            {
                return sqlConnection;
            }
        }
    }
}
