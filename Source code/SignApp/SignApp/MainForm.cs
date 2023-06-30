using C1.Win.C1FlexGrid;
using esDigitalSignature;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SignApp
{
    public partial class MainForm : Form
    {
        public string[] arrCol = new string[] { "Check", "Name", "SignBy", "Signtime", "Path", "Serial" };
        public string[] arrColName = new string[] { "", "Tên File", "Người ký", "Thời gian ký", "Đường dẫn", "Serial chứng thư" };
        public int[] arrWidth = new int[] { 25, 250, 200, 150, 100, 0 };
        public bool isOpen = true;
        public DataRow[] drArray;
        public DataTable dtFile = new DataTable();
        public List<string> lstFile = new List<string>();
        public List<string> lstFileSignSuccess = new List<string>();
        public List<string> lstFileSignNoSuccess = new List<string>();
        public X509Certificate2 cert = new X509Certificate2();

        public MainForm()
        {
            InitializeComponent();
        }

        public void CreatDataTable()
        {
            dtFile = null;
            dtFile = new DataTable();
            for (int i = 0; i < arrCol.Length; i++)
            {
                dtFile.Columns.Add(arrCol[i]);
            }
        }

        public void LoadGridView()
        {
            cfgFile.DataSource = dtFile;
            for (int i = 0; i < arrCol.Length; i++)
            {
                cfgFile.Cols[i].Name = arrCol[i];
                cfgFile.Cols[i].Caption = arrColName[i];
                cfgFile.Cols[i].Width = arrWidth[i];
                cfgFile.Cols[i].AllowEditing = false;
            }
            cfgFile.Cols[0].AllowEditing = true;
            cfgFile.Cols[0].DataType = typeof(bool);
            cfgFile.AllowMerging = AllowMergingEnum.Custom;
            string strlast = cfgFile.Rows[1]["Name"].ToString();
            int ilast = 1;
            int iCount = 2;
            while (iCount < cfgFile.Rows.Count)
            {
                if (cfgFile.Rows[iCount]["Name"].ToString() == strlast)
                {
                    cfgFile.MergedRanges.Add(cfgFile.GetCellRange(ilast, 0, iCount, 0));
                    cfgFile.MergedRanges.Add(cfgFile.GetCellRange(ilast, 1, iCount, 1));
                }
                else
                {
                    ilast = iCount;
                    strlast = cfgFile.Rows[iCount]["Name"].ToString();
                }
                iCount++;
            }
        }

        public void LoadDataGrid()
        {
            for (int i = 0; i < lstFile.Count; i++)
            {
                List<string> lstSignBy = new List<string>();
                List<DateTime> lstSignTime = new List<DateTime>();
                List<string> lstSerial = new List<string>();
                LoadOnlyFile(lstFile[i], ref lstSignBy, ref lstSignTime, ref lstSerial);

                if (lstSignBy.Count > 0)
                    for (int k = 0; k < lstSignBy.Count; k++)
                    {
                        DataRow dr = dtFile.NewRow();
                        dr[0] = false;
                        dr[1] = Path.GetFileName(lstFile[i]);
                        dr[2] = lstSignBy[k];
                        dr[3] = lstSignTime[k].ToString("dd/MM/yyyy");
                        dr[4] = Path.GetDirectoryName(lstFile[i]);
                        dr[5] = lstSerial[k];
                        dtFile.Rows.Add(dr);
                    }
                else
                {
                    DataRow dr = dtFile.NewRow();
                    dr[0] = false;
                    dr[1] = Path.GetFileName(lstFile[i]);
                    dr[4] = Path.GetDirectoryName(lstFile[i]);
                    dtFile.Rows.Add(dr);
                }
            }
        }

        public void LoadOnlyFile(string fullPath, ref List<string> lstSignBy, ref List<DateTime> lstSignTime)
        {
            string type = Path.GetExtension(fullPath);
            using (ESDigitalSignatureManager osm = new ESDigitalSignatureManager(File.ReadAllBytes(fullPath), Path.GetExtension(fullPath)))
            {
                foreach (ESignature item in osm.Signatures)
                {
                    foreach (string st in item.Signer.Subject.Split(','))
                    {
                        if (st.Split('=')[0].Trim() == "CN")
                            lstSignBy.Add(st.Split('=')[1]);
                    }
                    lstSignTime.Add(item.SigningTime);
                }
            }
        }

        public void LoadOnlyFile(string fullPath, ref List<string> lstSignBy, ref List<DateTime> lstSignTime, ref List<string> lstSerial)
        {
            string type = Path.GetExtension(fullPath);
            using (ESDigitalSignatureManager osm = new ESDigitalSignatureManager(File.ReadAllBytes(fullPath), Path.GetExtension(fullPath)))
            {
                foreach (ESignature item in osm.Signatures)
                {
                    foreach (string st in item.Signer.Subject.Split(','))
                    {
                        if (st.Split('=')[0].Trim() == "CN")
                            lstSignBy.Add(st.Split('=')[1]);
                    }
                    lstSignTime.Add(item.SigningTime);
                    lstSerial.Add(item.Signer.SerialNumber);
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                rdbOnlyFile.Checked = true;
                CreatDataTable();
                LoadGridView();
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
            }
        }

        public void DirSearch(string dir, ref List<string> arrFiles)
        {
            try
            {
                foreach (string f in Directory.GetFiles(dir, "*.*").Where(s => s.EndsWith(".doc") ||
                            s.EndsWith(".docx") || s.EndsWith(".xlsx") || s.EndsWith(".xls") || s.EndsWith(".pdf")))
                    arrFiles.Add(f);
                foreach (string d in Directory.GetDirectories(dir))
                {
                    DirSearch(d, ref arrFiles);
                }
            }
            catch (System.Exception ex)
            {
                string strErr = ex.Message;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                lstFile.Clear();
                if (rdbOnlyFile.Checked)
                {
                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        ofd.Filter = "(*.docx;*.xlsx;*.pdf; *.xml)|*.docx;*.xlsx;*.pdf; *.xml";
                        ofd.Multiselect = true;
                        if (ofd.ShowDialog() == DialogResult.OK)
                            lstFile.AddRange(ofd.FileNames);
                    }
                }
                else if (rdbFolder.Checked)
                {
                    using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                    {
                        string[] extension = new string[] { ".docx", ".xlsx", ".pdf" };
                        fbd.RootFolder = Environment.SpecialFolder.MyComputer;
                        fbd.Description = "Chọn thư mục để ký";
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            DirSearch(fbd.SelectedPath, ref lstFile);
                        }
                    }
                }
                Cursor.Current = Cursors.Default;
                btnReload_Click(null, null);
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                string strErr = ex.Message;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            try
            {
                drArray = null;
                lstFileSignNoSuccess.Clear();
                lstFileSignSuccess.Clear();
                drArray = dtFile.Select("Check = true");

                if (drArray.Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn ít nhất 1 văn bản để ký!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cert = Common.SelectCertificateFromStore("Danh sách chứng thư số", "Chọn chứng thư số của bạn", this.Handle);
                if (cert == null)
                {
                    MessageBox.Show("Quá trình ký bị hủy bởi người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                for (int i = 0; i < drArray.Length; i++)
                {
                    string fullPath = drArray[i][4] + "\\" + drArray[i][1];
                    if (lstFileSignSuccess.Contains(fullPath) || lstFileSignNoSuccess.Contains(fullPath))
                        continue;
                    try
                    {
                        using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(File.ReadAllBytes(fullPath), Path.GetExtension(fullPath)))
                        {
                            dsm.Sign(cert);
                            File.WriteAllBytes(fullPath, dsm.ToArray());
                            lstFileSignSuccess.Add(fullPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        string strErr = ex.Message;
                        lstFileSignNoSuccess.Add(fullPath);
                    }
                }

                string strResult = "Ký thành công " + lstFileSignSuccess.Count + " trên " + (lstFileSignSuccess.Count + lstFileSignNoSuccess.Count);
                MessageBox.Show(strResult, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
                MessageBox.Show("Xảy ra lỗi trong quá trình ký!\n" + strErr, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnReload_Click(null, null);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                CreatDataTable();
                LoadDataGrid();
                LoadGridView();
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> list = new List<string>();
                for (int i = 0; i < cfgFile.Rows.Count; i++)
                {
                    string fullPath = cfgFile.Rows[i][4] + "\\" + cfgFile.Rows[i][1];
                    if (!list.Contains(fullPath))
                    {
                        list.Add(fullPath);
                        cfgFile.Rows[i][0] = chkAll.Checked.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            try
            {
                About frmAbout = new About();
                frmAbout.ShowDialog();
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Application.StartupPath + "\\Help\\Hướng dẫn sử dụng phần mềm ký điện tử.pdf";
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình hay không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }
        }

        private void cfgFile_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int row = cfgFile.RowSel;

                if (row < 1) return;

                if (cfgFile.Rows[row]["Serial"] == null || cfgFile.Rows[row]["Serial"].ToString() == "")
                {
                    MessageBox.Show("File chưa được ký!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string serial = cfgFile.Rows[row]["Serial"].ToString();
                string filePath = Path.Combine(cfgFile.Rows[row]["Path"].ToString(), cfgFile.Rows[row]["Name"].ToString());

                ESDigitalSignatureManager osm = new ESDigitalSignatureManager(File.ReadAllBytes(filePath), Path.GetExtension(filePath));
                foreach (ESignature item in osm.Signatures)
                {
                    if (item.Signer.SerialNumber == serial)
                    {
                        X509Certificate2 cert = new X509Certificate2(item.Signer.RawData);
                        X509Certificate2UI.DisplayCertificate(cert);
                        break;
                    }
                }
                osm.Dispose();
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
            }
        }
    }
}
