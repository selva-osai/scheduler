<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master"
    AutoEventWireup="true" CodeBehind="PresenterContact.aspx.cs" Inherits="EBird.Web.App.Pages.popup.PresenterContact" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .lbl1
        {
            line-height: 20px;
            font-family: Verdana;
            font-size: 12px;
        }
        .lbl2
        {
            padding-bottom: 5px !important;
            padding-top: 5px !important;
            vertical-align: middle;
        }
    </style>
    <div class="regOutline">
        <table width="98%" align="center">
            <tr>
                <td colspan="2" style="padding-bottom: 5px;">
                    <asp:Label ID="lblpgCap2" runat="server" Font-Bold="True" Text="Webinar Presenter Details"
                        CssClass="frmHeading"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label CssClass="lbl1" Text="Dail-in Number: <b>888-555-1234</b> (United States), <b>503-345-2345</b> (Direct)<br>Dial-in Passcode: <b>558097</b>"
                        ID="lblContact" runat="server"></asp:Label>
                    <asp:HiddenField ID="hWebinarID" runat="server" Value="0" />
                    <textarea rows="3" ID="txtVal" STYLE="display:none;">Dail-in Number: 888-555-1234 (United States), 503-345-2345 (Direct) Dial-in Passcode: 558097</textarea>
                </td>
                <td valign="top" align="right">
                    <a href="javascript:copyToCB();">Copy to Clipboard</a>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox TextMode="MultiLine" ID="txtAttendeeEmails" CssClass="textbox_Email2" runat="server"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="txtWatermark" runat="server" TargetControlID="txtAttendeeEmails"
                        WatermarkText="Separate multiple addresses with semi-colon(;)" WatermarkCssClass="watermarked_EBReview textbox_Email2" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="chkIncCCURL" runat="server" />
                    &nbsp;<asp:Label CssClass="lbl1" Text="Include Command Center URL within Email" ID="chkIncCCURLLabel"
                        runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <table width="100%" align="center">
        <tr>
            <td colspan="2" height="24px">
                <asp:Button ID="btnEmail" OnClick="btnEmail_Click" runat="server" Text="Email" CssClass="SubBtn" />
                &nbsp;
                <asp:Button ID="btnClose" OnClick="btnEmail_Close" runat="server" Text="Close" CssClass="SubBtn" />&nbsp;&nbsp;<asp:Label
                    ID="lblError" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <!-- The following hidden variable is used for modal window closing flag -->
    <asp:HiddenField ID="hModalStatusFlg" runat="server" />
    <script type="text/javascript">
        function copyToCB() {
            var copyText = document.getElementById('txtVal').value;            
//            window.clipboardData.clearData();
//            window.clipboardData.setData('Text', copyText);
//            window.clipboardData.getData('Text');
//            //document.execCommand('copy', false, null);
              copyToClipboard(copyText);
        }

        function copyToClipboard(s){
	        if( window.clipboardData && clipboardData.setData )
	        {
                window.clipboardData.clearData();
                window.clipboardData.setData('Text', s);
                window.clipboardData.getData('Text');
                //document.execCommand('copy', false, null);
		        //clipboardData.setData("Text", s);
	        }
	        else
	        {
		        user_pref("signed.applets.codebase_principal_support", true);
		        netscape.security.PrivilegeManager.enablePrivilege('UniversalXPConnect');

		        var clip = Components.classes['@mozilla.org/widget/clipboard;[[[[1]]]]'].createInstance(Components.interfaces.nsIClipboard);
		        if (!clip) return;

		        // create a transferable
		        var trans = Components.classes['@mozilla.org/widget/transferable;[[[[1]]]]'].createInstance(Components.interfaces.nsITransferable);
		        if (!trans) return;

		        // specify the data we wish to handle. Plaintext in this case.
		        trans.addDataFlavor('text/unicode');

		        // To get the data from the transferable we need two new objects
		        var str = new Object();
		        var len = new Object();

		        var str = Components.classes["@mozilla.org/supports-string;[[[[1]]]]"].createInstance(Components.interfaces.nsISupportsString);
		        var copytext=meintext;
		        str.data=copytext;
		        trans.setTransferData("text/unicode",str,copytext.length*[[[[2]]]]);

		        var clipid=Components.interfaces.nsIClipboard;

		        if (!clip) return false;

		        clip.setData(trans,null,clipid.kGlobalClipboard);	   
	        }
        }
    </script>
</asp:Content>
