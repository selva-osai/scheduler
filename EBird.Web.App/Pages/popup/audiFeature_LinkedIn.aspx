<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master"
    AutoEventWireup="true" CodeBehind="audiFeature_LinkedIn.aspx.cs" Inherits="EBird.Web.App.Pages.popup.audiFeature_LinkedIn" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hWebinarID" Value="0" runat="server" />
    <div class="regOutline">
        <div style="margin-top: 5px; margin-bottom: 10px;">
            <asp:Label ID="lblConfigContent" runat="server" Text="Configure Audience Components – LinkedIn" Font-Bold="True"></asp:Label>
        </div>
        <asp:PlaceHolder ID="phConfig" runat="server" Visible="true">
            <table width="98%" align="center" class="genTable1">
                <tr>
                    <td colspan="2" align="left" style="padding-bottom: 8px; padding-top: 8px;">
                        <asp:Label ID="lblSubCap2" runat="server" Font-Bold="True" Text="Message Control Parameters" Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <%-- <tr>
                            <td colspan="2" align="left">
                                <asp:Label ID="Label1" runat="server" Text="Turn on/off the ability to manage a network update message"></asp:Label>
                            </td>
                        </tr>--%>
                <tr>
                    <td align="left" style="padding-left: 5px; font-size: 11px;">
                        Set Like/Unlike:
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkLike" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px; font-size: 11px;">
                        Allow Comments:
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkComments" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" style="padding-bottom: 8px; padding-top: 8px;">
                        <asp:Label ID="lblSubCap3" runat="server" Font-Bold="True" Text="Menu Navigation Parameters" Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <%--<tr>
                            <td colspan="2" align="left">
                                <asp:Label ID="Label2" runat="server" Text="Set which navigational options may be used"></asp:Label>
                            </td>
                        </tr>--%>
                <tr>
                    <td align="left" style="padding-left: 5px; font-size: 11px;">
                        Search:
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkSearch" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px; font-size: 11px;">
                        Filter Network Update Types:
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkNetwork" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" style="padding-bottom: 8px; padding-top: 8px;">
                        <asp:Label ID="lblSubCap4" runat="server" Font-Bold="True" Text="General Parameters" Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px; font-size: 11px;">
                        Check for Updates (minutes):
                    </td>
                    <td align="left" style="padding: 0px 0px 0px 3px">
                        <asp:TextBox ID="txtMinUpdates" CssClass="textbox_EB textbox_EB3" Text="2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px; font-size: 11px;">
                        Return Record Count:
                    </td>
                    <td align="left" style="padding: 0px 0px 0px 3px">
                        <asp:TextBox ID="txtMsgCount" CssClass="textbox_EB textbox_EB3" Text="10" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px; font-size: 11px;">
                        Default Invitation Subject:
                    </td>
                    <td align="left" style="padding: 0px 0px 0px 3px">
                        <asp:TextBox ID="txtInviteSub" CssClass="textbox_EB" runat="server" Text="Invitation to Connect"  />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px; font-size: 11px;">
                        Default Invitation Message:
                    </td>
                    <td align="left" style="padding: 0px 0px 0px 3px">
                        <asp:TextBox ID="txtInviteMsg" CssClass="textbox_EB" runat="server" Text="Please join my professional network on LinkedIn" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" style="padding-bottom: 8px; padding-top: 8px;">
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Permissions Parameters" Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <%--<tr>
                            <td colspan="2" align="left">
                                <asp:Label ID="Label2" runat="server" Text="Set which navigational options may be used"></asp:Label>
                            </td>
                        </tr>--%>
                <tr>
                    <td align="left" style="padding-left: 5px; font-size: 11px;">
                        Allow Network Updates (Posts):
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkAllowNetwork" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px; font-size: 11px;">
                        Allow Invitations:
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkAllowInvitation" runat="server" />
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