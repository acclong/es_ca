using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ES.CA_ManagementBUS;
using C1.Win.C1FlexGrid;
using Telerik.WinControls.UI;
using ESLogin;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using esDigitalSignature;

namespace ES.CA_ManagementUI
{
    public partial class ucDanhMucNguoiDung : UserControl
    {
        #region Var
        private BUSQuanTri _bus = new BUSQuanTri();

        private ContextMenuStrip _contextMenu = new ContextMenuStrip();
        private ToolStripMenuItem _tspItem;
        #endregion

        bool _headerCol = false;

        public ucDanhMucNguoiDung()
        {
            InitializeComponent();
        }

        private void ucDanhMucNguoiDung_Load(object sender, EventArgs e)
        {
            try
            {
                AddContextMenu_C1FlexGrid(ref cfgNguoiDung);
                LoadData();
                InitRgvNguoiDung();
                InitCboTrangThai();

                // Thêm sự kiện KeyDown
                cfgNguoiDung.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region Init
        private void InitCboTrangThai()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));

            DataRow dr = dt.NewRow();
            dr["ID"] = -1;
            dr["Name"] = "Tất cả";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 1;
            dr["Name"] = "Hiệu Lực";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 0;
            dr["Name"] = "Không hiệu lực";
            dt.Rows.Add(dr);

            cboTrangThai.DataSource = dt;
            cboTrangThai.DisplayMember = "Name";
            cboTrangThai.ValueMember = "ID";
            cboTrangThai.SelectedIndex = 0;
        }

        private void InitRgvNguoiDung()
        {
            // cấu hình radGrid
            cfgNguoiDung.ExtendLastCol = true;
            cfgNguoiDung.Cols.Fixed = 1;
            cfgNguoiDung.Cols[0].Width = 25;
            cfgNguoiDung.AllowSorting = AllowSortingEnum.SingleColumn;

            //Edited by Hieutm on 15/6/2015
            //Thêm trường Mã đơn vị
            string[] arrName = { "STT", "UserID", "Name", "CMND", "Status", "UnitNotation", "StatusName", "ValidFrom", "ValidTo", "UnitName", "UnitID", "CertID", "NameCN", "Email", "Description" };
            string[] arrHeader = { "STT", "UserID", "Họ và tên", "Chứng minh thư", "Status", "Đơn vị", "Trạng thái", "Ngày có hiệu lực", "Ngày hết hiệu lực", "UnitName", "UnitID", "CertID", "Tên chứng thư - Ngày hết hạn", "Email", "Mô tả" };

            for (int i = 0; i < arrName.Length; i++)
            {
                #region C1

                // tên và header    
                cfgNguoiDung.Cols[i + 1].Name = arrName[i];
                cfgNguoiDung.Cols[i + 1].Caption = arrHeader[i];

                // căn lề
                if (i == 2 || i == 3 || i == 5 || i == 12 || i == 14 || i == 13)
                    cfgNguoiDung.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                else
                    cfgNguoiDung.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;

                // kích thước cột
                if (i == 0)
                    cfgNguoiDung.Cols[i + 1].Width = 50;
                else if (i == 2)
                    cfgNguoiDung.Cols[i + 1].Width = 200;
                else if (i == 12)
                    cfgNguoiDung.Cols[i + 1].Width = 200;
                else if (i == 3 || i > 4)
                    cfgNguoiDung.Cols[i + 1].Width = 130;

                // format hiển thị cột
                if (i == 7 || i == 8)
                {
                    cfgNguoiDung.Cols[i + 1].Format = "dd/MM/yyyy";
                    cfgNguoiDung.Cols[i + 1].Width = 100;
                }

                // ẩn các cột không cần thiết
                if (i == 1 || i == 4 || i == 9 || i == 10 || i == 11)
                    cfgNguoiDung.Cols[i + 1].Visible = false;

                //Edited by Toantk on 16/4/2015
                //Cho phép filter
                if (i == 7 || i == 8)
                    cfgNguoiDung.Cols[i + 1].AllowFiltering = AllowFiltering.ByCondition;
                else
                    cfgNguoiDung.Cols[i + 1].AllowFiltering = AllowFiltering.Default;

                #endregion
            }

            // căn giừa hàng đầu
            cfgNguoiDung.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
            //cfgNguoiDung.Rows[0].Style.Font = new Font("Times New Roman", 11, FontStyle.Bold);
        }

        private void AddContextMenu_C1FlexGrid(ref C1FlexGrid c1Grid)
        {
            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "LogUser";
            _tspItem.Text = "Lịch sử người dùng";
            _contextMenu.Items.Add(_tspItem);

            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "ViewCTS";
            _tspItem.Text = "Xem chứng thư số";
            _contextMenu.Items.Add(_tspItem);

            ToolStripSeparator stripSeparator1 = new ToolStripSeparator();
            stripSeparator1.Alignment = ToolStripItemAlignment.Right;
            _contextMenu.Items.Add(stripSeparator1);

            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "AddCertificate";
            _tspItem.Text = "Nhập chứng thư số - A0";
            _contextMenu.Items.Add(_tspItem);

            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "ResetPass";
            _tspItem.Text = "Thiết lập lại mã PIN đăng nhập - A0";
            _contextMenu.Items.Add(_tspItem);

            //Add vào grid
            c1Grid.ContextMenuStrip = _contextMenu;
            //Add sự kiện
            _contextMenu.ItemClicked += new ToolStripItemClickedEventHandler(contextMenu_ItemClicked);

        }
        #endregion

        #region Data
        private void LoadData()
        {
            // lấy dữ liệu từ database
            BUSQuanTri bus = new BUSQuanTri();
            DataTable dttmp = bus.CA_User_SelectBy_Status_Seach("", -1);
            cfgNguoiDung.DataSource = dttmp;
        }
        #endregion

        #region Controls

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemSuaNguoiDung frm = new frmThemSuaNguoiDung();
                frm.UserID = -1;
                frm.ShowDialog();

                // load lại dữ liệu
                btnRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (cfgNguoiDung.Row > 0)
                {
                    frmThemSuaNguoiDung frm = new frmThemSuaNguoiDung();

                    frm.UserID = Convert.ToInt32(cfgNguoiDung.Rows[cfgNguoiDung.Row]["UserID"]);
                    frm.ShowDialog();

                    // load lại dữ liệu
                    btnRefresh_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BUSQuanTri bus = new BUSQuanTri();
                int userId = Convert.ToInt32(cfgNguoiDung.Rows[cfgNguoiDung.Row]["UserID"]);
                if (clsShare.Message_WarningYN("Bạn có chắc muốn xóa tài khoản [" + cfgNguoiDung.Rows[cfgNguoiDung.Row]["Name"] + "] không?"))
                {
                    if (bus.CA_User_DeleteBy_UserID(userId)) clsShare.Message_Info("Xóa người dùng thành công!");
                    else clsShare.Message_Warning("Không thể xóa do người dùng đã được liên kết!");
                    btnRefresh_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            #region Lấy thông tin Filter

            //ColumnFilter colFilterForm = new ColumnFilter();
            //if (cfgNguoiDung.Cols["ValidFrom"].Filter != null)
            //{
            //    colFilterForm = (ColumnFilter)(cfgNguoiDung.Cols["ValidFrom"].Filter);
            //}

            //ColumnFilter colFilterTo = new ColumnFilter();
            //if (cfgNguoiDung.Cols["ValidTo"].Filter != null)
            //{
            //    colFilterForm = (ColumnFilter)(cfgNguoiDung.Cols["ValidTo"].Filter);
            //}

            //ColumnFilter colFilterUnitNotation = new ColumnFilter();
            //if (cfgNguoiDung.Cols["UnitNotation"].Filter != null)
            //{
            //    colFilterForm = (ColumnFilter)(cfgNguoiDung.Cols["UnitNotation"].Filter);
            //}
            #endregion

            BUSQuanTri bus = new BUSQuanTri();
            DataTable dt = bus.CA_User_SelectBy_Status_Seach(txtTimKiem.Text, Convert.ToInt32(cboTrangThai.SelectedValue));
            cfgNguoiDung.DataSource = dt;

            InitRgvNguoiDung();

            #region Gán giá trị Filter

            //if (colFilterForm.ValueFilter.ShowValues != null)
            //    cfgNguoiDung.Cols["ValidFrom"].Filter = colFilterForm;
            //if (colFilterForm.ValueFilter.ShowValues != null)
            //    cfgNguoiDung.Cols["ValidTo"].Filter = colFilterTo;
            //if (colFilterForm.ValueFilter.ShowValues != null)
            //    cfgNguoiDung.Cols["UnitNotation"].Filter = colFilterUnitNotation;

            #endregion
        }

        #endregion

        #region Event
        private void cfgNguoiDung_DoubleClick(object sender, EventArgs e)
        {
            if (cfgNguoiDung.Row == cfgNguoiDung.RowSel)
                btnEdit_Click(null, null);
        }

        private void contextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Name)
                {
                    case "LogUser":
                        #region LogUser
                        frmXemLogDanhMuc frm = new frmXemLogDanhMuc();
                        frm.Text = "Lịch sử log người dùng";
                        frm.Log = frmXemLogDanhMuc.TypeLog.User;
                        int userID = Convert.ToInt32(cfgNguoiDung.Rows[cfgNguoiDung.Row]["UserID"]);
                        frm.UserID = userID;
                        frm.ShowDialog();
                        break;
                        #endregion
                    case "ViewCTS":
                        #region ViewCTS
                        _contextMenu.Close();

                        if (cfgNguoiDung.Rows[cfgNguoiDung.Row]["CertID"].ToString() == "")
                        {
                            clsShare.Message_Info("Người dùng chưa có chứng thư số!");
                            return;
                        }

                        // lấy giá trị CertAuthID tương ứng
                        int iCertID = Convert.ToInt32(cfgNguoiDung.Rows[cfgNguoiDung.Row]["CertID"]);
                        // lấy dữ liệu từ db
                        byte[] rawData = _bus.CA_Certificate_SelectRawDataByID(iCertID);
                        // show thông tin Certificate
                        X509Certificate2 cert = new X509Certificate2(rawData);
                        X509Certificate2UI.DisplayCertificate(cert);
                        break;
                        #endregion
                    case "AddCertificate":
                        _contextMenu.Close();
                        //Check người dùng A0
                        if (cfgNguoiDung.Rows[cfgNguoiDung.Row]["UnitNotation"].ToString() != "EVNNLDC")
                        {
                            clsShare.Message_Info("Không thể thực hiện do người dùng không thuộc EVNNLDC.");
                            return;
                        }

                        frmHSMImportChungThuSo frm1 = new frmHSMImportChungThuSo();
                        frm1.UserA0 = Convert.ToInt32(cfgNguoiDung.Rows[cfgNguoiDung.Row]["UserID"]);
                        frm1.ShowDialog();

                        //Cập nhật DB
                        if (frm1.DialogResult == DialogResult.OK)
                            if (clsShare.Message_QuestionYN("Nhập chứng thư số vào HSM thành công!\n\nBạn có muốn cập nhật cơ sở dữ liệu?"))
                            {
                                ////Cập nhật Object
                                //_bus.HSM_Object_Insert(frm1.SlotID, frm1.Label_Certificate, (int)HSMObjectTypeDB.CERTIFICATE, frm1.CKA_ID, frm1.X509Cert.Subject, clsShare.sUserName);

                                ////Cập nhật Certificate và liên kết với Object
                                //int certIDNew = _bus.CA_Certificate_InsertUpdate_OutCertID(frm1.X509Cert, clsShare.sUserName, 1);
                                //_bus.HSM_Object_UpdateCertID_ByCKA_ID(frm1.CKA_ID, certIDNew);

                                if (_bus.HSM_Object_InsertUpdate_Certificate(frm1.SlotID, frm1.Label_Certificate, frm1.CKA_ID, frm1.X509Cert, frm1.UserA0, clsShare.sUserName))
                                {
                                    clsShare.Message_Info("Cập nhật cơ sở dữ liệu thành công!");
                                    btnRefresh_Click(null, null);
                                }
                                else
                                    clsShare.Message_Error("Lỗi khi cập nhật cơ sở dữ liệu!");
                            }
                        break;
                    case "ResetPass":
                        #region ResetPass
                        _contextMenu.Close();
                        DataTable dtPK = new DataTable();

                        #region Kiểm tra điều kiện
                        //Check người dùng A0
                        if (cfgNguoiDung.Rows[cfgNguoiDung.Row]["UnitNotation"].ToString() != "EVNNLDC")
                        {
                            clsShare.Message_Info("Không thể thực hiện do người dùng không thuộc EVNNLDC.");
                            return;
                        }

                        // Check chứng thư số
                        if (cfgNguoiDung.Rows[cfgNguoiDung.Row]["CertID"].ToString() == "")
                        {
                            clsShare.Message_Info("Không thể thực hiện do người dùng chưa có chứng thư số.");
                            return;
                        }

                        if (cfgNguoiDung.Rows[cfgNguoiDung.Row]["Email"].ToString() == "")
                        {
                            clsShare.Message_Info("Không thể thực hiện do người dùng chưa có địa chỉ email.");
                            return;
                        }

                        // kiểm tra người dùng có được đăng ký cho slot không
                        int certID = Convert.ToInt32(cfgNguoiDung.Rows[cfgNguoiDung.Row]["CertID"]);
                        //Lấy danh sách cert -> object -> slot
                        dtPK = _bus.HSM_Slot_SelectBy_CertID(certID);
                        if (dtPK.Rows.Count < 1)
                        {
                            clsShare.Message_Info("Không thể thực hiện do không tìm thấy Slot của người dùng!");
                            return;
                        }

                        if (!clsShare.Message_WarningYN("CHÚ Ý: Các slot trên HSM chứa cặp khóa ký của người dùng sẽ được thiết lập lại mã PIN đăng nhập ngẫu nhiên. E-mail chứa mã PIN mới sẽ được gửi đến người dùng.\n\nBạn có chắc chắn muốn thiết lập lại mã PIN đăng nhập HSM của người dùng [" + cfgNguoiDung.Rows[cfgNguoiDung.Row]["Name"] + "] không?"))
                            return;

                        // Tạo chuỗi ngẫu nhiên
                        string newPIN = clsShare.CreateRamdomString(10);
                        //Lấy mã PIN cũ
                        string oldPIN = ESLogin.StringCryptor.DecryptString(dtPK.Rows[dtPK.Rows.Count - 1]["User_PIN"].ToString());
                        #endregion

                        #region Đổi PIN trên HSM và DB
                        try
                        {                            
                            //Kết nối HSM
                            HSMServiceProvider.Initialize(Common.CRYPTOKI);
                            foreach (DataRow dr in dtPK.Rows)
                            {
                                int slotID = Convert.ToInt32(dr["SlotID"]);
                                string serial = dr["Serial"].ToString();

                                using (HSMServiceProvider hsm = new HSMServiceProvider())
                                {
                                    //Lưu db
                                    _bus.HSM_Slot_UpdateUserPIN(slotID, ESLogin.StringCryptor.EncryptString(newPIN));
                                    //Đăng nhập
                                    HSMReturnValue ret = hsm.Login(serial, HSMLoginRole.User, oldPIN);
                                    if (ret != HSMReturnValue.OK)
                                        throw new Exception(ret.ToString());
                                    //Đổi mật khẩu
                                    ret = hsm.ChangeSlotPIN(oldPIN, newPIN);
                                    if (ret != HSMReturnValue.OK)
                                        throw new Exception(ret.ToString());
                                }
                            }                            
                        }
                        catch (Exception ex)
                        {
                            //Nếu lỗi thì thử hồi lại mật khẩu cũ
                            foreach (DataRow dr in dtPK.Rows)
                            {
                                int slotID = Convert.ToInt32(dr["SlotID"]);
                                string serial = dr["Serial"].ToString();

                                using (HSMServiceProvider hsm = new HSMServiceProvider())
                                {
                                    //Lưu db
                                    _bus.HSM_Slot_UpdateUserPIN(slotID, ESLogin.StringCryptor.EncryptString(oldPIN));
                                    //Đăng nhập
                                    HSMReturnValue ret = hsm.Login(serial, HSMLoginRole.User, newPIN);
                                    if (ret == HSMReturnValue.OK)
                                        ret = hsm.ChangeSlotPIN(newPIN, oldPIN);
                                }
                            }

                            throw ex;
                        }
                        finally
                        {
                            HSMServiceProvider.Finalize();
                        }
                        #endregion

                        #region Send Mail
                        try
                        {
                            string email_sender = "";
                            string email_password = "";
                            string smtp_server = "";
                            string smtp_port = "";
                            string smtp_ssl = "";
                            string email_to = "";
                            string user_to = "";
                            string cmtnd_to = "";

                            // Lấy địa chỉ người nhận
                            user_to = cfgNguoiDung.Rows[cfgNguoiDung.Row]["Name"].ToString();
                            cmtnd_to = cfgNguoiDung.Rows[cfgNguoiDung.Row]["CMND"].ToString();
                            email_to = cfgNguoiDung.Rows[cfgNguoiDung.Row]["Email"].ToString();

                            // gán giá trị thong tin mail
                            DataTable dt_Mail = _bus.Q_CONFIG_Select_Mail();
                            foreach (DataRow itemmail in dt_Mail.Rows)
                            {
                                switch (itemmail["Name"].ToString())
                                {
                                    case "email_sender":
                                        email_sender = itemmail["Value"].ToString();
                                        break;
                                    case "email_password":
                                        email_password = StringCryptor.DecryptString(itemmail["Value"].ToString());
                                        break;
                                    case "smtp_server":
                                        smtp_server = itemmail["Value"].ToString();
                                        break;
                                    case "smtp_port":
                                        smtp_port = itemmail["Value"].ToString();
                                        break;
                                    case "smtp_ssl":
                                        smtp_ssl = itemmail["Value"].ToString();
                                        break;
                                }
                            }

                            SmtpClient client = new SmtpClient(smtp_server);
                            client.Port = Convert.ToInt32(smtp_port);
                            client.EnableSsl = Convert.ToBoolean(smtp_ssl);
                            client.Timeout = 60000;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Credentials = new NetworkCredential(email_sender, email_password);
                            //Soạn message
                            MailMessage msg = new MailMessage();
                            msg.IsBodyHtml = true;
                            msg.From = new MailAddress(email_sender);
                            msg.To.Add(email_to);
                            msg.Subject = "[EVNNLDC] Thiết lập lại mã PIN đăng nhập HSM";

                            msg.Body = String.Format(@"
                            <b><u><i>Kính gửi</i></u></b>: {0}<br>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            CMND/MST: {1}<br><br>
                            Vào hồi {2}, Hệ thống quản trị chữ ký số đã thiết lập lại mã PIN đăng nhập HSM của bạn bằng mật khẩu ngẫu nhiên.<br>
                            Mã PIN mới: <b>{3}</b><br><br>
                            Để thay đổi mã PIN, vui lòng truy cập theo đường dẫn:<br>http://www.thitruongdien.evn.vn/Default.aspx?Menu=198 <br><br>
                            <b><u><i>Ghi chú:</i></u></b> Thông báo được gửi từ Hệ thống quản trị chữ ký số của EVN<i>NLDC</i>."
                                , user_to, cmtnd_to, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), newPIN);

                            client.Send(msg);
                        }
                        catch (Exception ex)
                        {
                            //Nếu lỗi thì thử hồi lại mật khẩu cũ
                            foreach (DataRow dr in dtPK.Rows)
                            {
                                int slotID = Convert.ToInt32(dr["SlotID"]);
                                string serial = dr["Serial"].ToString();

                                using (HSMServiceProvider hsm = new HSMServiceProvider())
                                {
                                    //Đăng nhập
                                    HSMReturnValue ret = hsm.Login(serial, HSMLoginRole.User, newPIN);
                                    if (ret == HSMReturnValue.OK)
                                        ret = hsm.ChangeSlotPIN(newPIN, oldPIN);
                                    //Lưu db
                                    _bus.HSM_Slot_UpdateUserPIN(slotID, ESLogin.StringCryptor.EncryptString(oldPIN));
                                }
                            }

                            throw ex;
                        }
                        #endregion

                        clsShare.Message_Info("Thiết lập lại mã PIN đăng nhập HSM và gửi e-mail cho người dùng thành công!");
                        break;
                        #endregion
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }
        #endregion
    }
}
