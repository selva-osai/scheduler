using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.DataAccess;

namespace EBird.Web.App.Pages
{
    public partial class recPlayback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                WebcastDA objWebcastDA = new WebcastDA();
                List<WebinarBO> objWebinarBO = objWebcastDA.GetMyWebcastDA(0);
                rpImg.DataSource = objWebinarBO;
                rpImg.DataBind();  
            }

        }

        protected void ibtnImg1_Click(object sender, ImageClickEventArgs e)
        {
            WebcastDA objWebcastDA = new WebcastDA();
            
            List<WebinarBO> objWebinarBO = objWebcastDA.GetWebcastDetailDA(2);
            if (objWebinarBO.Count > 0)
            {
                lblTitle.Text = objWebinarBO[0].Title;
                lblPresenter.Text = objWebinarBO[0].Presenter;
                lblDate.Text = objWebinarBO[0].SessionDateAndTime;
                ltrConfDetail.Text = objWebinarBO[0].Summary; 
            }
        }

        protected void rpImg_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            WebcastDA objWebcastDA = new WebcastDA();

            List<WebinarBO> objWebinarBO = objWebcastDA.GetWebcastDetailDA(Convert.ToInt32(e.CommandArgument));
            if (objWebinarBO.Count > 0)
            {
                lblTitle.Text = objWebinarBO[0].Title;
                lblPresenter.Text = objWebinarBO[0].Presenter;
                lblDate.Text = objWebinarBO[0].SessionDateAndTime;
                ltrConfDetail.Text = objWebinarBO[0].Summary;
                //ltrVideo.Text = "<video width=\"320\" height=\"240\" controls=\"controls\" src=\"../Data/" + objWebinarBO[0].recordedFilename + "\"></video>";
                ltrVideo.Text = "<video controls=\"controls\" src=\"..//Data//" + objWebinarBO[0].recordedFilename + "\"></video>";
            }
        }
    }
}