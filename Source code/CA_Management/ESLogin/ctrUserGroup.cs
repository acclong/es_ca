using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace ESLogin
{
    public partial class ctrUserGroup : UserControl
    {
        public ctrUserGroup()
        {
            InitializeComponent();
        }
        private BUS_UserManagement clsQT = new BUS_UserManagement();

        private short currentRoleID = -1;

        private void ucRoleGroup_Load(object sender, EventArgs e)
        {
            try
            {
                cfgRoleGroup_Load();
                loadComboModule();
                trvModule_Create();
                Role_Load();

                cfgRoleGroup_Click(null, null);

                cfgRoleGroupUser_Load();
                cfgRoleGroup.Click += new EventHandler(cfgRoleGroup_Click);
                cboModule.SelectedIndexChanged += cboModule_SelectedIndexChanged;
                chkAll.CheckedChanged += new EventHandler(chkAll_CheckedChanged);
                trvModule.NodeMouseClick += new TreeNodeMouseClickEventHandler(trvQuyen_NodeMouseClick);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void Role_Load()
        {
            if (clsSharing.Admin) return;
            //quyền cập nhật
            if (clsSharing.Permission("TTD.14.06") && clsSharing.Permission("TTD.14.08"))
            {
                return;
            }
            //quyền xem
            if (!clsSharing.Permission("TTD.14.05"))
            {
                this.btnRoleAdd.Enabled = false;
                this.btnGroupEdit.Enabled = false;
                this.btnGroupDelete.Enabled = false;
            }
            if (!clsSharing.Permission("TTD.14.07"))
            {
                this.btnModuleSave.Enabled = false;
                chkAll.Enabled = false;
                this.btnRoleUserUpdate.Enabled = false;
            }
        }

        void cfgRoleGroup_Click(object sender, EventArgs e)
        {
            if (cfgRoleGroup.RowSel < 1) return;

            currentRoleID = Convert.ToInt16(cfgRoleGroup.Rows[cfgRoleGroup.RowSel]["RoleID"]);

            //txtMoTa.Text = cfgNhomQuyen.Rows[cfgNhomQuyen.RowSel]["Descript"].ToString();

            trvModule_Load();
        }

        #region Cập nhật nhóm quyền

        private void cfgRoleGroup_Load()
        {
            cfgRoleGroup.Clear();

            cfgRoleGroup.Cols.Count = 3;

            cfgRoleGroup.Cols[0].Name = "RoleID";
            cfgRoleGroup.Cols[0].Visible = false;

            cfgRoleGroup.Cols[1].Name = "RoleName";
            cfgRoleGroup.Cols[1].Caption = "Tên nhóm quyền";

            cfgRoleGroup.Cols[2].Name = "Descript";
            cfgRoleGroup.Cols[2].Caption = "Mô tả";

            DataTable dt = clsQT.Q_ROLE_SelectByProgID(clsSharing.sProgramID);

            if (dt == null || dt.Rows.Count == 0)
            {
                //MessageBox.Show("Chưa có nhóm quyền nào được cập nhật!", "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cfgRoleGroup.Rows.Count = dt.Rows.Count + 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cfgRoleGroup.Rows[i + 1]["RoleID"] = dt.Rows[i]["RoleID"];
                cfgRoleGroup.Rows[i + 1]["RoleName"] = dt.Rows[i]["RoleName"];
                cfgRoleGroup.Rows[i + 1]["Descript"] = dt.Rows[i]["Descript"];
            }
            cfgRoleGroup.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
        }
        #endregion

        #region Phân quyền cho nhóm quyền

        void cboModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            trvModule_Create();
            trvModule_Load();
        }

        void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
                CheckTreeNode(trvModule.Nodes[0]);
            else
                UnCheckTreeNode(trvModule.Nodes[0]);
        }

        private void loadComboModule()
        {
            try
            {
                DataTable dt = clsQT.Q_FUNCTION_SelectModule();

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa cấu hình Module cho chương trình!", "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cboModule.DataSource = dt;
                cboModule.ValueMember = "ModuleID";
                cboModule.DisplayMember = "ModuleName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void trvModule_Create()
        {
            try
            {
                DataTable dtQuyen = clsQT.Q_Function_SelectAll();

                trvModule.Nodes.Clear();
                trvModule.CheckBoxes = true;

                //root
                TreeNode nodeRoot = new TreeNode();
                nodeRoot.Name = "root";
                nodeRoot.Text = "Danh sách chức năng sử dụng";
                nodeRoot.ForeColor = Color.Blue;
                //nodeRoot.NodeFont = new Font(nodeRoot.NodeFont, FontStyle.Bold);
                trvModule.Nodes.Add(nodeRoot);

                //Các node con
                for (int i = 0; i < dtQuyen.Rows.Count; i++)
                {
                    TreeNode node = new TreeNode();

                    //nếu ko có node cha thì lấy cha là root
                    if (clsSharing.isEmpty(dtQuyen.Rows[i]["FUNCTION_PARENT_ID"]) == true)
                    {
                        node.Name = dtQuyen.Rows[i]["FUNCTIONID"].ToString();
                        node.Text = dtQuyen.Rows[i]["FUNCTIONNAME"].ToString();
                        nodeRoot.Nodes.Add(node);
                    }
                    else
                    {
                        node.Name = dtQuyen.Rows[i]["FUNCTIONID"].ToString();
                        node.Text = dtQuyen.Rows[i]["FUNCTIONNAME"].ToString();

                        TreeNode[] parentNode = trvModule.Nodes.Find(dtQuyen.Rows[i]["FUNCTION_PARENT_ID"].ToString(), true);
                        if (parentNode != null && parentNode.Length > 0)
                        {
                            parentNode[0].Nodes.Add(node);
                        }
                    }
                }

                trvModule.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void trvModule_Load()
        {
            //bỏ check tất cả
            UnCheckTreeNode(trvModule.Nodes[0]);

            DataTable dtQuyen = clsQT.Q_PQFUNCTION_ROLE_SelectByRoleID(clsSharing.sProgramID, currentRoleID);

            for (int i = 0; i < dtQuyen.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtQuyen.Rows[i]["ROLEID"]) == currentRoleID)
                {
                    TreeNode[] node = trvModule.Nodes.Find(dtQuyen.Rows[i]["FUNCTIONID"].ToString(), true);
                    if (node != null && node.Length > 0)
                    {
                        node[0].Checked = true;
                        node[0].ForeColor = Color.Brown;
                    }
                }
            }
            trvModule.ExpandAll();
        }
        #endregion

        /****************************************************************************************************************/

        //bỏ check tất cả các node con
        private void UnCheckTreeNode(TreeNode node)
        {
            node.Checked = false;

            if (node.Nodes.Count == 0) return;

            foreach (TreeNode nod in node.Nodes)
            {
                UnCheckTreeNode(nod);
            }
        }

        //check tất cả các node con
        private void CheckTreeNode(TreeNode node)
        {
            node.Checked = true;

            if (node.Nodes.Count == 0) return;

            foreach (TreeNode nod in node.Nodes)
            {
                CheckTreeNode(nod);
            }
        }

        //cập nhật quyền cho các node con
        private void Node_Approve(TreeNode node)
        {
            if (node.Nodes.Count == 0)
            {
                if (node.Checked == true)
                    clsQT.Q_PQFUNCTION_ROLE_Insert(currentRoleID, node.Name);
                else
                    clsQT.Q_PQFUNCTION_ROLE_Delete(currentRoleID, node.Name);
            }
            else
            {
                foreach (TreeNode nod in node.Nodes)
                {
                    Node_Approve(nod);
                }
            }
        }

        //check/uncheck node cha thì check/uncheck tất cả node con
        void trvQuyen_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Checked == true)
                CheckTreeNode(e.Node);
            else
                UnCheckTreeNode(e.Node);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void cfgRoleGroupUser_Load()
        {
            #region create grid
            cfgRoleGroupUser.Clear();
            cfgRoleGroupUser.AllowEditing = true;
            cfgRoleGroupUser.Cols.Count = 1;

            cfgRoleGroupUser.Cols[0].Name = "UserID";
            cfgRoleGroupUser.Cols[0].Caption = "Tên đăng nhập";
            cfgRoleGroupUser.Cols[0].AllowEditing = false;
            cfgRoleGroupUser.Cols[0].Width = 100;

            //lấy danh sách các nhóm quyền
            DataTable dtNhomQuyen = clsQT.Q_ROLE_SelectByProgID(clsSharing.sProgramID);

            if (dtNhomQuyen == null || dtNhomQuyen.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có nhóm quyền nào được tạo!", "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //tạo các cột
            for (int i = 0; i < dtNhomQuyen.Rows.Count; i++)
            {
                C1.Win.C1FlexGrid.Column colmun = cfgRoleGroupUser.Cols.Add();
                colmun.Name = dtNhomQuyen.Rows[i]["RoleID"].ToString().Trim();
                colmun.Caption = dtNhomQuyen.Rows[i]["RoleName"].ToString().Trim();
                colmun.DataType = typeof(bool);
                colmun.AllowEditing = true;
                colmun.Width = TextRenderer.MeasureText(dtNhomQuyen.Rows[i]["RoleName"].ToString(), cfgRoleGroupUser.Font).Width;
            }

            //lấy danh sách user
            DataTable dtUser = clsQT.Q_USER_SelectAll();

            if (dtUser == null || dtUser.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có người dùng nào được tạo!", "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cfgRoleGroupUser.Rows.Count = dtUser.Rows.Count + 1;
            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
                cfgRoleGroupUser.Rows[i + 1]["UserID"] = dtUser.Rows[i]["UserID"];
            }
            #endregion
            cfgRoleGroupUser.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
            DataTable dt = clsQT.Q_USER_ROLE_SelectByProgID(clsSharing.sProgramID);

            if (dt == null || dt.Rows.Count == 0) return;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 1; j < cfgRoleGroupUser.Rows.Count; j++)
                {
                    if (dt.Rows[i]["UserID"].ToString().Trim() == cfgRoleGroupUser.Rows[j]["UserID"].ToString().Trim())
                    {
                        string _roleID = dt.Rows[i]["RoleID"].ToString();
                        cfgRoleGroupUser.Rows[j][_roleID] = true;
                        break;
                    }
                }
            }

        }



        private void btnRoleAdd_Click(object sender, EventArgs e)
        {
            frmRoleGroup_Update frm = new frmRoleGroup_Update();
            frm.ShowDialog();
            try
            {
                cfgRoleGroup_Load();
                cfgRoleGroupUser_Load();
                cfgRoleGroup_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình tải dữ liệu: " + ex.Message, "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnGroupEdit_Click(object sender, EventArgs e)
        {
            if (cfgRoleGroup.RowSel < 1) return;

            short roleID = Convert.ToInt16(cfgRoleGroup.Rows[cfgRoleGroup.RowSel]["RoleID"]);

            frmRoleGroup_Update frm = new frmRoleGroup_Update(roleID, cfgRoleGroup.Rows[cfgRoleGroup.RowSel]["RoleName"].ToString(),
                                                        cfgRoleGroup.Rows[cfgRoleGroup.RowSel]["Descript"].ToString());

            frm.ShowDialog();

            try
            {
                cfgRoleGroup_Load();
                cfgRoleGroupUser_Load();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình tải dữ liệu: " + ex.Message, "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnGroupDelete_Click(object sender, EventArgs e)
        {
            if (cfgRoleGroup.RowSel < 1) return;

            short roleID = Convert.ToInt16(cfgRoleGroup.Rows[cfgRoleGroup.RowSel]["RoleID"]);

            if (MessageBox.Show("Bạn có muốn xoá role này không", "ES - Monitoring", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            try
            {
                clsQT.Q_ROLE_DeleteByRoleID(roleID);
                MessageBox.Show("Xóa nhóm quyền thành công!", "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cfgRoleGroup_Load();
                cfgRoleGroupUser_Load();
                cfgRoleGroup_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xoá dữ liệu: " + ex.Message, "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnModuleSave_Click(object sender, EventArgs e)
        {
            try
            {
                Node_Approve(trvModule.Nodes[0]);

                MessageBox.Show("Cập nhật quyền cho nhóm quyền thành công!", "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Information);

                trvModule.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình ghi dữ liệu: " + ex.Message, "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnRoleUserUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i < cfgRoleGroupUser.Rows.Count; i++)
                {
                    for (int j = 1; j < cfgRoleGroupUser.Cols.Count; j++)
                    {
                        if (Convert.ToBoolean(cfgRoleGroupUser.Rows[i][j]) == true)
                            clsQT.Q_USER_ROLE_Insert(cfgRoleGroupUser.Rows[i]["UserID"].ToString().Trim(), Convert.ToInt16(cfgRoleGroupUser.Cols[j].Name));
                        else
                            clsQT.Q_USER_ROLE_Delete(cfgRoleGroupUser.Rows[i]["UserID"].ToString().Trim(), Convert.ToInt16(cfgRoleGroupUser.Cols[j].Name));
                    }
                }
                MessageBox.Show("Cập nhật dữ liệu thành công!", "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình ghi dữ liệu: " + ex.Message, "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
