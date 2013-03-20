using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using EBird.DocRepo;
using EBird.Email;
using System.Text.RegularExpressions;


namespace EBird.Web.App.Pages.popup
{
    public partial class addAttribute : System.Web.UI.Page
    {
        WebinarDA objWebinarDA = new WebinarDA();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hWebinarID.Value = Request["ID"].ToString();
                hDocId.Value = Request["docID"].ToString();
                getlogodetails();
                Session["Web_Tab"] = "2";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            WebinarResource objWebinarResource = new WebinarResource();
            objWebinarResource.WebinarID = Convert.ToInt32(hWebinarID.Value);
            objWebinarResource.DocID = Convert.ToInt32(hDocId.Value);
            objWebinarResource.LogoUrlName = txtURLName.Text;
            objWebinarResource.LogoUrl = txtURL.Text;
            objWebinarDA.UpdateWebinarResources(objWebinarResource);
            hModalStatusFlg.Value = "1";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            hModalStatusFlg.Value = "1";
        }
        public void getlogodetails()
        {
            List<WebinarResource> objRes = objWebinarDA.getWebinarDocID(Convert.ToInt32(hDocId.Value));
            if (objRes.Count > 0)
            {
                txtURLName.Text = objRes[0].LogoUrlName;
                txtURL.Text = objRes[0].LogoUrl;
            }

        }
    }
}