<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/masterWithNav.Master" AutoEventWireup="true" CodeBehind="Audit.aspx.cs" Inherits="EBird.Web.App.Pages.Audit" %>
<%@ Register Src="~/UserControls/schLPart.ascx" TagName="schLPart" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/audit.ascx" TagName="audit" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="MainCont">
        <div class="Temp1">
            <uc1:schlpart id="schLPart1" runat="server" />
            <uc3:audit id="Audit1" runat="server" />
            <div class="Clr">
            </div>
        </div>
        <div class="Clr">
        </div>
    </div>

</asp:Content>
