using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ES.CA_ManagementBUS
{
    public partial class BUSQuanTri
    {
        #region HSM_Device

        /// <summary>
        /// HieuTM: Lấy danh sách tất cả các thiếu bị HSM
        /// </summary>
        /// <returns></returns>
        public DataTable HSM_Device_SelectAll_Full()
        {
            return _dal.HSM_Device_SelectAll_Full();
        }

        /// <summary>
        /// Sangdm: Lấy danh sách tất cả các thiết bị HSM
        /// </summary>
        /// <returns></returns>
        public DataTable HSM_Device_SelectAll()
        {
            return _dal.HSM_Device_SelectAll();
        }

        public DataTable HSM_Device_SelectByNot_DeviceID(int deviceID)
        {
            return _dal.HSM_Device_SelectByNot_DeviceID(deviceID);
        }

        /// <summary>
        /// HieuTM: Lấy thông tin thiết bị theo chứng thư của người 
        /// </summary>
        /// <param name="certID"></param>
        /// <returns></returns>
        public DataTable HSM_Device_SelectBy_CertID(int certID)
        {
            return _dal.HSM_Device_SelectBy_CertID(certID);
        }

        /// <summary>
        /// Quanns: cập nhật mật khẩu cho HSM devices
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="newPIN"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Boolean HSM_Device_UpdatePIN(int deviceID, String newPIN, int type)
        {
            return _dal.HSM_Device_UpdatePIN(deviceID, newPIN, type);
        }

        /// <summary>
        /// HieuTM: Thêm mới, cập nhật thiết bị
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="serial"></param>
        /// <param name="so_pin"></param>
        /// <param name="user_pin"></param>
        /// <param name="usermodified"></param>
        /// <returns></returns>
        public bool HSM_Device_InsertUpdate(int deviceID, string name, string serial, string ip, string so_pin, string user_pin, string usermodified)
        {
            return _dal.HSM_Device_InsertUpdate(deviceID, name, serial, ip, so_pin, user_pin, usermodified);
        }

        /// <summary>
        /// HieuTM: Xoá thiết bị theo DeviceID
        /// </summary>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        public bool HSM_Device_DeleteBy_DeviceID(int deviceID)
        {
            return _dal.HSM_Device_DeleteBy_DeviceID(deviceID);
        }

        #endregion

        #region HSM_Slot

        /// <summary>
        /// Ninhtq: Lấy danh sách tất cả Slot
        /// </summary>
        /// <returns></returns>
        public DataTable HSM_Slot_SelectAll()
        {
            return _dal.HSM_Slot_SelectAll();
        }

        /// <summary>
        /// HieuTM:
        /// </summary>
        /// <returns></returns>
        public DataTable HSM_Slot_SelectAll_Editor()
        {
            return _dal.HSM_Slot_SelectAll_Editor();
        }

        /// <summary>
        /// HieuTM: Lay danh sach TokenLabel chua su dung trong wldSlot
        /// </summary>
        /// <returns></returns>
        public DataTable HSM_Slot_SelectAll_NotUse()
        {
            return _dal.HSM_Slot_SelectAll_NotUse();
        }

        /// <summary>
        /// HieuTM: Lấy danh sach theo CertID
        /// </summary>
        /// <param name="certID"></param>
        /// <returns></returns>
        public DataTable HSM_Slot_SelectBy_CertID(int certID)
        {
            return _dal.HSM_Slot_SelectBy_CertID(certID);
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách slot theo thiết bị HSM
        /// </summary>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        public DataTable HSM_Slot_SelectByDeviceID(int deviceID)
        {
            return _dal.HSM_Slot_SelectByDeviceID(deviceID);
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách slot theo trạng thái khởi tạo hay chưa.
        /// </summary>
        /// <param name="initToken"></param>
        /// <returns></returns>
        public DataTable HSM_Slot_SelectByInitToken(bool initToken)
        {
            return _dal.HSM_Slot_SelectByInitToken(initToken);
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách tất cả Slot mà có serial chưa tồn tại trong bảng HSM_WLDSlot
        /// </summary>
        /// <returns></returns>
        public DataTable HSM_Slot_SelectNotExistsHSM_WLDSlot()
        {
            return _dal.HSM_Slot_SelectNotExistsHSM_WLDSlot();
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách slot theo TokenLabel
        /// </summary>
        /// <param name="tokenLabel"></param>
        /// <returns></returns>
        public DataTable HSM_Slot_SelectByTokenLabel(string tokenLabel)
        {
            return _dal.HSM_Slot_SelectByTokenLabel(tokenLabel);
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách slot theo thiết bị HSM và TokenLabel
        /// </summary>
        /// <param name="tokenLabel"></param>
        /// <returns></returns>
        public DataTable HSM_Slot_SelectByTokenLabel_DeviceID(string tokenLabel, int deciveID)
        {
            return _dal.HSM_Slot_SelectByTokenLabel_DeviceID(tokenLabel, deciveID);
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách slot theo thiết bị HSM và slotSerial
        /// </summary>
        /// <param name="tokenLabel"></param>
        /// <returns></returns>
        public DataTable HSM_Slot_SelectBySlotSerial_DeviceID(string serial, int deciveID)
        {
            return _dal.HSM_Slot_SelectBySlotSerial_DeviceID(serial, deciveID);
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách slot theo slotID
        /// </summary>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        public DataTable HSM_Slot_SelectBySlotID(int slotID)
        {
            return _dal.HSM_Slot_SelectBySlotID(slotID);
        }

        /// <summary>
        /// Toantk: Lấy bảng thuộc tính của Slot cho Grid Editor
        /// </summary>
        /// <returns></returns>
        public DataSet HSM_Slot_Select_ComboboxEditor()
        {
            return _dal.HSM_Slot_Select_ComboboxEditor();
        }

        public void HSM_Slot_UpdateUserPIN(int slotID, string userPIN)
        {
            _dal.HSM_Slot_UpdateUserPIN(slotID, userPIN);
        }

        /// <summary>
        /// Sangdm: Cập nhật thông tin Slot
        /// </summary>
        /// <param name="bChangeAdminID"></param>
        /// <param name="slotID"></param>
        /// <param name="deviceID"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="label"></param>
        /// <param name="so_PIN"></param>
        /// <param name="user_PIN"></param>
        /// <returns></returns>
        public bool HSM_Slot_InsertUpdate(int slotID, int deviceID, int slotIndex, string serial, int type, bool initToken,
            string tokenLabel, string userModified)
        {
            return _dal.HSM_Slot_InsertUpdate(slotID, deviceID, slotIndex, serial, type, initToken, tokenLabel, userModified);
        }

        public bool HSM_Slot_UpdateUserPIN(DataTable data, string newPass)
        {
            return _dal.HSM_Slot_UpdateUserPIN(data, newPass);
        }

        /// <summary>
        /// Toantk: Cập nhật các thay đổi vào csdl
        /// </summary>
        /// <param name="dtOld"></param>
        /// <param name="dtNew"></param>
        /// <returns></returns>
        public bool HSM_Slot_InsertUpdate(DataTable dtOld, DataTable dtNew, string userModified)
        {
            return _dal.HSM_Slot_InsertUpdate(dtOld, dtNew, userModified);
        }

        /// <summary>
        /// Cập nhật Index slot theo HSM
        /// </summary>
        /// <param name="dtSlotHSM"></param>
        /// <returns></returns>
        public bool HSM_Slot_UpdateIndex(DataTable dtSlotHSM, string userModified)
        {
            return _dal.HSM_Slot_UpdateIndex(dtSlotHSM, userModified);
        }

        /// <summary>
        /// Sangdm: Xóa Slot
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="adminID"></param>
        /// <param name="slotID"></param>
        /// <returns></returns>
        public void HSM_Slot_DeleteBySlotSerial(int deviceID, string serial)
        {
            _dal.HSM_Slot_DeleteBySlotSerial(deviceID, serial);
        }

        /// <summary>
        /// HieuTM: Xóa bản ghi slot và object trong csdl theo slotID
        /// </summary>
        /// <param name="slotID"></param>
        /// <returns></returns>
        public bool HSM_Slot_DeleteBy_SlotID(int slotID)
        {
            return _dal.HSM_Slot_DeleteBy_SlotID(slotID);
        }

        #endregion

        #region HSM_Object

        /// <summary>
        /// Ninhtq: Bảng HSM_Object
        /// </summary>
        /// <returns></returns>
        public DataTable HSM_Object_SelectAll()
        {
            return _dal.HSM_Object_SelectAll();
        }

        /// <summary>
        /// Ninhtq: Lấy các object trong slot.
        /// </summary>
        /// <param name="slotID"></param>
        /// <returns></returns>
        public DataTable HSM_Object_SelectBySlotID(int slotID)
        {
            return _dal.HSM_Object_SelectBySlotID(slotID);
        }

        /// <summary>
        /// Toantk: Lấy thông tin chung của các object trong 1 slot
        /// </summary>
        /// <param name="slotID"></param>
        /// <returns></returns>
        public DataTable HSM_Object_SelectDistinctBySlotID(int slotID)
        {
            return _dal.HSM_Object_SelectDistinctBySlotID(slotID);
        }

        public DataTable HSM_Object_SelectBy_TokenLabel(string tokenLabel)
        {
            return _dal.HSM_Object_SelectBy_TokenLabel(tokenLabel);
        }

        /// <summary>
        /// Ninhtq: Lấy object theo ID
        /// </summary>
        /// <param name="objectID"></param>
        /// <returns></returns>
        public DataTable HSM_Object_SelectByObjectID(int objectID)
        {
            return _dal.HSM_Object_SelectByObjectID(objectID);
        }

        /// <summary>
        /// Toantk: Lấy bảng thuộc tính của Object cho Grid Editor
        /// </summary>
        /// <returns></returns>
        public DataSet HSM_Object_Select_ComboboxEditor()
        {
            return _dal.HSM_Object_Select_ComboboxEditor();
        }

        /// <summary>
        /// Ninhtq: Cập nhật object
        /// </summary>
        /// <param name="objectID"></param>
        /// <param name="slotID"></param>
        /// <param name="cka_LABEL"></param>
        /// <param name="cka_CLASS"></param>
        /// <param name="cka_ID"></param>
        /// <param name="cka_SUBJECT"></param>
        /// <param name="certID"></param>
        public void HSM_Object_Insert(int slotID, string cka_LABEL, Int16 cka_CLASS, byte[] cka_ID, string cka_SUBJECT, string userModified)
        {
            _dal.HSM_Object_Insert(slotID, cka_LABEL, cka_CLASS, cka_ID, cka_SUBJECT, userModified);
        }

        /// <summary>
        /// Toantk: Thêm private key, public key và cert request vào db sau khi tạo trên HSM
        /// </summary>
        /// <param name="slotID"></param>
        /// <param name="subject"></param>
        /// <param name="keyPUB"></param>
        /// <param name="keyPRV"></param>
        /// <param name="certRequest"></param>
        /// <param name="cka_ID"></param>
        public bool HSM_Object_Insert_3Obj(int slotID, string subject, string keyPUB, string keyPRV, string certRequest, byte[] cka_ID, string userModified)
        {
            return _dal.HSM_Object_Insert(slotID, subject, keyPUB, keyPRV, certRequest, cka_ID, userModified);
        }

        /// <summary>
        /// Ninhtq: Thêm mới HSM_Object giống HSM_Object khác slotID
        /// </summary>
        /// <param name="slotID"></param>
        /// <param name="userModified"></param>
        public bool HSM_Object_InsertCopy(int slotIDNew, int slotIDOld, string userModified)
        {
            return _dal.HSM_Object_InsertCopy(slotIDNew, slotIDOld, userModified);
        }

        /// <summary>
        /// Toantk: Cập nhật các thay đổi vào csdl
        /// </summary>
        /// <param name="dtOld"></param>
        /// <param name="dtNew"></param>
        /// <returns></returns>
        public bool HSM_Object_InsertUpdate(DataTable dtOld, DataTable dtNew, string userModified)
        {
            return _dal.HSM_Object_InsertUpdate(dtOld, dtNew, userModified);
        }

        /// <summary>
        /// Toantk: Cập nhật CertID để liên kết object với chứng thư số
        /// </summary>
        /// <param name="cka_ID"></param>
        /// <param name="certID"></param>
        public void HSM_Object_UpdateCertID_ByCKA_ID(byte[] cka_ID, int certID)
        {
            _dal.HSM_Object_UpdateCertID_ByCKA_ID(cka_ID, certID);
        }

        /// <summary>
        /// Toantk: Thêm object chứng thư và chứng thư, cập nhật liên kết CertID
        /// </summary>
        /// <param name="slotID"></param>
        /// <param name="label_Certificate"></param>
        /// <param name="cka_ID"></param>
        /// <param name="x509Cert"></param>
        /// <param name="userModified"></param>
        /// <returns></returns>
        public bool HSM_Object_InsertUpdate_Certificate(int slotID, string label_Certificate, byte[] cka_ID, X509Certificate2 x509Cert, int userID, string userModified)
        {
            return _dal.HSM_Object_InsertUpdate_Certificate(slotID, label_Certificate, cka_ID, x509Cert, userID,  userModified);
        }

        /// <summary>
        /// HieuTM: Xoa thiet bi theo objectID
        /// </summary>
        /// <param name="objectID"></param>
        /// <returns></returns>
        public bool HSM_Object_DeleteBy_ObjectID(int objectID)
        {
            return _dal.HSM_Object_DeleteBy_ObjectID(objectID);
        }

        /// <summary>
        /// HieuTM: Cập nhật lại object khi tao lại ma rsa
        /// </summary>
        /// <param name="slotID"></param>
        /// <param name="cka_ID"></param>
        /// <returns></returns>
        public bool HSM_Object_UpdateBy_SlotID(int slotID, byte[] cka_ID)
        {
            return _dal.HSM_Object_UpdateBy_SlotID(slotID, cka_ID);
        }
        #endregion

        #region HSM_WLDSlot

        /// <summary>
        /// Ninhtq: Lấy danh sách tất cả WLD_Slot
        /// </summary>
        /// <returns></returns>
        public DataTable HSM_WLDSlot_SelectAll()
        {
            return _dal.HSM_WLDSlot_SelectAll();
        }

        /// <summary>
        /// Ninhtq: Thêm mới 1 bản ghi vào HSM_WLDSlot
        /// </summary>
        /// <param name="wldSlotID"></param>
        /// <param name="tokenLabel"></param>
        /// <param name="serial"></param>
        /// <param name="sSO_PIN"></param>
        /// <param name="user_PIN"></param>
        /// <param name="description"></param>
        /// <param name="userModified"></param>
        public bool HSM_WLDSlot_Insert(int wldSlotID, string tokenLabel, string serial, string sSO_PIN, string user_PIN, string description, string userModified)
        {
            return _dal.HSM_WLDSlot_Insert(wldSlotID, tokenLabel, serial, sSO_PIN, user_PIN, description, userModified);
        }

        /// <summary>
        /// HieuTM: Insert Update dư liệu lên bảng HSM_WLDSlot
        /// </summary>
        /// <param name="wldSlotID"></param>
        /// <param name="tokenLabel"></param>
        /// <param name="serial"></param>
        /// <param name="sSO_PIN"></param>
        /// <param name="user_PIN"></param>
        /// <param name="description"></param>
        /// <param name="userModified"></param>
        /// <returns></returns>
        public bool HSM_WLDSlot_InsertUpdate(int wldSlotID, string tokenLabel, string serial, string sSO_PIN, string user_PIN,
                                            string description, string userModified)
        {
            return _dal.HSM_WLDSlot_InsertUpdate(wldSlotID, tokenLabel, serial, sSO_PIN, user_PIN,
                                            description, userModified);
        }

        /// <summary>
        /// HieuTM: so sanh 2 gia tri cu va moi roi insert update
        /// </summary>
        /// <param name="dtOld"></param>
        /// <param name="dtNew"></param>
        /// <param name="userModified"></param>
        /// <returns></returns>
        public bool HSM_WLDSlot_InsertUpdate(DataTable dtOld, DataTable dtNew, string userModified)
        {
            return _dal.HSM_WLDSlot_InsertUpdate(dtOld, dtNew, userModified);
        }

        /// <summary>
        /// Ninhtq: Xóa wldSlot theo ID
        /// </summary>
        /// <param name="wldSlotID"></param>
        public bool HSM_WLDSlot_DeleteByWLDSlotID(int wldSlotID)
        {
            return _dal.HSM_WLDSlot_DeleteByWLDSlotID(wldSlotID);
        }

        /// <summary>
        /// Ninhtq: Lấy WLDSlot theo ID
        /// </summary>
        /// <returns></returns>
        public DataTable HSM_WLDSlot_SelectByWLDSlotID(int wldSlotID)
        {
            return _dal.HSM_WLDSlot_SelectByWLDSlotID(wldSlotID);
        }

        /// <summary>
        /// Toantk: Kiểm tra ID, token label hoặc serial đã được sử dụng hay chưa.
        /// </summary>
        /// <returns></returns>
        public bool HSM_WLDSlot_Select_CheckUsed(int wldSlotID, string tokenLabel, string serial)
        {
            DataTable dt = _dal.HSM_WLDSlot_SelectByID_Serial(wldSlotID, tokenLabel, serial);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        #endregion
    }
}
