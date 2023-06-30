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
    public partial class frmThemSuaUnitHeThong : Form
    {
        #region Var
        
        BUSQuanTri _bus = new BUSQuanTri();
        int _progID = -1;
        int _unitID = -1;
        int _id_UnitProg = -1;

        public int ID_UnitProg
        {
            get { return _id_UnitProg; }
            set { _id_UnitProg = value; }
        }

        #endregion
        public frmThemSuaUnitHeThong()
        {
            InitializeComponent();
        }

        private void frmThemSuaUnitHeThong_Load(object sender, EventArgs e)
        {
            InitCboTrangThai();

            if (_id_UnitProg != -1)
            {
                DataTable dt = _bus.CA_UnitProgram_SelectBy_IDUnitProg(_id_UnitProg);
                if (dt.Rows.Count > 0)
                {
                    this.Text = "Sửa liên kết đơn vị -  hệ thống";
                    DataRow dr = dt.Rows[0];
                    txtID_UnitProgram.Text = dr["ID_UnitProgram"].ToString();
                    txtProg.Text = dr["ProgName"].ToString();
                    txtUnit.Text = dr["UnitName"].ToString();
                    cboStatus.SelectedValue = Convert.ToInt32(dr["Status"]);

                    _unitID = Convert.ToInt32(dr["UnitID"]);
                    _progID = Convert.ToInt32(dr["ProgID"]);
                }
                else
                {

                }

                
            }
            else
            {
                this.Text = "Thêm liên kết đơn vị -  hệ thống";
            }
            this.ActiveControl = btnSeachProg;
        }

        #region Init
        
        private void InitCboTrangThai()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));

            DataRow dr = dt.NewRow();
            dr["Id"] = 1;
            dr["Name"] = "Có check CA";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = 0;
            dr["Name"] = "Không check CA";
            dt.Rows.Add(dr);

            cboStatus.DataSource = dt;
            cboStatus.SelectedIndex = 0;
            cboStatus.DisplayMember = "Name";
            cboStatus.ValueMember = "Id";
        }

        #endregion

        #region Controls
        
        private void btnSeachProg_Click(object sender, EventArgs e)
        {
            try
            {
                frmLocHeThongTichHop frm = new frmLocHeThongTichHop(this);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnSeachUnit_Click(object sender, EventArgs e)
        {
            try
            {
                frmLocDonViCA frm = new frmLocDonViCA(this);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_progID == -1 || _unitID == -1)
                {
                    clsShare.Message_Error("Các trường có dấu * không được phép để trống!");
                    this.ActiveControl = btnSeachProg;
                    return;
                }
                if (_bus.CA_UnitProgram_InsertUpdate(_id_UnitProg, _progID, _unitID, Convert.ToInt32(cboStatus.SelectedValue), clsShare.sUserName))
                {
                    clsShare.Message_Info("Cập nhật hệ thống thành công!");
                }
                else
                {
                    clsShare.Message_Error("Cập nhật thất bại!");
                }

                if (_id_UnitProg == -1)
                {
                    cboStatus.SelectedIndex = 0;
                    txtID_UnitProgram.Clear();
                    txtProg.Clear();
                    txtUnit.Clear();
                }
                this.ActiveControl = btnSeachProg;

            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Data
        
        public void CapNhatDuLieuHeThong(string proName, int proID)
        {
            txtProg.Text = proName;
            _progID = proID;
        }

        public void CapNhatDuLieuUnit(string unitname, int unitID)
        {
            txtUnit.Text = unitname;
            _unitID = unitID;
        }

        #endregion
    }
}
