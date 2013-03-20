<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master" AutoEventWireup="true" CodeBehind="audiFeature_Video.aspx.cs" Inherits="EBird.Web.App.Pages.popup.audiFeature_Video" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /><asp:HiddenField ID="hWebinarID" Value="0" runat="server" />
    <table width="98%" align="center">
        <tr>
            <td>
                <asp:Label ID="lblConfigContent" runat="server" Font-Bold="True" Text="Build Audience Interface – Configure Components (Content)"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <div class="regOutline">
                    <img src="/Images/blank.gif" height="10" />
                    <asp:PlaceHolder ID="phConfig" runat="server" Visible="true">
                    <table width="95%" align="center" style="padding: 5px 5px 5px 5px;">
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Label ID="lblVideoFiles" runat="server" Text="Configure Audience Components – Upload Video"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadProgressManager runat="server" ID="RadProgressManager1" />
                                <div class="qsf-demo-canvas">
                                    <telerik:RadAsyncUpload runat="server" ID="aupVideo" UploadedFilesRendering="BelowFileInput" OnFileUploaded="aupVideo_FileUploaded" 
                                        HttpHandlerUrl="~/handler/VideoUploadT1.ashx" MultipleFileSelection="Automatic" />
                                    <telerik:RadProgressArea runat="server" ID="RadProgressArea1" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="tgrdResourceList" runat="server" GridLines="None" MasterTableView-EditFormSettings-EditFormType="Template"
                                    AutoGenerateColumns="False" ValidationSettings-EnableValidation="false">
                                    <ExportSettings HideStructureColumns="true" />
                                    <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="False" ShowHeader="true"
                                        CommandItemDisplay="None" ShowFooter="false" TableLayout="Auto" Width="100%">
                                        <NoRecordsTemplate>
                                            <center>
                                        <br />
                                        No record to display<br />
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
                                            <telerik:GridBoundColumn DataField="ResourceType" HeaderText="Content Type">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ResourceTitle" HeaderText="Title">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn UniqueName="DeleteColumn">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("ResourceID") %>'
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
                        <p align="center">
                            <br />
                            <br />
                            <asp:Label ID="lblDisableFeature" runat="server" Font-Size="11px"></asp:Label>
                            <br />
                            <br />
                            &nbsp;
                        </p>
                    </asp:PlaceHolder>
                    <img src="/Images/blank.gif" height="10" />
                </div>
            </td>
        </tr>
        <tr><td><img src="/Images/blank.gif" height="5" /></td></tr>
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="SubBtn" Text="Save" 
                    onclick="btnSave_Click" />&nbsp;
                <asp:Button ID="btnClear" runat="server" CssClass="SubBtn" Text="Clear config" />&nbsp;
                <asp:Button ID="btnCancel" runat="server" CssClass="SubBtn" Text="Cancel" 
                    onclick="btnCancel_Click" />
            </td>
        </tr>
    </table>
        <telerik:RadCodeBlock ID="rcRad" runat="server">
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
    </telerik:RadCodeBlock>
</asp:Content>
