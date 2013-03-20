using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using EBird.DocRepo;
using Telerik.Web.UI;
using System.Web.UI;

namespace EBird.Web.App.Pages
{
    public partial class MySnapSite : System.Web.UI.Page
    {
        SnapSiteDA objSnapSiteDA = new SnapSiteDA();
        WebinarDA objWebinarDA = new WebinarDA();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Page.RouteData.Values["Id"] != null)
                {
                    int UserID = Convert.ToInt32(Page.RouteData.Values["Id"]);
                    List<SnapSiteBO> objSS = objSnapSiteDA.GetMySnapSiteDetailsDA(UserID);
                    if (objSS.Count > 0)
                    {
                        if (objSS[0].IsEnabled)
                        {
                            phContent.Visible = true;
                            phUnAvail.Visible = false;
                            lblSSTitle.Text = objSS[0].Title;
                            lblDesc.Text = objSS[0].Description;
                            //Upcoming
                            popWebinar(UserID, true);
                            //Past
                            popWebinar(UserID, false);
                        }
                        else
                        {
                            phContent.Visible = false;
                            phUnAvail.Visible = true;
                        }
                    }
                    else
                    {
                        phNotConfig.Visible = true;
                    }
                }
                else
                {
                    phInvalid.Visible = true;
                    phMain.Visible = false;
                }
            }
        }

        private void popWebinar(int userID, bool isUpcoming)
        {
            if (isUpcoming)
            {
                rpUpcoming.DataSource = objSnapSiteDA.GetSSPubWebinarListDA(userID, true);
                rpUpcoming.DataBind();
            }
            else
            {
                
            }
        }

        protected void rpUpcoming_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string strDesc = string.Empty;
                ImageButton img1 = (ImageButton)e.Item.FindControl("imgArticle");
                if (img1 != null)
                    img1.ImageUrl = "~/handler/showImage.ashx?ID=-2";

                Label lt1 = (Label)e.Item.FindControl("lblContentSummary");
                if (lt1 != null)
                {
                    
                    strDesc = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Description"));
                    if (strDesc.Length > 300)
                        strDesc = strDesc.Substring(0, 300) + "&nbsp;<a href=# class='more'>more...</a>";
                    lt1.Text = strDesc;
                }
            }
        }

        protected void rpUpcoming_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "register")
            {
                int webinarID = Convert.ToInt32(e.CommandArgument);
                List<WebinarURLs> objURL = new List<WebinarURLs>();
                objURL = objWebinarDA.GetWebinarURLsDA(webinarID);
                if (objURL.Count > 0)
                {
                    if (objURL[0].RegistrationURL != "")
                    {
                        Response.Redirect(Constant.WebinarbaseURL + objURL[0].RegistrationURL);
                    }
                }
            }
        }

        protected void rpPast_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image img1 = (Image)e.Item.FindControl("imgPresenterImg");
                //HtmlGenericControl img1 = (HtmlGenericControl)e.Item.FindControl("imgPresenterImg");
                if (img1 != null)
                    img1.ImageUrl = "~/handler/showImage.ashx?ID=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ImageDocID"));
                Literal lt1 = (Literal)e.Item.FindControl("ltrPreInfo");
                if (lt1 != null)
                    lt1.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "PresenterName"));
                lt1 = (Literal)e.Item.FindControl("ltrDesi");
                if (lt1 != null)
                    lt1.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Title"))
                                + ", " + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Organization"));
                lt1 = (Literal)e.Item.FindControl("ltrPreBio");
                if (lt1 != null)
                    lt1.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Bio"));
            }
        }

    }
}