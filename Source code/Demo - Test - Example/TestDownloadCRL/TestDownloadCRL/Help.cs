using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace TestDownloadCRL
{
    public class Help
    {
        public bool DownloadFile(string sLinkCRL, string sPathSave)
        {
            try
            {
                // lấy tên file CRL
                string fileName = sLinkCRL.Split('/').Last();
                // Test thay đổi tên file để khi download file không bị ghi đè
                //Random rd = new Random();
                //string Name = fileName.Split('.').First() + rd.Next(100).ToString() + "." + fileName.Split('.').Last();
                //fileName = Name;
                WebClient wc = new WebClient();
                wc.DownloadFile(sLinkCRL, sPathSave + "\\" + fileName);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ReadConfigDB(string path, ref string sConnectString)
        {
            System.Xml.XmlDocument docCfg = new System.Xml.XmlDocument();
            try
            {
                docCfg.Load(path);
            }
            catch (Exception ex)
            {
                return false;
            }
            string ServerName = docCfg.GetElementsByTagName("ServerName").Item(0).InnerText;
            string UserName = docCfg.GetElementsByTagName("UserName").Item(0).InnerText;
            string Password = docCfg.GetElementsByTagName("Password").Item(0).InnerText;
            string DatabaseName = docCfg.GetElementsByTagName("DatabaseName").Item(0).InnerText;
            sConnectString = "Data Source= " + ServerName + "; User ID= " + UserName + "; Password=" + Password + "; Persist Security Info=TRUE; Initial Catalog=" + DatabaseName;
            return true;
        }
    }

    public class clsDB
    {
        string _connectionString;

        public clsDB(string SQLConnectionString)
        {
            _connectionString = SQLConnectionString;
        }

        public DataTable CA_CertificationAuthority_SelectCRL()
        {
            using (SqlConnection m_cnn = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCom = new SqlCommand();
                sqlCom.CommandText = "SELECT [CertAuthID], [NameCN], [CRL_URL] FROM [CA_CertificationAuthority] ORDER BY [CertAuthID]";
                sqlCom.CommandType = CommandType.Text;

                if (m_cnn.State != ConnectionState.Open)
                {
                    m_cnn.Open();
                }
                sqlCom.Connection = m_cnn;

                try
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sqlCom);
                    da.Fill(dt);
                    return dt;
                }
                catch
                {
                    return null;
                }
            }
        }

        public DataTable Q_CONFIG_SelectAll()
        {
            using (SqlConnection m_cnn = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCom = new SqlCommand();
                sqlCom.CommandText = "SELECT * FROM [Q_CONFIG]";
                sqlCom.CommandType = CommandType.Text;

                if (m_cnn.State != ConnectionState.Open)
                {
                    m_cnn.Open();
                }
                sqlCom.Connection = m_cnn;

                try
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sqlCom);
                    da.Fill(dt);
                    return dt;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
