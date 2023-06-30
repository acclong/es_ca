using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ES.CA_ManagementBUS;
using ESLogin;

namespace ES.CA_ManagementUI
{
    public partial class frmCauHinhPinDefault : Form
    {
        private BUSQuanTri _bus = new BUSQuanTri();

        public frmCauHinhPinDefault()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetConfig()
        {
            DataTable dtConfig = _bus.Q_CONFIG_SelectAll();

            txtPIN.Text =StringCryptor.DecryptString( dtConfig.Rows[5]["Value"].ToString());
        }

        private void frmCauHinhPinDefault_Load(object sender, EventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _bus.Q_CONFIG_Update(6, StringCryptor.EncryptString(txtPIN.Text));
                //_bus.Q_CONFIG_Update(6, txtPIN.Text);
                clsShare.Message_Info("Cập nhật dữ liệu thành công!");
                this.Close();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }
    }
}
