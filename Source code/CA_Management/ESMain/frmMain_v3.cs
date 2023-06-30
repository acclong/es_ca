using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using ES.CA_ManagementUI;
using ESLogin;

namespace ESMain
{
    public partial class frmMain_v3 : Form
    {
        private enum MainFunctionEnum
        {
            QuanTriChuKySo = 1,
            CauHinh = 2,
            QuanTriUser = 3
        }

        public frmMain_v3()
        {
            InitializeComponent();
            ThemeResolutionService.ApplicationThemeName = "TelerikMetroBlue";
        }

        private void frmMain_v3_Load(object sender, EventArgs e)
        {
            LoadTreeViewMenu(MainFunctionEnum.QuanTriChuKySo);
            LoadTreeViewMenu(MainFunctionEnum.CauHinh);
            LoadTreeViewMenu(MainFunctionEnum.QuanTriUser);
        }

        private void radTreeViewMenu_NodeMouseClick(object sender, RadTreeViewEventArgs e)
        {
            RadTreeViewElement element = (RadTreeViewElement)sender;
            LoadUserControl(element.SelectedNode.Value.ToString(), element.SelectedNode.Text);
        }

        private void LoadTreeViewMenu(MainFunctionEnum mainFunction)
        {
            if (mainFunction == MainFunctionEnum.QuanTriChuKySo)
            {
                //Lọc theo MainFunction
                string str = "MAIN_FUNCTION =" + ((int)mainFunction).ToString() + "";
                ModMain.dtRole.DefaultView.RowFilter = str;
                DataTable dtFunc = ModMain.dtRole.DefaultView.ToTable();
                //Đổ vào RadTreeView
                rtvQuanTriChuKySo.ValueMember = "FUNCTIONID";
                rtvQuanTriChuKySo.DisplayMember = "FUNCTIONNAME";
                rtvQuanTriChuKySo.ParentMember = "PARENT_ID";
                rtvQuanTriChuKySo.ChildMember = "CHILD_ID";
                rtvQuanTriChuKySo.DataSource = dtFunc;
                //Bắt sự kiện click chọn items
                rtvQuanTriChuKySo.NodeMouseClick += this.radTreeViewMenu_NodeMouseClick;
                rtvQuanTriChuKySo.ExpandAll();
            }
            else if (mainFunction == MainFunctionEnum.CauHinh)
            {
                //Lọc theo MainFunction
                string str = "MAIN_FUNCTION =" + ((int)mainFunction).ToString() + "";
                ModMain.dtRole.DefaultView.RowFilter = str;
                DataTable dtFunc = ModMain.dtRole.DefaultView.ToTable();
                //Đổ vào RadTreeView
                rtvCauHinh.ValueMember = "FUNCTIONID";
                rtvCauHinh.DisplayMember = "FUNCTIONNAME";
                rtvCauHinh.ParentMember = "PARENT_ID";
                rtvCauHinh.ChildMember = "CHILD_ID";
                rtvCauHinh.DataSource = dtFunc;
                //Bắt sự kiện click chọn items
                rtvCauHinh.NodeMouseClick += this.radTreeViewMenu_NodeMouseClick;
                rtvCauHinh.ExpandAll();
            }
            else if (mainFunction == MainFunctionEnum.QuanTriUser)
            {
                //Lọc theo MainFunction
                string str = "MAIN_FUNCTION =" + ((int)mainFunction).ToString() + "";
                ModMain.dtRole.DefaultView.RowFilter = str;
                DataTable dtFunc = ModMain.dtRole.DefaultView.ToTable();
                //Đổ vào RadTreeView
                rtvQuanTriUser.ValueMember = "FUNCTIONID";
                rtvQuanTriUser.DisplayMember = "FUNCTIONNAME";
                rtvQuanTriUser.ParentMember = "PARENT_ID";
                rtvQuanTriUser.ChildMember = "CHILD_ID";
                rtvQuanTriUser.DataSource = dtFunc;
                //Bắt sự kiện click chọn items
                rtvQuanTriUser.NodeMouseClick += this.radTreeViewMenu_NodeMouseClick;
                rtvQuanTriUser.ExpandAll();
            }
        }

        //Thêm control ở đây
        private void LoadUserControl(string name, string text)
        {
            UserControl uControl = null;
            Form frm = null;
            switch (name)
            {
                #region Danh mục
                case "DanhMucHeThong":
                    uControl = new ucDanhMucHeThongTichHop();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "DanhMucDonVi":
                    uControl = new ucDanhMucDonVi();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "DanhMucUser":
                    uControl = new ucDanhMucNguoiDung();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "DanhMucCert":
                    uControl = new ucDanhMucChungThuSo();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "DanhMucNhaCungCapCA":
                    uControl = new ucDanhMucNhaCungCap();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                #endregion
                #region Ánh xạ liên kết
                case "LienKetUserHeThong":
                    uControl = new ucLienKetUserHeThong();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "LienKetUserDonVi":
                    uControl = new ucLienKetUserDonVi();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "LichSuLienKet":
                    uControl = new ucLichSuLienKet();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "LienKetDonViHeThong":
                    uControl = new ucLienKetDonViHeThong();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "LienKetUyQuyen":
                    uControl = new ucLienKetUyQuyen();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                #endregion
                #region Quản lý file
                case "DanhSachVanBan":
                    uControl = new ucDanhsachVanBan();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "DanhSachHoSo":
                    uControl = new ucDanhSachHoSo();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "DanhMucLoaiVanBan":
                    uControl = new ucDanhMucLoaiVanBan();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "DanhMucLoaiHoSo":
                    //uControl = new ucDanhMucLoaiHoSo();
                    uControl = new ucDanhMucLoaiHoSoNew();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "DanhMucLoaiVBLienQuan":
                    uControl = new ucDanhMucLoaiVBLienQuan();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "LienKetHoSoVanBan":
                    uControl = new ucLienKetHoSoVanBan();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "DanhMucQuyenXacNhan":
                    uControl = new ucDanhMucQuyenXacNhan();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "XemVanBanClient":
                    uControl = new ucXemVBClient();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "XemVanBanServer":
                    uControl = new ucXemVBServer();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                #endregion
                #region Quản lý HSM
                case "HSMDoiPassAdmin":
                    uControl = new ucHSMQuanLyDevice();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "HSMQuanLySlots":
                    uControl = new ucHSMQuanLySlots();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "HSMQuanLyObjects":
                    uControl = new ucHSMQuanLyObjects();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "HSMQuanLyWLDSlots":
                    uControl = new ucHSMQuanlyWLDSlot();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                #endregion
                #region Cấu hình chương trình
                case "CauHinhCRL":
                    frm = new frmCauHinhCRL();
                    frm.ShowDialog();
                    break;

                case "CauHinhEmail":
                    frm = new frmCauHinhEmail();
                    frm.ShowDialog();
                    break;
                case "CauHinhTimeKy":
                    frm = new frmCauHinhTimeKy();
                    frm.ShowDialog();
                    break;
                case "CauHinhCheDoHSM":
                    frm = new frmCauHinhCheDoHSM();
                    frm.ShowDialog();
                    break;
                case "CauHinhMaPINMacDinh":
                    frm = new frmCauHinhPinDefault();
                    frm.ShowDialog();
                    break;
                case "CauHinhFileFolder":
                    frm = new frmCauHinhFileFolder();
                    frm.ShowDialog();
                    break;

                #endregion

                #region Quản lý người dùng
                case "Manage_User":
                    uControl = new ctrUser();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "Manage_UserGroup":
                    uControl = new ctrUserGroup();
                    AddRadPageViewPage(name, text, uControl);
                    break;
                case "Manage_ChangePass":
                    frmChangePass frmChangePass = new frmChangePass();
                    frmChangePass.ShowDialog();
                    break;
                #endregion

                default:
                    break;
            }
        }

        /// <summary>
        /// Thêm RadPageViewPage và load UserControl
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="pageText"></param>
        /// <param name="uc"></param>
        private void AddRadPageViewPage(string pageName, string pageText, UserControl uc)
        {
            if (CheckExistsInDocumentTab(pageName) == false)
            {
                RadPageViewPage page = new RadPageViewPage();
                page.Name = pageName;
                page.Text = pageText;
                page.BackColor = SystemColors.Control;
                page.Controls.Add(uc);
                uc.Dock = DockStyle.Fill;
                this.rpvPages.Pages.Add(page);
            }
            this.rpvPages.SelectedPage = this.rpvPages.Pages[pageName];
        }

        /// <summary>
        /// Check TabPage with name is exists or not exist. Return true if existed
        /// </summary>
        /// <param name="tabName">tab need check</param>
        /// <returns></returns>
        private bool CheckExistsInDocumentTab(string tabName)
        {
            for (int i = 0; i < this.rpvPages.Pages.Count; i++)
            {
                if (this.rpvPages.Pages[i].Name == tabName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
