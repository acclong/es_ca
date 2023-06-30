using C1.Win.C1FlexGrid;
using ES.CA_ManagementBUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace ES.CA_ManagementUI
{
    public partial class frmXemLogKy : Form
    {
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _daSource = null;
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public frmXemLogKy()
        {
            InitializeComponent();
        }
        
        public frmXemLogKy(int id)
        {
            InitializeComponent();
            _id = id;
        }

        private void frmXemLogKy_Load(object sender, EventArgs e)
        {
            LoadData();
            InitRgvCertificates();
        }

        private void InitRgvCertificates()
        {
            try
            {
                cfgVanBan1.Cols.Fixed = 0;
                cfgVanBan1.DataSource = _daSource;

                //Thêm trường STT và ẩn cột ID
                string[] arrName = new string[] { "STT", "ID_Log", "FileID", "NameCN", "CertID", "Serial", "Action", 
                    "SignTime", "Verify", "SignCreator", "UserModified", "DateModified", "FileNumber" };
                string[] arrHeader = new string[] { "STT", "ID Log", "ID File", "Người ký", "CertID", "Số serial", "Thao tác", 
                    "Thời gian", "Verify", "SignCreator", "Người sửa", "Ngày sửa","Số văn bản" };
                for (int i = 0; i < arrName.Length; i++)
                {
                    // tên và header
                    cfgVanBan1.Cols[i].Name = arrName[i];
                    cfgVanBan1.Cols[i].Caption = arrHeader[i];
                    cfgVanBan1.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                    cfgVanBan1.Cols[i].AllowEditing = true;
                    cfgVanBan1.Cols[i].Style.TextAlign = TextAlignEnum.CenterCenter;
                    if (i == 1 || i == 2 || i == 4 || i == 12)
                    {
                        cfgVanBan1.Cols[i].Visible = false;
                    }
                    // căn lề
                    if (i == 0 || i == 7 || i == 8 || i == 11)
                    {
                        cfgVanBan1.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                        cfgVanBan1.Cols[i].AllowEditing = true;
                    }
                    else
                    {
                        cfgVanBan1.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                        cfgVanBan1.Cols[i].AllowEditing = false;
                    }
                }

                //can chieu dai
                cfgVanBan1.Cols[0].Width = 50;
                cfgVanBan1.Cols[3].Width = 150;
                cfgVanBan1.Cols[5].Width = 250;
                cfgVanBan1.Cols[6].Width = 75;
                cfgVanBan1.Cols[7].Width = 150;
                cfgVanBan1.Cols[8].Width = 100;
                cfgVanBan1.Cols[9].Width = 250;
                cfgVanBan1.Cols[10].Width = 100;
                cfgVanBan1.Cols[11].Width = 150;

                //dinh dang thoi gian
                cfgVanBan1.Cols["SignTime"].Format = "dd/MM/yyyy HH:mm:ss";
                cfgVanBan1.Cols["DateModified"].Format = "dd/MM/yyyy HH:mm:ss";

                //can giua hang dau
                cfgVanBan1.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
            }
            catch (Exception ex)
            { }

        }
        private void LoadData()
        {
            _daSource = _bus.FL_LogFileSignature_SelectByFileID(_id);

        }
    }
}
