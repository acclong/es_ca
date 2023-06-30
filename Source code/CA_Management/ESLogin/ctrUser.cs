using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESLogin;
using C1.Win.C1FlexGrid;

namespace ESLogin
{
    public partial class ctrUser : UserControl
    {
        private BUS_UserManagement clsQT = new BUS_UserManagement();
        private string _curUsername = "";
        private bool _isTreeClick = false;
        private bool _isTreeFirstCheck = false;

        public ctrUser()
        {
            InitializeComponent();
        }

        private void ctrUser_Load(object sender, EventArgs e)
        {
            try
            {
                grvUser_Load();
                trvModule_Load();
                trvModule_CheckQuyen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        #region Load dữ liệu

        private void grvUser_Load()
        {
            grvUser.DataSource = clsQT.Q_USER_SelectAll();

            grvUser.Columns[0].Name = "USERNAME";
            grvUser.Columns[0].HeaderText = "Tên đăng nhập";
            grvUser.Columns[0].Width = 150;

            grvUser.Columns[1].Name = "FULLNAME";
            grvUser.Columns[1].HeaderText = "Tên người dùng";
            grvUser.Columns[1].Width = 200;

            grvUser.Columns[2].Name = "Descript";
            grvUser.Columns[2].HeaderText = "Mô tả";
            grvUser.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void trvModule_Load()
        {
            DataTable dtRole = clsQT.Q_Function_SelectAll();
            trvModule.Nodes.Clear();
            trvModule.CheckBoxes = true;

            //Các node con
            for (int i = 0; i < dtRole.Rows.Count; i++)
            {
                TreeNode node = new TreeNode();

                //nếu ko có node cha thì lấy cha là root
                if (clsSharing.isEmpty(dtRole.Rows[i]["PARENT_ID"]) == true)
                {
                    TreeNode nodeRoot = new TreeNode();
                    nodeRoot.Name = dtRole.Rows[i]["CHILD_ID"].ToString();
                    nodeRoot.Text = dtRole.Rows[i]["FUNCTIONNAME"].ToString();
                    nodeRoot.ForeColor = Color.Blue;
                    //nodeRoot.NodeFont = new Font(nodeRoot.NodeFont, FontStyle.Bold);
                    trvModule.Nodes.Add(nodeRoot);
                }
                else
                {
                    node.Name = dtRole.Rows[i]["CHILD_ID"].ToString();
                    node.Text = dtRole.Rows[i]["FUNCTIONNAME"].ToString();

                    TreeNode[] parentNode = trvModule.Nodes.Find(dtRole.Rows[i]["PARENT_ID"].ToString(), true);
                    if (parentNode != null)
                    {
                        parentNode[0].Nodes.Add(node);
                    }
                }
            }

            trvModule.ExpandAll();
        }

        private void trvModule_CheckQuyen()
        {
            foreach (TreeNode node in trvModule.Nodes)
                UnCheckTreeNode(node);

            DataTable dtSelect = clsQT.Q_USER_FUNCTION_SelectByUsername(_curUsername);

            for (int i = 0; i < dtSelect.Rows.Count; i++)
            {
                TreeNode[] node = trvModule.Nodes.Find(dtSelect.Rows[i]["FUNCTIONID"].ToString(), true);
                if (node != null && node.Length > 0)
                {
                    node[0].Checked = true;
                    node[0].ForeColor = Color.Brown;
                }
            }

            trvModule.ExpandAll();
        }

        //cập nhật quyền cho các node con
        private void Node_Approve(TreeNode node)
        {
            if (node.Checked == true)
                clsQT.Q_USER_FUNCTION_Insert(_curUsername, Int32.Parse(node.Name));
            else
                clsQT.Q_USER_FUNCTION_Delete(_curUsername, Int32.Parse(node.Name));

            foreach (TreeNode nod in node.Nodes)
            {
                Node_Approve(nod);
            }
        }
        #endregion

        #region Events

        private void grvUser_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                if (e.StateChanged != DataGridViewElementStates.Selected || e.Row.Selected == false) return;

                _curUsername = grvUser.SelectedRows[0].Cells["USERNAME"].Value.ToString();
                trvModule_CheckQuyen();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi " + ex.Message, "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void trvModule_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (!_isTreeClick) return;

            if (_isTreeFirstCheck) return;
            _isTreeFirstCheck = true;
            SelectChildren(e.Node, e.Node.Checked);
            SelectParents(e.Node, e.Node.Checked);
            _isTreeFirstCheck = false;
        }

        private void trvModule_MouseDown(object sender, MouseEventArgs e)
        {
            _isTreeClick = true;
        }

        private void trvModule_MouseUp(object sender, MouseEventArgs e)
        {
            _isTreeClick = false;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
                foreach (TreeNode node in trvModule.Nodes)
                    CheckTreeNode(node);
            else
                foreach (TreeNode node in trvModule.Nodes)
                    UnCheckTreeNode(node);
        }

        private void btnUserAdd_Click(object sender, EventArgs e)
        {
            frmUser_Update frm = new frmUser_Update();
            frm.ShowDialog();
            try
            {
                grvUser_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi " + ex.Message, "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnUserEdit_Click(object sender, EventArgs e)
        {
            if (grvUser.SelectedRows.Count < 1) return;

            frmUser_Update frm = new frmUser_Update(_curUsername);
            frm.ShowDialog();

            try
            {
                grvUser_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi " + ex.Message, "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnUserDelete_Click(object sender, EventArgs e)
        {
            if (grvUser.SelectedRows.Count < 1) return;

            if (MessageBox.Show("Bạn muốn xoá người dùng này không?", "Quản trị người dùng", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            try
            {
                clsQT.Q_USER_Delete(_curUsername);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa user: " + ex.Message, "Quản trị người dùng"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                grvUser_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi " + ex.Message, "ES - Monitoring"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnDefaultLogin_Click(object sender, EventArgs e)
        {
            clsSharing.gDB.capnhat_User_ACCess(ClsLogin.Quyen_truycap.capnhat, clsSharing.gDB.GetSQLConnection(), clsSharing.sProgramID, clsSharing.Admin);
        }

        private void btnRoleSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TreeNode note in trvModule.Nodes)
                    Node_Approve(note);

                MessageBox.Show("Cập nhật quyền cho người dùng thành công!", "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Information);

                trvModule_CheckQuyen();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình ghi dữ liệu: " + ex.Message, "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

        #region Help

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

        //bỏ check tất cả các node con
        private void UnCheckTreeNode(TreeNode node)
        {
            node.Checked = false;
            node.ForeColor = Color.Black;

            if (node.Nodes.Count == 0) return;

            foreach (TreeNode nod in node.Nodes)
            {
                UnCheckTreeNode(nod);
            }
        }

        private void SelectChildren(TreeNode node, Boolean isChecked)
        {
            foreach (TreeNode childNode in node.Nodes)
            {
                childNode.Checked = node.Checked;
                SelectChildren(childNode, isChecked);
            }
        }

        private void SelectParents(TreeNode node, Boolean isChecked)
        {
            var parent = node.Parent;

            if (parent == null)
                return;

            if (!isChecked && HasCheckedNode(parent))
                return;

            parent.Checked = isChecked;
            SelectParents(parent, isChecked);
        }

        private bool HasCheckedNode(TreeNode node)
        {
            return node.Nodes.Cast<TreeNode>().Any(n => n.Checked);
        }
        #endregion
    }
}
