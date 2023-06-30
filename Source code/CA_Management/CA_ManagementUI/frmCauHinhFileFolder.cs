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
    public partial class frmCauHinhFileFolder : Form
    {
        private BUSQuanTri _bus = new BUSQuanTri();

        public frmCauHinhFileFolder()
        {
            InitializeComponent();
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog FBD = new FolderBrowserDialog();
                FBD.Description = "Chọn thư mục lưu File";
                FBD.RootFolder = Environment.SpecialFolder.Desktop;
                if (FBD.ShowDialog() == DialogResult.Cancel)
                    return;
                else
                    txtPathSave.Text = FBD.SelectedPath;
                FBD.Dispose();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPathSave.Text == "")
                {
                    if (!clsShare.Message_QuestionYN("Bạn có chắc muốn để trống đường dẫn không?"))
                    {
                        return;
                    }
                }
                _bus.Q_CONFIG_Update(3, txtPathSave.Text);
                clsShare.Message_Info("Cập nhật thành công!");
                this.Close();
            }
            catch(Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void frmCauHinhFileFolder_Load(object sender, EventArgs e)
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
            txtPathSave.Text = dtConfig.Rows[2]["Value"].ToString();
        }
    }
}
