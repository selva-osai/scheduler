using System;
using EBird.Report;

namespace EBird.Web.App.Pages.Reports
{
    public partial class popRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ReportUtils objRptUtil = new ReportUtils();
                ltrStyle.Text = objRptUtil.getCSSDefns(1);
                WeeklyReports objRpt = new WeeklyReports();
                ltrContent.Text = objRpt.userWebinarReport(Convert.ToInt32(Session["UserID"]));
                ltrHeader.Text = objRptUtil.getEmailRptHeader();
                ltrFooter.Text = objRptUtil.getEmailRptFooter();
            }
        }
    }
}