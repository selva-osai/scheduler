<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/masterWithNav.Master"
    AutoEventWireup="true" CodeBehind="PresentationConsole.aspx.cs" Inherits="EBird.Web.App.Pages.PresentationConsole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /><br />
    <table width="100%" border="0">
        <tr>
            <td width="20%" valign="top" height="525">
                &nbsp;
                
                <asp:GridView ID="gvDoc" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="None" onrowcommand="gvDoc_RowCommand" BorderWidth="0" CellPadding="5" >
                <HeaderStyle Height="30" />
                <RowStyle Height="30" />
                    <Columns>
                        <asp:TemplateField HeaderText="Presentation Material(s)" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:LinkButton CommandName="Doc" CommandArgument='<%# Eval("Name") %>' Text='<%# Eval("Name") %>' runat="server"></asp:LinkButton>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            <td width="80%" valign="top">
                <table width="100%">
                <tr><td><asp:Label ID="lblFileName" runat="server"></asp:Label> </td></tr>
                <tr><td><asp:Panel BackColor="#666666" runat="server" Width="100%" Height="100%"></asp:Panel></td></tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
