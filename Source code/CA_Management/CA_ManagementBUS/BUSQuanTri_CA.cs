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
        #region CA_Program

        /// <summary>
        /// Ninhtq: Lấy tất cả Hệ thống tích hợp
        /// </summary>
        /// <returns></returns>
        public DataTable CA_Program_SelectAll()
        {
            return _dal.CA_Program_SelectAll();
        }

        /// <summary>
        /// Ninhtq: Lấy Hệ thống tích hợp theo ID
        /// </summary>
        /// <param name="iProgID"></param>
        /// <returns></returns>
        public DataTable CA_Program_SelectByProgID(int iProgID)
        {
            return _dal.CA_Program_SelectByProgID(iProgID);
        }

        /// <summary>
        /// Toantk: Lấy Hệ thống tích hợp theo Mã chương trình để kiểm tra, nếu có thì trả về ProgID, nếu chưa có trả về 0.
        /// </summary>
        /// <param name="sProgName"></param>
        /// <returns></returns>
        public int CA_Program_HasProgName(string sProgName)
        {
            DataTable dt = _dal.CA_Program_SelectByProgName(sProgName);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ProgID"]);
            else
                return 0;
        }

        /// <summary>
        /// Toantk: Lấy Hệ thống tích hợp theo Ký hiệu để kiểm tra, nếu có thì trả về ProgID, nếu chưa có trả về 0.
        /// </summary>
        /// <param name="sProgName"></param>
        /// <returns></returns>
        public int CA_Program_HasNotation(string sNotation)
        {
            DataTable dt = _dal.CA_Program_SelectByNotation(sNotation);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ProgID"]);
            else
                return 0;
        }

        /// <summary>
        /// Ninhtq: Cập nhật Hệ thống tích hợp
        /// </summary>
        /// <param name="iProgID"></param>
        /// <param name="sProgName"></param>
        /// <param name="sName"></param>
        /// <param name="sNotation"></param>
        /// <param name="iStatus"></param>
        /// <param name="sServerName"></param>
        /// <param name="sDBName"></param>
        /// <param name="sUserDB"></param>
        /// <param name="sPassword"></param>
        /// <param name="sTableUser"></param>
        /// <param name="sColummUserID"></param>
        /// <param name="sColummUserName"></param>
        /// <param name="sUserModified"></param>
        public void CA_Program_InsertUpdate(int iProgID, string sProgName, string sName, string sNotation, int iStatus, string sServerName, string sDBName,
            string sUserDB, string sPassword, string sQueryUser, string sUserModified)
        {
            _dal.CA_Program_InsertUpdate(iProgID, sProgName, sName, sNotation, iStatus, sServerName, sDBName,
                sUserDB, sPassword, sQueryUser, sUserModified);
        }

        /// <summary>
        /// HieuTM: Xóa bản ghi trong Bảng Program
        /// </summary>
        /// <param name="progID"></param>
        /// <returns></returns>
        public bool CA_Program_DeleteByProgID(int progID)
        {
            bool kq = false;

            if (_dal.CA_UnitProgram_SelectByProgID(progID).Rows.Count == 0 && _dal.CA_UserProgram_SelectByProgID(progID).Rows.Count == 0)
            {
                kq = _dal.CA_Program_DeleteByProgID(progID);
            }

            return kq;
        }

        #endregion

        #region CA_Program_Log

        public DataTable CA_Program_Log_SelectBy_ProgID(int iProgID)
        {
            return _dal.CA_Program_Log_SelectBy_ProgID(iProgID);
        }

        #endregion

        #region CA_Unit

        public DataTable CA_Unit_Select_Check(int ID_UserProg, string Seach)
        {
            return _dal.CA_Unit_Select_Check(ID_UserProg, Seach);
        }

        /// <summary>
        /// HieuTM: Lấy đơn vị theo loại đơn vị
        /// </summary>
        /// <param name="unitTypeID"></param>
        /// <returns></returns>
        public DataTable CA_Unit_SelectBy_UnitTypeID(int unitTypeID)
        {
            return _dal.CA_Unit_SelectBy_UnitTypeID(unitTypeID);
        }

        /// <summary>
        /// HieuTM: Lấy toàn bộ danh sách đơn vị cùng mã đơn vị
        /// </summary>
        /// <returns></returns>
        public DataTable CA_Unit_SelectByAll_UserUnit()
        {
            return _dal.CA_Unit_SelectByAll_UserUnit();
        }

        public DataTable CA_Unit_SelectBy_UserIDQuyen(int userID, int progID)
        {
            return _dal.CA_Unit_SelectBy_UserIDQuyen(userID, progID);
        }

        public bool CA_Unit_DeleteBy_UnitID(int unitID)
        {
            return _dal.CA_Unit_DeleteBy_UnitID(unitID);
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách tất cả ID, tên đơn vị cho combobox
        /// </summary>
        /// <returns></returns>
        public DataTable CA_Unit_SelectUnitName()
        {
            return _dal.CA_Unit_SelectUnitName();
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách tất cả Đơn vị
        /// </summary>
        /// <returns></returns>
        public DataTable CA_Unit_SelectAll()
        {
            return _dal.CA_Unit_SelectAll();
        }

        /// <summary>
        /// Ninhtq: Lấy Đơn vị theo ID
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public DataTable CA_Unit_SelectByUnitID(int unitID)
        {
            return _dal.CA_Unit_SelectByUnitID(unitID);
        }

        /// <summary>
        /// HieuTM: Lấy danh sách đơn vị theo loại đơn vị và trạng thái
        /// Khi không lọc truyền tham số -1
        /// </summary>
        /// <param name="unitTypeID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public DataTable CA_Unit_SelectBy_UnitTypeID_Status(int unitTypeID, int status)
        {
            return _dal.CA_Unit_SelectBy_UnitTypeID_Status(unitTypeID, status);
        }

        public int CA_Unit_Has_Notation_UnitTypeID(string notation, int unitTypeID)
        {
            DataTable dt = _dal.CA_Unit_SelectBy_Notation_UnitTypeID(notation, unitTypeID);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["UnitID"]);
            else
                return 0;
        }

        /// <summary>
        /// Toantk: Lấy Đơn vị theo Ký hiệu để kiểm tra, nếu có thì trả về UnitID, nếu chưa có trả về 0.
        /// </summary>
        /// <param name="sNotation"></param>
        /// <returns></returns>
        public int CA_Unit_HasNotation(string sNotation)
        {
            DataTable dt = _dal.CA_Unit_SelectByNotation(sNotation);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["UnitID"]);
            else
                return 0;
        }

        /// <summary>
        /// Toantk: Lấy Đơn vị theo Mã đơn vị để kiểm tra, nếu có thì trả về UnitID, nếu chưa có trả về 0.
        /// </summary>
        /// <param name="sMaDV"></param>
        /// <returns></returns>
        public int CA_Unit_HasMaDV(string sMaDV)
        {
            DataTable dt = _dal.CA_Unit_SelectByMaDV(sMaDV);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["UnitID"]);
            else
                return 0;
        }

        /// <summary>
        /// Hieutm: Lấy thông tin các đơn vị theo loại văn bản
        /// </summary>
        /// <param name="fileTypeId">id loại đơn vị</param>
        /// <returns></returns>       
        public DataTable CA_Unit_SelectByFileTypeID(int fileTypeId)
        {
            return _dal.CA_Unit_SelectByFileTypeID(fileTypeId);
        }

        /// <summary>
        /// Hieutm: Lấy thông tin đơn vi theo mã đơn vị
        /// </summary>
        /// <param name="maDV"></param>
        /// <returns></returns>
        public DataTable CA_Unit_SelectByMaDV(string maDV)
        {
            return _dal.CA_Unit_SelectByMaDV(maDV);
        }

        /// <summary>
        /// Quanns: Lấy danh sách tất cả những đơn vị chứa người dùng chưa liên kết với chứng thư
        /// </summary>
        /// <returns></returns>
        public DataTable CA_Unit_SelectAllUnitHasUnmappingUser()
        {
            return _dal.CA_Unit_SelectAllUnitHasUnmappingUser();
        }

        /// <summary>
        /// Ninhtq: Cập nhật Đơn vị
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <param name="sMaDV"></param>
        /// <param name="sName"></param>
        /// <param name="sNotation"></param>
        /// <param name="iStatus"></param>
        /// <param name="validFrom"></param>
        /// <param name="validTo"></param>
        /// <param name="iUnitTypeID"></param>
        /// <param name="iParentID"></param>
        /// <param name="sUserModified"></param>
        public void CA_Unit_InsertUpdate(int iUnitID, string sMaDV, string sName, string sNotation, int iStatus, DateTime validFrom,
            DateTime validTo, int iUnitTypeID, int iParentID, string TenTat, int mien, string sUserModified)
        {
            _dal.CA_Unit_InsertUpdate(iUnitID, sMaDV, sName, sNotation, iStatus, validFrom,
                validTo, iUnitTypeID, iParentID, TenTat, mien, sUserModified);
        }

        #endregion

        #region CA_Unit_Log

        public DataTable CA_Unit_Log_SelectBy_UnitID(int unitID)
        {
            return _dal.CA_Unit_Log_SelectBy_UnitID(unitID);
        }

        #endregion

        #region CA_User

        /// <summary>
        /// HieuTM: Xoa bản ghi trong bảng User
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool CA_User_DeleteBy_UserID(int userID)
        {
            return _dal.CA_User_DeleteBy_UserID(userID);
        }

        /// <summary>
        /// Lấy danh sách tất cả người dùng
        /// </summary>
        /// <returns></returns>
        public DataTable CA_User_SelectAllWithDate()
        {
            return _dal.CA_User_SelectAll();
        }

        /// <summary>
        /// HieuTM: Lấy thông tin User theo tìm kiếm và trạng thái
        /// </summary>
        /// <param name="seach"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public DataTable CA_User_SelectBy_Status_Seach(string seach, int status)
        {
            return _dal.CA_User_SelectBy_Status_Seach(seach, status);
        }

        /// <summary>
        /// Toantk: Lấy danh sách người dùng CA và xếp theo thứ tự Tên - Họ
        /// </summary>
        /// <returns></returns>
        public DataTable CA_User_SelectAll_SapXepTheoHoten()
        {
            DataTable dt = _dal.CA_User_SelectAll();

            //Edited by Toantk on 16/4/2015
            //sắp xếp và thêm số thứ tự vào DataTable theo Tên tiếng Việt
            //Thêm cột Ten
            dt.Columns.Add("TEN", typeof(string));

            //Tách phần tên từ trường Họ và tên
            foreach (DataRow dr in dt.Rows)
            {
                string[] hoten = dr["Name"].ToString().Split(new char[] { ' ' });
                if (hoten.Count() > 0)
                    dr["TEN"] = hoten[hoten.Count() - 1];
            }

            //Order theo Tên và cập nhật STT
            dt.DefaultView.Sort = "TEN";
            dt = dt.DefaultView.ToTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["STT"] = i + 1;
            }

            //Xóa cột Ten
            dt.Columns.Remove("TEN");

            return dt;
        }

        /// <summary>
        /// Ninhtq: Lấy Người dùng theo ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable CA_User_SelectByUserID(int userID)
        {
            return _dal.CA_User_SelectByUserID(userID);
        }

        /// <summary>
        /// HieuTM: Lấy Người dùng theo CertID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable CA_User_SelectBy_CertID(int userID)
        {
            return _dal.CA_User_SelectBy_CertID(userID);
        }

        /// <summary>
        /// Toantk: Lấy Người dùng theo CMND để kiểm tra, nếu có thì trả về UserID, nếu chưa có trả về 0.
        /// </summary>
        /// <param name="CMND"></param>
        /// <returns></returns>
        public int CA_User_HasCMND(string CMND)
        {
            DataTable dt = _dal.CA_User_SelectByCMND(CMND);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["UserID"]);
            else
                return 0;
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách người dùng theo đơn vị(chuyển bảng?)
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public DataTable CA_User_SelectByUnitID(int UnitID)
        {
            return _dal.CA_User_SelectByUnitID(UnitID);
        }

        /// <summary>
        /// Quanns: Lấy danh sách tất cả người dùng chưa liên kết với chứng thư
        /// </summary>
        /// <returns></returns>
        public DataTable CA_User_SelectAllUnmappingUser()
        {
            return _dal.CA_User_SelectAllUnmappingUser();
        }

        /// <summary>
        /// Ninhtq: Cập nhật người dùng
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="sName"></param>
        /// <param name="CMND"></param>
        /// <param name="iStatus"></param>
        /// <param name="validFrom"></param>
        /// <param name="validTo"></param>
        /// <param name="sUserModified"></param>
        /// //Edited by Hieutm on 15/6/2015
        //Thêm trường Mã đơn vị
        public void CA_User_InsertUpdate(int iUserID, string sName, string CMND, int iStatus, DateTime validFrom, DateTime validTo, string email, string sUserModified, int unitID, string description, int certID)
        {
            _dal.CA_User_InsertUpdate(iUserID, sName, CMND, iStatus, validFrom, validTo, email, sUserModified, unitID, description, certID);
        }

        #endregion

        #region CA_User_Log

        /// <summary>
        /// HieuTM: Lấy thông tin log theo Id người dùng
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable CA_User_Log_SelectBy_UserID(int userID)
        {
            return _dal.CA_User_Log_SelectBy_UserID(userID);
        }

        #endregion

        #region CA_Certificate

        public DataTable CA_Certificate_SelectBy_Seach_Status(string seach, int status)
        {
            return _dal.CA_Certificate_SelectBy_Seach_Status(seach, status);
        }

        /// <summary>
        /// HieuTM: Xóa bản ghi trong certificate
        /// </summary>
        /// <param name="certID"></param>
        /// <returns></returns>
        public bool CA_Certificate_DeleteBy_CertID(int certID)
        {
            return _dal.CA_Certificate_DeleteBy_CertID(certID);
        }

        /// <summary>
        /// HieuTM: Lấy ra các chứng thư chưa được gán cho ai
        /// </summary>
        /// <returns></returns>
        public DataTable CA_Certificate_SelectBy_NotUse(int userID)
        {
            return _dal.CA_Certificate_SelectBy_NotUse(userID);
        }

        /// <summary>
        /// Toantk: Lấy tất cả certificate trong hệ thống
        /// </summary>
        /// <returns></returns>
        public DataTable CA_Certificate_SelectAll()
        {
            return _dal.CA_Certificate_SelectAll();
        }

        /// <summary>
        /// Ninhtq: Lấy rawdata của certificate theo CertID
        /// </summary>
        /// <param name="id">CertID</param>
        /// <returns></returns>
        public byte[] CA_Certificate_SelectRawDataByID(int id)
        {
            DataTable dt = _dal.CA_Certificate_SelectRawDataByID(id);
            if (dt.Rows.Count < 1)
                throw new Exception("Không tìm thấy thông tin chứng thư số.");
            else
                return Convert.FromBase64String(dt.Rows[0]["RawBase64"].ToString());
        }

        /// <summary>
        /// Toantk: Lấy chứng thư trong db theo serial, nếu có thì trả về CertID, nếu chưa có trả về 0.
        /// </summary>
        /// <param name="serial">CertID</param>
        /// <returns></returns>
        public int CA_Certificate_HasCertificate(string serial)
        {
            DataTable dt = _dal.CA_Certificate_SelectBySerial(serial);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["CertID"]);
            else
                return 0;
        }

        /// <summary>
        /// Quanns: lấy nội dung chứng thư dựa trên id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable CA_Certificate_SelectByCertID(int id)
        {
            return _dal.CA_Certificate_SelectByCertID(id);
        }

        /// <summary>
        /// Quanns: thêm mới một chứng thư
        /// </summary>
        /// <param name="cert"></param>
        /// <param name="fileCert"></param>
        /// <param name="username"></param>
        /// <param name="status"></param>
        /// <param name="issuerID"></param>
        public void CA_Certificate_Insert(X509Certificate2 cert, String username, int status, int issuerID, int certType)
        {
            _dal.CA_Certificate_Insert(cert, username, status, issuerID, certType);
        }

        /// <summary>
        /// Quanns: cập nhật thông tin mới cho chứng thư
        /// </summary>
        /// <param name="certID"></param>
        /// <param name="status"></param>
        /// <param name="userModified"></param>
        public void CA_Certificate_Update(int certID, int status, string userModified, int certType)
        {
            _dal.CA_Certificate_Update(certID, status, userModified, certType);
        }

        /// <summary>
        /// Ninhtq: Insert mới 1 certificate vào db lấy ra giá trị certID vừa thêm mới
        /// </summary>
        /// <param name="cert"></param>
        /// <param name="fileCert"></param>
        /// <param name="username"></param>
        /// <param name="status"></param>
        /// <param name="issuerID"></param>
        /// <returns>giá trị certID vừa thêm mới</returns>
        public int CA_Certificate_InsertUpdate_OutCertID(X509Certificate2 cert, String username, int status)
        {
            return _dal.CA_Certificate_InsertUpdate_OutCertID(cert, username, status);
        }

        #endregion

        #region CA_Certificate_Log

        /// <summary>
        /// HieuTM: Lấy thông tin của log của bảng cert theo CertID
        /// </summary>
        /// <param name="certID"></param>
        /// <returns></returns>
        public DataTable CA_Certificate_Log_SelectBy_CertID(int certID)
        {
            return _dal.CA_Certificate_Log_SelectBy_CertID(certID);
        }

        #endregion

        #region CA_UnitProgram

        /// <summary>
        /// HieuTM: Thêm, sửa bản ghi của bảng CA_UnitProgram
        /// </summary>
        /// <param name="idUnitProg"></param>
        /// <param name="progID"></param>
        /// <param name="unitID"></param>
        /// <param name="status"></param>
        /// <param name="userModified"></param>
        /// <returns></returns>
        public bool CA_UnitProgram_InsertUpdate(int idUnitProg, int progID, int unitID, int status, string userModified)
        {
            return _dal.CA_UnitProgram_InsertUpdate(idUnitProg, progID, unitID, status, userModified);
        }

        public bool CA_UnitProgram_InsertUpdate(DataTable oldData, DataTable newData, string userModified)
        {
            return _dal.CA_UnitProgram_InsertUpdate(oldData, newData, userModified);
        }

        /// <summary>
        /// HieuTM: Lây thông tin đơn vị - hệ thống theo trạng thái, hệ thống và tìm kiếm.
        /// Muốn loại bổ lọc nào truyền tham số bằng -1
        /// </summary>
        /// <param name="status"></param>
        /// <param name="seach"></param>
        /// <param name="progID"></param>
        /// <returns></returns>
        public DataTable CA_UnitProgram_SelectBy_ProgID_Status_Seach(int status, string seach, int progID)
        {
            return _dal.CA_UnitProgram_SelectBy_ProgID_Status_Seach(status, seach, progID);
        }

        /// <summary>
        /// HieuTM: Lây thông tin đơn vị - hệ thống theo trạng thái và tìm kiếm.
        /// Muốn loại bổ lọc nào truyền tham số bằng -1
        /// </summary>
        /// <param name="status"></param>
        /// <param name="seach"></param>
        /// <returns></returns>
        public DataTable CA_UnitProgram_SelectBy_Status_Seach(int status, string seach)
        {
            return _dal.CA_UnitProgram_SelectBy_Status_Seach(status, seach);
        }

        /// <summary>
        /// HieuTM: Lấy tất cả thông tin hệ thống và đơn vị liên kết
        /// </summary>
        /// <returns></returns>
        public DataTable CA_UnitProgram_SelectAll()
        {
            return _dal.CA_UnitProgram_SelectAll();
        }

        /// <summary>
        ///  Hieutm: Lấy thông tin đơn vị sử dụng theo mã chương trình
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public DataTable CA_UnitProgram_SelectByProgID(int progID)
        {
            return _dal.CA_UnitProgram_SelectByProgID(progID);
        }

        /// <summary>
        /// HieuTM: Lấy thông tin bản ghi theo ID_UnitProgram
        /// </summary>
        /// <param name="id_UnitProg"></param>
        /// <returns></returns>
        public DataTable CA_UnitProgram_SelectBy_IDUnitProg(int id_UnitProg)
        {
            return _dal.CA_UnitProgram_SelectBy_IDUnitProg(id_UnitProg);
        }

        #endregion

        #region CA_UnitProgram_Log

        public DataTable CA_UnitProgram_Log_SelectBy_IDUnitProgram(int id_UnitProgram)
        {
            return _dal.CA_UnitProgram_Log_SelectBy_IDUnitProgram(id_UnitProgram);
        }

        #endregion

        #region CA_UserProgram

        /// <summary>
        /// HieuTM: Lấy thông tin bản ghi theo UserID
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public DataTable CA_UserProg_SelectBy_UserID(int iUserID)
        {
            return _dal.CA_UserProg_SelectBy_UserID(iUserID);
        }
        /// <summary>
        /// Sangdm - Select Program by UserID
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public DataTable CA_Program_SelectBy_UserID(int iUserID)
        {
            return _dal.CA_Program_SelectBy_UserID(iUserID);
        }

        /// <summary>
        /// HieuTM: Lấy thông tin bản ghi dựa trên ProgID, UserID, UserProgName.
        /// </summary>
        /// <param name="progID"></param>
        /// <param name="userID"></param>
        /// <param name="userprogname"></param>
        /// <returns></returns>
        public DataTable CA_UserProgram_SelectBy_UserProgName_ProgID_UserID(int progID, int userID, string userprogname)
        {
            return _dal.CA_UserProgram_SelectBy_UserProgName_ProgID_UserID(progID, userID, userprogname);
        }

        public DataSet CA_UserProgram_SelectBy_ProgID_SignTypeID_Seach(int progID, int signTypeID, string seach)
        {
            DataSet ds = _dal.CA_UserProgram_SelectBy_ProgID_SignTypeID_Seach(progID, signTypeID, seach);
            return ds;
        }
        /// <summary>
        /// Sangdm - 2015-08-23: Lien ket uy quyen
        /// </summary>
        /// <param name="User_UyQuyen"></param>
        /// <returns></returns>
        public DataTable CA_UyQuyen_SelectBy_NguoiUyQuyen(int User_UyQuyen)
        {
            return _dal.CA_UyQuyen_SelectBy_NguoiUyQuyen(User_UyQuyen);
        }
        /// <summary>
        /// sangdm
        /// </summary>
        /// <param name="User_UyQuyen"></param>
        /// <returns></returns>
        public DataTable CA_UyQuyen_QuyenUnit_SelectBy_ID_UyQuyen(int ID_UyQuye)
        {
            return _dal.CA_UyQuyen_QuyenUnit_SelectBy_ID_UyQuyen(ID_UyQuye);
        }
        /// <summary>
        /// Sangdm - lấy danh sách chương trình của ng ủy quyền và tên đăng nhập của người nhận ủy quyền, ko có tên đăng nhập
        /// thì lấy null
        /// </summary>
        /// <param name="User_UyQuyen"></param>
        /// <returns></returns>
        public DataTable CA_UserProgram_SelectBy_IDUyQuyen_IDDuocUyQuyen(int ID_UQ, int ID_UyQuyen, int ID_DuocUyQuyen)
        {
            return _dal.CA_UserProgram_SelectBy_IDUyQuyen_IDDuocUyQuyen(ID_UQ, ID_UyQuyen, ID_DuocUyQuyen);
        }

        /// <summary>
        /// Ninhtq: Lấy danh sách đã liên kết người dùng - hệ thống
        /// </summary>
        /// <returns></returns>
        public DataTable CA_UserProgram_SelectAll()
        {
            return _dal.CA_UserProgram_SelectAll();
        }

        /// <summary>
        /// Ninhtq: Lấy liên kết người dùng - hệ thống theo ID
        /// </summary>
        /// <param name="iUserProgID"></param>
        /// <returns></returns>
        public DataTable CA_UserProgram_SelectByUserProgID(int iUserProgID)
        {
            return _dal.CA_UserProgram_SelectByUserProgID(iUserProgID);
        }

        /// <summary>
        /// Hieutm: Lấy thông tin người dùng liên kết hệ thống theo mã đơn vị
        /// </summary>
        /// <param name="progID"></param>
        /// <returns></returns>
        public DataTable CA_UserProgram_SelectByUnitID(int unitID)
        {
            return _dal.CA_UserProgram_SelectByUnitID(unitID);
        }

        /// <summary>
        ///  Hieutm: Lấy thông tin người dùng liên kết hệ thống theo mã chương trình
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public DataTable CA_UserProgram_SelectByProgID(int progID)
        {
            return _dal.CA_UserProgram_SelectByProgID(progID);
        }

        /// <summary>
        /// Hieutm:Lấy liên kết người dùng - hệ thống theo UserID và ProgID
        /// </summary>
        /// <param name="iUserProgID"></param>
        /// <param name="iProgID"></param>
        /// <returns></returns>
        public DataTable CA_UserProgram_SelectBy_UserProgName_ProgID(string iUserProgName, int iProgID)
        {
            return _dal.CA_UserProgram_SelectBy_UserProgName_ProgID(iUserProgName, iProgID);
        }

        /// <summary>
        /// Hieutm: Lấy thông tin người dùng liên kết hệ thống theo mã đơn vị,tên đăng nhập hệ thống
        /// </summary>
        /// </summary>
        /// <param name="unitID"></param>
        /// <param name="userProg"></param>
        /// <returns></returns>
        public DataTable CA_UserProgram_SelectByUnitID_UserPro(int unitID, string userProg)
        {
            return _dal.CA_UserProgram_SelectByUnitID_UserPro(unitID, userProg);
        }

        /// <summary>
        /// Hieutm: Lấy thông tin người dùng liên kết hệ thống theo tên đăng nhập hệ thống
        /// </summary>
        /// </summary>
        /// <param name="unitID"></param>
        /// <param name="userProg"></param>
        /// <returns></returns>
        public DataTable CA_UserProgram_SelectByUserPro(string userProg)
        {
            return _dal.CA_UserProgram_SelectByUserPro(userProg);
        }

        /// <summary>
        /// Ninhtq: Lấy liên kết người dùng - hệ thống theo UserID và ProgID
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iProgID"></param>
        /// <returns></returns>
        public DataTable CA_UserProgram_SelectByUserID_ProgID(int iUserID, int iProgID)
        {
            return _dal.CA_UserProgram_SelectByUserID_ProgID(iUserID, iProgID);
        }

        /// <summary>
        /// HieuTM: Cập nhật liên kết người dùng - hệ thống. Trả về ID của dòng được cập nhật.
        /// Bằng -1 cập nhật thất bại
        /// </summary>
        /// <param name="iUserProgID"></param>
        /// <param name="iUserID"></param>
        /// <param name="iProgID"></param>
        /// <param name="sUserProgName"></param>
        /// <param name="validFrom"></param>
        /// <param name="validTo"></param>
        /// <param name="signatureTypeID"></param>
        /// <param name="sUserModified"></param>
        public int CA_UserProgram_InsertUpdate(int iUserProgID, int iUserID, int iProgID, string sUserProgName, DateTime validFrom,
            DateTime validTo, string sUserModified)
        {
            return _dal.CA_UserProgram_InsertUpdate(iUserProgID, iUserID, iProgID, sUserProgName, validFrom,
                validTo, sUserModified);
        }

        #endregion

        #region CA_UserProgram_Log

        public DataTable CA_UserProgram_Log_SelectBy_IDUserProgram(int id_UserProg)
        {
            return _dal.CA_UserProgram_Log_SelectBy_IDUserProgram(id_UserProg);
        }

        #endregion

        #region CA_CertificateUser

        /// <summary>
        /// Ninhtq: Lấy liên kết chứng thư số và người dùng theo ID
        /// </summary>
        /// <param name="id_CertUser"></param>
        /// <returns></returns>
        public DataTable CA_CertificateUser_SelectByID_CertUser(int id_CertUser)
        {
            return _dal.CA_CertificateUser_SelectByID_CertUser(id_CertUser);
        }

        /// <summary>
        /// Ninhtq: Lấy liên kết chứng thư số và người dùng theo certID và userID
        /// </summary>
        /// <param name="certID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable CA_CertificateUser_SelectBy_CertID_UserID(int certID, int userID)
        {
            return _dal.CA_CertificateUser_SelectBy_CertID_UserID(certID, userID);
        }

        /// <summary>
        /// Quanns: lấy danh sách ánh xạ phân quyền người dùng - chứng thư tương ứng.
        /// </summary>
        /// <returns></returns>
        public DataTable CA_CertificateUser_SelectAll()
        {
            return _dal.CA_CertificateUser_SelectAll();
        }

        /// <summary>
        /// Quanns: cập nhật quyền của người dùng
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="certID"></param>
        /// <param name="validFrom"></param>
        /// <param name="validTo"></param>
        /// <param name="type"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public Boolean CA_CertificateUser_InsertNewMappping(int userID, int certID, DateTime validFrom, DateTime validTo, int type, String username)
        {
            return _dal.CA_CertificateUser_InsertNewMappping(userID, certID, validFrom, validTo, type, username);
        }

        /// <summary>
        /// Quanns: cập nhật thông tin phân quyền chứng từ - người dùng
        /// </summary>
        /// <param name="id_CertUser"></param>
        /// <param name="validFrom"></param>
        /// <param name="validTo"></param>
        /// <param name="type"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public Boolean CA_CertificateUser_UpdateMappping(int id_CertUser, DateTime validFrom, DateTime validTo, int type, String username)
        {
            return _dal.CA_CertificateUser_UpdateMappping(id_CertUser, validFrom, validTo, type, username);
        }

        /// <summary>
        /// Ninhtq: Cập nhật liên kết chứng thư số và người dùng
        /// </summary>
        /// <param name="iUserCertID"></param>
        /// <param name="iUserID"></param>
        /// <param name="iCertID"></param>
        /// <param name="validFrom"></param>
        /// <param name="validTo"></param>
        /// <param name="iType"></param>
        /// <param name="sUserModified"></param>
        public void CA_CertificateUser_InsertUpdate(int iUserCertID, int iUserID, int iCertID, DateTime validFrom,
            DateTime validTo, int iType, string sUserModified)
        {
            _dal.CA_CertificateUser_InsertUpdate(iUserCertID, iUserID, iCertID, validFrom,
                validTo, iType, sUserModified);
        }

        #endregion

        #region CA_TypeUnit

        /// <summary>
        /// Ninhtq: Lấy tất cả danh sách loại đơn vị
        /// </summary>
        /// <returns></returns>
        public DataTable CA_UnitType_SelectAll()
        {
            return _dal.CA_UnitType_SelectAll();
        }

        #endregion

        #region CA_CertificationAuthority


        public bool CA_CertificationAuthority_DeleteBy_CertAuthID(int certAuthID)
        {
            return _dal.CA_CertificationAuthority_DeleteBy_CertAuthID(certAuthID);
        }

        /// <summary>
        /// Ninhtq: Lấy nhà cung cấp CA trong db theo serial, nếu có thì trả về CertAuthID, nếu chưa có trả về 0.
        /// </summary>
        /// <param name="sSerial"></param>
        /// <returns></returns>
        public int CA_CertificationAuthority_HasCA(string sSerial)
        {
            DataTable dt = _dal.CA_CertificationAuthority_SelectBySerial(sSerial);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["CertAuthID"]);
            else
                return 0;
        }

        /// <summary>
        /// Ninhtq: Lấy rawdata của CA nhà cung cấp theo CertAuthID
        /// </summary>
        /// <param name="id">CertAuthID</param>
        /// <returns></returns>
        public DataTable CA_CertificationAuthority_SelectRawDataByID(int id)
        {
            return _dal.CA_CertificationAuthority_SelectRawDataByID(id);
        }

        /// <summary>
        /// Toantk: Lấy danh sách nhà cung cấp chứng thư số
        /// </summary>
        /// <returns></returns>
        public DataTable CA_CertificationAuthority_SelectAll()
        {
            return _dal.CA_CertificationAuthority_SelectAll();
        }

        /// <summary>
        /// Toantk: Lấy thông tin nhà cung cấp chứng thư số theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable CA_CertificationAuthority_SelectByID(int id)
        {
            return _dal.CA_CertificationAuthority_SelectByID(id);
        }

        /// <summary>
        /// Toantk: Thêm nhà cung cấp chứng thư số
        /// </summary>
        /// <param name="certID"></param>
        /// <param name="cert"></param>
        /// <param name="fileCert"></param>
        /// <param name="url">Link download file CRL</param>
        /// <param name="username"></param>
        public void CA_CertificationAuthority_Insert(int certID, X509Certificate2 cert, string url, string username)
        {
            _dal.CA_CertificationAuthority_Insert(certID, cert, url, username);
        }

        /// <summary>
        /// Toantk: Cập nhật thông tin cung cấp chứng thư số
        /// </summary>
        /// <param name="certID"></param>
        /// <param name="url">Link download file CRL</param>
        /// <param name="revokedDate">Ngày bị thu hồi. Truyền DateTime.MinValue nếu không thiết lập.</param>
        /// <param name="username"></param>
        public void CA_CertificationAuthority_Update(int certID, string url, DateTime revokedDate, string username)
        {
            _dal.CA_CertificationAuthority_Update(certID, url, revokedDate, username);
        }

        /// <summary>
        /// Quanns: Lấy danh sách tất cả các nhà phát hành CA: id - Tên
        /// </summary>
        /// <returns></returns>
        public DataTable CA_CertificationAuthority_SelectAllIssuers()
        {
            return _dal.CA_CertificationAuthority_SelectAllIssuers();
        }

        #endregion

        #region CA_SignatureType

        /// <summary>
        /// HieuTM: Lấy danh sách các loại ký
        /// </summary>
        /// <returns></returns>
        public DataTable CA_SignatureType_SelectAll()
        {
            return _dal.CA_SignatureType_SelectAll();
        }

        #endregion

        #region CA_UserProgram_Unit

        public DataTable CA_UserProgram_Unit_SelectBy_IDUserProg(int ID_UserProg)
        {
            return _dal.CA_UserProgram_Unit_SelectBy_IDUserProg(ID_UserProg);
        }

        #endregion

        #region CA_UyQuyen_QuyenUnit
        public bool CA_UyQuyen_QuyenUnit_ResetBy_UserUyQuyenID_ProgID(int userUyQuyenID, int progID)
        {
            return _dal.CA_UyQuyen_QuyenUnit_ResetBy_UserUyQuyenID_ProgID(userUyQuyenID, progID);
        }
        #endregion

        #region CA_UyQuyen_QuyenUnit
        public bool CA_UyQuyen_QuyenUnit_IsertUpdate(int iUserProgID, int[] iUnitID, bool[] enable, int[] signatureTypeID, string sUserModified)
        {
            return _dal.CA_UyQuyen_QuyenUnit_IsertUpdate(iUserProgID, iUnitID, enable, signatureTypeID, sUserModified);
        }
        /// <summary>
        /// Sangdm - 24082015- insertUpdate CA_UyQuyen
        /// </summary>
        /// <param name="bInsertUpdate"></param>
        /// <param name="User_UyQuyen"></param>
        /// <param name="User_NhanUyQuyen"></param>
        /// <param name="ProgID"></param>
        /// <param name="sUserProgName"></param>
        /// <param name="validFrom"></param>
        /// <param name="validTo"></param>
        /// <param name="sUserModified"></param>
        /// <param name="DateModified"></param>
        /// <returns></returns>
        public bool CA_UyQuyen_InsertUpdate(bool bInsertUpdate, int ID_UyQuyen, DataTable dtSave, int User_UyQuyen, int User_DuocUyQuyen, string sUserModified, ref List<int> lstErr)
        {
            return _dal.CA_UyQuyen_InsertUpdate(bInsertUpdate, ID_UyQuyen, dtSave, User_UyQuyen, User_DuocUyQuyen, sUserModified, ref lstErr);
        }
        #endregion

        /// <summary>
        /// Toantk: Lấy danh sách liên kết người dùng với đơn vị
        /// </summary>
        /// <returns></returns>
        public DataTable CA_UserUnit_SelectAll()
        {
            return _dal.CA_UserUnit_SelectAll();
        }

        /// <summary>
        /// Toantk: Lấy thông tin liên kết theo ID liên kết
        /// </summary>
        /// <param name="id_UserUnit"></param>
        /// <returns></returns>
        public DataTable CA_UserUnit_SelectByID(int id_UserUnit)
        {
            return _dal.CA_UserUnit_SelectByID(id_UserUnit);
        }

        /// <summary>
        /// Toantk: Lấy bản ghi liên kết User-Unit để kiểm tra đã có hay chưa
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public DataTable CA_UserUnit_SelectByUserID_UnitID(int userID, int unitID)
        {
            return _dal.CA_UserUnit_SelectByUserID_UnitID(userID, unitID);
        }

        /// <summary>
        /// Toantk: Cập nhật liên kết người dùng - đơn vị
        /// </summary>
        /// <param name="id_UserUnit"></param>
        /// <param name="userID"></param>
        /// <param name="unitID"></param>
        /// <param name="department"></param>
        /// <param name="validFrom"></param>
        /// <param name="validTo"></param>
        /// <param name="userModified"></param>
        public void CA_UserUnit_InsertUpdate(int id_UserUnit, int userID, int unitID, string department, DateTime validFrom,
            DateTime validTo, string userModified)
        {
            _dal.CA_UserUnit_InsertUpdate(id_UserUnit, userID, unitID, department, validFrom, validTo, userModified);
        }

        /// <summary>
        /// HieuTM: Lấy thông lien kết các log theo thời gian
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataTable CA_LichSuLienKet(DateTime date)
        {
            return _dal.CA_LichSuLienKet(date);
        }

        /// <summary>
        /// HieuTM: Thêm mới hoặc cập nhật dữ liệu vào 2 bảng UserProg và CA_UserProg_QuyenUnit
        /// </summary>
        /// <param name="iUserProgID"></param>
        /// <param name="iProg"></param>
        /// <param name="iUser"></param>
        /// <param name="userProgName"></param>
        /// <param name="iUnitID"></param>
        /// <param name="validFrom"></param>
        /// <param name="validTo"></param>
        /// <param name="enable"></param>
        /// <param name="signatureTypeID"></param>
        /// <param name="sUserModified"></param>
        public bool CA_UserProg_UnitPhanQuyen_InsertUpdate(int iUserProgID, int iProg, int iUser, string userProgName, int iUnitID, DateTime validFrom, DateTime validTo, bool enable, int signatureTypeID, string sUserModified)
        {
            return _dal.CA_UserProg_UnitPhanQuyen_InsertUpdate(iUserProgID, iProg, iUser, userProgName, iUnitID, validFrom, validTo, enable, signatureTypeID, sUserModified);
        }

        public bool CA_UserProg_UnitPhanQuyen_InsertUpdate_Array(int iUserProgID, int iUserID, int iProgID, string sUserProgName, DateTime validFrom,
                                               DateTime validTo, int[] iUnitID, bool[] enable, int[] signatureTypeID, string sUserModified)
        {
            return _dal.CA_UserProg_UnitPhanQuyen_InsertUpdate_Array(iUserProgID, iUserID, iProgID, sUserProgName, validFrom, validTo,
                                                                    iUnitID, enable, signatureTypeID, sUserModified);
        }

        public bool CA_UserProg_QuyenUnit_ResetBy_ID_UserProg(int idUserProg)
        {
            return _dal.CA_UserProg_QuyenUnit_ResetBy_ID_UserProg(idUserProg);
        }
    }
}