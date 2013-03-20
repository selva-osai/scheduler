<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master"
    AutoEventWireup="true" CodeBehind="DeleteWebinar.aspx.cs" Inherits="EBird.Web.App.Pages.popup.DeleteWebinar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:PlaceHolder ID="phEnable" runat="server" Visible="true">
        <div class="regOutline">
            <div id="Div3" style="margin: 5px 3px 0px 5px; line-height: 25px;">
                <asp:HiddenField ID="hWebinarID" runat="server" />
                <asp:Literal ID="ltrStatus" runat="server" Visible="false"></asp:Literal>
                <asp:HiddenField ID="hStartDate" runat="server" />
                <asp:HiddenField ID="hStartTime" runat="server" />
                <asp:HiddenField ID="hEndTime" runat="server" />
                <asp:Label ID="lblDelInstruction" Text="This Webinar will be cancelled and a notification will be sent to all participants."
                    runat="server"></asp:Label>
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <asp:HyperLink ID="hlnkDelPreviewEmail" runat="server" Text="Preview notification message"
            Font-Underline="True" CssClass="hyperlink_EB" Visible="false"></asp:HyperLink>
        <div id="Div2" style="margin: 3px 5px 0px 5px;">
            <asp:Button ID="btnDelWebinar" OnClick="btnDelWebinar_Click" runat="server" Text="Cancel Webinar"
                CssClass="SubBtn" />&nbsp;
            <asp:Button ID="btnDelCancel" CssClass="SubBtn" runat="server" Visible="true" Text="Do Not Cancel"
                OnClick="btnDelCancel_Click" CausesValidation="False" CommandName="Cancel" />&nbsp;
                
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phDelete" runat="server" Visible="false">
        <div class="regOutline">
            <div id="dvWebDelConfirm" style="margin: 5px 5px 2px 5px; line-height: 28px;">
                <asp:Label ID="lblWebDelConfirm" runat="server" Text="This is a hard delete and is unrecoverable, all data will be lost. Do you want to continue?"></asp:Label>
            </div>
        </div>
        <div id="Div1" style="margin: 3px 5px 0px 5px;">
            <asp:Button ID="btnWebDelConfirm" runat="server" CssClass="SubBtn" Text="Confirm delete"
                OnClick="btnWebDelConfirm_Click" />&nbsp;
            <asp:Button ID="btnCancel" CssClass="SubBtn" runat="server" Visible="true" Text="Do Not Cancel"
                OnClick="btnDelCancel_Click" CausesValidation="False" CommandName="Cancel" />
        </div>
    </asp:PlaceHolder>
    <asp:HiddenField ID="hModalStatusFlg" runat="server" />
</asp:Content>
