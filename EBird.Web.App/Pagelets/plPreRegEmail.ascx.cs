using System;
using System.Text;
using System.Collections.Generic;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using System.Collections;

namespace EBird.Web.App.Pagelets
{
    public partial class plPreRegEmail : System.Web.UI.UserControl
    {
        private string webID;
        
        public string WebinarID
        {
            get
            {
                return webID;
            }
            set
            {
                webID = value;
            }
        }

        public string isPreview
        {
            get
            {
                return hPreview.Value;
            }
            set
            {
                hPreview.Value = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                lblError.Text = "";
                hWebinarID.Value = webID;
                //btnPreLogin.Enabled = !Convert.ToBoolean(Convert.ToInt32(hPreview.Value));
                if (hPreview.Value == "1")
                {
                    btnPreLogin.Visible = false;
                    Predummy1.Visible = true;
                }
                else
                {
                    btnPreLogin.Visible = true;
                    Predummy1.Visible = false;
                }
            }
        }

        public void setSingleColumnAttributes()
        {
            dvPreEmail.Attributes.Remove("class");
            dvPreEmail.Attributes.Add("class", "Pre-Reg-Email1N");  
        }

        protected void btnPreLogin_Click(object sender, EventArgs e)
        {
            if (hPreview.Value == "0")
            {
                if (txtPreEmail.Text != "")
                {
                    WebinarDA objWebinarDA = new WebinarDA();
                    List<Registrants> objReg = objWebinarDA.GetWebinarRegistrantDetail(Convert.ToInt32(hWebinarID.Value), txtPreEmail.Text.Trim());
                    if (objReg.Count > 0)
                    {
                        List<WebinarURLs> objURL = new List<WebinarURLs>();
                        objURL = objWebinarDA.GetWebinarURLsDA(Convert.ToInt32(hWebinarID.Value));
                        if (objURL.Count > 0)
                        {
                            if (objURL[0].AudienceInterfaceURL != "")
                                Response.Redirect(Constant.WebinarViewerBaseURL + objURL[0].AudienceInterfaceURL);
                        }
                    }
                    else
                    {
                        lblError.Text = "Login failed - no registered email exist for this webinar";
                    }
                }
            }
        }
    }
}