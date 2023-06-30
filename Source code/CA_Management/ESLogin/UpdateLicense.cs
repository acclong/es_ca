using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace ESLogin
{
    public partial class UpdateLicense : Form
    {
        public bool bHasLicense = false;
        public string sLicenseStatus;
        public string sAppDataPath;
        public string sProgramID;

        public string sComID;
        public string sDeadline;
        public string sDonviTinh;
        public string sTimeCreate;
        public string sLoaiDonvi;
        public string sChuongTrinh;

        private int iCount = 0;
        private string sKey = "E-solutions";

        public UpdateLicense()
        {
            InitializeComponent();
        }

        private void UpdateLicense_Load(object sender, EventArgs e)
        {
            lbInfoLicense.Text = "Loading . . .";
            panel1.Visible = false;
            btnNext.Visible = false;
            timerLoading.Start();

            GetInfoFromLicenseFile();
            CheckLicense(true);
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            lbInfoLicense.Text = "Loading . . .";
            panel1.Visible = false;
            btnNext.Visible = false;
            timerLoading.Start();

            GetInfoFromLicenseFile();
            CheckLicense(false);

            timerLoading.Stop();
            timerLoading.Dispose();
            lbInfoLicense.Text = sLicenseStatus;

            if (bHasLicense) btnNext.Visible = true;
            else
                panel1.Visible = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.Filter = @"Tập tin (*.*)|*.*";
                SFD.Title = @"Save file config.xml đến nơi bạn muốn";
                SFD.FileName = "config.xml";
                SFD.RestoreDirectory = true;
                if (SFD.ShowDialog() == DialogResult.OK)
                {
                    FileInfo f = new FileInfo(sAppDataPath + "\\config.xml");
                    f.CopyTo(SFD.FileName, true);
                }
                SFD.Dispose();
            }
            catch
            {
                MessageBox.Show("Chương trình không tìm thấy file config.xml", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            string sLicensePath = "";
            
            OpenFileDialog OFD = new OpenFileDialog
            {
                Filter = @"Tập tin (.lic)|*.lic",
                Multiselect = false,
                Title = @"Chọn tập tin"
            };

            if (OFD.ShowDialog() == DialogResult.Cancel) 
                return;
            else
                sLicensePath = OFD.FileName;
            OFD.Dispose();

            if (AddLic(sLicensePath))
            {
                btnCheck_Click(sender, e);
            }
        }

        private void timerLoading_Tick(object sender, EventArgs e)
        {
            iCount++;
            string[] mLoading = lbInfoLicense.Text.Split(' ');

            if (mLoading[0] == "Loading")
                lbInfoLicense.Text += " .";
            if (mLoading.Length == 5)
                lbInfoLicense.Text = mLoading[0];

            if (iCount > 20 && bHasLicense == true)
            {
                timerLoading.Stop();
                timerLoading.Dispose();
                this.Close();
            }
            if (iCount > 20 && bHasLicense == false)
            {
                timerLoading.Stop();
                timerLoading.Dispose();
                lbInfoLicense.Text = sLicenseStatus;
                panel1.Visible = true;
            }
        }

        private bool AddLic(string pathlic)
        {
            string[] parts = pathlic.Split('\\');
            string name = parts[parts.Length - 1];

            string ext = Path.GetExtension(pathlic);
            if (ext != ".lic")
            {
                MessageBox.Show("File license không đúng định dạng .lic", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                File.Copy(pathlic, sAppDataPath + "\\license.lic", true);
                FileSystem.DeleteFile(pathlic, UIOption.OnlyErrorDialogs, RecycleOption.DeletePermanently);
                return true;
            }
        }

        private void GetInfoFromLicenseFile()
        {
            if (File.Exists(sAppDataPath + "\\license.lic"))
                try
                {
                    string fileout = sAppDataPath + "\\temp2.txt";
                    string filein = sAppDataPath + "\\license.lic";
                    EncryptionFile.DecryptFile(filein, fileout, sKey);

                    StreamReader sr1 = new StreamReader(fileout);
                    string fromFile = sr1.ReadLine();
                    sr1.Dispose();

                    string gm = StringCryptor.DecryptString(fromFile, sKey);
                    string[] data = gm.Split('#');
                    sComID = data[4];
                    sDeadline = data[1];
                    sDonviTinh = data[2];
                    sTimeCreate = data[0];
                    sLoaiDonvi = data[3];
                    sChuongTrinh = data[5].Split('%')[2];

                    FileSystem.DeleteFile(fileout, UIOption.OnlyErrorDialogs, RecycleOption.DeletePermanently);
                }
                catch
                {
                    FileSystem.DeleteFile(sAppDataPath + "\\temp2.txt", UIOption.OnlyErrorDialogs, RecycleOption.DeletePermanently);
                    bHasLicense = false;
                    sLicenseStatus = "License không hợp lệ";
                }
            else
            {
                bHasLicense = false;
                sLicenseStatus = "Phần mềm chưa được cấp license";
            }
        }

        private void CheckLicense(bool change)
        {
            try
            {
                if (sChuongTrinh != sProgramID)
                {
                    bHasLicense = false;
                    sLicenseStatus = "License không hợp lệ";
                }
                else
                {
                    if (sComID != CheckID.GetSystemInfo())
                    {
                        bHasLicense = false;
                        sLicenseStatus = "License không hợp lệ";
                    }
                    else
                    {
                        if (Convert.ToInt16(sLoaiDonvi) == 2)
                        {
                            bHasLicense = true;
                            sLicenseStatus = "Phần mềm được sử dụng vô thời hạn";
                        }
                        else if (Convert.ToInt16(sLoaiDonvi) == 0)
                        {
                            DateTime cDeadline;
                            DateTime today = DateTime.Now.Date.AddHours(23);
                            switch (sDonviTinh)
                            {
                                case "ngày":
                                    cDeadline = Convert.ToDateTime(sTimeCreate).AddDays(Convert.ToInt32(sDeadline));
                                    break;
                                case "tháng":
                                    cDeadline = Convert.ToDateTime(sTimeCreate).AddMonths(Convert.ToInt32(sDeadline));
                                    break;
                                case "năm":
                                    cDeadline = Convert.ToDateTime(sTimeCreate).AddYears(Convert.ToInt32(sDeadline));
                                    break;
                                default:
                                    MessageBox.Show("File license không đúng định dạng", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                            }
                            if (DateTime.Compare(today, cDeadline) > 0)
                            {
                                bHasLicense = false;
                                sLicenseStatus = "Phần mềm đã hết thời hạn sử dụng";
                            }
                            else
                            {
                                bHasLicense = true;
                                sLicenseStatus = "Phần mềm được sử dụng đến ngày " + cDeadline.ToString("dd/MM/yyyy");
                            }
                        }
                        else
                        {
                            if (change)
                            {
                                int timeth = Convert.ToInt32(sDeadline) - 1;
                                if (timeth < 0)
                                {
                                    bHasLicense = false;
                                    sLicenseStatus = "Phần mềm hết thời hạn sử dụng";
                                }
                                else
                                {
                                    bHasLicense = true;
                                    sLicenseStatus = "Phần mềm được sử dụng thêm " + Convert.ToString(timeth) + " lần";
                                    WriteLanChayToFile(sTimeCreate, Convert.ToString(timeth), sDonviTinh, sLoaiDonvi, sComID, sChuongTrinh);
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(sDeadline) < 0)
                                {
                                    bHasLicense = false;
                                    sLicenseStatus = "Phần mềm hết thời hạn sử dụng";
                                }
                                else
                                {
                                    bHasLicense = true;
                                    sLicenseStatus = "Phần mềm được sử dụng thêm " + sDeadline + " lần";
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                bHasLicense = false;
                sLicenseStatus = "License không hợp lệ";
            }
        }

        private void WriteLanChayToFile(string datecreate, string deadline, string donvi, string loaidonvi, string idComputer, string ChuongTrinh)
        {
            string mh = datecreate + "#" + deadline + "#" + donvi + "#" + loaidonvi + "#" + idComputer + "#" + ChuongTrinh;
            string toFile = StringCryptor.EncryptString(mh, sKey);
            StreamWriter sw = new StreamWriter(sAppDataPath + "\\temp.txt");
            sw.WriteLine(toFile);
            sw.WriteLine(StringCryptor.EncryptString("Công ty cổ phần và giải pháp năng lượng Việt Nam hân hạnh phục vụ.", sKey));
            sw.WriteLine(StringCryptor.EncryptString("Mọi thắc mắc liên hệ với chúng tôi.", sKey));
            sw.Flush();
            sw.Dispose(); ;

            string filein = sAppDataPath + "\\temp.txt";
            string fileout = sAppDataPath + "\\license.lic";
            EncryptionFile.EncryptFile(filein, fileout, sKey);
            FileSystem.DeleteFile(filein, UIOption.OnlyErrorDialogs, RecycleOption.DeletePermanently);
        }
    }
}
