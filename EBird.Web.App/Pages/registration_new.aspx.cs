using System;
using System.Collections.Generic;
using System.Web.UI;
using EBird.BusinessEntity;
using EBird.DataAccess;
using EBird.Common;
using System.Collections;
using EBird.Framework;

namespace EBird.Web.App.Pages
{
    public partial class registration_new : System.Web.UI.Page
    {
        WebinarDA objWebinarDA = new WebinarDA();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Page.RouteData.Values["Id"] != null)
                {
                    EBirdUtility objUtil = new EBirdUtility();

                    setPlaceHolder(true);
                    string URLkey = Page.RouteData.Values["Id"].ToString();
                    bool isPreview = false;
                    URLkey.Split(new char[] { '_' });

                    if (URLkey.IndexOf("_") > 1)
                    {
                        Response.Redirect("/Regpreview/" + URLkey);
                        Response.End();
                        isPreview = true;
                        if (isPreview == true)
                        {
                            btnRefCol1.Visible = false;
                            Predummy3.Visible = true;
                        }
                        else
                        {
                            btnRefCol1.Visible = true;
                            Predummy3.Visible = false;
                        }
                        ArrayList arr = objUtil.StringToArrayList(URLkey, new char[] { '_' });
                        if (arr.Count > 1)
                        {
                            ArrayList arr1 = objUtil.StringToArrayList(arr[0].ToString(), new char[] { '$' });
                            layoutCSS.Href = "~/Styles/layout/theme" + arr1[1].ToString() + ".css";

                            if (arr1[0].ToString().Substring(0, 1) == "1")
                                dvSummary.Visible = true;
                            else
                                dvSummary.Visible = false;

                            if (arr1[0].ToString().Substring(1, 1) == "1")
                                dvSpeaker.Visible = true;
                            else
                                dvSpeaker.Visible = false;
                        }
                        URLkey = arr[1].ToString();
                    }

                    
                    int WebinarID = objWebinarDA.getWebinarIDFromURLKey("REg", URLkey);
                    hWebinarID.Value = WebinarID.ToString();
                    if (WebinarID != 0)
                    {
                        #region getting the pagelet setting
                        List<WebinarRegistration> objWebReg = objWebinarDA.getWebinarRegistration(WebinarID);
                        if (objWebReg.Count > 0)
                        {
                            if (!isPreview)
                            {
                                dvLogo.Visible = objWebReg[0].IncludeLogoBanner;
                                dvSpeaker.Visible = objWebReg[0].IncludeSpeakerBio;
                                dvSummary.Visible = objWebReg[0].IncludeSummary;
                            }
                            if (!dvSpeaker.Visible && !dvSummary.Visible)
                            {
                                phAll.Visible = false;
                                phOpt1.Visible = true;
                                //layoutCSS.Href = "~/Styles/layout1/layout2.css";

                                plPreEmail2.setSingleColumnAttributes();
                                dvPre.Attributes.Remove("class");
                                dvPre.Attributes.Add("class", "Regi-Top-Sec1N");
                            }

                            // Check registration status
                            if (objWebReg[0].isRegistrationEnabled)
                            {
                                plRegForm1.Visible = true;
                                plRegForm2.Visible = true;
                                lblRegStatus.Visible = false;
                            }
                            else
                            {
                                plRegForm1.Visible = false;
                                plRegForm2.Visible = false;
                                lblRegStatus.Visible = true;
                            }


                        }
                        #endregion

                        if (!isPreview)
                        {
                            List<WebinarTheme> objWebTheme = objWebinarDA.getWebinarTheme(WebinarID);
                            if (objWebTheme.Count > 0)
                            {
                                layoutCSS.Href = "~/Styles/layout/theme" + objWebTheme[0].ThemeLayoutID.ToString() + ".css";
                            }
                        }

                        string eventTime = "";

                        List<WebinarBE> objWebDetail = objWebinarDA.GetWebinarDetailDA(WebinarID);
                        if (objWebDetail.Count > 0)
                        {
                            List<WebinarResource> objWRes = objWebinarDA.getRegFormResoures(WebinarID);

                            if (objWRes.Count > 0)
                            {
                                if (objWRes[0].ResourceType.ToUpper() == "BANNER")
                                    dvTitle.Visible = false;
                            }
                            else
                                dvLogo.Visible = false;

                            lblWebinarTitle.Text = objWebDetail[0].Title;
                            if (dvSummary.Visible && objWebDetail[0].Description.Trim() != "")
                                lblWebinarDesc.Text = objWebDetail[0].Description;
                            else
                                dvSummary.Visible = false;
                            //Pass values to DateTime user controls
                            MasterDA objMas = new MasterDA();
                            List<TimeZoneBE> tmzone = objMas.getTimeZoneName(objWebDetail[0].TimeZoneID);
                            if (tmzone.Count > 0)
                                eventTime = objWebDetail[0].StartDate.ToLongDateString() + "<br>" + Convert.ToDateTime(objWebDetail[0].StartTime).ToString("h:mm tt") + " - " + Convert.ToDateTime(objWebDetail[0].EndTime).ToString("h:mm tt") + "<br>" + tmzone[0].ShortTimeZoneName;
                            else
                                eventTime = objWebDetail[0].StartDate.ToLongDateString() + "<br>" + Convert.ToDateTime(objWebDetail[0].StartTime).ToLongTimeString();

                            if (phOpt1.Visible)
                                plDateTime2.WebinarDateTime = eventTime;
                            else
                                plDateTime1.WebinarDateTime = eventTime;
                        }

                        //Pass values to Logo user control
                        if (dvLogo.Visible)
                            plLogos1.WebinarID = WebinarID;
                        //Pass values to registration form user controls
                        if (phOpt1.Visible)
                        {
                            plRegForm2.WebinarID = WebinarID.ToString();
                            plRegForm2.isPreview = (isPreview ? "1" : "0");
                        }
                        else
                        {
                            plRegForm1.WebinarID = WebinarID.ToString();
                            //plRegForm1.isPreview = isPreview;
                            plRegForm1.isPreview = (isPreview ? "1" : "0");
                        }
                        //Pass values to Presenter user controls
                        if (dvSpeaker.Visible)
                            plRegPresenter1.WebinarID = WebinarID.ToString();

                        //pass values to pre-registered user controls
                        plPreEmail1.WebinarID = WebinarID.ToString();
                        plPreEmail1.isPreview = (isPreview ? "1" : "0");
                    }
                    else
                        setPlaceHolder(false);
                }
                else
                    setPlaceHolder(false);
            }
        }

        protected void btnRefCol1_Click(object sender, EventArgs e)
        {
            ////btnRefCol1.Visible = false;
            //plPreEmail2.Visible = false;
            //plDateTime2.Visible = false;
            ////dvSummary.Visible = false;
            ////plRegPresenter1.Visible = false;
            //phAll.Visible = false;
            //phOpt1.Visible = true;
            ////layoutCSS.Href = "~/Styles/layout1/layout2.css";

            //plPreEmail2.setSingleColumnAttributes();
            //dvPre.Attributes.Remove("class");
            //dvPre.Attributes.Add("class", "Regi-Top-Sec1N");           
            //plRegForm2.toggleReferColleague(true);
            hphReg.Value = phAll.Visible ? "1" : "2";
            phAll.Visible = false;
            phOpt1.Visible = false;
            phRegFormColleague.Visible = true;
        }

        private void setPlaceHolder(bool val)
        {
            //dvTitle.Visible = true;
            //dvLogo.Visible = false;
            //dvSpeaker.Visible = true;
            //dvSummary.Visible = true;
            //phHeader.Visible = val;
            //phContent.Visible = val;
            //phFooter.Visible = val;
            //phError.Visible = !val;
            if (val == false)
                layoutCSS.Href = "~/Styles/layout/layout1.css";
        }

        protected void btnReferCollegue_Click(object sender, EventArgs e)
        {
            List<WebinarReferCollegue> objRef = objWebinarDA.GetWebinarReferedCollegueDetail(Convert.ToInt32(hWebinarID.Value), txtCEmail.Text.Trim(), txtEmail.Text.Trim());
            if (objRef.Count > 0)
            {
                dvRAFForm.Visible = false;
                dvRefExist.Visible = true;
                TemplateMgmt objTemplateMgmt = new TemplateMgmt();
                ltrRefExist.Text = objTemplateMgmt.GetReferedCollegueExist(objRef[0].EmailedOn, objRef[0].ReferInitiatedIP, Constant.DocTemplate + "referExist.tpl");
            }
            else
            {
                WebinarReferCollegue objRefData = new WebinarReferCollegue();
                objRefData.RefererFirstName = txtFName.Text.Trim();
                objRefData.RefererLastName = txtLName.Text.Trim();
                objRefData.RefererEmail = txtEmail.Text.Trim();
                objRefData.CollegueFirstName = txtCFName.Text.Trim();
                objRefData.CollegueLastName = txtCLName.Text.Trim();
                objRefData.CollegueEmail = txtCEmail.Text.Trim();

                objRefData.WebinarID = Convert.ToInt32(hWebinarID.Value);
                objRefData.ReferInitiatedIP = Request.UserHostAddress;
                int eventRefID = objWebinarDA.SaveWebinarReferCollegue(objRefData);
                phRegFormColleague.Visible = false;
                if (hphReg.Value == "1")
                    phAll.Visible = true;
                else
                    phOpt1.Visible = true;
                //btnRefCol1.Visible = true;
                //TemplateMgmt objTemplateMgmt = new TemplateMgmt();
                //ltrRefConf.Text = objTemplateMgmt.GetReferedCollegueConfirm(Constant.DocTemplate + "refConfirm.tpl");

            }
        }

    }
}