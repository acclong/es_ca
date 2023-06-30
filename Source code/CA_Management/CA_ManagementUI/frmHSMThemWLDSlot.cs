using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ES.CA_ManagementBUS;
using esDigitalSignature;

namespace ES.CA_ManagementUI
{
    public partial class frmHSMThemWLDSlot : Form
    {
        #region Var
        private BUSQuanTri _bus = new BUSQuanTri();
        private int _id;
        private string _tokenLabel;
        private string _serial;
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Serial
        {
            get { return _serial; }
            set { _serial = value; }
        }

        public string TokenLabel
        {
            get { return _tokenLabel; }
            set { _tokenLabel = value; }
        }

        public int ID
        {
            get { return _id; }
        }

        #endregion

        public frmHSMThemWLDSlot()
        {
            InitializeComponent();
        }

        private void frmThemWLDSlotNew_Load(object sender, EventArgs e)
        {
            try
            {
                InitCboTokenLabel();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        #region Data

        #endregion

        #region Init
        private void InitCboTokenLabel()
        {
            cboTokenLabel.DataSource = _bus.HSM_Slot_SelectAll_NotUse();
            cboTokenLabel.DisplayMember = "TokenLabel";
            cboTokenLabel.ValueMember = "TokenLabel";
        }
        #endregion

        #region Controls
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _id = Convert.ToInt32(nudID.Value);
                _serial = txtSerial.Text;
                _tokenLabel = cboTokenLabel.SelectedValue.ToString();
                _description = txtDescription.Text;

                // Check rang buoc
                if (_bus.HSM_WLDSlot_Select_CheckUsed(_id, _tokenLabel, _serial))
                {
                    clsShare.Message_Error("ID, Token label hoặc Serial đã được sử dụng cho WLD Slot. Hãy kiểm tra lại!");
                    return;
                }

                // Xu ly HSM
                using (HSMServiceProvider hsm = new HSMServiceProvider())
                {
                    hsm.CreateWLDSlot(_id, _tokenLabel, _serial, _description);
                }

                // Tra ket qua
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }
        #endregion

    }
}
