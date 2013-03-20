<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master"
    AutoEventWireup="true" CodeBehind="audiFeature_Content.aspx.cs" Inherits="EBird.Web.App.Pages.popup.audiFeature_Content" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .ruUploadProgress, li .ruCancel, li .ruRemove
        {
            visibility: hidden;
        }
        li.ruUploading
        {
            display: none;
        }
        .setHt
        {
            min-height:300px;
        }
    </style>
    <asp:HiddenField ID="hWebinarID" Value="0" runat="server" />
    <div class="regOutline setHt" runat="server" id="dvOutline">
        <div style="margin-top: 5px; margin-bottom: 10px;">
            <asp:Label ID="Label1" runat="server" Text="Configure Audience Components - Briefcase"
                Font-Bold="True"></asp:Label>
        </div>
        <asp:PlaceHolder ID="phConfig" runat="server" Visible="true">
            <table width="100%">
                <tr>
                    <td align="left" colspan="2">
                        <div style="float: left; margin-right: 1px; line-height: 22px;">
                            <asp:Label ID="lblContentFiles" runat="server" Text="Briefcase Files" Font-Size="11px"></asp:Label>
                            &nbsp;
                            <telerik:RadAsyncUpload runat="server" ID="aupPresentation" AllowedFileExtensions="ppt,pptx,doc,docx,pdf,xls,xlsx,xps,txt,csv"
                                Localization-Select="Upload" InputSize="30" OnFileUploaded="aupContent_FileUploaded"
                                OnClientFileUploaded="fileUploaded" OnClientValidationFailed="validationFailed"
                                HttpHandlerUrl="~/handler/VideoUploadT1.ashx?ftyp=c&u=1">
                            </telerik:RadAsyncUpload>
                            <asp:LinkButton ID="lnkUploadContent" runat="server" Text="" OnClick="lnkUploadContent_Click"
                                CausesValidation="false"></asp:LinkButton>
                        </div>
                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                            <script type="text/javascript">
                                function fileUploaded(sender, args) {
                                    $telerik.$(".invalid").html("");
                                    setTimeout(function () { sender.deleteFileInputAt(0); }, 10);
                                    __doPostBack('ctl00$ContentPlaceHolder1$lnkUploadContent', '')
                                }
                                function validationFailed(sender, args) {
                                    $telerik.$(".invalid").html("Invalid extension, please choose an image file").get(0).style.display = "block";
                                    sender.deleteFileInputAt(0);
                                } 
                            </script>
                        </telerik:RadCodeBlock>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:Label ID="lblContentURL" runat="server" Text="Briefcase URLs" Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="30%">
                        <asp:TextBox ID="txtURLName" runat="server" CssClass="textbox_EB"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="txtWMURLname" runat="server" TargetControlID="txtURLName"
                            WatermarkText="URL Name" WatermarkCssClass="watermarked_EBSearch textbox_EB" />
                    </td>
                    <td align="left" width="70%">
                        <asp:TextBox ID="txtURL" runat="server" CssClass="textbox_EB textbox_EB2"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="txtWMURLink" runat="server" TargetControlID="txtURL"
                            WatermarkText="URL Link" WatermarkCssClass="watermarked_EBSearch textbox_EB textbox_EB2" />
                        &nbsp;<asp:Button ID="btnAddURL" runat="server" CssClass="SubBtn" Text="Add URL"
                            OnClick="btnAddURL_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadGrid ID="tgrdResourceList" runat="server" GridLines="None" MasterTableView-EditFormSettings-EditFormType="Template"
                            AutoGenerateColumns="False" ValidationSettings-EnableValidation="false" OnItemCommand="tgrdResourceList_ItemCommand"
                            OnItemDataBound="tgrdResourceList_ItemDataBound">
                            <ExportSettings HideStructureColumns="true" />
                            <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="False" ShowHeader="true"
                                CommandItemDisplay="None" ShowFooter="false" TableLayout="Auto" Width="100%">
                                <NoRecordsTemplate>
                                    <center>
                                        <br />
                                        No files or URLs to display<br />
                                        &nbsp;</center>
                                </NoRecordsTemplate>
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ResourceID" HeaderText="ResourceID" Visible="false" />
                                    <telerik:GridBoundColumn DataField="ResourceType" HeaderText="Briefcase Type">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ResourceTitle" HeaderText="Title" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ResourceValue" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="ResourceLink" HeaderText="Title">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="urlLink" CssClass="lnkBtn1" Target="_blank" runat="server" Visible="false"></asp:HyperLink>
                                            <asp:Label ID="lblNoLink" Text="-" runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="DeleteColumn">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("ResourceID") %>' OnClientClick="return confirm('Are you sure you want to delete this presentation?');"
                                                CommandName="delResource" ImageUrl="~/Images/icons/trash.png" ToolTip="Delete" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings>
                                <Scrolling AllowScroll="false" />
                                <Selecting AllowRowSelect="true" />
                                <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phDisableFeature" runat="server" Visible="false">
            <asp:Label ID="lblDisableFeature" runat="server" Font-Size="11px"></asp:Label>
        </asp:PlaceHolder>
    </div>
    <div style="margin-top: 5px; margin-bottom: 5px">
        <asp:Button ID="btnSave" runat="server" CssClass="SubBtn" Text="Save" OnClick="btnSave_Click" />&nbsp;
        <asp:Button ID="btnCancel" runat="server" CssClass="SubBtn" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;
        <asp:Label ID="lblURLError" runat="server" ForeColor="Red"></asp:Label>
        <asp:RegularExpressionValidator ID="rgExpURL" runat="server" ControlToValidate="txtURL" ErrorMessage="Invalid URL" ForeColor="Red" 
            ValidationExpression="(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?"></asp:RegularExpressionValidator>
    </div>
    <asp:HiddenField ID="hModalStatusFlg" runat="server" />
    <%--<telerik:RadCodeBlock ID="rcRad" runat="server">
        <script type="text/javascript">

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow; //For mozilla
                if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE  
                return oWindow;
            }

            function CloseAndReload() {
                var oWnd = GetRadWindow();
                //oWnd.BrowserWindow.location.reload(); add this line if you want to refresh the parent page      
                oWnd.close();
            }
 
        </script>
    </telerik:RadCodeBlock>--%>
</asp:Content>
