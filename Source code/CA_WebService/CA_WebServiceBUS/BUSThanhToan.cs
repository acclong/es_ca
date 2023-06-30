using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ES.CA_WebServiceDAL;

namespace ES.CA_WebServiceBUS
{
    public partial class BUSThanhToan
    {
        #region Constructor
        private DALThanhToan _dal;
        /// <summary>
        /// Khởi tạo dùng kết nối mặc định
        /// </summary>
        public BUSThanhToan()
        {
            _dal = new DALThanhToan();
        }

        /// <summary>
        /// Khởi tạo dùng kết nối tạm (đến database khác)
        /// </summary>
        /// <param name="strConn"></param>
        public BUSThanhToan(string strConn)
        {
            _dal = new DALThanhToan(strConn);
        }

        /// <summary>
        /// Khởi tạo dùng kết nối tạm (đến database khác)
        /// </summary>
        /// <param name="sComputerName"></param>
        /// <param name="sDBName"></param>
        /// <param name="sUserName"></param>
        /// <param name="sPassword"></param>
        public BUSThanhToan(string sComputerName, string sDBName, string sUserName, string sPassword)
        {
            _dal = new DALThanhToan(sComputerName, sDBName, sUserName, sPassword);
        }
        #endregion

        #region Methods
        public DataTable TT_XacThucThongKe_TuNgayDenNgay_EPTC(string Ma_NM, DateTime TuNgay, DateTime DenNgay)
        {
            return _dal.TT_XacThucThongKe_TuNgayDenNgay_EPTC(Ma_NM, TuNgay, DenNgay);
        }

        public DataTable TT_XacThucThongKe_Ngay_EPTC(string Ma_NM, DateTime Ngay)
        {
            return _dal.TT_XacThucThongKe_Ngay_EPTC(Ma_NM, Ngay);
        }

        public bool InsertXacThucBangKe_EPTC(string ma_NM, DateTime ngay, int lanXacThuc, bool XacThuc, string LyDo, string NguoiXacThuc, DateTime NgayXacThuc)
        {
            return _dal.InsertXacThucBangKe_EPTC(ma_NM, ngay, lanXacThuc, XacThuc, LyDo, NguoiXacThuc, NgayXacThuc);
        }
        #endregion
    }
}
