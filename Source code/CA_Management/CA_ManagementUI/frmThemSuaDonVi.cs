using ES.CA_ManagementBUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace ES.CA_ManagementUI
{
    public partial class frmThemSuaDonVi : Form
    {
        #region Var

        private int _iUnitId;
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtUnit = new DataTable();
        DataTable _dtUnitType = new DataTable();

        public int UnitID
        {
            set { _iUnitId = value; }
            get { return _iUnitId; }
        }

        #endregion

        public frmThemSuaDonVi()
        {
            InitializeComponent();
        }

        private void frmThemSuaDonVi_Load(object sender, EventArgs e)
        {
            try
            {
                //Ẩn List đơn vị
                //grbListUnit.Visible = false;
                //this.Width -= grbListUnit.Width;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;

                //Khởi tạo list view
                if (UnitID == -1)
                {
                    //rlvUnit.AllowArbitraryItemHeight = true;
                    //rlvUnit.AllowArbitraryItemWidth = true;
                    //rlvUnit.VisualItemCreating += rlvUnit_VisualItemCreating;
                    InitdrpUnitGroup();

                }

                InitcboStatus();
                InitcboTypeUnit();
                InitcboParent();
                InitcboMien();

                if (UnitID != -1)
                {


                    // lấy dữ liệu từ database
                    _dtUnit = _bus.CA_Unit_SelectByUnitID(UnitID);

                    txtMaDv.Text = _dtUnit.Rows[0]["MaDV"].ToString();
                    txtUnitID.Text = _dtUnit.Rows[0]["UnitID"].ToString();
                    txtUnitName.Text = _dtUnit.Rows[0]["Name"].ToString();
                    txtNotation.Text = _dtUnit.Rows[0]["Notation"].ToString();

                    cboStatus.SelectedIndex = Convert.ToInt32(_dtUnit.Rows[0]["Status"]);

                    if (_dtUnit.Rows[0]["ValidFrom"] == DBNull.Value)
                        dpkValidFrom.Value = DateTime.Now;
                    else
                        dpkValidFrom.Value = Convert.ToDateTime(_dtUnit.Rows[0]["ValidFrom"].ToString());
                    if (_dtUnit.Rows[0]["ValidTo"] == DBNull.Value)
                        chkVaildTo.Checked = false;
                    else
                    {
                        dpkValidTo.Value = Convert.ToDateTime(_dtUnit.Rows[0]["ValidTo"].ToString());
                        chkVaildTo.Checked = true;
                    }

                    cboTypeUnit.SelectedValue = _dtUnit.Rows[0]["UnitTypeID"];

                    if (_dtUnit.Rows[0]["ParentID"] == DBNull.Value)
                        cboParent.SelectedValue = -1;
                    else
                        cboParent.SelectedValue = _dtUnit.Rows[0]["ParentID"];

                    ////Ẩn List đơn vị
                    //grbListUnit.Visible = false;
                    //this.Width -= grbListUnit.Width;
                    //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                    btnFind.Visible = false;
                    cboTypeUnit.Enabled = false;

                    this.Text = "Sửa đơn vị";
                    this.ActiveControl = txtNotation;
                }
                else
                {
                    this.Text = "Thêm đơn vị";
                    cboStatus.SelectedIndex = 1;
                    cboTypeUnit.SelectedIndex = 0;
                    cboParent.SelectedIndex = 0;

                    this.ActiveControl = cboTypeUnit;

                    //tbUnitFilter.TextChanged += tbUnitFilter_TextChanged;
                    //rlvUnit.SelectedIndexChanged += rlvUnit_SelectedIndexChanged;
                }

            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region Init

        private void InitcboStatus()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));

            DataRow dr = dt.NewRow();
            dr["Id"] = 0;
            dr["Name"] = "Không hiệu lực";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = 1;
            dr["Name"] = "Hiệu lực";
            dt.Rows.Add(dr);

            cboStatus.DataSource = dt;
            cboStatus.DisplayMember = "Name";
            cboStatus.ValueMember = "Id";

            cboStatus.SelectedIndex = 1;
        }

        private void InitcboTypeUnit()
        {
            // lấy datatable unit từ db
            _dtUnitType = _bus.CA_UnitType_SelectAll();
            // gán giá trị vào cho cbo
            cboTypeUnit.DisplayMember = "Name";
            cboTypeUnit.ValueMember = "UnitTypeID";
            cboTypeUnit.DataSource = _dtUnitType;
        }

        private void InitcboParent()
        {
            // lấy datatable unit từ db
            _dtUnit = _bus.CA_Unit_SelectUnitName();
            // thêm dòng giá trị null
            DataRow dr = _dtUnit.NewRow();
            dr["UnitID"] = -1;
            dr["Name"] = "[Không có]";
            _dtUnit.Rows.InsertAt(dr, 0);
            // gán giá trị vào cho cbo
            cboParent.DisplayMember = "Name";
            cboParent.ValueMember = "UnitID";
            cboParent.DataSource = _dtUnit.Copy();
        }

        private void InitcboMien()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));

            DataRow dr = dt.NewRow();
            dr["Id"] = -1;
            dr["Name"] = "";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = 1;
            dr["Name"] = "Miền Bắc";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = 2;
            dr["Name"] = "Miền Nam";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = 3;
            dr["Name"] = "Miền Trung";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = 150;
            dr["Name"] = "Cả nước";
            dt.Rows.Add(dr);

            cboMien.DataSource = dt;
            cboMien.DisplayMember = "Name";
            cboMien.ValueMember = "Id";

            cboMien.SelectedIndex = 1;
        }

        private void InitdrpUnitGroup()
        {
            string[] array = { "[Không]" };
            //drpUnitGroup.Items.AddRange(array);
            //drpUnitGroup.SelectedIndex = 0;
        }

        #endregion

        #region Controls

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị biến tạm thời
                string sMaDv = txtMaDv.Text.Trim();
                string sName = txtUnitName.Text.Trim();
                string sNotation = txtNotation.Text.Trim().ToUpper();
                string sTenTat = txtTenTat.Text.Trim();
                int status = Convert.ToInt32(cboStatus.SelectedValue);
                int mien = Convert.ToInt32(cboMien.SelectedValue);
                DateTime validFrom = dpkValidFrom.Value;
                DateTime validTo = chkVaildTo.Checked ? dpkValidTo.Value : DateTime.MaxValue;
                if (status == 0)
                {
                    validFrom = DateTime.MaxValue;
                    validTo = DateTime.MaxValue;
                }
                int unitTypeID = Convert.ToInt32(cboTypeUnit.SelectedValue);
                int parentID = Convert.ToInt32(cboParent.SelectedValue) == -1 ?
                    int.MinValue : Convert.ToInt32(cboParent.SelectedValue);
                string UserModified = clsShare.sUserName;

                // Kiểm tra dữ liệu đầu vào
                if (sName == "" || validFrom == null || sNotation == "" || sMaDv == "" || sTenTat == "")
                {
                    clsShare.Message_Error("Các trường có dấu * không được phép để trống!");
                    if (UnitID != -1)
                    {
                        this.ActiveControl = txtNotation;
                    }
                    else
                    {
                        this.ActiveControl = cboTypeUnit;
                    }
                    return;
                }
                if (chkVaildTo.Checked)
                    if (validTo != null)
                        if (DateTime.Compare(validTo, validFrom) == -1)
                        {
                            clsShare.Message_Error("Ngày hết hiệu lực không được nhỏ hơn Ngày có hiệu lực!");
                            if (UnitID != -1)
                            {
                                this.ActiveControl = txtNotation;
                            }
                            else
                            {
                                this.ActiveControl = cboTypeUnit;
                            }
                            return;
                        }

                // Xác nhận thông tin thu hồi hiệu lực user
                if (status == 0)
                    if (clsShare.Message_WarningYN("Bạn có chắc chắn lưu thông tin KHÔNG HIỆU LỰC cho Đơn vị này không?") == false)
                    {
                        if (UnitID != -1)
                        {
                            this.ActiveControl = txtNotation;
                        }
                        else
                        {
                            this.ActiveControl = cboTypeUnit;
                        }
                        return;
                    }

                // Kiểm tra Ký hiệu
                //Edited by Toantk on 23/4/2015
                //Chuyển hàm lấy mã để kiểm tra vào Business
                int unitIDinDB = _bus.CA_Unit_HasNotation(sNotation);
                unitIDinDB = _bus.CA_Unit_Has_Notation_UnitTypeID(sNotation, unitTypeID);
                if (UnitID != -1 && unitIDinDB != 0)
                {
                    if (UnitID != unitIDinDB)
                    {
                        clsShare.Message_Error("Ký hiệu đơn vị đã tồn tại. Vui lòng nhập lại Ký hiệu đơn vị!");
                        if (UnitID != -1)
                        {
                            this.ActiveControl = txtNotation;
                        }
                        else
                        {
                            this.ActiveControl = cboTypeUnit;
                        }
                        return;
                    }
                }
                else if (UnitID == -1 && unitIDinDB != 0)
                {
                    clsShare.Message_Error("Ký hiệu đơn vị đã tồn tại. Vui lòng nhập lại Ký hiệu đơn vị!");
                    if (UnitID != -1)
                    {
                        this.ActiveControl = txtNotation;
                    }
                    else
                    {
                        this.ActiveControl = cboTypeUnit;
                    }
                    return;
                }

                // Kiểm tra Mã đơn vị
                //Edited by Toantk on 23/4/2015
                //Chuyển hàm lấy mã để kiểm tra vào Business
                unitIDinDB = _bus.CA_Unit_HasMaDV(sMaDv);
                //if (UnitID != -1 && unitIDinDB != 0)
                //{
                //    if (UnitID != unitIDinDB)
                //    {
                //        clsShare.Message_Error("Mã đơn vị đã tồn tại. Vui lòng chọn đơn vị khác!");
                //        return;
                //    }
                //}
                //else 
                if (UnitID == -1 && unitIDinDB != 0)
                {
                    clsShare.Message_Error("Mã đơn vị đã tồn tại. Vui lòng chọn đơn vị khác!");
                    if (UnitID != -1)
                    {
                        this.ActiveControl = txtNotation;
                    }
                    else
                    {
                        this.ActiveControl = cboTypeUnit;
                    }
                    return;
                }

                // cập nhật vào cơ sở dữ liệu
                _bus.CA_Unit_InsertUpdate(UnitID, sMaDv, sName, sNotation, status, validFrom, validTo, unitTypeID, parentID,sTenTat, mien, UserModified);

                clsShare.Message_Info("Cập nhật đơn vị thành công!");
                if (UnitID == -1)
                {
                    this.ActiveControl = cboTypeUnit;
                    cboTypeUnit.SelectedIndex = 0;
                    txtMaDv.Clear();
                    txtNotation.Clear();
                    if (cboParent.Items.Count > 0)
                    {
                        cboParent.SelectedIndex = 0;
                    }
                    txtUnitName.Clear();
                    cboStatus.SelectedIndex = 0;
                    chkVaildTo.Checked = false;
                    dpkValidTo.Visible = false;
                    dpkValidTo.Value = DateTime.Now;
                    dpkValidFrom.Value = DateTime.Now;
                }
                else
                {
                    this.ActiveControl = txtNotation;
                }
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

        private void chkVaildTo_CheckedChanged(object sender, EventArgs e)
        {
            dpkValidTo.Visible = chkVaildTo.Checked;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            frmLocDonVi frm = new frmLocDonVi(this);
            frm.TypeUnit = Convert.ToInt16(cboTypeUnit.SelectedIndex);
            frm.ShowDialog();

            //txtMaDv.Text = frm.UnitMa.ToString();
            //txtUnitName.Text = frm.UnitName;
            //txtNotation.Text = frm.UnitNotation;

        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStatus.Items.Count > 0)
                if (cboStatus.SelectedIndex == 0)
                {
                    dpkValidFrom.Enabled = false;
                    dpkValidTo.Enabled = false;
                    chkVaildTo.Enabled = false;
                }
                else
                {
                    dpkValidFrom.Enabled = true;
                    dpkValidTo.Enabled = true;
                    chkVaildTo.Enabled = true;
                }
        }

        #endregion

        #region Event

        private void cboTypeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Lấy danh sách đơn vị từ CSDL_Chung
                string sp_Select = _dtUnitType.Rows[cboTypeUnit.SelectedIndex]["SP_Select"].ToString();

                //rlvUnit.DataSource = _bus.S_DonVi_SelectAll(sp_Select);
                //rlvUnit.DisplayMember = "Name";
                //rlvUnit.ValueMember = "MaDV";
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        internal void capNhatDuLieu(string madv, string unitname, string notation)
        {
            txtMaDv.Text = madv;
            txtUnitName.Text = unitname;
            txtNotation.Text = notation;
        }

        private void frmThemSuaDonVi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnExit_Click(null, null);
            }
        }

        #endregion

    }
}
