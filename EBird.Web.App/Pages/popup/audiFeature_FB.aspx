<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master"
    AutoEventWireup="true" CodeBehind="audiFeature_FB.aspx.cs" Inherits="EBird.Web.App.Pages.popup.audiFeature_FB" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hWebinarID" Value="0" runat="server" />
    <div class="regOutline">
        <div style="margin-top: 5px; margin-bottom: 10px;">
            <asp:Label ID="lblConfigContent" runat="server" Text="Configure Audience Components - Facebook" Font-Bold="True"></asp:Label>
        </div>
        <asp:PlaceHolder ID="phConfig" runat="server" Visible="true">
            <style>
                .FBRow
                {
                    height: 22px;
                }
            </style>
            <table width="98%" align="center" class="genTable1">
                <tr>
                    <td colspan="2" align="left" class="FBRow">
                        <asp:Label ID="lblSubCap1" runat="server" Font-Bold="True" Text="Status Update" Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="47%" align="left" class="FBRow" style="padding-left: 5px; font-size: 11px;">
                        Allow Status Updates:
                    </td>
                    <td width="53%" align="left" class="FBRow">
                        <asp:CheckBox ID="chkStatusUpdate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="FBRow" style="padding-left: 5px; font-size: 11px;">
                        Default Status Update:
                    </td>
                    <td align="left" class="FBRow" style="padding: 0px 0px 0px 3px">
                        <asp:TextBox ID="txtDefaultStatus" runat="server" CssClass="textbox_EB" Text="Snap! Check out this webinar - #WEBCASTURL#" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" class="FBRow">
                        <asp:Label ID="lblSubCap2" runat="server" Font-Bold="True" Text="Message Control Parameters" Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="FBRow" style="padding-left: 5px; font-size: 11px;">
                        Allow Like/Unlike Feature:
                    </td>
                    <td align="left" class="FBRow">
                        <asp:CheckBox ID="chkLike" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="FBRow" style="padding-left: 5px; font-size: 11px;">
                        Allow Comments:
                    </td>
                    <td align="left" class="FBRow">
                        <asp:CheckBox ID="chkComments" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" class="FBRow">
                        <asp:Label ID="lblSubCap3" runat="server" Font-Bold="True" Text="Menu Navigation Parameters" Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="FBRow" style="padding-left: 5px; font-size: 11px;">
                        Friends:
                    </td>
                    <td align="left" class="FBRow">
                        <asp:CheckBox ID="chkFriends" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="FBRow" style="padding-left: 5px; font-size: 11px;">
                        Search:
                    </td>
                    <td align="left" class="FBRow">
                        <asp:CheckBox ID="chkSearch" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" class="FBRow">
                        <asp:Label ID="lblSubCap4" runat="server" Font-Bold="True" Text="General Parameters" Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="FBRow" style="padding-left: 5px; font-size: 11px;">
                        Check for Updates (minutes):
                    </td>
                    <td align="left" class="FBRow" style="padding: 0px 0px 0px 3px">
                        <asp:TextBox ID="txtMinUpdates" CssClass="textbox_EB textbox_EB3" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="FBRow" style="padding-left: 5px; font-size: 11px;">
                        Number of Messages Returned:
                    </td>
                    <td align="left" class="FBRow" style="padding: 0px 0px 0px 3px">
                        <asp:TextBox ID="txtMsgCount" CssClass="textbox_EB textbox_EB3" runat="server" />
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
        <asp:Button ID="btnCancel" runat="server" CssClass="SubBtn" Text="Cancel" OnClick="btnCancel_Click" />
    </div>
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
