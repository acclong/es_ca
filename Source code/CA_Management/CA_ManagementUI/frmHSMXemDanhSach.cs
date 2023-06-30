using C1.Win.C1FlexGrid;
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
    public partial class frmHSMXemDanhSach : Form
    {
        DataTable _dt = new DataTable();

        public DataTable DT
        {
            set { _dt = value; }
        }

        public frmHSMXemDanhSach()
        {
            InitializeComponent();
        }

        private void frmXemHSM_Load(object sender, EventArgs e)
        {
            cfgData.DataSource = _dt;
            InitGrid();
        }

        private void InitGrid()
        {
            cfgData.ExtendLastCol = true;
            cfgData.Cols.Fixed = 1;
            cfgData.Cols[0].Width = 25;

            if (_dt.TableName == "DeviceList")
            {
                string[] arrName = { "DeviceID", "Label", "SerialNumber", "Manufacturer", "Model", "TotalMemory", "FreeMemory" };
                string[] arrHeader = { "DeviceID", "Label", "Serial Number", "Manufacturer", "Model", "Total Memory", "Free Memory" };

                for (int i = 0; i < arrName.Count(); i++)
                {
                    // tên cột và header
                    cfgData.Cols[i + 1].Name = arrName[i];
                    cfgData.Cols[i + 1].Caption = arrHeader[i];
                    cfgData.Cols[i + 1].TextAlignFixed = TextAlignEnum.CenterCenter;

                    if (i == 0)
                    {
                        cfgData.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                        cfgData.Cols[i + 1].Width = 60;
                    }
                    else if (i == 1)
                        cfgData.Cols[i + 1].Width = 150;
                }
            }
            else if (_dt.TableName == "SlotList")
            {
                string[] arrName = { "SlotIndex", "Label", "SerialNumber", "TokenInitialised", "ObjectCount" };
                string[] arrHeader = { "Slot Index", "Token Label", "Serial Number", "Token Initialised", "Public Object Count" };

                for (int i = 0; i < arrName.Count(); i++)
                {
                    // tên cột và header
                    cfgData.Cols[i + 1].Name = arrName[i];
                    cfgData.Cols[i + 1].Caption = arrHeader[i];
                    cfgData.Cols[i + 1].TextAlignFixed = TextAlignEnum.CenterCenter;

                    if (i == 0)
                    {
                        cfgData.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                        cfgData.Cols[i + 1].Width = 70;
                    }
                    else if (i == 1)
                        cfgData.Cols[i + 1].Width = 150;
                }
            }
        }
    }
}
