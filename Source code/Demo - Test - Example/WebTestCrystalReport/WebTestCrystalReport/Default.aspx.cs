using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Text;

namespace WebTestCrystalReport
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection con = new SqlConnection("Data Source=TOANTK-PC\\SQLEXPRESS;Initial Catalog=DEMO;Persist Security Info=True;User ID=sa;Password=sa123");
                dsStudent ds = new dsStudent();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT MaSV, Hoten, Email FROM dbo.Students", con);
                adapter.Fill(ds.viewStudent);
                crptStudent report = new crptStudent();
                report.SetDataSource(ds);
                CrystalReportViewer1.ReportSource = report;
            }
        }
    }
}
