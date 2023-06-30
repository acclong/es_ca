using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using esDigitalSignature;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.Web.Services;
using WebSignTest.CA_SignOnWeb;

namespace AppSign
{
    public partial class MainForm : Form
    {
        private int iTick = 0;
        private string strAppDefault = "TTDSignApp";
        private string keyUpdate = "NLDC";
        private string defaultStringResult = "\nVui lòng nhấn vào nút Refresh trên trình duyệt để làm mới dữ liệu";
        private string sEncrypt = "";
        private string NumberFile;
        private string Program;
        private string User;
        private string Key;
        private string TypeSign;
        private string Version;
        private string PathUpdate = Application.StartupPath + "\\AppUpdate.exe";
        private string UrlServiceWLAN = "http://192.168.68.3:8070/CA_SignOnWeb.asmx";
        private string UrlServiceInternet = "http://192.168.68.3:8070/CA_SignOnWeb.asmx";
        private string[] arrDataReceive_Lan1;
        private string[] arrDataReceive_Lan2;
        private string strErr = "";
        private DataTable dtFileInfo = new DataTable("RESULTS");
        private ES_Encrypt encrypt = new ES_Encrypt();

        private enum TypeLink
        {
            Internet,
            WLAN
        }

        private enum TypeSignOverWeb
        {
            HaveFileID,
            CreateFileServer,
            SaveFileInDB
        }

        public MainForm()
        {
            InitializeComponent();
            lblStatus.Text = "Chương trình ký bắt đầu thực hiện!";
            DefaultDataSource();
            LoadGrid();
        }

        public MainForm(string strEncrypt)
        {
            InitializeComponent();
            sEncrypt = strEncrypt;
            lblStatus.Text = "Chương trình ký bắt đầu thực hiện!";
            DefaultDataSource();
            LoadGrid();
            timerStart.Start();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timerStart_Tick(object sender, EventArgs e)
        {
            if (iTick == 5)
            {
                iTick = 0;
                timerStart.Stop();
                timerStart.Dispose();
                Run();
            }
            else
                iTick++;
        }

        private void Run()
        {
            try
            {
                #region Kiểm tra cấu trúc dữ liệu, lấy thông tin
                //Kiểm tra cấu trúc dữ liệu đẩy xuống
                bool bStructDataReceive = false;
                //Cắt lần 1: lấy thông tin tên chương trình
                arrDataReceive_Lan1 = sEncrypt.Split(':');
                if (arrDataReceive_Lan1[0].ToLower() == strAppDefault.ToLower() && arrDataReceive_Lan1.Length == 2)
                {
                    string strDataReceive = encrypt.DecryptString(arrDataReceive_Lan1[1]);
                    strDataReceive = strDataReceive.Replace("@~", " ");
                    //Cắt lần 2 lấy thông tin mã hóa
                    arrDataReceive_Lan2 = strDataReceive.Split(' ');
                    if (arrDataReceive_Lan2.Length == 6)
                        bStructDataReceive = true;
                    else
                        bStructDataReceive = false;
                }
                else
                    bStructDataReceive = false;
                if (!bStructDataReceive)
                {
                    lblStatus.Text = "Quá trình ký kết thúc!\nDữ liệu đầu vào không đúng!!" + defaultStringResult;
                    return;
                }

                //Gán giá trị cho các biên private
                Key = arrDataReceive_Lan2[0];
                User = arrDataReceive_Lan2[1];
                Program = arrDataReceive_Lan2[2];
                NumberFile = arrDataReceive_Lan2[3];
                TypeSign = arrDataReceive_Lan2[4];
                Version = arrDataReceive_Lan2[5];
                #endregion

                #region Kiểm tra version
                //Kiểm tra Version
                if (!CheckVersion(Version))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = PathUpdate;
                    startInfo.Verb = "runas";
                    startInfo.Arguments = keyUpdate + "#" + Version + "#" + sEncrypt;
                    Process.Start(startInfo);
                    this.Close();
                }
                #endregion
                else
                {
                    #region Lấy thông tin ký từ Webservice
                    //Kết nối webservice
                    ES_Encrypt enc = new ES_Encrypt();
                    CA_SignOnWeb service = new CA_SignOnWeb();
                    //Add a cookie to the web service to allow the use of session on the server side
                    System.Net.CookieContainer cookies = new System.Net.CookieContainer();
                    service.CookieContainer = cookies;
                    
                    //Set lại Url
                    if (TypeSign.Split('.').First() == TypeLink.Internet.ToString())
                        service.Url = UrlServiceInternet;
                    else
                        service.Url = UrlServiceWLAN;

                    //Login webservice
                    DateTime defaultTime = new DateTime(2012, 7, 1);
                    DateTime nowTime = DateTime.Now;
                    TimeSpan ts = nowTime - defaultTime;
                    double iTime = ts.TotalSeconds;
                    string pass = enc.EncryptString(iTime.ToString());
                    if (!service.LogIn("CATTD" + nowTime.ToString("yyyyMMddHHmmssffff"), pass))
                    {
                        lblStatus.Text = "Thời gian ký giữa client và server không đồng bộ!" + defaultStringResult;
                        return;
                    }

                    //Mã hóa lại
                    string primaryString = User + "@" + Program + "@" + NumberFile + "@" + TypeSign + "@" + Key;
                    string MessageSign = enc.EncryptString(primaryString);
                    
                    //So sánh mã khóa -- Check quyền -- Tạo bảng fileData
                    if (!service.GetInfoToSign(MessageSign, ref strErr, out dtFileInfo))
                    {
                        lblStatus.Text = strErr + defaultStringResult;
                        //Thoát Webservice
                        service.LogOut(Key);
                        return;
                    }
                    #endregion

                    #region Thực hiện Ký
                    //Khóa form
                    this.Enabled = false;

                    //Chọn Certificate
                    X509Certificate2 cert = Common.SelectCertificateFromStore("Thư viện ký", "Chọn một chứng thư số", this.Handle);

                    //Kiểm tra chứng thư số có được chọn
                    if (cert == null)
                    {
                        lblStatus.Text = "Quá trình ký kết thúc:\nChương trình bị hủy bởi người dùng!" + defaultStringResult;

                        goto OpenLock;
                    }

                    //Ký file
                    foreach (DataRow dr in dtFileInfo.Rows)
                    {
                        if (Convert.ToBoolean(dr["OKtoSign"]))
                        {
                            // ký file
                            using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager((byte[])dr["FileData"], dr["ExtensionFile"].ToString()))
                            {
                                dsm.Sign(cert);
                                dr["FileData"] = dsm.ToArray();
                            }
                        }
                    }
                    #endregion

                    #region Gửi thông tin sau khi ký lên Webservice
                    //Mã hóa thông tin gửi lên webservice
                    primaryString = Key + "$" + User + "$" + Program + "$" + TypeSign;
                    MessageSign = enc.EncryptString(primaryString);

                    //Gửi lên webservice kiểm tra văn bản ký và cập nhật db
                    DataTable dtResult = new DataTable("RESULT");
                    if (TypeSign.Split('.')[1] == TypeSignOverWeb.CreateFileServer.ToString())
                    {
                        if (!service.CreateAndSaveFileInServer(dtFileInfo, MessageSign, ref dtResult, ref strErr))
                        {
                            dgvResult.DataSource = dtResult;
                            LoadGrid();
                            lblStatus.Text = strErr + defaultStringResult;

                            goto OpenLock;
                        }
                        else
                        {
                            dgvResult.DataSource = dtResult;
                            LoadGrid();
                            lblStatus.Text = "Quá trình ký thực hiện thành công!" + defaultStringResult;
                        }
                    }
                    else if (TypeSign.Split('.')[1] == TypeSignOverWeb.HaveFileID.ToString())
                    {
                        if (!service.SaveFile(dtFileInfo, MessageSign, ref dtResult, ref strErr))
                        {
                            dgvResult.DataSource = dtResult;
                            LoadGrid();
                            lblStatus.Text = strErr + defaultStringResult;

                            goto OpenLock;
                        }
                        else
                        {
                            dgvResult.DataSource = dtResult;
                            LoadGrid();
                            lblStatus.Text = "Quá trình ký thực hiện thành công!" + defaultStringResult;
                        }
                    }
                    else if (TypeSign.Split('.')[1] == TypeSignOverWeb.SaveFileInDB.ToString())
                    {
                        if (!service.CreateAndSaveFileInDB(dtFileInfo, MessageSign, ref dtResult, ref strErr))
                        {
                            dgvResult.DataSource = dtResult;
                            LoadGrid();
                            lblStatus.Text = strErr + defaultStringResult;

                            goto OpenLock;
                        }
                        else
                        {
                            dgvResult.DataSource = dtResult;
                            LoadGrid();
                            lblStatus.Text = "Quá trình ký thực hiện thành công!" + defaultStringResult;
                        }
                    }
                    
                OpenLock:
                    //Thoát Webservice
                    service.LogOut(Key);
                    //Mở khóa form
                    this.Enabled = true;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //Mở khóa form
                this.Enabled = true;
            }
        }

        private void LoadGrid()
        {
            //Cấu hình grid
            dgvResult.BackgroundColor = Color.WhiteSmoke;
            dgvResult.DefaultCellStyle.Font = new Font("Times New Roman", 13);
            dgvResult.RowHeadersVisible = false;
            dgvResult.AllowUserToAddRows = false;
            dgvResult.AllowUserToDeleteRows = false;
            dgvResult.MultiSelect = false;
            dgvResult.Anchor = AnchorStyles.Top & AnchorStyles.Left & AnchorStyles.Right;
            dgvResult.RowTemplate.DefaultCellStyle.Font = new Font("Times New Roman", 9);
            dgvResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            
            //Gán Header
            dgvResult.Columns[0].HeaderText = "Tên File";
            dgvResult.Columns[1].HeaderText = "Kết quả ký";
            
            //Ẩn cột thừa nếu có
            for (int i = 0; i < dgvResult.Columns.Count; i++)
            {
                string colName = dgvResult.Columns[i].Name;
                if (colName != "FileName" && colName != "SignDetails")
                    dgvResult.Columns[i].Visible = false;
            }
            //Độ rộng các cột
            int widthColFirst = dgvResult.Width / 2;
            dgvResult.Columns["FileName"].Width = widthColFirst;

            for (int i = 0; i < dgvResult.Rows.Count; i++)
            {
                ((DataTable)dgvResult.DataSource).Rows[i]["FileName"] = FormatName(((DataTable)dgvResult.DataSource).Rows[i]["FileName"].ToString(), widthColFirst);
            }
            
            int widthColSecond = 0;
            if(dgvResult.Controls.OfType<VScrollBar>().Count() > 0)
                widthColSecond = dgvResult.Width - widthColFirst - 3;
            else
                widthColSecond = dgvResult.Width - widthColFirst - SystemInformation.VerticalScrollBarWidth - 3;
            dgvResult.Columns["SignDetails"].Width = widthColSecond;

            //wraptext trong cell
            dgvResult.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
        
        private void DefaultDataSource()
        {
            //Lấy dataSource
            DataTable dt = new DataTable();
            dt.Columns.Add("FileName");
            dt.Columns.Add("SignDetails");

            dgvResult.DataSource = dt;
        }

        private string FormatName(string strName, int widthMax)
        {
            string[] array = strName.Split('_');
            strName = "";
            for (int i = 0, lastWidth = 0; i < array.Length; i++)
            {
                if (i != 0)
                    array[i] = '_' + array[i];
                int width = TextRenderer.MeasureText(array[i], new Font(dgvResult.RowTemplate.DefaultCellStyle.Font.FontFamily,
                                                    dgvResult.RowTemplate.DefaultCellStyle.Font.Size,
                                                    dgvResult.RowTemplate.DefaultCellStyle.Font.Style)).Width;

                if (width + lastWidth > widthMax && lastWidth == 0)
                {
                    strName += (array[i] + '\n');
                    lastWidth = 0;
                }
                else if (width + lastWidth > widthMax && lastWidth != 0)
                {
                    strName += ('\n' + array[i]);
                    lastWidth = width;
                }
                else
                {
                    lastWidth += width;
                    strName += array[i];
                }
            }

            return strName;
        }

        private bool CheckVersion(string version)
        {
            //Lấy version từ assembly
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string versionCurent = fvi.FileVersion;
            //Kiểm tra version
            var Version = new Version(version);
            var VersionCurrent = new Version(versionCurent);

            var result = Version.CompareTo(VersionCurrent);

            if (result > 0)
                return false;
            else
                return true;
        }
    }
}
