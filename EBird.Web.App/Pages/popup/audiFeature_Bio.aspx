<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master"
    AutoEventWireup="true" CodeBehind="audiFeature_Bio.aspx.cs" Inherits="EBird.Web.App.Pages.popup.audiFeature_Bio" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        tr.rgRow > td span input, tr.rgAltRow > td span input
        {
            margin-top: 11px;
        }
    </style>
    <script type="text/javascript" src="../../js/myscroll.js"></script>
    <link id="Link1" href="../../css/scrollbar.css" rel="stylesheet" />
    <link rel="stylesheet" href="/styles/newstyle1.css" />
    <asp:HiddenField ID="hWebinarID" Value="0" runat="server" />
    <div class="regOutline setHt" runat="server" id="dvOutline">
        <div style="margin-top: 5px; margin-bottom: 10px;">
            <asp:Label ID="Label1" runat="server" Text="Configure Audience Components - Briefcase"
                Font-Bold="True"></asp:Label>
        </div>
        <asp:PlaceHolder ID="phConfig" runat="server" Visible="true">
            <telerik:RadTabStrip runat="server" ID="rtabBio" MultiPageID="rmpgBio" SelectedIndex="0"
                CausesValidation="false" ReorderTabsOnSelect="True">
                <Tabs>
                    <telerik:RadTab Text="Add / Edit Presenter" Selected="True" />
                    <telerik:RadTab Text="List of Presenter(s)" />
                </Tabs>
            </telerik:RadTabStrip>
            <!-- test -->
            <telerik:RadMultiPage runat="server" ID="rmpgBio" SelectedIndex="0" Height="320px"
                Width="637px">
                <telerik:RadPageView runat="server" ID="RadPageView1">
                    <div class="regOutline">
                        <div class="bioLeft">
                            <span class="fldLabel">
                                <asp:Label ID="lblPreName" runat="server" Text="Presenter Name" CssClass="frmFields"></asp:Label>
                            </span><span class='EBmsg'>&nbsp;*</span><span class="fldInput">
                                <telerik:RadComboBox ID="rcmbPresenter" runat="server" CssClass="cmbPkg" AutoPostBack="True"
                                    CausesValidation="false" AllowCustomText="true" MarkFirstMatch="true" OnSelectedIndexChanged="rcmbPresenter_SelectedIndexChanged">
                                </telerik:RadComboBox>
                                <asp:TextBox ID="txtPresenterName" runat="server" MaxLength="50" Height="20" Width="200px"
                                    Visible="false" CssClass="textbox_EB" />
                                <asp:RequiredFieldValidator ID="vx_txtPresenterName" runat="server" CssClass="ValidationSummary"
                                    ControlToValidate="rcmbPresenter" ErrorMessage="Please enter required field - Presenter Name"
                                    ForeColor="Red" Display="None" Text="*" />
                                <asp:HiddenField ID="hPresenterID" runat="server" Value="0" />
                            </span>
                            <div class="Clr">
                            </div>
                            <span class="fldLabel">
                                <asp:Label ID="lblTitle" runat="server" Text="Title" CssClass="frmFields"></asp:Label></span><span
                                    class='EBmsg'>&nbsp;*</span> <span class="fldInput">
                                        <asp:TextBox ID="txtPresenterTitle" runat="server" MaxLength="50" Height="20" Width="200px"
                                            CssClass="textbox_EB" />
                                        <asp:RequiredFieldValidator ID="vx_txtpresenterTitle" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtPresenterTitle" ErrorMessage="Please enter required field - Presenter Title"
                                            ForeColor="Red" Display="None" Text="*" />
                                    </span>
                            <div class="Clr">
                            </div>
                            <span class="fldLabel">
                                <asp:Label ID="lblOrgName" runat="server" Text="Organization" CssClass="frmFields"></asp:Label></span><span
                                    class='EBmsg'>&nbsp;*</span> <span class="fldInput">
                                        <asp:TextBox ID="txtPreOrgName" runat="server" MaxLength="50" Height="20" Width="200px"
                                            CssClass="textbox_EB" />
                                        <asp:RequiredFieldValidator ID="vx_txtPreOrgName" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtPreOrgName" ErrorMessage="Please enter required field - Organization Name"
                                            ForeColor="Red" Display="None" Text="*" />
                                    </span>
                        </div>
                        <div class="bioRight">
                            <div id="dvProfileImg" runat="server" visible="true">
                                <span>
                                    <asp:Label ID="lblPath" runat="server" Visible="false"></asp:Label>
                                    <img runat="server" id="imgprofileImg" alt="" src="#" />
                                    <asp:HiddenField ID="hProfileImgID" runat="server" Value="0" />
                                </span><span style="vertical-align: top">
                                    <asp:ImageButton ImageUrl="~/images/icons/ico-delete-disable.png" Enabled="true"
                                        ID="ibtnDel" runat="server" onmouseover="this.src='/images/icons/ico-delete-hover.png'"
                                        onmouseout="this.src='/images/icons/ico-delete-active.png'" OnClick="ibtnDel_Click" /></span>
                                <div class="Clr">
                                </div>
                                <div style="margin-right: 10px; margin-left: 180px; float: left;">
                                    <telerik:RadAsyncUpload runat="server" ID="aupPhoto" AllowedFileExtensions=".png,.gif,.jpg"
                                        Localization-Select="Upload" CssClass="rAU4" OnFileUploaded="aupPhoto_FileUploaded"
                                        OnClientFileUploaded="fileUploaded" OnClientValidationFailed="validationFailed"
                                        HttpHandlerUrl="~/handler/VideoUploadT1.ashx" Visible="true">
                                    </telerik:RadAsyncUpload>
                                    <asp:LinkButton ID="lnkUploadProfile" runat="server" Text="" OnClick="lnkUploadProfile_Click"
                                        CausesValidation="false"></asp:LinkButton>
                                </div>
                                <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                    <script type="text/javascript">
                                        function fileUploaded(sender, args) {
                                            $telerik.$(".invalid").html("");
                                            setTimeout(function () { sender.deleteFileInputAt(0); }, 10);
                                            __doPostBack('ctl00$ContentPlaceHolder1$lnkUploadProfile', '')
                                        }
                                        function validationFailed(sender, args) {
                                            $telerik.$(".invalid").html("Invalid extension, please choose an image file").get(0).style.display = "block";
                                            sender.deleteFileInputAt(0);
                                        }

                                        function onClientFileUploading(sender, args) {
                                            var obj = { ftyp: 'u', second: 2 };
                                            args.set_queryStringParams(obj);
                                        }
                                    </script>
                                </telerik:RadCodeBlock>
                            </div>
                        </div>
                        <div class="Clr">
                        </div>
                        <div id="dvRTBio">
                            <telerik:RadEditor runat="server" ID="redtBio" ToolbarMode="Default" Height="165px"
                                ToolsWidth="100%" ToolsFile="~/editor/BasicTools1.xml" BorderStyle="None" CssClass="rteditor1"
                                Width="607px" EnableResize="False" EditModes="Design">
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
                    </div>
                    <table width="100%" align="center">
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" runat="server" CssClass="SubBtn" Text="Save" OnClick="btnSave_Click" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" CssClass="SubBtn" Text="Cancel" OnClick="btnCancel_Click"
                                    CausesValidation="false" />&nbsp;&nbsp;
                                <asp:Label ID="lblError" ForeColor="Red" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="RadPageView2">
                    <div class="regOutline">
                        <div id="dvpresenter" class='scroll'>
                            <telerik:RadGrid ID="tgrdPresenterList" runat="server" GridLines="None" AllowPaging="false"
                                MasterTableView-EditFormSettings-EditFormType="Template" AutoGenerateColumns="False"
                                ValidationSettings-EnableValidation="false" OnItemCommand="tgrdPresenterList_ItemCommand"
                                OnItemDataBound="tgrdPresenterList_ItemDataBound" MasterTableView-NoDetailRecordsText="No Presenter associated to this webinars"
                                BorderColor="#D7D7D7">
                                <MasterTableView CommandItemDisplay="None" AutoGenerateColumns="false" AllowFilteringByColumn="False"
                                    ShowFooter="false" TableLayout="Auto" Width="100%">
                                    <NoRecordsTemplate>
                                        <center>
                                    <br />
                                    No Presenter associated to this webinars<br />
                                    &nbsp;</center>
                                    </NoRecordsTemplate>
                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px" />
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Disabled" UniqueName="Inactive" HeaderStyle-Width="20px">
                                            <ItemStyle Height="50px" VerticalAlign="Top" HorizontalAlign="Center" />
                                            <HeaderStyle Height="50px" VerticalAlign="Top" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkDisable" runat="server" AutoPostBack="True" OnCheckedChanged="UpdateEnabledStatus" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Presenter Detail" UniqueName="Detail" HeaderStyle-Width="280px"
                                            AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDetail" CommandName="View" Text='' runat="server" CommandArgument='<%# Eval("presenterID") %>'
                                                    CausesValidation="false"></asp:LinkButton>
                                                <asp:Label runat="server" ID="lblDetail" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="390px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Image ID="imgPresenterImg" runat="server" ImageUrl="/Images/brian-bio-125px.png"
                                                    alt="" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="presenterID" Visible="false" />
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Scrolling AllowScroll="false" />
                                    <Selecting AllowRowSelect="true" />
                                    <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phDisableFeature" runat="server" Visible="false">
            <asp:Label ID="lblDisableFeature" runat="server" Font-Size="11px"></asp:Label>
        </asp:PlaceHolder>
    </div>
    <asp:HiddenField ID="hModalStatusFlg" runat="server" />
</asp:Content>
