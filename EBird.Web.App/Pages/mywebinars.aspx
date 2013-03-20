<%@ Page Title="My Webinars" Language="C#" MasterPageFile="~/MasterPages/masterWithNav.Master" AutoEventWireup="true"  EnableEventValidation="false"  
 CodeBehind="mywebinars.aspx.cs" Inherits="EBird.Web.App.Pages.mywebinars" %>

<%@ Register Src="~/UserControls/schLPart.ascx" TagName="schLPart" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/mywebinarlist.ascx" TagName="webinarlist" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="MainCont">
        <div class="Temp1">
            <uc1:schlpart id="schLPart1" runat="server" />
            <uc3:webinarlist id="webinarlist1" runat="server" />
            <div class="Clr">
            </div>
        </div>
        <div class="Clr">
        </div>
    </div>
</asp:Content>
