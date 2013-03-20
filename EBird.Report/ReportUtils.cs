using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;

namespace EBird.Report
{
    public class ReportUtils
    {
        public string getStyleDefinitions(string CSSFilePath)
        {
            if (File.Exists(CSSFilePath))
            {
                string fileContent = File.ReadAllText(CSSFilePath);
                return fileContent;
            }
            else
                return "";
        }

        public string getEmailRptHeader()
        {
            string tplFile = EnumUtils.stringValueOf(ReportTemplates.EmailHeader);
            if (File.Exists(Constant.DocTemplate + tplFile))
            {
                string fileContent = File.ReadAllText(Constant.DocTemplate + tplFile);
                return fileContent;
            }
            else
                return "";
        }

        public string getEmailRptHeader(int webinarID)
        {
            WebinarDA objWebinarDA = new WebinarDA();
            List<WebinarResource> objWRes = objWebinarDA.getRegFormResoures(webinarID);
            int Rec = objWRes.Count;
            StringBuilder sb = new StringBuilder("<table width='100%'><tr>");

            DocumentDA objDocDA = new DocumentDA();
             
            if (Rec > 0)
            {
                if (objWRes[0].ResourceType.ToUpper() == "BANNER")
                {
                    //sb.Append("<td><img src='" + Common.Constant.BaseURL + "Pages/logo/" + objWRes[0].DocID.ToString() + "'></td>");
                    sb.Append("<td><img src='" + objDocDA.GetDocumentPath(Convert.ToInt32(objWRes[0].DocID), true, true) + "'></td>");
                }
                else
                {
                    Rec = (int)(100 / objWRes.Count);
                    for (int idx = 0; idx < objWRes.Count; idx++)
                    {
                        if (idx == objWRes.Count-1)
                            sb.Append("<td valign=middle width='" + Rec.ToString() + "%' align='right'><img src='" + objDocDA.GetDocumentPath(Convert.ToInt32(objWRes[idx].DocID), true, true) + "'></td>");
                        else
                        {
                            if (idx % 2 != 0 && idx != 0)
                                sb.Append("<td valign=middle width='" + Rec.ToString() + "%' align='center'><img src='" + objDocDA.GetDocumentPath(Convert.ToInt32(objWRes[idx].DocID), true, true) + "'></td>");
                            else
                                sb.Append("<td valign=middle width='" + Rec.ToString() + "%' ><img src='" + objDocDA.GetDocumentPath(Convert.ToInt32(objWRes[idx].DocID), true, true) + "'></td>");
                        }
                    }
                }
            }
            sb.Append("</tr></table>");
            return sb.ToString(); 
        }

        //public string getEmailRptHeader(int webinarID)
        //{
        //    WebinarDA objWebinarDA = new WebinarDA();
        //    List<WebinarResource> objWRes = objWebinarDA.getRegFormResoures(intWebinarID);
        //    int Rec = objWRes.Count;
        //    if (Rec > 0)
        //    {
        //        HtmlTableRow trImageRow = new HtmlTableRow();
        //        if (objWRes[0].ResourceType == "BANNER")
        //        {
        //            //phBanner.Visible = true;
        //            //imgBanner.Src = "~/handler/showImage.ashx?ID=" + objWRes[idx].DocID.ToString();

        //            HtmlTableCell tdImageCell = new HtmlTableCell();
        //            Image imgLogo = new Image();
        //            imgLogo.ID = "id0";
        //            imgLogo.ImageUrl = "~/handler/showImage.ashx?ID=" + objWRes[0].DocID.ToString();
        //            tdImageCell.Controls.Add(imgLogo);
        //            trImageRow.Cells.Add(tdImageCell);
        //            tLogo.Rows.Add(trImageRow);
        //        }
        //        else
        //        {
        //            //phLogo.Visible = true;
        //            //Rec = (int) Math.Round(100 / objWRes.Count);
        //            Rec = (int)(100 / objWRes.Count);
        //            for (int idx = 0; idx < objWRes.Count; idx++)
        //            {
        //                HtmlTableCell tdImageCell = new HtmlTableCell();
        //                Image imgLogo = new Image();
        //                imgLogo.ID = "id" + idx.ToString();
        //                imgLogo.ImageUrl = "~/handler/showImage.ashx?ID=" + objWRes[idx].DocID.ToString();
        //                tdImageCell.Controls.Add(imgLogo);
        //                trImageRow.Cells.Add(tdImageCell);
        //                trImageRow.Cells[idx].VAlign = "middle";
        //                trImageRow.Cells[idx].Width = Rec.ToString() + "%";
        //                if (idx == objWRes.Count)
        //                    trImageRow.Cells[idx].Align = "right";
        //                else
        //                {
        //                    if (idx % 2 != 0 && idx != 0)
        //                        trImageRow.Cells[idx].Align = "middle";
        //                }
        //            }
        //            tLogo.Rows.Add(trImageRow);
        //        }
        //    }
        //}

        public string getEmailRptFooter()
        {
            string tplFile = EnumUtils.stringValueOf(ReportTemplates.EmailFooter);
            if (File.Exists(Constant.DocTemplate + tplFile))
            {
                string fileContent = File.ReadAllText(Constant.DocTemplate + tplFile);
                return fileContent;
            }
            else
                return "";
        }

        public string getSystemRequirement(string audiurl = "")
        {
            WebinarAllEmailTagsBO WebInvSysReq = new WebinarAllEmailTagsBO();
            if (audiurl == null) audiurl = "";
            string tplFile;
            if (audiurl != "")
                tplFile = EnumUtils.stringValueOf(ReportTemplates.WebinarInviteSysReq);
            else
                tplFile = EnumUtils.stringValueOf(ReportTemplates.SystemReq);

            if (File.Exists(Constant.DocTemplate + tplFile))
            {
                string fileContent = File.ReadAllText(Constant.DocTemplate + tplFile);
                fileContent = fileContent.Replace("##AUDI_URL##", "<a href='" + audiurl + "'>" + audiurl + "</a>");
                return fileContent;
            }
            else
                return "";
        }

        public string getCSSPath(int themeID)
        {
            return Constant.DocRepoCSS + "rptCSS" + themeID.ToString() + ".css";
        }

        public string getCSSDefns(int themeID)
        {
            string str1 = getStyleDefinitions(Constant.DocRepoCSS + "rptCSS" + themeID.ToString() + ".css");
            return "<style>" + str1 + "</style>";
        }

        public string getCSSDefns(int themeID, string inStr)
        {
            string rtnStr = string.Empty;
            rtnStr = inStr;
            ReportDA objRpt = new ReportDA();
            List<themeCSSBO> objTheme = objRpt.getThemeCSSValues(themeID);
            if (objTheme.Count > 0)
            {
                rtnStr = rtnStr.Replace("##STYLE_BG##", objTheme[0].shade1);
                rtnStr = rtnStr.Replace("##STYLE_BORDER##", objTheme[0].shade2); 
            }
            return rtnStr;
        }

    }

    
    public enum ReportTemplates
    {
        [DescriptionAttribute("WeeklyStatusReportEmail.tpl")] WeeklyStatus,
        [DescriptionAttribute("reminderEmail.tpl")] ReminderEmail,
        [DescriptionAttribute("regConfirm.tpl")] RegistrationConfirmation,
        [DescriptionAttribute("AttendeeFollowup.tpl")] AttendeeFollowUps,
        [DescriptionAttribute("Invitation.tpl")] WebinarInvitation,
        [DescriptionAttribute("NonAttendeeFollowup.tpl")] NonAttendeeFollowUps,
        [DescriptionAttribute("regExist.tpl")] RegisteredAlready,
        [DescriptionAttribute("EmailHeader.tpl")] EmailHeader,
        [DescriptionAttribute("EmailFooter.tpl")] EmailFooter,
        [DescriptionAttribute("sysReq.tpl")] SystemReq,
        [DescriptionAttribute("WebinarInvitation.tpl")] WebinarInviteSysReq
    }

    public enum ReportStyles
    {
        [DescriptionAttribute("dvTable600.css")]
        Tabular600CSS,
        [DescriptionAttribute("dvTable500.css")]
        Tabular500CSS,
        [DescriptionAttribute("dvTable450.css")]
        Tabular450CSS
    }

    public class StyleTags
    {
        public string mainContainer { get { return ".container {width: 600px;margin-left: auto;margin-right: auto;border: 3px solid ######;background-color: #fff;line-height:20px;border-radius: 5px;min-height: 200px;}"; } }
        public string contentContainer { get { return " div#content{margin: 10px 10px 10px 10px;}"; } }
    }

    public class EnumUtils
    {
        public static string stringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static object enumValueOf(string value, Type enumType)
        {
            string[] names = Enum.GetNames(enumType);
            foreach (string name in names)
            {
                if (stringValueOf((Enum)Enum.Parse(enumType, name)).Equals(value))
                {
                    return Enum.Parse(enumType, name);
                }
            }

            throw new ArgumentException("The string is not a description or value of the specified enum.");
        }

    }
}
