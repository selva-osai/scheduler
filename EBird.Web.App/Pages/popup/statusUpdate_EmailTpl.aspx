<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master"
    AutoEventWireup="true" CodeBehind="statusUpdate_EmailTpl.aspx.cs" Inherits="EBird.Web.App.Pages.popup.statusUpdate_EmailTpl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<style>
        .container
        {
            width: 600px;
            margin-left: auto;
            margin-right: auto;
            border: 3px solid #c0c0c0;
            background-color: #fff;
            line-height:20px;
            border-radius: 5px;
            min-height: 200px;
        }
        div#content
        {
            margin: 10px 10px 10px 10px;
        }
    </style>--%>
    <asp:Literal ID="ltrStyle" runat="server"></asp:Literal>
    <div id="mn" class="container">
        <div id="content">
            <asp:Label ID="ltrContent" runat="server" />
        </div>
    </div>
</asp:Content>
