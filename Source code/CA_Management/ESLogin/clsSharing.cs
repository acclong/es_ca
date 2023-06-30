using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
//using C1.Win.C1FlexGrid;
using System.Windows.Forms;
using System.Data.SqlClient;
using ESLogin;
using System.Data.SqlTypes;

namespace ESLogin
{
    /// <summary>
    /// Đây là class chứa các dữ liệu dùng chung của phần chào giá
    /// </summary>
    public class clsSharing
    {
        //Khai báo 1 số thứ test 
        public static string gAppPath = System.Windows.Forms.Application.StartupPath;
        public static string gcAppName = System.Windows.Forms.Application.ExecutablePath;

        //danh sách đơn vị phát điện mà user được quyền truy cập trong module chào giá
        public static string sMaDVPD;
        public static Int16 iLoaiHinhSx = 1;
        //public static DataTable dtDVPD;
        public static DataTable dtRole;

        //người dùng đăng nhập vào chương trình
        public static string userName;

        //quyền admin
        public static bool Admin;

        public enum eUserType : short { Normal = 1, TeamLear = 2 }

        //ID chương trình
        public static string sProgramID;

        //Lấy username
        public static string getUsername()
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            bool bxml = false;
            try
            {
                doc.Load(gAppPath + "\\config.xml");
                bxml = true;
            }
            catch
            { }

            if (bxml == true)
            {
                try
                {
                    userName = doc.GetElementsByTagName("UserName").Item(0).InnerText;
                }
                catch
                {}
            }
            return userName;
        }

        public static SqlConnection SqlConnection;

        public static ClsLogin gDB;

        /// <summary>
        /// hàm kiểm tra 1 object là null hay không
        /// </summary>
        /// <param name="obj">object cần kiểm tra</param>
        /// <returns></returns>
        public static bool isEmpty(object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString().Trim() == "") return true;
            else return false;
        }

        ///// <summary>
        ///// Lấy loại hình sản xuất của nhà máy
        ///// </summary>
        ///// <param name="MaDV">mã đơn vị cần lấy loại hình</param>
        ///// <returns></returns>
        //public static Int16 loaihinhSx(string MaDV)
        //{
        //    for (int i = 0; i < dtDVPD.Rows.Count; i++)
        //    {
        //        if (dtDVPD.Rows[i]["Ma_DVPD"].ToString().Trim() == MaDV)
        //        {
        //            return Convert.ToInt16(dtDVPD.Rows[i]["loaihinhSx"]);
        //        }
        //    }
        //    return 0;
        //}

        public static bool Permission(string MaChucNang)
        {
            string str;
            str = "FUNCTIONID ='" + MaChucNang + "'";
            DataView dv = new DataView();
            dv = dtRole.DefaultView;
            dv.RowFilter = str;
            if (dv.Count <= 0)
            {
                return false;
            }
            else
            {
                if (Convert.ToBoolean(dv[0]["IS_LAST"]) == true)
                {
                    return true;
                }
                else
                {
                    if (Convert.ToBoolean(dv[0]["IS_LAST"]) == false)
                    {
                        str = "FUNCTION_PARENT_ID ='" + MaChucNang + "'";
                        dv = dtRole.DefaultView;
                        dv.RowFilter = str;
                        if (dv.Count <= 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        ///// <summary>
        ///// Chuyển một dãy nhị phânn 01 thành số kiểu int
        ///// </summary>
        ///// <param name="arrBoolean">dãy nhị phân nhập vào</param>
        ///// <returns></returns>
        //public static Int32 BoolToInt(bool[] arrBoolean)
        //{
        //    if (arrBoolean == null)
        //        return 0;
        //    if (arrBoolean.Length == 0)
        //        return 0;
        //    Int32 intResult = 0;
        //    for (int i = arrBoolean.Length - 1; i >= 0; i += -1)
        //    {
        //        intResult = intResult << 1;
        //        if (arrBoolean[i])
        //        {
        //            intResult += 1;
        //        }
        //    }
        //    return intResult;
        //}

        ///// <summary>
        ///// Chuyển một số kiểu int về dạng nhị phân 01, kết quả trả về là một mảng kiêu bool
        ///// </summary>
        ///// <param name="intInput"></param>
        ///// <returns></returns>
        //public static bool[] IntToBool(Int32 intInput)
        //{
        //    bool[] arrBoolean = new bool[32];
        //    for (int i = 0; i <= 31; i++)
        //    {
        //        if ((intInput % 2) == 0)
        //        {
        //            arrBoolean[i] = false;
        //        }
        //        else
        //        {
        //            arrBoolean[i] = true;
        //        }
        //        intInput = intInput >> 1;
        //    }
        //    return arrBoolean;
        //}

        ///// <summary>
        ///// kiểm tra xem tab đã tồn tại trong tabStrip hay chưa, nếu có rồi thì focus vào đó
        ///// </summary>
        ///// <param name="tabStrip">tabStrip cần kiểm tra</param>
        ///// <param name="tabName">tên của tab cần kiểm tra</param>
        ///// <returns></returns>
        //public static bool checkExistTab(FarsiLibrary.Win.FATabStrip tabStrip, string tabName)
        //{
        //    for (int i = 0; i < tabStrip.Items.Count; i++)
        //    {
        //        if (tabStrip.Items[i].Name == tabName)
        //        {
        //            tabStrip.SelectedItem = tabStrip.Items[i];
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        ///// <summary>
        ///// kiểm tra xem tab đã tồn tại trong tabStrip hay chưa, nếu có rồi thì đóng lại
        ///// </summary>
        ///// <param name="tabStrip">tabStrip cần kiểm tra</param>
        ///// <param name="tabName">tên của tab cần kiểm tra</param>
        ///// <returns></returns>
        //public static void closeExistTab(FarsiLibrary.Win.FATabStrip tabStrip, string tabName)
        //{
        //    for (int i = 0; i < tabStrip.Items.Count; i++)
        //    {
        //        if (tabStrip.Items[i].Name == tabName)
        //        {
        //            tabStrip.RemoveTab(tabStrip.Items[i]);
        //            //tabStrip.Items.RemoveAt(i);
        //            return;
        //        }
        //    }
        //}

        ///// <summary>
        ///// thực hiện các thao tác
        ///// ctrl + x: cut
        ///// ctrl + c: copy
        ///// ctrl + v: paste
        ///// ctrl + delete: delete
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public static void C1FlexGrid_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Control)
        //    {
        //        C1FlexGrid _grid = sender as C1FlexGrid;
        //        switch (e.KeyCode)
        //        {
        //            //copy
        //            case Keys.C:
        //                System.Windows.Forms.Clipboard.SetDataObject(_grid.Clip, true);
        //                break;
        //            //cut
        //            case Keys.X:
        //                System.Windows.Forms.Clipboard.SetDataObject(_grid.Clip);
        //                CellRange rg = _grid.Selection;
        //                rg.Data = null;
        //                break;
        //            //paste
        //            case Keys.V:
        //                IDataObject data1 = System.Windows.Forms.Clipboard.GetDataObject();
        //                if (data1.GetDataPresent(Type.GetType("System.String")))
        //                {
        //                    _grid.Select(_grid.Row, _grid.Col, _grid.Rows.Count - 1, _grid.Cols.Count - 1, false);
        //                    _grid.Clip = Convert.ToString(data1.GetData(Type.GetType("System.String")));
        //                    _grid.Select(_grid.Row, _grid.Col, false);
        //                }
        //                break;
        //            //delete
        //            case Keys.Delete:
        //                CellRange rg1 = _grid.Selection;
        //                rg1.Data = null;
        //                break;
        //        }
        //    }
        //}
    }
}
