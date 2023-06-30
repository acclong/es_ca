using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ES.CA_ManagementDAL
{
    public partial class DALQuanTri
    {
        #region CA_Program

        public DataTable CA_Program_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Program_SelectAll";
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

        public DataTable CA_Program_SelectByProgID(int iProgID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Program_SelectByProgID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ProgID", iProgID).Direction = ParameterDirection.Input;

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

        public DataTable CA_Program_SelectByProgName(string sProgName)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Program_SelectByProgName";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ProgName", sProgName).Direction = ParameterDirection.Input;

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

        public DataTable CA_Program_SelectByNotation(string sNotation)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Program_SelectByNotation";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@Notation", sNotation).Direction = ParameterDirection.Input;

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

        public void CA_Program_InsertUpdate(int iProgID, string sProgName, string sName, string sNotation, int iStatus, string sServerName, string sDBName,
            string sUserDB, string sPassword, string sQueryUser, string sUserModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Program_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@ProgID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iProgID));
                cmd.Parameters.Add(new SqlParameter("@ProgName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sProgName));
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sName));
                cmd.Parameters.Add(new SqlParameter("@Notation", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sNotation));
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iStatus));
                cmd.Parameters.Add(new SqlParameter("@ServerName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sServerName));
                cmd.Parameters.Add(new SqlParameter("@DBName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sDBName));
                cmd.Parameters.Add(new SqlParameter("@UserDB", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sUserDB));
                cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sPassword));
                cmd.Parameters.Add(new SqlParameter("@QueryUser", SqlDbType.NVarChar, 2000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sQueryUser));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sUserModified));

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

        public bool CA_Program_DeleteByProgID(int progID)
        {
            bool kq = true;
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Program_DeleteByProgID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@ProgID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, progID));

                int x = cmd.ExecuteNonQuery();
                if (x > 0) kq = true;
                else kq = false;
                connection.Close();
            }
            catch (Exception ex)
            {
                kq = false;
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
            return kq;
        }

        #endregion

        #region CA_Program_Log

        public DataTable CA_Program_Log_SelectBy_ProgID(int iProgID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Program_Log_SelectBy_ProgID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ProgID", iProgID).Direction = ParameterDirection.Input;

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

        #region CA_Unit

        public DataTable CA_Unit_Select_Check(int ID_UserProg, string Seach)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Unit_Select_Check";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ID_UserProg", ID_UserProg).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@Seach", Seach).Direction = ParameterDirection.Input;

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

        public bool CA_Unit_DeleteBy_UnitID(int unitID)
        {
            bool kq = true;
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Unit_DeleteBy_UnitID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, unitID));

                int x = cmd.ExecuteNonQuery();
                if (x > 0) kq = true;
                else kq = false;
                connection.Close();
            }
            catch (Exception ex)
            {
                kq = false;
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
            return kq;
        }

        public DataTable CA_Unit_SelectUnitName()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Unit_SelectUnitName";
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

        public DataTable CA_Unit_SelectByAll_UserUnit()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Unit_SelectByAll_UserUnit";
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

        public DataTable CA_Unit_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Unit_SelectAll";
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

        public DataTable CA_Unit_SelectByUnitID(int unitID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Unit_SelectByUnitID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@UnitID", unitID).Direction = ParameterDirection.Input;

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

        public DataTable CA_Unit_SelectBy_UserIDQuyen(int userID, int progID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Unit_SelectBy_UserIDQuyen";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@UserID", userID).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@ProgID", progID).Direction = ParameterDirection.Input;

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

        public DataTable CA_Unit_SelectBy_UnitTypeID(int unitTypeID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Unit_SelectBy_UnitTypeID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@UnitTypeID", unitTypeID).Direction = ParameterDirection.Input;

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

        public DataTable CA_Unit_SelectBy_UnitTypeID_Status(int unitTypeID, int status)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Unit_SelectBy_UnitTypeID_Status";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@UnitTypeID", unitTypeID).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@Status", status).Direction = ParameterDirection.Input;

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

        public DataTable CA_Unit_SelectBy_Notation_UnitTypeID(string notation, int unitTypeID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Unit_SelectBy_Notation_UnitTypeID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@Notation", notation).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@UnitTypeID", unitTypeID).Direction = ParameterDirection.Input;

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

        public DataTable CA_Unit_SelectByNotation(string sNotation)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Unit_SelectByNotation";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@Notation", sNotation).Direction = ParameterDirection.Input;

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

        public DataTable CA_Unit_SelectByMaDV(string sMaDV)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Unit_SelectByMaDV";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@MaDV", sMaDV).Direction = ParameterDirection.Input;

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

        public DataTable CA_Unit_SelectByFileTypeID(int fileTypeId)
        {
            DataTable dtKQ = new DataTable();
            SqlConnection connection = sc.GetConnection();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "CA_Unit_SelectByFileTypeID";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;

                command.Parameters.AddWithValue("@FileTypeID", fileTypeId).Direction = ParameterDirection.Input;

                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(dtKQ);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return dtKQ;
        }

        public DataTable CA_Unit_SelectAllUnitHasUnmappingUser()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "CA_Unit_SelectAllUnitHasUnmappingUser";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void CA_Unit_InsertUpdate(int iUnitID, string sMaDV, string sName, string sNotation, int iStatus, DateTime validFrom,
            DateTime validTo, int iUnitTypeID, int iParentID, string sTenTat, int mien, string sUserModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Unit_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iUnitID));
                cmd.Parameters.Add(new SqlParameter("@MaDV", SqlDbType.NVarChar, 14, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sMaDV));
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sName));
                cmd.Parameters.Add(new SqlParameter("@Notation", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sNotation));
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iStatus));
                if (validFrom == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validFrom));

                if (validTo == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validTo));

                cmd.Parameters.Add(new SqlParameter("@UnitTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iUnitTypeID));

                if (iParentID == int.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@ParentID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@ParentID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iParentID));

                cmd.Parameters.Add(new SqlParameter("@TenTat", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sTenTat));

                if (mien == -1)
                    cmd.Parameters.Add(new SqlParameter("@MienId", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@MienId", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, mien));

                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sUserModified));

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

        #region CA_Unit_Log

        public DataTable CA_Unit_Log_SelectBy_UnitID(int unitID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Unit_Log_SelectBy_UnitID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@UnitID", unitID).Direction = ParameterDirection.Input;

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

        #region CA_User

        public bool CA_User_DeleteBy_UserID(int userID)
        {
            bool kq = true;
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_User_DeleteBy_UserID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userID));

                int x = cmd.ExecuteNonQuery();
                if (x > 0) kq = true;
                else kq = false;
                connection.Close();
            }
            catch (Exception ex)
            {
                kq = false;
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
            return kq;
        }

        public DataTable CA_User_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_User_SelectAll";
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

        public DataTable CA_User_SelectBy_Status_Seach(string seach, int status)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_User_SelectBy_Status_Seach";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@Seach", seach).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@Status", status).Direction = ParameterDirection.Input;

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

        public DataTable CA_User_SelectByUserID(int userID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_User_SelectByUserID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@UserID", userID).Direction = ParameterDirection.Input;

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

        public DataTable CA_User_SelectByCMND(string CMND)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_User_SelectByCMND";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@CMND", CMND).Direction = ParameterDirection.Input;

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

        public DataTable CA_User_SelectByUnitID(int unitID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_User_SelectByUnitID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@UnitID", unitID).Direction = ParameterDirection.Input;

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

        public DataTable CA_User_SelectBy_CertID(int certID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_User_SelectBy_CertID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@CertID", certID).Direction = ParameterDirection.Input;

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

        public DataTable CA_User_SelectAllUnmappingUser()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "CA_User_SelectAllUnmappingUser";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void CA_User_InsertUpdate(int iUserID, string sName, string CMND, int iStatus, DateTime validFrom, DateTime validTo, string email, string sUserModified, int unitID, string description, int certID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_User_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iUserID));
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sName));
                cmd.Parameters.Add(new SqlParameter("@CMND", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, CMND));
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, email));
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iStatus));

                if (validFrom == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validFrom));

                if (validTo == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validTo));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sUserModified));
                //Edited by Hieutm on 15/6/2015
                //Thêm trường Mã đơn vị, Mô tả

                cmd.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, unitID));
                cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, description));
                cmd.Parameters.Add(new SqlParameter("@CertID", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certID));

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

        #region CA_User_Log

        public DataTable CA_User_Log_SelectBy_UserID(int userID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_User_Log_SelectBy_UserID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@UserID", userID).Direction = ParameterDirection.Input;

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

        #region CA_Certificate

        public DataTable CA_Certificate_SelectBy_Seach_Status(string seach, int status)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Certificate_SelectBy_Seach_Status";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@Seach", seach).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@Status", status).Direction = ParameterDirection.Input;

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

        public bool CA_Certificate_DeleteBy_CertID(int certID)
        {
            bool kq = true;
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Certificate_DeleteBy_CertID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@CertID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certID));

                int x = cmd.ExecuteNonQuery();
                if (x > 0) kq = true;
                else kq = false;
                connection.Close();
            }
            catch (Exception ex)
            {
                kq = false;
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
            return kq;
        }

        public DataTable CA_Certificate_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Certificate_SelectAll";
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

        public DataTable CA_Certificate_SelectBy_NotUse(int userID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Certificate_SelectBy_NotUse";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userID));

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

        public DataTable CA_Certificate_SelectRawDataByID(int id)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Certificate_SelectRawDataByID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ID ", id).Direction = ParameterDirection.Input;

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

        public DataTable CA_Certificate_SelectBySerial(string serial)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Certificate_SelectBySerial";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@Serial ", serial).Direction = ParameterDirection.Input;

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

        public DataTable CA_Certificate_SelectByCertID(int id)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Certificate_SelectByCertID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@CertID", id).Direction = ParameterDirection.Input;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void CA_Certificate_Insert(X509Certificate2 cert, String username, int status, int issuerID, int certType)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "CA_Certificate_Insert";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.Add(new SqlParameter("@NameCN", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.GetNameInfo(X509NameType.DnsName, false)));
                command.Parameters.Add(new SqlParameter("@Serial", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.SerialNumber));
                command.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, status));
                command.Parameters.Add(new SqlParameter("@Subject", SqlDbType.NVarChar, 300, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.Subject));
                command.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.NotBefore));
                command.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.NotAfter));
                command.Parameters.Add(new SqlParameter("@ThumbPrint", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.Thumbprint));
                command.Parameters.Add(new SqlParameter("@RawData", SqlDbType.VarBinary, cert.RawData.Length, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.RawData));
                command.Parameters.Add(new SqlParameter("@IssuerID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, issuerID));
                command.Parameters.Add(new SqlParameter("@CertType", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certType));
                command.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, username));
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void CA_Certificate_Update(int certID, int status, string userModified, int certType)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "CA_Certificate_Update_Status_Type";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@CertID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certID));
                command.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, status));
                command.Parameters.Add(new SqlParameter("@CertType", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certType));
                command.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public int CA_Certificate_InsertUpdate_OutCertID(X509Certificate2 cert, String username, int status)
        {
            int newId = 0;
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "CA_Certificate_InsertUpdate_OutCertID";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.Add(new SqlParameter("@NameCN", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.GetNameInfo(X509NameType.DnsName, false)));
                command.Parameters.Add(new SqlParameter("@Serial", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.SerialNumber));
                command.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, status));
                command.Parameters.Add(new SqlParameter("@Subject", SqlDbType.NVarChar, 300, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.Subject));
                command.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.NotBefore));
                command.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.NotAfter));
                command.Parameters.Add(new SqlParameter("@ThumbPrint", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.Thumbprint));
                command.Parameters.Add(new SqlParameter("@RawData", SqlDbType.VarBinary, cert.RawData.Length, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.RawData));
                command.Parameters.Add(new SqlParameter("@IssuerID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                command.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, username));

                newId = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return newId;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
        }

        #endregion

        #region CA_Certificate_Log

        public DataTable CA_Certificate_Log_SelectBy_CertID(int certID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Certificate_Log_SelectBy_CertID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@CertID", certID).Direction = ParameterDirection.Input;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        #endregion

        #region CA_UnitProgram

        public bool CA_UnitProgram_InsertUpdate(int idUnitProg, int progID, int unitID, int status, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UnitProgram_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@ID_UnitProgram", idUnitProg));
                cmd.Parameters.Add(new SqlParameter("@ProgID", progID));
                cmd.Parameters.Add(new SqlParameter("@UnitID", unitID));
                cmd.Parameters.Add(new SqlParameter("@Status", status));
                cmd.Parameters.Add(new SqlParameter("@UserModified", userModified));

                int x = cmd.ExecuteNonQuery();
                connection.Close();
                if (x > 0)
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
            return false;
        }

        public bool CA_UnitProgram_InsertUpdate(DataTable oldData, DataTable newData, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            bool result = true;
            try
            {
                for (int i = 0; i < oldData.Rows.Count; i++)
                {
                    cmd = new SqlCommand();
                    int idUnitProg = Convert.ToInt32(oldData.Rows[i]["ID_UnitProgram"]);
                    int progID = Convert.ToInt32(oldData.Rows[i]["ProgID"]);
                    int unitID = Convert.ToInt32(oldData.Rows[i]["UnitID"]);
                    int statusold = Convert.ToInt32(oldData.Rows[i]["Status"]);
                    int statusnew = Convert.ToInt32(newData.Rows[i]["Status"]);

                    if (statusold != statusnew)
                    {
                        cmd.CommandText = "CA_UnitProgram_InsertUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Transaction = transaction;

                        cmd.Parameters.Add(new SqlParameter("@ID_UnitProgram", idUnitProg));
                        cmd.Parameters.Add(new SqlParameter("@ProgID", progID));
                        cmd.Parameters.Add(new SqlParameter("@UnitID", unitID));
                        cmd.Parameters.Add(new SqlParameter("@Status", statusnew));
                        cmd.Parameters.Add(new SqlParameter("@UserModified", userModified));

                        cmd.ExecuteNonQuery();
                    }
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
                result = false;
            }
            return result;
        }

        public DataTable CA_UnitProgram_SelectBy_ProgID_Status_Seach(int status, string seach, int progID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UnitProgram_SelectBy_ProgID_Status_Seach";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@Status", status).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@Seach", seach).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@ProgID ", progID).Direction = ParameterDirection.Input;

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

        public DataTable CA_UnitProgram_SelectBy_Status_Seach(int status, string seach)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UnitProgram_SelectBy_Status_Seach";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@Status", status).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@Seach", seach).Direction = ParameterDirection.Input;

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

        public DataTable CA_UnitProgram_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UnitProgram_SelectAll";
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

        public DataTable CA_UnitProgram_SelectByProgID(int progID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UnitProgram_SelectByProgID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ProgID", progID).Direction = ParameterDirection.Input;

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

        public DataTable CA_UnitProgram_SelectBy_IDUnitProg(int id_UnitProg)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UnitProgram_SelectBy_IDUnitProg";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ID_UnitProg", id_UnitProg).Direction = ParameterDirection.Input;

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

        #region CA_UnitProgram_Log

        public DataTable CA_UnitProgram_Log_SelectBy_IDUnitProgram(int id_UnitProgram)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UnitProgram_Log_SelectBy_IDUnitProgram";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ID_UnitProgram", id_UnitProgram).Direction = ParameterDirection.Input;

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

        #region CA_UserProgram

        public DataTable CA_UserProgram_SelectBy_UserProgName_ProgID_UserID(int progID, int userID, string userprogname)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProgram_SelectBy_UserProgName_ProgID_UserID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ProgID", progID).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@UserID", userID).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@UserProgName", userprogname).Direction = ParameterDirection.Input;

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

        public DataSet CA_UserProgram_SelectBy_ProgID_SignTypeID_Seach(int progID, int signTypeID, string seach)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProgram_SelectBy_ProgID_SignTypeID_Seach";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ProgID", progID).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@SignType", signTypeID).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@Seach", seach).Direction = ParameterDirection.Input;

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

        public DataTable CA_UserProgram_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProgram_SelectAll";
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

        public DataTable CA_UyQuyen_SelectBy_NguoiUyQuyen(int User_UyQuyen)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UyQuyen_SelectBy_NguoiUyQuyen";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@User_UyQuyen", User_UyQuyen).Direction = ParameterDirection.Input;

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

        public DataTable CA_UyQuyen_QuyenUnit_SelectBy_ID_UyQuyen(int ID_UyQuyen)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UyQuyen_QuyenUnit_SelectBy_ID_UyQuyen";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ID_UyQuyen", ID_UyQuyen).Direction = ParameterDirection.Input;

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

        public DataTable CA_UserProgram_SelectBy_IDUyQuyen_IDDuocUyQuyen(int ID_UQ, int ID_UyQuyen, int ID_DuocUyQuyen)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProgram_SelectBy_IDUyQuyen_IDDuocUyQuyen";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ID_UQ", ID_UQ).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@ID_UyQuyen", ID_UyQuyen).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@ID_DuocUyQuyen", ID_DuocUyQuyen).Direction = ParameterDirection.Input;

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

        public DataTable CA_UserProg_SelectBy_UserID(int iUserID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProg_SelectBy_UserID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@UserID", iUserID).Direction = ParameterDirection.Input;

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

        public DataTable CA_Program_SelectBy_UserID(int iUserID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Program_SelectBy_UserID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@UserID", iUserID).Direction = ParameterDirection.Input;

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

        public DataTable CA_UserProgram_SelectByUserProgID(int iUserProgID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProgram_SelectByUserProgID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ID_UserProg", iUserProgID).Direction = ParameterDirection.Input;

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

        public DataTable CA_UserProgram_SelectByUserID_ProgID(int iUserID, int iProgID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProgram_SelectByUserID_ProgID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iUserID));
                cmd.Parameters.Add(new SqlParameter("@ProgID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iProgID));

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

        public DataTable CA_UserProgram_SelectBy_UserProgName_ProgID(string iUserProgName, int iProgID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProgram_SelectBy_UserProgName_ProgID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@UserProgName", SqlDbType.NVarChar, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iUserProgName));
                cmd.Parameters.Add(new SqlParameter("@ProgID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iProgID));

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

        public int CA_UserProgram_InsertUpdate(int iUserProgID, int iUserID, int iProgID, string sUserProgName, DateTime validFrom,
                                                DateTime validTo, string sUserModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            int result = -1;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProgram_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                SqlParameter ID_UserProg_Out = new SqlParameter("@ID_UserProg", SqlDbType.Int, 8, ParameterDirection.InputOutput, false, 0, 0, "", DataRowVersion.Proposed, iUserProgID);

                cmd.Parameters.Add(ID_UserProg_Out);
                cmd.Parameters.Add(new SqlParameter("@ProgID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iProgID));
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iUserID));
                if (isEmpty(sUserProgName))
                    cmd.Parameters.Add(new SqlParameter("@UserProgName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@UserProgName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sUserProgName));
                cmd.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validFrom));
                if (validTo == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validTo));

                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sUserModified));

                int x = cmd.ExecuteNonQuery();
                //if (x > 0)
                //{
                result = (int)ID_UserProg_Out.Value;
                //}
                connection.Close();
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                result = -1;
            }
            return result;
        }

        public DataTable CA_UserProgram_SelectByUnitID(int unitID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProgram_SelectByUnitID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@UnitID", unitID).Direction = ParameterDirection.Input;

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

        public DataTable CA_UserProgram_SelectByProgID(int progID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProgram_SelectByProgID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ProgID", progID).Direction = ParameterDirection.Input;

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

        public DataTable CA_UserProgram_SelectByUnitID_UserPro(int unitID, string userProg)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProgram_SelectByUnitID_UserPro";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@UnitID", unitID).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@UserProgName", userProg).Direction = ParameterDirection.Input;

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

        public DataTable CA_UserProgram_SelectByUserPro(string userProg)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProgram_SelectByUserPro";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@UserProgName", userProg).Direction = ParameterDirection.Input;

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

        #region CA_UserProgram_Log

        public DataTable CA_UserProgram_Log_SelectBy_IDUserProgram(int id_UserProg)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProgram_Log_SelectBy_IDUserProgram";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ID_UserProg", id_UserProg).Direction = ParameterDirection.Input;

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

        #region CA_CertificateUser

        public DataTable CA_CertificateUser_SelectByID_CertUser(int id_CertUser)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_CertificateUser_SelectByID_CertUser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ID_CertUser", id_CertUser).Direction = ParameterDirection.Input;

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

        public DataTable CA_CertificateUser_SelectBy_CertID_UserID(int certID, int userID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_CertificateUser_SelectBy_CertID_UserID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userID));
                cmd.Parameters.Add(new SqlParameter("@CertID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certID));

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

        public DataTable CA_CertificateUser_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "CA_CertificateUser_SelectAll";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured");
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }

        public bool CA_CertificateUser_InsertNewMappping(int userID, int certID, DateTime validFrom, DateTime validTo, int type, String username)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "CA_CertificateUser_InsertNewMappping";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userID));
                command.Parameters.Add(new SqlParameter("@CertID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certID));
                command.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validFrom));
                command.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validTo));
                command.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, type));
                command.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, username));
                command.Connection = connection;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool CA_CertificateUser_UpdateMappping(int id_CertUser, DateTime validFrom, DateTime validTo, int type, String username)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "CA_HSMDevice_UpdatePIN";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@ID_CertUser", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, id_CertUser));
                command.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validFrom));
                command.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validTo));
                command.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, type));
                command.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, username));
                command.Connection = connection;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void CA_CertificateUser_InsertUpdate(int iUserCertID, int iUserID, int iCertID, DateTime validFrom,
            DateTime validTo, int iType, string sUserModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_CertificateUser_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@ID_CertUser", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iUserCertID));
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iUserID));
                cmd.Parameters.Add(new SqlParameter("@CertID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iCertID));
                cmd.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validFrom));
                if (validTo == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validTo));
                if (iType == -1)
                    cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iType));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sUserModified));

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

        #region CA_CertificationAuthority

        public bool CA_CertificationAuthority_DeleteBy_CertAuthID(int certAuthID)
        {
            bool kq = true;
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_CertificationAuthority_DeleteBy_CertAuthID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@CertAuthID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certAuthID));

                int x = cmd.ExecuteNonQuery();
                if (x > 0) kq = true;
                else kq = false;
                connection.Close();

                return kq;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
        }

        public DataTable CA_CertificationAuthority_SelectBySerial(string sSerial)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_CertificationAuthority_SelectBySerial";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@Serial ", sSerial).Direction = ParameterDirection.Input;

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

        public DataTable CA_CertificationAuthority_SelectRawDataByID(int id)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_CertificationAuthority_SelectRawDataByID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ID ", id).Direction = ParameterDirection.Input;

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

        public DataTable CA_CertificationAuthority_SelectAllIssuers()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "CA_CertificationAuthority_SelectAllIssuers";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured");
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public DataTable CA_CertificationAuthority_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_CertificationAuthority_SelectAll";
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

        public DataTable CA_CertificationAuthority_SelectByID(int id)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_CertificationAuthority_SelectByID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ID", id).Direction = ParameterDirection.Input;

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

        public void CA_CertificationAuthority_Insert(int certID, X509Certificate2 cert, string url, string username)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_CertificationAuthority_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@CertAuthID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certID));
                cmd.Parameters.Add(new SqlParameter("@NameCN", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.GetNameInfo(X509NameType.DnsName, false)));
                cmd.Parameters.Add(new SqlParameter("@Serial", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.SerialNumber));
                cmd.Parameters.Add(new SqlParameter("@Subject", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.Subject));
                cmd.Parameters.Add(new SqlParameter("@Issuer", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.Issuer));
                cmd.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.NotBefore));
                cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.NotAfter));
                cmd.Parameters.Add(new SqlParameter("@ThumbPrint", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.Thumbprint));
                cmd.Parameters.Add(new SqlParameter("@RawData", SqlDbType.VarBinary, cert.RawData.Length, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.RawData));
                cmd.Parameters.Add(new SqlParameter("@CRL_URL", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, url));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, username));

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

        public void CA_CertificationAuthority_Update(int certID, string url, DateTime revokedDate, string username)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_CertificationAuthority_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@CertAuthID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certID));
                cmd.Parameters.Add(new SqlParameter("@CRL_URL", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, url));
                if (revokedDate == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@RevokedFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@RevokedFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, revokedDate));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, username));

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

        #region CA_Type

        public DataTable CA_UnitType_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UnitType_SelectAll";
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

        #endregion

        #region CA_SignatureType

        public DataTable CA_SignatureType_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_SignatureType_SelectAll";
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

        #endregion

        #region CA_UserProgram_Unit

        public DataTable CA_UserProgram_Unit_SelectBy_IDUserProg(int ID_UserProg)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProgram_Unit_SelectBy_IDUserProg";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ID_UserProg", ID_UserProg).Direction = ParameterDirection.Input;

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

        public bool CA_UserProg_QuyenUnit_ResetBy_ID_UserProg(int idUserProg)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UyQuyen_QuyenUnit_ResetBy_UserUyQuyenID_ProgID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@ID_UserProg", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, idUserProg));

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return false;
            }

            return true;
        }
        #endregion

        #region CA_UyQuyen_QuyenUnit
        public bool CA_UyQuyen_QuyenUnit_ResetBy_UserUyQuyenID_ProgID(int userUyQuyenID, int progID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UyQuyen_QuyenUnit_ResetBy_UserUyQuyenID_ProgID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@UserUyQuyenID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userUyQuyenID));
                cmd.Parameters.Add(new SqlParameter("@ProgID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, progID));

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return false;
            }

            return true;
        }

        #endregion

        public DataTable CA_UserUnit_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserUnit_SelectAll";
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

        public DataTable CA_UserUnit_SelectByID(int id_UserUnit)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserUnit_SelectByID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ID_UserUnit", id_UserUnit).Direction = ParameterDirection.Input;

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

        public DataTable CA_UserUnit_SelectByUserID_UnitID(int userID, int unitID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserUnit_SelectByUserID_UnitID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userID));
                cmd.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, unitID));

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

        public void CA_UserUnit_InsertUpdate(int id_UserUnit, int userID, int unitID, string department, DateTime validFrom,
            DateTime validTo, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserUnit_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@ID_UserUnit", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, id_UserUnit));
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userID));
                cmd.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, unitID));
                cmd.Parameters.Add(new SqlParameter("@Department", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, department));
                cmd.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validFrom));
                if (validTo == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validTo));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));

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

        public DataTable CA_LichSuLienKet(DateTime date)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_LichSuLienKet";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@Date", date).Direction = ParameterDirection.Input;

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

        public bool CA_UserProg_UnitPhanQuyen_InsertUpdate(int iUserProgID, int iProg, int iUser, string userProgName, int iUnitID, DateTime validFrom, DateTime validTo, bool enable, int signatureTypeID, string sUserModified)
        {
            bool _kq = true;
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProg_UnitPhanQuyen_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@ID_UserProg", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iUserProgID));
                cmd.Parameters.Add(new SqlParameter("@ProgID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iProg));
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iUser));
                cmd.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iUnitID));
                cmd.Parameters.Add(new SqlParameter("@UserProgName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userProgName));
                cmd.Parameters.Add(new SqlParameter("@Enable", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, enable));
                if (validFrom == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validFrom));
                if (validTo == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validTo));
                if (signatureTypeID == -1)
                    cmd.Parameters.Add(new SqlParameter("@SignatureTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@SignatureTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, signatureTypeID));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sUserModified));

                int x = cmd.ExecuteNonQuery();

                connection.Close();
                if (x > 0)
                    _kq = true;
                else
                    _kq = false;
            }
            catch (Exception ex)
            {
                _kq = false;
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return _kq;
        }

        public bool CA_UserProg_UnitPhanQuyen_InsertUpdate_Array(int iUserProgID, int iUserID, int iProgID, string sUserProgName, DateTime validFrom,
                                                DateTime validTo, int[] iUnitID, bool[] enable, int[] signatureTypeID, string sUserModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                #region Cập nhật liên kết
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProgram_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;
                cmd.Transaction = transaction;

                cmd.Parameters.Add(new SqlParameter("@ID_UserProg", SqlDbType.Int, 8, ParameterDirection.InputOutput, false, 0, 0, "", DataRowVersion.Proposed, iUserProgID));
                cmd.Parameters.Add(new SqlParameter("@ProgID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iProgID));
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iUserID));
                cmd.Parameters.Add(new SqlParameter("@UserProgName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sUserProgName));
                cmd.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validFrom));
                if (validTo == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, validTo));

                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sUserModified));
                cmd.ExecuteNonQuery();

                #endregion

                //Lấy ID liên kết
                iUserProgID = (int)cmd.Parameters["@ID_UserProg"].Value;

                #region Reset CA_UserProg_QuyenUnit
                cmd = new SqlCommand();
                cmd.CommandText = "CA_UserProg_QuyenUnit_ResetBy_ID_UserProg";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;
                cmd.Transaction = transaction;

                cmd.Parameters.Add(new SqlParameter("@ID_UserProg1", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iUserProgID));

                cmd.ExecuteNonQuery();
                #endregion

                #region Cập nhật quyền đơn vị
                for (int i = 0; i < iUnitID.Count(); i++)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "CA_UserProg_QuyenUnit_InsertUpdate";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;

                    cmd.Parameters.Add(new SqlParameter("@ID_UserProgUnit", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, -1));
                    cmd.Parameters.Add(new SqlParameter("@ID_UserProg", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iUserProgID));
                    cmd.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iUnitID[i]));
                    cmd.Parameters.Add(new SqlParameter("@Enable", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, enable[i]));
                    if (signatureTypeID[i] == -1)
                        cmd.Parameters.Add(new SqlParameter("@SignatureTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                    else
                        cmd.Parameters.Add(new SqlParameter("@SignatureTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, signatureTypeID[i]));
                    cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sUserModified));
                    cmd.ExecuteNonQuery();
                }
                #endregion

                transaction.Commit();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                {
                    transaction.Rollback();
                    connection.Close();
                }
                return false;
            }
        }

        #region CA_UyQuyen
        public bool CA_UyQuyen_QuyenUnit_IsertUpdate(int ID_UyQuyen, int[] iUnitID, bool[] enable, int[] signatureTypeID, string sUserModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand();

            try
            {
                #region Cập nhật quyền đơn vị
                for (int i = 0; i < iUnitID.Count(); i++)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "CA_UyQuyen_QuyenUnit_IsertUpdate";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;

                    cmd.Parameters.Add(new SqlParameter("@ID_UyQuyen", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ID_UyQuyen));
                    cmd.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, iUnitID[i]));
                    cmd.Parameters.Add(new SqlParameter("@Enable", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, enable[i]));
                    cmd.Parameters.Add(new SqlParameter("@SignatureTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, signatureTypeID[i]));
                    cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sUserModified));
                    int x = cmd.ExecuteNonQuery();
                }
                #endregion

                transaction.Commit();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                {
                    transaction.Rollback();
                    connection.Close();
                }
                return false;
            }
        }

        public bool CA_UyQuyen_InsertUpdate(bool bInsertUpdate, int ID_UyQuyen, DataTable dtSave, int User_UyQuyen, int User_DuocUyQuyen, string sUserModified, ref List<int> lstErr)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                bool kq = false;
                for (int i = 0; i < dtSave.Rows.Count; i++)
                {
                    if (!Convert.ToBoolean(dtSave.Rows[i]["Checkbox"]))
                    {
                        continue;
                    }
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "CA_UyQuyen_InsertUpdate";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;

                    cmd.Parameters.Add(new SqlParameter("@bInsertUpdate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, bInsertUpdate));
                    //cmd.Parameters.AddWithValue("@bInsertUpdate", bInsertUpdate).Direction = ParameterDirection.ou;
                    cmd.Parameters.Add(new SqlParameter("@ID_UyQuyen", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ID_UyQuyen));
                    cmd.Parameters.Add(new SqlParameter("@User_UyQuyen", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, User_UyQuyen));
                    cmd.Parameters.Add(new SqlParameter("@User_DuocUyQuyen", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, User_DuocUyQuyen));
                    cmd.Parameters.Add(new SqlParameter("@ProgID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dtSave.Rows[i]["ProgID"]));
                    cmd.Parameters.Add(new SqlParameter("@UserProgName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dtSave.Rows[i]["UserProgName"]));
                    cmd.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dtSave.Rows[i]["ValidFrom"]));
                    cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dtSave.Rows[i]["ValidTo"]));
                    cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sUserModified));
                    cmd.Parameters.AddWithValue("@KQua", kq).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    kq = Convert.ToBoolean(cmd.Parameters["@KQua"].Value);
                    if (!kq)
                    {
                        lstErr.Add(i);
                    }
                }
                if (kq)
                {
                    transaction.Commit();
                }
                return kq;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                {
                    transaction.Rollback();
                    connection.Close();
                }
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        #endregion

    }
}
