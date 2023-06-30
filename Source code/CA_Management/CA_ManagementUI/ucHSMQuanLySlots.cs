using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ES.CA_ManagementBUS;
using Telerik.WinControls.UI;
using esDigitalSignature;
using C1.Win.C1FlexGrid;
using ESLogin;

namespace ES.CA_ManagementUI
{
    public partial class ucHSMQuanLySlots : UserControl
    {
        #region Var
        BUSQuanTri _bus = new BUSQuanTri();

        private DataTable _dtSlot, _dtHSM = new DataTable();
        private ComboBox _cboEditorType = new ComboBox();
        private ComboBox _cboEditorDeviceID = new ComboBox();

        private DataTable _dtOld = new DataTable();
        private DataTable _dtNew = new DataTable();

        ContextMenuStrip _contextMenu = new ContextMenuStrip();
        ToolStripMenuItem _tspItem;

        public int DeviceID
        {
            get { return Convert.ToInt32(cboHSM.SelectedValue); }
            set { cboHSM.SelectedValue = value; }
        }
        #endregion

        public ucHSMQuanLySlots()
        {
            InitializeComponent();
        }

        private void ucHSMQuanLySlots_Load(object sender, EventArgs e)
        {
            try
            {
                //Load dữ liệu
                InitCboHSM();
                InitComboboxEditor();
                LoadData();
                InitGrid();

                //thêm sự kiện cho form
                this.cfgSlot.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
                AddContextMenu_C1FlexGrid(ref cfgSlot);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Có lỗi xảy ra khi tải dữ liệu.\n\n" + ex.Message);
            }
        }

        #region Init
        private void InitCboHSM()
        {
            // lấy danh sách slot đã khai báo từ db
            _dtHSM = _bus.HSM_Device_SelectAll();
            //Edited by Toantk on 28/4/2015
            //Thêm dòng tất cả
            DataRow dr = _dtHSM.NewRow();
            dr["DeviceID"] = -1;
            dr["Name"] = "[Tất cả]";
            _dtHSM.Rows.InsertAt(dr, 0);
            // gán giá trị vào combobox
            cboHSM.DataSource = _dtHSM;
            cboHSM.ValueMember = "DeviceID";
            cboHSM.DisplayMember = "Name";
            //Gán giá trị đầu tiên
            cboHSM.SelectedIndex = 0;
        }

        private void InitComboboxEditor()
        {
            //Lấy thuộc tính
            DataSet dsEdit = _bus.HSM_Slot_Select_ComboboxEditor();

            //Type
            _cboEditorType.DataSource = dsEdit.Tables[0];
            _cboEditorType.DisplayMember = "NAME";
            _cboEditorType.ValueMember = "VAL";

            //deviceID
            DataTable dt_Device = _bus.HSM_Device_SelectAll_Full();
            _cboEditorDeviceID.DataSource = dt_Device;
            _cboEditorDeviceID.DisplayMember = "DeviceID";
            _cboEditorDeviceID.ValueMember = "DeviceID";
        }

        private void InitGrid()
        {
            // cấu hình cfg
            //cfgSlot.AllowAddNew = false;
            //cfgSlot.AllowEditing = false;
            cfgSlot.AllowFiltering = true;
            cfgSlot.AllowSorting = AllowSortingEnum.SingleColumn;

            string[] arrName = new string[] { "", "STT", "SlotID", "DeviceID", "SlotIndex", "Serial", "Type", "Type_TXT", "InitToken", "TokenLabel", 
                "SO_PIN", "User_PIN", "UserModified", "DateModified", "SO_PIN_V", "User_PIN_V" };
            string[] arrCaption = new string[] { "", "STT", "SlotID", "HSM", "Chỉ số slot", "Số serial slot", "Type", "Loại slot", "Đã khởi tạo", "Tên token", 
                "SO_PIN", "User_PIN", "Người sửa", "Ngày sửa", "SO_PIN_V", "User_PIN_V" };

            for (int i = 0; i < arrName.Length; i++)
            {
                cfgSlot.Cols[i].Name = arrName[i];
                cfgSlot.Cols[i].Caption = arrCaption[i];
                cfgSlot.Cols[i].TextAlignFixed = TextAlignEnum.CenterCenter;

                #region Width
                // kích thước
                switch (arrName[i])
                {
                    case "":
                        cfgSlot.Cols[i].Width = 25;
                        break;
                    case "STT":
                    case "DeviceID":
                        cfgSlot.Cols[i].Width = 50;
                        break;
                    case "SlotIndex":
                    case "Type":
                    case "Status":
                    case "SO_PIN":
                    case "User_PIN":
                    case "UserModified":
                        cfgSlot.Cols[i].Width = 100;
                        break;
                    case "Serial":
                    case "TokenLabel":
                        cfgSlot.Cols[i].Width = 200;
                        break;
                    case "DateModified":
                        cfgSlot.Cols[i].Width = 150;
                        break;
                }
                #endregion

                #region Căn lề
                // căn lề
                switch (arrName[i])
                {
                    case "STT":
                    case "DeviceID":
                    case "SlotIndex":
                    case "DateModified":
                        cfgSlot.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    default:
                        cfgSlot.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                        break;
                }
                #endregion

                #region Edit
                // Chỉnh sửa
                switch (arrName[i])
                {
                    case "STT":
                    case "UserModified":
                    case "DateModified":
                        cfgSlot.Cols[i].AllowEditing = false;
                        break;
                    default:
                        cfgSlot.Cols[i].AllowEditing = true;
                        break;
                }
                #endregion

                #region Hide
                // ẩn cột
                switch (arrName[i])
                {
                    case "SlotID":
                    case "Type":
                    case "SO_PIN_V":
                    case "SO_PIN":
                    case "User_PIN":
                    case "User_PIN_V":
                        cfgSlot.Cols[i].Visible = false;
                        break;
                }
                #endregion

                #region Format
                //Format
                switch (arrName[i])
                {
                    case "DateModified":
                        cfgSlot.Cols[i].Format = "dd/MM/yyyy HH:mm:ss";
                        break;
                }
                #endregion

                #region Editor
                switch (arrName[i])
                {
                    case "DeviceID":
                        cfgSlot.Cols[i].Editor = _cboEditorDeviceID;
                        break;
                    case "Type_TXT":
                        cfgSlot.Cols[i].Editor = _cboEditorType;
                        break;
                    default:
                        break;
                }
                #endregion
            }
        }

        private void AddContextMenu_C1FlexGrid(ref C1FlexGrid c1Grid)
        {
            //Thêm Lệnh Xem log
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "ListSlot";
            _tspItem.Text = "Danh sách slot trên HSM";
            _contextMenu.Items.Add(_tspItem);

            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "IndexToDB";
            _tspItem.Text = "Cập nhật Index HSM vào cơ sở dữ liệu";
            _contextMenu.Items.Add(_tspItem);

            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "AddSlot";
            _tspItem.Text = "Thêm slot trên HSM";
            _contextMenu.Items.Add(_tspItem);

            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "Replicate";
            _tspItem.Text = "Nhân bản slot trên HSM";
            _contextMenu.Items.Add(_tspItem);

            ////Thêm Lệnh
            //_tspItem = new ToolStripMenuItem();
            //_tspItem.Name = "ResetPIN";
            //_tspItem.Text = "Thiết lập lại mã PIN";
            //_contextMenu.Items.Add(_tspItem);

            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "DelSlot";
            _tspItem.Text = "Xóa slot trên HSM";
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
            //Edited by Toantk on 28/4/2015
            //Sửa chuyển PIN thành ký tự "*" trong store
            //Sửa chọn load tất cả HSM hoặc từng HSM
            if (DeviceID == -1)
                _dtSlot = _bus.HSM_Slot_SelectAll();
            else
                _dtSlot = _bus.HSM_Slot_SelectByDeviceID(DeviceID);
            //
            cfgSlot.DataSource = _dtSlot;
        }
        #endregion

        #region Event

        private void btnResetPin_Click(object sender, EventArgs e)
        {
            try
            {
                if (cfgSlot.Row != cfgSlot.RowSel)
                    return;
                int index = cfgSlot.Row - 1;
                string serial = _dtSlot.Rows[index]["Serial"].ToString();
                string tokenLabel = _dtSlot.Rows[index]["TokenLabel"].ToString();
                frmResetPin frm = new frmResetPin(HSMLoginRole.User, DeviceID, serial);
                frm.Text = "Thiết lập lại mã PIN cho slot<" + serial + "> " + tokenLabel;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Lỗi khi nhân bản slot!\n\n" + ex.Message);
            }
        }

        //==============================================================================//
        private void btnXem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitGrid();
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
                if (cfgSlot.Row <= ((DataTable)cfgSlot.DataSource).Rows.Count)
                {
                    if (cfgSlot.Rows[cfgSlot.Row]["SlotID"].ToString() != "")
                    {
                        if (clsShare.Message_WarningYN("CHÚ Ý: Xóa slot sẽ xóa đồng thời các object tương ứng.\n\nBạn có chắc chắn muốn xóa slot này không?"))
                        {

                            int slotID = Convert.ToInt32(cfgSlot.Rows[cfgSlot.Row]["SlotID"]);
                            if (_bus.HSM_Slot_DeleteBy_SlotID(slotID))
                            {
                                clsShare.Message_Info("Xóa slot thành công!");
                                btnXem_Click(null, null);
                            }
                            else
                                clsShare.Message_Error("Lỗi khi cập nhật cơ sở dữ liệu!");
                        }
                    }
                    else
                        cfgSlot.Rows.Remove(cfgSlot.Row);
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                bool isEditing = cfgSlot.AllowAddNew;
                if (!isEditing)
                {
                    btnUpdate.Text = "Dừng cập nhật";
                    cfgSlot.AllowAddNew = !isEditing;
                    cfgSlot.AllowEditing = !isEditing;
                    //
                    _dtOld = ((DataTable)cfgSlot.DataSource).Copy();
                }
                else
                {
                    //Lấy dữ liệu mới
                    _dtNew = ((DataTable)cfgSlot.DataSource).Copy();

                    // Kiểm tra các trường bắt buộc nhập
                    DataRow[] drError = _dtNew.Select(@"DeviceID IS NULL OR SlotIndex IS NULL OR (Serial IS NULL OR Serial = '') 
                                                         OR Type IS NULL OR InitToken IS NULL");
                    if (drError.Count() > 0)
                    {
                        clsShare.Message_Error("Các trường HSM, Chỉ số slot, Số serial slot, Loại slot và Đã khởi tạo không được phép để trống.");
                        return;
                    }

                    //Lưu
                    if (clsShare.Message_QuestionYN("Bạn có muốn lưu thay đổi không?"))
                    {
                        if (_bus.HSM_Slot_InsertUpdate(_dtOld, _dtNew, clsShare.sUserName))
                            clsShare.Message_Info("Cập nhật thành công!");
                        else
                        {
                            clsShare.Message_Error("Lỗi trong quá trình lưu dữ liệu.");
                            return;
                        }
                    }

                    btnUpdate.Text = "Cập nhật";
                    cfgSlot.AllowAddNew = !isEditing;
                    cfgSlot.AllowEditing = !isEditing;
                    LoadData();
                    InitGrid();
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void cfgSlot_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                //Kiểm tra ràng buộc
                switch (cfgSlot.Cols[e.Col].Name)
                {
                    case "DeviceID":
                    case "SlotIndex":
                    case "Serial":
                    case "Type_TXT":
                    case "InitToken":
                        if (clsShare.isEmpty(cfgSlot.Rows[e.Row][e.Col]))
                        {
                            clsShare.Message_Warning("Trường bắt buộc nhập!");
                            e.Cancel = false;
                        }
                        break;
                    default:
                        break;
                }

                //Cập nhật ID
                switch (cfgSlot.Cols[e.Col].Name)
                {
                    case "Type_TXT":
                        cfgSlot.Rows[e.Row]["Type"] = _cboEditorType.SelectedValue;
                        break;
                    default:
                        break;
                }

                //Check TokenLabel không được trùng
                switch (cfgSlot.Cols[e.Col].Name)
                {
                    case "DeviceID":
                    case "TokenLabel":
                        int deviceID = cfgSlot.Rows[e.Row]["DeviceID"].ToString() == "" ? -1 : Convert.ToInt32(cfgSlot.Rows[e.Row]["DeviceID"]);
                        string tokenLabel = cfgSlot.Rows[e.Row]["TokenLabel"].ToString();
                        if (tokenLabel != "")
                        {
                            DataTable dt_tokenlabel = _bus.HSM_Slot_SelectByTokenLabel_DeviceID(tokenLabel, deviceID);
                            if (dt_tokenlabel.Rows.Count > 0)
                            {
                                clsShare.Message_Warning("Tên Token Label này đã được khai báo");
                                cfgSlot.Rows[e.Row]["TokenLabel"] = "";
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

        private void contextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Name)
                {
                    case "ListSlot":
                        #region ListSlot
                        DataTable dtSlot = new DataTable();
                        using (HSMServiceProvider hsm = new HSMServiceProvider(Common.CRYPTOKI))
                        {
                            dtSlot = hsm.GetSlotList();
                        }
                        frmHSMXemDanhSach frm1 = new frmHSMXemDanhSach();
                        frm1.Text = "Danh sách slot trên HSM";
                        frm1.DT = dtSlot;
                        frm1.ShowDialog();
                        break;
                        #endregion
                    case "AddSlot":
                        #region AddSlot
                        //Gọi form Thêm Slot
                        //Toantk 28/4/2015: Bỏ truyền Service Provider, thay bằng tạo mới
                        //Toantk 23/8/2015: Bỏ truyền thông tin Device; chọn HSM và nhập pass trên form Thêm
                        frmHSMThemSlot frm = new frmHSMThemSlot();
                        frm.ShowDialog();

                        //Cập nhật db
                        if (frm.DialogResult == DialogResult.OK)
                        {
                            if (clsShare.Message_QuestionYN("Thêm và khởi tạo slot trên HSM thành công!\n\nBạn có muốn cập nhật cơ sở dữ liệu?"))
                            {
                                // cập nhật trong csdl
                                if (_bus.HSM_Slot_InsertUpdate(-1, frm.DeviceID, -1, frm.SlotSerial, 1, true, frm.TokenLabel, clsShare.sUserName))
                                {
                                    clsShare.Message_Info("Cập nhật cơ sở dữ liệu thành công!");
                                    btnXem_Click(null, null);
                                }
                                else
                                    clsShare.Message_Error("Lỗi khi cập nhật cơ sở dữ liệu!");
                            }
                        }
                        break;
                        #endregion
                    case "Replicate":
                        #region Replicate
                        frmHSMNhanBanSlot frm2 = new frmHSMNhanBanSlot();
                        frm2.SlotID_Nguon = Convert.ToInt32(cfgSlot.Rows[cfgSlot.Row]["SlotID"]);
                        frm2.DeviceID_Nguon = Convert.ToInt32(cfgSlot.Rows[cfgSlot.Row]["DeviceID"]);
                        frm2.ShowDialog();

                        if (frm2.DialogResult == DialogResult.OK)
                        {
                            if (clsShare.Message_QuestionYN("Nhân bản slot trên HSM thành công!\n\nBạn có muốn cập nhật cơ sở dữ liệu?"))
                            {                                
                                // cập nhật trong csdl
                                if (_bus.HSM_Object_InsertCopy(frm2.SlotID_Dich, frm2.SlotID_Nguon, clsShare.sUserName))
                                {
                                    clsShare.Message_Info("Cập nhật cơ sở dữ liệu thành công!");
                                    btnXem_Click(null, null);
                                }
                                else
                                    clsShare.Message_Error("Lỗi khi cập nhật cơ sở dữ liệu!");
                            }
                        }
                        break;
                        #endregion
                    case "IndexToDB":
                        #region IndexToDB
                        _contextMenu.Close();
                        DataTable dtSlots = new DataTable();
                        using (HSMServiceProvider hsm = new HSMServiceProvider(Common.CRYPTOKI))
                        {
                            dtSlots = hsm.GetSlotList();
                        }
                        // cập nhật trong csdl
                        if (_bus.HSM_Slot_UpdateIndex(dtSlots, clsShare.sUserName))
                        {
                            clsShare.Message_Info("Cập nhật cơ sở dữ liệu thành công!");
                            btnXem_Click(null, null);
                        }
                        else
                            clsShare.Message_Error("Lỗi khi cập nhật cơ sở dữ liệu!");
                        break;
                        #endregion
                    case "DelSlot":
                        #region DelSlot
                        _contextMenu.Close();
                        if (cfgSlot.Row <= ((DataTable)cfgSlot.DataSource).Rows.Count)
                        {
                            if (cfgSlot.Rows[cfgSlot.Row]["SlotID"].ToString() != "")
                            {
                                int slotID = Convert.ToInt32(cfgSlot.Rows[cfgSlot.Row]["SlotID"]);
                                string label = cfgSlot.Rows[cfgSlot.Row]["TokenLabel"].ToString();
                                string serial = cfgSlot.Rows[cfgSlot.Row]["Serial"].ToString();
                                int deviceID = Convert.ToInt32(cfgSlot.Rows[cfgSlot.Row]["DeviceID"]);
                                DataRow drDeviceDich = _dtHSM.Select("DeviceID = " + deviceID)[0];

                                //Chỉ được xóa User Slot
                                if (cfgSlot.Rows[cfgSlot.Row]["Type_TXT"].ToString() != "User Slot")
                                {
                                    clsShare.Message_Error("Không thể xóa do không phải User Slot!");
                                    return;
                                }

                                //Toantk 31/12/2015: Bỏ check slot rỗng
                                //// kiem tra da co object chua
                                //DataTable dt_object = _bus.HSM_Object_SelectBySlotID(slotID);
                                //if (dt_object.Rows.Count > 0)
                                //{
                                //    clsShare.Message_Info("Slot cần xóa phải rỗng. Hãy kiểm tra lại!");
                                //    return;
                                //}

                                if (clsShare.Message_WarningYN("CHÚ Ý: Cặp khóa ký của người dùng trong slot sẽ bị xóa vĩnh viễn nếu xóa slot.\n\nBạn có chắc chắn muốn xóa slot " + label + " (" + serial + ") trên HSM không?"))
                                {
                                    //Gọi form nhập pass
                                    frmHSMInputPassword frm4 = new frmHSMInputPassword("Hãy nhập mã User PIN của thiết bị " + drDeviceDich["Name"].ToString());
                                    if (frm4.ShowDialog() == DialogResult.Cancel)
                                        return;
                                    string pin = frm4.InputPassword;

                                    //Xóa slot trên thiết bị HSM
                                    using (HSMServiceProvider hsm = new HSMServiceProvider(Common.CRYPTOKI))
                                    {
                                        //Login Admin
                                        HSMReturnValue eResultLogin = hsm.LoginAdmin(deviceID, HSMLoginRole.User, pin);
                                        if (eResultLogin == HSMReturnValue.OK)
                                        {
                                            hsm.DestroySlot(serial);
                                        }
                                        else if (eResultLogin == HSMReturnValue.PIN_INCORRECT)
                                        {
                                            clsShare.Message_Error("Sai mã PIN thiết bị!");
                                            return;
                                        }
                                        else
                                            throw new Exception(eResultLogin.ToString());
                                    }

                                    //Xóa trong csdl
                                    if (clsShare.Message_QuestionYN("Xóa slot trên HSM thành công!\n\nBạn có muốn cập nhật cơ sở dữ liệu?"))
                                    {
                                        // cập nhật trong csdl
                                        if (_bus.HSM_Slot_DeleteBy_SlotID(slotID))
                                        {
                                            clsShare.Message_Info("Cập nhật cơ sở dữ liệu thành công!");
                                            btnXem_Click(null, null);
                                        }
                                        else
                                            clsShare.Message_Error("Lỗi khi cập nhật cơ sở dữ liệu!");
                                    }
                                }
                            }
                        }
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
