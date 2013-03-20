<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="subscription.ascx.cs"
    Inherits="EBird.Web.App.UserControls.subscription" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
.RadComboBoxDropDown_Default .rcbHovered {
background:#EBEBEB;
color:#C80000;
}

.RadCalendar_Default .rcMain .rcRow .rcHover,.RadCalendar_Default .rcRow .rcSelected {
background:#EBEBEB 0 -1700px repeat-x url(Calendar/sprite.gif);
border-color:#EBEBEB;
}

.RadCalendar_Default .rcMain .rcRow .rcHover a,.RadCalendar_Default .rcMain .rcRow .rcSelected a {
color:#C80000;
}

.RadCalendarMonthView_Default .rcSelected a,.RadCalendarTimeView_Default td.rcHover a,.RadCalendarTimeView_Default td.rcSelected a {
background:#EBEBEB 0 -1700px repeat-x url(Calendar/sprite.gif);
color:#C80000;
border-color:#EBEBEB;
}
</style>
<div class="RPart">
    <div class="TopBg">
    </div>
    <div class="widgets">
        <asp:PlaceHolder ID="phClientList" runat="server" Visible="true">
            <!--<b><font color="#C80108" size="3">Client Management</font></b><br />&nbsp;-->
            <div class="Steps1">
                <table border="0" width="100%">
                    <tr>
                        <td align="center">
                            <asp:TextBox ID="txtSearch" Columns="40" MaxLength="40" runat="server" Height="20"
                                Width="270" />
                            <asp:TextBoxWatermarkExtender ID="txtWatermark" runat="server" TargetControlID="txtSearch"
                                WatermarkText="Client Name" WatermarkCssClass="watermarked_EBSearch" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rcmbPkgType" runat="server" CssClass="cmbPkg" MarkFirstMatch="True"  HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                   CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                   EnableTheming="True" 
                                   Font-Italic="False" Skin="Default">
                                <Items>
                                    <telerik:RadComboBoxItem Text="ALL" Value="All" Selected="True"></telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem Text="Enterprise" Value="Enterprise"></telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem Text="Professional" Value="Professional"></telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem Text="Custom" Value="Custom"></telerik:RadComboBoxItem>
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" CssClass="SubBtn" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                        <td align="right" width="50%">
                            &nbsp;
                            <!--  &nbsp;<asp:HyperLink runat="server" ID="AdvancedSearch" Text="Advanced Search" NavigateUrl="~/AdministrationSearch.aspx" ForeColor="#1589FF"></asp:HyperLink> -->
                        </td>
                    </tr>
                </table>
            </div>
            <div class="Steps1">
                <table border="0" width="100%">
                    <tr>
                        <td colspan="2" valign="middle" height="21">
                            From&nbsp;<telerik:RadDatePicker ID="dpFrom" Width="120px" runat="server" AutoPostBack="true"
                                OnSelectedDateChanged="dpFrom_SelectedDateChanged" HideAnimation-Duration="800" ShowAnimation-Duration="800">
                            </telerik:RadDatePicker>
                            To&nbsp;<telerik:RadDatePicker ID="dpTo" Width="120px" runat="server" AutoPostBack="true"
                                OnSelectedDateChanged="dpTo_SelectedDateChanged" HideAnimation-Duration="800" ShowAnimation-Duration="800">
                            </telerik:RadDatePicker>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblFilterError" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <table width="100%" align="center" cellpadding="10" style="min-height: 350px;">
                <tr>
                    <td valign="top">
                        <telerik:RadGrid ID="tgrdClientList" runat="server" GridLines="None" AllowPaging="True"
                            PageSize="15" MasterTableView-EditFormSettings-EditFormType="Template" AllowSorting="True"
                            AutoGenerateColumns="False" OnItemCommand="tgrdClientList_ItemCommand" OnItemDataBound="tgrdClientList_ItemDataBound"
                            MasterTableView-NoDetailRecordsText="No active clients available" BorderColor="#D7D7D7">
                            <PagerStyle Mode="NumericPages" PagerTextFormat="Navigate pages {4} Page {0} of {1}, Clients {2} to {3} of {5}" />
                            <ExportSettings HideStructureColumns="true" />
                            <MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" AllowFilteringByColumn="False"
                                ShowFooter="false" TableLayout="Auto" Width="100%">
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToWordButton="true"
                                    ShowRefreshButton="true" ShowExportToExcelButton="true" ShowExportToCsvButton="true"
                                    ShowExportToPdfButton="true" />
                                <NoRecordsTemplate>
                                    <center>
                                        <br />
                                        No active clients available<br />
                                        &nbsp;</center>
                                </NoRecordsTemplate>
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="clientID" HeaderText="Client ID">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Client Name" UniqueName="ClientName" SortExpression="ClientName"
                                        DataField="ClientName">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkView1" CssClass="lnkBtn1" CommandName="View" Text='<%# Eval("ClientName") %>'
                                                runat="server" CommandArgument='<%# Eval("clientID") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="CurrentPkgSubscribed" HeaderText="Platform Edition">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NoOfUsers" HeaderText="Seats">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NoOfWebinars" HeaderText="Webinars" UniqueName="Webinars" SortExpression="NoOfWebinars">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Client As Of" UniqueName="ClientAsOf" SortExpression="CreatedOn">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreatedOn" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="CreatedOn" HeaderText="Client As Of" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Visible="false" DataField="clientStatus">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="">
                                        <ItemTemplate>
                                            <img src="~/Images/icons/icoAnalytics.gif" alt="Analytics" runat="server" id="imgAnal" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
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
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phClientInfo" runat="server" Visible="false">
            <table width="100%" align="center">
                <tr>
                    <td align="right" style="padding-bottom: 10px;">
                        <asp:Label ID="lbtnBackLabel" runat="server" Font-Bold="False" Text="Return to " ></asp:Label>
                        <asp:LinkButton ID="lbtnBack" runat="server" Text="Subscription" OnClick="lbtnBack_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="FormCont Steps">
                            <table border="0" width="100%">
                                <tr>
                                    <td>
                                        <b>Client Name</b><br />
                                        <br />
                                        <asp:Label ID="lblClient" runat="server"></asp:Label>
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </td>
                                    <td>
                                        <b>Package subscribed</b><br />
                                        <br />
                                        <asp:Label ID="lblPkg" runat="server" Visible="true"></asp:Label>
                                    </td>
                                    <td>
                                        <b>Status</b><br />
                                        <br />
                                        &nbsp;&nbsp;<asp:Image ImageUrl="~/Images/icons/ActiveStatus1.png" runat="server"
                                            ID="imgStatus" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" valign="top">
                        <telerik:RadGrid ID="tgrdPkgHistory" runat="server" GridLines="None" AllowPaging="True"
                            MasterTableView-EditFormSettings-EditFormType="Template" AllowSorting="True" PageSize="15"
                            AutoGenerateColumns="False" MasterTableView-NoDetailRecordsText="No subscription information available"
                            Width="100%" BorderColor="#D7D7D7">
                            <PagerStyle Mode="NumericPages" PagerTextFormat="Navigate pages {4} Page {0} of {1}, Subscription Changes {2} to {3} of {5}" />
                            <MasterTableView AutoGenerateColumns="False" HorizontalAlign="NotSet">
                                <NoRecordsTemplate>
                                    <center>
                                        <br />
                                        No subscription information available<br />
                                        &nbsp;</center>
                                </NoRecordsTemplate>
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ActionDate" HeaderText="Subscribed Date" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ActionDetail" HeaderText="Package Name">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <asp:HiddenField ID="hClientID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="min-height: 300px">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
    </div>
    <div class="BottBg">
    </div>
    <img src="../Images/blank.gif" height="10" />
    <div class="Clr">
    </div>
</div>
