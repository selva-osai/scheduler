<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master"
    AutoEventWireup="true" CodeBehind="addAttribute.aspx.cs" Inherits="EBird.Web.App.Pages.popup.addAttribute" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .ruUploadProgress, li .ruCancel, li .ruRemove
        {
            visibility: hidden;
        }
        li.ruUploading
        {
            display: none;
        }
        .setHt
        {
            min-height: 60px;
        }
    </style>
    <asp:HiddenField ID="hWebinarID" Value="0" runat="server" />
    <asp:HiddenField ID="hModalStatusFlg" Value="0" runat="server" />
    <asp:HiddenField ID="hDocId" Value="0" runat="server" />
    
    <div class="regOutline setHt" runat="server" id="dvOutline">
        <div style="margin-top: 5px; margin-bottom: 10px;">
            <asp:Label ID="Label1" runat="server" Text="Add/Edit Attribute"
                Font-Bold="True"></asp:Label>
        </div>
        <table>
            <tr>
                <td align="left" width="30%">
                    <asp:TextBox ID="txtURLName" runat="server" CssClass="textbox_EB"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="txtWMURLname" runat="server" TargetControlID="txtURLName"
                        WatermarkText="URL Name" WatermarkCssClass="watermarked_EBSearch textbox_EB" />
                </td>
                <td align="left" width="70%">
                    <asp:TextBox ID="txtURL" runat="server" CssClass="textbox_EB textbox_EB2"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="txtWMURLink" runat="server" TargetControlID="txtURL"
                        WatermarkText="URL Link" WatermarkCssClass="watermarked_EBSearch textbox_EB textbox_EB2" />
                   
                </td>
            </tr>
        </table>
    </div>
    <div style="margin-top: 5px; margin-bottom: 5px">
        <asp:Button ID="btnSave" runat="server" CssClass="SubBtn" Text="Save" OnClick="btnSave_Click" />&nbsp;
        <asp:Button ID="btnCancel" runat="server" CssClass="SubBtn" Text="Cancel" OnClick="btnCancel_Click" />&nbsp;
        <asp:Label ID="lblURLError" runat="server" ForeColor="Red"></asp:Label>
        <asp:RegularExpressionValidator ID="rgExpURL" runat="server" ControlToValidate="txtURL"
            ErrorMessage="Invalid URL" ForeColor="Red" ValidationExpression="(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?"></asp:RegularExpressionValidator>
    </div>
</asp:Content>
