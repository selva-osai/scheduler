<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master"
    AutoEventWireup="true" CodeBehind="audiFeature_Search.aspx.cs" Inherits="EBird.Web.App.Pages.popup.audiFeature_Search" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hWebinarID" Value="0" runat="server" />
    <div class="regOutline" style="padding-bottom:5px !important">
        <div style="margin-top: 7px; margin-bottom: 10px;">
            <asp:Label ID="lblConfigContent" runat="server" Text="Configure Audience Components - Search"
                Font-Bold="True"></asp:Label>
        </div>
        <asp:PlaceHolder ID="phConfig" runat="server" Visible="true">
            <table width="100%" align="center" style="padding: 0px 5px 5px 0px;" border="0" id="searchtbl">
                <tr>
                    <td colspan="2" align="left" style="padding-bottom: 8px; padding-top: 8px;">
                        <asp:Label ID="lblSearch" runat="server" Text="Select Desired Search Engine" Font-Size="11px"
                            Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="30" align="center">
                        <img src="/Images/icons/SearchY.png" alt="yahoo Search" />
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkSearchY" runat="server" Font-Size="11px" CssClass="chkGen chkAlign1"
                            Text="&nbsp;&nbsp;Yahoo" />
                    </td>
                </tr>
                <tr>
                    <td width="30" align="center">
                        <img src="/Images/icons/SearchB.png" alt="Bing Search" />
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkSearchB" runat="server" Font-Size="11px" CssClass="chkGen chkAlign1"
                            Text="&nbsp;&nbsp;Bing" />
                    </td>
                </tr>
                <tr>
                    <td width="30" align="center">
                        <img src="/Images/icons/SearchG.png" alt="Google Search" />
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkSearchG" runat="server" Font-Size="11px" CssClass="chkGen chkAlign1"
                            Text="&nbsp;&nbsp;Google" />
                    </td>
                </tr>
                <tr><td colspan="2"><asp:Label ID="lblInstruction" runat="server" Text="If no search option is selected than the feature will not be enabled" Visible="false"></asp:Label></td></tr>
            </table>
            <script type="text/javascript">
                jQuery(function ($) {
                    $("#ContentPlaceHolder1_chkSearchY").live('click', function (e) {
                        if ($('#ContentPlaceHolder1_chkSearchY').is(':checked')) {
                            $('#ContentPlaceHolder1_chkSearchB').removeAttr('checked');
                            $('#ContentPlaceHolder1_chkSearchG').removeAttr('checked');
                        }
                    });
                    $("#ContentPlaceHolder1_chkSearchB").live('click', function (e) {
                        if ($('#ContentPlaceHolder1_chkSearchB').is(':checked')) {
                            $('#ContentPlaceHolder1_chkSearchY').removeAttr('checked');
                            $('#ContentPlaceHolder1_chkSearchG').removeAttr('checked');
                        }
                    });
                    $("#ContentPlaceHolder1_chkSearchG").live('click', function (e) {
                        if ($('#ContentPlaceHolder1_chkSearchG').is(':checked')) {
                            $('#ContentPlaceHolder1_chkSearchB').removeAttr('checked');
                            $('#ContentPlaceHolder1_chkSearchY').removeAttr('checked');
                        }
                    });
                });
            </script>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phDisableFeature" runat="server" Visible="false">
            <div>
                <asp:Label ID="lblDisableFeature" runat="server" Font-Size="11px"></asp:Label>
            </div>
        </asp:PlaceHolder>
    </div>
    <div style="margin-top: 5px">
        <asp:Button ID="btnSave" runat="server" CssClass="SubBtn" Text="Save" OnClick="btnSave_Click" />&nbsp;
        <asp:Button ID="btnCancel" runat="server" CssClass="SubBtn" Text="Cancel" OnClick="btnCancel_Click" />
    </div>
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
    <asp:HiddenField ID="hModalStatusFlg" runat="server" />
</asp:Content>
