using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using System.Web.UI;


namespace EBird.Web.App.Pagelets
{
    public partial class plPresenters : System.Web.UI.UserControl
    {
        WebinarBE objWebinarBE = new WebinarBE();
        EBirdUtility objUtil = new EBirdUtility();
        WebinarDA objWebinarDA = new WebinarDA();
        EBErrorMessages objError = new EBErrorMessages();
        int _priPresenter;
        bool _addtnlPresenter;

        public string WebinarID
        {
            get
            {
                return hWebinarID.Value;
            }
            set
            {
                hWebinarID.Value = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //rpPresenter.DataSource = objWebinarDA.GetPresenterDetail(3);
                if (hWebinarID.Value != "")
                {
                    rpPresenter.DataSource = objWebinarDA.GetWebinarPresenterDetail(Convert.ToInt32(hWebinarID.Value),false);
                    rpPresenter.DataBind();
                }
            }
        }

        protected void rpPresenter_ItemDataBound(object sender, RepeaterItemEventArgs e)
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