using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace ESLogin
{
    public class BUS_UserManagement
    {
        #region Constructor
        private DAL_UserManagement _dal;
        /// <summary>
        /// Khởi tạo dùng kết nối mặc định
        /// </summary>
        public BUS_UserManagement()
        {
            _dal = new DAL_UserManagement();
        }

        /// <summary>
        /// Khởi tạo dùng kết nối tạm (đến database khác)
        /// </summary>
        /// <param name="strConn"></param>
        public BUS_UserManagement(string strConn)
        {
            _dal = new DAL_UserManagement(strConn);
        }

        /// <summary>
        /// Khởi tạo dùng kết nối tạm (đến database khác)
        /// </summary>
        /// <param name="sComputerName"></param>
        /// <param name="sDBName"></param>
        /// <param name="sUserName"></param>
        /// <param name="sPassword"></param>
        public BUS_UserManagement(string sComputerName, string sDBName, string sUserName, string sPassword)
        {
            _dal = new DAL_UserManagement(sComputerName, sDBName, sUserName, sPassword);
        }
        #endregion

        #region method

        #region Quản lý người dùng

        /// <summary>
        /// Lấy danh sách tất cả các user
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable Q_USER_SelectAll()
        {
            return _dal.Q_USER_SelectAll();
        }

        /// <summary>
        /// Lấy ra thông tin của 1 người dùng cụ thể
        /// </summary>
        /// <param name="UserID">Tên đăng nhập</param>
        /// <returns>DataTable</returns>
        public DataTable Q_USER_SelectByUsername(string UserID)
        {
            return _dal.Q_USER_SelectByUsername(UserID);
        }

        public DataTable Q_USER_TYPE_SelectByDVPD(string ma_dvpd)
        {
            return _dal.Q_USER_TYPE_SelectByDVPD(ma_dvpd);
        }

        /// <summary>
        /// Xóa quyền truy cập chương trình của user, nếu user không còn quyền truy cập vào chương trình nào thì xóa user khỏi bảng user
        /// </summary>
        /// <param name="UserID">userID</param>
        /// <param name="ProgID">programID</param>
        public void Q_USER_Delete(string Username)
        {
            _dal.Q_USER_Delete(Username);
        }

        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="Username">tên đăng nhập</param>
        /// <param name="UserName">tên người dùng</param>
        /// <param name="Passwords">mật khẩu</param>
        /// <param name="Descript">mô tả</param>
        /// <param name="hasQuyen">có quyền truy cập vào chương trình hay không</param>
        /// <param name="ProgID">ID của chương trình</param>
        public void Q_USER_InsertUpdate(string Username, string Fullname, string Passwords, string Descript)
        {
            _dal.Q_USER_InsertUpdate(Username, Fullname, Passwords, Descript);
        }

        /// <summary>
        /// Thay đổi mật khẩu của user
        /// </summary>
        /// <param name="Username">tên đăng nhập</param>
        /// <param name="Passwords">mật khẩu</param>
        public void Q_USER_ChangePassword(string Username, string Passwords)
        {
            _dal.Q_USER_ChangePassword(Username, Passwords);
        }

        #endregion

        #region Quản lý nhóm quyền

        /// <summary>
        /// Lấy ra danh sách nhóm quyền của chương trình
        /// </summary>
        /// <param name="ProgID">ID chương trình</param>
        /// <returns>DataTable</returns>
        public DataTable Q_ROLE_SelectByProgID(string ProgID)
        {
            return _dal.Q_ROLE_SelectByProgID(ProgID);
        }

        /// <summary>
        /// Xóa một nhóm quyền
        /// </summary>
        /// <param name="RoleID">ID của nhóm quyền</param>
        public void Q_ROLE_DeleteByRoleID(short RoleID)
        {
            _dal.Q_ROLE_DeleteByRoleID(RoleID);
        }

        /// <summary>
        /// Cập nhật lại một nhóm quyền
        /// </summary>
        /// <param name="RoleID">ID của nhóm quyền</param>
        /// <param name="RoleName">tên nhóm quyền</param>
        /// <param name="Descript">mô tả nhóm quyền</param>
        /// <param name="ProgID">ID của chương trình</param>
        public void Q_ROLE_InsertUpdate(short RoleID, string RoleName, string Descript, string ProgID)
        {
            _dal.Q_ROLE_InsertUpdate(RoleID, RoleName, Descript, ProgID);
        }

        #endregion

        #region Phân quyền cho nhóm quyền
        /// <summary>
        /// Lấy ra danh sách các module có trong chương trình
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable Q_Module_Select()
        {
            return _dal.Q_Module_Select();
        }

        public DataTable Q_Module_SelectByDVPD(string ma_dvpd)
        {
            return _dal.Q_Module_SelectByDVPD(ma_dvpd);
        }

        public DataTable Q_FUNCTION_SelectModule()
        {
            return _dal.Q_FUNCTION_SelectModule();
        }

        /// <summary>
        /// Lấy ra danh sách các quyền tương ứng với Module của chương trình
        /// </summary>
        /// <param name="ProgID">ID của chương trình</param>
        /// <param name="ModID">ID của Module</param>
        /// <returns>DataTable</returns>
        public DataTable Q_Function_SelectAll()
        {
            return _dal.Q_Function_SelectAll();
        }

        /// <summary>
        /// Lấy ra danh sách quyền của từng nhóm quyền trong chương trình
        /// </summary>
        /// <param name="ProgID">ID chương trình</param>
        /// <param name="RoleID">ID nhóm quyền</param>
        /// <returns>DataTable</returns>
        public DataTable Q_PQFUNCTION_ROLE_SelectByRoleID(string ProgID, short RoleID)
        {
            return _dal.Q_PQFUNCTION_ROLE_SelectByRoleID(ProgID, RoleID);
        }

        /// <summary>
        /// Cập nhật quyền cho nhóm quyền
        /// </summary>
        /// <param name="RoleID">ID nhóm quyền</param>
        /// <param name="FunctionID">ID quyền</param>
        public void Q_PQFUNCTION_ROLE_Insert(short RoleID, string FunctionID)
        {
            _dal.Q_PQFUNCTION_ROLE_Insert(RoleID, FunctionID);
        }

        /// <summary>
        /// Xóa quyền của nhóm quyền
        /// </summary>
        /// <param name="RoleID">ID nhóm quyền</param>
        /// <param name="FunctionID">ID quyền</param>
        public void Q_PQFUNCTION_ROLE_Delete(short RoleID, string FunctionID)
        {
            _dal.Q_PQFUNCTION_ROLE_Delete(RoleID, FunctionID);
        }

        #endregion

        #region Phân người dùng vào nhóm quyền

        /// <summary>
        /// Lấy ra danh sách phân quyền người dùng vào nhóm quyền của chương trình
        /// </summary>
        /// <param name="ProgID">ID chương trình</param>
        /// <returns>DataTable</returns>
        public DataTable Q_USER_ROLE_SelectByProgID(string ProgID)
        {
            return _dal.Q_USER_ROLE_SelectByProgID(ProgID);
        }

        /// <summary>
        /// Bỏ người dùng khỏi nhóm quyền
        /// </summary>
        /// <param name="UserID">tên đăng nhập</param>
        /// <param name="RoleID">ID của nhóm quyền</param>
        public void Q_USER_ROLE_Delete(string UserID, short RoleID)
        {
            _dal.Q_USER_ROLE_Delete(UserID, RoleID);
        }

        /// <summary>
        /// Thêm người dùng vào nhóm quyền
        /// </summary>
        /// <param name="UserID">tên đăng nhập</param>
        /// <param name="RoleID">ID của nhóm quyền</param>
        public void Q_USER_ROLE_Insert(string UserID, short RoleID)
        {
            _dal.Q_USER_ROLE_Insert(UserID, RoleID);
        }

        #endregion

        #region Phân quyền cho người dùng

        /// <summary>
        /// Lấy ra danh sách các quyền tương ứng của User trong chương trình
        /// </summary>
        /// <param name="UserID">tên đăng nhập</param>
        /// <param name="ProgID">ID của chương trình</param>
        /// <returns>DataTable</returns>
        public DataTable Q_USER_FUNCTION_SelectByUsername(string Username)
        {
            return _dal.Q_USER_FUNCTION_SelectByUsername(Username);
        }

        /// <summary>
        /// Thêm quyền chức năng cho người dùng
        /// </summary>
        /// <param name="username">tên đăng nhập</param>
        /// <param name="functionID">ID chức năng</param>
        public void Q_USER_FUNCTION_Insert(string username, int functionID)
        {
            _dal.Q_USER_FUNCTION_Insert(username, functionID);
        }

        /// <summary>
        /// Bỏ quyền chức năng cho người dùng
        /// </summary>
        /// <param name="username">tên đăng nhập</param>
        /// <param name="functionID">ID chức năng</param>
        public void Q_USER_FUNCTION_Delete(string username, int functionID)
        {
            _dal.Q_USER_FUNCTION_Delete(username, functionID);
        }

        #endregion

        #endregion
    }

    public class DAL_UserManagement
    {
        #region Private Members & Constructors
        /// <summary>
        /// SQL Connection
        /// </summary>
        private DAL_SqlConnector sc;

        /// <summary>
        /// Constructs new SqlDataProvider instance use default connection
        /// </summary>
        public DAL_UserManagement()
        {
            sc = new DAL_SqlConnector();
        }

        /// <summary>
        /// Constructs new SqlDataProvider instance with specific connection
        /// </summary>
        public DAL_UserManagement(string strConn)
        {
            sc = new DAL_SqlConnector(strConn);
        }

        /// <summary>
        /// Constructs new SqlDataProvider instance with specific connection
        /// </summary>
        public DAL_UserManagement(string sComputerName, string sDBName, string sUserName, string sPassword)
        {
            sc = new DAL_SqlConnector(sComputerName, sDBName, sUserName, sPassword);
        }
        #endregion

        #region method

        #region Quản lý người dùng

        public DataTable Q_USER_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_USER_SelectAll";
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

        public DataTable Q_USER_SelectByUsername(string UserID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_USER_SelectByUsername";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@Username", UserID);

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

        public void Q_USER_Delete(string Username)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_USER_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@Username", Username);

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

        public void Q_USER_InsertUpdate(string Username, string Fullname, string Passwords, string Descript)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_USER_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@USERNAME", SqlDbType.NVarChar, 25, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Username));
                cmd.Parameters.Add(new SqlParameter("@FULLNAME", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Fullname));
                cmd.Parameters.Add(new SqlParameter("@PASSWORDS", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Passwords));
                cmd.Parameters.Add(new SqlParameter("@DESCRIPT", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Descript));

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

        public void Q_USER_ChangePassword(string Username, string Passwords)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_USER_ChangePassword";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@USERNAME", SqlDbType.NVarChar, 25, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Username));
                cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Passwords));
                
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

        #endregion

        #region loại user
        public DataTable Q_USER_TYPE_SelectByDVPD(string ma_dvpd)
        {
            //return db.ExecuteReader("TTD_NEW_Q_USER_TYPE_SelectByDVPD", ma_dvpd);

            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_USER_TYPE_SelectByDVPD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ma_dvpd", ma_dvpd);

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

        #region Quản lý nhóm quyền

        public DataTable Q_ROLE_SelectByProgID(string ProgID)
        {
            //return db.ExecuteReader("TTD_NEW_Q_ROLE_SelectByProgID", ProgID);

            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_ROLE_SelectByProgID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ProgID", ProgID);

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

        public void Q_ROLE_DeleteByRoleID(short RoleID)
        {
            //db.ExecuteNonQuery("TTD_NEW_Q_ROLE_DeleteByRoleID", RoleID);

            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_ROLE_DeleteByRoleID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@RoleID", RoleID);

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

        public void Q_ROLE_InsertUpdate(short RoleID, string RoleName, string Descript, string ProgID)
        {
            //db.ExecuteNonQuery("TTD_NEW_Q_ROLE_InsertUpdate", RoleID, RoleName, Descript, ProgID);

            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_ROLE_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.SmallInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, RoleID));
                cmd.Parameters.Add(new SqlParameter("@RoleName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, RoleName));
                cmd.Parameters.Add(new SqlParameter("@DESCRIPT", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Descript));
                cmd.Parameters.Add(new SqlParameter("@ProgID", SqlDbType.NVarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ProgID));
                
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

        #endregion

        #region Phân quyền cho nhóm quyền

        public DataTable Q_Module_Select()
        {
            //return db.ExecuteReader("TTD_NEW_Q_Module_Select");

            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "TTD_NEW_Q_Module_Select";
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

        public DataTable Q_Module_SelectByDVPD(string ma_dvpd)
        {
            //return db.ExecuteReader("TTD_NEW_Q_Module_SelectByDVPD", ma_dvpd);

            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_Module_SelectByDVPD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ma_dvpd", ma_dvpd);

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

        public DataTable Q_FUNCTION_SelectModule()
        {
            //return db.ExecuteReader("TTD_NEW_Q_FUNCTION_SelectModuleByProgID", ProgID);

            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_FUNCTION_SelectModule";
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

        public DataTable Q_Function_SelectAll()
        {
            //return db.ExecuteReader("TTD_NEW_Q_Function_SelectByProgID_ModID", ProgID, ModID, LoaiHinhSX);

            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_Function_SelectAll";
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

        public DataTable Q_PQFUNCTION_ROLE_SelectByRoleID(string ProgID, short RoleID)
        {
            //return db.ExecuteReader("TTD_NEW_Q_PQFUNCTION_ROLE_SelectByRoleID", ProgID, RoleID);

            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_PQFUNCTION_ROLE_SelectByRoleID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ProgID", ProgID);
                cmd.Parameters.AddWithValue("@RoleID", RoleID);

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

        public void Q_PQFUNCTION_ROLE_Insert(short RoleID, string FunctionID)
        {
            //db.ExecuteNonQuery("TTD_NEW_Q_PQFUNCTION_ROLE_Insert", RoleID, FunctionID);

            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_PQFUNCTION_ROLE_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.SmallInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, RoleID));
                cmd.Parameters.Add(new SqlParameter("@FunctionID", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FunctionID));

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

        public void Q_PQFUNCTION_ROLE_Delete(short RoleID, string FunctionID)
        {
            //db.ExecuteNonQuery("TTD_NEW_Q_PQFUNCTION_ROLE_Delete", RoleID, FunctionID);

            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_PQFUNCTION_ROLE_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@RoleID", RoleID);
                cmd.Parameters.AddWithValue("@FunctionID", FunctionID);

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

        #endregion

        #region Phân người dùng vào nhóm quyền

        public DataTable Q_USER_ROLE_SelectByProgID(string ProgID)
        {
            //return db.ExecuteReader("TTD_NEW_Q_USER_ROLE_SelectByProgID", ProgID);

            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_USER_ROLE_SelectByProgID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ProgID", ProgID);

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

        public void Q_USER_ROLE_Delete(string UserID, short RoleID)
        {
            //db.ExecuteNonQuery("TTD_NEW_Q_USER_ROLE_Delete", UserID, RoleID);

            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_USER_ROLE_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@RoleID", RoleID);

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

        public void Q_USER_ROLE_Insert(string UserID, short RoleID)
        {
            //db.ExecuteNonQuery("TTD_NEW_Q_USER_ROLE_Insert", UserID, RoleID);

            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "TTD_NEW_Q_USER_ROLE_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.NVarChar, 25, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, UserID));
                cmd.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.SmallInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, RoleID));

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

        #endregion

        #region Phân quyền cho người dùng

        public DataTable Q_USER_FUNCTION_SelectByUsername(string Username)
        {
            //return db.ExecuteReader("TTD_NEW_Q_PQFUNCTION_USER_SelectByUserID", UserID, ProgID);

            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_USER_FUNCTION_SelectByUsername";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@USERNAME", Username);

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

        public void Q_USER_FUNCTION_Insert(string username, int functionID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_USER_FUNCTION_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@USERNAME", SqlDbType.NVarChar, 25, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, username));
                cmd.Parameters.Add(new SqlParameter("@FUNCTIONID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, functionID));

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

        public void Q_USER_FUNCTION_Delete(string username, int functionID)
        {
            //db.ExecuteNonQuery("TTD_NEW_Q_PQFUNCTION_USER_Delete", UserID, FunctionID);

            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_USER_FUNCTION_Delete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@USERNAME", username);
                cmd.Parameters.AddWithValue("@FUNCTIONID", functionID);

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
        
        #endregion

        #endregion
    }

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
