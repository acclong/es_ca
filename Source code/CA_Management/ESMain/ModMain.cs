using System;
using System.Collections.Generic;
using System.Text;
using ESLogin;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data.SqlTypes;
using Microsoft.Win32;
using System.IO;

namespace ESMain
{
    public class ModMain
    {
        #region "Khai bao quan tri"
        public SqlConnection gConn = new SqlConnection();
        public static ClsLogin gLogin = new ClsLogin();
        public static string userName;
        public string gAppPath;
        public bool Admin = false;
        public static DataTable dtHoChua;
        public static DataTable dtRole;
        public static DataTable dtNhaMay;
        public static DataTable dtDVPD;
        public static DataTable dtNm_TH;
        public static DataTable dtDV_TH;
        public static string strTenDV;
        public string gcAppName;

        public static bool mod_TTC = true;
        public static bool mod_CG = false;
        public static bool mod_QT = false;
        #endregion

        /////////////CHÚ Ý\\\\\\\\\\\\\
        //Khai báo [sProgramID] phải trùng với [Product Name], [Registry/Subkey] 
        //trong Smart Install Maker và [Tên chương trình] trong CreateLicense       
        public static string sProgramID = "CA_Management";
        public string sProgramName = "Hệ thống quản trị chữ ký số";
        public string ID_License = "TTCG";
        public string ID_Access = "1";
        //Tài khoản mặc định truy cập các bảng phân quyền Q_
        public string MvarUsername_default;
        public string MvarPassword_default;

        public ModMain()
        {
            DangNhap();
        }

        private void DangNhap()
        {
            try
            {
                #region Check license, Login, auto update
                // lấy giá trị đường link nơi chưa các file cần thiết là config, license, version -  Ninhtq
                gAppPath = ReadRegistry("link", sProgramID);
                System.Xml.XmlDocument docCfg = new System.Xml.XmlDocument();

                try
                {
                    docCfg.Load(gAppPath + "\\config.xml");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chương trình không tìm thấy file config.xml \n" + ex.Message,
                        sProgramID, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    gLogin.ServerName = docCfg.GetElementsByTagName("ServerName").Item(0).InnerText;
                    gLogin.UserName = docCfg.GetElementsByTagName("UserName").Item(0).InnerText;
                    gLogin.DBName = docCfg.GetElementsByTagName("DatabaseName").Item(0).InnerText;
                    gLogin.AppPath = gAppPath;
                    //
                    MvarUsername_default = docCfg.GetElementsByTagName("dbUsername_default").Item(0).InnerText;
                    MvarPassword_default = docCfg.GetElementsByTagName("dbPassword_default").Item(0).InnerText;
                    //
                    try
                    {
                        docCfg.GetElementsByTagName("ComputerID").Item(0).InnerText = CheckID.GetSystemInfo();
                        docCfg.Save(gAppPath + "\\config.xml");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Chương trình không ghi được file config.xml " + ex.Message,
                            sProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chương trình không đọc được file config.xml " + ex.Message,
                        sProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //===========================================================
                gLogin.ProgramID = sProgramID;
                gLogin.Version = Application.ProductVersion;

                gLogin.ID_License = ID_License;
                gLogin.ID_Access = ID_Access;
                gLogin.MvarUsername_default = MvarUsername_default;
                gLogin.MvarPassword_default = MvarPassword_default;
                //============================================================

                bool kt;
                //// kiểm tra license
                //gLogin.CheckLicense();
                //kt = gLogin.bHasLicense;
                //if (kt == false)
                //    return;
                gLogin.bHasLicense = true;  //Thêm khi không check license

                // Đăng nhập vào chương trình
                kt = gLogin.ExecFullConnect_SQL(sProgramName);
                if (kt == false)
                    return;
                //gLogin.ConnectToSQl_SQLConnection(gLogin.ServerName, gLogin.DBName, "sa", "sa123");  //thêm khi không đăng nhập

                ////Them phan kiem tra tu dong cap nhat chuong trinh
                //try
                //{
                //    string[] CurrentVersion = gDB.Version.Split('.');
                //    int iCurrMainVer = Convert.ToInt32(CurrentVersion[0]);
                //    int iCurrSubVer = Convert.ToInt32(CurrentVersion[1]);
                //    int iCurrBuildVer = Convert.ToInt32(CurrentVersion[3]);

                //    string DBVersion = gDB.CheckVersion(gDB.ServerName, gDB.DBName, gDB.UserName, (string)gDB.Password);
                //    string[] NewVersion = DBVersion.Split('.');
                //    int iNewMainVer = Convert.ToInt32(NewVersion[0]);
                //    int iNewSubVer = Convert.ToInt32(NewVersion[1]);
                //    int iNewBuildVer = Convert.ToInt32(NewVersion[3]);

                //    if (NewVersion != null)
                //    {
                //        if ((iNewMainVer > iCurrMainVer) || (iNewMainVer == iCurrMainVer && iNewSubVer > iCurrSubVer)
                //            || (iNewMainVer == iCurrMainVer && iNewSubVer == iCurrSubVer && iNewBuildVer > iCurrBuildVer))
                //        {
                //            DialogResult result = MessageBox.Show("Phiên bản hiện tại: " + gDB.Version + "\nPhiên bản mới: " +
                //                DBVersion + "\nBạn có muốn tải và cài đặt phiên bản mới ngay bây giờ không? ",
                //                sProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //            if (result == System.Windows.Forms.DialogResult.Yes)
                //            {
                //                gDB.Update();
                //                if (gDB.CompleteUpdate)
                //                {
                //                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                //                    p.StartInfo.FileName = gDB.AppPath + "\\" + gDB.sLinkFileUpdate;
                //                    try
                //                    {
                //                        p.Start();
                //                        return;
                //                    }
                //                    catch
                //                    {
                //                        return;
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("Chương trình không đọc được dữ liệu phiên bản từ database!", sProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Chương trình không kiểm tra phiên bản!\n" + ex.Message, sProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //////////////////////////////////////////

                gConn = gLogin.GetSQLConnection();
                Admin = gLogin.Admin;
                dtRole = gLogin.quyen();

                /////////////Ghi thông tin kết nối vào file config
                try
                {
                    docCfg.GetElementsByTagName("ServerName").Item(0).InnerText = gLogin.ServerName;
                    docCfg.GetElementsByTagName("UserName").Item(0).InnerText = gLogin.UserName;
                    docCfg.GetElementsByTagName("ComputerID").Item(0).InnerText = CheckID.GetSystemInfo();
                    docCfg.GetElementsByTagName("DatabaseName").Item(0).InnerText = gLogin.DBName;
                    docCfg.Save(gAppPath + "\\config.xml");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chương trình không ghi file config.xml.\n\n" + ex.Message,
                        sProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #endregion

                #region Lấy các thông tin của đơn vị
                //SqlDataAdapter da1;
                //string Ma_DVPD = docCfg.GetElementsByTagName("ComapanyID").Item(0).InnerText;
                //string str_loadLoaiHinhSX = "exec TTD_NEW_SelectNhaMayByMa_DVPD " + Ma_DVPD;
                //string str_loadHoChua = "exec S_HoChua_SelectByMaDVPD " + Ma_DVPD;
                //string str_loadNhaMay = "exec TTD_NEW_s_NhaMay_SelectByMaNM " + Ma_DVPD;
                //string str_loadDVPD = "select * from S_DonViPhatDien where Ma_DVPD='" + Ma_DVPD + "'";
                //string str_dtNm_TH = "select Ma_NM, TenTatDV_U from S_NhaMay where Ma_DVPD = '" + Ma_DVPD + "'";
                //string str_dtDV_TH = "select Ma_DVPD, TenTatPD_U from dbo.S_DonViPhatDien where Ma_DVPD = '" + Ma_DVPD + "'";

                //if (gConn.State == ConnectionState.Closed)
                //{
                //    gConn.Open();
                //}

                //da1 = new SqlDataAdapter(str_loadLoaiHinhSX, gConn);
                //DataTable dtLoaiHinhSx = new DataTable();
                //da1.Fill(dtLoaiHinhSx);

                //da1 = new SqlDataAdapter(str_loadHoChua, gConn);
                //dtHoChua = new DataTable();
                //da1.Fill(dtHoChua);

                //da1 = new SqlDataAdapter(str_loadNhaMay, gConn);
                //dtNhaMay = new DataTable();
                //da1.Fill(dtNhaMay);

                //da1 = new SqlDataAdapter(str_loadDVPD, gConn);
                //dtDVPD = new DataTable();
                //da1.Fill(dtDVPD);
                //if (dtDVPD.Rows.Count > 0)
                //{
                //    strTenDV = dtDVPD.Rows[0]["TenTatPD_U"].ToString();
                //}

                ////Lấy bảng cho Thông tin tùy chọn
                //da1 = new SqlDataAdapter(str_dtNm_TH, gConn);
                //dtNm_TH = new DataTable();
                //da1.Fill(dtNm_TH);

                //da1 = new SqlDataAdapter(str_dtDV_TH, gConn);
                //dtDV_TH = new DataTable();
                //da1.Fill(dtDV_TH);
                #endregion

                #region Truyền thông tin cho các module
                //Module nghiệp vụ
                ES.CA_ManagementDAL.DAL_SqlConnector.ConnectionString = gLogin.ConnectString;
                ES.CA_ManagementUI.clsShare.sUserName = gLogin.UserName;
                ES.CA_ManagementUI.clsShare.sAppPath = gLogin.AppPath;
                //Module quản trị người dùng
                ESLogin.DAL_SqlConnector.ConnectionString = gLogin.ConnectString;
                ESLogin.clsSharing.gDB = gLogin;
                ESLogin.clsSharing.Admin = this.Admin;
                ESLogin.clsSharing.dtRole = dtRole;
                ESLogin.clsSharing.sProgramID = sProgramID;

                //ES.CA_ManagementUI.clsShare.CRYPTOKI = Application.StartupPath +
                //    (Environment.Is64BitProcess ? "\\cryptoki_win64\\cryptoki.dll" : "\\cryptoki_win32\\cryptoki.dll"); 
                
                //    ////
                //    ES.Share_DAL.DAL_Common._connectionString = gDB.ConnectString;
                //    ES.ThongTinChung_DAL.DAL_Common._connectionString = gDB.ConnectString;
                //    ES.SanXuatKinhDoanh_DAL.DAL_Common._connectionString = gDB.ConnectString;
                //    ES.PhanTichDanhGia_DAL.DAL_Common._connectionString = gDB.ConnectString;
                //    ES.TongHop_DAL.DAL_Common._connectionString = gDB.ConnectString;
                //    ES.BaoCaoSanXuat_DAL.DAL_Common._connectionString = gDB.ConnectString;
                //    ES.ManagementDAL.DAL_Common._connectionString = gDB.ConnectString;
                //    ////
                //if (dtLoaiHinhSx != null && dtLoaiHinhSx.Rows.Count > 0)
                //{
                //    ////
                //    ES.MonitoringMain.clsSharing.sMaDVPD = Ma_DVPD;
                //    ES.MonitoringMain.clsSharing.iLoaiHinhSx = Convert.ToInt16(dtLoaiHinhSx.Rows[0]["MaLoaiHinhSX"]);
                //    ES.MonitoringMain.clsSharing.TenTatPD_U = dtLoaiHinhSx.Rows[0]["TenTatPD_U"].ToString();
                //    ES.MonitoringMain.clsSharing.dtRole = dtRole;
                //    ES.MonitoringMain.clsSharing.Admin = this.Admin;
                //    ES.MonitoringMain.clsSharing.currDay = DateTime.Now.AddDays(1).Date;
                //    ES.MonitoringMain.clsSharing.IDChuongTrinh = sProgramID;
                //    ////
                //    ES.Share_Ctrl.clsShare.sMaDVPD = Ma_DVPD;
                //    ES.Share_Ctrl.clsShare.strTenDV = strTenDV;
                //    ES.Share_Ctrl.clsShare.iLoaiHinhSx = Convert.ToInt16(dtLoaiHinhSx.Rows[0]["MaLoaiHinhSX"]);
                //    ES.Share_Ctrl.clsShare.TenTatPD_U = dtLoaiHinhSx.Rows[0]["TenTatPD_U"].ToString();
                //    ES.Share_Ctrl.clsShare.dtRole = dtRole;
                //    ES.Share_Ctrl.clsShare.Admin = this.Admin;
                //    ES.Share_Ctrl.clsShare.currDay = DateTime.Now.AddDays(1).Date;
                //    ES.Share_Ctrl.clsShare.IDChuongTrinh = sProgramID;
                //    ES.Share_Ctrl.clsShare.sUserName = gDB.UserName;
                //    ES.Share_Ctrl.clsShare.dtHoChua = dtHoChua;
                //    ES.Share_Ctrl.clsShare.dtNhaMay = dtNhaMay;
                //    ES.Share_Ctrl.clsShare.dtDVPD = dtDVPD;
                //    ES.Share_Ctrl.clsShare.sAppPath = gAppPath;
                //    ES.Share_Ctrl.clsShare.sExePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                //    ////
                //    ES.ManagementUI.clsSharing.gDB = gDB;
                //    ES.ManagementUI.clsSharing.Admin = this.Admin;
                //    ES.ManagementUI.clsSharing.dtRole = dtRole;
                //    ES.ManagementUI.clsSharing.sMaDVPD = Ma_DVPD;
                //    ES.ManagementUI.clsSharing.iLoaiHinhSx = Convert.ToInt16(dtLoaiHinhSx.Rows[0]["MaLoaiHinhSX"]); ;
                //    ES.ManagementUI.clsSharing.sProgramID = sProgramID;
                //    ////
                //    ES.TongHop_Interface.clsSharing.dtNm = dtNm_TH;
                //    ES.TongHop_Interface.clsSharing.dtDvpd = dtDV_TH;
                //}
                //else
                //{
                //    MessageBox.Show("Mã đơn vị không tồn tại. Xem lại file config.xml ", sProgramName,
                //        MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                #endregion

                #region Gọi form Main
                frmMain_v3 frm = new frmMain_v3();
                //frm.gTenCT = sProgramName;
                //frm.sCompanyInfo = dtLoaiHinhSx.Rows[0]["Ten_DVPD"].ToString();//sCompanyInfo;
                frm.ShowDialog();
                frm.Dispose();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, sProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool Permission(string MaChucNang)
        {
            //MinhDN viết riêng cho module quản trị mã chức năng TTD.01
            if (MaChucNang == "TTD.01")
            {
                for (int i = 0; i < ModMain.dtRole.Rows.Count; i++)
                {
                    if (ModMain.dtRole.Rows[i]["FUNCTIONID"].ToString().Contains(MaChucNang) == true)
                        return true;
                }
                return false;
            }
            //kết thúc
            string str;
            str = "FUNCTIONID ='" + MaChucNang + "'";

            DataView dv = new DataView();
            dv = ModMain.dtRole.DefaultView;
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
                        dv = ModMain.dtRole.DefaultView;
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

        //code đọc registry thư mục Current_User
        //Create by Ninhtq
        //Modify: 10/3/2015 by Toantk, trả về thư mục chạy nếu không tìm được link trong registry.
        public string ReadRegistry(string KeyName, string SubKey)
        {
            string sExePath = Application.StartupPath;

            // Opening the registry key
            RegistryKey rk = Registry.CurrentUser;
            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(SubKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return sExePath;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    return (string)sk1.GetValue(KeyName.ToUpper());
                }
                catch
                {
                    return sExePath;
                }
            }
        }
    }

}

