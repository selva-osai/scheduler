<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/masterWithNav.Master" AutoEventWireup="true" EnableEventValidation="false" 
  CodeBehind="clientInfo.aspx.cs" Inherits="EBird.Web.App.Pages.admin.clientInfo" %>

<%@ Register src="~/UserControls/schLPart.ascx" tagname="schLPart" tagprefix="uc1" %>
<%@ Register src="~/UserControls/clientList.ascx" tagname="clientList" tagprefix="uc2" %>

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
