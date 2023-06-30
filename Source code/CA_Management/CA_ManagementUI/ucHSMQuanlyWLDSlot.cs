using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ES.CA_ManagementBUS;
using esDigitalSignature;
using Telerik.WinControls.UI;
using C1.Win.C1FlexGrid;
using System.Net.Mail;
using System.Net;
using ESLogin;

namespace ES.CA_ManagementUI
{
    public partial class ucHSMQuanlyWLDSlot : UserControl
    {
        #region Var
        BUSQuanTri _bus = new BUSQuanTri();

        private DataTable _dt_Slot = new DataTable();
        private DataTable _dtNew = new DataTable();
        private DataTable _dtOld = new DataTable();

        private DataRow _dataRowBefor; // lưu thông tin dòng
        private int _rowBefor = -1; // lưu chỉ số dòng 
        private bool _isAfterEdit = false;

        private ComboBox _cboSlotTokenLable = new ComboBox();
        ContextMenuStrip _contextMenu = new ContextMenuStrip();
        ToolStripMenuItem _tspItem;
        #endregion

        public ucHSMQuanlyWLDSlot()
        {
            InitializeComponent();
        }

        private void ucHSMQuanlyWLDSlot_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDataWLDSlot();
                InitComboboxEditor();
                InitrgvWLDSlot();
                LoadDataWLDObject();
                InitrgvWLDObject();

                AddContextMenu_C1FlexGrid(ref cfgWLDSlot);
                cfgWLDSlot.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
                cfgWLDObject.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region Data
        private void LoadDataWLDSlot()
        {
            //cfgWLDSlot.SelChange -= cfgWLDSlot_SelChange;
            _dt_Slot = _bus.HSM_WLDSlot_SelectAll();
            cfgWLDSlot.DataSource = _dt_Slot;
            //cfgWLDSlot.SelChange += cfgWLDSlot_SelChange;

            _dataRowBefor = _dt_Slot.NewRow();
        }

        private void LoadDataWLDObject()
        {
            DataTable dtWLDObject = new DataTable();
            string[] arrName = { "STT", "WLDSlotID", "NAME", "CKA_LABEL", "CKA_CLASS", "CKA_CLASS_Name", "CKA_SUBJECT", "CertID", "NameCN" };
            for (int i = 0; i < arrName.Length; i++)
            {
                if (i == 2 || i == 3 || i == 5 || i == 8)
                    dtWLDObject.Columns.Add(arrName[i], typeof(string));
                else
                    dtWLDObject.Columns.Add(arrName[i], typeof(int));
            }
            cfgWLDObject.DataSource = dtWLDObject;
        }
        #endregion

        #region Init
        private void InitrgvWLDSlot()
        {
            cfgWLDSlot.AllowFiltering = true;
            cfgWLDSlot.AllowSorting = AllowSortingEnum.SingleColumn;

            //Thêm trường STT và ẩn cột ID
            string[] arrName = { "", "WLDSlotID", "NAME", "TokenLabel", "Serial", "Description" };
            string[] arrCaption = { "", "ID", "Tên", "Token Label", "Serial", "Mô tả" };

            #region For
            for (int i = 0; i < arrName.Length; i++)
            {
                // tên và header
                cfgWLDSlot.Cols[i].Name = arrName[i];
                cfgWLDSlot.Cols[i].Caption = arrCaption[i];
                cfgWLDSlot.Cols[i].TextAlignFixed = TextAlignEnum.CenterCenter;

                #region Căn lề
                // căn lề
                //if (i == 1)
                //    cfgWLDSlot.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                //else
                //    cfgWLDSlot.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                switch (arrName[i])
                {
                    case "WLDSlotID":
                        cfgWLDSlot.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    default:
                        cfgWLDSlot.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                        break;
                }
                #endregion

                #region Width
                // kích thước cột
                //if (i == 0)
                //    cfgWLDSlot.Cols[i].Width = 25;
                //else if (i == 1)
                //    cfgWLDSlot.Cols[i].Width = 50;
                //else if (i == 3)
                //    cfgWLDSlot.Cols[i].Width = 250;
                //else if (i == 4)
                //    cfgWLDSlot.Cols[i].Width = 180;
                //else if(i == 5 || i == 6)
                //    cfgWLDSlot.Cols[i].Width = 150;
                switch (arrName[i])
                {
                    case "":
                        cfgWLDSlot.Cols[i].Width = 25;
                        break;
                    case "WLDSlotID":
                        cfgWLDSlot.Cols[i].Width = 50;
                        break;
                    case "NAME":
                        cfgWLDSlot.Cols[i].Width = 250; ;
                        break;
                    case "TokenLabel":
                        cfgWLDSlot.Cols[i].Width = 180;
                        break;
                    case "Serial":
                    case "Description":
                        cfgWLDSlot.Cols[i].Width = 150;
                        break;
                }
                #endregion

                //// ẩn các cột không cần thiết
                //if (i == 2)
                //    cfgWLDSlot.Cols[i].Visible = false;

                #region Edit
                // Edit
                switch (arrName[i])
                {
                    case "WLDSlotID":
                    case "TokenLabel":
                    case "Serial":
                    case "Description":
                        cfgWLDSlot.Cols[i].AllowEditing = true;
                        break;
                    default:
                        cfgWLDSlot.Cols[i].AllowEditing = false;
                        break;
                }
                #endregion

                #region Edittor
                switch (arrName[i])
                {
                    case "TokenLabel":
                        cfgWLDSlot.Cols[i].Editor = _cboSlotTokenLable;
                        break;
                }
                #endregion
            }
            #endregion

            //cfgWLDSlot.SelChange += cfgWLDSlot_SelChange;
            cfgWLDSlot.StartEdit += cfgWLDSlot_StartEdit;
            cfgWLDSlot.Click += cfgWLDSlot_Click;
        }

        private void InitrgvWLDObject()
        {
            // cau hinh
            cfgWLDObject.AllowMerging = AllowMergingEnum.RestrictRows;

            string[] arrName = { "", "STT", "ObjectID", "SlotID", "Slot", "CKA_LABEL", "CKA_CLASS", "CKA_CLASS_Name", "CKA_ID", "CKA_SUBJECT", 
                "STATUS", "STATUS_Name", "CertID", "NameCN" };
            string[] arrCaption = { "", "STT", "ID", "SlotID", "Slot", "LABEL", "CKA_CLASS", "CLASS", "ID", "SUBJECT", 
                "STATUS", "Trạng thái", "CertID", "Chứng thư số - Ngày hết hạn" };

            #region For
            for (int i = 0; i < arrName.Length; i++)
            {
                cfgWLDObject.Cols[i].Name = arrName[i];
                cfgWLDObject.Cols[i].Caption = arrCaption[i];
                cfgWLDObject.Cols[i].TextAlignFixed = TextAlignEnum.CenterCenter;

                #region Căn lề
                // căn lề
                switch (arrName[i])
                {
                    case "STT":
                    case "STATUS_Name":
                        cfgWLDObject.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    default:
                        cfgWLDObject.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                        break;
                }
                #endregion

                #region Width
                // kích thước
                switch (arrName[i])
                {
                    case "":
                        cfgWLDObject.Cols[i].Width = 25;
                        break;
                    case "STT":
                        cfgWLDObject.Cols[i].Width = 50;
                        break;
                    case "Slot":
                    case "CKA_SUBJECT":
                    case "CKA_ID":
                        cfgWLDObject.Cols[i].Width = 180;
                        break;
                    case "CKA_LABEL":
                    case "CKA_CLASS_Name":
                    case "STATUS_Name":
                        cfgWLDObject.Cols[i].Width = 140;
                        break;
                    case "NameCN":
                        cfgWLDObject.Cols[i].Width = 250;
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
                        cfgWLDObject.Cols[i].Visible = false;
                        break;
                    default:
                        cfgWLDObject.Cols[i].Visible = true;
                        break;
                }
                #endregion

                #region Edit
                switch (arrName[i])
                {
                    case "Slot":
                    case "CKA_LABEL":
                    case "CKA_CLASS_Name":
                    case "CKA_ID":
                    case "CKA_SUBJECT":
                    case "STATUS_Name":
                    case "NameCN":
                        cfgWLDObject.Cols[i].AllowEditing = true;
                        break;
                    default:
                        cfgWLDObject.Cols[i].AllowEditing = false;
                        break;
                }
                #endregion

                #region Merging
                switch (arrName[i])
                {
                    case "Slot":
                        cfgWLDObject.Cols[i].AllowMerging = true;
                        break;
                    default:
                        cfgWLDObject.Cols[i].AllowMerging = false;
                        break;
                }
                #endregion
            }
            #endregion
        }

        private void InitComboboxEditor()
        {
            _cboSlotTokenLable.DataSource = _bus.HSM_Slot_SelectAll_Editor();
            _cboSlotTokenLable.DisplayMember = "TokenLabel";
            _cboSlotTokenLable.ValueMember = "TokenLabel";
        }

        private void AddContextMenu_C1FlexGrid(ref C1FlexGrid c1Grid)
        {
            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "CreateWLDSlot";
            _tspItem.Text = "Tạo WLD Slot trên HSM Server";
            _contextMenu.Items.Add(_tspItem);

            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "DeleteWLDSlot";
            _tspItem.Text = "Xóa WLD Slot trên HSM Server";
            _contextMenu.Items.Add(_tspItem);

            //Add vào grid
            c1Grid.ContextMenuStrip = _contextMenu;
            //Add sự kiện
            _contextMenu.ItemClicked += new ToolStripItemClickedEventHandler(contextMenu_ItemClicked);
        }

        private void Refesh()
        {
            LoadDataWLDSlot();
            InitrgvWLDSlot();
        }
        #endregion

        #region Controls
        private void btnDelSlot_Click(object sender, EventArgs e)
        {
            try
            {
                if (cfgWLDSlot.Row <= ((DataTable)cfgWLDSlot.DataSource).Rows.Count)
                {
                    if (cfgWLDSlot.Row > 0)
                        if (cfgWLDSlot.Rows[cfgWLDSlot.Row]["NAME"].ToString() != "")
                        {
                            if (clsShare.Message_QuestionYN("Bạn có chắc muốn xóa WLD Slot này không?"))
                            {
                                int wLDSlotID = Convert.ToInt32(cfgWLDSlot.Rows[cfgWLDSlot.Row]["WLDSlotID"]);
                                if (_bus.HSM_WLDSlot_DeleteByWLDSlotID(wLDSlotID))
                                {
                                    clsShare.Message_Info("Xóa WLD Slot thành công!");
                                    Refesh();
                                }
                                else
                                    clsShare.Message_Error("Lỗi khi cập nhật cơ sở dữ liệu. Hãy thử lại!");
                            }
                        }
                        else
                            cfgWLDSlot.Rows.Remove(cfgWLDSlot.Row);
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cfgWLDSlot_Click(object sender, EventArgs e)
        {
            try
            {
                if (cfgWLDSlot.Row != cfgWLDSlot.RowSel)
                    return;
                int index = cfgWLDSlot.Row;
                DataTable dttmp = (DataTable)cfgWLDSlot.DataSource;
                if (cfgWLDSlot.Row <= 0 || cfgWLDSlot.Rows[index]["WLDSlotID"] == null || cfgWLDSlot.Rows[index]["WLDSlotID"].ToString() == "") return;

                //load danh sach các object
                string tokenLabel = cfgWLDSlot.Rows[index]["TokenLabel"].ToString();
                cfgWLDObject.DataSource = _bus.HSM_Object_SelectBy_TokenLabel(tokenLabel);
                InitrgvWLDObject();
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
                bool isEditing = cfgWLDSlot.AllowAddNew;
                if (!isEditing)
                {
                    btnUpdate.Text = "Dừng cập nhật";
                    _dtOld = ((DataTable)cfgWLDSlot.DataSource).Copy();
                }
                else
                {
                    // Lay du lieu moi
                    _dtNew = ((DataTable)cfgWLDSlot.DataSource).Copy();

                    // Check cac truong bat buoc nhap
                    DataRow[] dr_Error = _dtNew.Select("WLDSlotID IS NULL OR TokenLabel IS NULL OR TokenLabel = '' OR Serial IS NULL OR Serial = '' ");
                    if (dr_Error.Count() > 0)
                    {
                        clsShare.Message_Error("Các trường ID, Token Label, Serial không được để trống.");
                        return;
                    }

                    if (clsShare.Message_QuestionYN("Bạn có muốn lưu lại thay đổi không?"))
                        // Insert Update vao db
                        if (_bus.HSM_WLDSlot_InsertUpdate(_dtOld, _dtNew, clsShare.sUserName))
                            clsShare.Message_Info("Cập nhật dữ liệu thành công!");
                        else
                        {
                            clsShare.Message_Error("Xảy ra lỗi trong quá trình cập nhật.");
                            return;
                        }
                    //Reset dữ liệu
                    Refesh();

                    btnUpdate.Text = "Cập nhật";

                }
                cfgWLDSlot.AllowAddNew = !isEditing;
                cfgWLDSlot.AllowEditing = !isEditing;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cfgWLDSlot_SelChange(object sender, EventArgs e)
        {
            try
            {
                if (_isAfterEdit && _rowBefor != -1 && _rowBefor != cfgWLDSlot.Row)
                {
                    _isAfterEdit = false;

                    //cập nhật 
                    string wLDSlotIDOld = _dataRowBefor["WLDSlotID"].ToString();
                    string wLDSlotIDNew = cfgWLDSlot.Rows[_rowBefor]["WLDSlotID"].ToString();
                    string nameOld = _dataRowBefor["NAME"].ToString();
                    string nameNew = cfgWLDSlot.Rows[_rowBefor]["NAME"].ToString();
                    string tokenLabelOld = _dataRowBefor["TokenLabel"].ToString();
                    string tokenLabelNew = cfgWLDSlot.Rows[_rowBefor]["TokenLabel"].ToString();
                    string serialOld = _dataRowBefor["Serial"].ToString();
                    string serialNew = cfgWLDSlot.Rows[_rowBefor]["Serial"].ToString();
                    string descriptionOld = _dataRowBefor["Description"].ToString();
                    string descriptionNew = cfgWLDSlot.Rows[_rowBefor]["Description"].ToString();

                    //Kiểm tra có thay đổi hay không
                    if (wLDSlotIDOld != wLDSlotIDNew || nameOld != nameNew || tokenLabelOld != tokenLabelNew || serialOld != serialNew
                        || descriptionOld != descriptionNew)
                    {
                        if (clsShare.Message_QuestionYN("Bạn có muốn lưu thay đổi không?"))
                        {
                            //Kiểm tra bắt buộc nhập
                            if (wLDSlotIDNew == "")
                            {
                                clsShare.Message_Error("Các trường ID, Token Label không được để trống.");
                                cfgWLDSlot.StartEditing(_rowBefor, 1);
                                return;
                            }

                            if (_bus.HSM_WLDSlot_InsertUpdate(Convert.ToInt32(wLDSlotIDNew), tokenLabelNew, serialNew,
                                "", "", descriptionNew, clsShare.sUserName))
                            {
                                clsShare.Message_Info("Cập nhật thiết bị thành công!");
                                Refesh();
                            }
                            else
                                clsShare.Message_Error("Lỗi trong quá trình cập nhật thiết bị!");
                        }
                        else
                        {
                            //Trả giá trị cũ
                            cfgWLDSlot.Rows[_rowBefor]["WLDSlotID"] = wLDSlotIDOld;
                            cfgWLDSlot.Rows[_rowBefor]["Name"] = nameOld;
                            cfgWLDSlot.Rows[_rowBefor]["TokenLabel"] = tokenLabelOld;
                            cfgWLDSlot.Rows[_rowBefor]["Serial"] = serialOld;
                            cfgWLDSlot.Rows[_rowBefor]["Description"] = descriptionOld; ;
                        }
                    }
                    _rowBefor = cfgWLDSlot.Row;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void cfgWLDSlot_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                //_isAfterEdit = true;

                //Kiểm tra ràng buộc
                switch (cfgWLDSlot.Cols[e.Col].Name)
                {
                    case "WLDSlotID":
                    case "TokenLabel":
                    case "Serial":
                        if (clsShare.isEmpty(cfgWLDSlot.Rows[e.Row][e.Col]))
                        {
                            clsShare.Message_Warning("Trường bắt buộc nhập!");
                            e.Cancel = false;
                        }
                        break;
                    default:
                        break;
                }

                #region Code cu
                //Serial la duy nhat
                //switch (cfgWLDSlot.Cols[e.Col].Name)
                //{
                //    case "Serial":
                //        DataTable dt = (DataTable)cfgWLDSlot.DataSource;
                //        //dt.Columns[e.Col].GetType
                //        DataRow[] dr = dt.Select("Serial = " + cfgWLDSlot.Rows[e.Row][e.Col].ToString());
                //        if (dr.Count() > 1)
                //        {
                //            clsShare.Message_Warning("Serial là duy nhất!");
                //            cfgWLDSlot.Rows[e.Row][e.Col] = "";
                //        }
                //        break;
                //    default:
                //        break;
                //}
                #endregion
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void cfgWLDSlot_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                //_isAfterEdit = false;
                ////If row change --> lưu giá trị
                //if (_rowBefor != cfgWLDSlot.Row)
                //{
                //    _rowBefor = cfgWLDSlot.Row;
                //    DataTable dt = (DataTable)cfgWLDSlot.DataSource;
                //    if (cfgWLDSlot.Rows[_rowBefor].DataSource != null)
                //    {
                //        DataRowView drv = (DataRowView)cfgWLDSlot.Rows[_rowBefor].DataSource;
                //        _dataRowBefor.ItemArray = drv.Row.ItemArray;
                //    }
                //    else
                //        _dataRowBefor = _dt_Slot.NewRow();
                //}

                // đổ dữ liệu các tokenlabel chưa được 
                DataTable dt_Editor = _bus.HSM_Slot_SelectAll_NotUse();
                if (cfgWLDSlot.Cols[e.Col].Name == "TokenLabel")
                {
                    string WLDSlotID = "";
                    if (cfgWLDSlot.Row < _dt_Slot.Rows.Count)
                    {
                        WLDSlotID = cfgWLDSlot.Rows[cfgWLDSlot.Row]["WLDSlotID"].ToString();
                    }

                    if (WLDSlotID != "")
                    {
                        //cho du lieu cu vao bảng
                        DataTable dt_SlotID = _bus.HSM_WLDSlot_SelectByWLDSlotID(Convert.ToInt32(WLDSlotID));
                        if (dt_SlotID.Rows.Count > 0)
                        {
                            DataRow dr = dt_Editor.NewRow();
                            dr["TokenLabel"] = dt_SlotID.Rows[0]["TokenLabel"];
                            dt_Editor.Rows.Add(dr);
                        }
                    }
                    _cboSlotTokenLable.DataSource = dt_Editor;
                    _cboSlotTokenLable.DisplayMember = "TokenLabel";
                    _cboSlotTokenLable.ValueMember = "TokenLabel";
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
                    case "CreateWLDSlot":
                        frmHSMThemWLDSlot frm = new frmHSMThemWLDSlot();
                        frm.ShowDialog();

                        //Cập nhật db
                        if (frm.DialogResult == DialogResult.OK)
                            if (clsShare.Message_QuestionYN("Tạo WLD Slot trên HSM Server thành công!\n\nBạn có muốn cập nhật cơ sở dữ liệu?"))
                            {
                                // thuc hien insert
                                if (_bus.HSM_WLDSlot_Insert(frm.ID, frm.TokenLabel, frm.Serial, "", "", frm.Description, clsShare.sUserName))
                                {
                                    clsShare.Message_Info("Cập nhật cơ sở dữ liệu thành công!");
                                    Refesh();
                                }
                                else
                                    clsShare.Message_Error("Lỗi khi cập nhật cơ sở dữ liệu!");
                                
                            }
                        break;
                    case "DeleteWLDSlot":
                        _contextMenu.Close();
                        if (cfgWLDSlot.Row <= ((DataTable)cfgWLDSlot.DataSource).Rows.Count)
                        {
                            if (cfgWLDSlot.Rows[cfgWLDSlot.Row]["WLDSlotID"].ToString() != "")
                            {
                                int slotID = Convert.ToInt32(cfgWLDSlot.Rows[cfgWLDSlot.Row]["WLDSlotID"]);
                                string label = cfgWLDSlot.Rows[cfgWLDSlot.Row]["TokenLabel"].ToString();
                                string serial = cfgWLDSlot.Rows[cfgWLDSlot.Row]["Serial"].ToString();

                                if (clsShare.Message_QuestionYN("Bạn có chắc muốn xóa WLD Slot " + label + " (" + serial + ") trên HSM Server không?"))
                                {
                                    //Xóa WLD slot
                                    using (HSMServiceProvider hsm = new HSMServiceProvider())
                                    {
                                        hsm.DeleteWLDSlot(slotID);
                                    }

                                    //Xóa trong csdl
                                    if (clsShare.Message_QuestionYN("Xóa WLD Slot trên HSM Server thành công!\n\nBạn có muốn cập nhật cơ sở dữ liệu?"))
                                    {
                                        // cập nhật trong csdl
                                        if (_bus.HSM_WLDSlot_DeleteByWLDSlotID(slotID))
                                        {
                                            clsShare.Message_Info("Cập nhật cơ sở dữ liệu thành công!");
                                            Refesh();
                                        }
                                        else
                                            clsShare.Message_Error("Lỗi khi cập nhật cơ sở dữ liệu!");
                                    }
                                }
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
        #endregion
    }
}
