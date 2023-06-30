using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;

namespace ES.CA_WebServiceDAL
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

        #region Methods

        public DataTable CA_UnitProgram_SelectByMaDV_ProgName(int loaiDV, string maDV, string programName)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UnitProgram_SelectByMaDV_ProgName";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@UnitType", loaiDV);
                cmd.Parameters.AddWithValue("@MaDV", maDV);
                cmd.Parameters.AddWithValue("@ProgName", programName);

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

        public DataTable CA_UnitProgram_SelectBy_ArrayMaDV_Prog(int loaiDV, string arrMaDV, string programName)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_UnitProgram_SelectBy_ArrayMaDV_Prog";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@UnitType", loaiDV);
                cmd.Parameters.AddWithValue("@arrMaDV", arrMaDV);
                cmd.Parameters.AddWithValue("@ProgName", programName);

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

        public void CA_Certificate_InsertUpdate(int certID, byte[] fileCert, string serial, string nameCN,
            string thumbPrint, byte[] rawData, int status, int typeUnit, int unitID)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();

            try
            {
                SqlCommand sqlcomm = new SqlCommand();

                sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "CA_Certificate_InsertUpdate";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = sqlcon;
                //add Parameters
                sqlcomm.Parameters.Add(new SqlParameter("@CertID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certID));
                sqlcomm.Parameters.Add(new SqlParameter("@FileCert", SqlDbType.Image, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fileCert));
                sqlcomm.Parameters.Add(new SqlParameter("@Serial", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, serial));
                sqlcomm.Parameters.Add(new SqlParameter("@NameCN", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, nameCN));
                sqlcomm.Parameters.Add(new SqlParameter("@ThumbPrint", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, thumbPrint));
                sqlcomm.Parameters.Add(new SqlParameter("@RawData", SqlDbType.VarBinary, -1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, rawData));
                sqlcomm.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, status));
                sqlcomm.Parameters.Add(new SqlParameter("@TypeUnit", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, typeUnit));
                sqlcomm.Parameters.Add(new SqlParameter("@UnitID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, unitID));
                //thực hiện insert update
                sqlcomm.ExecuteNonQuery();
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
        }

        public DataTable CA_Certificate_SelectByProgUser(string programName, string userName)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Certificate_SelectByProgUser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@ProgramName", programName);
                cmd.Parameters.AddWithValue("@UserProgName", userName);

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

        public DataTable FL_File_SelectFileID_ByNgayMaNMType(DateTime TuNgay, DateTime DenNgay, string Ma_NM, int FileTypeID)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_File_SelectFileID_ByNgayMaNMType";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@TuNgay", TuNgay);
                cmd.Parameters.AddWithValue("@DenNgay", DenNgay);
                cmd.Parameters.AddWithValue("@Ma_NM", Ma_NM);
                cmd.Parameters.AddWithValue("@FileTypeID", FileTypeID);

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

        public void FL_FileRelation_Insert(int FileID_A, int FileID_B, int RelationTypeID, string programName, string userName)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();

            try
            {
                SqlCommand sqlcomm = new SqlCommand();

                sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "FL_FileRelation_Insert";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = sqlcon;
                //add Parameters
                sqlcomm.Parameters.AddWithValue("@FileID_A", FileID_A);
                sqlcomm.Parameters.AddWithValue("@FileID_B", FileID_B);
                sqlcomm.Parameters.AddWithValue("@RelationTypeID", RelationTypeID);
                sqlcomm.Parameters.AddWithValue("@ProgramName", programName);
                sqlcomm.Parameters.AddWithValue("@UserProgName", userName);

                //thực hiện insert update
                sqlcomm.ExecuteNonQuery();
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
        }

        public DataTable CA_Certificate_SelectChainByCertProg(string programName, string userName, string certSerial)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "CA_Certificate_SelectChainByCertProg";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@ProgramName", programName);
                cmd.Parameters.AddWithValue("@UserProgName", userName);
                cmd.Parameters.AddWithValue("@Serial", certSerial);

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

        public DataTable HSM_Slot_SelectFormObjectByCertID(int certID)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Slot_SelectFormObjectByCertID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@CertID", certID);

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

        public void HSM_Slot_UpdateUserPIN(int slotID, string newPIN)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();

            try
            {
                SqlCommand sqlcomm = new SqlCommand();

                sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "HSM_Slot_UpdateUserPIN";
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Connection = sqlcon;
                //add Parameters
                sqlcomm.Parameters.AddWithValue("@SlotID", slotID);
                sqlcomm.Parameters.AddWithValue("@NewPIN", newPIN);

                //thực hiện insert update
                sqlcomm.ExecuteNonQuery();
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
        }
        
        public DataTable HSM_Object_SelectPrivateKeyByCertID(int certID)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_SelectPrivateKeyByCertID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@CertID", certID);

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

                cmd.Parameters.AddWithValue("@FileID", fileID);

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

        public bool FL_File_SelectForSign(int fileID, string programName, string userName, ref DataTable dtFile)
        {
            //dtFile = new DataTable("FL_File_SelectForSign");

            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_File_SelectForSign";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@FileID", fileID);
                cmd.Parameters.AddWithValue("@OKtoSign", 0).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@ProgramName", programName);
                cmd.Parameters.AddWithValue("@UserProgName", userName);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtFile);
                bool bOK = Convert.ToBoolean(cmd.Parameters["@OKtoSign"].Value);

                sqlcon.Close();

                return bOK;
            }
            catch (Exception ex)
            {
                if (sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();
                throw ex;
            }
        }

        public bool FL_File_SelectForAllowSign_Array(string arrFileID, string programName, string userName, ref DataTable dtFile)
        {
            //dtFile = new DataTable("FL_File_SelectForAllowSign_Array");

            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_File_SelectForAllowSign_Array";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@arrFileID", arrFileID);
                cmd.Parameters.AddWithValue("@OKtoSign", 0).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@ProgramName", programName);
                cmd.Parameters.AddWithValue("@UserProgName", userName);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtFile);
                bool bOK = Convert.ToBoolean(cmd.Parameters["@OKtoSign"].Value);

                sqlcon.Close();

                return bOK;
            }
            catch (Exception ex)
            {
                if (sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();
                throw ex;
            }
        }

        public bool FL_File_SelectForSaveSign(int id_StatusLog)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_File_SelectForSaveSign";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@ID_StatusLog", id_StatusLog);
                cmd.Parameters.AddWithValue("@OKtoSave", 0).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                bool bOK = Convert.ToBoolean(cmd.Parameters["@OKtoSave"].Value);

                sqlcon.Close();

                return bOK;
            }
            catch (Exception ex)
            {
                if (sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();
                throw ex;
            }
        }

        public void FL_File_UpdateForLogSign(int fileID, string certSerial, DateTime signTime, int verify, int signatureType,
            int action, string backupPath, string reason, string programName, string userName)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_File_UpdateForLogSign";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.Add(new SqlParameter("@FileID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fileID));
                cmd.Parameters.Add(new SqlParameter("@CertSerial", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certSerial));
                cmd.Parameters.Add(new SqlParameter("@SignTime", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, signTime));
                cmd.Parameters.Add(new SqlParameter("@Verify", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, verify));
                cmd.Parameters.Add(new SqlParameter("@SignatureTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, signatureType));
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.TinyInt, 2, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, action));
                cmd.Parameters.Add(new SqlParameter("@BackupPath", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, backupPath));
                cmd.Parameters.Add(new SqlParameter("@Reason", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, reason));
                cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, programName));
                cmd.Parameters.Add(new SqlParameter("@UserProgName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userName));

                cmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                if (sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();
                throw ex;
            }
        }

        public DataTable FL_File_CheckBeforeSaveSign(int fileID, string certSerial, string programName)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_File_CheckBeforeSaveSign";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@FileID", fileID);
                cmd.Parameters.AddWithValue("@CertSerial", certSerial);
                cmd.Parameters.AddWithValue("@ProgramName", programName);

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

        public DataTable FL_FileType_QuyenXacNhan_CheckByFileID_CertID(int fileID, string certSerial)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_FileType_QuyenXacNhan_CheckByFileID_CertID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@FileID", fileID);
                cmd.Parameters.AddWithValue("@CertSerial", certSerial);

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

        public DataTable FL_File_InsertSelectNewFile(string programName, string userName, int fileTypeID, string fileMaDV, DateTime fileDate, 
            string fileName, string description)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_File_InsertSelectNewFile";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, programName));
                cmd.Parameters.Add(new SqlParameter("@UserProgName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userName));
                cmd.Parameters.Add(new SqlParameter("@FileTypeID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fileTypeID));
                cmd.Parameters.Add(new SqlParameter("@FileMaDV", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fileMaDV));
                cmd.Parameters.Add(new SqlParameter("@FileDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fileDate));
                cmd.Parameters.Add(new SqlParameter("@FileName", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fileName));
                cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, description));

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

        public void FL_File_UpdateStatus_WithHash(int fileID, int status, byte[] fileHash, string reason, string programName, string userName)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_File_UpdateStatus_WithHash";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.Add(new SqlParameter("@FileID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fileID));
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, status));
                cmd.Parameters.Add(new SqlParameter("@FileHash", SqlDbType.VarBinary, fileHash.Length, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fileHash));
                cmd.Parameters.Add(new SqlParameter("@Reason", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, reason));
                cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, programName));
                cmd.Parameters.Add(new SqlParameter("@UserProgName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userName));

                cmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                if (sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();
                throw ex;
            }
        }

        public void FL_File_UpdateStatus(int fileID, int status, string reason, string programName, string userName)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_File_UpdateStatus";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.Add(new SqlParameter("@FileID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fileID));
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, status));
                cmd.Parameters.Add(new SqlParameter("@Reason", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, reason));
                cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, programName));
                cmd.Parameters.Add(new SqlParameter("@UserProgName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userName));

                cmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                if (sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();
                throw ex;
            }
        }

        public void FL_LogFileSignature_Insert(int fileID, string certSerial, DateTime signTime, int verify,
            int action, string programName, string userName)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FL_LogFileSignature_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.Add(new SqlParameter("@FileID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fileID));
                cmd.Parameters.Add(new SqlParameter("@CertSerial", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certSerial));
                cmd.Parameters.Add(new SqlParameter("@SignTime", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, signTime));
                cmd.Parameters.Add(new SqlParameter("@Verify", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, verify));
                cmd.Parameters.Add(new SqlParameter("@Action", SqlDbType.TinyInt, 2, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, action));
                cmd.Parameters.Add(new SqlParameter("@ProgramName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, programName));
                cmd.Parameters.Add(new SqlParameter("@UserProgName", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userName));

                cmd.ExecuteNonQuery();
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                if (sqlcon.State == ConnectionState.Open)
                    sqlcon.Close();
                throw ex;
            }
        }

        public DataTable Q_CONFIG_SelectBy_ConfigID(int configID)
        {
            SqlConnection sqlcon = sc.GetConnection();
            sqlcon.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Q_CONFIG_SelectBy_ConfigID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlcon;

                cmd.Parameters.AddWithValue("@ConfigID", configID);

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
        #endregion
    }
}
