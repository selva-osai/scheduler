using System;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using EBird.DocRepo;
using EBird.Email;
using System.Text.RegularExpressions;

namespace EBird.Web.App.UserControls
{
    public partial class webRegistration : System.Web.UI.UserControl
    {
        WebinarBE objWebinarBE = new WebinarBE();
        EBirdUtility objUtil = new EBirdUtility();
        WebinarDA objWebinarDA = new WebinarDA();
        EBErrorMessages objError = new EBErrorMessages();
        static int ImgID;
        bool boolCampTrackerEmail = false;
        bool boolConnectRegistration = false;
        bool _isWebinarPast = false;
        static private int _logoCount;

        static public int logoCount
        {
            get
            {
                return _logoCount;
            }
            set
            {
                _logoCount = value;
            }
        }

        public bool CampTrackerEmail
        {
            get
            {
                return boolCampTrackerEmail;
            }
            set
            {
                boolCampTrackerEmail = value;
            }
        }

        public bool ConnectRegistration
        {
            get
            {
                return boolConnectRegistration;
            }
            set
            {
                boolConnectRegistration = value;
            }
        }

        public bool isWebinarPast
        {
            get
            {
                return _isWebinarPast;
            }
            set
            {
                _isWebinarPast = value;
            }

        }

        private List<Telerik.Web.UI.UploadedFileInfo> uploadedFiles = new List<Telerik.Web.UI.UploadedFileInfo>();

        public List<Telerik.Web.UI.UploadedFileInfo> UploadedFiles
        {
            get { return uploadedFiles; }
            set { uploadedFiles = value; }
        }

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
                regTabLoad();

                #region FOLLOWING CODE IS BLOCKED, AND WILL BE REVISITED FOR PHASE II
                ////string[] videoExt = {"mp4","ogg"};
                ////raupVideoFile.AllowedFileExtensions = videoExt;
                ////telerikUploadConfig config = raupVideoFile.CreateDefaultUploadConfiguration<telerikUploadConfig>();
                ////// Populate any additional fields
                ////config.ActionID = Convert.ToInt32(Session["UserID"]);
                ////config.ClientID = Convert.ToInt32(Session["ClientID"]);
                ////config.FolderType = "PRESENTATION";
                ////// The upload configuration will be available in the handler
                ////raupVideoFile.UploadConfiguration = config;
                #endregion

                disableUpdates();
            }
            else
                setNonStdDominCount();
        }

        public void disableUpdates()
        {
            if (_isWebinarPast)
            {
                rcmbHeader.Enabled = false;
                aupLogo.Visible = false;
                btnNewQuestion.Visible = false;
                //iterate through repeater to remove the delete icons
                //tgrdQAList - iterate thru this gris to remove the dlete icon
                chkIncSummary.Enabled = false;
                chkIncSpeakerBio.Enabled = false;
                chkEnableReg.Enabled = false;
                lbtnEditInvite1.Visible = false;
                lbtnEmailInvite1.Visible = false;
                lbtnSendReviewInvite1.Visible = false;
                ltrSep1.Visible = false;
                ltrSep2.Visible = false;
                ltrSep3.Visible = false;
                spAdd.Visible = false;
                imgbtnDel2.Visible = false;
            }
        }

        public void regTabLoad()
        {
            Session["FolderType"] = "LOGO";
            //Session["WebinarRegFormQA"] = null;
            #region registration info & reg form
            popRegistrationDetails();
            popQAs();
            btnEditCancel_click(null, null);
            #endregion

            #region theme, header type
            rcmbHeader.Text = "-Select Header-";
            getThemeDetails();
            getThemeLayouts();
            setUploadConfigvalues();
            #endregion

            #region domain blocking
            popWebinarDomain();
            #endregion

            setPreviewLink();
            Session["WebinarID"] = hWebinarID.Value;

            //Accordion1.SelectedIndex = 1;   
            //Premium feature check
            //Feature ID 7 - Adavance registration setting option
            if (Session["PREMIUM_FEATURE"].ToString().IndexOf(",7,") < 0)
            {
                spPre1.Visible = true;
                chkAPI.Enabled = false;

                spPre2.Visible = true;
                chkCamp.Enabled = false;

                spPre3.Visible = true;
                spAdd.Visible = false;
                chkHotmail.Enabled = chkYahoo.Enabled = chkGmail.Enabled = chkAol.Enabled = chksbc.Enabled = false;
            }
            //Feature ID 37 - Webinar Invite
            if (Session["PREMIUM_FEATURE"].ToString().IndexOf(",37,") < 0)
            {
                spPre4.Visible = true;
                phPreEnable.Visible = false;
                phPreDisable.Visible = true;
            }
        }

        private void setPreviewLink()
        {
            List<WebinarURLs> objURL = new List<WebinarURLs>();
            objURL = objWebinarDA.GetWebinarURLsDA(Convert.ToInt32(hWebinarID.Value));
            if (objURL.Count > 0)
            {
                if (objURL[0].RegistrationURL != "")
                {
                    //rwndRegPreview.Visible = true;
                    //rwndRegPreview.NavigateUrl = Constant.WebinarbaseURL + "_" + objURL[0].RegistrationURL;
                    hRegGUID.Value = Constant.WebinarbaseURL + "_" + objURL[0].RegistrationURL;
                }
                //else
                //    rwndRegPreview.Visible = false;

                if (objURL[0].AudienceInterfaceURL != "")
                {
                    //rwndRegWR.Visible = true;
                    //rwndRegWR.NavigateUrl = Constant.WebinarViewerBaseURL + objURL[0].AudienceInterfaceURL;
                    hAudGUID.Value = Constant.WebinarViewerBaseURL + "_" + objURL[0].AudienceInterfaceURL;
                }
                //else
                //    rwndRegWR.Visible = false;
            }
        }

        protected void tgrdQAList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //Set Response Type Value
            if (e.Item is GridDataItem)
            {
                GridDataItem dataBoundItem = e.Item as GridDataItem;
                Label lblRegFormQuestion = (Label)dataBoundItem.FindControl("lblRegFormQuestion");
                Label lblResponseType = (Label)dataBoundItem.FindControl("lblQResponseType");

                //Get Question Size
                if (lblRegFormQuestion.Text.Length > 100)
                    lblRegFormQuestion.Text = lblRegFormQuestion.Text.Substring(0, 97) + "...";

                //Get Response Type Value
                if (lblResponseType.Text == "COMM")
                {
                    lblResponseType.Text = "Comment";
                }
                else if (lblResponseType.Text == "MCSA")
                {
                    lblResponseType.Text = "Multiple Choice - Single Answer";
                }
                else if (lblResponseType.Text == "MCMA")
                {
                    lblResponseType.Text = "Multiple Choice - Multiple Answer";
                }

                if (_isWebinarPast)
                {
                    ImageButton ibtn = (ImageButton)dataBoundItem.FindControl("btnEdit");
                    ibtn.Visible = false;
                    ibtn = (ImageButton)dataBoundItem.FindControl("btnDelete");
                    ibtn.Visible = false;
                }
            }
        }

        protected void tgrdQAList_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "editQA")
            {
                List<WebinarRegFormQA> objFormQA = objWebinarDA.getWebinarRegFormQADetail(Convert.ToInt32(e.CommandArgument));
                if (objFormQA.Count > 0)
                {
                    txtQuestion.Text = objFormQA[0].RegFormQuestion;
                    rcmbType.SelectedValue = objFormQA[0].QResponseType;
                    if (objFormQA[0].QResponseType != "COMM")
                    {
                        TextBox txt;
                        for (int idx = 1; idx <= 5; idx++)
                        {
                            txt = (TextBox)this.FindControl("txtRespone" + idx.ToString());
                            if (txt != null)
                                txt.Text = "";
                        }
                    }
                    if (objFormQA[0].QResponseOptions.Trim() != "")
                    {
                        ArrayList values = objUtil.StringToArrayList(objFormQA[0].QResponseOptions.Trim(), new char[] { ';' });
                        if (values.Count > 0)
                        {
                            TextBox txt;
                            for (int idx = 1; idx <= values.Count; idx++)
                            {
                                txt = (TextBox)this.FindControl("txtRespone" + idx.ToString());
                                if (txt != null)
                                    txt.Text = values[idx - 1].ToString();
                            }
                        }
                    }

                    hqaID.Value = e.CommandArgument.ToString();
                    //btnEditCancel.Visible = true;
                    btnAddQuestion.Text = "Edit form question";
                    rcmbType.Enabled = false;

                    // setting the UI visible aspects
                    //dvQADetails.Visible = true;
                    //dvQADetails.Attributes.Remove("style");
                    //dvQADetails.Attributes.Add("style", "display: block;");

                    dvAddQBtn.Visible = false;
                    dvQADetails.Visible = true;
                    dvAddQA.Visible = true;
                    dvQAList.Visible = true;

                    if (objFormQA[0].QResponseType == "COMM")
                        tbResp.Visible = false;
                    else
                    {
                        tbResp.Visible = true;
                        dvQADetails.Attributes.Remove("style");
                        dvQADetails.Attributes.Add("style", "display: block;");

                        HtmlGenericControl sp;
                        for (int idx = 1; idx <= 5; idx++)
                        {
                            sp = (HtmlGenericControl)this.FindControl("spQL" + idx.ToString());
                            sp.Attributes.Remove("style");
                            if (objFormQA[0].QResponseType == "DD")
                                sp.Attributes.Add("style", "display:block;");
                            else
                                sp.Attributes.Add("style", "display:block;");

                            sp = (HtmlGenericControl)this.FindControl("spQC" + idx.ToString());
                            sp.Attributes.Remove("style");
                            if (objFormQA[0].QResponseType == "MCMA")
                                sp.Attributes.Add("style", "display:block;");
                            else
                                sp.Attributes.Add("style", "display:none;");

                            sp = (HtmlGenericControl)this.FindControl("spQR" + idx.ToString());
                            sp.Attributes.Remove("style");
                            if (objFormQA[0].QResponseType == "MCSA")
                                sp.Attributes.Add("style", "display:block;");
                            else
                                sp.Attributes.Add("style", "display:none;");
                        }
                    }
                }
            }
            else
            {
                objWebinarDA.DeleteWebinarFormqaID(Convert.ToInt32(hWebinarID.Value), Convert.ToInt32(e.CommandArgument));
                popQAs();
            }
        }

        #region drag & drop
        protected void tgrdQAList_RowDrop(object sender, GridDragDropEventArgs e)
        {
            if (string.IsNullOrEmpty(e.HtmlElement))
            {
                if (e.DestDataItem != null && e.DestDataItem.OwnerGridID == tgrdQAList.ClientID)
                {
                    //reorder items in pending grid
                    IList<WebinarRegFormQA> WebinarRegFormQA = getQAList();
                    IList<WebinarRegFormQA> webinarRegFormQA = getQAList(); //WebinarRegFormQA;

                    WebinarRegFormQA qa = getQAList(webinarRegFormQA, (int)e.DestDataItem.GetDataKeyValue("qaID"));

                    int destinationIndex = webinarRegFormQA.IndexOf(qa);

                    if (e.DropPosition == GridItemDropPosition.Above && e.DestDataItem.ItemIndex > e.DraggedItems[0].ItemIndex)
                    {
                        destinationIndex -= 1;
                    }
                    if (e.DropPosition == GridItemDropPosition.Below && e.DestDataItem.ItemIndex < e.DraggedItems[0].ItemIndex)
                    {
                        destinationIndex += 1;
                    }

                    List<WebinarRegFormQA> QAToMove = new List<WebinarRegFormQA>();
                    foreach (GridDataItem draggedItem in e.DraggedItems)
                    {
                        WebinarRegFormQA tmpOrder = getQAList(webinarRegFormQA, (int)draggedItem.GetDataKeyValue("qaID"));
                        if (tmpOrder != null)
                            QAToMove.Add(tmpOrder);
                    }

                    foreach (WebinarRegFormQA qaToMove in QAToMove)
                    {
                        webinarRegFormQA.Remove(qaToMove);
                        webinarRegFormQA.Insert(destinationIndex, qaToMove);
                    }
                    //WebinarRegFormQA = webinarRegFormQA;
                    //
                    for (int idx = 0; idx < webinarRegFormQA.Count; idx++)
                    {
                        objWebinarDA.UpdateRegFormQAOrder(webinarRegFormQA[idx].qaID, idx + 1);
                    }
                    popQAs();
                    //int destinationItemIndex = destinationIndex - (tgrdQAList.PageSize * tgrdQAList.CurrentPageIndex);
                    //e.DestinationTableView.Items[destinationItemIndex].Selected = true;
                }

            }
        }

        //protected IList<WebinarRegFormQA> WebinarRegFormQA    // PendingOrders
        //{
        //    get
        //    {
        //        try
        //        {
        //            object obj = Session["WebinarRegFormQA"];
        //            if (obj == null)
        //            {
        //                obj = getQAList();
        //                if (obj != null)
        //                {
        //                    Session["WebinarRegFormQA"] = obj;
        //                }
        //                else
        //                {
        //                    obj = new List<WebinarRegFormQA>();
        //                }
        //            }
        //            return (IList<WebinarRegFormQA>)obj;
        //        }
        //        catch
        //        {
        //            Session["WebinarRegFormQA"] = null;
        //        }
        //        return new List<WebinarRegFormQA>();
        //    }
        //    set { Session["WebinarRegFormQA"] = value; }
        //}

        protected IList<WebinarRegFormQA> getQAList()
        {
            IList<WebinarRegFormQA> objIList = new List<WebinarRegFormQA>();
            List<WebinarRegFormQA> objList = objWebinarDA.getWebinarRegFormQA(Convert.ToInt32(hWebinarID.Value));
            for (int idx = 0; idx < objList.Count; idx++)
            {
                objIList.Add(new WebinarRegFormQA(objList[idx].qaID, objList[idx].QuestionOrder, objList[idx].RegFormQuestion,
                    objList[idx].QResponseType));

            }
            return objIList;
        }

        private static WebinarRegFormQA getQAList(IEnumerable<WebinarRegFormQA> QAToSearchIn, int qaId)
        {
            foreach (WebinarRegFormQA WebQA in QAToSearchIn)
            {
                if (WebQA.qaID == qaId)
                {
                    return WebQA;
                }
            }
            return null;
        }

        #endregion

        private void popQAs()
        {
            List<WebinarRegFormQA> objFormQA = objWebinarDA.getWebinarRegFormQA(Convert.ToInt32(hWebinarID.Value));
            if (objFormQA.Count > 0)
            {
                dvAddQBtn.Visible = false;
                dvAddQA.Visible = true;
                dvQAList.Visible = true;
                tgrdQAList.Visible = true;

                tgrdQAList.DataSource = objFormQA;
                tgrdQAList.DataBind();
            }
            else
            {
                dvAddQBtn.Visible = true;
                dvAddQA.Visible = false;
                dvQAList.Visible = false;
                tgrdQAList.Visible = false;
            }
        }

        protected void btnAddQuestion_click(object sender, EventArgs e)
        {
            string errString = "";
            string sRespOpt = "";
            errLbl.Text = "";
            if (txtQuestion.Text.Trim() == "")
                errString = "Question cannot be empty";
            if (errString == "")
            {
                if (rcmbType.SelectedValue != "COMM")
                {
                    TextBox txt;
                    for (int idx = 1; idx < 6; idx++)
                    {
                        txt = (TextBox)this.FindControl("txtRespone" + idx.ToString());
                        if (txt != null)
                        {
                            if (txt.Text.Trim() != "")
                                sRespOpt += txt.Text.Trim() + ";";
                        }
                    }
                    if (sRespOpt != "")
                        sRespOpt = sRespOpt.Substring(0, sRespOpt.Length - 1);
                }
                if (rcmbType.SelectedValue != "COMM" && sRespOpt == "")
                    if (errString == "")
                        errString = "Atleast one response should be entered";
                    else
                        errString = "Question cannot be empty and at least one response should be entered";

            }
            if (errString == "")
            {
                WebinarRegFormQA objWebinarRegFormQA = new WebinarRegFormQA();
                objWebinarRegFormQA.qaID = Convert.ToInt32(hqaID.Value);
                objWebinarRegFormQA.QResponseOptions = sRespOpt;
                objWebinarRegFormQA.QResponseType = rcmbType.SelectedValue;
                objWebinarRegFormQA.QuestionOrder = Convert.ToInt32(hQOrder.Value);
                objWebinarRegFormQA.RegFormQuestion = txtQuestion.Text.Trim();
                objWebinarRegFormQA.webinarID = Convert.ToInt32(hWebinarID.Value);
                errLbl.Text = objWebinarDA.InsertWebinarFormQA(objWebinarRegFormQA);
                if (errLbl.Text == "Success")
                {
                    popQAs();
                    btnEditCancel_click(null, null);
                }
                //clearQuestionFields();
            }
        }

        private void clearQuestionFields()
        {
            hqaID.Value = "0";
            //btnEditCancel.Visible = false;
            btnAddQuestion.Text = "Add to form";
            rcmbType.Text = "-Select Additional Question Type-";
            hQOrder.Value = (Convert.ToInt32(hQOrder.Value) + 1).ToString();
            txtQuestion.Text = "";
            txtRespone1.Text = "";
            txtRespone2.Text = "";
            txtRespone3.Text = "";
            txtRespone4.Text = "";
            txtRespone5.Text = "";
            dvQADetails.Attributes.Remove("style");
            dvQADetails.Attributes.Add("style", "display: none;");
            rcmbType.Enabled = true;
        }

        protected void btnEditCancel_click(object sender, EventArgs e)
        {
            clearQuestionFields();
            dvAddQBtn.Visible = true;
            dvAddQA.Visible = false;
            errLbl.Text = "";
            //dvQAList.Visible = false;
        }

        /// BLOCKED START - FOR PHASE II
        //public void raupVideoFil_FileUploaded(object sender, FileUploadedEventArgs e)
        //{

        //    telerikAsyncUploadResult result = e.UploadResult as telerikAsyncUploadResult;
        //    if (result != null)
        //    {
        //        if (result.DocumentID > 0)
        //        {
        //            objWebinarDA.UpdateRegPageVideo(result.DocumentID, Convert.ToInt32(hWebinarID.Value));
        //            hVdocID.Value = result.DocumentID.ToString();
        //        }
        //    }
        //} 

        //private void PopulateUploadedFilesList()
        //{
        //    foreach (UploadedFile file in raupVideoFile.UploadedFiles)
        //    {
        //        UploadedFileInfo uploadedFileInfo = new UploadedFileInfo(file);
        //        UploadedFiles.Add(uploadedFileInfo);
        //    }
        //}

        /// BLOCKED END - FOR PHASE II
        /// 

        private void popRegistrationDetails()
        {
            hAPIReg.Value = Session["EmailID"].ToString();
            hEmailCampaign.Value = Session["EmailID"].ToString();
            if (hWebinarID.Value != "")
            {
                List<WebinarRegistration> objWebReg = objWebinarDA.getWebinarRegistration(Convert.ToInt32(hWebinarID.Value));
                if (objWebReg.Count > 0)
                {
                    chkEnableReg.Checked = !objWebReg[0].isRegistrationEnabled;
                    hInitStatus.Value = (objWebReg[0].isRegistrationEnabled ? "1" : "0");
                    //chkIncLogo.Checked = objWebReg[0].IncludeLogoBanner;
                    chkIncSummary.Checked = objWebReg[0].IncludeSummary;
                    chkIncSpeakerBio.Checked = objWebReg[0].IncludeSpeakerBio;
                    boolConnectRegistration = objWebReg[0].isConnectRegEmailed;
                    if (objWebReg[0].APIEmails.Trim() != "")
                    {
                        txtAPIReg.Text = objWebReg[0].APIEmails.Trim();
                        hAPIReg.Value = objWebReg[0].APIEmails.Trim();
                        chkAPI.Checked = true;
                        Accordion1.RequireOpenedPane = true;
                    }
                    boolCampTrackerEmail = objWebReg[0].isCampTrackerEmailed;
                    if (objWebReg[0].CampaignTrackerEmails.Trim() != "")
                    {
                        txtEmailCampaign.Text = objWebReg[0].CampaignTrackerEmails.Trim();
                        hEmailCampaign.Value = objWebReg[0].CampaignTrackerEmails.Trim();
                        chkCamp.Checked = true;
                        Accordion1.RequireOpenedPane = true;
                    }
                    //chkVideo.Checked = objWebReg[0].isVideoFile;
                    //chkAddPresenter.Checked = objWebReg[0].isAdditionalPresenter;
                    //chkAddWebinar.Checked = objWebReg[0].isAdditionalWebinar;
                    //hVdocID.Value = objWebReg[0].videoFileDocID.ToString();
                    //if (Convert.ToInt32(hVdocID.Value) > 0)
                    //{
                    //    DocumentDA objDocumentDA = new DocumentDA();
                    //    //lbtnVideoFilename.Text = objDocumentDA.GetDocumentNameDA(Convert.ToInt32(hVdocID.Value), "Original");
                    //    //lbtnVideoFilename.Visible = true;
                    //}

                    //popRegFormFields(objWebReg[0].FormFields, objWebReg[0].FormFieldRequired);
                    List<WebinarRegFormFields> objReg = objWebinarDA.getWebinarRegFormFields(Convert.ToInt32(hWebinarID.Value));
                    CheckBox chk;
                    TextBox txt;
                    for (int idx = 0; idx < objReg.Count; idx++)
                    {
                        if (this.FindControl("chkInc" + objReg[idx].FieldID.ToString()) != null)
                        {
                            chk = (CheckBox)this.FindControl("chkInc" + objReg[idx].FieldID.ToString());
                            chk.Checked = true;
                        }
                        if (this.FindControl("txtFld" + objReg[idx].FieldID.ToString()) != null)
                        {
                            txt = (TextBox)this.FindControl("txtFld" + objReg[idx].FieldID.ToString());
                            txt.Text = objReg[idx].FieldLabel;
                        }
                        if (objReg[idx].isRequired)
                        {
                            if (this.FindControl("chkReq" + objReg[idx].FieldID.ToString()) != null)
                            {
                                chk = (CheckBox)this.FindControl("chkReq" + objReg[idx].FieldID.ToString());
                                chk.Checked = true;
                            }
                        }
                    }
                }
            }
        }

        #region deprecate
        //private void popRegFormFields()
        //{
        //    List<RegistrationFormFieldBE> objRegForm = new List<RegistrationFormFieldBE>();
        //    objRegForm = objWebinarDA.getWebinarRegFormFields(Convert.ToInt32(hWebinarID.Value)); 
        //    if (objRegForm.Count > 0)
        //    {
        //        CheckBox chk;
        //        for (int idx = 0; idx < objRegForm.Count; idx++)
        //        {
        //            if (this.FindControl("chkInc" + objRegForm[idx].FieldID.ToString()) != null)
        //            {
        //                chk = (CheckBox) this.FindControl("chkInc" + objRegForm[idx].FieldID.ToString());
        //                chk.Checked = true;
        //            }
        //            if (objRegForm[idx].isRequired)
        //            {
        //                if (this.FindControl("chkReq" + objRegForm[idx].FieldID.ToString()) != null)
        //                {
        //                    chk = (CheckBox) this.FindControl("chkReq" + objRegForm[idx].FieldID.ToString());
        //                    chk.Checked = true;
        //                }
        //            }
        //        }
        //    }
        //}

        //private void popRegFormFields(string regForm, string regFormReq)
        //{
        //    ArrayList valRegForm = objUtil.StringToArrayList(regForm, new char[] { ',' });
        //    ArrayList valRegFormReq = objUtil.StringToArrayList(regFormReq, new char[] { ',' });

        //    //List<RegistrationFormFieldBE> objRegForm = new List<RegistrationFormFieldBE>();
        //    //objRegForm = objWebinarDA.getWebinarRegFormFields(Convert.ToInt32(hWebinarID.Value));
        //    CheckBox chk;
        //    for (int idx = 0; idx < valRegForm.Count; idx++)
        //    {
        //        if (this.FindControl("chkInc" + valRegForm[idx].ToString()) != null)
        //        {
        //            chk = (CheckBox)this.FindControl("chkInc" + valRegForm[idx].ToString());
        //            chk.Checked = true;
        //        }
        //        if (valRegFormReq[idx].ToString() == "1")
        //        {
        //            if (this.FindControl("chkReq" + valRegForm[idx].ToString()) != null)
        //            {
        //                chk = (CheckBox)this.FindControl("chkReq" + valRegForm[idx].ToString());
        //                chk.Checked = true;
        //            }
        //        }
        //    }
        //}
        #endregion

        public string saveRegistrationInfo()
        {
            string strError = string.Empty;

            if (chkAPI.Checked && !objUtil.isEmail(txtAPIReg.Text.Trim()))
                strError = "Registration API instructions email is missing or invalid<br>";
            if (chkCamp.Checked && !objUtil.isEmail(txtEmailCampaign.Text.Trim()))
                strError = strError + "Webinar Campaign Tracking email is missing or invalid";

            if (strError == string.Empty)
            {
                foreach (RepeaterItem iter in rpLogo.Items)
                {
                    if (iter.ItemType == ListItemType.Item || iter.ItemType == ListItemType.AlternatingItem)
                    {
                        Image sImg = ((Image)iter.FindControl("imgLogo"));
                        string sVal = sImg.Attributes["Name"]; //((Image)iter.FindControl("imgLogo")).ImageUrl;
                        string hDocID = ((HiddenField)iter.FindControl("hDocID")).Value.ToString();
                    }
                }

                #region registration info - tblwebinarregistration
                WebinarRegistration objWebReg = new WebinarRegistration();
                objWebReg.WebinarID = Convert.ToInt32(hWebinarID.Value);
                //objWebReg.isRegistrationEnabled = chkEnableReg.Checked;
                //objWebReg.isAdditionalPresenter = chkAddPresenter.Checked;
                objWebReg.isVideoFile = false;
                objWebReg.isAdditionalWebinar = false;
                objWebReg.APIEmails = txtAPIReg.Text.Trim();
                objWebReg.IncludeLogoBanner = true; //chkIncLogo.Checked;
                objWebReg.IncludeSummary = chkIncSummary.Checked;
                objWebReg.IncludeSpeakerBio = chkIncSpeakerBio.Checked;
                objWebReg.CampaignTrackerEmails = txtEmailCampaign.Text.Trim();
                objWebinarDA.SaveWebinarRegistration(objWebReg);
                if (chkCamp.Checked && txtEmailCampaign.Text.Trim() != "")
                {
                    boolCampTrackerEmail = true;
                }
                if (chkAPI.Checked && txtAPIReg.Text.Trim() != "")
                    boolConnectRegistration = true;
                #endregion

                #region registration form
                List<WebinarRegFormFields> objWebinarRegFormFields = new List<WebinarRegFormFields>();
                CheckBox chk;
                CheckBox chk1;
                TextBox txt1;

                int webID = Convert.ToInt32(hWebinarID.Value);
                for (int idx = 1; idx < 19; idx++)
                {
                    if (this.FindControl("chkInc" + idx.ToString()) != null && this.FindControl("txtFld" + idx.ToString()) != null)
                    {
                        chk = (CheckBox)this.FindControl("chkInc" + idx.ToString());
                        chk1 = (CheckBox)this.FindControl("chkReq" + idx.ToString());
                        txt1 = (TextBox)this.FindControl("txtFld" + idx.ToString());
                        if (chk.Checked && txt1.Text.Trim() != "")
                        {
                            objWebinarRegFormFields.Add(new WebinarRegFormFields
                            {
                                webinarID = webID,
                                FieldID = idx,
                                FieldLabel = txt1.Text,
                                isRequired = chk1.Checked
                            });
                        }
                    }
                }
                objWebinarDA.UpdateRegFormFields(objWebinarRegFormFields);
                #endregion

                #region theme, header type
                saveWebTheme();
                popLogo();
                #endregion

                #region domain blocking
                saveWebinarDomain();
                #endregion

                regTabLoad();
            }
            return strError;
        }

        #region Theme and logo related functions

        protected void rcmbHeader_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            hCurrentImg.Value = "1";
            setMaxFileCount();

            //rcmbHeader.Text = e.Text;
        }

        private void setMaxFileCount()
        {
            switch (rcmbHeader.SelectedIndex)
            {
                //case 0:
                //    aupLogo.MaxFileInputsCount = 1 - _logoCount;
                //    break;
                case 0:
                    aupLogo.MaxFileInputsCount = Constant.MaxLogoCount - _logoCount;
                    Session["FolderType"] = "LOGO";
                    break;
                case 1:
                    aupLogo.MaxFileInputsCount = 1 - _logoCount;
                    Session["FolderType"] = "BANNER";
                    break;
            }

            if (aupLogo.MaxFileInputsCount <= 0)
                aupLogo.Visible = false;
            else
                aupLogo.Visible = true;
        }

        public void aupLogo_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            telerikAsyncUploadResult result = e.UploadResult as telerikAsyncUploadResult;
            if (result != null)
            {
                if (result.DocumentID > 0)
                {
                    WebinarResource objWebinarResource = new WebinarResource();
                    objWebinarResource.WebinarID = Convert.ToInt32(hWebinarID.Value);
                    objWebinarResource.DocID = result.DocumentID;
                    objWebinarResource.ResourceType = (rcmbHeader.SelectedValue == "BANNER" ? "BANNER" : "Logo");
                    objWebinarResource.ResourceTitle = e.File.FileName;
                    objWebinarResource.ResourceValue = "";
                    objWebinarResource.IsBriefcase = false;
                    objWebinarResource.LogoUrlName = "";
                    objWebinarResource.LogoUrl = "";
                    objWebinarDA.InsertWebinarResources(objWebinarResource);
                }
            }
        }

        protected void rpLogo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                WebinarResource objRes = (WebinarResource)(e.Item.DataItem);

                //string DocID = ((HiddenField)e.Item.FindControl("hDocID")).Value;
                //string ResourceType = ((HiddenField)e.Item.FindControl("hResourceType")).Value;
                //setResizedLogoURL(Convert.ToInt32(DocID));

                HtmlGenericControl dvL = (HtmlGenericControl)e.Item.FindControl("dvLogoContainer");
                //HtmlGenericControl dvB = (HtmlGenericControl)e.Item.FindControl("dvBannerContainer");

                //Image rimg1 = ((Image)e.Item.FindControl("imgBanner"));
                Image rimg = ((Image)e.Item.FindControl("imgLogo"));
                //if (ResourceType.ToUpper() == "BANNER")
                //{
                //    dvB.Visible = true;
                //    dvL.Visible = false;
                //    if (rimg1 != null)
                //    {
                //        rimg1.ImageUrl = "~/handler/showImage.ashx?ID=" + DocID;
                //        rimg1.Attributes.Add("Name", DocID);
                //        hdynImgIDs.Value += ImgID + ",";
                //        ++ImgID;
                //    }
                //}
                //else
                //{
                //  dvB.Visible = false;
                dvL.Visible = true;
                if (rimg != null)
                {
                    rimg.ImageUrl = "~/handler/showImage.ashx?ID=" + Convert.ToString(objRes.DocID);
                    rimg.Attributes.Add("Name", Convert.ToString(objRes.DocID));
                    rimg.ToolTip = Convert.ToString(objRes.LogoUrlName);
                    hdynImgIDs.Value += ImgID + ",";
                    ++ImgID;
                    if (objRes.LogoUrlName != "")
                    {
                        HyperLink lnkAttribute = ((HyperLink)e.Item.FindControl("hlnkAttribute"));
                        lnkAttribute.Text = "[Edit Attribute]";
                    }
                    if (_isWebinarPast)
                    {
                        ImageButton delBtn = (ImageButton)e.Item.FindControl("imgbtnDel");
                        delBtn.Visible = false;
                    }
                }

                //}
            }
        }

        private void setResizedLogoURL(int docID)
        {
            bool isResizeRequired = false;
            DocumentDA objDocDA = new DocumentDA();
            string rtnFilePath = objDocDA.GetDocumentPath(docID);

            if (rtnFilePath != "")
            {
                if (File.Exists(Constant.DocRepoClient + rtnFilePath))
                {
                    rtnFilePath = Constant.DocRepoClient + rtnFilePath;
                    isResizeRequired = true;
                }
                else
                    rtnFilePath = Constant.DocRepRoot + "NoDocs.png";
            }
            else
                rtnFilePath = Constant.DocRepRoot + "NoDocs.png";

            if (isResizeRequired)
            {
                List<DocumentBE> objDoc = objDocDA.GetDocumentDA(docID);
                if (objDoc.Count > 0)
                {
                    if (!objDoc[0].isResized)
                    {

                        ImageUtility objImgUtil = new ImageUtility();
                        System.Drawing.Image original = System.Drawing.Image.FromFile(rtnFilePath);
                        System.Drawing.Image resized = objImgUtil.ResizeImage(original, (objDoc[0].Category.ToUpper() == "LOGO" ? Common.Constant.LogoSize : Common.Constant.BannerSize));
                        string SavedFileName = "";

                        switch ((Path.GetExtension(objDoc[0].SavedFileName).Substring(1)).ToUpper())
                        {
                            case "PNG":
                                SavedFileName = docID.ToString() + "_rs.png";
                                resized.Save(Constant.DocRepoClient + Session["ClientID"].ToString() + "\\Logo\\" + SavedFileName, System.Drawing.Imaging.ImageFormat.Png);
                                break;
                            case "JPG":
                                SavedFileName = docID.ToString() + "_rs.jpg";
                                resized.Save(Constant.DocRepoClient + Session["ClientID"].ToString() + "\\Logo\\" + SavedFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            case "GIF":
                                SavedFileName = docID.ToString() + "_rs.gif";
                                resized.Save(Constant.DocRepoClient + Session["ClientID"].ToString() + "\\Logo\\" + SavedFileName, System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                        }
                        objDocDA.SaveDocumentDA(docID, "isResized", "1");
                        objDocDA.SaveDocumentDA(docID, "SavedFileName", "'" + SavedFileName + "'");
                    }
                }
            }
        }

        protected void rpLogo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandArgument != "")
            {
                objWebinarDA.DeleteRegFormResources(Convert.ToInt32(hWebinarID.Value), Convert.ToInt32(e.CommandArgument));
                DocAccess objDoc = new DocAccess();
                objDoc.removeDocumentFromRepositary(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session["ClientID"]), "Logo");
                popLogo();
            }
        }
        protected void imgbtnDel2_Click(object sender, EventArgs e)
        {

            if (hBannerDocID.Value != "")
            {
                objWebinarDA.DeleteRegFormResources(Convert.ToInt32(hWebinarID.Value), Convert.ToInt32(hBannerDocID.Value));
                DocAccess objDoc = new DocAccess();
                objDoc.removeDocumentFromRepositary(Convert.ToInt32(hBannerDocID.Value), Convert.ToInt32(Session["ClientID"]), "Logo");
                popLogo();
            }
        }

        private void popLogo()
        {
            string headerType = rcmbHeader.SelectedValue;
            List<WebinarResource> objRes = objWebinarDA.getRegFormResoures(Convert.ToInt32(hWebinarID.Value));
            if (objRes.Count > 0)
            {
                hdynImgIDs.Value = "";
                ImgID = 0;
                logocanvas.Visible = true;
                rcmbHeader.SelectedValue = objRes[0].ResourceType;
                if (objRes[0].ResourceType.ToUpper() == "LOGO")
                {
                    phLogo.Visible = true;
                    phBanner.Visible = false;
                    rpLogo.DataSource = objRes;
                    rpLogo.DataBind();
                }
                else
                {
                    if (objRes.Count == 0)
                    {
                        dvBannerContainer.Visible = false;
                    }
                    else
                    {
                        dvBannerContainer.Visible = true;
                        phLogo.Visible = false;
                        phBanner.Visible = true;
                        imgBanner.ImageUrl = "~/handler/showImage.ashx?ID=" + objRes[0].DocID.ToString();
                        hBannerDocID.Value = objRes[0].DocID.ToString();
                        bannerHref.HRef = objRes[0].LogoUrl;
                        imgBanner.ToolTip = objRes[0].LogoUrlName;
                        hlnkAttribute.Attributes.Add("data-id", objRes[0].DocID.ToString());
                        if (objRes[0].LogoUrlName != "")
                        {
                            hlnkAttribute.Text = "[Edit Attribute]";
                        }
                    }
                }
            }
            else
            {
                rcmbHeader.SelectedValue = "Logo";
                logocanvas.Visible = false;
            }
            //if (headerType.ToUpper() == "LOGO")

            _logoCount = objRes.Count;
            hLogoCount.Value = _logoCount.ToString();
            setMaxFileCount();
        }

        private void getThemeLayouts()
        {

            DirectoryInfo objDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/") + "Images\\Theme");
            FileInfo[] fi = objDir.GetFiles("layout*.png");
            FileInfo[] fi1 = objDir.GetFiles("layout*.png");

            for (int idx = 0; idx < fi.Length; idx++)
            {
                if (getThemeID(fi[idx].Name) == hSelThemeID.Value)
                {
                    int nxSt = 0;
                    for (int idx1 = idx; idx1 < fi.Length; idx1++)
                    {
                        fi1[nxSt] = fi[idx1];
                        nxSt++;
                    }
                    for (int idx1 = 0; idx1 < idx; idx1++)
                    {
                        fi1[nxSt] = fi[idx1];
                        nxSt++;
                    }
                    break;
                }
            }
            rpThemeCarousel.DataSource = fi1;
            rpThemeCarousel.DataBind();
        }

        protected string getThemeID(object layoutFileName)
        {
            string FName = layoutFileName.ToString();
            FName = FName.Replace(".png", "");
            FName = FName.Replace("layout", "");
            return FName;
        }

        private void setUploadConfigvalues()
        {
            telerikUploadConfig config = aupLogo.CreateDefaultUploadConfiguration<telerikUploadConfig>();
            // Populate any additional fields
            config.ActionID = Convert.ToInt32(Session["UserID"]);
            config.ClientID = Convert.ToInt32(Session["ClientID"]);
            config.FolderType = "LOGO";
            // The upload configuration will be available in the handler
            aupLogo.UploadConfiguration = config;
            //since config calue is not getting passed, the foldertype also pass thru session value
            if (rcmbHeader.SelectedValue.ToUpper() == "BANNER")
                Session["FolderType"] = "BANNER";
            else
                Session["FolderType"] = "LOGO";
        }

        public void saveWebTheme()
        {
            WebinarTheme objTheme = new WebinarTheme();
            objTheme.HeaderType = rcmbHeader.SelectedValue;
            objTheme.ThemeLayoutID = Convert.ToInt32(hSelThemeID.Value);
            objTheme.WebinarID = Convert.ToInt32(hWebinarID.Value);
            objWebinarDA.SaveWebinarTheme(objTheme);
            if (hdynImgIDs.Value.Trim() != "")
            {
                objWebinarDA.UpdateRegFormLogoOrder(Convert.ToInt32(hWebinarID.Value), objUtil.StringToArrayList(hdynImgIDs.Value.Trim(), new char[] { ',' }));
            }
        }

        private void getThemeDetails()
        {
            if (hWebinarID.Value != "")
            {
                List<WebinarTheme> objWebTheme = objWebinarDA.getWebinarTheme(Convert.ToInt32(hWebinarID.Value));
                if (objWebTheme.Count > 0)
                {
                    rcmbHeader.SelectedValue = objWebTheme[0].HeaderType;
                    hSelThemeID.Value = objWebTheme[0].ThemeLayoutID.ToString();
                    popLogo();
                    //if (objWebTheme[0].HeaderType.ToUpper() == "BANNER")
                    //    Session["FolderType"] = "BANNER";
                    //else
                    //    Session["FolderType"] = "LOGO";
                }
            }
        }

        #endregion

        #region domain blocking

        private void setNonStdDominCount()
        {
            rowCount.Value = objWebinarDA.GetebinarNonStdHostCount(Convert.ToInt32(hWebinarID.Value)).ToString();
        }

        private void popWebinarDomain()
        {
            List<WebinarHostBE> objWebHost = new List<WebinarHostBE>();
            objWebHost = objWebinarDA.GetWebinarHostDA(Convert.ToInt32(hWebinarID.Value));
            chkHotmail.Checked = chkYahoo.Checked = chkGmail.Checked = chkAol.Checked = chksbc.Checked = false;
            int nonStdDomainCount = 0;
            int c = 1, r = 1, ot = 1;
            bool flg = false;
            for (int idx = 0; idx < objWebHost.Count; idx++)
            {
                switch (objWebHost[idx].WebinarHost.ToUpper())
                {
                    case "HOTMAIL.COM":
                        chkHotmail.Checked = true;
                        flg = true;
                        break;
                    case "YAHOO.COM":
                        chkYahoo.Checked = true;
                        flg = true;
                        break;
                    case "GMAIL.COM":
                        chkGmail.Checked = true;
                        flg = true;
                        break;
                    case "AOL.COM":
                        chkAol.Checked = true;
                        flg = true;
                        break;
                    case "SBCGLOBAL.COM":
                        chksbc.Checked = true;
                        flg = true;
                        break;
                    default:
                        TextBox txt = (TextBox)AccordionPane1.FindControl("txtr" + r.ToString() + "c" + c.ToString());
                        if (txt != null)
                        {
                            txt.Text = objWebHost[idx].WebinarHost;
                            ot++;
                            nonStdDomainCount++;
                        }
                        CheckBox chk = (CheckBox)AccordionPane1.FindControl("chkr" + r.ToString() + "c" + c.ToString());
                        if (chk != null)
                        {
                            chk.Checked = true;
                            flg = true;
                            hDomainNonStdShow.Value = "1";
                        }
                        ++c;
                        if (c > 5)
                        {
                            ++r;
                            c = 1;
                        }
                        break;
                }
            }
            if (flg)
            {
                Accordion1.RequireOpenedPane = true;
                //nonStdDomainCount / 5
                rowCount.Value = (nonStdDomainCount / 5 + (nonStdDomainCount % 5 > 0 ? 1 : 0)).ToString();
                switch (Convert.ToInt32(rowCount.Value))
                {
                    //case 0:
                    //    domainBlockBtn.Visible = true;
                    //    break;
                    case 1:
                        row1.Visible = true;
                        break;
                    case 2:
                        row1.Visible = true;
                        row2.Visible = true;
                        break;
                    case 3:
                        row1.Visible = true;
                        row2.Visible = true;
                        row3.Visible = true;
                        //domainBlockBtn.Visible = false;
                        break;
                    default:
                        row1.Visible = true;
                        row2.Visible = true;
                        row3.Visible = true;
                        //domainBlockBtn.Visible = false;
                        break;
                }
            }
        }

        private void saveWebinarDomain()
        {
            StringBuilder strDomain = new StringBuilder();

            int webID = Convert.ToInt32(hWebinarID.Value);
            if (chkHotmail.Checked)
                strDomain.Append("'hotmail.com',");
            //objWebinarDA.SaveWebinarDomains(webID, "hotmail.com");
            if (chkYahoo.Checked)
                strDomain.Append("'yahoo.com',");
            //objWebinarDA.SaveWebinarDomains(webID, "yahoo.com");
            if (chkGmail.Checked)
                strDomain.Append("'gmail.com',");
            //objWebinarDA.SaveWebinarDomains(webID, "gmail.com");
            if (chkAol.Checked)
                strDomain.Append("'aol.com',");
            //objWebinarDA.SaveWebinarDomains(webID, "aol.com");
            if (chksbc.Checked)
                strDomain.Append("'sbcglobal.com',");
            //objWebinarDA.SaveWebinarDomains(webID, ".sbcglobal");

            for (int idx = 1; idx < 4; idx++)
            {
                for (int jdx = 1; jdx < 6; jdx++)
                {

                    //CheckBox chk = (CheckBox)this.FindControl("chkr" + idx + "c" + jdx);
                    CheckBox chk = (CheckBox)AccordionPane1.FindControl("chkr" + idx + "c" + jdx);

                    if (chk != null)
                    {
                        if (chk.Checked)
                        {
                            TextBox txt1 = (TextBox)AccordionPane1.FindControl("txtr" + idx + "c" + jdx);
                            if (txt1 != null)
                                if (txt1.Text.Trim() != "")
                                    strDomain.Append("'" + txt1.Text.Trim() + "',");
                            //objWebinarDA.SaveWebinarDomains(webID, txt1.Text.Trim());
                        }
                    }
                }
            }
            if (strDomain.ToString() != "")
            {
                string str1 = strDomain.ToString();
                objWebinarDA.SaveWebinarDomains(webID, str1.Substring(0, str1.Length - 1));
            }
        }

        #endregion

        protected void btnNewQuestion_Click(object sender, EventArgs e)
        {
            dvAddQBtn.Visible = false;
            dvQADetails.Visible = true;
            dvAddQA.Visible = true;
            dvQAList.Visible = true;
            rcmbType.SelectedValue = "COMM";
            errLbl.Text = "";
        }

        protected void rpThemeCarousel_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hFld = ((HiddenField)e.Item.FindControl("hThemeLayoutID"));
                if (hFld != null)
                {
                    string thID = getThemeID(hFld.Value);
                    HtmlImage hImg = ((HtmlImage)e.Item.FindControl("imgTheme"));
                    hImg.Src = "~/Images/Theme/" + hFld.Value;
                    hImg.Attributes.Add("data-themeurl", thID);

                    if (thID == hSelThemeID.Value)
                    {
                        hImg.Attributes.Remove("class");
                        hImg.Attributes.Add("class", "previewBtn selected");
                    }
                }
            }
        }

        protected void lnkUpdateLogo_Click(object sender, EventArgs e)
        {
            #region theme, header type
            popLogo();
            #endregion
        }

        protected void lbtnEmailInvite1_Click(object sender, EventArgs e)
        {
            try
            {
                int reqID = SaveToEmailJob(Session["EmailID"].ToString(), "Webinar Invitation");
                string sMsg = "Successfully emailed...";
                if (reqID > 0)
                {
                    EmailApp objEmailing = new EmailApp();
                    objEmailing.SendEmail(reqID, Convert.ToInt32(hWebinarID.Value));
                }
                else
                    sMsg = "Error encountered in emailing";
                lblMsg.Text = sMsg;
            }
            catch (Exception ex)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "ERROR: " + ex.Message;
            }
        }

        private int SaveToEmailJob(string toEmail, string reqTyp)
        {
            EmailBE objEmailBE = new EmailBE();
            EmailDA objEmailDA = new EmailDA();
            EmailApp objEmailing = new EmailApp();

            string emlContent = string.Empty;
            string frEmail = string.Empty;
            string subject = string.Empty;
            switch (reqTyp)
            {
                case "Webinar Invitation":
                    int webinarID = Convert.ToInt32(hWebinarID.Value);
                    List<WebinarEmailBE> objWBEmail = new List<WebinarEmailBE>();
                    objWBEmail = objEmailDA.GetWebinarEmail(webinarID, reqTyp);
                    emlContent = objEmailing.getHTMLFormattedEmailContent(objWBEmail[0].EmailContent, objWBEmail[0].RequestType, webinarID);
                    emlContent = objEmailing.getFormedEmailContent(emlContent, reqTyp, webinarID);
                    frEmail = Session["EmailID"].ToString();
                    subject = "Webinar Invitation";
                    break;
                case "Campaign Tracking":
                    emlContent = objEmailing.getHTMLFormattedGeneralEmail(reqTyp, Convert.ToInt32(Session["Client_LanguageID"]));
                    frEmail = "support@ebird.com";
                    subject = "SnapSession Webinar Campaign Tracking";
                    break;
                case "Connect Your Registration":
                    emlContent = objEmailing.getHTMLFormattedGeneralEmail(reqTyp, Convert.ToInt32(Session["Client_LanguageID"]));
                    frEmail = "support@ebird.com";
                    subject = "SnapSession Connect Your Registration";
                    break;
            }

            objEmailBE.isToEmailRef = false;
            objEmailBE.RequestStatus = "No-delay";
            objEmailBE.RequestType = reqTyp;
            objEmailBE.Subject = subject;
            objEmailBE.SubmittedBy = Convert.ToInt32(Session["UserID"]);
            objEmailBE.ToEmail = toEmail;
            objEmailBE.FromEmail = frEmail;
            objEmailBE.EmailContent = emlContent;
            return objEmailDA.SaveEmailRequest(objEmailBE);
        }

        public void CampaignTrackingEmailing()
        {

            if (chkCamp.Checked && !CampTrackerEmail)
            {
                int reqID = SaveToEmailJob(txtEmailCampaign.Text.Trim(), "Campaign Tracking");
                if (reqID > 0)
                {
                    EmailApp objEmailing = new EmailApp();
                    objEmailing.SendEmail(reqID, Constant.DocRepRoot + Constant.CampTrackInstructionDoc);
                    objWebinarDA.UpdateWebinarRegistrationEmailUpdate(Convert.ToInt32(hWebinarID.Value), "CampTracking");
                }
            }
        }

        public void ConnectRegistrationEmailing()
        {
            if (chkAPI.Checked && !ConnectRegistration)
            {
                int reqID = SaveToEmailJob(txtAPIReg.Text.Trim(), "Connect Your Registration");
                if (reqID > 0)
                {
                    EmailApp objEmailing = new EmailApp();
                    objEmailing.SendEmail(reqID, Constant.DocRepRoot + Constant.ConnectRegInstructionDoc);
                    objWebinarDA.UpdateWebinarRegistrationEmailUpdate(Convert.ToInt32(hWebinarID.Value), "ConnectReg");
                }
            }
        }

    }
}