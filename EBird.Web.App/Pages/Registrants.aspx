<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/masterWithNav.Master" AutoEventWireup="true" CodeBehind="Registrants.aspx.cs" Inherits="EBird.Web.App.Pages.Registrants" %>

<%@ Register Src="~/UserControls/schLPart.ascx" TagName="schLPart" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/registrantList.ascx" TagName="registrantlist" TagPrefix="uc3" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="MainCont">
        <div class="Temp1">
            <uc1:schlpart id="schLPart1" runat="server" />
            <uc3:registrantlist id="registrantlist1" runat="server" />
            <div class="Clr">
            </div>
        </div>
        <div class="Clr">
        </div>
    </div>
</asp:Content>
