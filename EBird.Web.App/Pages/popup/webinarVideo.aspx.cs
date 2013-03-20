using System;
using System.IO;
using System.Web;
using System.Drawing;
using EBird.Common;
using EBird.BusinessEntity;
using EBird.DataAccess;
using EBird.DocRepo;


namespace EBird.Web.App.Pages.popup
{
    public partial class webinarVideo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                if (Request["ID"] != null)
                {
                    hWebinarID.Value =  Request["ID"].ToString();
                    hDocID.Value = Request["docID"].ToString();
                    DocumentDA objDocumentDA = new DocumentDA();
                    string _FileName = objDocumentDA.GetDocumentPath(Convert.ToInt32(hDocID.Value));
                    if (File.Exists(_FileName))
                    {
                        ImgNoVideo.Visible = false;
                        regVideo.Visible = true;
                    }
                    else
                    {
                        ImgNoVideo.Visible = true;
                        regVideo.Visible = false;
                    }
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            int docID = 0;
            DocAccess objDocAccess = new DocAccess();
            WebinarDA objWebinarDA = new WebinarDA();
            docID = objDocAccess.saveFiles(Request, "PRESENTATION", Convert.ToInt32(Session["ClientID"]), Convert.ToInt32(Session["UserID"]));
            if (docID > 0)
            {
                objWebinarDA.UpdateRegPageVideo(docID, Convert.ToInt32(hWebinarID.Value));
                hDocID.Value = docID.ToString();
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}