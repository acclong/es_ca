using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ES.CA_ManagementBUS;

namespace ES.CA_ManagementUI
{
    public partial class frmCauHinhCheDoHSM : Form
    {
        private BUSQuanTri _bus = new BUSQuanTri();

        public frmCauHinhCheDoHSM()
        {
            InitializeComponent();
        }

        private void frmCauHinhCheDoHSM_Load(object sender, EventArgs e)
        {
            try
            {
                GetConfig();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void GetConfig()
        {
            DataTable dtConfig = _bus.Q_CONFIG_SelectAll();

            // lây dư liệu cho device
            DataTable dt_value = _bus.HSM_Device_SelectAll_Full();
            DataColumn dc = new DataColumn("NameView", typeof(string));
            dt_value.Columns.Add(dc);

            foreach (DataRow item in dt_value.Rows)
            {
                item["NameView"] = string.Format("{0} ({1})", item["Name"], item["Serial"]);
            }

            cbbValue.DataSource = dt_value;
            cbbValue.DisplayMember = "NameView";
            cbbValue.ValueMember = "DeviceID";

            // đổ dữ liệu cho mode
            cbbMode.SelectedText = dtConfig.Rows[11]["Value"].ToString();
            cbbMode.Text = dtConfig.Rows[11]["Value"].ToString();

            // check chế độ WLD
            if (cbbMode.Text == "WLD")
            {
                cbbValue.Enabled = false;
                cbbValue.Text = "";
            }
            else
            {
                cbbValue.SelectedValue = dtConfig.Rows[12]["Value"];
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _bus.Q_CONFIG_Update(12, cbbMode.Text);
                if (cbbValue.Enabled)
                    _bus.Q_CONFIG_Update(13, cbbValue.SelectedValue.ToString());
                else
                    _bus.Q_CONFIG_Update(13, null);
                clsShare.Message_Info("Cập nhật giá trị thành công!");
                this.Close();
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

        private void cbbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbMode.SelectedIndex == 1)
            {
                cbbValue.Text = "";
                cbbValue.Enabled = false;
            }
            else
            {
                cbbValue.Enabled = true;
                cbbValue.SelectedIndex = 0;
            }
        }
    }
}
