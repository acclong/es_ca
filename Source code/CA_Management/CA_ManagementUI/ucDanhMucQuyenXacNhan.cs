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

namespace ES.CA_ManagementUI
{
    public partial class ucDanhMucQuyenXacNhan : UserControl
    {
        BUSQuanTri _bus = new BUSQuanTri();
        ComboBox _cboEditorLoaiVB = new ComboBox();
        ComboBox _cboEditorLoaiDonVi = new ComboBox();
        ComboBox _cboEditorLoaiCert = new ComboBox();
        ComboBox _cboEditorTrangThai = new ComboBox();

        private int FileTypeID
        {
            get { return Convert.ToInt32(cboLoaiVB.SelectedValue); }
        }

        public ucDanhMucQuyenXacNhan()
        {
            InitializeComponent();
        }

        private void ucDanhMucQuyenXacNhan_Load(object sender, EventArgs e)
        {
            try
            {
                InitCboLoaiVB();
                InitComboboxEditor();
                LoadData();
                InitCfgQuyen();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void LoadData()
        {
            // lấy dữ liệu từ database
            cfgQuyen.DataSource = _bus.FL_FileType_QuyenXacNhan_SelectBy_FileTypeID(FileTypeID);
        }

        private void InitCboLoaiVB()
        {
            BUSQuanTri bus = new BUSQuanTri();
            DataTable dt = bus.FL_FileType_SelectAll();
            DataRow dr = dt.NewRow();

            dr["FileTypeID"] = -1;
            dr["Name"] = "-- Tất cả --";

            dt.Rows.InsertAt(dr, 0);

            cboLoaiVB.DataSource = dt;
            cboLoaiVB.DisplayMember = "Name";
            cboLoaiVB.ValueMember = "FileTypeID";
            cboLoaiVB.SelectedIndex = 0;
        }

        private void InitComboboxEditor()
        {
            //Loại văn bản
            _cboEditorLoaiVB.DataSource = _bus.FL_FileType_SelectAll();
            _cboEditorLoaiVB.DisplayMember = "Name";
            _cboEditorLoaiVB.ValueMember = "FileTypeID";
            _cboEditorLoaiVB.DropDownStyle = ComboBoxStyle.DropDownList;

            //Loại đơn vị
            _cboEditorLoaiDonVi.DataSource = _bus.CA_UnitType_SelectAll();
            _cboEditorLoaiDonVi.DisplayMember = "Name";
            _cboEditorLoaiDonVi.ValueMember = "UnitTypeID";
            _cboEditorLoaiDonVi.DropDownStyle = ComboBoxStyle.DropDownList;

            //Loại chứng thư
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            DataRow dr = dt.NewRow();
            dr["id"] = 1;
            dr["name"] = "Cá nhân";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["id"] = 2;
            dr["name"] = "Doanh nghiệp";
            dt.Rows.Add(dr);
            _cboEditorLoaiCert.DataSource = dt;
            _cboEditorLoaiCert.DisplayMember = "name";
            _cboEditorLoaiCert.ValueMember = "id";
            _cboEditorLoaiCert.DropDownStyle = ComboBoxStyle.DropDownList;

            //Xác nhận
            dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dr = dt.NewRow();
            dr["id"] = 1;
            dr["name"] = "Có";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["id"] = 0;
            dr["name"] = "Không";
            dt.Rows.Add(dr);
            _cboEditorTrangThai.DataSource = dt;
            _cboEditorTrangThai.DisplayMember = "name";
            _cboEditorTrangThai.ValueMember = "id";
            _cboEditorTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void InitCfgQuyen()
        {
            cfgQuyen.AllowMerging = AllowMergingEnum.RestrictRows;
            //Thêm trường STT và ẩn cột ID
            string[] arrName = { "", "ID_QuyenXacNhan", "FileTypeID", "FileTypeName", "UnitTypeID", "UnitTypeName", "CertType", "CertTypeName", "TrangThai", "TrangThaiName", "UserModified", "DateModified" };
            string[] arrCaption = { "", "ID_QuyenXacNhan", "FileTypeID", "Loại văn bản", "UnitTypeID", "Loại đơn vị", "CertType", "Loại chứng thư", "TrangThai", "Được xác nhận", "Người sửa", "Ngày sửa" };

            for (int i = 0; i < arrName.Length; i++)
            {
                // tên và header
                cfgQuyen.Cols[i].Name = arrName[i];
                cfgQuyen.Cols[i].Caption = arrCaption[i];
                // căn lề
                if (i == 10 || i == 11)
                    cfgQuyen.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                else
                    cfgQuyen.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                // kích thước cột
                switch (i)
                {
                    case 0: cfgQuyen.Cols[i].Width = 25; break;
                    case 3:
                    case 5:
                    case 7: cfgQuyen.Cols[i].Width = 200; break;
                    case 9:
                    case 10:
                    case 11: cfgQuyen.Cols[i].Width = 100; break;
                    case 1:
                    case 2:
                    case 4:
                    case 6:
                    case 8: cfgQuyen.Cols[i].Visible = false; break;
                }
                //
                if (i == 10 || i == 11)
                    cfgQuyen.Cols[i].AllowEditing = false;
            }

            cfgQuyen.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
            cfgQuyen.Cols["FileTypeName"].AllowMerging = true;
            cfgQuyen.Cols["DateModified"].Style.Format = "dd/MM/yyyy HH:mm:ss";
            cfgQuyen.AutoGenerateColumns = false;

            //Edittor
            cfgQuyen.Cols["FileTypeName"].Style.Editor = _cboEditorLoaiVB;
            cfgQuyen.Cols["UnitTypeName"].Style.Editor = _cboEditorLoaiDonVi;
            cfgQuyen.Cols["CertTypeName"].Style.Editor = _cboEditorLoaiCert;
            cfgQuyen.Cols["TrangThaiName"].Style.Editor = _cboEditorTrangThai;

            cfgQuyen.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.cfgQuyen_BeforeEdit);
            cfgQuyen.AfterEdit += cfgQuyen_AfterEdit;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
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
                bool isEditing = cfgQuyen.AllowAddNew;
                if (!isEditing)
                {
                    btnEdit.Text = "Dừng cập nhật";
                    cfgQuyen.AllowAddNew = !isEditing;
                    cfgQuyen.AllowEditing = !isEditing;
                }
                else
                {
                    //Lấy dữ liệu mới
                    DataTable dtNew = (DataTable)cfgQuyen.DataSource;
                    // Kiểm tra các trường bắt buộc nhập
                    if (dtNew.Select("FileTypeID is NULL or UnitTypeID is NULL or CertType is NULL or TrangThai is NULL", "", DataViewRowState.CurrentRows).Count() > 0)
                    {
                        clsShare.Message_Error("Danh sách cấu hình thiếu dữ liệu!");
                        return;
                    }

                    //Lưu
                    if (clsShare.Message_QuestionYN("Bạn có muốn lưu thay đổi không?"))
                    {
                        _bus.FL_FileType_QuyenXacNhan__InsertUpdate(dtNew, clsShare.sUserName);
                        clsShare.Message_Info("Cập nhật thành công!");
                    }

                    btnEdit.Text = "Cập nhật";
                    cfgQuyen.AllowAddNew = !isEditing;
                    cfgQuyen.AllowEditing = !isEditing;
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

            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cfgQuyen_BeforeEdit(object sender, RowColEventArgs e)
        {
            try
            {
                //var flex = s1 as C1.Win.C1FlexGrid.C1FlexGrid;
                //string tmp = cfgQuyen.Rows[e.Row][1].ToString();
                if ((e.Col == 3 || e.Col == 5 || e.Col == 7) && cfgQuyen.Rows[e.Row]["ID_QuyenXacNhan"] != null && cfgQuyen.Rows[e.Row]["ID_QuyenXacNhan"].ToString() != "")
                    e.Cancel = true;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cfgQuyen_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (cfgQuyen.Cols[e.Col].Name == "FileTypeName")
                    cfgQuyen.Rows[e.Row]["FileTypeID"] = _cboEditorLoaiVB.SelectedValue;
                else if (cfgQuyen.Cols[e.Col].Name == "UnitTypeName")
                    cfgQuyen.Rows[e.Row]["UnitTypeID"] = _cboEditorLoaiDonVi.SelectedValue;
                else if (cfgQuyen.Cols[e.Col].Name == "CertTypeName")
                    cfgQuyen.Rows[e.Row]["CertType"] = _cboEditorLoaiCert.SelectedValue;
                else if (cfgQuyen.Cols[e.Col].Name == "TrangThaiName")
                    cfgQuyen.Rows[e.Row]["TrangThai"] = _cboEditorTrangThai.SelectedValue;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }
    }
}
