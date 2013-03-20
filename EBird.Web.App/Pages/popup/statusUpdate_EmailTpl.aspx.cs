using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Web.UI;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using EBird.Framework;
using EBird.Report;

namespace EBird.Web.App.Pages.popup
{
    public partial class statusUpdate_EmailTpl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TemplateMgmt objTemplateMgmt = new TemplateMgmt();
            //string tpl = objTemplateMgmt.GetWeeklyStatusReport(Constant.DocTemplate + "weeklyStatusReportEmail.tpl");
            //if (tpl != "")
            //{
            //    ReportDA objReportDA = new ReportDA();
            //    List<DailyStatusReportBO> objRpt = objReportDA.getDailyStatusReport(Convert.ToInt32(Session["UserID"]));
            //    if (objRpt.Count > 0)
            //    {
            //        tpl = tpl.Replace("##FIRSTNAME##", objRpt[0].userFirstName);
            //        tpl = tpl.Replace("#WEBINARCOUNT", objRpt[0].NoOfWebinar.ToString() );
            //        tpl = tpl.Replace("##NEXTWEBINAR##", objRpt[0].NextWebinar);
            //        tpl = tpl.Replace("##DAYSAWAY##", objRpt[0].LastWebinarDaysAway.ToString());
            //    }
            //}
            ReportUtils objRptUtil = new ReportUtils();
            ltrStyle.Text = objRptUtil.getCSSDefns(0);
            WeeklyReports objRpt = new WeeklyReports();
            ltrContent.Text = objRpt.userWebinarReport(Convert.ToInt32(Session["UserID"]));
        }
    }
}