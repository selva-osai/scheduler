<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addWebinar.aspx.cs" Inherits="EBird.Web.App.Pages.popup.addWebinar" %>
<%@ Register Src="~/UserControls/mywebinarlist.ascx" TagName="webinarlist" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function RowMouseOver(sender, eventArgs) {
            $get(eventArgs.get_id()).className += " RowMouseOver";
        }

        function RowMouseOut(sender, eventArgs) {
            var row = $get(eventArgs.get_id());
            row.className = row.className.replace("RowMouseOver", "RowMouseOut");
        }
    </script>
</head>
<link id="Link1" href="/Styles/popUpWinStyle.css" rel="stylesheet" type="text/css" />
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc3:webinarlist id="webinarlist1" runat="server" />
    </form>
</body>
</html>
