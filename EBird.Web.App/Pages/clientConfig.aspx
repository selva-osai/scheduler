<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/masterWithNav.Master" AutoEventWireup="true" CodeBehind="clientConfig.aspx.cs" Inherits="EBird.Web.App.Pages.clientConfig" %>

<%@ Register src="~/UserControls/schLPart.ascx" tagname="schLPart" tagprefix="uc1" %>
<%@ Register src="~/UserControls/clientConfigList.ascx" tagname="clientList" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Begin Main Container -->
    <div class="MainCont">
        <div class="Temp1">
        <uc1:schLPart ID="schLPart1" runat="server" />
             <uc2:clientList ID="clientList1" runat="server" />
            <div class="Clr">
            </div>
        </div>
        <div class="Clr">
        </div>
    </div>
    <!--End Main Container -->
</asp:Content>

