using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;
using System.Net;
using System.ComponentModel;
using Telerik.WinControls.UI;
using Telerik.WinControls.Layouts;
using Telerik.WinControls;
using System.Security;
using C1.Win.C1FlexGrid;

namespace ES.CA_ManagementUI
{
    public partial class clsShare
    {
        #region Variable common - Thông tin người dùng đăng nhập
        //Tiêu đề Message Box
        public static string stMsgTitle = "Hệ thống quản lý chữ ký số";
        //người dùng đăng nhập vào chương trình
        public static string sUserName;
        //quyền admin
        public static bool Admin;
        //Quyền truy cập menu
        public static DataTable dtRole;

        //đường dẫn file config chương trình
        public static string sAppPath;
        //đường dẫn CRYPTOKI.DLL
        public static string CRYPTOKI = "cryptoki.dll";
        #endregion

        #region Variable common - Biến cố định (ko cập nhật)
        // Màu tô hàng của grid
        public static Color clRow = Color.FromArgb(222, 230, 239);

        //Số mũ
        public static string Superscript0 = "\u2070";
        public static string Superscript1 = "\u00B9";
        public static string Superscript2 = "\u00B2";
        public static string Superscript3 = "\u00B3";
        public static string Superscript4 = "\u2074";

        //Định dạng số
        public static string sDecimalFormat = "### ### ### ### ### ##0.###";
        public static string sIntegerFormat = "### ### ### ### ### ##0";

        #endregion

        #region Common
        /// <summary>
        /// hàm kiểm tra 1 object là null hay không
        /// </summary>
        /// <param name="obj">object cần kiểm tra</param>
        /// <returns></returns>
        public static bool isEmpty(object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString().Trim() == "")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Thêm dấu ngăn cách vào chuỗi ngày ddMMyyyy
        /// </summary>
        /// <param name="stdt"></param>
        /// <param name="charJoin"></param>
        /// <returns></returns>
        public static String RecoverStringToDate(string stdt, string charJoin)
        {
            return stdt.Substring(0, 2) + charJoin + stdt.Substring(2, 2) + charJoin + stdt.Substring(4);
        }

        /// <summary>
        /// Convert string to datetime
        /// </summary>
        /// <param name="stdt"></param>
        /// <param name="charSplit"></param>
        /// <returns></returns>
        public static DateTime StringDate(string stdt, string charSplit)
        {
            DateTime dt;
            if (charSplit == string.Empty)
            {
                dt = new DateTime(int.Parse(stdt.Substring(4)), int.Parse(stdt.Substring(2, 2)), int.Parse(stdt.Substring(0, 2)));
            }
            else
            {
                dt = new DateTime(int.Parse(stdt.Substring(stdt.LastIndexOf(charSplit) + 1)), int.Parse(stdt.Substring(stdt.IndexOf(charSplit) + 1, 2)), int.Parse(stdt.Substring(0, 2)));
            }
            return dt;
        }

        public static DateTime PivotStringDate(string stdt, string charSplit)
        {
            DateTime dt;
            if (charSplit == string.Empty)
            {
                dt = new DateTime(int.Parse(stdt.Substring(4)), int.Parse(stdt.Substring(2, 2)), int.Parse(stdt.Substring(0, 2)));
            }
            else
            {
                dt = new DateTime(int.Parse(stdt.Substring(stdt.LastIndexOf(charSplit) + 1)), int.Parse(stdt.Substring(stdt.IndexOf(charSplit) + 1, 2)), int.Parse(stdt.Substring(0, 2))).Date;
            }
            return dt;
        }

        /// <summary>
        /// Check is row null of data (Return true is null or false is not null)
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="dr"></param>
        /// <returns>Return true is null or false is not null</returns>
        public bool CheckRowDataNull(string[] arr, DataRow dr)
        {
            bool kt = true;
            foreach (string prefix in arr)
            {
                if (!isEmpty(dr[prefix]))
                {
                    kt = false;
                    break;
                }
            }
            return kt;
        }

        public void FreeObject(ref DataSet ds)
        {
            try
            {
                if (ds != null)
                {
                    ds.Dispose();
                }
                ds = new DataSet();
            }
            catch { }
        }

        public void FreeObject(ref DataTable dt)
        {
            try
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                dt = new DataTable();
            }
            catch { }
        }

        public void FreeObject(ref ComboBox cbo)
        {
            if (cbo.Items.Count > 0)
                cbo.Items.Clear();
        }

        public void FreeObject(ref ListBox lsb)
        {
            if (lsb.Items.Count > 0)
                lsb.Items.Clear();
        }

        public void FreeObject(ref CheckedListBox clb)
        {
            if (clb.Items.Count > 0)
                clb.Items.Clear();
        }

        public string ReturnColectionCode(DataTable dtSource)
        {
            int i = 0;
            string str = string.Empty;
            foreach (DataRow row in dtSource.Rows)
            {
                if (i > 0)
                    str += ",";
                str += row["Ma_NM"].ToString();
                i++;
            }
            return str;
        }
        #endregion

        #region Các hàm xử lý thời gian
        /// <summary>
        /// Đếm tuần dựa trên tháng của năm
        /// </summary>
        /// <param name="year">Năm hiện tại</param>
        /// <param name="thang">Tháng hiện tại</param>
        /// <returns>Số tuần của tháng</returns>
        public static int WeekOfMonth_Count(int year, int month)
        {
            DateTime date = new DateTime(year, month, 1);
            string dayofwwek = date.ToString("ddd");
            int temp = 0;
            switch (dayofwwek)
            {
                case "Mon":
                    temp = 0;
                    break;
                case "Tue":
                    temp = 1;
                    break;
                case "Wed":
                    temp = 2;
                    break;
                case "Thu":
                    temp = 3;
                    break;
                case "Fri":
                    temp = 4;
                    break;
                case "Sat":
                    temp = 5;
                    break;
                case "Sun":
                    temp = 6;
                    break;
                default: break;
            };
            DateTime fromDay = date.AddDays(-temp + 7);
            int count = 1;
            while (fromDay.Month == month)
            {
                count++;
                fromDay = fromDay.AddDays(7);
            }
            return count;
        }

        /// <summary>
        /// Lấy các ngày của 1 tuần
        /// </summary>
        /// <param name="week">Tuần hiện tai</param>
        /// <param name="thang">Tháng hiện tại</param>
        /// <param name="year">Năm hiện tại</param>
        /// <param name="fromDay">Ngày đầu tuần được trả về</param>
        /// <param name="toDay">Ngày cuối tuần được trả về</param>
        public static void DayOfWeek_Load(int week, int month, int year, ref DateTime fromDay, ref DateTime toDay)
        {
            DateTime date = new DateTime(year, month, 1);
            string dayofwwek = date.ToString("ddd");
            int temp = 0;
            switch (dayofwwek)
            {
                case "Mon":
                    temp = 0;
                    break;
                case "Tue":
                    temp = 1;
                    break;
                case "Wed":
                    temp = 2;
                    break;
                case "Thu":
                    temp = 3;
                    break;
                case "Fri":
                    temp = 4;
                    break;
                case "Sat":
                    temp = 5;
                    break;
                case "Sun":
                    temp = 6;
                    break;
                default: break;
            };
            fromDay = date.AddDays((week - 1) * 7 - temp);
            toDay = fromDay.AddDays(6);
        }

        /// <summary>
        /// Lấy các ngày của 1 tuần
        /// </summary>
        /// <param name="week"></param>
        /// <param name="year"></param>
        /// <param name="fromDay"></param>
        /// <param name="toDay"></param>
        public static void DayOfWeek_Load(int week, int year, ref DateTime fromDay, ref DateTime toDay)
        {
            DateTime date = new DateTime(year, 1, 1);
            string dayofwwek = date.ToString("ddd");
            int temp = 0;
            switch (dayofwwek)
            {
                case "Mon":
                    temp = 0;
                    break;
                case "Tue":
                    temp = 1;
                    break;
                case "Wed":
                    temp = 2;
                    break;
                case "Thu":
                    temp = 3;
                    break;
                case "Fri":
                    temp = 4;
                    break;
                case "Sat":
                    temp = 5;
                    break;
                case "Sun":
                    temp = 6;
                    break;
            };

            fromDay = date.AddDays((week - 1) * 7 - temp);

            toDay = fromDay.AddDays(6);
        }

        /// <summary>
        /// Lấy các tuần theo tháng và năm
        /// </summary>
        /// <param name="year">Năm hiện tại</param>
        /// <param name="month">Tháng hiện tại</param>
        /// <returns></returns>
        public static DataTable WeekOfYear_Load(int year, int month)
        {
            DataTable dtWeek = new DataTable();
            dtWeek.Columns.Add("weekID", typeof(int));
            dtWeek.Columns.Add("fromDay", typeof(DateTime));
            dtWeek.Columns.Add("toDay", typeof(DateTime));
            int week_sum = 53;
            DateTime fromDate = new DateTime(year, 1, 1);
            DateTime toDate = new DateTime(year, 1, 1);
            if (DateTime.IsLeapYear(fromDate.Year) && fromDate.DayOfWeek.ToString() == "Sunday")
            {
                week_sum = 54;
            }
            if (month == 0)
            {
                for (int i = 1; i <= week_sum; i++)
                {
                    DayOfWeek_Load(i, year, ref fromDate, ref toDate);
                    dtWeek.Rows.Add(i, fromDate, toDate);
                }
            }
            else
            {
                for (int i = 1; i <= week_sum; i++)
                {
                    DayOfWeek_Load(i, year, ref fromDate, ref toDate);
                    if ((fromDate.Month == month && fromDate.Year == year) || (toDate.Month == month && toDate.Year == year))
                    {
                        dtWeek.Rows.Add(i, fromDate, toDate);
                    }
                }
            }
            return dtWeek;
        }
        #endregion

        #region Message Box
        /// <summary>
        /// Hàm hiển thị thông báo info từ string
        /// </summary>
        /// <param name="strCaption"></param>
        /// <param name="strTitle"></param>
        public static void Message_Info(string strCaption)
        {
            MessageBox.Show(strCaption, stMsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Hàm hiển thị thông báo warning từ string
        /// </summary>
        /// <param name="strCaption"></param>
        /// <param name="strTitle"></param>
        public static void Message_Warning(string strCaption)
        {
            MessageBox.Show(strCaption, stMsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Hàm hiển thị thông báo warning từ string lựa chọn YES/NO/CANCEL (yes = true/ no, cancel = false)
        /// </summary>
        /// <param name="strCaption"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        public static bool Message_WarningYNC(string strCaption)
        {
            return (MessageBox.Show(strCaption, stMsgTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes);
        }

        /// <summary>
        /// Hàm hiển thị thông báo warning từ string lựa chọn YES/NO (yes = true/ no = false)
        /// </summary>
        /// <param name="strCaption"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        public static bool Message_WarningYN(string strCaption)
        {
            return (MessageBox.Show(strCaption, stMsgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes);
        }

        /// <summary>
        /// Hàm hiển thị thông báo error từ string lựa chọn retry (retry = true)
        /// </summary>
        /// <param name="strCaption"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        public static bool Message_WarningConfirm(string strCaption)
        {
            return (MessageBox.Show(strCaption, stMsgTitle, MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Retry);
        }

        /// <summary>
        /// Hàm hiển thị thông báo error từ string
        /// </summary>
        /// <param name="strCaption"></param>
        /// <param name="strTitle"></param>
        public static void Message_Error(string strCaption)
        {
            MessageBox.Show(strCaption, stMsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Hàm hiển thị thông báo error từ exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="strTitle"></param>
        public static void Message_Error(System.Exception ex)
        {
            MessageBox.Show("LỖI TRONG QUÁ TRÌNH XỬ LÝ!\n\n" + ex.Message, stMsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Hàm hiển thị thông báo error lựa chọn YES/NO (yes = true/ no = false)
        /// </summary>
        /// <param name="strCaption"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        public static bool Message_ErrorConfirm(string strCaption)
        {
            return (MessageBox.Show(strCaption, stMsgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes);
        }

        /// <summary>
        /// Hàm hiển thị thông báo question lựa chọn YES/NO/CANCEL (yes = true/ no, cancel = false)
        /// </summary>
        /// <param name="strCaption"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        public static bool Message_Question(string strCaption)
        {
            return (MessageBox.Show(strCaption, stMsgTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes);
        }

        /// <summary>
        /// Hàm hiển thị thông báo question lựa chọn YES/NO (yes = true/ no = false)
        /// </summary>
        /// <param name="strCaption"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        public static bool Message_QuestionYN(string strCaption)
        {
            return (MessageBox.Show(strCaption, stMsgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
        }

        /// <summary>
        /// Hàm hiển thị thông báo question từ string
        /// </summary>
        /// <param name="strCaption"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        public static bool Message_QuestionConfirm(string strCaption)
        {
            return (MessageBox.Show(strCaption, stMsgTitle, MessageBoxButtons.RetryCancel, MessageBoxIcon.Question) == DialogResult.Retry);
        }
        #endregion

        #region Thay đổi kích thước combobox theo độ dài item
        // thay đổi size của combobox
        public static bool AdjustWidthComboBox(object sender)
        {
            try
            {
                // chọn đối tượng là combobox cần thay đổi kích thước
                ComboBox senderComboBox = (ComboBox)sender;

                int width = 0;
                Graphics g = senderComboBox.CreateGraphics();
                Font font = senderComboBox.Font;
                int newWidth;

                // hiển thị độ dài mặc định cho các combobox không dữ liệu
                if (senderComboBox.Items.Count == 0)
                {
                    senderComboBox.Width = 120;
                    return true;
                }

                // tìm item có độ dài lớn nhất và cộng thêm vào độ dài đó 1 khoảng bằng kích thước của item dropdownlist
                foreach (var item in senderComboBox.Items)
                {
                    DataRowView drw = item as DataRowView;
                    string sItem = (drw != null ? drw[senderComboBox.DisplayMember].ToString() : item.ToString());
                    newWidth = (int)g.MeasureString(sItem, font).Width;
                    if (width < newWidth)
                    {
                        width = newWidth;
                    }
                }
                senderComboBox.Width = width + 25;
                return true;
            }
            catch
            {
                return false;
            }
        }

        // sắp xếp các item trong panel khi combobox thay đổi size
        // chú ý phải sắp xếp thứ tự add các control vào panel trong Designer
        // this.pnlTop.Controls.Add(this.btnRefresh);
        public static void ChangeLocaltionInPanel(object sender)
        {
            Panel senderPanel = (Panel)sender;
            int newLocaltion = 10;
            //Chỉnh vị trí
            foreach (Control ctl in senderPanel.Controls)
            {
                if (ctl.Visible == true)
                {
                    int X = ctl.Location.X;
                    int Y = (-ctl.Height + senderPanel.Height) / 2;
                    ctl.Location = new System.Drawing.Point(newLocaltion, Y);
                    newLocaltion += (ctl.Width + 3);
                }
            }
        }

        /// <summary>
        /// định dạng cho tất cả các combobox trong panel
        /// </summary>
        /// <param name="sender">panel chứa các combobox cần định dạng</param>
        public static void FormatWidthComboBoxInPanel(object sender)
        {
            try
            {
                Panel senderPanel = (Panel)sender;
                foreach (Control ctrCombobox in senderPanel.Controls)
                {
                    if (ctrCombobox is ComboBox)
                        if (!AdjustWidthComboBox(ctrCombobox))
                            return;
                }
                ChangeLocaltionInPanel(senderPanel);
            }
            catch (Exception ex)
            {
                Message_Error("Lỗi khi định dạng lại các combobox:\n\n" + ex.Message.ToString());
            }
        }
        #endregion

        #region Custom Item cho RadListView
        /// <summary>
        /// Hiển thị User CA
        /// </summary>
        public class UserListVisualItem : SimpleListViewVisualItem
        {
            private LightVisualElement[] _elements;
            private StackLayoutPanel _layout;

            //Tạo các cột properties
            protected override void CreateChildElements()
            {
                base.CreateChildElements();

                this._layout = new StackLayoutPanel();
                this._layout.EqualChildrenWidth = true;
                this._layout.Margin = new Padding(30, 20, 0, 0);

                //Add cột vào layout
                _elements = new LightVisualElement[2];      //Số cột = 2
                for (int i = 0; i < _elements.Length; i++)
                {
                    _elements[i] = new LightVisualElement();
                    _elements[i].MinSize = new Size(150, 0);    //Độ rộng mặc định
                    _elements[i].NotifyParentOnMouseInput = true;
                    _elements[i].ShouldHandleMouseInput = false;
                    _elements[i].TextAlignment = ContentAlignment.MiddleLeft;
                    this._layout.Children.Add(_elements[i]);
                }

                this.Children.Add(this._layout);
            }

            //Kiểm tra để highlight màu đỏ
            private bool IsNotZero(ListViewDataItem item, string field)
            {
                return item[field] != null && item[field].ToString() != "0";
            }

            //Định dạng hiện thị cho các trường
            protected override void SynchronizeProperties()
            {
                base.SynchronizeProperties();

                RadElement element = this.FindAncestor<RadListViewElement>();

                if (element == null)
                {
                    return;
                }

                //Main text
                this.Text = "<html><span style=\"color:#141718;font-size:10.5pt;\"> " + this.Data["Name"] + "</span>";
                //Cột 0
                _elements[0].Text = "<html><span style=\"color:#010102;font-size:8.5pt;font-family:Segoe UI Semibold;\">" +
                    "Số CMND:<span style=\"color:#13224D;font-family:Segoe UI;\">" + this.Data["CMND"] + "</span>" +
                    "<br>Trạng thái:" + (this.IsNotZero(this.Data, "Status") ? "<span style=\"color:#13224D;font-family:Segoe UI;\">" + this.Data["StatusName"] : "<span style=\"color:#D71B0E;\">" + this.Data["StatusName"]) + "</span>" + "</span>";
                //Cột 1
                _elements[1].Text = "<html><span style=\"color:#010102;font-size:8.5pt;font-family:Segoe UI Semibold;\">" +
                    "Hiệu lực từ   :<span style=\"color:#13224D;font-family:Segoe UI;\">"
                    + (this.Data["ValidFrom"] == DBNull.Value ? "" : Convert.ToDateTime(this.Data["ValidFrom"]).ToString("dd/MM/yyyy")) + "</span>" +
                    "<br>Hiệu lực đến:<span style=\"color:#13224D;font-family:Segoe UI;\">"
                    + (this.Data["ValidTo"] == DBNull.Value ? "" : Convert.ToDateTime(this.Data["ValidTo"]).ToString("dd/MM/yyyy")) + "</span></span>";

                this.TextAlignment = ContentAlignment.TopLeft;
            }

            protected override Type ThemeEffectiveType
            {
                get
                {
                    return typeof(SimpleListViewVisualItem);
                }
            }
        }

        /// <summary>
        /// Hiển thị đơn vị dùng CA
        /// </summary>
        public class UnitListVisualItem : SimpleListViewVisualItem
        {
            private LightVisualElement[] _elements;
            private StackLayoutPanel _layout;

            //Tạo các cột properties
            protected override void CreateChildElements()
            {
                base.CreateChildElements();

                this._layout = new StackLayoutPanel();
                this._layout.EqualChildrenWidth = true;
                this._layout.Margin = new Padding(30, 20, 0, 0);

                //Add cột vào layout
                _elements = new LightVisualElement[2];      //Số cột = 2
                for (int i = 0; i < _elements.Length; i++)
                {
                    _elements[i] = new LightVisualElement();
                    _elements[i].MinSize = new Size(150, 0);    //Độ rộng mặc định
                    _elements[i].NotifyParentOnMouseInput = true;
                    _elements[i].ShouldHandleMouseInput = false;
                    _elements[i].TextAlignment = ContentAlignment.MiddleLeft;
                    this._layout.Children.Add(_elements[i]);
                }

                this.Children.Add(this._layout);
            }

            //Kiểm tra để highlight màu đỏ
            private bool IsNotZero(ListViewDataItem item, string field)
            {
                return item[field] != null && item[field].ToString() != "0";
            }

            //Định dạng hiện thị cho các trường
            protected override void SynchronizeProperties()
            {
                base.SynchronizeProperties();
                this.AutoSize = true;
                this.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;

                RadElement element = this.FindAncestor<RadListViewElement>();

                if (element == null)
                {
                    return;
                }

                //Main text
                this.Text = "<html><span style=\"color:#141718;font-size:10.5pt;\"> " + this.Data["Name"] + "</span>";
                //Cột 0
                _elements[0].Text = "<html><span style=\"color:#010102;font-size:8.5pt;font-family:Segoe UI Semibold;\">" +
                    "Mã đơn vị:<span style=\"color:#13224D;font-family:Segoe UI;\">" + this.Data["MaDV"] + "</span>" +
                    "<br>Trạng thái:" + (this.IsNotZero(this.Data, "Status") ? "<span style=\"color:#13224D;font-family:Segoe UI;\">" + this.Data["StatusName"] : "<span style=\"color:#D71B0E;\">" + this.Data["StatusName"]) + "</span>" + "</span>";
                //Cột 2
                _elements[1].Text = "<html><span style=\"color:#010102;font-size:8.5pt;font-family:Segoe UI Semibold;\">" +
                    "Hiệu lực từ: <span style=\"color:#13224D;font-family:Segoe UI;\">"
                    + (this.Data["ValidFrom"] == DBNull.Value ? "" : Convert.ToDateTime(this.Data["ValidFrom"]).ToString("dd/MM/yyyy")) + "</span>" +
                    "<br>Hiệu lực đến: <span style=\"color:#13224D;font-family:Segoe UI;\">"
                    + (this.Data["ValidTo"] == DBNull.Value ? "" : Convert.ToDateTime(this.Data["ValidTo"]).ToString("dd/MM/yyyy")) + "</span></span>";

                this.TextAlignment = ContentAlignment.TopLeft;
            }

            protected override Type ThemeEffectiveType
            {
                get
                {
                    return typeof(SimpleListViewVisualItem);
                }
            }
        }

        /// <summary>
        /// Hiển thị Hệ thống tích hợp CA
        /// </summary>
        public class ProgListVisualItem : SimpleListViewVisualItem
        {
            private LightVisualElement[] _elements;
            private StackLayoutPanel _layout;

            //Tạo các cột properties
            protected override void CreateChildElements()
            {
                base.CreateChildElements();

                this._layout = new StackLayoutPanel();
                this._layout.EqualChildrenWidth = true;
                this._layout.Margin = new Padding(30, 20, 0, 0);

                //Add cột vào layout
                _elements = new LightVisualElement[2];      //Số cột = 2
                for (int i = 0; i < _elements.Length; i++)
                {
                    _elements[i] = new LightVisualElement();
                    _elements[i].MinSize = new Size(150, 0);    //Độ rộng mặc định
                    _elements[i].NotifyParentOnMouseInput = true;
                    _elements[i].ShouldHandleMouseInput = false;
                    _elements[i].TextAlignment = ContentAlignment.MiddleLeft;
                    this._layout.Children.Add(_elements[i]);
                }

                this.Children.Add(this._layout);
            }

            //Kiểm tra để highlight màu đỏ
            private bool IsNotZero(ListViewDataItem item, string field)
            {
                return item[field] != null && item[field].ToString() != "0";
            }

            //Định dạng hiện thị cho các trường
            protected override void SynchronizeProperties()
            {
                base.SynchronizeProperties();
                this.AutoSize = true;
                this.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;

                RadElement element = this.FindAncestor<RadListViewElement>();

                if (element == null)
                {
                    return;
                }

                //Main text
                this.Text = "<html><span style=\"color:#141718;font-size:10.5pt;\"> " + this.Data["Name"] + "</span>";
                //Cột 0
                _elements[0].Text = "<html><span style=\"color:#010102;font-size:8.5pt;font-family:Segoe UI Semibold;\">" +
                    "Mã hệ thống:<span style=\"color:#13224D;font-family:Segoe UI;\">" + this.Data["ProgName"] + "</span>" +
                    "<br>Trạng thái:" + (this.IsNotZero(this.Data, "Status") ? "<span style=\"color:#13224D;font-family:Segoe UI;\">" + this.Data["StatusName"] : "<span style=\"color:#D71B0E;\">" + this.Data["StatusName"]) + "</span>" + "</span>";
                //Cột 2
                _elements[1].Text = "<html><span style=\"color:#010102;font-size:8.5pt;font-family:Segoe UI Semibold;\">" +
                    "Server Name: <span style=\"color:#13224D;font-family:Segoe UI;\">"
                    + (this.Data["ServerName"] == DBNull.Value ? "" : this.Data["ServerName"]) + "</span>" +
                    "<br>Database: <span style=\"color:#13224D;font-family:Segoe UI;\">"
                    + (this.Data["DBName"] == DBNull.Value ? "" : this.Data["DBName"]) + "</span></span>";

                this.TextAlignment = ContentAlignment.TopLeft;
            }

            protected override Type ThemeEffectiveType
            {
                get
                {
                    return typeof(SimpleListViewVisualItem);
                }
            }
        }

        /// <summary>
        /// Hiển thị đơn vị lấy từ CSDL_Chung
        /// </summary>
        public class DonViListVisualItem : SimpleListViewVisualItem
        {
            private LightVisualElement[] _elements;
            private StackLayoutPanel _layout;

            //Tạo các cột properties
            protected override void CreateChildElements()
            {
                base.CreateChildElements();

                this._layout = new StackLayoutPanel();
                this._layout.EqualChildrenWidth = true;
                this._layout.Margin = new Padding(30, 20, 0, 0);

                //Add cột vào layout
                _elements = new LightVisualElement[2];      //Số cột = 2
                for (int i = 0; i < _elements.Length; i++)
                {
                    _elements[i] = new LightVisualElement();
                    _elements[i].MinSize = new Size(150, 0);    //Độ rộng mặc định
                    _elements[i].NotifyParentOnMouseInput = true;
                    _elements[i].ShouldHandleMouseInput = false;
                    _elements[i].TextAlignment = ContentAlignment.MiddleLeft;
                    this._layout.Children.Add(_elements[i]);
                }

                this.Children.Add(this._layout);
            }

            //Kiểm tra để highlight màu đỏ
            private bool IsNotZero(ListViewDataItem item, string field)
            {
                return item[field] != null && item[field].ToString() != "0";
            }

            //Định dạng hiện thị cho các trường
            protected override void SynchronizeProperties()
            {
                base.SynchronizeProperties();
                this.AutoSize = true;
                this.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;

                RadElement element = this.FindAncestor<RadListViewElement>();

                if (element == null)
                {
                    return;
                }

                //Main text
                this.Text = "<html><span style=\"color:#141718;font-size:10.5pt;\"> " + this.Data["Name"] + "</span>";
                //Cột 0
                _elements[0].Text = "<html><span style=\"color:#010102;font-size:8.5pt;font-family:Segoe UI Semibold;\">" +
                    "Mã đơn vị:<span style=\"color:#13224D;font-family:Segoe UI;\">" + this.Data["MaDV"] + "</span>" + "</span>";
                //Cột 2
                _elements[1].Text = "<html><span style=\"color:#010102;font-size:8.5pt;font-family:Segoe UI Semibold;\">" +
                    "Tên tắt: <span style=\"color:#13224D;font-family:Segoe UI;\">" + this.Data["TenTat"] + "</span>" + "</span>";

                this.TextAlignment = ContentAlignment.TopLeft;
            }

            protected override Type ThemeEffectiveType
            {
                get
                {
                    return typeof(SimpleListViewVisualItem);
                }
            }
        }

        /// <summary>
        /// Hiển thị chứng thư số từ db
        /// </summary>
        public class CertListVisualItem : SimpleListViewVisualItem
        {
            private LightVisualElement[] _elements;
            private StackLayoutPanel _layout;

            //Tạo các cột properties
            protected override void CreateChildElements()
            {
                base.CreateChildElements();

                this._layout = new StackLayoutPanel();
                this._layout.EqualChildrenWidth = true;
                this._layout.Margin = new Padding(30, 20, 0, 0);

                //Add cột vào layout
                _elements = new LightVisualElement[2];      //Số cột = 2
                for (int i = 0; i < _elements.Length; i++)
                {
                    _elements[i] = new LightVisualElement();
                    _elements[i].MinSize = new Size(150, 0);    //Độ rộng mặc định
                    _elements[i].NotifyParentOnMouseInput = true;
                    _elements[i].ShouldHandleMouseInput = false;
                    _elements[i].TextAlignment = ContentAlignment.MiddleLeft;
                    this._layout.Children.Add(_elements[i]);
                }

                this.Children.Add(this._layout);
            }

            //Kiểm tra để highlight màu đỏ
            private bool IsNotZero(ListViewDataItem item, string field)
            {
                return item[field] != null && item[field].ToString() != "0";
            }

            //Định dạng hiện thị cho các trường
            protected override void SynchronizeProperties()
            {
                base.SynchronizeProperties();
                this.AutoSize = true;
                this.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;

                RadElement element = this.FindAncestor<RadListViewElement>();

                if (element == null)
                {
                    return;
                }

                //Main text
                this.Text = "<html><span style=\"color:#141718;font-size:10.5pt;\"> " + this.Data["NameCN"] + "</span>";
                //Cột 0
                _elements[0].Text = "<html><span style=\"color:#010102;font-size:8.5pt;font-family:Segoe UI Semibold;\">" +
                    "Issuer:<span style=\"color:#13224D;font-family:Segoe UI;\">" + this.Data["Issuer"] + "</span>" +
                    "<br>Trạng thái:" + (this.IsNotZero(this.Data, "Status") ? "<span style=\"color:#13224D;font-family:Segoe UI;\">" + this.Data["Status_Text"] : "<span style=\"color:#D71B0E;\">" + this.Data["Status_Text"]) + "</span>" + "</span>";
                //Cột 2
                _elements[1].Text = "<html><span style=\"color:#010102;font-size:8.5pt;font-family:Segoe UI Semibold;\">" +
                    "Hiệu lực từ: <span style=\"color:#13224D;font-family:Segoe UI;\">"
                    + (this.Data["ValidFrom"] == DBNull.Value ? "" : Convert.ToDateTime(this.Data["ValidFrom"]).ToString("dd/MM/yyyy")) + "</span>" +
                    "<br>Hiệu lực đến: <span style=\"color:#13224D;font-family:Segoe UI;\">"
                    + (this.Data["ValidTo"] == DBNull.Value ? "" : Convert.ToDateTime(this.Data["ValidTo"]).ToString("dd/MM/yyyy")) + "</span></span>";

                this.TextAlignment = ContentAlignment.TopLeft;
            }

            protected override Type ThemeEffectiveType
            {
                get
                {
                    return typeof(SimpleListViewVisualItem);
                }
            }
        }

        /// <summary>
        /// Hiển thị văn bản từ db
        /// </summary>
        public class FileTypeListVisualItem : SimpleListViewVisualItem
        {
            private LightVisualElement[] _elements;
            private StackLayoutPanel _layout;

            //Tạo các cột properties
            protected override void CreateChildElements()
            {
                base.CreateChildElements();

                this._layout = new StackLayoutPanel();
                this._layout.EqualChildrenWidth = true;
                this._layout.Margin = new Padding(30, 20, 0, 0);

                //Add cột vào layout
                _elements = new LightVisualElement[2];      //Số cột = 2
                for (int i = 0; i < _elements.Length; i++)
                {
                    _elements[i] = new LightVisualElement();
                    _elements[i].MinSize = new Size(150, 0);    //Độ rộng mặc định
                    _elements[i].NotifyParentOnMouseInput = true;
                    _elements[i].ShouldHandleMouseInput = false;
                    _elements[i].TextAlignment = ContentAlignment.MiddleLeft;
                    this._layout.Children.Add(_elements[i]);
                }

                this.Children.Add(this._layout);
            }

            //Kiểm tra để highlight màu đỏ
            private bool IsNotZero(ListViewDataItem item, string field)
            {
                return item[field] != null && item[field].ToString() != "0";
            }

            //Định dạng hiện thị cho các trường
            protected override void SynchronizeProperties()
            {
                base.SynchronizeProperties();
                this.AutoSize = true;
                this.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;

                RadElement element = this.FindAncestor<RadListViewElement>();

                if (element == null)
                {
                    return;
                }

                //Main text
                this.Text = "<html><span style=\"color:#141718;font-size:10.5pt;\"> " + this.Data["Name"] + "</span>";
                //Cột 0
                _elements[0].Text = "<html><span style=\"color:#010102;font-size:8.5pt;font-family:Segoe UI Semibold;\">" +
                    "<br>Hiệu lực từ: <span style=\"color:#13224D;font-family:Segoe UI;\">"
                    + (this.Data["DateStart"] == DBNull.Value ? "" : Convert.ToDateTime(this.Data["DateStart"]).ToString("dd/MM/yyyy")) + "</span></span>";
                //Cột 2
                _elements[1].Text = "<html><span style=\"color:#010102;font-size:8.5pt;font-family:Segoe UI Semibold;\">" +
                    "<br>Hiệu lực đến: <span style=\"color:#13224D;font-family:Segoe UI;\">"
                    + (this.Data["DateEnd"] == DBNull.Value ? "" : Convert.ToDateTime(this.Data["DateEnd"]).ToString("dd/MM/yyyy")) + "</span></span>";

                this.TextAlignment = ContentAlignment.TopLeft;
            }

            protected override Type ThemeEffectiveType
            {
                get
                {
                    return typeof(SimpleListViewVisualItem);
                }
            }
        }

        /// <summary>
        /// Hiển thị loại hồ sơ từ db
        /// </summary>
        public class ProfileTypeListVisualItem : SimpleListViewVisualItem
        {
            private LightVisualElement[] _elements;
            private StackLayoutPanel _layout;

            //Tạo các cột properties
            protected override void CreateChildElements()
            {
                base.CreateChildElements();

                this._layout = new StackLayoutPanel();
                this._layout.EqualChildrenWidth = true;
                this._layout.Margin = new Padding(30, 20, 0, 0);

                //Add cột vào layout
                _elements = new LightVisualElement[2];      //Số cột = 2
                for (int i = 0; i < _elements.Length; i++)
                {
                    _elements[i] = new LightVisualElement();
                    _elements[i].MinSize = new Size(150, 0);    //Độ rộng mặc định
                    _elements[i].NotifyParentOnMouseInput = true;
                    _elements[i].ShouldHandleMouseInput = false;
                    _elements[i].TextAlignment = ContentAlignment.MiddleLeft;
                    this._layout.Children.Add(_elements[i]);
                }

                this.Children.Add(this._layout);
            }

            //Kiểm tra để highlight màu đỏ
            private bool IsNotZero(ListViewDataItem item, string field)
            {
                return item[field] != null && item[field].ToString() != "0";
            }

            //Định dạng hiện thị cho các trường
            protected override void SynchronizeProperties()
            {
                base.SynchronizeProperties();
                this.AutoSize = true;
                this.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;

                RadElement element = this.FindAncestor<RadListViewElement>();

                if (element == null)
                {
                    return;
                }

                //Main text
                this.Text = "<html><span style=\"color:#141718;font-size:10.5pt;\"> " + this.Data["Name"] + "</span>";
                //Cột 0
                _elements[0].Text = "<html><span style=\"color:#010102;font-size:8.5pt;font-family:Segoe UI Semibold;\">" +
                    "<br>Hiệu lực từ: <span style=\"color:#13224D;font-family:Segoe UI;\">"
                    + (this.Data["DateStart"] == DBNull.Value ? "" : Convert.ToDateTime(this.Data["DateStart"]).ToString("dd/MM/yyyy")) + "</span></span>";
                //Cột 2
                _elements[1].Text = "<html><span style=\"color:#010102;font-size:8.5pt;font-family:Segoe UI Semibold;\">" +
                    "<br>Hiệu lực đến: <span style=\"color:#13224D;font-family:Segoe UI;\">"
                    + (this.Data["DateEnd"] == DBNull.Value ? "" : Convert.ToDateTime(this.Data["DateEnd"]).ToString("dd/MM/yyyy")) + "</span></span>";

                this.TextAlignment = ContentAlignment.TopLeft;
            }

            protected override Type ThemeEffectiveType
            {
                get
                {
                    return typeof(SimpleListViewVisualItem);
                }
            }
        }

        #endregion

        #region Combobox item
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        #endregion

        /// <summary>
        /// Kiểm tra chuỗi truyền vào HSM: giới hạn độ dài và ký tự không dấu
        /// </summary>
        /// <param name="s"></param>
        /// <param name="min_length"></param>
        /// <param name="max_length"></param>
        /// <returns></returns>
        public static bool CheckStringHSM(string s, int min_length, int max_length)
        {
            if (s.Length < min_length || s.Length > max_length)
                return false;

            foreach (char c in s)
            {
                if (c <= 127)
                    continue;
                else
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Hàm sinh chuỗi ngẫu nhiên
        /// </summary>
        /// <param name="codeCount"></param>
        /// <returns></returns>
        public static string CreateRamdomString(int codeCount)
        {
            string allChar = @"0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(36);
                if (temp != -1 && temp == t)
                {
                    return CreateRamdomString(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        /// <summary>
        /// Copy số, thực hiện các thao tác
        /// ctrl + x: cut
        /// ctrl + c: copy
        /// ctrl + v: paste
        /// ctrl + delete: delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void C1FlexGrid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control)
                {
                    C1FlexGrid _grid = sender as C1FlexGrid;
                    switch (e.KeyCode)
                    {
                        //copy
                        case Keys.C:
                            System.Windows.Forms.Clipboard.Clear();
                            System.Windows.Forms.Clipboard.SetDataObject(_grid.Clip, true);
                            break;
                        //cut
                        case Keys.X:
                            int colsFrozen = _grid.Cols.Frozen;
                            if (_grid.ColSel <= _grid.Cols.Frozen - 1)
                            {
                                return;
                            }
                            if (_grid.AllowEditing == false || _grid.Cols[_grid.ColSel].AllowEditing == false)
                            {
                                return;
                            }
                            System.Windows.Forms.Clipboard.SetDataObject(_grid.Clip);
                            CellRange rg = _grid.Selection;
                            rg.Data = null;
                            break;
                        //paste
                        case Keys.V:
                            colsFrozen = _grid.Cols.Frozen;
                            if (_grid.ColSel <= _grid.Cols.Frozen - 1)
                            {
                                return;
                            }
                            if (_grid.AllowEditing == false)
                            {
                                return;
                            }
                            IDataObject data1 = System.Windows.Forms.Clipboard.GetDataObject();
                            if (data1.GetDataPresent(Type.GetType("System.String")))
                            {
                                _grid.Select(_grid.Row, _grid.Col, _grid.Rows.Count - 1, _grid.Cols.Count - 1, false);
                                string tempClip = Convert.ToString(data1.GetData(Type.GetType("System.String"))).ToString();
                                int _lenght = tempClip.LastIndexOf("\r\n");// -1;
                                if (_lenght < 0)
                                    _lenght = tempClip.Length;
                                //Không paste vào ô readonly
                                tempClip = tempClip.Substring(0, _lenght);
                                int i = _grid.Row; int j = _grid.Col;
                                string tempCell = "";
                                foreach (char c in tempClip)
                                {
                                    if (c == '\r')
                                    {
                                        if (_grid.Cols[j].AllowEditing != false)
                                            _grid.Cols[j][i] = tempCell;
                                        i++; j = _grid.Col; tempCell = "";
                                    }
                                    else if (c == '\t')
                                    {
                                        if (_grid.Cols[j].AllowEditing != false)
                                            _grid.Cols[j][i] = tempCell;
                                        j++; tempCell = "";
                                    }
                                    else
                                    {
                                        tempCell += c;
                                    }
                                }
                                if (_grid.Cols[j].AllowEditing != false)
                                    _grid.Cols[j][i] = tempCell;
                                //--
                                _grid.Select(_grid.Row, _grid.Col, false);
                            }
                            break;
                        //delete
                        case Keys.Delete:
                            colsFrozen = _grid.Cols.Frozen;
                            if (_grid.ColSel <= _grid.Cols.Frozen - 1)
                            {
                                return;
                            }
                            if (_grid.AllowEditing == false || _grid.Cols[_grid.ColSel].AllowEditing == false)
                            {
                                return;
                            }
                            CellRange rg1 = _grid.Selection;
                            rg1.Data = null;
                            break;
                    }
                }
            }
            catch { }
        }
    }

    public class GrowLabel : Label
    {
        private bool mGrowing;
        public GrowLabel()
        {
            this.AutoSize = false;
        }
        private void resizeLabel()
        {
            if (mGrowing) return;
            try
            {
                mGrowing = true;
                Size sz = new Size(this.Width, Int32.MaxValue);
                sz = TextRenderer.MeasureText(this.Text, this.Font, sz, TextFormatFlags.WordBreak);
                this.Height = sz.Height;
            }
            finally
            {
                mGrowing = false;
            }
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            resizeLabel();
        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            resizeLabel();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            resizeLabel();
        }
    }
}