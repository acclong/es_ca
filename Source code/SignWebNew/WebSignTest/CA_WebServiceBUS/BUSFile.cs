using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ClassDAL;

namespace ClassBUS
{
    public partial class BUSFile
    {
        #region Constructor
        private DALFile _dal;
        /// <summary>
        /// Khởi tạo dùng kết nối mặc định
        /// </summary>
        public BUSFile()
        {
            _dal = new DALFile();
        }

        /// <summary>
        /// Khởi tạo dùng kết nối tạm (đến database khác)
        /// </summary>
        /// <param name="strConn"></param>
        public BUSFile(string strConn)
        {
            _dal = new DALFile(strConn);
        }

        /// <summary>
        /// Khởi tạo dùng kết nối tạm (đến database khác)
        /// </summary>
        /// <param name="sComputerName"></param>
        /// <param name="sDBName"></param>
        /// <param name="sUserName"></param>
        /// <param name="sPassword"></param>
        public BUSFile(string sComputerName, string sDBName, string sUserName, string sPassword)
        {
            _dal = new DALFile(sComputerName, sDBName, sUserName, sPassword);
        }
        #endregion

        public bool UpdateInfoToSign(DataTable dtInputSign, DataTable dtInputDB,string key, string userName,
            string program, ref string strErr)
        {
            return _dal.UpdateInfoToSign(dtInputSign, dtInputDB, key, userName, program, ref strErr);
        }
    }
}
