<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/PreviewLayout.Master"
    AutoEventWireup="true" CodeBehind="previewTheme.aspx.cs" Inherits="EBird.Web.App.Pages.popup.previewTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link id="lnkStyle" runat="server" href="/Styles/layoutpreview.css" rel="stylesheet"
        type="text/css" />
    <table width="100%" id="tbContainer" runat="server" style="background: #f5f5f5; height:600px" >
        <tr>
            <td align="center">
                 <img src="/images/layout/prelayout1.png" runat="server" id="imglayout" />
                 <asp:Label ID="lblID" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <%-- <asp:PlaceHolder ID="phLayout1" runat="server">
        <div class="Registration-Cont" style="background: #fff; border: solid 1px #ccc; border-radius: 6px;">
            <br />
            &nbsp;
            <div class="Regi-Top-Sec">
                <div class="L-Part Clr">
                    <table style="min-height: 450px;">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="Clr">
                </div>
            </div>
            <div class="FRight" style="width: 270px; margin-right: 12px;">
                <div class="Clr">
                </div>
                <br>
                &nbsp;
            </div>
        </div>
    </asp:PlaceHolder>--%>
</asp:Content>
