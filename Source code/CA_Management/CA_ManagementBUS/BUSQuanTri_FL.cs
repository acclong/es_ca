using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ES.CA_ManagementBUS
{
    public partial class BUSQuanTri
    {
        #region FL_File

        /// <summary>
        /// Ninhtq: Lấy danh sách văn bản
        /// </summary>
        /// <returns></returns>
        public DataTable FL_File_SelectAll()
        {
            return _dal.FL_File_SelectAll();
        }

        /// <summary>
        /// Ninhtq: Lấy văn bản theo file ID
        /// </summary>
        /// <returns></returns>
        public DataTable FL_File_SelectByFileID(int fileID)
        {
            return _dal.FL_File_SelectByFileID(fileID);
        }

        /// <summary>
        /// Toantk: Lấy file theo FileTypeID để kiểm tra Loại file có đang được sử dụng?
        /// </summary>
        /// <param name="fileTypeID"></param>
        /// <returns></returns>
        public bool FL_File_LoaiFileDangSuDung(int fileTypeID)
        {
            DataTable dt = _dal.FL_File_SelectByFileTypeID(fileTypeID);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Hieutm: Lấy mã đơn vị theo id loại file
        /// </summary>
        /// <param name="fileTypeId">id loại file</param>
        /// <returns></returns>
        public DataTable FL_File_SelectUnitIdByFileTypeID(int fileTypeId)
        {
            return _dal.FL_File_SelectUnitIdByFileTypeID(fileTypeId);
        }

        /// <summary>
        /// Hieutm: Lấy thông tin file theo loại file và đơn vị
        /// </summary>
        /// <param name="fileTypeId">id loại file</param>
        /// <param name="unitId">id đơn vị</param>
        /// <returns></returns>
        public DataTable FL_File_SelectByFileTypeId_UnitId(int fileTypeId, int unitId, DateTime begin, DateTime end)
        {
            return _dal.FL_File_SelectByFileTypeId_UnitId(fileTypeId, unitId, begin, end);
        }

        /// <summary>
        /// Ninhtq: Lấy thông tin file thay thế
        /// </summary>
        /// <param name="fileRelationId">Id file bị thay</param>
        /// <returns></returns>
        public DataTable FL_File_SelectByFileRelationID(int fileRelationId)
        {
            return _dal.FL_File_SelectByFileRelationID(fileRelationId);
        }

        /// <summary>
        /// Ninhtq: Lấy văn bản theo file ID và từ ngày đến ngày
        /// </summary>
        /// <returns></returns>
        public DataTable FL_File_SelectByUnitID_FromDateToDate(int UnitID, DateTime FromDate, DateTime ToDate)
        {
            return _dal.FL_File_SelectByUnitID_FromDateToDate(UnitID, FromDate, ToDate);
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách file theo loại hồ sơ, đơn vị và thời gian
        /// </summary>
        /// <param name="fileTypeId">id loại file</param>
        /// <param name="unitId">id đơn vị</param>
        /// <returns></returns>
        public DataTable FL_File_SelectProfileTypeID_DateType_UnitID_UnitType_Date(int profileTypeId, int unitType, int unitId, int DateType, DateTime date)
        {
            return _dal.FL_File_SelectProfileTypeID_DateType_UnitID_UnitType_Date(profileTypeId, unitType, unitId, DateType, date);
        }
        #endregion

        #region FL_LogFileSignature

        /// <summary>
        /// Ninhtq: Lấy danh sách trạng thái ký của văn bản theo ID
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns></returns>
        public DataTable FL_LogFileSignature_SelectByFileID(int fileID)
        {
            return _dal.FL_LogFileSignature_SelectByFileID(fileID);
        }
        #endregion

        #region FL_LogFileStatus

        /// <summary>
        /// Ninhtq: Lấy danh sách trạng thái của văn bản theo ID
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns></returns>
        public DataTable FL_LogFileStatus_SelectByFileID(int fileID)
        {
            return _dal.FL_LogFileStatus_SelectByFileID(fileID);
        }
        #endregion

        #region FL_FileRelation

        /// <summary>
        /// Toantk: Lấy các file liên quan theo RelationTypeID để kiểm tra Loại quan hệ có đang được sử dụng?
        /// </summary>
        /// <param name="relationTypeID"></param>
        /// <returns></returns>
        public bool FL_FileRelation_LoaiQuanHeDangSuDung(int relationTypeID)
        {
            DataTable dt = _dal.FL_FileRelation_SelectByRelationTypeID(relationTypeID);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// HieuTM: Lấy các thông tin file thay thế
        /// </summary>
        /// <param name="fileID"></param>
        /// <returns></returns>
        public DataTable FL_FileRelation_SelectBy_FileID(int fileID)
        {
            return _dal.FL_FileRelation_SelectBy_FileID(fileID);
        }
        #endregion

        #region FL_FileProfile

        /// <summary>
        /// Toantk: Lấy liên kết theo ProfileTypeID để kiểm tra Loại hồ sơ có đang được sử dụng?
        /// </summary>
        /// <param name="profileTypeID"></param>
        /// <returns></returns>
        public bool FL_FileProfile_LoaiHoSoDangSuDung(int profileTypeID)
        {
            DataTable dt = _dal.FL_FileProfile_SelectByProfileTypeID(profileTypeID);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách hồ sơ và văn bản 
        /// </summary>
        /// <returns></returns>
        public DataTable FL_FileProfile_SelectAll()
        {
            return _dal.FL_FileProfile_SelectAll();
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách liên kết loại hồ sơ và loại văn bản theo profileTypeID, ngày áp dụng và có tìm kiếm
        /// </summary>
        /// <returns></returns>
        public DataTable FL_FileProfile_SelectByProfileTypeID_Date_Search(int profileTypeID, DateTime date, string search)
        {
            return _dal.FL_FileProfile_SelectByProfileTypeID_Date_Search(profileTypeID, date, search);
        }

        /// <summary>
        /// Ninhtq: Lấy hồ sơ và văn bản theo ID
        /// </summary>
        /// <param name="idFileProfile"></param>
        /// <returns></returns>
        public DataTable FL_FileProfile_SelectByIDFileProfile(int idFileProfile)
        {
            return _dal.FL_FileProfile_SelectByIDFileProfile(idFileProfile);
        }

        /// <summary>
        /// Ninhtq: Lấy liên kết hồ sơ - văn bản theo FileTypeID và ProfileTypeID
        /// </summary>
        /// <param name="fileTypeID"></param>
        /// <param name="profileTypeID"></param>
        /// <returns></returns>
        public DataTable FL_FileProfile_FileTypeID_ProfileTypeID(int fileTypeID, int profileTypeID)
        {
            return _dal.FL_FileProfile_SelectByFileTypeID_ProfileTypeID(fileTypeID, profileTypeID);
        }

        /// <summary>
        /// ToanTK: lấy danh sách tất cả loại văn bản thuộc hồ sơ
        /// </summary>
        /// <param name="profileTypeID"></param>
        /// <returns></returns>
        public DataTable FL_FileProfile_SelectByProfileTypeID(int profileTypeID)
        {
            return _dal.FL_FileProfile_SelectByProfileTypeID(profileTypeID);
        }

        /// <summary>
        /// Toantk 26/5/2016: Lấy tất cả liên kết loại văn bản thuộc hồ sơ theo ngày áp dụng và từ khóa
        /// </summary>
        /// <param name="date"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public DataTable FL_FileProfile_SelectBy_Date_Search(DateTime date, string search)
        {
            return _dal.FL_FileProfile_SelectBy_Date_Search(date, search);
        }

        /// <summary>
        /// Ninhtq: Cập nhật liên kết loại hồ sơ - loại file
        /// </summary>
        /// <param name="idFileProfile"></param>
        /// <param name="fileTypeID"></param>
        /// <param name="proFileTypeID"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="sUserModified"></param>
        public void FL_FileProfile_InsertUpdate(int idFileProfile, int fileTypeID, int proFileTypeID, DateTime dateStart,
            DateTime dateEnd, string sUserModified)
        {
            _dal.FL_FileProfile_InsertUpdate(idFileProfile, fileTypeID, proFileTypeID, dateStart, dateEnd, sUserModified);
        }
        #endregion

        #region FL_FileType

        /// <summary>
        /// Ninhtq: Lấy danh sách các loại file
        /// </summary>
        /// <returns></returns>
        public DataTable FL_FileType_SelectAll()
        {
            return _dal.FL_FileType_SelectAll();
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách các loại file theo ngày áp dụng và có tìm kiếm
        /// </summary>
        /// <returns></returns>
        public DataTable FL_FileType_SelectByDateSearch(DateTime date, string search)
        {
            return _dal.FL_FileType_SelectByDateSearch(date, search);
        }

        /// <summary>
        /// Ninhtq: Lấy loại file theo id
        /// </summary>
        /// <param name="fileTypeID"></param>
        /// <returns></returns>
        public DataTable FL_FileType_SelectByFileTypeID(int fileTypeID)
        {
            return _dal.FL_FileType_SelectByFileTypeID(fileTypeID);
        }

        /// <summary>
        /// Toantk: Lấy loại file theo ký hiệu, nếu có thì trả về FileTypeID, nếu chưa có trả về 0.
        /// </summary>
        /// <param name="notation"></param>
        /// <returns></returns>
        public int FL_FileType_HasNotation(string notation)
        {
            DataTable dt = _dal.FL_FileType_SelectByNotation(notation);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["FileTypeID"]);
            else
                return 0;
        }

        /// <summary>
        /// Ninhtq: Xóa loại file theo id
        /// </summary>
        /// <param name="fileTypeID"></param>
        /// <returns></returns>
        public DataTable FL_FileType_DeleteByFileTypeID(int fileTypeID)
        {
            return _dal.FL_FileType_DeleteByFileTypeID(fileTypeID);
        }

        /// <summary>
        /// Ninhtq: Cập nhật danh sách loại file
        /// </summary>
        /// <param name="fileTypeID"></param>
        /// <param name="name"></param>
        /// <param name="dateType"></param>
        /// <param name="unitType"></param>
        /// <param name="notation"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="userModified"></param>
        public void FL_FileType_InsertUpdate(int fileTypeID, string name, int dateType, int unitType, string notation, DateTime dateStart, DateTime dateEnd, string userModified)
        {
            _dal.FL_FileType_InsertUpdate(fileTypeID, name, dateType, unitType, notation, dateStart, dateEnd, userModified);
        }

        /// <summary>
        /// Toantk: Kiểm tra fileTypeID đã được dùng hay chưa
        /// </summary>
        /// <param name="fileTypeID"></param>
        /// <returns></returns>
        public bool FL_FileType_HasFileTypeID(int fileTypeID)
        {
            DataTable dt = _dal.FL_FileType_SelectByFileTypeID(fileTypeID);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region FL_ProfileType

        /// <summary>
        /// Lấy danh sách các loại Hồ sơ
        /// </summary>
        /// <returns></returns>
        public DataTable FL_ProfileType_SelectAll()
        {
            return _dal.FL_ProfileType_SelectAll();
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách các loại Hồ sơ theo ngày áp dụng và có tìm kiếm
        /// </summary>
        /// <returns></returns>
        public DataTable FL_ProfileType_SelectByDateSearch(DateTime date, string search)
        {
            return _dal.FL_ProfileType_SelectByDateSearch(date, search);
        }

        /// <summary>
        /// Lấy loại hồ sơ theo ID
        /// </summary>
        /// <param name="profileTypeID"></param>
        /// <returns></returns>
        public DataTable FL_ProfileType_SelectByProfileTypeID(int profileTypeID)
        {
            return _dal.FL_ProfileType_SelectByProfileTypeID(profileTypeID);
        }

        /// <summary>
        /// Cập nhật loại hồ sơ
        /// </summary>
        /// <param name="profileTypeID"></param>
        /// <param name="name"></param>
        /// <param name="notation"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="userModified"></param>
        public void FL_ProfileType_InsertUpdate(int profileTypeID, string name, int unitType, int dateType, DateTime dateStart, DateTime dateEnd, string userModified)
        {
            _dal.FL_ProfileType_InsertUpdate(profileTypeID, name, unitType, dateType, dateStart, dateEnd, userModified);
        }

        /// <summary>
        /// ToanTK 25/5/2016: Thêm sửa loại hồ sơ và các văn bản thuộc hồ sơ
        /// </summary>
        /// <param name="profileTypeID"></param>
        /// <param name="name"></param>
        /// <param name="unitType"></param>
        /// <param name="dateType"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="userModified"></param>
        /// <param name="dtFileProfile"></param>
        public void FL_ProfileType_InsertUpdate_LienKetVB(int profileTypeID, string name, int unitType, int dateType, DateTime dateStart, DateTime dateEnd, string userModified, DataTable dtFileProfile)
        {
            _dal.FL_ProfileType_InsertUpdate_LienKetVB(profileTypeID, name, unitType, dateType, dateStart, dateEnd, userModified, dtFileProfile);
        }

        /// <summary>
        /// HieuTM: Insert Update FL_ProfileType tra ve ID duoc cap nhat
        /// </summary>
        /// <param name="profileTypeID"></param>
        /// <param name="name"></param>
        /// <param name="unitType"></param>
        /// <param name="dateType"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="userModified"></param>
        /// <returns></returns>
        public int FL_ProfileType_InsertUpdate_Out_ProfileTypeID(int profileTypeID, string name, int unitType, int dateType, DateTime dateStart, DateTime dateEnd, string userModified)
        {
            return _dal.FL_ProfileType_InsertUpdate_Out_ProfileTypeID(profileTypeID, name, unitType, dateType, dateStart, dateEnd, userModified);
        }

        /// <summary>
        /// Ninhtq: Xóa loại hồ sơ theo ID
        /// </summary>
        /// <param name="profileTypeID"></param>
        /// <returns></returns>
        /// HieuTM: Chuyển từ DataTable sang Bool(04/09/2015)
        public bool FL_ProfileType_DeleteByProfileTypeID(int profileTypeID)
        {
            return _dal.FL_ProfileType_DeleteByProfileTypeID(profileTypeID);
        }

        /// <summary>
        /// Lấy loại hồ sơ theo loại đơn vị
        /// </summary>
        /// <param name="profileTypeID"></param>
        /// <returns></returns>
        public DataTable FL_ProfileType_SelectByUnitType_DateType_Date(int UnitType, int DateType, DateTime Date)
        {
            return _dal.FL_ProfileType_SelectByUnitType_DateType_Date(UnitType, DateType, Date);
        }
        #endregion

        #region FL_RelationType

        /// <summary>
        /// Ninhtq: Lấy danh sách các loại file Liên quan
        /// </summary>
        /// <returns></returns>
        public DataTable FL_RelationType_SelectAll()
        {
            return _dal.FL_RelationType_SelectAll();
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách các loại file Liên quan theo ngày áp dụng và có tìm kiếm
        /// </summary>
        /// <returns></returns>
        public DataTable FL_RelationType_SelectByDateSearch(DateTime date, string search)
        {
            return _dal.FL_RelationType_SelectByDateSearch(date, search);
        }

        /// <summary>
        /// Ninhtq: Lấy loại file liên quan theo ID
        /// </summary>
        /// <param name="relationTypeID"></param>
        /// <returns></returns>
        public DataTable FL_RelationType_SelectByRelationTypeID(int relationTypeID)
        {
            return _dal.FL_RelationType_SelectByRelationTypeID(relationTypeID);
        }

        /// <summary>
        /// Ninhtq: Cập nhật loại file liên quan
        /// </summary>
        /// <param name="relationTypeID"></param>
        /// <param name="name"></param>
        /// <param name="notation"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="userModified"></param>
        public void FL_RelationType_InsertUpdate(int relationTypeID, string name, DateTime dateStart, DateTime dateEnd, string userModified)
        {
            _dal.FL_RelationType_InsertUpdate(relationTypeID, name, dateStart, dateEnd, userModified);
        }

        /// <summary>
        /// Ninhtq: Xóa loại hồ sơ theo ID
        /// </summary>
        /// <param name="relationTypeID"></param>
        /// <returns></returns>
        public DataTable FL_RelationType_DeleteByRelationTypeID(int relationTypeID)
        {
            return _dal.FL_RelationType_DeleteByRelationTypeID(relationTypeID);
        }

        #endregion

        #region FL_FileType_QuyenXacNhan
        /// <summary>
        /// Lấy danh sách quyền xác nhận theo loại văn bản
        /// </summary>
        /// <param name="FileTypeID"></param>
        /// <returns></returns>
        public DataTable FL_FileType_QuyenXacNhan_SelectBy_FileTypeID(int FileTypeID)
        {
            return _dal.FL_FileType_QuyenXacNhan_SelectBy_FileTypeID(FileTypeID);
        }

        /// <summary>
        /// Toantk 27/5/2016: Cập nhật thay đổi cấu hình
        /// </summary>
        /// <param name="_dtNew"></param>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public void FL_FileType_QuyenXacNhan__InsertUpdate(DataTable dtNew, string userModified)
        {
            _dal.FL_FileType_QuyenXacNhan__InsertUpdate(dtNew, userModified);
        }
        #endregion

        public bool ProfileType_FileProfile_InsertUpdate(DataTable profileType, DataTable fileProfile, string userModified)
        {
            return _dal.ProfileType_FileProfile_InsertUpdate(profileType, fileProfile, userModified);
        }
    }
}
