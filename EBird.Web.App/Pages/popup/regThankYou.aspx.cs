using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using Telerik.Web.UI;

namespace EBird.Web.App.Pages.popup
{
    public partial class regThankYou : System.Web.UI.Page
    {
        WebinarDA objWebinarDA = new WebinarDA();
        EBErrorMessages objError = new EBErrorMessages();
        EBirdUtility objUtil = new EBirdUtility();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request["ID"] != null)
                {
                    hWebinarID.Value = Request["ID"].ToString();
                    hReqType.Value = "Thank You for registering";
                    string rtyp = Request["typ"].ToString();
                    int webinarID = Convert.ToInt32(hWebinarID.Value);

                    List<WebinarContentBE> objWBContent = objWebinarDA.GetWebinarContent(webinarID, hReqType.Value);

                    switch (rtyp)
                    {
                        case "PV":
                            phEdit.Visible = false;
                            phView.Visible = true;
                            lblpgCap2.Text = hReqType.Value + " - Preview";
                            ltrThankContent.Text = objWBContent[0].ContentDescription;
                            break;
                        case "ED":
                            phEdit.Visible = true;
                            phView.Visible = false;
                            lblpgCap2.Text = hReqType.Value + " - Edit";
                            redtThankContent.Content = objWBContent[0].ContentDescription;
                            break;
                        case "SR":
                            phEdit.Visible = false;
                            phView.Visible = true;
                            lblpgCap2.Text = hReqType.Value + " - Review";
                            ltrThankContent.Text = objWBContent[0].ContentDescription;
                            break;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (redtThankContent.Content != "")
            {
                objWebinarDA.SaveWebinarContent(Convert.ToInt32(hWebinarID.Value), "Thank You for registering", redtThankContent.Content);
                lblError.ForeColor = System.Drawing.Color.Green;
                lblError.Text = "Content saved successfully";
            }
            else
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Content missing"; 
        }

        protected void btnReview_Click(object sender, EventArgs e)
        {
            lblError1.Text = "";
            if (txtReviewerEmail.Text.Trim() != "")
            {
                if (objUtil.isEmailsValid(txtReviewerEmail.Text.Trim(), new char[] { ';' }))
                {
                    EmailBE objEmailBE = new EmailBE();
                    EmailDA objEmailDA = new EmailDA();
                    int emlReqID = 0;
                    objEmailBE.isToEmailRef = true;
                    objEmailBE.RequestStatus = "No-delay";
                    objEmailBE.RequestType = "Thank You for registering"; // "Webinar Registrant Emailing";
                    objEmailBE.Subject = "Thank You for registering content for review";         // txtSubject.Text;
                    objEmailBE.SubmittedBy = Convert.ToInt32(Session["UserID"]);
                    objEmailBE.ToEmail = "";
                    objEmailBE.FromEmail = Session["EmailID"].ToString();
                    objEmailBE.EmailContent = "<b>Following are the content for review</b><br><br>" + redtThankContent.Content;
                    emlReqID = objEmailDA.SaveEmailRequest(objEmailBE);
                    objEmailDA.SaveToEmail(new EmailTo
                                {
                                    EmailRequestID = emlReqID,
                                    ToEmails = txtReviewerEmail.Text.Trim(),
                                    ToType = ""
                                });

                    lblError1.Text = "Emailed to reviewer(s)";
                }
                else
                {
                    lblError1.Text = "One or more entered reviewer's email is invalid";
                }
            }
            else
                lblError1.Text = "Reviewer email(s) is missing";
        }
    }
}