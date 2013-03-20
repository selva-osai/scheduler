<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master"
    AutoEventWireup="true" CodeBehind="ConfirmWebinarStatus.aspx.cs" Inherits="EBird.Web.App.Pages.popup.ConfirmWebinarStatus" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .modalClose
        {
            position: absolute;
            float: right;
            top: -3px;
            right: -1px;
            z-index: 6000;
        }
    </style>
    <asp:Label ID="hWebinarID" runat="server" Visible="false"></asp:Label>
    <div class="regOutline">
        <table width="100%" align="center" style="padding: 5px 5px 5px 5px;">
            <tr>
                <td>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="margin: 2px 0px 3px 2px;">
        <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="SubBtn" OnClick="btnYes_Click" />&nbsp;
        <asp:Button ID="btnNo" OnClick="btnNo_Click" runat="server" Text="No" CssClass="SubBtn" />
    </div>
    <!-- The following hodden variable is used for modal window closing flag -->
    <asp:HiddenField ID="hModalStatusFlg" runat="server" />
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
