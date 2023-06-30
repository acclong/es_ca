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
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using Telerik.WinControls.Data;
using C1.Win.C1FlexGrid;
using esDigitalSignature.Library;
using System.Diagnostics;

namespace ES.CA_ManagementUI
{
    public partial class ucHSMQuanLyObjects : UserControl
    {
        #region Var
        BUSQuanTri _bus = new BUSQuanTri();
        private DataTable _dtSlot = new DataTable();
        private DataTable _dtObject = new DataTable();
        private DataTable _dtOld = new DataTable();
        private DataTable _dtNew = new DataTable();

        //combobox add vao grid
        ComboBox _cboEditorSlot = new ComboBox();
        ComboBox _cboEditorClass = new ComboBox();
        ComboBox _cboEditorStatus = new ComboBox();
        ComboBox _cboEditorCert = new ComboBox();

        ContextMenuStrip _contextMenu = new ContextMenuStrip();
        ToolStripMenuItem _tspItem;

        public int SlotID
        {
            get { return Convert.ToInt32(cboSelectToken.SelectedValue); }
        }
        #endregion

        public ucHSMQuanLyObjects()
        {
            InitializeComponent();
        }

        private void ucHSMQuanLyObjects_Load(object sender, EventArgs e)
        {
            try
            {
                // Load Combobox
                InitCboSlot();
                InitComboboxEditor();

                // Load dữ liệu vào rgv 
                LoadData();
                InitGrid();

                // thêm sự kiện cho combobox
                AddContextMenu_C1FlexGrid(ref cfgObject);

                cfgObject.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown); ;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region Data
        private void LoadData()
        {
            // lấy giá trị dataTable tương ứng cho rgv
            if (SlotID == -1)
                _dtObject = _bus.HSM_Object_SelectAll();
            else
                _dtObject = _bus.HSM_Object_SelectBySlotID(SlotID);

            // gán giá trị vào rgv
            cfgObject.DataSource = _dtObject;
        }
        #endregion

        #region Init
        private void InitCboSlot()
        {
            // lấy danh sách slot đã khai báo từ db
            _dtSlot = _bus.HSM_Slot_SelectByInitToken(true);
            // thêm Tất cả vào vị trí index = 0
            DataRow dr = _dtSlot.NewRow();
            dr["SlotID"] = -1;
            dr["Label_SlotID"] = "[Tất cả Slot]";
            _dtSlot.Rows.InsertAt(dr, 0);

            // gán giá trị vào combobox
            cboSelectToken.DisplayMember = "Label_SlotID";
            cboSelectToken.ValueMember = "SlotID";
            cboSelectToken.DataSource = _dtSlot;
            cboSelectToken.SelectedIndex = 0;
        }

        private void InitGrid()
        {
            // cau hinh
            cfgObject.AllowMerging = AllowMergingEnum.RestrictRows;
            cfgObject.AllowSorting = AllowSortingEnum.SingleColumn;

            string[] arrName = { "", "STT", "ObjectID", "SlotID", "Slot", "CKA_LABEL", "CKA_CLASS", "CKA_CLASS_Name", "CKA_ID", "CKA_SUBJECT", 
                "STATUS", "STATUS_Name", "CertID", "NameCN" };
            string[] arrCaption = { "", "STT", "ID", "SlotID", "Slot", "LABEL", "CKA_CLASS", "CLASS", "ID", "SUBJECT", 
                "STATUS", "Trạng thái", "CertID", "Chứng thư số - Ngày hết hạn" };

            #region For
            for (int i = 0; i < arrName.Length; i++)
            {
                cfgObject.Cols[i].Name = arrName[i];
                cfgObject.Cols[i].Caption = arrCaption[i];
                cfgObject.Cols[i].TextAlignFixed = TextAlignEnum.CenterCenter;

                #region Căn lề
                // căn lề
                switch (arrName[i])
                {
                    case "STT":
                    case "STATUS_Name":
                        cfgObject.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    default:
                        cfgObject.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                        break;
                }
                #endregion

                #region Width
                // kích thước
                switch (arrName[i])
                {
                    case "":
                        cfgObject.Cols[i].Width = 25;
                        break;
                    case "STT":
                        cfgObject.Cols[i].Width = 50;
                        break;
                    case "Slot":
                    case "CKA_SUBJECT":
                    case "CKA_ID":
                        cfgObject.Cols[i].Width = 180;
                        break;
                    case "CKA_LABEL":
                    case "CKA_CLASS_Name":
                    case "STATUS_Name":
                        cfgObject.Cols[i].Width = 140;
                        break;
                    case "NameCN":
                        cfgObject.Cols[i].Width = 250;
                        break;
                }
                #endregion

                #region Hide
                // ẩn cột
                switch (arrName[i])
                {
                    case "ObjectID":
                    case "SlotID":
                    case "CKA_CLASS":
                    case "CertID":
                    case "STATUS":
                        cfgObject.Cols[i].Visible = false;
                        break;
                    default:
                        cfgObject.Cols[i].Visible = true;
                        break;
                }
                #endregion

                #region Edit
                switch (arrName[i])
                {
                    case "STT":
                    case "ObjectID":
                        cfgObject.Cols[i].AllowEditing = false;
                        break;
                    default:
                        cfgObject.Cols[i].AllowEditing = true;
                        break;
                }
                #endregion

                #region Merging
                switch (arrName[i])
                {
                    case "Slot":
                        cfgObject.Cols[i].AllowMerging = true;
                        break;
                    default:
                        cfgObject.Cols[i].AllowMerging = false;
                        break;
                }
                #endregion

                #region Editor
                switch (arrName[i])
                {
                    case "Slot":
                        cfgObject.Cols[i].Editor = _cboEditorSlot;
                        break;
                    case "CKA_CLASS_Name":
                        cfgObject.Cols[i].Editor = _cboEditorClass;
                        break;
                    case "STATUS_Name":
                        cfgObject.Cols[i].Editor = _cboEditorStatus;
                        break;
                    case "NameCN":
                        cfgObject.Cols[i].Editor = _cboEditorCert;
                        break;
                    default:
                        break;
                }
                #endregion
            }
            #endregion
        }

        private void InitComboboxEditor()
        {
            //Slot
            DataTable dt_slot = _bus.HSM_Slot_SelectByInitToken(true);
            _cboEditorSlot.DataSource = dt_slot;
            _cboEditorSlot.DisplayMember = "Label_SlotID";
            _cboEditorSlot.ValueMember = "SlotID";

            //Lấy thuộc tính
            DataSet dsEdit = _bus.HSM_Object_Select_ComboboxEditor();

            //Class
            _cboEditorClass.DataSource = dsEdit.Tables[0];
            _cboEditorClass.DisplayMember = "NAME";
            _cboEditorClass.ValueMember = "VAL";

            //Status
            _cboEditorStatus.DataSource = dsEdit.Tables[1];
            _cboEditorStatus.DisplayMember = "NAME";
            _cboEditorStatus.ValueMember = "VAL";

            //Cert
            DataTable dt_cert = _bus.CA_Certificate_SelectAll();
            foreach (DataRow item in dt_cert.Rows)
            {
                item["NameCN"] = string.Format("{0} - {1}", item["NameCN"], item["ValidTo"] == DBNull.Value? "Không hiệu lực" : DateTime.Parse(item["ValidTo"].ToString()).ToString("dd/MM/yyyy"));
            }
            _cboEditorCert.DataSource = dt_cert;
            _cboEditorCert.DisplayMember = "NameCN";
            _cboEditorCert.ValueMember = "CertID";
        }

        private void AddContextMenu_C1FlexGrid(ref C1FlexGrid c1Grid)
        {
            ////Thêm Lệnh Xem log
            //_tspItem = new ToolStripMenuItem();
            //_tspItem.Name = "ListObject";
            //_tspItem.Text = "Danh sách object trên HSM";
            //_contextMenu.Items.Add(_tspItem);

            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "GenKeyPair";
            _tspItem.Text = "Tạo cặp khóa RSA trên HSM";
            _contextMenu.Items.Add(_tspItem);

            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "ExportCertRequest";
            _tspItem.Text = "Xuất file Certificate Request";
            _contextMenu.Items.Add(_tspItem);

            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "ImportCert";
            _tspItem.Text = "Nhập chứng thư số vào HSM";
            _contextMenu.Items.Add(_tspItem);

            //Add vào grid
            c1Grid.ContextMenuStrip = _contextMenu;
            //Add sự kiện
            _contextMenu.ItemClicked += new ToolStripItemClickedEventHandler(contextMenu_ItemClicked);
        }
        #endregion

        #region Controls
        //======================================================================//
        private void btnXem_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy dữ liệu vào rgv
                LoadData();
                InitGrid();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cfgObject_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                //Kiểm tra ràng buộc
                switch (cfgObject.Cols[e.Col].Name)
                {
                    case "Slot":
                    case "CKA_LABEL":
                    case "CKA_CLASS_Name":
                    case "CKA_ID":
                    case "STATUS_Name":
                        if (clsShare.isEmpty(cfgObject.Rows[e.Row][e.Col]))
                        {
                            clsShare.Message_Warning("Trường bắt buộc nhập!");
                            e.Cancel = false;
                        }
                        break;
                    default:
                        break;
                }

                //Cập nhật ID
                switch (cfgObject.Cols[e.Col].Name)
                {
                    case "Slot":
                        cfgObject.Rows[e.Row]["SlotID"] = _cboEditorSlot.SelectedValue;
                        break;
                    case "CKA_CLASS_Name":
                        cfgObject.Rows[e.Row]["CKA_CLASS"] = _cboEditorClass.SelectedValue;
                        break;
                    case "STATUS_Name":
                        cfgObject.Rows[e.Row]["STATUS"] = _cboEditorStatus.SelectedValue;
                        break;
                    case "NameCN":
                        cfgObject.Rows[e.Row]["CertID"] = _cboEditorCert.SelectedValue;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (cfgObject.Row <= ((DataTable)cfgObject.DataSource).Rows.Count)
                {
                    if (cfgObject.Rows[cfgObject.Row]["ObjectID"].ToString() != "")
                    {
                        if (clsShare.Message_QuestionYN("Bạn có chắc muốn xóa object này không?"))
                        {
                            int objectID = Convert.ToInt32(cfgObject.Rows[cfgObject.Row]["ObjectID"]);
                            if (_bus.HSM_Object_DeleteBy_ObjectID(objectID))
                            {
                                clsShare.Message_Info("Xóa object thành công!");
                                btnXem_Click(null, null);
                            }
                            else
                                clsShare.Message_Error("Lỗi trong quá trình xóa dữ liệu.");
                        }
                    }
                    else
                        cfgObject.Rows.Remove(cfgObject.Row);
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
                bool isEditing = cfgObject.AllowAddNew;
                if (!isEditing)
                {
                    btnUpdate.Text = "Dừng cập nhật";
                    cfgObject.AllowAddNew = !isEditing;
                    cfgObject.AllowEditing = !isEditing;
                    //
                    _dtOld = ((DataTable)cfgObject.DataSource).Copy();
                }
                else
                {
                    //Lấy dữ liệu mới
                    _dtNew = ((DataTable)cfgObject.DataSource).Copy();

                    // Kiểm tra các trường bắt buộc nhập
                    DataRow[] drError = _dtNew.Select(@"SlotID IS NULL OR (CKA_LABEL IS NULL OR CKA_LABEL = '') OR CKA_CLASS IS NULL 
                                                        OR (CKA_ID IS NULL OR CKA_ID = '') OR STATUS IS NULL");
                    if (drError.Count() > 0)
                    {
                        clsShare.Message_Error("Các trường Slot, LABEL, CLASS, ID, Trạng thái không được phép để trống.");
                        return;
                    }

                    //Lưu
                    if (clsShare.Message_QuestionYN("Bạn có muốn lưu thay đổi không?"))
                    {
                        if (_bus.HSM_Object_InsertUpdate(_dtOld, _dtNew, clsShare.sUserName))
                            clsShare.Message_Info("Cập nhật thành công!");
                        else
                        {
                            clsShare.Message_Error("Lỗi trong quá trình lưu dữ liệu.");
                            return;
                        }
                    }

                    btnUpdate.Text = "Cập nhật";
                    cfgObject.AllowAddNew = !isEditing;
                    cfgObject.AllowEditing = !isEditing;
                    btnXem_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void contextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Name)
                {
                    case "ListObject":

                        break;
                    case "GenKeyPair":
                        //Gọi form tạo cặp key
                        frmHSMTaoCapKhoaRSA frm = new frmHSMTaoCapKhoaRSA();
                        frm.SlotID = Convert.ToInt32(cboSelectToken.SelectedValue);
                        frm.ShowDialog();

                        //Cập nhật db
                        if (frm.DialogResult == DialogResult.OK)
                            if (clsShare.Message_QuestionYN("Tạo cặp khóa RSA trên HSM thành công!\n\nBạn có muốn cập nhật cơ sở dữ liệu?"))
                            {
                                // cập nhật trong csdl
                                if (_bus.HSM_Object_Insert_3Obj(frm.SlotID, frm.Subject, frm.Label_KeyPUB, frm.Label_KeyPRV, frm.Label_CertRequest, frm.CKA_ID, clsShare.sUserName))
                                {
                                    clsShare.Message_Info("Cập nhật cơ sở dữ liệu thành công!");
                                    btnXem_Click(null, null);
                                }
                                else
                                    clsShare.Message_Error("Lỗi khi cập nhật cơ sở dữ liệu!");
                            }
                        break;
                    case "ExportCertRequest":
                        _contextMenu.Close();
                        //Lấy thông tin request
                        string slotSerial = cfgObject.Rows[cfgObject.Row]["Slot"].ToString().Split(new char[] { '(', ')' })[1];
                        string reqName = cfgObject.Rows[cfgObject.Row]["CKA_LABEL"].ToString();
                        byte[] reqID = Common.ConvertHexToByte(cfgObject.Rows[cfgObject.Row]["CKA_ID"].ToString());//(byte[])dt.Rows[0]["CKA_ID"];

                        // lấy giá trị đường dẫn file xuất
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Title = "Chọn thư mục lưu Certificate Request";
                        sfd.Filter = "PEM file|*.pem";
                        sfd.FileName = reqName + ".pem";
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            using (HSMServiceProvider hsm = new HSMServiceProvider(Common.CRYPTOKI))
                            {
                                //Mở session nhưng ko đăng nhập
                                hsm.Login(slotSerial, HSMLoginRole.User, "1");
                                // lấy nội dung file xuất                
                                string sContent = hsm.ExportCertificateRequestToPEM(reqID);
                                // ghi vào file
                                File.WriteAllText(sfd.FileName, sContent);
                            }

                            if (clsShare.Message_QuestionYN("Xuất file Certificate Request thành công!\nBạn có muốn mở thư mục chứa file ngay bây giờ?"))
                                Process.Start(Path.GetDirectoryName(sfd.FileName));
                        }
                        else
                            return;
                        break;
                    case "ImportCert":
                        frmHSMImportChungThuSo frm1 = new frmHSMImportChungThuSo();
                        frm1.SlotID = Convert.ToInt32(cfgObject.Rows[cfgObject.Row]["SlotID"]);
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
                                    btnXem_Click(null, null);
                                }
                                else
                                    clsShare.Message_Error("Lỗi khi cập nhật cơ sở dữ liệu!");
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
        #endregion
    }
}
