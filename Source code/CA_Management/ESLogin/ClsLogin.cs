using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace ESLogin
{
    public class ClsLogin
    {
        public string connectSQL;

        public string m_sLastError;
        public int M_CapError = 0;

        public bool Admin;
        public string ID_Access;
        public string ID_License;
        public string MvarUsername_default;
        public string MvarPassword_default;
        //public string m_ProgBuiltVersion;
        public bool bHasLicense;
        public string infolicense;
        public string sLinkFileUpdate;

        private bool bComplete;
        private SqlConnection m_conn_sql;
        private string mvarUserName;
        private string mvarServerName;
        private string mvarDBName;
        private object mvarPassword;
        private bool mvarDispPassword;
        private string mvarVersion;
        private string sPath;
        private string mProgram;

        public enum Quyen_truycap
        {
            xem = 1,
            capnhat = 2
        }

        //public struct DBConnInfoType
        //{
        //    public string ServerName;
        //    public string DBName;
        //    public string UserName;
        //    public string Password;
        //    public bool DispPassword;
        //}

        public object Password
        {
            get { return mvarPassword; }
            set { mvarPassword = value; }
        }

        public string DBName
        {
            get { return mvarDBName; }
            set { mvarDBName = value; }
        }

        public string ConnectString
        {
            get { return connectSQL; }
            set { connectSQL = value; }
        }

        public string ServerName
        {
            get { return mvarServerName; }
            set { mvarServerName = value; }
        }

        public string UserName
        {
            get { return mvarUserName; }
            set { mvarUserName = value; }
        }

        public string Version
        {
            get { return mvarVersion; }
            set { mvarVersion = value; }
        }

        public string ProgramID
        {
            get { return mProgram; }
            set { mProgram = value; }
        }

        public string AppPath
        {
            get { return sPath; }
            set { sPath = value; }
        }

        public bool DispPassword
        {
            get { return mvarDispPassword; }
            set { mvarDispPassword = value; }
        }

        public bool CompleteUpdate
        {
            get { return bComplete; }
            set { bComplete = value; }
        }

        public bool ConnectToSQl_SQLConnection(string sComputerName, string sDBName, string sUserName, string sPassword)
        {
            bool functionReturnValue = false;
            m_sLastError = "";

            try
            {
                sUserName = sUserName.Trim();

                if (sDBName.Length > 0)
                {
                    connectSQL = "Data Source= " + sComputerName + ";User ID=" + sUserName + ";Password=" + sPassword + ";Persist Security Info=TRUE; Initial Catalog=" + sDBName;
                }
                else
                {
                    connectSQL = "Data Source= " + sComputerName + ";User ID=" + sUserName + ";Password=" + sPassword + ";Persist Security Info=TRUE; Initial Catalog=MASTER";
                }

                if (m_conn_sql != null)
                {
                    if (m_conn_sql.State != ConnectionState.Closed)
                        m_conn_sql.Close();
                }
                else
                {
                    m_conn_sql = new SqlConnection();
                }
                m_conn_sql.ConnectionString = connectSQL;
                m_conn_sql.Open();

                if (m_conn_sql.State == ConnectionState.Open)
                {
                    functionReturnValue = true;
                    ////////////////////////luu lai thong so\\\\\\\\\\\\\\\\\\\\\\\\
                    ServerName = sComputerName;
                    DBName = sDBName;
                    UserName = sUserName;
                    Password = sPassword;
                    ///////////////////////kiem tra admin\\\\\\\\\\\\\\\\\\\
                    SqlCommand cm = new SqlCommand();
                    int bien = -1;
                    if (m_conn_sql.State == ConnectionState.Closed)
                        m_conn_sql.Open();
                    cm.Connection = m_conn_sql;
                    cm.CommandText = "sp_PQ_check_admin";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add(new SqlParameter("@isAdmin", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, bien));
                    cm.ExecuteNonQuery();
                    bien = Convert.ToInt32(cm.Parameters["@isAdmin"].Value);

                    if (bien == 1)
                    {
                        functionReturnValue = true;
                        Admin = true;
                    }
                    else if (bien == 0)
                    {
                        if (m_conn_sql.State != ConnectionState.Closed)
                            m_conn_sql.Close();
                        m_conn_sql = null;
                        m_conn_sql = new SqlConnection();
                        m_sLastError = "Bạn không phải là người quản trị!";

                        functionReturnValue = false;
                        return functionReturnValue;
                    }
                }
                else
                {
                    if (m_conn_sql.State != ConnectionState.Closed)
                        m_conn_sql.Close();
                    m_conn_sql = null;
                    m_conn_sql = new SqlConnection();
                    m_sLastError = "Bạn không phải là người quản trị!";

                    functionReturnValue = false;
                }

                return functionReturnValue;
            }
            catch (Exception ex)
            {
                m_conn_sql = null;
                m_sLastError = "Bạn không phải là người quản trị!\n\nLỗi: " + ex.Message;
                M_CapError = 1;
                functionReturnValue = false;
            }

            return functionReturnValue;
        }

        public bool ConnectToSQL_SQLConnection_default(string sComputerName, string sDBName, string sUserName, string sPassword)
        {
            bool functionReturnValue = false;
            m_sLastError = "";

            try
            {
                if (m_conn_sql.State != ConnectionState.Closed)
                    m_conn_sql.Close();
            }
            catch { }
            m_conn_sql = null;
            m_conn_sql = new SqlConnection();

            try
            {
                if (m_conn_sql == null)
                    m_conn_sql = new SqlConnection();

                sUserName = sUserName.Trim();
                sPassword = sPassword.Trim();
                if (sDBName.Length > 0)
                {
                    connectSQL = "server=" + sComputerName + ";User ID=" + MvarUsername_default + ";Password=" + MvarPassword_default + ";database=" + sDBName + ";Connection Reset=FALSE;Connection Lifetime=5;Pooling=true;Max Pool Size=500";
                }
                else
                {
                    connectSQL = "Data Source= " + sComputerName + ";User ID=" + MvarUsername_default + ";Password=" + MvarPassword_default + ";Persist Security Info=TRUE; Initial Catalog=MASTER";
                }

                if (m_conn_sql.State != ConnectionState.Closed)
                    m_conn_sql.Close();
                m_conn_sql.ConnectionString = connectSQL;
                m_conn_sql.Open();

                if (m_conn_sql.State == ConnectionState.Open)
                {
                    functionReturnValue = true;
                    ////////////////////////luu lai thong so\\\\\\\\\\\\\\\\\\\\\\\\
                    ServerName = sComputerName;
                    DBName = sDBName;
                    UserName = sUserName;
                    Password = sPassword;

                    ///////////////////////kiem tra user va password\\\\\\\\\\\\\\\\
                    SqlCommand cm = new SqlCommand();
                    SqlDataAdapter da = default(SqlDataAdapter);
                    DataSet ds = new DataSet();
                    if (m_conn_sql.State == ConnectionState.Closed)
                        m_conn_sql.Open();
                    cm.Connection = m_conn_sql;
                    cm.CommandText = "sp_PQ_checkUser";
                    cm.CommandType = CommandType.StoredProcedure;

                    SqlParameter p = new SqlParameter();
                    p.ParameterName = "@Username";
                    p.Direction = ParameterDirection.Input;
                    p.SqlDbType = SqlDbType.VarChar;
                    p.Value = sUserName;
                    cm.Parameters.Add(p);

                    p = new SqlParameter();
                    p.ParameterName = "@Password";
                    p.Direction = ParameterDirection.Input;
                    p.SqlDbType = SqlDbType.NVarChar;
                    p.Value = StringCryptor.EncryptString(sPassword);
                    cm.Parameters.Add(p);

                    //p = new SqlParameter();
                    //p.ParameterName = "@ID_Program";
                    //p.Direction = ParameterDirection.Input;
                    //p.SqlDbType = SqlDbType.NVarChar;
                    //p.Value = ProgramID;
                    //cm.Parameters.Add(p);

                    da = new SqlDataAdapter(cm);
                    da.Fill(ds, "user");
                    if (ds.Tables["user"].Rows.Count > 0)
                    {
                        functionReturnValue = true;
                    }
                    else
                    {
                        if (m_conn_sql.State != ConnectionState.Closed)
                            m_conn_sql.Close();
                        m_conn_sql = null;
                        m_conn_sql = new System.Data.SqlClient.SqlConnection();

                        m_sLastError = "User hoặc password sai!";
                        M_CapError = 3;
                        functionReturnValue = false;
                        return functionReturnValue;
                    }
                }
                else
                {
                    m_conn_sql = null;
                    functionReturnValue = false;
                }
                return functionReturnValue;
            }
            catch (Exception ex)
            {
                if (m_conn_sql != null && m_conn_sql.State != ConnectionState.Closed)
                {
                    m_conn_sql.Close();
                    m_conn_sql = null;
                }
                m_sLastError = "User mặc định chưa có hoặc có nhưng sai về password.\nBạn hãy nhờ người quản trị thiết lập lại!\nLỗi: " + ex.Message;
                M_CapError = 2;
                functionReturnValue = false;
            }

            return functionReturnValue;
        }

        public bool ConnectToSQL_SQLConnection_security(string sComputerName, string sDBName, string sUserName, string sPassword)
        {
            bool functionReturnValue = false;

            if (ConnectToSQL_SQLConnection_default(sComputerName, sDBName, sUserName, sPassword))
            {
                m_sLastError = "";
                string User_access = null;
                string Password_Access = null;

                try
                {
                    ///////////////Phan lay Q-access \\\\\\\\\\\\\\\\\\
                    SqlCommand cm = new SqlCommand();
                    SqlDataAdapter da = default(SqlDataAdapter);
                    DataSet ds = new DataSet();
                    if (m_conn_sql.State == ConnectionState.Closed)
                        m_conn_sql.Open();
                    cm.Connection = m_conn_sql;
                    cm.CommandText = "sp_PQ_Qaccess";
                    cm.CommandType = CommandType.StoredProcedure;

                    SqlParameter p = new SqlParameter();
                    p.ParameterName = "@ID_Access";
                    p.Direction = ParameterDirection.Input;
                    p.SqlDbType = SqlDbType.Int;
                    p.Value = ID_Access;
                    cm.Parameters.Add(p);
                    da = new SqlDataAdapter(cm);
                    da.Fill(ds, "Q_Access");
                    if (ds.Tables["Q_Access"].Rows.Count > 0)
                    {
                        User_access = (string)ds.Tables["Q_Access"].Rows[0]["USERNAME"];
                        Password_Access = (string)ds.Tables["Q_Access"].Rows[0]["PASSWORD"];
                    }
                    else
                    {
                        if (m_conn_sql.State != ConnectionState.Closed)
                            m_conn_sql.Close();
                        m_conn_sql = null;
                        m_conn_sql = new System.Data.SqlClient.SqlConnection();

                        m_sLastError = "User_Access chưa có. Bạn phải nhờ người quản trị nhập lại User_Access!";
                        M_CapError = 4;
                        functionReturnValue = false;
                        return functionReturnValue;
                    }
                }
                catch { }

                ///////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                if (m_conn_sql.State != ConnectionState.Closed)
                    m_conn_sql.Close();
                m_conn_sql = null;
                m_conn_sql = new SqlConnection();

                try
                {
                    if (m_conn_sql == null)
                        m_conn_sql = new System.Data.SqlClient.SqlConnection();

                    sUserName = User_access;
                    sPassword = StringCryptor.DecryptString(Password_Access);
                    if (sDBName.Length > 0)
                    {
                        connectSQL = "server=" + sComputerName + ";User ID=" + sUserName + ";Password=" + sPassword + ";database=" + sDBName + ";Connection Reset=FALSE;Connection Lifetime=5;Pooling=true;Max Pool Size=500";
                    }
                    else
                    {
                        connectSQL = "Data Source= " + sComputerName + ";User ID=" + sUserName + ";Password=" + sPassword + ";Persist Security Info=TRUE; Initial Catalog=MASTER";
                    }

                    if (m_conn_sql.State != ConnectionState.Closed)
                        m_conn_sql.Close();
                    m_conn_sql.ConnectionString = connectSQL;
                    m_conn_sql.Open();

                    if (m_conn_sql.State == ConnectionState.Open)
                    {
                        functionReturnValue = true;
                    }
                    else
                    {
                        m_conn_sql = null;
                        m_sLastError = "User Access không truy cập vào cơ sở dữ liệu. Nhờ quản trị thiết lập lại User Access!";
                        functionReturnValue = false;
                    }
                    return functionReturnValue;
                }
                catch (Exception ex)
                {
                    m_conn_sql = null;
                    m_sLastError = "User Access không thể truy cập vào cơ sở dữ liệu.\nHãy nhờ quản trị thiết lập lại User Access!\nLỗi: " + ex.Message;
                    M_CapError = 5;
                    functionReturnValue = false;
                }

            }
            return functionReturnValue;
        }

        public bool dangnhap(string sComputerName, string sDBName, string sUserName, string sPassword)
        {
            m_sLastError = "";
            SqlConnection conn = new SqlConnection();

            try
            {
                sUserName = sUserName.Trim();
                sPassword = sPassword.Trim();
                if (sDBName.Length > 0)
                    connectSQL = "Data Source= " + sComputerName + "; User ID= " + sUserName + ";Password= " + sPassword + ";Persist Security Info=TRUE; Initial Catalog=MASTER";

                if (conn.State != ConnectionState.Closed)
                    conn.Close();
                conn.ConnectionString = connectSQL;
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    conn = null;
                    return true;
                }
                else
                {
                    conn = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn = null;
                m_sLastError = ex.Message;
                return false;
            }
        }

        public bool IsSQLConnected()
        {
            if (m_conn_sql == null)
                return false;

            if (m_conn_sql.State == ConnectionState.Open)
                return true;
            else
                return false;
        }

        public void DisConnect_SQLConnection()
        {
            try
            {
                if (IsSQLConnected())
                    if (m_conn_sql.State != ConnectionState.Closed)
                        m_conn_sql.Close();
            }
            catch { }
        }

        public DataTable quyen()
        {
            DataTable dt = new DataTable();
            SqlCommand cm = new SqlCommand();
            SqlConnection gconn = this.GetSQLConnection();
            if (gconn.State == ConnectionState.Closed)
                gconn.Open();
            try
            {
                cm.Connection = gconn;
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "sp_PQ_Quyen_Access_Menu";

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@USERID";
                p.Direction = ParameterDirection.Input;
                p.SqlDbType = SqlDbType.NVarChar;
                p.Value = Admin? "" : UserName;
                cm.Parameters.Add(p);

                p = new SqlParameter();
                p.ParameterName = "@PROGID";
                p.Direction = ParameterDirection.Input;
                p.SqlDbType = SqlDbType.NVarChar;
                p.Value = ProgramID;
                cm.Parameters.Add(p);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load quyền\n" + ex.Message);
            }
            return dt;
        }

        public DataTable quyen_Control()
        {
            DataTable dt = new DataTable();
            SqlCommand cm = new SqlCommand();
            SqlConnection gconn = this.GetSQLConnection();
            if (gconn.State == ConnectionState.Closed)
                gconn.Open();
            try
            {
                cm.Connection = gconn;
                cm.CommandText = "sp_PQ_Quyen_control";
                cm.CommandType = CommandType.StoredProcedure;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@USERID";
                p.Direction = ParameterDirection.Input;
                p.SqlDbType = SqlDbType.NVarChar;
                p.Value = UserName;
                cm.Parameters.Add(p);

                p = new SqlParameter();
                p.ParameterName = "@PROGID";
                p.Direction = ParameterDirection.Input;
                p.SqlDbType = SqlDbType.NVarChar;
                p.Value = ProgramID;
                cm.Parameters.Add(p);

                SqlDataAdapter da = new SqlDataAdapter(cm);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load quyền control\n" + ex.Message);
            }
            return dt;
        }

        public SqlConnection GetSQLConnection()
        {
            return m_conn_sql;
        }

        public bool ExecFullConnect_SQL(string ConnTitle)
        {
            frmLoginFull dlg = new frmLoginFull();
            dlg.mLogin = this;
            dlg.mConnTitle = ConnTitle;
            dlg.mUseSQLConnection = true;
            dlg.ShowDialog();
            Admin = dlg.IsAdmin;
            return dlg.mOk;

        }

        public void CheckLicense()
        {
            UpdateLicense UDL = new UpdateLicense();
            UDL.sAppDataPath = AppPath;
            UDL.sProgramID = ProgramID;
            UDL.ShowDialog();
            bHasLicense = UDL.bHasLicense;
            infolicense = UDL.sLicenseStatus;
            UDL.Dispose();
        }

        public void Update()
        {
            UpdateVersion UV = new UpdateVersion();
            UV.fileName = sLinkFileUpdate;
            UV.ServerName = ServerName;
            UV.localPath = AppPath;
            UV.ShowDialog();
            sLinkFileUpdate = UV.fileName;
            CompleteUpdate = UV.complete;
            UV.Dispose();
        }

        public string CheckVersion(string sComputerName, string sDBName, string sUserName, string sPassword)
        {
            string newversion = null;
            //string data;

            DataTable dt = new DataTable();
            SqlCommand cm = new SqlCommand();
            SqlConnection gconn = this.GetSQLConnection();
            if (gconn.State == ConnectionState.Closed)
                gconn.Open();
            try
            {
                cm.Connection = gconn;
                cm.CommandType = CommandType.Text;
                cm.CommandText = "select * from Q_VERSION";

                SqlDataAdapter da = new SqlDataAdapter(cm);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[dt.Rows.Count - 1];
                    newversion = dr["TenPhienBan"].ToString();
                    sLinkFileUpdate = dr["LinkFileCapNhat"].ToString();
                }

                return newversion;
            }
            catch
            {
                return null;
            }
        }

        public void capnhat_User_ACCess()
        {
            frmUser_Access frm = new frmUser_Access();
            frm.ID_chuongtrinh = ProgramID;
            frm.gconn = m_conn_sql;
            frm.phanquyen = this;
            frm.ID_ACCess = ID_Access;
            frm.IsAdmin = this.Admin;
            frm.ShowDialog();
            frm.Dispose();
        }

        public void capnhat_User_ACCess(Quyen_truycap quyen, SqlConnection cnn, string IDChuongtrinh, bool isAdmin)
        {
            frmUser_Access frm = new frmUser_Access();
            frm.ID_chuongtrinh = ProgramID;
            frm.gconn = cnn;
            frm.phanquyen = this;
            frm.ID_ACCess = this.ID_Access;
            frm.IsAdmin = isAdmin;
            frm.ShowDialog();
            frm.Dispose();
        }

    }
}
