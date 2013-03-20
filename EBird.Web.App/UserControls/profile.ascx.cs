using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.DataAccess;
using EBird.BusinessEntity;
using EBird.Common;
using Telerik.Web.UI;
using EBird.DocRepo;

namespace EBird.Web.App.UserControls
{
    public partial class profile : System.Web.UI.UserControl
    {
        UserDA objUserDA = new UserDA();
        WebinarDA objWebinarDA = new WebinarDA();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //lblPath.Text = HttpContext.Current.Server.MapPath("~/DocRepo/profile/biologo.gif");

                imgprofileImg.Src = "~/handler/MyProfilePhoto.ashx?ID=" + Convert.ToString(Session["UserID"]);
                getMyprofile();
                getPresenterInfo();
                dvToggle();
                if (Session["Role"].ToString().ToUpper() == "SSADMIN" || Session["Role"].ToString().ToUpper() == "AEADMIN")
                {
                    dvpresenterbio.Visible = false;
                    dvpresenterinfo.Visible = false;
                    ltrBack.Text = "Return to <a href='Client' class='lnkBtn1'>Client Information</a>";
                }
            }
        }

        private void dvToggle()
        {
            if (hProfileImgID.Value == "0")
            {
                aupPhoto.Visible = true;
                ibtnDel.Enabled = false;
                ibtnDel.ImageUrl = "~/images/icons/ico-delete-disable.png";
            }
            else
            {
                aupPhoto.Visible = false;
                ibtnDel.Enabled = true;
                ibtnDel.ImageUrl = "~/images/icons/ico-delete-active.png";
            }
        }

        private void getMyprofile()
        {
           List<UserBE> objUserBE = objUserDA.GetUserDetailDA(Convert.ToInt32(Session["UserID"]));   
            if (objUserBE.Count > 0)
            {
                txtFName.Text = objUserBE[0].FirstName;
                txtLName.Text = objUserBE[0].LastName; 
                txtEmail.Text = objUserBE[0].EmailID; 
                txtTelephone.Text = objUserBE[0].Telephone;
                txtJobTitle.Text = objUserBE[0].JobTitle;
                txtDepartment.Text = objUserBE[0].Department;
            }
        }

        private void getPresenterInfo()
        {
            List<PresenterBE> objPreBE = objWebinarDA.GetPresenterDetail(Convert.ToInt32(Session["UserID"]));
            if (objPreBE.Count > 0)
            {
                txtPresenterName.Text = objPreBE[0].PresenterName;
                txtPresenterTitle.Text = objPreBE[0].Title;
                txtPreOrgName.Text = objPreBE[0].Organization;
                hProfileImgID.Value = Convert.ToString (objPreBE[0].ImageDocID);
                redtBio.Content = objPreBE[0].Bio;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserBE objUserBE = new UserBE();
            objUserBE.FirstName = txtFName.Text.Trim(); 
            objUserBE.LastName = txtLName.Text.Trim(); 
            objUserBE.Telephone = txtTelephone.Text.Trim(); 
            objUserBE.Department = txtDepartment.Text.Trim();  
            objUserBE.JobTitle = txtJobTitle.Text.Trim();
            objUserBE.UserID = Convert.ToInt32(Session["UserID"]);
            objUserDA.UpdateUserProfileRecord(objUserBE);
  
            PresenterBE objPreBE = new PresenterBE();
            txtPresenterName.Text = txtFName.Text.Trim() + " " + txtLName.Text.Trim();
            txtPresenterTitle.Text = txtJobTitle.Text.Trim();
            objPreBE.PresenterName = txtPresenterName.Text.Trim(); 
            objPreBE.Title =  txtPresenterTitle.Text.Trim(); 
            objPreBE.Organization = txtPreOrgName.Text.Trim();
            objPreBE.Bio = redtBio.Text;
            objPreBE.UserID = Convert.ToInt32(Session["UserID"]);
            objWebinarDA.UpdatePresenterDetail(objPreBE);
        }

        public void aupPhoto_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            Session["FolderType"] = "Profile";
            telerikAsyncUploadResult result = e.UploadResult as telerikAsyncUploadResult;
            if (result != null)
            {
                if (result.DocumentID > 0)
                {
                    //objWebinarDA.UpdatePresenterProfileDoc(result.DocumentID, Convert.ToInt32(Session["UserID"]));
                    hProfileImgID.Value = result.DocumentID.ToString();
                    //DocAccess objDocAccess = new DocAccess();
                    //objDocAccess.resizeImage(result.DocumentID, Convert.ToInt32(Session["ClientID"]));
                    aupPhoto.Visible = false;
                    imgprofileImg.Src = "~/handler/MyProfilePhoto.ashx?ID=" + Convert.ToString(Session["UserID"]);
                    dvToggle();
                }
            }
        }

        protected void ibtnDel_Click(object sender, ImageClickEventArgs e)
        {
            DocAccess objDocAccess = new DocAccess();
            DocumentDA objDocDA = new DocumentDA();
            objDocDA.ResetPresenterImgDocID(Convert.ToInt32(hProfileImgID.Value), Convert.ToInt32(Session["UserID"]));
            objDocAccess.removePresenterprofileImage(Convert.ToInt32(hProfileImgID.Value));
            hProfileImgID.Value = "0";
            dvToggle();
        }

    }
}