using System;
using System.Collections.Generic;
using System.Web.UI;
using EBird.BusinessEntity;
using EBird.DataAccess;
using EBird.Common;
using System.Collections;
using System.Web.UI.HtmlControls;

namespace EBird.Web.App.Pages
{
    public partial class WaitingRoom : System.Web.UI.Page
    {
        EBirdUtility objUtil = new EBirdUtility();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Page.RouteData.Values["Id"] != null)
                {
                    WebinarDA objWebinarDA = new WebinarDA();
                    string URLkey = Page.RouteData.Values["Id"].ToString();

                    bool isPreview = false;
                   // URLkey.Split(new char[] { '_' });

                    if (URLkey.IndexOf("_") > 1)
                    {
                        isPreview = true;
                        ArrayList arr = objUtil.StringToArrayList(URLkey, new char[] { '_' });
                        if (arr.Count > 1)
                        {
                            ArrayList arr1 = objUtil.StringToArrayList(arr[0].ToString(), new char[] { '$' });
                            layoutCSS.Href = "~/Styles/layout/theme" + arr1[1].ToString() + ".css";
                        }
                        URLkey = arr[1].ToString();
                    }


                    int WebinarID = objWebinarDA.getWebinarIDFromURLKey("AUD", URLkey);
                    if (WebinarID != 0)
                    {
                        List<WebinarBE> objWebDetail = objWebinarDA.GetWebinarDetailDA(WebinarID);
                        if (objWebDetail.Count > 0)
                        {
                            lblWebinarTitle.Text = objWebDetail[0].Title;
                            lblWebinarDesc.Text = objWebDetail[0].Description;

                            if (DateTime.Compare(objWebDetail[0].StartDate, DateTime.Now) != -1)
                            {

                                MasterDA objMas = new MasterDA();
                                List<TimeZoneBE> tmzone = objMas.getTimeZoneName(objWebDetail[0].TimeZoneID);
                                if (tmzone.Count > 0)
                                    ltrInstruction.Text = "This presentation will begin on " + objWebDetail[0].StartDate.ToLongDateString() + " at " + Convert.ToDateTime(objWebDetail[0].StartTime).ToString("h:mm tt") + " " + tmzone[0].ShortTimeZoneName + ".<br />"
                                        + "<br />Audience members may arrive 15 minutes in advance of this time.";
                                else
                                    ltrInstruction.Text = "This presentation will begin on " + objWebDetail[0].StartDate.ToLongDateString() + " at " + Convert.ToDateTime(objWebDetail[0].StartTime).ToString("h:mm tt") + "<br />"
                                        + "<br />Audience members may arrive 15 minutes in advance of this time.";
                            }
                            else
                            {
                                ltrInstruction.Text = "This is past event...";
                                btnLaunch.Visible = false;
                            }

                            plRegPresenter1.WebinarID = WebinarID.ToString();

                            if (!isPreview)
                            {
                                List<WebinarTheme> objWebTheme = objWebinarDA.getWebinarTheme(WebinarID);
                                if (objWebTheme.Count > 0)
                                    layoutCSS.Href = "~/Styles/layout/theme" + objWebTheme[0].ThemeLayoutID.ToString() + ".css";
                            }

                            List<WebinarResource> objWRes = objWebinarDA.getRegFormResoures(WebinarID);
                            if (objWRes.Count > 0)
                            {
                                plLogos1.WebinarID = WebinarID;
                                if (objWRes[0].ResourceType.ToUpper() == "BANNER")
                                    dvTitle.Visible = false;
                            }
                            else
                                dvLogo.Visible = false;

                        }
                        //List<WebinarTheme> objWT = objWebinarDA.getWebinarTheme(WebinarID);
                        //if (objWT.Count > 0)
                        //{
                        //    //lblWebinarTitle.ForeColor = System.Drawing.ColorTranslator.FromHtml(objWT[0].PriThemeColor);
                        //    //lblWebinarDesc.ForeColor = System.Drawing.ColorTranslator.FromHtml(objWT[0].SecThemeColor);
                            
                        //    plLogos1.LogoID1 = objWT[0].LogoID1;
                        //    plLogos1.LogoID2 = objWT[0].LogoID2;
                        //    plLogos1.HeaderType = objWT[0].HeaderType;
                        //    //Pass values to Presenter user controls
                            
                        //}
                    }
                }
            }
        }
    }
}