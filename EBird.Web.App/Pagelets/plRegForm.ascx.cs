using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Web.UI;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using EBird.Framework;

namespace EBird.Web.App.Pagelets
{
    public partial class plRegForm : System.Web.UI.UserControl
    {
        WebinarBE objWebinarBE = new WebinarBE();
        EBirdUtility objUtil = new EBirdUtility();
        WebinarDA objWebinarDA = new WebinarDA();
        EBErrorMessages objError = new EBErrorMessages();
        private bool _isPreview;
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
                getRegForm();
                getAdditionalFormQuestion();
                btnRegister.Enabled = !Convert.ToBoolean(Convert.ToInt32(hPreview.Value));
                if (hPreview.Value == "1")
                {
                    btnRegister.Visible = false;
                    Predummy2.Visible = true;
                }
                else
                {
                    btnRegister.Visible = true;
                    Predummy2.Visible = false;
                }
            }
        }

        private void getRegForm()
        {
            if (hWebinarID.Value != "")
            {
                List<WebinarRegFormFields> objWebReg = objWebinarDA.getWebinarRegFormFields(Convert.ToInt32(hWebinarID.Value));

                if (objWebReg.Count > 0)
                {
                    HtmlGenericControl hcntrl1;
                    HtmlGenericControl hcntrl2;
                    Label reqStar;
                    RequiredFieldValidator rfv;
                    Label InputLabel;
                    TextBox InputValue;
                    RadComboBox rcmb;
                    for (int idx = 0; idx < objWebReg.Count; idx++)
                    {
                        hcntrl1 = (HtmlGenericControl)this.FindControl("fl" + objWebReg[idx].FieldID.ToString());
                        hcntrl2 = (HtmlGenericControl)this.FindControl("fi" + objWebReg[idx].FieldID.ToString());
                        if (hcntrl1 != null)
                        {
                            hcntrl1.Visible = true;
                            hcntrl2.Visible = true;
                            InputLabel = (Label)this.FindControl("lblF" + objWebReg[idx].FieldID.ToString());
                            //if (objWebReg[idx].FieldID.ToString() == "8")
                            //    rcmb = (RadComboBox)this.FindControl("InV" + objWebReg[idx].FieldID.ToString());
                            //else
                            InputValue = (TextBox)this.FindControl("InV" + objWebReg[idx].FieldID.ToString());
                            InputLabel.Text = objWebReg[idx].FieldLabel;

                            if (objWebReg[idx].isRequired)
                            {
                                reqStar = (Label)this.FindControl("lblR" + objWebReg[idx].FieldID.ToString());
                                rfv = (RequiredFieldValidator)this.FindControl("fv" + objWebReg[idx].FieldID.ToString());
                                if (reqStar != null)
                                {
                                    reqStar.Visible = true;
                                    if (rfv != null)
                                    {
                                        rfv.Enabled = true;
                                        rfv.ErrorMessage = objWebReg[idx].FieldLabel + " cannot be left blank";
                                    }
                                }
                            }
                        }
                    }
                    //if (objWebReg[0].isAdditionalWebinar)
                    //{
                    //    List<WebinarBE> objRelWeb = objWebinarDA.GetRelatedWebinarsDA(Convert.ToInt32(hWebinarID.Value));
                    //    if (objRelWeb.Count > 0)
                    //    {
                    //        dvAdditonalWeb.Visible = true;
                    //        rpRelatedWebinar.DataSource = objRelWeb;
                    //        rpRelatedWebinar.DataBind();
                    //    }
                    //    else
                    //        dvAdditonalWeb.Visible = false;
                    //}
                }
            }
        }

        private void getAdditionalFormQuestion()
        {
            if (hWebinarID.Value != "")
            {
                List<WebinarRegFormQA> objWebQA = objWebinarDA.getWebinarRegFormQA(Convert.ToInt32(hWebinarID.Value));
                rpAdditionalForm.DataSource = objWebQA;
                rpAdditionalForm.DataBind();
            }
        }

        protected void rpAdditionalForm_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                switch (((HiddenField)e.Item.FindControl("hQResponseType")).Value)
                {
                    case "COMM":
                        ((TextBox)e.Item.FindControl("txtComments")).Visible = true;
                        break;
                    case "DD":
                        ((RadComboBox)e.Item.FindControl("rcmbType")).Visible = true;
                        ArrayList values = objUtil.StringToArrayList(((HiddenField)e.Item.FindControl("hQResponseOptions")).Value, new char[] { ';' });
                        RadComboBox rcmb1 = ((RadComboBox)e.Item.FindControl("rcmbType"));
                        rcmb1.DataSource = values;
                        rcmb1.DataBind();
                        break;
                    case "MCSA":
                        //((RadioButtonList)e.Item.FindControl("rbtnList")).Visible = true;
                        //ArrayList values1 = objUtil.StringToArrayList(((HiddenField)e.Item.FindControl("hQResponseOptions")).Value, new char[] { ';' });
                        //RadioButtonList rbtn1 = ((RadioButtonList)e.Item.FindControl("rbtnList"));
                        //rbtn1.DataSource = values1;
                        //rbtn1.DataBind();
                        ((RadComboBox)e.Item.FindControl("rcmbType")).Visible = true;
                        ArrayList values1 = objUtil.StringToArrayList(((HiddenField)e.Item.FindControl("hQResponseOptions")).Value, new char[] { ';' });
                        RadComboBox rcmb2 = ((RadComboBox)e.Item.FindControl("rcmbType"));
                        rcmb2.DataSource = values1;
                        rcmb2.DataBind();
                        break;
                    case "MCMA":
                        ((CheckBoxList)e.Item.FindControl("chkList")).Visible = true;
                        ArrayList values2 = objUtil.StringToArrayList(((HiddenField)e.Item.FindControl("hQResponseOptions")).Value, new char[] { ';' });
                        CheckBoxList chk1 = ((CheckBoxList)e.Item.FindControl("chkList"));
                        chk1.DataSource = values2;
                        chk1.DataBind();
                        break;
                }
            }

        }

        protected void rpRelatedWebinar_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfld = (HiddenField)e.Item.FindControl("hID");
                LinkButton lbtn = (LinkButton)e.Item.FindControl("lbtnTitle");
                hfld.Value = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "WebinarID"));
                lbtn.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "WebinarTitle"));
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnRegister_Click(object sender, EventArgs e)
        {
            if (hPreview.Value == "0")
            {
                lblEror.Text = "";
                List<Registrants> objReg = objWebinarDA.GetWebinarRegistrantDetail(Convert.ToInt32(hWebinarID.Value), InV3.Text.Trim());
                if (objReg.Count > 0)
                {
                    dvRegForm.Visible = false;
                    dvRegExist.Visible = true;
                    TemplateMgmt objTemplateMgmt = new TemplateMgmt();
                    RegistrationExistTemplateBO objExistBO = new RegistrationExistTemplateBO();
                    objExistBO.RegistrantID = objReg[0].RegistrationID;
                    objExistBO.RegistrantEmail = objReg[0].EmailAddress;
                    objExistBO.RegisteredOn = objReg[0].RegisteredOn;
                    objExistBO.RegisteredFromIP = objReg[0].RegFromIP;
                    ltrExist.Text = objTemplateMgmt.GetRegistrationExist(objExistBO, Constant.DocTemplate + "regExist.tpl");
                }
                else
                {
                    if (objWebinarDA.IsWebinarHostExistDA(Convert.ToInt32(hWebinarID.Value), InV3.Text.Substring(InV3.Text.IndexOf("@") + 1)))
                    {
                        lblEror.Text = objError.getMessage("WB0009");
                    }
                    else
                    {
                        Registrants objRegData = new Registrants();
                        TextBox InputValue;
                        RadComboBox rcmb;
                        string fieldValue;
                        for (int idx = 1; idx < 21; idx++)
                        {
                            fieldValue = "";

                            //if (idx == 8 || idx == 10 || idx == 16)
                            //{
                            //    if ((RadComboBox)this.FindControl("InV" + idx.ToString()) != null)
                            //    {
                            //        rcmb = (RadComboBox)this.FindControl("InV" + idx.ToString());
                            //        fieldValue = rcmb.SelectedValue;
                            //    }
                            //}
                            //else
                            //{
                            if ((TextBox)this.FindControl("InV" + idx.ToString()) != null)
                            {
                                InputValue = (TextBox)this.FindControl("InV" + idx.ToString());
                                fieldValue = InputValue.Text;
                            }
                            //}

                            switch (idx)
                            {
                                case 1:
                                    objRegData.Fld1 = fieldValue;
                                    break;
                                case 2:
                                    objRegData.Fld2 = fieldValue;
                                    break;
                                case 3:
                                    objRegData.Fld3 = fieldValue;
                                    break;
                                case 4:
                                    objRegData.Fld4 = fieldValue;
                                    break;
                                case 5:
                                    objRegData.Fld5 = fieldValue;
                                    break;
                                case 6:
                                    objRegData.Fld6 = fieldValue;
                                    break;
                                case 7:
                                    objRegData.Fld7 = fieldValue;
                                    break;
                                case 8:
                                    objRegData.Fld8 = fieldValue;
                                    break;
                                case 9:
                                    objRegData.Fld9 = fieldValue;
                                    break;
                                case 10:
                                    objRegData.Fld10 = fieldValue;
                                    break;
                                case 11:
                                    objRegData.Fld11 = fieldValue;
                                    break;
                                case 12:
                                    objRegData.Fld12 = fieldValue;
                                    break;
                                case 13:
                                    objRegData.Fld13 = fieldValue;
                                    break;
                                case 14:
                                    objRegData.Fld14 = fieldValue;
                                    break;
                                case 15:
                                    objRegData.Fld15 = fieldValue;
                                    break;
                                case 16:
                                    objRegData.Fld16 = fieldValue;
                                    break;
                                case 17:
                                    objRegData.Fld17 = fieldValue;
                                    break;
                                case 18:
                                    objRegData.Fld18 = fieldValue;
                                    break;
                                case 19:
                                    objRegData.Fld19 = fieldValue;
                                    break;
                                case 20:
                                    objRegData.Fld20 = fieldValue;
                                    break;
                            }
                        }

                        objRegData.webinarID = Convert.ToInt32(hWebinarID.Value);
                        objRegData.RegFromIP = Request.UserHostAddress;
                        int eventRegID = objWebinarDA.SaveWebinarRegistrant(objRegData);
                        updateQAValues(eventRegID);

                        dvRegForm.Visible = false;
                        dvRegConf.Visible = true;

                        TemplateMgmt objTemplateMgmt = new TemplateMgmt();
                        RegistrationConfirmTemplateBO objConfirmBO = new RegistrationConfirmTemplateBO();
                        objConfirmBO.RegistrantID = eventRegID;

                        ltrConf.Text = objTemplateMgmt.GetRegistrationConfirm(objConfirmBO, Constant.DocTemplate + "regConfirm.tpl");
                        //Email confirmation - need to be added here

                        //int eventRegID = objWebinarDA.SaveWebinarEvent(objRegData);
                        //if (dvAdditonalWeb.Visible)
                        //{
                        //    int addEventID = 0;
                        //    foreach (RepeaterItem item in rpRelatedWebinar.Items)
                        //    {
                        //        CheckBox chk = (CheckBox)item.FindControl("chkID");
                        //        if (chk.Checked)
                        //        {
                        //            HiddenField hfld = (HiddenField)item.FindControl("hID");
                        //            objRegData.webinarID = Convert.ToInt32(hfld.Value);
                        //            addEventID = objWebinarDA.SaveWebinarEvent(objRegData);
                        //            // is every webinar confirmation number to be captured and 
                        //            // displayed in confirmation page and as well in email
                        //        }
                        //    }
                        //}
                    }
                }
            }
        }

        private void updateQAValues(int regID)
        {
            HiddenField hQID, hQstOptType;
            TextBox txt1;
            RadComboBox rcmb1;
            RadioButtonList rbtn1;
            CheckBoxList chk1;
            string QAResponse = "";
            foreach (RepeaterItem item in rpAdditionalForm.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    hQID = (HiddenField)item.FindControl("hQID");
                    hQstOptType = (HiddenField)item.FindControl("hQResponseType");
                    if (hQID != null)
                    {
                        switch (hQstOptType.Value)
                        {
                            case "COMM":
                                txt1 = (TextBox)item.FindControl("txtComments");
                                QAResponse = txt1.Text;
                                break;
                            case "DD":
                                rcmb1 = (RadComboBox)item.FindControl("rcmbType");
                                QAResponse = rcmb1.SelectedValue;
                                break;
                            case "MCSA":
                                rbtn1 = (RadioButtonList)item.FindControl("rbtnList");
                                QAResponse = rbtn1.SelectedValue;
                                break;
                            case "MCMA":
                                chk1 = (CheckBoxList)item.FindControl("chkList");
                                for (int idx = 0; idx < chk1.Items.Count; idx++)
                                {
                                    if (chk1.Items[idx].Selected)
                                        QAResponse += chk1.Items[idx].Text + ";";
                                }
                                break;
                        }
                        objWebinarDA.SaveAdditionalQA(regID, Convert.ToInt32(hQID.Value), QAResponse);
                    }
                }
            }
        }

        public void toggleReferColleague(bool isReferC)
        {
            if (isReferC)
            {
                phRegFormColleague.Visible = true;
                phRegFormUser.Visible = false;
            }
            else
            {
                phRegFormColleague.Visible = false;
                phRegFormUser.Visible = true;
            }
        }

        protected void btnReferCollegue_Click(object sender, EventArgs e)
        {
            List<WebinarReferCollegue> objRef = objWebinarDA.GetWebinarReferedCollegueDetail(Convert.ToInt32(hWebinarID.Value), txtCEmail.Text.Trim(), txtEmail.Text.Trim());
            if (objRef.Count > 0)
            {
                dvCForm.Visible = false;
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
                dvCForm.Visible = false;
                dvRefConf.Visible = true;
                phRegFormUser.Visible = true;

                //TemplateMgmt objTemplateMgmt = new TemplateMgmt();
                //ltrRefConf.Text = objTemplateMgmt.GetReferedCollegueConfirm(Constant.DocTemplate + "refConfirm.tpl");

            }
        }

    }
}