<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="profile.ascx.cs" Inherits="EBird.Web.App.UserControls.profile" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link rel="stylesheet" href="/styles/newstyle1.css" />
<asp:ValidationSummary ID="xevs_userAdmin" CssClass="ValidationSummary" HeaderText=""
    runat="server" ShowSummary="False" ShowMessageBox="True" />
<script type="text/javascript">
    function OnClientLoad(editor, args) {
        var style = editor.get_contentArea().style;
        style.color = "black";
    }
</script>
<style type="text/css">
    .ruUploadProgress, li .ruCancel, li .ruRemove
    {
        visibility: hidden;
    }
    li.ruUploading
    {
        display: none;
    }
</style>
<div class="RPart">
    <div class="TopBg">
    </div>
    <div class="widgets">
        <!-- START: page content -->
        <table width="97%" align="center" border="0">
            <tr>
                <td align="right" style="padding-bottom: 10px;">
                    <asp:Literal ID="ltrBack" runat="server" Text="Return to <a href='Webinar' class='lnkBtn1'>My Webinar</a> to manage or start your webinars"></asp:Literal>
                </td>
            </tr>
            <tr style="float:left;">
                <td align="left">
                    <div class="FormCont Steps" style="width: 700px; padding-bottom: 20px; margin-bottom: 10px">
                        <table border="0" width="100%">
                            <tr>
                                <td align="left" style="padding-bottom: 10px;">
                                    <asp:Label ID="lblContatctCap" runat="server" Font-Bold="True" Text="Contact Information"
                                        CssClass="frmHeading"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="20%" style="padding-left: 5px">
                                    <asp:Label ID="lblFName" runat="server" Text="First Name" CssClass="frmFields" /><span
                                        class='EBmsg'>&nbsp;*</span>
                                </td>
                                <td width="80%">
                                    <asp:TextBox ID="txtFName" runat="server" MaxLength="50" Height="20" Width="250"
                                        onchange="javascript:setpresenterField('N');" CssClass="textbox_EB"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="vx_txtFName" runat="server" CssClass="ValidationSummary"
                                        ControlToValidate="txtFName" ErrorMessage="Please enter required field - First Name"
                                        ForeColor="Red" Display="None" Text="*" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 5px">
                                    <asp:Label ID="lblLName" runat="server" Text="Last Name" CssClass="frmFields" /><span
                                        class='EBmsg'>&nbsp;*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLName" runat="server" MaxLength="50" Height="20" Width="250"
                                        onchange="javascript:setpresenterField('N');" CssClass="textbox_EB"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="vx_txtLName" runat="server" CssClass="ValidationSummary"
                                        ControlToValidate="txtLName" ErrorMessage="Please enter required field - Last Name"
                                        ForeColor="Red" Display="None" Text="*" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 5px">
                                    <asp:Label ID="lblEmail" runat="server" Text="Email Address" CssClass="frmFields" /><span
                                        class='EBmsg'>&nbsp;*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" Height="20" MaxLength="255" Width="250"
                                        CssClass="textbox_EB" Enabled="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="vx_txtEmail" runat="server" CssClass="ValidationSummary"
                                        ControlToValidate="txtEmail" ErrorMessage="Please enter required field - Email Address"
                                        ForeColor="Red" Display="None" Text="*" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" CssClass="ValidationSummary"
                                        ControlToValidate="txtEmail" Display="None" ForeColor="Red" ErrorMessage="Email must be in the form someone@domain.com. Check leading and trailing space(s)."
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 5px">
                                    <asp:Label ID="lblTelephone" runat="server" Text="Telephone" CssClass="frmFields" /><span
                                        class='EBmsg'>&nbsp;*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTelephone" runat="server" Height="20" MaxLength="15" Width="200"
                                        CssClass="textbox_EB"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="vx_txtTelephone" runat="server" CssClass="ValidationSummary"
                                        ControlToValidate="txtTelephone" ErrorMessage="Please enter required field - Telephone"
                                        ForeColor="Red" Display="None" Text="*" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="ValidationSummary"
                                        ControlToValidate="txtTelephone" Display="None" ForeColor="Red" ErrorMessage="Telephone is invalid format or has invalid characters. It can contain only numeric space , & ."
                                        ValidationExpression="1?\s*\W?\s*([2-9][0-8][0-9])\s*\W?\s*([2-9][0-9]{2})\s*\W?\s*([0-9]{4})(\se?x?t?(\d*))?"></asp:RegularExpressionValidator> 
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 5px">
                                    <asp:Label ID="lblJobTitle" runat="server" Text="Job Title" CssClass="frmFields"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtJobTitle" runat="server" Height="20" MaxLength="30" Width="250"
                                        onchange="javascript:setpresenterField('J');" CssClass="textbox_EB"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 5px">
                                    <asp:Label ID="lblDepartment" runat="server" Text="Department" CssClass="frmFields"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDepartment" runat="server" Height="20" MaxLength="30" Width="250"
                                        CssClass="textbox_EB"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div class="right">
                            <font color="#A00000">*</font><i><small>Required field</small></i>
                        </div>
                        <script>
                            function setpresenterField(fldType) {
                                if (fldType == 'N') {
                                    var tpresenterName = document.getElementById('<%= txtPresenterName.ClientID %>');
                                    var fName = document.getElementById('<%= txtFName.ClientID %>');
                                    var lName = document.getElementById('<%= txtLName.ClientID %>');
                                    tpresenterName.value = fName.value + ' ' + lName.value;
                                }
                                else {
                                    var tjobTitle = document.getElementById('<%= txtJobTitle.ClientID %>');
                                    var tTitle = document.getElementById('<%= txtPresenterTitle.ClientID %>');
                                    tTitle.value = tjobTitle.value;
                                }
                            }
                        </script>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="dvpresenterinfo" runat="server" class="FormCont Steps" style="width: 700px; margin-bottom: 10px">
                        <table border="0" width="100%">
                            <tr>
                                <td align="left" width="65%" style="padding-bottom: 10px;">
                                    <asp:Label ID="lblPresenter" runat="server" Font-Bold="True" Text="My Presenter Information"
                                        CssClass="frmHeading"></asp:Label>
                                </td>
                                <td align="left" width="35%" style="padding-bottom: 10px;">
                                    <asp:Label ID="lblPresenterPhoto" runat="server" Font-Bold="True" Text="My Presenter Photo"
                                        CssClass="frmHeading"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div class="regOutline">
                            <div class="bioLeft bioLeft1">
                                <span class="fldLabel">
                                    <asp:Label ID="lblPreName" runat="server" Text="Presenter Name" CssClass="frmFields"></asp:Label></span>
                                <span class="fldInput">
                                    <asp:TextBox ID="txtPresenterName" runat="server" MaxLength="50" Height="20" Width="250px"
                                        Enabled="False" CssClass="textbox_EB" /></span>
                                <div class="Clr">
                                </div>
                                <span class="fldLabel">
                                    <asp:Label ID="lblTitle" runat="server" Text="Title" CssClass="frmFields"></asp:Label></span>
                                <span class="fldInput">
                                    <asp:TextBox ID="txtPresenterTitle" runat="server" MaxLength="50" Height="20" Width="200"
                                        Enabled="False" CssClass="textbox_EB" /></span>
                                <div class="Clr">
                                </div>
                                <span class="fldLabel">
                                    <asp:Label ID="lblOrgName" runat="server" Text="Organization" CssClass="frmFields"></asp:Label></span>
                                <span class="fldInput">
                                    <asp:TextBox ID="txtPreOrgName" runat="server" MaxLength="50" Height="20" Width="250px"
                                        CssClass="textbox_EB" /></span>
                            </div>
                            <div class="bioRight bioRight1">
                                <div id="dvProfileImg">
                                    <span>
                                        <asp:Label ID="lblPath" runat="server" Visible="false"></asp:Label>
                                        <img runat="server" id="imgprofileImg" alt="" src="#" />
                                        <asp:HiddenField ID="hProfileImgID" runat="server" Value="0" />
                                    </span><span style="vertical-align: top">
                                        <asp:ImageButton ImageUrl="~/images/icons/ico-delete-disable.png" Enabled="false"
                                            ID="ibtnDel" runat="server" onmouseover="this.src='/images/icons/ico-delete-hover.png'"
                                            onmouseout="this.src='/images/icons/ico-delete-active.png'" OnClick="ibtnDel_Click" /></span>
                                    <div class="Clr">
                                    </div>
                                    <telerik:RadAsyncUpload runat="server" ID="aupPhoto" AllowedFileExtensions=".png,.gif,.jpg"
                                        MaxFileInputsCount="1" CssClass="rAU3" OnFileUploaded="aupPhoto_FileUploaded"
                                        OnClientFileUploaded="fileUploaded" HttpHandlerUrl="~/handler/VideoUploadT1.ashx?ftyp=u&uid=1"
                                        Localization-Select="Upload">
                                    </telerik:RadAsyncUpload>
                                </div>
                            </div>
                            <div class="Clr">
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <div id="dvpresenterbio" runat="server" class="FormCont Steps" style="width: 700px; margin-bottom: 10px">
                        <table border="0" width="100%" align="center">
                            <tr>
                                <td align="left" style="padding-bottom: 10px;">
                                    <asp:Label ID="lblCap4" runat="server" Font-Bold="True" Text="My Presenter Bio" CssClass="frmHeading"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <div id="dvRTBio">
                                        <telerik:RadEditor runat="server" ID="redtBio" ToolbarMode="Default" Height="180px"
                                            ToolsFile="~/editor/BasicTools.xml" BorderStyle="None" CssClass="rteditor1" ToolsWidth="100%"
                                            EnableResize="False" EditModes="Design" Width="673px">
                                            <CssFiles>
                                                <telerik:EditorCssFile Value="~/Styles/RTEditor.css" />
                                            </CssFiles>
                                            <ContextMenus>
                                                <telerik:EditorContextMenu Enabled="false" />
                                                <telerik:EditorContextMenu TagName="IMG" Enabled="false" />
                                                <telerik:EditorContextMenu TagName="A" Enabled="false" />
                                                <telerik:EditorContextMenu TagName="TABLE" Enabled="false" />
                                            </ContextMenus>
                                        </telerik:RadEditor>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <table border="0">
                        <tr style="float:left;">
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="SubBtn" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
          <script type="text/javascript">
              function fileUploaded(sender, args) {
                  __doPostBack('', '');
              }
          </script>
        </telerik:RadCodeBlock>
        <!-- END: page content -->
    </div>
    <div class="BottBg">
    </div>
    <img src="../Images/blank.gif" height="10" />
    <div class="Clr">
    </div>
</div>
