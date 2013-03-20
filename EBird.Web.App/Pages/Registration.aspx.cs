using System;
using System.Collections.Generic;
using System.Web.UI;
using EBird.BusinessEntity;
using EBird.DataAccess;

namespace EBird.Web.App.Pages
{
    public partial class Registration : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Page.RouteData.Values["Id"] != null)
                {
                    setPlaceHolder(true);
                    WebinarDA objWebinarDA = new WebinarDA();
                    string URLkey = Page.RouteData.Values["Id"].ToString();
                    int WebinarID = objWebinarDA.getWebinarIDFromURLKey("REg", URLkey);
                    if (WebinarID != 0)
                    {
                        List<WebinarBE> objWebDetail = objWebinarDA.GetWebinarDetailDA(WebinarID);
                        if (objWebDetail.Count > 0)
                        {
                            lblWebinarTitle.Text = objWebDetail[0].Title;
                            lblWebinarDesc.Text = objWebDetail[0].Description;
                            //Pass values to DateTime user controls
                            MasterDA objMas = new MasterDA();
                            List<TimeZoneBE> tmzone = objMas.getTimeZoneName(objWebDetail[0].TimeZoneID);
                            if (tmzone.Count > 0)
                                plDateTime1.WebinarDateTime = objWebDetail[0].StartDate.ToLongDateString() + "<br>" + Convert.ToDateTime(objWebDetail[0].StartTime).ToLongTimeString() + "<br>" + tmzone[0].TimeZoneName; 
                            else
                                plDateTime1.WebinarDateTime = objWebDetail[0].StartDate.ToLongDateString() + "<br>" + Convert.ToDateTime(objWebDetail[0].StartTime).ToLongTimeString(); 
                            
                        }
                        List<WebinarTheme> objWT = objWebinarDA.getWebinarTheme(WebinarID);
                        if (objWT.Count > 0)
                        {
                            layoutCSS.Href = "~/Styles/layout/layout" + objWT[0].ThemeLayoutID.ToString() + ".css";
                            lblWebinarTitle.ForeColor = System.Drawing.ColorTranslator.FromHtml(objWT[0].PriThemeColor);
                            lblWebinarDesc.ForeColor = System.Drawing.ColorTranslator.FromHtml(objWT[0].SecThemeColor);
                            regPage.Style.Add("font-family",objWT[0].ThemeFontName);
                            regPage.Style.Add("font-size", "12px");
                            //Pass values to Logo user control
                            plLogos1.WebinarID = objWT[0].WebinarID;
                            //plLogos1.LogoID1 = objWT[0].LogoID1;
                            //plLogos1.LogoID2 = objWT[0].LogoID2;
                            plLogos1.HeaderType = objWT[0].HeaderType;
                            //Pass values to registration form user controls
                            plRegForm1.WebinarID = WebinarID.ToString();
                            //Pass values to Presenter user controls
                            plRegPresenter1.WebinarID = WebinarID.ToString();
                        }
                    }
                    else
                        setPlaceHolder(false); 
                }
                else
                    setPlaceHolder(false);
            }
        }

        private void setPlaceHolder(bool val)
        {
            phHeader.Visible = val;
            phContent.Visible = val;
            phFooter.Visible = val;
            phError.Visible = !val;
            if (val==false)
                layoutCSS.Href = "~/Styles/layout/layout1.css";
        }
    }
}