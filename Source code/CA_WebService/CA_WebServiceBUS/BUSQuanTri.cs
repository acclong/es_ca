using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ES.CA_WebServiceDAL;

namespace ES.CA_WebServiceBUS
{
    public partial class BUSQuanTri
    {
        #region Constructor
        private DALQuanTri _dal;
        /// <summary>
        /// Khởi tạo dùng kết nối mặc định
        /// </summary>
        public BUSQuanTri()
        {
            _dal = new DALQuanTri();
        }

        /// <summary>
        /// Khởi tạo dùng kết nối tạm (đến database khác)
        /// </summary>
        /// <param name="strConn"></param>
        public BUSQuanTri(string strConn)
        {
            _dal = new DALQuanTri(strConn);
        }

        /// <summary>
        /// Khởi tạo dùng kết nối tạm (đến database khác)
        /// </summary>
        /// <param name="sComputerName"></param>
        /// <param name="sDBName"></param>
        /// <param name="sUserName"></param>
        /// <param name="sPassword"></param>
        public BUSQuanTri(string sComputerName, string sDBName, string sUserName, string sPassword)
        {
            _dal = new DALQuanTri(sComputerName, sDBName, sUserName, sPassword);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Lấy liên kết đơn vị và chương trình để kiểm tra
        /// </summary>
        /// <param name="LoaiDV"></param>
        /// <param name="MaDV"></param>
        /// <param name="TenChuongTrinh"></param>
        /// <returns></returns>
        public DataTable CA_UnitProgram_SelectByMaDV_ProgName(int loaiDV, string maDV, string programName)
        {
            return _dal.CA_UnitProgram_SelectByMaDV_ProgName(loaiDV, maDV, programName);
        }

        public DataTable CA_UnitProgram_SelectByMaDV_ProgName(int loaiDV, List<string> lstMaDV, string programName)
        {
            string arrMaDV = "";
            for (int i = 0; i < lstMaDV.Count; i++)
            {
                if (arrMaDV != "") arrMaDV += ";";
                arrMaDV += lstMaDV[i];
            }
            return _dal.CA_UnitProgram_SelectBy_ArrayMaDV_Prog(loaiDV, arrMaDV, programName);
        }

        /// <summary>
        /// Lấy thông tin đơn vị có đang sử dụng CA hay không theo danh sách
        /// </summary>
        /// <param name="loaiDV"></param>
        /// <param name="arrMaDV">Format: G10101;G10102;G25600;</param>
        /// <param name="programName"></param>
        /// <returns></returns>
        public DataTable CA_UnitProgram_SelectBy_ArrayMaDV_Prog(int loaiDV, string arrMaDV, string programName)
        {
            return _dal.CA_UnitProgram_SelectBy_ArrayMaDV_Prog(loaiDV, arrMaDV, programName);
        }

        /// <summary>
        /// Lấy chứng thư số tương ứng với user trong program
        /// </summary>
        /// <param name="programName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable CA_Certificate_SelectByProgUser(string programName, string userName)
        {
            return _dal.CA_Certificate_SelectByProgUser(programName, userName);
        }

        public DataTable FL_File_SelectFileID_ByNgayMaNMType(DateTime TuNgay, DateTime DenNgay, string Ma_NM, int FileTypeID)
        {
            return _dal.FL_File_SelectFileID_ByNgayMaNMType(TuNgay, DenNgay, Ma_NM, FileTypeID);
        }

        public void FL_FileRelation_Insert(int FileID_A, int FileID_B, int RelationTypeID, string programName, string userName)
        {
            _dal.FL_FileRelation_Insert(FileID_A, FileID_B, RelationTypeID, programName, userName);
        }

        /// <summary>
        /// Lấy chứng thư số để ký qua HSM
        /// </summary>
        /// <param name="programName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable CA_Certificate_SelectForSign(string programName, string userName)
        {
            DataTable dt = this.CA_Certificate_SelectByProgUser(programName, userName);
            if (dt.Rows.Count < 1)
                throw new Exception("WS_Không tìm thấy chứng thư hợp lệ của người dùng.");
            else if (dt.Rows.Count > 1)
                throw new Exception("WS_Có nhiều hơn 01 chứng thư hợp lệ.");
            else
                return dt;
        }

        /// <summary>
        /// Lấy chứng thư số để xác thực trong TTĐ khi nhận file từ đơn vị
        /// </summary>
        /// <param name="programName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable CA_Certificate_SelectForValid(string programName, string userName)
        {
            return this.CA_Certificate_SelectByProgUser(programName, userName);
        }

        /// <summary>
        /// Lấy chuỗi liên kết Chứng thư số - Người dùng - Hệ thống 
        /// </summary>
        /// <param name="certSerial"></param>
        /// <param name="programName"></param>
        /// <returns></returns>
        public DataTable CA_Certificate_SelectChainByCertProg(string programName, string userName, string certSerial)
        {
            return _dal.CA_Certificate_SelectChainByCertProg(programName, userName, certSerial);
        }

        /// <summary>
        /// Lấy chuỗi liên kết Chứng thư số - Người dùng - Hệ thống 
        /// </summary>
        /// <param name="certSerial"></param>
        /// <param name="programName"></param>
        /// <returns></returns>
        public DataTable CA_Certificate_SelectChainByCertProgUnit(string programName, string certSerial, string MaDV, int UnitTypeId)
        {
            return _dal.CA_Certificate_SelectChainByCertProgUnit(programName, certSerial, MaDV, UnitTypeId);
        }

        /// <summary>
        /// Lấy danh sách các slot chứa key theo certID
        /// </summary>
        /// <param name="certID"></param>
        /// <returns></returns>
        public DataTable HSM_Slot_SelectFormObjectByCertID(int certID)
        {
            return _dal.HSM_Slot_SelectFormObjectByCertID(certID);
        }

        public void HSM_Slot_UpdateUserPIN(int slotID, string newPIN)
        {
            _dal.HSM_Slot_UpdateUserPIN(slotID, newPIN);
        }

        /// <summary>
        /// Lấy thông tin privatekey trong HSM theo certID
        /// </summary>
        /// <param name="certID"></param>
        /// <returns></returns>
        public DataTable HSM_Object_SelectPrivateKeyByCertID(int certID)
        {
            DataTable dt = _dal.HSM_Object_SelectPrivateKeyByCertID(certID);
            if (dt.Rows.Count < 1)
                throw new Exception("WS_Không tìm thấy Private Key của người dùng.");
            else if (dt.Rows.Count > 1)
                throw new Exception("WS_Có nhiều hơn 01 Private Key của người dùng.");
            else
                return dt;
        }

        /// <summary>
        /// Lấy thông tin file theo FileID
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns></returns>
        public DataTable FL_File_SelectByFileID(int fileID)
        {
            return _dal.FL_File_SelectByFileID(fileID);
        }

        /// <summary>
        /// Kiểm tra trạng thái file có cho phép ký hay không, lấy thông tin file
        /// </summary>
        /// <param name="fileID"></param>
        /// <param name="programName"></param>
        /// <param name="userName"></param>
        /// <param name="dtFile"></param>
        /// <returns></returns>
        public bool FL_File_SelectForSign(int fileID, string programName, string userName, ref DataTable dtFile)
        {
            bool bOK = _dal.FL_File_SelectForSign(fileID, programName, userName, ref dtFile);
            if (dtFile.Rows.Count < 1)
                throw new Exception("WS_FileNotFound");

            return bOK;
        }

        /// <summary>
        /// Kiểm tra trạng thái nhiều file có cho phép ký hay không và ghi log. Trả về thông tin file và ID_Log phiên ký.
        /// </summary>
        /// <param name="arrFileID">Format: id1;id2;id3;</param>
        /// <param name="programName"></param>
        /// <param name="userName"></param>
        /// <param name="dtFile"></param>
        /// <returns></returns>
        public bool FL_File_SelectForAllowSign_Array(string arrFileID, string programName, string userName, ref DataTable dtFile)
        {
            bool bOK = _dal.FL_File_SelectForAllowSign_Array(arrFileID, programName, userName, ref dtFile);
            if (dtFile.Rows.Count < 1)
                throw new Exception("WS_Không tìm thấy file.");

            return bOK;
        }

        /// <summary>
        /// Xét phiên log file xem có cho phép lưu file hay không.
        /// </summary>
        /// <param name="id_StatusLog"></param>
        /// <returns></returns>
        public bool FL_File_SelectForSaveSign(int id_StatusLog)
        {
            return _dal.FL_File_SelectForSaveSign(id_StatusLog);
        }

        /// <summary>
        /// Cập nhật log ký/xóa chữ ký và thiết lập trạng thái về LuuFile
        /// </summary>
        /// <param name="fileID"></param>
        /// <param name="certSerial"></param>
        /// <param name="signTime"></param>
        /// <param name="verify"></param>
        /// <param name="signCreator"></param>
        /// <param name="action"></param>
        /// <param name="reason"></param>
        /// <param name="programName"></param>
        /// <param name="userName"></param>
        public void FL_File_UpdateForLogSign(int fileID, string certSerial, DateTime signTime, int verify, int signatureType,
            int action, string backupPath, string reason, string programName, string userName)
        {
            _dal.FL_File_UpdateForLogSign(fileID, certSerial, signTime, verify, signatureType, action, backupPath, reason, programName, userName);
        }

        public DataTable FL_File_CheckBeforeSaveSign(int fileID, string certSerial, string programName)
        {
            return _dal.FL_File_CheckBeforeSaveSign(fileID, certSerial, programName);
        }

        /// <summary>
        /// Toantk 21/10/2015: Kiểm tra xem chứng thư có quyền Xác nhận hay không
        /// </summary>
        /// <param name="fileID"></param>
        /// <param name="certID"></param>
        /// <returns></returns>
        public bool FL_FileType_QuyenXacNhan_CheckByFileID_CertID(int fileID, string certSerial)
        {
            DataTable dt = _dal.FL_FileType_QuyenXacNhan_CheckByFileID_CertID(fileID, certSerial);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Lấy đường dẫn file để ký
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns></returns>
        public string FL_File_GetFilePathForSign(int fileID)
        {
            DataTable dt = _dal.FL_File_SelectByFileID(fileID);

            if (dt.Rows.Count <= 0)
                throw new Exception("WS_FileNotFound");
            if (Convert.ToInt32(dt.Rows[0]["Status"]) == 0)
                throw new Exception("WS_FileNotSaved");

            return dt.Rows[0]["FilePath"].ToString();
        }

        /// <summary>
        /// Lấy đường dẫn của nhiều file để ký
        /// </summary>
        /// <param name="fileIDs"></param>
        /// <returns></returns>
        public Dictionary<int, string> FL_File_GetFilePaths(int[] fileIDs)
        {
            Dictionary<int, string> filePaths = new Dictionary<int, string>();
            foreach (int fileID in fileIDs)
            {
                DataTable dt = _dal.FL_File_SelectByFileID(fileID);

                if (dt.Rows.Count <= 0)
                    filePaths.Add(fileID, "Failed: FileNotFound");
                if (Convert.ToInt32(dt.Rows[0]["Status"]) == 0)
                    filePaths.Add(fileID, "Failed: FileNotSaved");

                filePaths.Add(fileID, dt.Rows[0]["FilePath"].ToString());
            }

            return filePaths;
        }

        public DataTable FL_File_InsertNewFile(string programName, string userName, int fileTypeID, string fileMaDV, DateTime fileDate,
            string fileName, string description)
        {
            return _dal.FL_File_InsertSelectNewFile(programName, userName, fileTypeID, fileMaDV, fileDate, fileName, description);
        }

        /// <summary>
        /// Toantk: Lưu chuỗi hash và cập nhật trạng thái sau khi tạo file.
        /// </summary>
        /// <param name="fileID"></param>
        /// <param name="status"></param>
        /// <param name="fileHash"></param>
        /// <param name="reason"></param>
        /// <param name="programName"></param>
        /// <param name="userName"></param>
        public void FL_File_UpdateStatus_WithHash(int fileID, int status, byte[] fileHash, string reason, string programName, string userName)
        {
            _dal.FL_File_UpdateStatus_WithHash(fileID, status, fileHash, reason, programName, userName);
        }

        public void FL_File_UpdateStatus(int fileID, int status, string reason, string programName, string userName)
        {
            _dal.FL_File_UpdateStatus(fileID, status, reason, programName, userName);
        }

        public void FL_LogFileSignature_Insert(int fileID, string certSerial, DateTime signTime, int verify,
            int action, string programName, string userName)
        {
            _dal.FL_LogFileSignature_Insert(fileID, certSerial, signTime, verify, action, programName, userName);
        }

        /// <summary>
        /// Toantk: Lấy chế độ làm việc của HSM (NORMAL/WLD)
        /// </summary>
        /// <returns></returns>
        public string Q_CONFIG_GetHSM_MODE()
        {
            DataTable dt = _dal.Q_CONFIG_SelectBy_ConfigID(12);
            return dt.Rows[0]["Value"].ToString();
        }

        private bool isEmpty(object obj)
        {
            if (obj == DBNull.Value || obj == null || obj.ToString().Trim() == "") return true;
            else return false;
        }
        #endregion
    }
}
