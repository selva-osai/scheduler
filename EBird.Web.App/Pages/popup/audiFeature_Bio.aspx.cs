using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.DataAccess;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using EBird.DocRepo;

namespace EBird.Web.App.Pages.popup
{
    public partial class audiFeature_Bio : System.Web.UI.Page
    {
        WebinarDA objWebDA = new WebinarDA();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Session["Web_Tab"] = "3";
                if (Request["ID"] != null)
                {
                    hWebinarID.Value = Request["ID"].ToString();
                    if (Request["flg"].ToString() == "1")
                    {
                        imgprofileImg.Src = "~/handler/MyProfilePhoto.ashx?ID=-1";
                        //dvToggle();
                        popPresenter();
                        popPresenterDD();
                    }
                    else
                    {
                        //phConfig.Visible = false;
                        //phDisableFeature.Visible = true;
                        //lblDisableFeature.Text = objError.getMessage("WB1001");
                    }
                }
            }
        }

        private void popPresenterDD()
        {
            List<PresenterBE> objPre = objWebDA.GetOtherWebinarPresenters(Convert.ToInt32(Session["UserID"]), Convert.ToInt32(hWebinarID.Value));
            rcmbPresenter.DataSource = objPre;
            rcmbPresenter.DataTextField = "PresenterName";
            rcmbPresenter.DataValueField = "PresenterID";
            rcmbPresenter.DataBind();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            PresenterBE objPreBE = new PresenterBE();
            lblError.Text = "";
            if (rcmbPresenter.Text != "")
            {
                if (btnSave.Text == "Add to webinar")
                {
                    objWebDA.UpdateAdditionalPresenter(Convert.ToInt32(rcmbPresenter.SelectedValue), Convert.ToInt32(hWebinarID.Value));
                }
                else
                {
                    objPreBE.PresenterID = Convert.ToInt32(hPresenterID.Value);
                    objPreBE.PresenterName = rcmbPresenter.Text.Trim();
                    objPreBE.Title = txtPresenterTitle.Text.Trim();
                    objPreBE.Organization = txtPreOrgName.Text.Trim();
                    objPreBE.Bio = redtBio.Text;
                    objPreBE.UserID = 0;
                    int presenterID = objWebDA.UpdatePresenterDetail(objPreBE, Convert.ToInt32(hWebinarID.Value));
                    if (presenterID != 0)
                    {
                        dvProfileImg.Visible = true;
                        hPresenterID.Value = presenterID.ToString();
                        hProfileImgID.Value = "0";
                        dvToggle();
                    }
                    else
                        lblError.Text = "Presenter already exist";
                }
                if (lblError.Text == "")
                {
                    hPresenterID.Value = "0";
                    refreshNav2Tab2();
                }
            }

            //hModalStatusFlg.Value = "1";
            //if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
        }

        private void refreshNav2Tab2()
        {
            popPresenterDD();
            popPresenter();
            rtabBio.SelectedIndex = 1;
            rmpgBio.SelectedIndex = 1;
            clearFields(true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["Web_Tab"] = "3";
            hModalStatusFlg.Value = "1";
            //if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
        }

        protected void ibtnDel_Click(object sender, ImageClickEventArgs e)
        {
            //DocAccess objDocAccess = new DocAccess();
            //DocumentDA objDocDA = new DocumentDA();
            //objDocDA.ResetPresenterImgDocID(Convert.ToInt32(hProfileImgID.Value), Convert.ToInt32(Session["UserID"]));
            //objDocAccess.removePresenterprofileImage(Convert.ToInt32(hProfileImgID.Value));

            //   dvToggle();
        }

        private void popPresenter()
        {
            WebinarDA objWebinarDA = new WebinarDA();
            List<PresenterBE> objPresenter = objWebinarDA.GetWebinarPresenterDetail(Convert.ToInt32(hWebinarID.Value), true);

            //rpPresenterList.DataSource = objPresenter;
            //rpPresenterList.DataBind();
            tgrdPresenterList.DataSource = objPresenter;
            tgrdPresenterList.DataBind();
        }

        protected void rpPresenterList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image img1 = (Image)e.Item.FindControl("imgPresenterImg");
                //HtmlGenericControl img1 = (HtmlGenericControl)e.Item.FindControl("imgPresenterImg");

                if (img1 != null)
                    img1.ImageUrl = "~/handler/MyProfilePhoto.ashx?ID=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "UserID"));
            }
        }

        protected void tgrdPresenterList_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                List<PresenterBE> objPre = objWebDA.GetPresenterDetail(Convert.ToInt32(e.CommandArgument.ToString()), "PresenterID");
                if (objPre.Count > 0)
                {
                    rcmbPresenter.Text = objPre[0].PresenterName;
                    hPresenterID.Value = objPre[0].PresenterID.ToString();
                    txtPresenterTitle.Text = objPre[0].Title;
                    txtPreOrgName.Text = objPre[0].Organization;
                    hProfileImgID.Value = Convert.ToString(objPre[0].ImageDocID);
                    redtBio.Content = objPre[0].Bio;
                    imgprofileImg.Src = "~/handler/MyProfilePhoto.ashx?pID=" + Convert.ToString(objPre[0].PresenterID);
                    ibtnDel.Visible = false;
                    btnSave.Text = "Save";
                    rtabBio.SelectedIndex = 0;
                    rmpgBio.SelectedIndex = 0;
                }
            }
        }

        protected void tgrdPresenterList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //Get the instance of the right type
                GridDataItem dataBoundItem = e.Item as GridDataItem;

                PresenterBE objPreBE = (PresenterBE)(e.Item.DataItem);
                Label lbl1 = (Label)dataBoundItem.FindControl("lblDetail");
                LinkButton lbtn = (LinkButton)dataBoundItem.FindControl("lnkDetail");

                lbtn.Text = objPreBE.PresenterName;
                lbtn.ForeColor = System.Drawing.Color.Blue;
                lbl1.Text = "<br><br>" + objPreBE.Title + "<br><br>" + objPreBE.Organization;
                CheckBox chk1 = (CheckBox)dataBoundItem.FindControl("chkDisable");
                if (objPreBE.UserID.ToString() == Session["UserID"].ToString())
                {
                    chk1.Enabled = false;
                    chk1.Checked = true;
                }
                else
                {
                    chk1.Attributes.Add("data-theme", objPreBE.PresenterID.ToString());
                }
                Image img1 = (Image)e.Item.FindControl("imgPresenterImg");
                //HtmlGenericControl img1 = (HtmlGenericControl)e.Item.FindControl("imgPresenterImg");
                if (img1 != null)
                    //img1.ImageUrl = "~/handler/MyProfilePhoto.ashx?ID=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "UserID"));
                    img1.ImageUrl = "~/handler/MyProfilePhoto.ashx?pID=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "PresenterID"));
            }
        }

        public void UpdateEnabledStatus(object sender, System.EventArgs e)
        {
            try
            {
                //Loop Through Grid
                foreach (GridDataItem item in tgrdPresenterList.Items)
                {
                    //Set Current Status
                    bool DisableStatus = false;

                    //Get PresenterID
                    string strtest = (item["presenterID"].Text);

                    // Get Checkbox Value
                    CheckBox chk1 = (CheckBox)item.FindControl("chkDisable");
                    DisableStatus = chk1.Checked;


                    //List<PresenterBE> objP = objWebDA.GetWebinarPresenterDetail(Convert.ToInt32(hWebinarID.Value), Convert.ToInt32(strtest));
                    //if (objP.Count > 0)
                    //{
                    //if (objP[0].IsEnabled != DisableStatus)
                    //{
                    objWebDA.updateWebinarEnabledState(!DisableStatus, Convert.ToInt32(strtest), Convert.ToInt32(hWebinarID.Value));
                    //}
                    //}
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void rcmbPresenter_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (rcmbPresenter.SelectedValue != "")
            {
                List<PresenterBE> objPre = objWebDA.GetPresenterDetail(Convert.ToInt32(rcmbPresenter.SelectedValue), "PresenterID");
                if (objPre.Count > 0)
                {
                    txtPresenterTitle.Text = objPre[0].Title;
                    txtPreOrgName.Text = objPre[0].Organization;
                    hProfileImgID.Value = Convert.ToString(objPre[0].ImageDocID);
                    redtBio.Content = objPre[0].Bio;
                    imgprofileImg.Src = "~/handler/MyProfilePhoto.ashx?pID=" + Convert.ToString(objPre[0].PresenterID);
                    ibtnDel.Visible = false;
                    btnSave.Text = "Add to webinar";
                }
            }
            else
            {
                clearFields(false);
            }
            dvToggle();
        }

        private void clearFields(bool isNameToo)
        {
            hProfileImgID.Value = "0";
            txtPresenterTitle.Text = "";
            txtPreOrgName.Text = "";
            redtBio.Content = "";
            imgprofileImg.Src = "~/handler/MyProfilePhoto.ashx?pID=-1";
            btnSave.Text = "Save";
            if (isNameToo)
            {
                rcmbPresenter.Text = "";
            }
        }

        public void aupPhoto_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            Session["FolderType"] = "Profile";
            telerikAsyncUploadResult result = e.UploadResult as telerikAsyncUploadResult;
            if (result != null)
            {
                if (result.DocumentID > 0)
                {
                    hProfileImgID.Value = result.DocumentID.ToString();
                    UpdatePresenterProfileID(result.DocumentID, Convert.ToInt32(hPresenterID.Value));
                    aupPhoto.Visible = false;
                    //imgprofileImg.Src = "~/handler/showImage.ashx?ID=" + result.DocumentID.ToString();
                    refreshNav2Tab2();
                }
            }
        }

        private void UpdatePresenterProfileID(int DocumentID, int PresenterID)
        {
            DocumentDA objDoc = new DocumentDA();
            if (PresenterID > 0 && DocumentID > 0)
                objDoc.UpdatePresenterImgDocID(DocumentID, PresenterID);
        }

        protected void lnkUploadProfile_Click(object sender, EventArgs e)
        {
            Session["FolderType"] = "PROFILE";
            popPresenter();
        }
    }
}