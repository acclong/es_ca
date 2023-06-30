using ES.CA_ManagementBUS;
using Microsoft.Win32;
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
    public partial class frmCauHinhCRL : Form
    {
        BUSQuanTri _bus = new BUSQuanTri();
        private string _pathSave = "";
        private string _period = "";
        private string _pathConfig = "";

        public frmCauHinhCRL()
        {
            InitializeComponent();
        }

        private void frmCauHinhCRL_Load(object sender, EventArgs e)
        {
            try
            {
                _pathConfig = clsShare.sAppPath;
                GetConfig();
                txtPathSave.Text = _pathSave;
                txtPeriod.Text = _period;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog FBD = new FolderBrowserDialog();
                FBD.Description = "Chọn thư mục lưu file CRL";
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _pathSave = txtPathSave.Text;
                _period = txtPeriod.Text;
                UpdateConfig();
                clsShare.Message_Info("Lưu thành công!");
                this.Close();
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

        private bool ReadConfig(string pathFileXML)
        {
            System.Xml.XmlDocument docCfg = new System.Xml.XmlDocument();
            try
            {
                docCfg.Load(pathFileXML + "\\config.xml");
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Chương trình không đọc được file config!\n"+ex);
                return false;
            }
            _pathSave = docCfg.GetElementsByTagName("PathSave").Item(0).InnerText;
            _period = docCfg.GetElementsByTagName("Period").Item(0).InnerText;
            return true;
        }

        private bool SaveConfig(string pathFileXML)
        {
            System.Xml.XmlDocument docCfg = new System.Xml.XmlDocument();
            try
            {
                docCfg.Load(pathFileXML + "\\config.xml");
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Chương trình không đọc được file config!\n" + ex);
                return false;
            }
            docCfg.GetElementsByTagName("PathSave").Item(0).InnerText = _pathSave;
            docCfg.GetElementsByTagName("Period").Item(0).InnerText = _period;
            docCfg.Save(pathFileXML + "\\config.xml");
            return true;
        }

        private void GetConfig()
        {
            DataTable dtConfig = _bus.Q_CONFIG_SelectAll();
            _pathSave = dtConfig.Rows[0]["Value"].ToString();
            _period = dtConfig.Rows[1]["Value"].ToString();
        }

        private void UpdateConfig()
        {
            _bus.Q_CONFIG_Update(1, _pathSave);
            _bus.Q_CONFIG_Update(2, _period);
        }
    }
}
