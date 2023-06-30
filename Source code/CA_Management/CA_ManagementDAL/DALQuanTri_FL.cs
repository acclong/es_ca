using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ES.CA_ManagementDAL
{
    public partial class DALQuanTri
    {
        #region FL_File

        public DataTable FL_File_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_File_SelectAll";
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

        public DataTable FL_File_SelectByFileID(int fileID)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_File_SelectByFileID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@FileID", fileID).Direction = ParameterDirection.Input;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
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

        public DataTable FL_File_SelectByFileTypeID(int fileTypeID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_File_SelectByFileTypeID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@FileTypeID", fileTypeID).Direction = ParameterDirection.Input;

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

        public DataTable FL_File_SelectUnitIdByFileTypeID(int fileTypeId)
        {
            DataTable _kq = new DataTable();
            SqlConnection con = sc.GetConnection();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "FL_File_SelectUnitIdByFileTypeID";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = con;

                command.Parameters.AddWithValue("@FileTypeID", fileTypeId).Direction = ParameterDirection.Input;

                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(_kq);

                con.Close();
                return _kq;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw ex;
            }
        }

        public DataTable FL_File_SelectByFileTypeId_UnitId(int fileTypeId, int unitId, DateTime begin, DateTime end)
        {
            DataTable daKq = new DataTable();
            SqlConnection con = sc.GetConnection();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "FL_File_SelectByFileTypeId_UnitId";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = con;

                command.Parameters.AddWithValue("@FileTypeID", fileTypeId).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@UnitID", unitId).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Begin", begin).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@End", end).Direction = ParameterDirection.Input;

                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(daKq);

                con.Close();
                return daKq;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw ex;
            }
        }

        public DataTable FL_File_SelectByFileRelationID(int fileRelationId)
        {
            DataTable daKq = new DataTable();
            SqlConnection con = sc.GetConnection();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "FL_File_SelectByFileRelationID";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = con;

                command.Parameters.AddWithValue("@FileRelationID", fileRelationId).Direction = ParameterDirection.Input;

                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(daKq);

                con.Close();
                return daKq;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw ex;
            }
        }

        public DataTable FL_File_SelectByUnitID_FromDateToDate(int UnitID, DateTime FromDate, DateTime ToDate)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_File_SelectByUnitID_FromDateToDate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@UnitID", UnitID).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@FromDate", FromDate).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@ToDate", ToDate).Direction = ParameterDirection.Input;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
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

        public DataTable FL_File_SelectProfileTypeID_DateType_UnitID_UnitType_Date(int profileTypeId, int unitType, int unitId, int DateType, DateTime date)
        {
            DataTable daKq = new DataTable();
            SqlConnection con = sc.GetConnection();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "FL_File_SelectProfileTypeID_DateType_UnitID_UnitType_Date";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = con;

                command.Parameters.AddWithValue("@ProfileTypeID", profileTypeId).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@UnitType", unitType).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@UnitID", unitId).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@DateType", DateType).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@date", date).Direction = ParameterDirection.Input;

                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(daKq);

                con.Close();
                return daKq;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw ex;
            }
        }

        #endregion

        #region FL_LogFileSignature

        public DataTable FL_LogFileSignature_SelectByFileID(int fileID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_LogFileSignature_SelectByFileID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@FileID", fileID).Direction = ParameterDirection.Input;

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

        #region FL_LogFileStatus

        public DataTable FL_LogFileStatus_SelectByFileID(int fileID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_LogFileStatus_SelectByFileID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@FileID", fileID).Direction = ParameterDirection.Input;

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

        #region FL_FileRelation

        public DataTable FL_FileRelation_SelectByRelationTypeID(int relationTypeID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileRelation_SelectByRelationTypeID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@RelationTypeID", relationTypeID).Direction = ParameterDirection.Input;

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


        public DataTable FL_FileRelation_SelectBy_FileID(int fileID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileRelation_SelectBy_FileID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@FileID", fileID).Direction = ParameterDirection.Input;

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

        #region FL_FileProfile

        public DataTable FL_FileProfile_SelectByProfileTypeID(int profileTypeID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileProfile_SelectByProfileTypeID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ProfileTypeID", profileTypeID).Direction = ParameterDirection.Input;

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

        public DataTable FL_FileProfile_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileProfile_SelectAll";
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

        public DataTable FL_FileProfile_SelectByProfileTypeID_Date_Search(int profileTypeID, DateTime date, string search)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileProfile_SelectByProfileTypeID_Date_Search";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@ProfileTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, profileTypeID));
                if (date != DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, date));
                else
                    cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Search", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, search));

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

        public DataTable FL_FileProfile_SelectByIDFileProfile(int idFileProfile)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileProfile_SelectByIDFileProfile";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ID_FileProfile", idFileProfile).Direction = ParameterDirection.Input;

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

        public DataTable FL_FileProfile_SelectByFileTypeID_ProfileTypeID(int fileTypeID, int profileTypeID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileProfile_SelectByFileTypeID_ProfileTypeID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@FileTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fileTypeID));
                cmd.Parameters.Add(new SqlParameter("@ProfileTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, profileTypeID));

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

        public DataTable FL_FileProfile_SelectBy_Date_Search(DateTime date, string search)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileProfile_SelectBy_Date_Search";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                if (date != DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, date));
                else
                    cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Search", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, search));

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

        public void FL_FileProfile_InsertUpdate(int idFileProfile, int fileTypeID, int proFileTypeID, DateTime dateStart,
            DateTime dateEnd, string sUserModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileProfile_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@ID_FileProfile", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, idFileProfile));
                cmd.Parameters.Add(new SqlParameter("@FileTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fileTypeID));
                cmd.Parameters.Add(new SqlParameter("@ProFileTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, proFileTypeID));
                cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateStart));
                if (dateEnd == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateEnd));
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

        #region FL_FileType

        public DataTable FL_FileType_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileType_SelectAll";
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

        public DataTable FL_FileType_SelectByDateSearch(DateTime date, string search)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileType_SelectByDateSearch";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                if (date == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, date));
                cmd.Parameters.Add(new SqlParameter("@Search", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, search));

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

        public DataTable FL_FileType_SelectByFileTypeID(int fileTypeID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileType_SelectByFileTypeID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@FileTypeId", fileTypeID).Direction = ParameterDirection.Input;

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

        public DataTable FL_FileType_SelectByNotation(string Notation)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileType_SelectByNotation";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@Notation", Notation).Direction = ParameterDirection.Input;

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

        public DataTable FL_FileType_DeleteByFileTypeID(int fileTypeID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileType_DeteleByFileTypeID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@FileTypeId", fileTypeID).Direction = ParameterDirection.Input;

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

        public void FL_FileType_InsertUpdate(int fileTypeID, string name, int dateType, int unitType, string notation, DateTime dateStart, DateTime dateEnd, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileType_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@FileTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fileTypeID));
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, name));
                cmd.Parameters.Add(new SqlParameter("@DateType", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateType));
                cmd.Parameters.Add(new SqlParameter("@UnitType", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, unitType));
                cmd.Parameters.Add(new SqlParameter("@Notation", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, notation));
                cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateStart));
                if (dateEnd == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateEnd));
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

        #endregion

        #region FL_ProfileType

        public DataTable FL_ProfileType_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_ProfileType_SelectAll";
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

        public DataTable FL_ProfileType_SelectByDateSearch(DateTime date, string search)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_ProfileType_SelectByDateSearch";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                if (date == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, date));
                cmd.Parameters.Add(new SqlParameter("@Search", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, search));

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

        public DataTable FL_ProfileType_SelectByProfileTypeID(int profileTypeID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_ProfileType_SelectByProfileTypeID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ProfileTypeID", profileTypeID).Direction = ParameterDirection.Input;

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

        public DataTable FL_ProfileType_SelectByUnitType_DateType_Date(int UnitType, int DateType, DateTime Date)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_ProfileType_SelectByUnitType_DateType_Date";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@UnitType", UnitType).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@DateType", DateType).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@Date", Date).Direction = ParameterDirection.Input;

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

        public void FL_ProfileType_InsertUpdate(int profileTypeID, string name, int unitType, int dateType, DateTime dateStart, DateTime dateEnd, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_ProfileType_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@ProfileTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, profileTypeID));
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, name));
                cmd.Parameters.Add(new SqlParameter("@UnitType", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, unitType));
                cmd.Parameters.Add(new SqlParameter("@DateType", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateType));
                cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateStart));
                if (dateEnd == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateEnd));
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

        public void FL_ProfileType_InsertUpdate_LienKetVB(int profileTypeID, string name, int unitType, int dateType, DateTime dateStart, DateTime dateEnd, string userModified, DataTable dtFileProfile)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand();

            try
            {
                //Cập nhật hồ sơ
                cmd = new SqlCommand();
                cmd.CommandText = "FL_ProfileType_InsertUpdate_Out_ProfileTypeID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;
                cmd.Transaction = transaction;

                cmd.Parameters.Add(new SqlParameter("@ProfileTypeID", SqlDbType.Int, 8, ParameterDirection.InputOutput, false, 0, 0, "", DataRowVersion.Proposed, profileTypeID));
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, name));
                cmd.Parameters.Add(new SqlParameter("@UnitType", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, unitType));
                cmd.Parameters.Add(new SqlParameter("@DateType", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateType));
                cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateStart));
                if (dateEnd == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateEnd));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));
                cmd.ExecuteNonQuery();
                profileTypeID = (int)cmd.Parameters["@ProfileTypeID"].Value;


                //Cập nhật danh sách văn bản thuộc hồ sơ
                //Thêm
                DataRow[] rowAdd = dtFileProfile.Select("", "", DataViewRowState.Added);
                foreach (DataRow row in rowAdd)
                {
                    int idFileProfile = -1;
                    int fileTypeID = Convert.ToInt32(row["FileTypeID"]);

                    cmd = new SqlCommand();
                    cmd.CommandText = "FL_FileProfile_InsertUpdate";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;

                    cmd.Parameters.Add(new SqlParameter("@ID_FileProfile", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, idFileProfile));
                    cmd.Parameters.Add(new SqlParameter("@FileTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fileTypeID));
                    cmd.Parameters.Add(new SqlParameter("@ProFileTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, profileTypeID));
                    cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, row["DateStart"]));
                    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, row["DateEnd"]));
                    cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));

                    cmd.ExecuteNonQuery();
                }
                //Sửa
                DataRow[] rowUpdate = dtFileProfile.Select("", "", DataViewRowState.ModifiedCurrent);
                foreach (DataRow row in rowUpdate)
                {
                    int idFileProfile = Convert.ToInt32(row["ID_FileProfile"]);
                    int fileTypeID = Convert.ToInt32(row["FileTypeID"]);

                    cmd = new SqlCommand();
                    cmd.CommandText = "FL_FileProfile_InsertUpdate";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;

                    cmd.Parameters.Add(new SqlParameter("@ID_FileProfile", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, idFileProfile));
                    cmd.Parameters.Add(new SqlParameter("@FileTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fileTypeID));
                    cmd.Parameters.Add(new SqlParameter("@ProFileTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, profileTypeID));
                    cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, row["DateStart"]));
                    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, row["DateEnd"]));
                    cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));

                    cmd.ExecuteNonQuery();
                }
                //xóa văn bản thuộc hồ sơ
                DataRow[] rowDelete = dtFileProfile.Select("", "", DataViewRowState.Deleted);
                foreach (DataRow row in rowDelete)
                {
                    int idFileProfile = Convert.ToInt32(row["ID_FileProfile", DataRowVersion.Original]);

                    cmd = new SqlCommand();
                    cmd.CommandText = "FL_FileProfile_Delete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;

                    cmd.Parameters.Add(new SqlParameter("@ID_FileProfile", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, idFileProfile));

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

        public int FL_ProfileType_InsertUpdate_Out_ProfileTypeID(int profileTypeID, string name, int unitType, int dateType, DateTime dateStart, DateTime dateEnd, string userModified)
        {
            int result = -1;
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_ProfileType_InsertUpdate_Out_ProfileTypeID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                //cmd.Parameters.Add(new SqlParameter("@ProfileTypeID", SqlDbType.Int, 8, ParameterDirection.InputOutput, false, 0, 0, "", DataRowVersion.Proposed, profileTypeID));
                SqlParameter ProfileTypeID_Out = new SqlParameter("@ID_UserProg", SqlDbType.Int, 8, ParameterDirection.InputOutput, false, 0, 0, "", DataRowVersion.Proposed, profileTypeID);
                cmd.Parameters.Add(ProfileTypeID_Out);
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, name));
                cmd.Parameters.Add(new SqlParameter("@UnitType", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, unitType));
                cmd.Parameters.Add(new SqlParameter("@DateType", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateType));
                cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateStart));
                if (dateEnd == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateEnd));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));

                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = (int)ProfileTypeID_Out.Value;
                }
                else
                {
                    result = -1;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                throw ex;
            }
            return result;
        }

        public bool FL_ProfileType_DeleteByProfileTypeID(int profileTypeID)
        {
            bool result = true;
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_ProfileType_DeleteByProfileTypeID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ProfileTypeID", profileTypeID).Direction = ParameterDirection.Input;
                int x = -1;
                x = cmd.ExecuteNonQuery();
                //if (x <= 0) result = false;

                connection.Close();
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                result = false;
            }
            return result;
        }

        #endregion

        #region FL_RelationType

        public DataTable FL_RelationType_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_RelationType_SelectAll";
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

        public DataTable FL_RelationType_SelectByDateSearch(DateTime date, string search)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_RelationType_SelectByDateSearch";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                if (date == DateTime.MinValue)
                    cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, date));
                cmd.Parameters.Add(new SqlParameter("@Search", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, search));

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

        public DataTable FL_RelationType_SelectByRelationTypeID(int relationTypeID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_RelationType_SelectByRelationTypeID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@RelationTypeID", relationTypeID).Direction = ParameterDirection.Input;

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

        public void FL_RelationType_InsertUpdate(int relationTypeID, string name, DateTime dateStart, DateTime dateEnd, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_RelationType_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@RelationTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, relationTypeID));
                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, name));
                cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateStart));
                if (dateEnd == DateTime.MaxValue)
                    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateEnd));
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

        public DataTable FL_RelationType_DeleteByRelationTypeID(int relationTypeID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_RelationTypeID_DeleteByProfileTypeID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@RelationTypeID", relationTypeID).Direction = ParameterDirection.Input;

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

        #region FL_FileType_QuyenXacNhan
        public DataTable FL_FileType_QuyenXacNhan_SelectBy_FileTypeID(int FileTypeID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileType_QuyenXacNhan_SelectBy_FileTypeID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@FileTypeId", FileTypeID).Direction = ParameterDirection.Input;

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

        public void FL_FileType_QuyenXacNhan__InsertUpdate(DataTable dtNew, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand();

            try
            {
                //Cập nhật danh sách văn bản thuộc hồ sơ
                //Thêm
                DataRow[] rowAdd = dtNew.Select("", "", DataViewRowState.Added);
                foreach (DataRow row in rowAdd)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "FL_FileType_QuyenXacNhan_InsertUpdate";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;

                    cmd.Parameters.Add(new SqlParameter("@ID_QuyenXacNhan", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, -1));
                    cmd.Parameters.Add(new SqlParameter("@FileTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, row["FileTypeID"]));
                    cmd.Parameters.Add(new SqlParameter("@UnitTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, row["UnitTypeID"]));
                    cmd.Parameters.Add(new SqlParameter("@SignatureTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@CertType", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, row["CertType"]));
                    cmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, row["TrangThai"]));
                    cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));

                    cmd.ExecuteNonQuery();
                }
                //Sửa
                DataRow[] rowUpdate = dtNew.Select("", "", DataViewRowState.ModifiedCurrent);
                foreach (DataRow row in rowUpdate)
                {
                    cmd = new SqlCommand();
                    cmd.CommandText = "FL_FileType_QuyenXacNhan_InsertUpdate";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;

                    cmd.Parameters.Add(new SqlParameter("@ID_QuyenXacNhan", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, row["ID_QuyenXacNhan"]));
                    cmd.Parameters.Add(new SqlParameter("@FileTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, row["FileTypeID"]));
                    cmd.Parameters.Add(new SqlParameter("@UnitTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, row["UnitTypeID"]));
                    cmd.Parameters.Add(new SqlParameter("@SignatureTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@CertType", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, row["CertType"]));
                    cmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, row["TrangThai"]));
                    cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));

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

        public bool ProfileType_FileProfile_InsertUpdate(DataTable profileType, DataTable fileProfile, string userModified)
        {
            bool result = true;

            SqlConnection connection = sc.GetConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand();

            try
            {
                if (profileType != null)
                {
                    foreach (DataRow item in profileType.Rows)
                    {
                        #region Insert Update ProfileType
                        // Thêm mới cập nhật bảng Type
                        cmd = new SqlCommand();
                        cmd.CommandText = "FL_ProfileType_InsertUpdate_Out_ProfileTypeID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Transaction = transaction;

                        // Lấy dữ liệu từ datatable
                        int profileTypeID = item["ProfileTypeID"].ToString() != "" ? Convert.ToInt32(item["ProfileTypeID"]) : -1;
                        string name = item["Name"].ToString();
                        int unitType = Convert.ToInt32(item["UnitType"]);
                        int dateType = Convert.ToInt32(item["DateTypeValue"]);
                        DateTime dateStart = Convert.ToDateTime(item["DateStart"]);
                        DateTime dateEnd = item["DateEnd"].ToString() != "" ? Convert.ToDateTime(item["DateEnd"]) : DateTime.MaxValue;

                        SqlParameter ProfileTypeID_Out = new SqlParameter("@ProfileTypeID", SqlDbType.Int, 8, ParameterDirection.InputOutput, false, 0, 0, "", DataRowVersion.Proposed, profileTypeID);
                        cmd.Parameters.Add(ProfileTypeID_Out);
                        cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, name));
                        cmd.Parameters.Add(new SqlParameter("@UnitType", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, unitType));
                        cmd.Parameters.Add(new SqlParameter("@DateType", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateType));
                        cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateStart));
                        if (dateEnd == DateTime.MaxValue)
                            cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                        else
                            cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateEnd));
                        cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));

                        // Lấy ID mới được cập nhật
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            profileTypeID = (int)ProfileTypeID_Out.Value;
                            if (profileTypeID == -1)
                            {
                                transaction.Rollback();
                                result = false;
                                break;
                            }
                        }
                        else
                        {
                            transaction.Rollback();
                            result = false;
                            break;
                        }
                        #endregion

                        #region Insert Update FileProfile
                        // Cập nhật bảng FileProfile

                        DataRow[] drtmp = fileProfile.Select("ProFileTypeID = " + profileTypeID.ToString());

                        if (drtmp != null)
                        {
                            foreach (DataRow item1 in drtmp)
                            {
                                int idFileProfile = Convert.ToInt32(item1["ID_FileProfile"]);
                                int fileTypeID = Convert.ToInt32(item1["FileTypeID"]);
                                int proFileTypeID = profileTypeID;
                                dateStart = Convert.ToDateTime(item1["DateStart"]);
                                dateEnd = item1["DateEnd"].ToString() != "" ? Convert.ToDateTime(item1["DateEnd"]) : DateTime.MaxValue;

                                cmd = new SqlCommand();
                                cmd.CommandText = "FL_FileProfile_InsertUpdate";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = connection;
                                cmd.Transaction = transaction;

                                cmd.Parameters.Add(new SqlParameter("@ID_FileProfile", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, idFileProfile));
                                cmd.Parameters.Add(new SqlParameter("@FileTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fileTypeID));
                                cmd.Parameters.Add(new SqlParameter("@ProFileTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, proFileTypeID));
                                cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateStart));
                                if (dateEnd == DateTime.MaxValue)
                                    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                                else
                                    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dateEnd));
                                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));

                                cmd.ExecuteNonQuery();
                            }
                        }
                        #endregion
                    }
                    transaction.Commit();
                    connection.Close();
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                {
                    transaction.Rollback();
                    connection.Close();
                }
            }

            return result;
        }
    }
}
