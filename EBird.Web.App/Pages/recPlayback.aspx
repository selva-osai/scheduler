<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/masterFullScreen.Master"
    AutoEventWireup="true" CodeBehind="recPlayback.aspx.cs" Inherits="EBird.Web.App.Pages.recPlayback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr height="450">
            <td width="25%" valign="top" align="center" bgcolor="#f8f8f8">
                <br />
                <table width="95%">
                    <tr>
                        <td>
                            <b>Title</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Date & Time</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Presenter(s)</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPresenter" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Summary</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p align="justify">
                                <asp:Literal ID="ltrConfDetail" runat="server"></asp:Literal>
                            </p>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="75%" valign="middle" align="center">
                <asp:Literal ID="ltrVideo" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr height="100">
            <td colspan="2" bgcolor="#f8f8f8">
                <asp:Repeater ID="rpImg" runat="server" onitemcommand="rpImg_ItemCommand">
                    <HeaderTemplate>
                        <table width="100%">
                            <tr>
                                <td>
                                    <img src="../Images/leftScroll.png" />
                                </td>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <td valign="middle">
                            <asp:ImageButton ImageUrl="~/Images/grayImg.png" CommandArgument='<%# DataBinder.Eval (Container.DataItem, "WebcastID") %>'
                                runat="server" ID="ibtnImg1" />
                        </td>
                    </ItemTemplate>
                    <FooterTemplate>
                        <td>
                            <img src="../Images/rightScroll.png" />
                        </td>
                        </tr> </table>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
</asp:Content>
