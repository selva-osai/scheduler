<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="registrantList.ascx.cs"
    Inherits="EBird.Web.App.UserControls.registrantList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script type="text/javascript">
    function OnClientLoad(editor, args) {
        var style = editor.get_contentArea().style;
        style.color = "black";
    }
</script>
<div class="RPart">
    <div class="TopBg">
    </div>
    <div class="widgets">
        <table width="97%" align="center" runat="server" id="tbHeader">
            <tr>
                <td align="right" style="padding: 0px 0px 10px 0px;">
                    Return to
                    <asp:LinkButton ID="lbtnBack" runat="server" Text="My Webinar" CssClass="lnkBtn1"
                        OnClick="lbtnBack_Click" CausesValidation="false"></asp:LinkButton>
                    to manage or start your webinars
                </td>
            </tr>
            <tr>
                <td>
                    <div >
                        <table border="0" style="padding: 10px 5px 10px 2px; width: 100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblWebinarTitle" runat="server" CssClass="frmHeading"></asp:Label>
                                    <asp:HiddenField ID="hWebinarID" runat="server" />
                                    <asp:HiddenField ID="hFilterType" runat="server" />
                                </td>
                                <td align="right">
                                    <asp:Literal ID="ltrStatus" runat="server" Visible="false" />
                                    <asp:Label ID="lblTime" runat="server" CssClass="frmHeading"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <asp:MultiView ID="mvRegistrants" runat="server">
            <asp:View ID="vwInvalid" runat="server">
                <table width="97%" align="center">
                    <tr>
                        <td>
                            <div class="FormCont Steps">
                                <table border="0" width="100%" cellpadding="5" style="font-family: Verdana; padding: 5px 2px 5px 5px;">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblInactivemsg" runat="server" Text="Invalid webinar or insufficient previledge"
                                                ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vwRegistrants" runat="server">
                <table width="97%" align="center">
                    <tr>
                        <td>
                            &nbsp;<asp:Label ID="lblRegistrantList" runat="server" Font-Bold="True" Text="List of registered registrants" CssClass="frmHeading" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" width="100%">
                                <tr>
                                    <td>
                                        <img src="../Images/blank.gif" height="2" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="tgrdRegistrantList" runat="server" GridLines="None" AllowPaging="True"
                                            Width="100%" PageSize="15" MasterTableView-EditFormSettings-EditFormType="Template"
                                            AllowSorting="True" OnItemDataBound="tgrdRegistrantList_ItemDataBound" AutoGenerateColumns="False" OnItemCommand="tgrdRegistrantList_ItemCommand"
                                            ValidationSettings-EnableValidation="false" MasterTableView-NoDetailRecordsText="No registrants for this webinar so far" BorderColor="#D7D7D7">
                                            <PagerStyle Mode="NumericPages" PagerTextFormat="Navigate pages {4} Page {0} of {1}, Registrants {2} to {3} of {5}" />
                                            <ExportSettings HideStructureColumns="true" />
                                            <MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" AllowFilteringByColumn="False"
                                                ShowFooter="false" TableLayout="Auto" Width="100%">
                                                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToWordButton="true"
                                                    ShowRefreshButton="true" ShowExportToExcelButton="true" ShowExportToCsvButton="true"
                                                    ShowExportToPdfButton="true" />
                                                <NoRecordsTemplate>
                                                    <center>
                                                        <br />
                                                        No registrant to display<br />
                                                        &nbsp;</center>
                                                </NoRecordsTemplate>
                                                <RowIndicatorColumn>
                                                    <HeaderStyle Width="20px" />
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn>
                                                    <HeaderStyle Width="20px" />
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="FullName" HeaderText="Registrant">
                                                        <ItemStyle Width="260" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="EmailAddress" HeaderText="Email Address">
                                                        <ItemStyle Width="260" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="RegisteredOn" Visible="false" HeaderText="Registration Date and Time" />
                                                    <telerik:GridTemplateColumn HeaderText="Registration Date and Time" UniqueName="RegistrationDate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDateTime" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="180" />
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings>
                                                <Scrolling AllowScroll="false" />
                                                <Selecting AllowRowSelect="true" />
                                                <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img src="../Images/blank.gif" height="5" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </div>
    <div class="BottBg">
    </div>
    <img src="../Images/blank.gif" height="10" />
    <div class="Clr">
    </div>
</div>
