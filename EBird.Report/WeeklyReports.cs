using System;
using System.Text;
using System.Collections.Generic;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using EBird.Framework;

namespace EBird.Report
{
    public class WeeklyReports
    {
        ReportDA objReportDA = new ReportDA();

        public string userWebinarReport(int userID)
        {
            string tplFile = EnumUtils.stringValueOf(ReportTemplates.WeeklyStatus); 

            TemplateMgmt objTemplateMgmt = new TemplateMgmt();
            string tpl = objTemplateMgmt.GetWeeklyStatusReport(Constant.DocTemplate + tplFile);
            if (tpl != "")
            {
                
                List<DailyStatusReportBO> objRpt = objReportDA.getWeeklyStatusReport(userID);
                if (objRpt.Count > 0)
                {
                    tpl = tpl.Replace("##FIRSTNAME##", objRpt[0].userFirstName);
                    tpl = tpl.Replace("##WEBINARCOUNT##", objRpt[0].NoOfWebinar.ToString());
                    tpl = tpl.Replace("##NEXTWEBINAR##", objRpt[0].NextWebinar);
                    tpl = tpl.Replace("##DAYSAWAY##", objRpt[0].LastWebinarDaysAway.ToString());
                }
                tpl = tpl.Replace("##TAB_4WEEK##", userWebinarListingReport(userID, 4));
            }
            return tpl;
        }

        public string userWebinarListingReport(int userID, int NoOfWeeks)
        {
            ReportUtils objRptUtil = new ReportUtils();
            string styleOut = objRptUtil.getStyleDefinitions(Constant.DocRepoCSS + EnumUtils.stringValueOf(ReportStyles.Tabular600CSS));
            if (styleOut != "")
                styleOut = "<style>" + styleOut + "</style>";

            List<WebinarInfoListBO> objWList = objReportDA.getWebinarWeeklyList(userID,NoOfWeeks);
           
           StringBuilder sb = new StringBuilder("<div class=\"divTable\">");
           sb.Append("<div class=\"headRow\">");
           sb.Append("<div class=\"divCell1\" align=\"left\">Your Upcoming Webinars</div>");
           sb.Append("<div class=\"divCell2\" align=\"center\">When</div>");
           sb.Append("<div class=\"divCell3\" align=\"center\">Registrants</div></div>");
           //sb.Append("<div class=\"divCell4\" align=\"center\">Actions</div></div>");
           if (objWList.Count > 0)
           {
               for (int idx = 0; idx < objWList.Count; idx++)
               {
                   if (idx % 2 == 0)
                       sb.Append("<div class=\"divAltRow\">");
                   else
                       sb.Append("<div class=\"divRow\">");
                   sb.Append("<div class=\"divCell1\" align=\"left\">" + objWList[idx].UpcomingWebinar + "</div>");
                   sb.Append("<div class=\"divCell2\" align=\"center\">" + objWList[idx].When + "</div>");
                   sb.Append("<div class=\"divCell3\" align=\"center\">" + objWList[idx].Registrants + "</div></div>");
                   //sb.Append("<div class=\"divCell4\" align=\"center\">Action</div></div>");

               }
           }
           sb.Append("</div>");
           return styleOut + sb.ToString();
        }
    }
}
