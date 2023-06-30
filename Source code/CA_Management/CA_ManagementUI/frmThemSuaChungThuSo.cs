using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using esDigitalSignature;
using ES.CA_ManagementBUS;
using System.IO;

namespace ES.CA_ManagementUI
{
    public partial class frmThemSuaChungThuSo : Form
    {
        #region Var

        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtCert = new DataTable();
        X509Certificate2 _x509Cert;
        int _CertID = 0;

        public int CertID
        {
            get { return _CertID; }
            set { _CertID = value; }
        }

        #endregion

        public frmThemSuaChungThuSo()
        {
            InitializeComponent();
        }

        public void frmThemSuaChungThuSo_Load(object sender, EventArgs e)
        {
            try
            {
                InitcboStatus();
                InitcboCertType();
                if (CertID == 0)
                {
                    //thêm chứng thư mới.
                    txtCertID.Text = "";
                    btnLoadFile.Text = "Load file";
                }
                else
                {
                    // xem thông tin chứng thư đã chọn và tiến hành cập nhật nếu cần thiết.
                    txtCertID.Text = CertID.ToString();
                    // lấy x509cert tương ứng
                    _dtCert = _bus.CA_Certificate_SelectByCertID(CertID);
                    _x509Cert = new X509Certificate2((byte[])(_dtCert.Rows[0]["RawData"]));
                    // Load form với cert đã chọn
                    FillControlsFromCert(_x509Cert, _dtCert.Rows[0]["Status"], _dtCert.Rows[0]["CertType"]);
                    btnLoadFile.Text = "Get file";
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region Data

        #endregion

        #region Init

        private void InitcboStatus()
        {
            string[] array = { "Không hiệu lực", "Hiệu lực" };
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            DataRow dr = null;
            for (int i = 0; i < array.Length; i++)
            {
                dr = dt.NewRow();
                dr["ID"] = i;
                dr["Name"] = array[i];
                dt.Rows.Add(dr);
            }

            cboStatus.DataSource = dt;
            cboStatus.DisplayMember = "Name";
            cboStatus.ValueMember = "ID";
        }

        private void InitcboCertType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            DataRow dr = null;

            dr = dt.NewRow();
            dr["ID"] = 1;
            dr["Name"] = "Cá nhân";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 2;
            dr["Name"] = "Doanh nghiệp";
            dt.Rows.Add(dr);

            cboCertType.DataSource = dt;
            cboCertType.DisplayMember = "Name";
            cboCertType.ValueMember = "ID";
        }

        private void FillControlsFromCert(X509Certificate2 x509Cert, object status, object type)
        {
            // hiển thị thông tin của chứng thư
            txtName.Text = x509Cert.GetNameInfo(X509NameType.DnsName, false);
            txtSerial.Text = x509Cert.SerialNumber;
            txtSubject.Text = x509Cert.Subject.Replace(", ", "\r\n");
            txtIssuer.Text = x509Cert.Issuer.Replace(", ", "\r\n");
            txtValidFrom.Text = x509Cert.NotBefore.ToString("dd/MM/yyyy HH:mm:ss");
            txtValidTo.Text = x509Cert.NotAfter.ToString("dd/MM/yyyy HH:mm:ss");
            txtThumbPrint.Text = x509Cert.Thumbprint;

            // cboStatus
            cboStatus.SelectedValue = status;
            cboCertType.SelectedValue = type;
        }

        #endregion

        #region Controls

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (CertID == 0)
                {
                    // load file để tạo mới
                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        ofd.Multiselect = false;

                        ofd.Filter = "Certificate Files (*.cer,*.crt,*.pem)|*.cer;*.crt;*.pem|All files (*.*)|*.*";

                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            _x509Cert = Common.GetCertificateByFile(ofd.FileName);
                            FillControlsFromCert(_x509Cert, 1, 1);
                        }
                    }
                }
                else
                {
                    // tải file về
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.FileName = txtName.Text + ".cer";
                        sfd.Filter = "Certificate Files (*.cer,*.crt)|*.cer;*.crt|All files (*.*)|*.*";
                        if (sfd.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(sfd.FileName))
                        {
                            File.WriteAllBytes(sfd.FileName, _x509Cert.RawData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Lỗi khi load chứng thư số!\n\n" + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // kiểm tra đã nhập thông tin chứng thư số
                if (_x509Cert == null)
                {
                    clsShare.Message_Error("Không có thông tin của chứng thư!");
                    return;
                }

                // kiểm tra chứng thư đã nhập vào
                //Edited by Toantk on 23/4/2015
                //Chuyển hàm lấy chứng thư để kiểm tra vào Business
                int certIDinDB = _bus.CA_Certificate_HasCertificate(_x509Cert.SerialNumber);
                if (CertID == 0 && certIDinDB != 0)
                {
                    clsShare.Message_Error("Chứng thư số đã tồn tại trong hệ thống. Không thể thêm mới chứng thư này!");
                    return;
                }
                else if (CertID != 0 && certIDinDB != 0)
                    if (CertID != certIDinDB)
                    {
                        clsShare.Message_Error("Chứng thư số đã bị trùng với chứng thư số khác trong hệ thống. Cập nhật chứng thư số thất bại!");
                        return;
                    }

                // Cập nhật or thêm mới chứng thư số
                if (CertID == 0)
                {
                    // Tìm IssuerID
                    // lấy cây liên kết cert
                    X509Chain x509Ch = new X509Chain();
                    x509Ch.Build(_x509Cert);
                    if (x509Ch.ChainElements.Count == 1)
                    {
                        clsShare.Message_Error("Hệ thống không thể tìm thấy Issuer của chứng thư số. Hãy kiểm tra lại!");
                        return;
                    }
                    string serialIssuer = x509Ch.ChainElements[1].Certificate.SerialNumber;
                    // tìm ID theo serialNumber
                    //Edited by Toantk on 23/4/2015
                    //Chuyển hàm lấy CA để kiểm tra vào Business
                    int issuerID = _bus.CA_CertificationAuthority_HasCA(serialIssuer);
                    if (issuerID == 0)
                    {
                        clsShare.Message_Warning("Nhà cung cấp chưa được khai báo trong hệ thống. Hãy thêm mới nhà cung cấp trước!");
                        return;
                    }

                    // Insert vào db
                    _bus.CA_Certificate_Insert(_x509Cert, clsShare.sUserName, Convert.ToInt32(cboStatus.SelectedValue), issuerID, Convert.ToInt32(cboCertType.SelectedValue));
                }
                else
                    _bus.CA_Certificate_Update(CertID, Convert.ToInt32(cboStatus.SelectedValue), clsShare.sUserName, Convert.ToInt32(cboCertType.SelectedValue));

                clsShare.Message_Info("Cập nhật chứng thư số thành công!");
                this.ActiveControl = btnLoadFile;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Event

        #endregion

    }
}
