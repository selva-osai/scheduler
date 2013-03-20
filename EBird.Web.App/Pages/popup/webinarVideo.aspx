<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webinarVideo.aspx.cs" Inherits="EBird.Web.App.Pages.popup.webinarVideo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Webinar - Video Upload</title>
    <style>
        .regOutline
        {
            background-color: #ececec;
            border-radius: 5px;
        }
        .textbox_EB
        {
            border: solid 1px #9e9e9e;
            padding-left: 3px;
            font-size: 11px;
            font-family: verdana;
            border-radius: 3px;
            margin-bottom: 6px;
            height: 20px;
            width: 215px;
        }
        
        .SubBtn, a.SubBtn
        {
            background: url(../Images/LinkBtnBg.gif) repeat-x left top;
            height: 22px;
            line-height: 18px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            padding: 0 10px;
            border: solid 1px #d9dcdc;
            font-size: 11px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="regOutline">
        <table width="98%" align="center">
            <tr>
                <td width="100%">
                    Video Upload
                </td>
            </tr>
            <tr>
                <td>
                    <div id="dvUploadCntrl" runat="server">
                        <telerik:RadUpload ID="rupLoad" runat="server" CssClass="textbox_EB">
                        </telerik:RadUpload>
                        <asp:FileUpload ID="fupProfile" runat="server" Height="21px" Width="227px" BackColor="#ffffff"
                            CssClass="textbox_EB" />
                        <asp:Button ID="btnUpload" runat="server" Text="Upload Video" CssClass="SubBtn" OnClientClick="return fileSizeCheck();"
                            CausesValidation="False" OnClick="btnUpload_Click" />&nbsp;
                        <asp:Button ID="btnUpCancel" runat="server" Text="Cancel" Visible="false" CssClass="SubBtn" />
                    </div>
                    <script language="javascript" type="text/javascript">
                        function fileSizeCheck() {
                            //file = document.getElementById('<%=fupProfile.ClientID %>');
                            //alert('Height: ' + file.height + '\n' + 'File size: ' + file.size + '\n' + 'File extension: ' + file.type);
                            //return false;
                            return true;
                        }
                    </script>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <video runat="server" id="regVideo" width="320" height="240" poster="~/images/icons/blankVideoTN.png">
                        <source runat="server" id="vsrc" src='/handler/VideoHandler.ashx?id=0' type="video/mp4" />
                    </video>
                    <asp:Image ID="ImgNoVideo" runat="server" Visible="false" ImageUrl="~/images/icons/blankVideoTN.png" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:HiddenField ID="hWebinarID" runat="server" />
                    <asp:HiddenField ID="hDocID" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <table width="98%" align="center">
        <tr>
            <td height="28">
                <asp:Button ID="btnOK" runat="server" CssClass="SubBtn" Text="Submit" OnClientClick="popSubmit('S');" />&nbsp;
                <asp:Button ID="btnCancel" runat="server" CssClass="SubBtn" Text="Cancel" OnClientClick="popSubmit('C');" />
                <telerik:RadCodeBlock ID="rcVideo" runat="server">
                    <script type="text/javascript">
                        function popSubmit(typ) {
                            if (typ == 'S') {
                                self.close();
                            }
                            else {
                                self.close();
                            }
                        }
                    </script>
                </telerik:RadCodeBlock>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
