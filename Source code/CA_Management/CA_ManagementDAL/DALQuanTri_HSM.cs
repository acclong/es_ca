using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ESLogin;
using esDigitalSignature.Library;
using System.Security.Cryptography.X509Certificates;

namespace ES.CA_ManagementDAL
{
    public partial class DALQuanTri
    {
        #region HSM_Device

        public DataTable HSM_Device_SelectAll_Full()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Device_SelectAll_Full";
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

        public DataTable HSM_Device_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Device_SelectAll";
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

        public DataTable HSM_Device_SelectBy_CertID(int certID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Device_SelectBy_CertID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

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

        public DataTable HSM_Device_SelectByNot_DeviceID(int deviceID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Device_SelectByNot_DeviceID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@DeviceID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deviceID));

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

        public bool HSM_Device_UpdatePIN(int deviceID, String newPIN, int type)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "HSM_Device_UpdatePIN";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@DeviceID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deviceID));
                command.Parameters.Add(new SqlParameter("@NewPIN", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, newPIN));
                command.Parameters.Add(new SqlParameter("@Role", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, type));
                command.Connection = connection;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool HSM_Device_DeleteBy_DeviceID(int deviceID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "HSM_Device_DeleteBy_DeviceID";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@DeviceID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deviceID));
                command.Connection = connection;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool HSM_Device_InsertUpdate(int deviceID, string name, string serial, string ip, string so_pin, string user_pin, string usermodified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "HSM_Device_InsertUpdate";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@DeviceID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deviceID));
                command.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, name));
                command.Parameters.Add(new SqlParameter("@Serial", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, serial));
                command.Parameters.Add(new SqlParameter("@IP", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ip));
                if (so_pin == "")
                    command.Parameters.Add(new SqlParameter("@SO_PIN", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    command.Parameters.Add(new SqlParameter("@SO_PIN", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, so_pin));
                if (user_pin == "")
                    command.Parameters.Add(new SqlParameter("@User_PIN", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    command.Parameters.Add(new SqlParameter("@User_PIN", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, user_pin));
                command.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, usermodified));
                command.Connection = connection;
                int x = command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        #endregion

        #region HSM_Slot

        public DataTable HSM_Slot_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Slot_SelectAll";
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

        public DataTable HSM_Slot_SelectAll_Editor()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Slot_SelectAll_Editor";
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

        public DataTable HSM_Slot_SelectAll_NotUse()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Slot_SelectAll_NotUse";
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

        public DataTable HSM_Slot_SelectByDeviceID(int deviceID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Slot_SelectByDeviceID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@DeviceID", deviceID).Direction = ParameterDirection.Input;

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

        public DataTable HSM_Slot_SelectBy_CertID(int certID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Slot_SelectBy_CertID";
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

        public DataTable HSM_Slot_SelectByInitToken(bool initToken)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Slot_SelectByInitToken";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@InitToken", initToken).Direction = ParameterDirection.Input;

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

        public DataTable HSM_Slot_SelectNotExistsHSM_WLDSlot()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Slot_SelectNotExistsHSM_WLDSlot";
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

        public DataTable HSM_Slot_SelectByTokenLabel(string tokenLabel)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Slot_SelectByTokenLabel";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@TokenLabel", tokenLabel).Direction = ParameterDirection.Input;

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

        public DataTable HSM_Slot_SelectByTokenLabel_DeviceID(string tokenLabel, int deviceID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Slot_SelectByTokenLabel_DeviceID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@DeviceID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deviceID));
                cmd.Parameters.Add(new SqlParameter("@TokenLabel", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, tokenLabel));

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

        public DataTable HSM_Slot_SelectBySlotSerial_DeviceID(string serial, int deviceID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Slot_SelectBySlotSerial_DeviceID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@DeviceID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deviceID));
                cmd.Parameters.Add(new SqlParameter("@SlotSerial", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, serial));

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

        public DataTable HSM_Slot_SelectBySlotID(int slotID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Slot_SelectBySlotID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@SlotID", slotID).Direction = ParameterDirection.Input;

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

        public DataSet HSM_Slot_Select_ComboboxEditor()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Slot_Select_ComboboxEditor";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

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

        public bool HSM_Slot_InsertUpdate(int slotID, int deviceID, int slotIndex, string serial, int type, bool initToken,
            string tokenLabel, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "HSM_Slot_InsertUpdate";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.Add(new SqlParameter("@SlotID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotID));
                command.Parameters.Add(new SqlParameter("@DeviceID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deviceID));
                command.Parameters.Add(new SqlParameter("@SlotIndex", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotIndex));
                command.Parameters.Add(new SqlParameter("@Serial", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, serial));
                command.Parameters.Add(new SqlParameter("@Type", SqlDbType.TinyInt, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, type));
                command.Parameters.Add(new SqlParameter("@InitToken", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, initToken));
                command.Parameters.Add(new SqlParameter("@TokenLabel", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, tokenLabel));
                command.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void HSM_Slot_UpdateUserPIN(int slotID, string userPIN)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            userPIN = StringCryptor.EncryptString(userPIN);
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "HSM_Slot_UpdateUserPIN";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.Add(new SqlParameter("@SlotID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotID));
                command.Parameters.Add(new SqlParameter("@NewPIN", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userPIN));

                command.ExecuteNonQuery();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool HSM_Slot_UpdateUserPIN(DataTable data, string newPass)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand cmd = new SqlCommand();

            string userPIN = StringCryptor.EncryptString(newPass);

            try
            {
                foreach (DataRow item in data.Rows)
                {
                    int slotID = Convert.ToInt32(item["SlotID"]);

                    cmd = new SqlCommand();
                    cmd.CommandText = "HSM_Slot_UpdateUserPIN";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;

                    cmd.Parameters.Add(new SqlParameter("@SlotID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotID));
                    cmd.Parameters.Add(new SqlParameter("@UserPIN", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userPIN));

                    cmd.ExecuteNonQuery();
                }
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

        public bool HSM_Slot_InsertUpdate(DataTable dtOld, DataTable dtNew, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                for (int i = 0; i < dtNew.Rows.Count; i++)
                {
                    int slotIDNew = -1;
                    int deviceIdNew = Convert.ToInt32(dtNew.Rows[i]["DeviceID"]);
                    int slotIndexNew = Convert.ToInt32(dtNew.Rows[i]["SlotIndex"]);
                    string serialNew = dtNew.Rows[i]["Serial"].ToString();
                    int typeNew = Convert.ToInt32(dtNew.Rows[i]["Type"]);
                    bool initTokenNew = Convert.ToBoolean(dtNew.Rows[i]["InitToken"]);
                    string tokenLabelNew = dtNew.Rows[i]["TokenLabel"].ToString();

                    if (!isEmpty(dtNew.Rows[i]["SlotID"]))
                    {
                        slotIDNew = Convert.ToInt32(dtNew.Rows[i]["SlotID"]);

                        //Lấy dữ liệu cũ tương ứng
                        DataRow drOld = dtOld.Select("SlotID = " + slotIDNew.ToString())[0];

                        //So sánh thay đổi
                        int deviceIdOld = Convert.ToInt32(drOld["DeviceID"]);
                        int slotIndexOld = Convert.ToInt32(drOld["SlotIndex"]);
                        string serialOld = drOld["Serial"].ToString();
                        int typeOld = Convert.ToInt32(drOld["Type"]);
                        bool initTokenOld = Convert.ToBoolean(drOld["InitToken"]);
                        string tokenLabelOld = drOld["TokenLabel"].ToString();

                        //Nếu không thay đổi -> không cập nhật
                        if (deviceIdNew == deviceIdOld && slotIndexNew == slotIndexOld && serialNew == serialOld && typeNew == typeOld
                            && initTokenNew == initTokenOld && tokenLabelNew == tokenLabelOld)
                            continue;
                    }

                    SqlCommand command = new SqlCommand();
                    command.CommandText = "HSM_Slot_InsertUpdate";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Transaction = transaction;

                    command.Parameters.Add(new SqlParameter("@SlotID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotIDNew));
                    command.Parameters.Add(new SqlParameter("@DeviceID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deviceIdNew));
                    command.Parameters.Add(new SqlParameter("@SlotIndex", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotIndexNew));
                    command.Parameters.Add(new SqlParameter("@Serial", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, serialNew));
                    command.Parameters.Add(new SqlParameter("@Type", SqlDbType.TinyInt, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, typeNew));
                    command.Parameters.Add(new SqlParameter("@InitToken", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, initTokenNew));
                    command.Parameters.Add(new SqlParameter("@TokenLabel", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, tokenLabelNew));
                    command.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));
                    command.ExecuteNonQuery();
                }
                transaction.Commit();
                connection.Close();

                return true;
            }
            catch
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
                    connection.Close();
            }
        }

        public bool HSM_Slot_UpdateIndex(DataTable dtSlotHSM, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                for (int i = 0; i < dtSlotHSM.Rows.Count; i++)
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "HSM_Slot_UpdateIndex";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Transaction = transaction;

                    command.Parameters.Add(new SqlParameter("@SlotIndex", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Convert.ToInt32(dtSlotHSM.Rows[i]["SlotIndex"])));
                    command.Parameters.Add(new SqlParameter("@Serial", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dtSlotHSM.Rows[i]["SerialNumber"].ToString()));
                    command.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));
                    command.ExecuteNonQuery();
                }
                transaction.Commit();
                connection.Close();

                return true;
            }
            catch
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
                    connection.Close();
            }
        }

        public void HSM_Slot_DeleteBySlotSerial(int deviceID, string serial)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "HSM_Slot_DeleteBySlotSerial";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.Add(new SqlParameter("@DeviceID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deviceID));
                command.Parameters.Add(new SqlParameter("@Serial", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, serial));
                command.ExecuteNonQuery();
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

        public bool HSM_Slot_DeleteBy_SlotID(int slotID)
        {
            bool result = false;
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "HSM_Slot_DeleteBy_SlotID";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;
                command.Parameters.Add(new SqlParameter("@SlotID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotID));
                command.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                //throw new Exception("Database error: " + ex.Message);

            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return result;
        }
        #endregion

        #region HSM_Object

        public DataTable HSM_Object_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_SelectAll";
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

        public DataTable HSM_Object_SelectBySlotID(int slotID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_SelectBySlotID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@SlotID", slotID).Direction = ParameterDirection.Input;

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

        public DataTable HSM_Object_SelectDistinctBySlotID(int slotID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_SelectDistinctBySlotID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@SlotID", slotID);

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

        public DataTable HSM_Object_SelectByObjectID(int objectID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_SelectByObjectID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ObjectID", objectID).Direction = ParameterDirection.Input;

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

        public DataTable HSM_Object_SelectBy_TokenLabel(string tokenLabel)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_SelectBy_TokenLabel";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@TokenLabel", tokenLabel).Direction = ParameterDirection.Input;

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

        public DataSet HSM_Object_Select_ComboboxEditor()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_Select_ComboboxEditor";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

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

        public void HSM_Object_Insert(int slotID, string cka_LABEL, Int16 cka_CLASS, byte[] cka_ID, string cka_SUBJECT, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@SlotID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotID));
                cmd.Parameters.Add(new SqlParameter("@CKA_LABEL", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_LABEL));
                cmd.Parameters.Add(new SqlParameter("@CKA_CLASS", SqlDbType.TinyInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_CLASS));
                cmd.Parameters.Add(new SqlParameter("@CKA_ID", SqlDbType.VarBinary, cka_ID.Length, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_ID));
                cmd.Parameters.Add(new SqlParameter("@CKA_SUBJECT", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_SUBJECT));
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

        public bool HSM_Object_Insert(int slotID, string subject, string keyPUB, string keyPRV, string certRequest, byte[] cka_ID, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                //Private key
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;
                cmd.Transaction = transaction;

                cmd.Parameters.Add(new SqlParameter("@SlotID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotID));
                cmd.Parameters.Add(new SqlParameter("@CKA_LABEL", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, keyPRV));
                cmd.Parameters.Add(new SqlParameter("@CKA_CLASS", SqlDbType.TinyInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, (int)HSMObjectTypeDB.PRIVATE_KEY));
                cmd.Parameters.Add(new SqlParameter("@CKA_ID", SqlDbType.VarBinary, cka_ID.Length, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_ID));
                cmd.Parameters.Add(new SqlParameter("@CKA_SUBJECT", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, subject));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));
                cmd.ExecuteNonQuery();

                //Public Key
                cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;
                cmd.Transaction = transaction;

                cmd.Parameters.Add(new SqlParameter("@SlotID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotID));
                cmd.Parameters.Add(new SqlParameter("@CKA_LABEL", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, keyPUB));
                cmd.Parameters.Add(new SqlParameter("@CKA_CLASS", SqlDbType.TinyInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, (int)HSMObjectTypeDB.PUBLIC_KEY));
                cmd.Parameters.Add(new SqlParameter("@CKA_ID", SqlDbType.VarBinary, cka_ID.Length, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_ID));
                cmd.Parameters.Add(new SqlParameter("@CKA_SUBJECT", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, subject));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));
                cmd.ExecuteNonQuery();

                //Cert request
                cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;
                cmd.Transaction = transaction;

                cmd.Parameters.Add(new SqlParameter("@SlotID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotID));
                cmd.Parameters.Add(new SqlParameter("@CKA_LABEL", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certRequest));
                cmd.Parameters.Add(new SqlParameter("@CKA_CLASS", SqlDbType.TinyInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, (int)HSMObjectTypeDB.CERTIFICATE_REQUEST));
                cmd.Parameters.Add(new SqlParameter("@CKA_ID", SqlDbType.VarBinary, cka_ID.Length, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_ID));
                cmd.Parameters.Add(new SqlParameter("@CKA_SUBJECT", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, subject));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));
                cmd.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                return true;
            }
            catch
            {
                if (connection.State == ConnectionState.Open)
                {
                    transaction.Rollback();
                    connection.Close();
                }
                return false;
            }
        }

        public void HSM_Object_InsertUpdate(int objectID, int slotID, string cka_LABEL, Int16 cka_CLASS, byte[] cka_ID, string cka_SUBJECT, int status, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@ObjectID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, objectID));
                cmd.Parameters.Add(new SqlParameter("@SlotID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotID));
                cmd.Parameters.Add(new SqlParameter("@CKA_LABEL", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_LABEL));
                cmd.Parameters.Add(new SqlParameter("@CKA_CLASS", SqlDbType.TinyInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_CLASS));
                cmd.Parameters.Add(new SqlParameter("@CKA_ID", SqlDbType.VarBinary, cka_ID.Length, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_ID));
                cmd.Parameters.Add(new SqlParameter("@CKA_SUBJECT", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_SUBJECT));
                cmd.Parameters.Add(new SqlParameter("@STATUS", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, status));
                cmd.Parameters.Add(new SqlParameter("@CertID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
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

        public bool HSM_Object_InsertUpdate(DataTable dtOld, DataTable dtNew, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                for (int i = 0; i < dtNew.Rows.Count; i++)
                {
                    int objectID_New = -1;
                    int slotID_New = Convert.ToInt32(dtNew.Rows[i]["SlotID"]);
                    string cka_LABEL_New = dtNew.Rows[i]["CKA_LABEL"].ToString();
                    int cka_CLASS_New = Convert.ToInt32(dtNew.Rows[i]["CKA_CLASS"]);
                    string cka_ID_New = dtNew.Rows[i]["CKA_ID"].ToString().Replace(" ", "");
                    string cka_SUBJECT_New = dtNew.Rows[i]["CKA_SUBJECT"].ToString();
                    int status_New = Convert.ToInt32(dtNew.Rows[i]["STATUS"]);
                    string certID_New = dtNew.Rows[i]["CertID"].ToString();

                    if (!isEmpty(dtNew.Rows[i]["ObjectID"]))
                    {
                        objectID_New = Convert.ToInt32(dtNew.Rows[i]["ObjectID"]);

                        //Lấy dữ liệu cũ tương ứng
                        DataRow drOld = dtOld.Select("ObjectID = " + objectID_New.ToString())[0];

                        //So sánh thay đổi
                        int slotID_Old = Convert.ToInt32(drOld["SlotID"]);
                        string cka_LABEL_Old = drOld["CKA_LABEL"].ToString();
                        int cka_CLASS_Old = Convert.ToInt32(drOld["CKA_CLASS"]);
                        string cka_ID_Old = drOld["CKA_ID"].ToString();
                        string cka_SUBJECT_Old = drOld["CKA_SUBJECT"].ToString();
                        int status_Old = Convert.ToInt32(drOld["STATUS"]);
                        string certID_Old = drOld["CertID"].ToString();

                        //Nếu không thay đổi -> không cập nhật
                        if (slotID_New == slotID_Old && cka_LABEL_New == cka_LABEL_Old && cka_CLASS_New == cka_CLASS_Old && cka_ID_New == cka_ID_Old
                            && cka_SUBJECT_New == cka_SUBJECT_Old && status_New == status_Old && certID_New == certID_Old)
                            continue;
                    }

                    SqlCommand command = new SqlCommand();
                    command.CommandText = "HSM_Object_InsertUpdate";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Transaction = transaction;

                    command.Parameters.Add(new SqlParameter("@ObjectID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, objectID_New));
                    command.Parameters.Add(new SqlParameter("@SlotID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotID_New));
                    command.Parameters.Add(new SqlParameter("@CKA_LABEL", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_LABEL_New));
                    command.Parameters.Add(new SqlParameter("@CKA_CLASS", SqlDbType.TinyInt, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_CLASS_New));
                    command.Parameters.Add(new SqlParameter("@CKA_ID", SqlDbType.VarBinary, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, StringToByteArray(cka_ID_New)));
                    command.Parameters.Add(new SqlParameter("@CKA_SUBJECT", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_SUBJECT_New));
                    command.Parameters.Add(new SqlParameter("@STATUS", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, status_New));
                    if (certID_New != "")
                        command.Parameters.Add(new SqlParameter("@CertID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certID_New));
                    else
                        command.Parameters.Add(new SqlParameter("@CertID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));
                    command.ExecuteNonQuery();
                }

                transaction.Commit();
                connection.Close();

                return true;
            }
            catch
            {
                if (connection.State == ConnectionState.Open)
                {
                    transaction.Rollback();
                    connection.Close();
                }
                return false;
            }
        }

        public bool HSM_Object_InsertCopy(int slotIDNew, int slotIDOld, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_InsertCopy";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@SlotIDNew", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotIDNew));
                cmd.Parameters.Add(new SqlParameter("@SlotIDOld", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotIDOld));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));

                cmd.ExecuteNonQuery();
                connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return false;
            }
        }
        
        public void HSM_Object_UpdateCertID_ByCKA_ID(byte[] cka_ID, int certID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_UpdateCertID_ByCKA_ID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@CKA_ID", SqlDbType.VarBinary, cka_ID.Length, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_ID));
                cmd.Parameters.Add(new SqlParameter("@CertID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certID));

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

        public bool HSM_Object_InsertUpdate_Certificate(int slotID, string label_Certificate, byte[] cka_ID, X509Certificate2 cert, int userID, string userModified)
        {            
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                //Cập nhật Object
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;
                cmd.Transaction = transaction;
                cmd.Parameters.Add(new SqlParameter("@SlotID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotID));
                cmd.Parameters.Add(new SqlParameter("@CKA_LABEL", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, label_Certificate));
                cmd.Parameters.Add(new SqlParameter("@CKA_CLASS", SqlDbType.TinyInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, (int)HSMObjectTypeDB.CERTIFICATE));
                cmd.Parameters.Add(new SqlParameter("@CKA_ID", SqlDbType.VarBinary, cka_ID.Length, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_ID));
                cmd.Parameters.Add(new SqlParameter("@CKA_SUBJECT", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.Subject));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));
                cmd.ExecuteNonQuery();

                //Cập nhật Certificate
                cmd = new SqlCommand();
                cmd.CommandText = "CA_Certificate_InsertUpdate_OutCertID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;
                cmd.Transaction = transaction;
                cmd.Parameters.Add(new SqlParameter("@NameCN", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.GetNameInfo(X509NameType.DnsName, false)));
                cmd.Parameters.Add(new SqlParameter("@Serial", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.SerialNumber));
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, 1));
                cmd.Parameters.Add(new SqlParameter("@Subject", SqlDbType.NVarChar, 300, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.Subject));
                cmd.Parameters.Add(new SqlParameter("@ValidFrom", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.NotBefore));
                cmd.Parameters.Add(new SqlParameter("@ValidTo", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.NotAfter));
                cmd.Parameters.Add(new SqlParameter("@ThumbPrint", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.Thumbprint));
                cmd.Parameters.Add(new SqlParameter("@RawData", SqlDbType.VarBinary, cert.RawData.Length, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cert.RawData));
                cmd.Parameters.Add(new SqlParameter("@IssuerID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));
                int certID = Convert.ToInt32(cmd.ExecuteScalar());

                //Liên kết với Object
                cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_UpdateCertID_ByCKA_ID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;
                cmd.Transaction = transaction;
                cmd.Parameters.Add(new SqlParameter("@CKA_ID", SqlDbType.VarBinary, cka_ID.Length, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_ID));
                cmd.Parameters.Add(new SqlParameter("@CertID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certID));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));
                cmd.ExecuteNonQuery();

                //Liên kết với User A0
                cmd = new SqlCommand();
                cmd.CommandText = "CA_User_UpdateCertID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;
                cmd.Transaction = transaction;
                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userID));
                cmd.Parameters.Add(new SqlParameter("@CertID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certID));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));
                cmd.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                return true;
            }
            catch
            {
                if (connection.State == ConnectionState.Open)
                {
                    transaction.Rollback();
                    connection.Close();
                }
                return false;
            }
        }

        public bool HSM_Object_DeleteBy_ObjectID(int objectID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_DeleteBy_ObjectID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@ObjectID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, objectID));

                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return false;
            }
        }

        public bool HSM_Object_UpdateBy_SlotID(int slotID, byte[] cka_ID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_Object_UpdateBy_SlotID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@SlotID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, slotID));
                cmd.Parameters.Add(new SqlParameter("@CKA_ID", SqlDbType.VarBinary, cka_ID.Length, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cka_ID));
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return false;
            }
        }

        #endregion

        #region HSM_WLDSlot

        public DataTable HSM_WLDSlot_SelectAll()
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_WLDSlot_SelectAll";
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

        public DataTable HSM_WLDSlot_SelectByWLDSlotID(int wldSlotID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_WLDSlot_SelectByWLDSlotID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@WLDSlotID", wldSlotID).Direction = ParameterDirection.Input;

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

        public DataTable HSM_WLDSlot_SelectByID_Serial(int wldSlotID, string tokenLabel, string serial)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_WLDSlot_SelectByID_Serial";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@WLDSlotID", wldSlotID);
                cmd.Parameters.AddWithValue("@TokenLabel", tokenLabel);
                cmd.Parameters.AddWithValue("@Serial", serial);

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

        public bool HSM_WLDSlot_Insert(int wldSlotID, string tokenLabel, string serial, string sSO_PIN, string user_PIN, string description, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_WLDSlot_Insert";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@WLDSlotID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, wldSlotID));
                cmd.Parameters.Add(new SqlParameter("@TokenLabel", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, tokenLabel));
                cmd.Parameters.Add(new SqlParameter("@Serial", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, serial));
                if (sSO_PIN == "")
                    cmd.Parameters.Add(new SqlParameter("@SO_PIN", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@SO_PIN", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sSO_PIN));
                if (user_PIN == "")
                    cmd.Parameters.Add(new SqlParameter("@User_PIN", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@User_PIN", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, user_PIN));
                cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, description));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));

                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return false;
            }
        }

        public bool HSM_WLDSlot_InsertUpdate(int wldSlotID, string tokenLabel, string serial, string sSO_PIN, string user_PIN,
                                            string description, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_WLDSlot_InsertUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.Add(new SqlParameter("@WLDSlotID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, wldSlotID));
                cmd.Parameters.Add(new SqlParameter("@TokenLabel", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, tokenLabel));
                cmd.Parameters.Add(new SqlParameter("@Serial", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, serial));
                if (sSO_PIN == "")
                    cmd.Parameters.Add(new SqlParameter("@SO_PIN", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@SO_PIN", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sSO_PIN));
                if (user_PIN == "")
                    cmd.Parameters.Add(new SqlParameter("@User_PIN", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmd.Parameters.Add(new SqlParameter("@User_PIN", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, user_PIN));
                cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, description));
                cmd.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));

                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return false;
            }
        }

        public bool HSM_WLDSlot_InsertUpdate(DataTable dtOld, DataTable dtNew, string userModified)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                for (int i = 0; i < dtNew.Rows.Count; i++)
                {
                    int wldSlotID_New = Convert.ToInt32(dtNew.Rows[i]["WLDSlotID"]);
                    string tokenLabel_New = dtNew.Rows[i]["TokenLabel"].ToString();
                    //string so_Pin_New = dtNew.Rows[i]["SO_PIN"].ToString();
                    //string user_Pin_New = dtNew.Rows[i]["User_PIN"].ToString();
                    string serial_New = dtNew.Rows[i]["Serial"].ToString();
                    string description_New = dtNew.Rows[i]["Description"].ToString();

                    if (!isEmpty(dtNew.Rows[i]["NAME"]))
                    {
                        //Lấy dữ liệu cũ tương ứng
                        DataRow drOld = dtOld.Select("WLDSlotID = " + wldSlotID_New.ToString())[0];

                        //So sánh thay đổi
                        int wldSlotID_Old = Convert.ToInt32(drOld["WLDSlotID"]);
                        string tokenLabel_Old = drOld["TokenLabel"].ToString();
                        //int cka_CLASS_Old = Convert.ToInt32(drOld["CKA_CLASS"]);
                        //string cka_ID_Old = drOld["CKA_ID"].ToString();
                        string serial_Old = drOld["Serial"].ToString();
                        string description_Old = drOld["Description"].ToString();

                        //Nếu không thay đổi -> không cập nhật
                        if (wldSlotID_New == wldSlotID_Old && tokenLabel_New == tokenLabel_Old
                            && serial_New == serial_Old && description_New == description_Old)
                            continue;
                    }

                    SqlCommand command = new SqlCommand();
                    command.CommandText = "HSM_WLDSlot_InsertUpdate";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Transaction = transaction;

                    command.Parameters.Add(new SqlParameter("@WLDSlotID", SqlDbType.Int, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, Convert.ToInt32(wldSlotID_New)));
                    command.Parameters.Add(new SqlParameter("@TokenLabel", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, tokenLabel_New));
                    command.Parameters.Add(new SqlParameter("@Serial", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, serial_New));
                    command.Parameters.Add(new SqlParameter("@SO_PIN", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@User_PIN", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@DESCRIPTION", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, description_New));
                    command.Parameters.Add(new SqlParameter("@UserModified", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, userModified));

                    command.ExecuteNonQuery();
                }
                transaction.Commit();
                connection.Close();

                return true;
            }
            catch
            {
                if (connection.State == ConnectionState.Open)
                {
                    transaction.Rollback();
                    connection.Close();
                }
                return false;
            }
        }

        public bool HSM_WLDSlot_DeleteByWLDSlotID(int wldSlotID)
        {
            SqlConnection connection = sc.GetConnection();
            connection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "HSM_WLDSlot_DeleteByWLDSlotID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@WLDSlotID", wldSlotID).Direction = ParameterDirection.Input;

                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return false;
            }
        }

        #endregion
    }
}
