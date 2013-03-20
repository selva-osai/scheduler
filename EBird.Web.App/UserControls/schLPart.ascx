<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="schLPart.ascx.cs" Inherits="EBird.Web.App.UserControls.schLPart" %>
<div class="LPart">
    <div class="widgets">
        <%--        <asp:Literal ID="ltrLeftPanNav" runat="server"></asp:Literal>--%>
        <%--<h2>
            Webinars</h2>
        <ul>
            <li><a runat="server" href="~/Pages/webinarlist.aspx">Webinars</a></li>
            <li><a runat="server" href="~/Pages/schedule.aspx">Schedule a Webinar </a></li>
        </ul>--%>
        <h2><asp:Label ID="lblLWCap" runat="server" Text="Administration" /></h2>
        <ul runat="server" id="ulnav1">
            <li runat="server" id="lia1"><a runat="server" id="A1" visible="false" href="~/Pages/Webinar">My Webinars</a></li>
            <li runat="server" id="lia2"><a runat="server" id="A2" visible="false" href="~/Pages/Schedule">Schedule a Webinar</a></li>
            <li runat="server" id="lia3"><a runat="server" id="A3" visible="false" href="~/Pages/Analytics">My Analytics</a></li>
            <li runat="server" id="lia4"><a runat="server" id="A4" class="LeftNavLastItem" visible="false" href="~/Pages/SnapSite">My SnapSite</a>
              <span id="sp1" runat="server" class="spPremium" visible="false">My SnapSite <span class="spSS" id="sp2">[p]</span></span></li>
            <li runat="server" id="s1"><a runat="server" id="Set1" visible="false">My Profile</a></li>
            <li runat="server" id="s2"><a runat="server" id="Set2" visible="false">Account Setting</a></li>
            <li runat="server" id="s3"><a runat="server" id="Set3" visible="false">User Management</a></li>
            <li runat="server" id="linav1"><a runat="server" id="ssl1" visible="false">Client Information</a></li>
            <li runat="server" id="linav2"><a runat="server" id="ssl2" visible="false">Client Configuration</a></li>
            <li runat="server" id="linav4"><a runat="server" id="ssl4" visible="false">Subscription</a></li>
            <%--<li runat="server" id="linav3"><a runat="server" ID="ssl3" visible="false">Theme Builder</a></li>--%>
             <li runat="server" id="linav7"><a runat="server" id="ssl7" visible="false">Audit</a></li>
            <li runat="server" id="linav6"><a runat="server" id="ssl6" visible="false">Admin Management</a></li>
           <li runat="server" id="linav5"><a runat="server" id="ssl5" visible="false">Email Contents</a></li>
        </ul>
    </div>
    <div class="widgets">
        <h2>
            View Page Demo</h2>
        <div class="PageDemo">
            <img runat="server" id="pgDemo" src="~/Images/PageDemo.jpg" alt="View Page Demo" />
        </div>
    </div>
    <div class="LinkBtn">
        <ul>
            <%--<li><a href="#">Beginner's Guide </a></li>--%>
            <li><a href="#">How-to Tutorials</a></li>
            <%--<li><a href="#">Best Practices </a></li>--%>
            <li><a href="#">FAQ and Support</a></li>
        </ul>
    </div>
</div>
