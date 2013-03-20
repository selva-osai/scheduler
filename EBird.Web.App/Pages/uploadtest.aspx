<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploadtest.aspx.cs" Inherits="EBird.Web.App.Pages.uploadtest" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
    //<![CDATA[
        function fileUploaded(sender, args) {
            var name = args.get_fileName();
            var $ = $telerik.$;

            $(".info-panel").
                append($("<div>" + name + "</div>")).show();
            $(".upload-panel").hide();
        }
        
    //]]>
    </script>
    <div class="upload-panel">
        <telerik:RadProgressManager runat="server" ID="RadProgressManager1" />
        <telerik:RadAsyncUpload runat="server" ID="AsyncUpload1" OnClientFileUploaded="fileUploaded"  
            MultipleFileSelection="Automatic">
        </telerik:RadAsyncUpload>
        <telerik:RadProgressArea runat="server" ID="RadProgressArea1">
        </telerik:RadProgressArea>
    </div>
    <div class="info-panel">
        Uploaded files in the target folder:
    </div>
    </form>
</body>
</html>
