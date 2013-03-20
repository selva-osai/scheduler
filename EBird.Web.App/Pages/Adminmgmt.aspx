<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/masterWithNav.Master" AutoEventWireup="true" CodeBehind="Adminmgmt.aspx.cs" Inherits="EBird.Web.App.Pages.Adminmgmt" %>
<%@ Register Src="~/UserControls/schLPart.ascx" TagName="schLPart" TagPrefix="uc1" %>
<%@ Register src="~/UserControls/userAdmin.ascx" tagname="adminprofile" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="MainCont">
        <div class="Temp1">
            <uc1:schlpart id="schLPart1" runat="server" />
             <uc2:adminprofile ID="adminprofile1" runat="server" />
              <div class="Clr">
            </div>
        </div>

        <div class="Clr">
        </div>
    </div>

</asp:Content>
