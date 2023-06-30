using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using ES.CA_ManagementBUS;
using ESLogin;
using esDigitalSignature;

namespace ES.CA_ManagementUI
{
    public partial class ucHSMQuanLyDevice : UserControl
    {
        #region Var
        private BUSQuanTri _bus = new BUSQuanTri();

        private ContextMenuStrip _contextMenu = new ContextMenuStrip();
        private ToolStripMenuItem _tspItem;

        private DataTable _dt_Device = new DataTable();
        private DataRow _dataRowBefor; // lưu thông tin dòng
        private int _rowBefor = -1; // lưu chỉ số dòng 
        private bool _isAfterEdit = false;
        #endregion

        public ucHSMQuanLyDevice()
        {
            InitializeComponent();
        }

        private void ucHSMQuanLyDevice_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitcfgDevice();
                AddContextMenu_C1FlexGrid(ref cfgDevice);

                // Thêm sự kiện KetDown
                cfgDevice.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        #region Init
        private void InitcfgDevice()
        {
            // cấu hình radGrid
            cfgDevice.ExtendLastCol = true;
            cfgDevice.Cols.Fixed = 1;
            cfgDevice.Cols[0].Width = 25;
            cfgDevice.AllowFiltering = true;

            string[] arrName = { "DeviceID", "Name", "Serial", "IP", "SO_PIN_V", "User_PIN_V", "UserModified", "DateModified", "SO_PIN", "User_PIN" };
            string[] arrHeader = { "DeviceID", "Name", "Serial", "IP", "SO PIN", "User PIN", "Người sửa", "Ngày sửa", "SO_PIN", "User_PIN" };

            #region For
            for (int i = 0; i < arrName.Count(); i++)
            {
                // tên cột và header
                cfgDevice.Cols[i + 1].Name = arrName[i];
                cfgDevice.Cols[i + 1].Caption = arrHeader[i];
                cfgDevice.Cols[i + 1].TextAlignFixed = TextAlignEnum.CenterCenter;
                cfgDevice.Cols[i + 1].AllowMerging = true;

                // căn lề
                switch (arrName[i])
                {
                    case "DeviceID":
                    case "UserModified":
                    case "DateModified":
                        cfgDevice.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    default:
                        cfgDevice.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                        break;
                }

                // ẩn các cột
                switch (arrName[i])
                {
                    case "SO_PIN":
                    case "User_PIN":
                        cfgDevice.Cols[i + 1].Visible = false;
                        break;
                }

                //cho phep sua cot
                switch (arrName[i])
                {
                    case "DeviceID":
                    case "Name":
                    case "Serial":
                    case "IP":
                    case "SO_PIN_V":
                    case "User_PIN_V":
                        cfgDevice.Cols[i + 1].AllowEditing = true;
                        break;
                    default:
                        cfgDevice.Cols[i + 1].AllowEditing = false;
                        break;
                }

                //Format
                switch (arrName[i])
                {
                    case "DateModified":
                        cfgDevice.Cols[i].Format = "dd/MM/yyyy HH:mm:ss";
                        break;
                }

                // tạo Filter và định dạng ngày tháng
                cfgDevice.Cols[i + 1].AllowFiltering = AllowFiltering.Default;
            }
            #endregion

            // kích thước cột
            cfgDevice.Cols["DeviceID"].Width = 50;
            cfgDevice.Cols["Name"].Width = 150;
            cfgDevice.Cols["Serial"].Width = 150;
            cfgDevice.Cols["IP"].Width = 150;
            cfgDevice.Cols["SO_PIN_V"].Width = 200;
            cfgDevice.Cols["User_PIN_V"].Width = 130;
        }

        private void AddContextMenu_C1FlexGrid(ref C1FlexGrid c1Grid)
        {
            //Thêm Lệnh Xem Slot
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "ListHSM";
            _tspItem.Text = "Danh sách thiết bị HSM";
            _contextMenu.Items.Add(_tspItem);

            //Thêm Lệnh Đổi mật khẩu
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "ChangePassword";
            _tspItem.Text = "Đổi mật khẩu HSM";
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
            _dt_Device = _bus.HSM_Device_SelectAll_Full();

            //Fill DataSource
            cfgDevice.SelChange -= cfgDevice_SelChange;
            cfgDevice.DataSource = _dt_Device;
            cfgDevice.SelChange += cfgDevice_SelChange;

            //Tạo cấu trúc DataRow
            _dataRowBefor = _dt_Device.NewRow();

        }
        #endregion

        #region Event
        private void cfgDevice_SelChange(object sender, EventArgs e)
        {
            try
            {
                if (cfgDevice.AllowEditing && _isAfterEdit && _rowBefor != -1 && _rowBefor != cfgDevice.Row)
                {
                    _isAfterEdit = false;

                    //cập nhật 
                    string deviceIdOld = _dataRowBefor["DeviceID"].ToString();
                    string deviceIdNew = cfgDevice.Rows[_rowBefor]["DeviceID"].ToString();
                    string nameOld = _dataRowBefor["Name"].ToString();
                    string nameNew = cfgDevice.Rows[_rowBefor]["Name"].ToString();
                    string serialOld = _dataRowBefor["Serial"].ToString();
                    string serialNew = cfgDevice.Rows[_rowBefor]["Serial"].ToString();
                    string ipOld = _dataRowBefor["IP"].ToString();
                    string ipNew = cfgDevice.Rows[_rowBefor]["IP"].ToString();
                    string soPinOld = _dataRowBefor["SO_PIN_V"].ToString();
                    string soPinNew = cfgDevice.Rows[_rowBefor]["SO_PIN_V"].ToString();
                    string userPinOld = _dataRowBefor["User_PIN_V"].ToString();
                    string userPinNew = cfgDevice.Rows[_rowBefor]["User_PIN_V"].ToString();

                    //Kiểm tra có thay đổi hay không
                    if (deviceIdOld != deviceIdNew || nameOld != nameNew || serialOld != serialNew
                        || ipOld != ipNew || soPinOld != soPinNew || userPinOld != userPinNew)
                    {
                        if (clsShare.Message_QuestionYN("Bạn có muốn lưu thay đổi không?"))
                        {
                            //Kiểm tra bắt buộc nhập
                            if (deviceIdNew == "" || nameNew == "" || serialNew == "" || ipNew == "")
                            {
                                clsShare.Message_Error("Các trường DeviceID, Name, Serial và IP không được để trống.");
                                cfgDevice.StartEditing(_rowBefor, 1);
                                return;
                            }

                            //Nếu không thay đổi PIN-View thì lấy mã pin ẩn
                            //mã hóa pin nếu khác rỗng
                            if (soPinOld == soPinNew)
                                soPinNew = cfgDevice.Rows[_rowBefor]["SO_PIN"].ToString();
                            else
                                soPinNew = soPinNew == "" ? "" : StringCryptor.EncryptString(soPinNew);
                            //
                            if (userPinOld == userPinNew)
                                userPinNew = cfgDevice.Rows[_rowBefor]["User_PIN"].ToString();
                            else
                                userPinNew = userPinNew == "" ? "" : StringCryptor.EncryptString(userPinNew);

                            if (_bus.HSM_Device_InsertUpdate(Convert.ToInt32(deviceIdNew), nameNew, serialNew, ipNew, soPinNew, userPinNew, clsShare.sUserName))
                                clsShare.Message_Info("Cập nhật thiết bị thành công!");
                            else
                                clsShare.Message_Error("Lỗi trong quá trình cập nhật thiết bị!");
                        }
                        else
                        {
                            //Trả giá trị cũ
                            cfgDevice.Rows[_rowBefor]["DeviceID"] = deviceIdOld;
                            cfgDevice.Rows[_rowBefor]["Name"] = nameOld;
                            cfgDevice.Rows[_rowBefor]["Serial"] = serialOld;
                            cfgDevice.Rows[_rowBefor]["IP"] = ipOld;
                            cfgDevice.Rows[_rowBefor]["SO_PIN_V"] = soPinOld;
                            cfgDevice.Rows[_rowBefor]["User_PIN_V"] = userPinOld;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void cfgDevice_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                _isAfterEdit = false;
                //If row change --> lưu giá trị
                if (_rowBefor != cfgDevice.Row)
                {
                    _rowBefor = cfgDevice.Row;
                    if (cfgDevice.Rows[_rowBefor].DataSource != null)
                        _dataRowBefor.ItemArray = ((DataRowView)cfgDevice.Rows[_rowBefor].DataSource).Row.ItemArray;
                    else
                        _dataRowBefor = _dt_Device.NewRow();
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void cfgDevice_AfterEdit(object sender, RowColEventArgs e)
        {
            _isAfterEdit = true;
        }

        private void contextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Name)
                {
                    case "ListHSM":
                        DataTable dtHSM = new DataTable();
                        using (HSMServiceProvider hsm = new HSMServiceProvider(Common.CRYPTOKI))
                        {
                            dtHSM = hsm.GetDeviceList();
                        }
                        frmHSMXemDanhSach frm1 = new frmHSMXemDanhSach();
                        frm1.Text = "Danh sách thiết bị HSM";
                        frm1.DT = dtHSM; 
                        frm1.ShowDialog();
                        break;
                    case "ChangePassword":
                        frmHSMDoiPassAdmin frm = new frmHSMDoiPassAdmin();
                        frm.ShowDialog();

                        //Cập nhật db
                        if (frm.DialogResult == DialogResult.OK)
                        {
                            if (clsShare.Message_QuestionYN("Thay đổi mật khẩu HSM thành công!\n\nBạn có muốn cập nhật cơ sở dữ liệu?"))
                            {
                                // cập nhật trong csdl
                                if (_bus.HSM_Device_UpdatePIN(frm.DeviceID, StringCryptor.EncryptString(frm.NewPass1), (int)frm.Role))
                                    clsShare.Message_Info("Cập nhật cơ sở dữ liệu thành công!");
                                else
                                    clsShare.Message_Error("Lỗi khi cập nhật cơ sở dữ liệu!");
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                bool isEditing = cfgDevice.AllowAddNew;
                cfgDevice.AllowAddNew = !isEditing;
                cfgDevice.AllowEditing = !isEditing;
                if (!isEditing)
                {
                    btnUpdate.Text = "Dừng cập nhật";
                }
                else
                {
                    //
                    _rowBefor = -1;
                    btnUpdate.Text = "Cập nhật";
                    //Reset dữ liệu
                    LoadData();
                    InitcfgDevice();
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
                if (clsShare.Message_QuestionYN("Bạn có chắc muốn xóa HSM này trong cơ sở dữ liệu không?"))
                {
                    int deviceID = Convert.ToInt32(cfgDevice.Rows[cfgDevice.Row]["DeviceID"]);
                    if (_bus.HSM_Device_DeleteBy_DeviceID(deviceID))
                    {
                        clsShare.Message_Info("Xóa thiết bị thành công!");
                        LoadData();
                        InitcfgDevice();
                    }
                    else
                        clsShare.Message_Error("Xảy ra lỗi trong quá trình xóa HSM. Có thể do HSM đang chứa slot.\nHãy kiểm tra lại!");
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }
        #endregion
    }
}
