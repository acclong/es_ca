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
    public partial class frmXemLogStatusServer : Form
    {
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _daSource = null;
        
        private int _id;
        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        public frmXemLogStatusServer()
        {
            InitializeComponent();
        }
        
        public frmXemLogStatusServer(int id)
        {
            InitializeComponent();
            _id = id;
        }
        
        private void frmXemLogStatusServer_Load(object sender, EventArgs e)
        {
            LoadData();
            InitRgvCertificates();
        }
        
        private void LoadData()
        {
            _daSource = _bus.FL_LogFileStatus_SelectByFileID(Id);
        }
       
        private void InitRgvCertificates()
        {
            try
            {
                cfgVanBan1.Cols.Fixed = 0;

                cfgVanBan1.DataSource = _daSource;
                
                //Thêm trường STT và ẩn cột ID
                string[] arrName = new string[] { "STT", "ID_Log", "FileID", "FileNumber", "StatusName", "Reason", "DateModified", "UserModified", "Status" };
                string[] arrHeader = new string[] { "STT", "ID Log", "ID File", "Số Serial", "Trạng thái", "Lý do", "Ngày sửa", "Người sửa", "Status" };
                for (int i = 0; i < arrHeader.Length; i++)
                {
                    // tên và header
                    cfgVanBan1.Cols[i].Name = arrName[i];
                    cfgVanBan1.Cols[i].Caption = arrHeader[i];
                    cfgVanBan1.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                    cfgVanBan1.Cols[i].AllowEditing = true;
                    cfgVanBan1.Cols[i].Style.TextAlign = TextAlignEnum.CenterCenter;
                    
                    //an hien truong du lieu
                    if (i == 1 || i ==2 || i== 3 || i == 8)
                    {
                        cfgVanBan1.Cols[i].Visible = false;
                    }

                    // căn lề
                    if (i == 0 || i == 4 || i == 6)
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
               
                //độ dài cột
                cfgVanBan1.Cols[0].Width = 50;
                cfgVanBan1.Cols[3].Width = 200;
                cfgVanBan1.Cols[4].Width = 110;
                cfgVanBan1.Cols[5].Width = 250;
                cfgVanBan1.Cols[6].Width = 120;
                cfgVanBan1.Cols[7].Width = 120;

                //Định đạng cho time
                cfgVanBan1.Cols["DateModified"].Format = "dd/MM/yyyy HH:mm:ss";
                
                //Căn giữa hàng đầu
                cfgVanBan1.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
            }
            catch (Exception ex)
            { }
        }
    }
}
