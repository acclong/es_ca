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
    public partial class frmCauHinhTimeKy : Form
    {
        private BUSQuanTri _bus = new BUSQuanTri();

        public frmCauHinhTimeKy()
        {
            InitializeComponent();
        }

        private void GetConfig()
        {
            DataTable dtConfig = _bus.Q_CONFIG_SelectAll();
            nudNext.Value = Convert.ToInt32(dtConfig.Rows[3]["Value"]);
            nudSave.Value = Convert.ToInt32(dtConfig.Rows[4]["Value"]);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _bus.Q_CONFIG_Update(4, nudNext.Value.ToString());
                _bus.Q_CONFIG_Update(5, nudSave.Value.ToString());
                clsShare.Message_Info("Cập nhật dữ liệu thành công!");
                this.Close();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void frmCauHinhTimeKy_Load(object sender, EventArgs e)
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
    }
}
