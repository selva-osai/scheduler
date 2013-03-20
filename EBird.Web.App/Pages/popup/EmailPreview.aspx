<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailPreview.aspx.cs" Inherits="EBird.Web.App.Pages.popup.EmailPreview" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <asp:Literal ID="ltrStyle" runat="server"></asp:Literal>
    <title></title>
</head>
<body>
<script type="text/javascript" src="/js/myscroll.js"></script>
    <link id="css_scroll" href="/css/scrollbar.css" rel="stylesheet" />
    <form id="form1" runat="server">
    <%--    <div id="mn" class="my-container">
        <asp:Literal ID="ltrHeader" runat="server"></asp:Literal>
        <div id="mc" class="my-content">--%>
        
    <div style="padding: 15px 10px 5px 80px">
        <asp:Label ID="ltrSubject" runat="server" />
    </div>
    
    <asp:Label ID="ltrContent" runat="server" />
  
    <%-- <asp:Label ID="lblSys" runat="server" />
        </div>
    </div>
    <asp:Literal ID="ltrFooter" runat="server"></asp:Literal>--%>
    </form>
</body>
</html>
