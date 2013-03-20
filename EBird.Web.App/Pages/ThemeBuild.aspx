<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/masterWithNav.Master" AutoEventWireup="true" CodeBehind="ThemeBuild.aspx.cs" Inherits="EBird.Web.App.Pages.ThemeBuild" %>
<%@ Register Src="~/UserControls/schLPart.ascx" TagName="schLPart" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Theme.ascx" TagName="Theme" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="MainCont">
        <div class="Temp1">
            <uc1:schlpart id="schLPart1" runat="server" />
            <uc3:Theme id="Theme1" runat="server" />
            <div class="Clr">
            </div>
        </div>
        <div class="Clr">
        </div>
    </div>
</asp:Content>
