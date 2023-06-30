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
    public partial class frmThemSuaLoaiFileLienQuan : Form
    {
        private int _relationTypeId;
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtRelationType = new DataTable();

        public int RelationTypeId
        {
            set { _relationTypeId = value; }
            get { return _relationTypeId; }
        }

        public frmThemSuaLoaiFileLienQuan()
        {
            InitializeComponent();
        }

        private void frmThemSuaLoaiFileLienQuan_Load(object sender, EventArgs e)
        {
            try
            {
                txtName.Select();
                if (RelationTypeId != -1)
                {
                    //txtName.ReadOnly = true;
                    // lấy dữ liệu từ database
                    _dtRelationType = _bus.FL_RelationType_SelectByRelationTypeID(RelationTypeId);
                    // truyền vào các control
                    txtIdRelationType.Text = _dtRelationType.Rows[0]["RelationTypeID"].ToString();
                    txtName.Text = _dtRelationType.Rows[0]["Name"].ToString();
                    dpkDateStart.Value = Convert.ToDateTime(_dtRelationType.Rows[0]["DateStart"]);
                    if (_dtRelationType.Rows[0]["DateEnd"] == DBNull.Value)
                        chkDateEnd.Checked = false;
                    else
                    {
                        dpkDateEnd.Value = Convert.ToDateTime(_dtRelationType.Rows[0]["DateEnd"]);
                        chkDateEnd.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
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
                string name = txtName.Text.Trim();
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

                // cập nhật vào cơ sở dữ liệu
                _bus.FL_RelationType_InsertUpdate(RelationTypeId, name, dateStart, dateEnd, userModified);

                clsShare.Message_Info("Cập nhật loại văn bản liên quan thành công!");
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
