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
    public partial class frmThemSuaLoaiVanBan : Form
    {
        private int _fileTypeId;
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtFileType = new DataTable();

        public int FileTypeId
        {
            set { _fileTypeId = value; }
            get { return _fileTypeId; }
        }

        public frmThemSuaLoaiVanBan()
        {
            InitializeComponent();
        }

        private void frmThemSuaLoaiVanBan_Load(object sender, EventArgs e)
        {
            try
            {
                txtIdFileType.Select();
                InitCboUnitType();
                InitCboDateType();
                if (FileTypeId != -1)
                {
                    txtName.Select();
                    // lấy dữ liệu từ database
                    _dtFileType = _bus.FL_FileType_SelectByFileTypeID(FileTypeId);
                    // truyền vào các control
                    txtIdFileType.Text = _dtFileType.Rows[0]["FileTypeID"].ToString();
                    txtName.Text = _dtFileType.Rows[0]["Name"].ToString();
                    txtNotation.Text = _dtFileType.Rows[0]["Notation"].ToString();
                    if (_dtFileType.Rows[0]["DateType"] != DBNull.Value)
                        cboDateType.SelectedValue = Convert.ToInt32(_dtFileType.Rows[0]["DateType"]);
                    else
                        cboDateType.SelectedIndex = 0;
                    if (_dtFileType.Rows[0]["UnitType"] != DBNull.Value)
                        cboUnitType.SelectedValue = Convert.ToInt32(_dtFileType.Rows[0]["UnitType"]);
                    else
                        cboUnitType.SelectedIndex = 0;
                    dpkDateStart.Value = Convert.ToDateTime(_dtFileType.Rows[0]["DateStart"]);
                    if (_dtFileType.Rows[0]["DateEnd"] == DBNull.Value)
                        chkDateEnd.Checked = false;
                    else
                    {
                        dpkDateEnd.Value = Convert.ToDateTime(_dtFileType.Rows[0]["DateEnd"]);
                        chkDateEnd.Checked = true;
                    }

                    //Edited by Toantk on 21/5/2015
                    //Nếu cập nhật thì không cho sửa ID
                    txtIdFileType.ReadOnly = true;
                }
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

        private void chkDateEnd_CheckedChanged(object sender, EventArgs e)
        {
            dpkDateEnd.Visible = chkDateEnd.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị biến tạm thời
                int fileTypeID;
                string name = txtName.Text.Trim();
                string notation = txtNotation.Text.Trim();
                int dateType = Convert.ToInt32(cboDateType.SelectedValue);
                int unitType = Convert.ToInt32(cboUnitType.SelectedValue);
                DateTime dateStart = dpkDateStart.Value.Date;
                DateTime dateEnd = chkDateEnd.Checked ? dpkDateEnd.Value.Date : DateTime.MaxValue;
                string userModified = clsShare.sUserName;

                // Kiểm tra dữ liệu đầu vào
                if (txtIdFileType.Text == "" || name == "" || dateStart == null || notation == "")
                {
                    clsShare.Message_Error("Các trường có dấu * không được phép để trống!");
                    return;
                }

                if (!Int32.TryParse(txtIdFileType.Text, out fileTypeID) || fileTypeID <= 0)
                {
                    clsShare.Message_Error("ID phải là một số nguyên lớn hơn 0. Vui lòng nhập lại ID!");
                    return;
                }

                if (chkDateEnd.Checked)
                    if (dateEnd != null)
                        if (DateTime.Compare(dateEnd, dateStart) == -1)
                        {
                            clsShare.Message_Error("Ngày kết thúc không được nhỏ hơn Ngày áp dụng!");
                            return;
                        }

                //Edited by Toantk on 21/5/2015
                //Nếu thêm mới thì phải Kiểm tra ID do bỏ tự tăng
                if (FileTypeId == -1 && _bus.FL_FileType_HasFileTypeID(fileTypeID))
                {
                    clsShare.Message_Error("ID này đã tồn tại. Vui lòng nhập lại ID!");
                    return;
                }
                else
                    FileTypeId = fileTypeID;

                // Kiểm tra Notation
                //Edited by Toantk on 23/4/2015
                //Chuyển hàm lấy mã để kiểm tra vào Business
                int fileTypeID2 = _bus.FL_FileType_HasNotation(notation);
                if (fileTypeID2 != 0 && fileTypeID != fileTypeID2)
                {
                    clsShare.Message_Error("Ký hiệu này đã tồn tại. Vui lòng nhập lại Ký hiệu!");
                    return;
                }

                // cập nhật vào cơ sở dữ liệu
                _bus.FL_FileType_InsertUpdate(FileTypeId, name, dateType, unitType, notation, dateStart, dateEnd, userModified);

                clsShare.Message_Info("Cập nhật loại văn bản thành công!");
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
    }
}
