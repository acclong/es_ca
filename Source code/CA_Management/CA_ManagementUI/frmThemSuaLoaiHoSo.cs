using C1.Win.C1FlexGrid;
using ES.CA_ManagementBUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ES.CA_ManagementUI
{
    public partial class frmThemSuaLoaiHoSo : Form
    {
        private int _profileTypeId;
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtProfileType = new DataTable();
        ComboBox _cboEditorFileType = new ComboBox();

        public int ProfileTypeId
        {
            set { _profileTypeId = value; }
            get { return _profileTypeId; }
        }

        public frmThemSuaLoaiHoSo()
        {
            InitializeComponent();
        }

        private void frmThemSuaLoaiHoSo_Load(object sender, EventArgs e)
        {
            try
            {
                txtName.Select();
                InitCboUnitType();
                InitCboDateType();
                InitCboEditor();
                if (ProfileTypeId != -1)
                {
                    //txtName.ReadOnly = true;
                    // lấy dữ liệu từ database
                    _dtProfileType = _bus.FL_ProfileType_SelectByProfileTypeID(ProfileTypeId);
                    // truyền vào các control
                    txtIdProfileType.Text = _dtProfileType.Rows[0]["ProfileTypeID"].ToString();
                    txtName.Text = _dtProfileType.Rows[0]["Name"].ToString();
                    if (_dtProfileType.Rows[0]["DateType"] != DBNull.Value)
                        cboDateType.SelectedValue = Convert.ToInt32(_dtProfileType.Rows[0]["DateType"]);
                    else
                        cboDateType.SelectedIndex = 0;
                    if (_dtProfileType.Rows[0]["UnitType"] != DBNull.Value)
                        cboUnitType.SelectedValue = Convert.ToInt32(_dtProfileType.Rows[0]["UnitType"]);
                    else
                        cboUnitType.SelectedIndex = 0;
                    dpkDateStart.Value = Convert.ToDateTime(_dtProfileType.Rows[0]["DateStart"]);
                    if (_dtProfileType.Rows[0]["DateEnd"] == DBNull.Value)
                        chkDateEnd.Checked = false;
                    else
                    {
                        dpkDateEnd.Value = Convert.ToDateTime(_dtProfileType.Rows[0]["DateEnd"]);
                        chkDateEnd.Checked = true;
                    }
                }
                //danh sách loại văn bản
                DataTable dtFileProfile = _bus.FL_FileProfile_SelectByProfileTypeID(ProfileTypeId);
                cfgFileProfile.DataSource = dtFileProfile;
                InitCfgFileProfile();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void InitCboUnitType()
        {
            cboUnitType.DataSource = _bus.CA_UnitType_SelectAll();
            cboUnitType.DisplayMember = "Name";
            cboUnitType.ValueMember = "UnitTypeID";
            cboUnitType.SelectedIndex = 0;
        }

        private void InitCboDateType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            string[] array = { "Ngày", "Tuần", "Tháng", "Quý", "Năm" };
            for (int i = 0; i < 5; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i + 1;
                dr[1] = array[i];
                dt.Rows.Add(dr);
            }
            cboDateType.DataSource = dt;
            cboDateType.DisplayMember = "Name";
            cboDateType.ValueMember = "ID";
            cboDateType.SelectedIndex = 0;
        }

        private void InitCboEditor()
        {
            // UnitType
            _cboEditorFileType.DataSource = _bus.FL_FileType_SelectAll();
            _cboEditorFileType.DisplayMember = "Name";
            _cboEditorFileType.ValueMember = "FileTypeID";
        }

        private void InitCfgFileProfile()
        {
            cfgFileProfile.AllowEditing = true;
            cfgFileProfile.AllowAddNew = true;
            cfgFileProfile.AllowDelete = true;
            cfgFileProfile.AllowMerging = AllowMergingEnum.RestrictRows;

            //Tên cột
            string[] arrName = {"", "STT", "ID_FileProfile", "ProfileTypeID", "ProfileType",
                                   "FileTypeID", "FileType", "Notation", "DateStart", "DateEnd"};
            string[] arrCaption = {"", "STT", "ID_FileProfile", "ProfileTypeID", "Loại hồ sơ",
                                   "FileTypeID", "Loại văn bản", "Notation", "Ngày áp dụng", "Ngày kết thúc" };

            #region For
            for (int i = 0; i < arrName.Length; i++)
            {
                // Tên và Header
                cfgFileProfile.Cols[i].Name = arrName[i];
                cfgFileProfile.Cols[i].Caption = arrCaption[i];
                switch (i)
                {
                    case 0: cfgFileProfile.Cols[i].Width = 25; break;
                    case 1: cfgFileProfile.Cols[i].Width = 40; break;
                    case 6: cfgFileProfile.Cols[i].Width = 200; break;
                    case 4:
                    case 7: cfgFileProfile.Cols[i].Width = 100; break;
                    case 8:
                    case 9: cfgFileProfile.Cols[i].Width = 110; break;
                }
                // Căn lề
                if (i == 4 || i == 6 || i == 7)
                    cfgFileProfile.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                else
                    cfgFileProfile.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                cfgFileProfile.Rows[0].TextAlign = TextAlignEnum.CenterCenter;

                // định dạng và filter
                if (i == 8 || i == 9)
                {
                    cfgFileProfile.Cols[i].Format = "dd/MM/yyyy";
                    cfgFileProfile.Cols[i].AllowFiltering = AllowFiltering.ByCondition;
                }
                else if (i != 0)
                    cfgFileProfile.Cols[i].AllowFiltering = AllowFiltering.Default;

                // ẩn các cột ID
                if (i == 2 || i == 3 || i == 4 || i == 5 || i == 7)
                    cfgFileProfile.Cols[i].Visible = false;

                switch (arrName[i])
                {
                    case "ProfileType":
                        cfgFileProfile.Cols[i].AllowMerging = true;
                        break;
                    default:
                        cfgFileProfile.Cols[i].AllowMerging = false;
                        break;
                }

                //edit
                if (i == 1 || i == 7)
                    cfgFileProfile.Cols[i].AllowEditing = false;
            }
            #endregion

            cfgFileProfile.Cols["FileType"].Width = 200;
            cfgFileProfile.AutoGenerateColumns = false;
            //Edittor
            cfgFileProfile.Cols["FileType"].Style.Editor = _cboEditorFileType;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị biến tạm thời
                string name = txtName.Text.Trim();
                int dateType = Convert.ToInt32(cboDateType.SelectedValue);
                int unitType = Convert.ToInt32(cboUnitType.SelectedValue);
                DateTime dateStart = dpkDateStart.Value.Date;
                DateTime dateEnd = chkDateEnd.Checked ? dpkDateEnd.Value.Date : DateTime.MaxValue;
                string userModified = clsShare.sUserName;

                // Kiểm tra dữ liệu đầu vào
                if (name == "" || dateStart == null)
                {
                    clsShare.Message_Error("Các trường có dấu * không được phép để trống!");
                    return;
                }
                if (chkDateEnd.Checked)
                    if (dateEnd != null)
                        if (DateTime.Compare(dateEnd, dateStart) == -1)
                        {
                            clsShare.Message_Error("Ngày kết thúc không được nhỏ hơn Ngày áp dụng!");
                            return;
                        }

                //Kiểm tra danh sách văn bản
                DataTable dtFilePro = (DataTable)cfgFileProfile.DataSource;
                if (dtFilePro.Select("FileTypeID is NULL or DateStart is NULL", "", DataViewRowState.CurrentRows).Count() > 0)
                {
                    clsShare.Message_Error("Danh sách văn bản thiếu dữ liệu!");
                    return;
                }
                if (dtFilePro.Select("DateEnd is not NULL and DateEnd < DateStart", "", DataViewRowState.CurrentRows).Count() > 0)
                {
                    clsShare.Message_Error("Danh sách văn bản có ngày kết thúc nhỏ hơn ngày bắt đầu!");
                    return;
                }
                //Kiểm tra trùng giai đoạn áp dụng
                List<int> filetypeIDs = dtFilePro.Select("", "", DataViewRowState.CurrentRows).AsEnumerable().Select(s => s.Field<int>("FileTypeID")).Distinct().ToList();
                for (int i = 0; i < filetypeIDs.Count; i++)
                {
                    DataRow[] rows = dtFilePro.Select("FileTypeID=" + filetypeIDs[i].ToString(), "DateStart ASC");
                    for (int j = 1; j < rows.Count(); j++)
                    {
                        DateTime end = rows[j - 1]["DateEnd"] == DBNull.Value ? new DateTime(9999, 9, 9) : Convert.ToDateTime(rows[j - 1]["DateEnd"]);
                        if (Convert.ToDateTime(rows[j]["DateStart"]) < end)
                        {
                            clsShare.Message_Error("Danh sách văn bản có giai đoạn áp dụng trùng lặp!");
                            return;
                        }
                    }
                }
                
                // cập nhật vào cơ sở dữ liệu
                _bus.FL_ProfileType_InsertUpdate_LienKetVB(ProfileTypeId, name, unitType, dateType, dateStart, dateEnd, userModified, dtFilePro);

                clsShare.Message_Info("Cập nhật loại hồ sơ thành công!");
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkDateEnd_CheckedChanged(object sender, EventArgs e)
        {
            dpkDateEnd.Visible = chkDateEnd.Checked;
        }

        private void cfgFileProfile_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                cfgFileProfile.Rows[e.Row]["ProfileTypeID"] = ProfileTypeId;
                //Cập nhật ID
                switch (cfgFileProfile.Cols[e.Col].Name)
                {
                    case "FileType":
                        cfgFileProfile.Rows[e.Row]["FileTypeID"] = _cboEditorFileType.SelectedValue;
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
    }
}
