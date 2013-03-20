<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/masterWithNav.Master" AutoEventWireup="true" CodeBehind="SnapSite.aspx.cs" Inherits="EBird.Web.App.Pages.SnapSite" %>

<%@ Register src="../UserControls/ssadmin.ascx" tagname="ssadmin" tagprefix="uc1" %>
<%@ Register Src="~/UserControls/schLPart.ascx" TagName="schLPart" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
     <div class="MainCont">
        <div class="Temp1">
            <uc2:schlpart id="schLPart1" runat="server" />
            <uc1:ssadmin ID="ssadmin1" runat="server" />
            <div class="Clr">
            </div>
        </div>
        <div class="Clr">
        </div>
    </div>
</asp:Content>
