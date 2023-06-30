using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ES.CA_ManagementBUS;

namespace ES.CA_ManagementUI
{
    public partial class ucDemoUI : UserControl
    {
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable dtCerts = new DataTable();

        public ucDemoUI()
        {
            InitializeComponent();
        }

        private void ucDanhSachChungThuSo_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void InitRgvCertificates()
        {

        }

        private void LoadData()
        {
            
        }
    }
}
