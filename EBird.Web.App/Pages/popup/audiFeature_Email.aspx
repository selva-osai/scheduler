<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master"
    AutoEventWireup="true" CodeBehind="audiFeature_Email.aspx.cs" ValidateRequest="false" 
    Inherits="EBird.Web.App.Pages.popup.audiFeature_Email" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:HiddenField ID="hWebinarID" Value="0" runat="server" />
    <table width="98%" align="center">
        <tr>
            <td>
                <asp:Label ID="lblConfigContent" runat="server" Font-Bold="True" Text="Configure Audience Components - Email a Friend"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <div class="regOutline">
                    <img src="/Images/blank.gif" height="10" />
                    <asp:PlaceHolder ID="phConfig" runat="server" Visible="true">
                    <table width="95%" align="center" style="padding: 5px 5px 5px 5px;">
                        <tr>
                            <td align="left" colspan="2" height="24">
                                <asp:Label ID="lblEmail" runat="server" Text="Email a Friend"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="12%" valign="top" align="left">
                                <asp:Label ID="lblTo" runat="server" Text="To"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtTo" runat="server" CssClass="textbox_EB textbox_EB4"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="left">
                                From
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFrom" runat="server" CssClass="textbox_EB textbox_EB4"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="left">
                                Subject
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSubject" Text="Check out this webinar" runat="server" CssClass="textbox_EB textbox_EB4"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="left">
                                Body
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Columns="55" Rows="15"
                                    CssClass="textarea_EB"></asp:TextBox>
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
        <tr>
            <td>
                <img src="/Images/blank.gif" height="5" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="SubBtn" Text="Save" OnClick="btnSave_Click" />&nbsp;
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
