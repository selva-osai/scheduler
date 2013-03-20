<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/masterWithNav.Master" AutoEventWireup="true" CodeBehind="analytics.aspx.cs" Inherits="EBird.Web.App.Pages.analytics" %>
<%@ Register src="../UserControls/analytics.ascx" tagname="analytics" tagprefix="uc1" %>
<%@ Register Src="~/UserControls/schLPart.ascx" TagName="schLPart" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="MainCont">
        <div class="Temp1">
            <uc2:schlpart id="schLPart1" runat="server" />
            <uc1:analytics ID="analytics1" runat="server" />
            <div class="Clr">
            </div>
        </div>
        <div class="Clr">
        </div>
    </div>
</asp:Content>
