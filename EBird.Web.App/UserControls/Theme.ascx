<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Theme.ascx.cs" Inherits="EBird.Web.App.UserControls.Theme" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<div class="RPart">
    <div class="TopBg">
    </div>
    <div class="widgets">
        <asp:PlaceHolder ID="phClientList" runat="server" Visible="true">
            <!--<b><font color="#C80108" size="3">Client Management</font></b><br />&nbsp;-->
            <div class="FormCont Steps">
                <table border="0" width="100%" cellpadding="5" style="font-family: Verdana; padding: 5px 2px 5px 5px;">
                    <tr>
                        <td align="center">
                            <asp:TextBox ID="txtSearch" Columns="40" MaxLength="40" runat="server" Height="20"
                                Width="270" />
                            <asp:TextBoxWatermarkExtender ID="txtWatermark" runat="server" TargetControlID="txtSearch"
                                WatermarkText="Client Name" WatermarkCssClass="watermarked_EBSearch" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rcmbPkgType" runat="server" CssClass="cmbPkg">
                                <Items>
                                    <telerik:RadComboBoxItem Text="ALL" Value="All"></telerik:RadComboBoxItem>
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
                    <tr>
                        <td colspan="4" valign="middle" height="40">
                            From&nbsp;<telerik:RadDatePicker ID="dpFrom" Width="120px" runat="server" HideAnimation-Duration="800" ShowAnimation-Duration="800">>
                            </telerik:RadDatePicker>
                            To&nbsp;<telerik:RadDatePicker ID="dpTo" Width="120px" runat="server" HideAnimation-Duration="800" ShowAnimation-Duration="800">>
                            </telerik:RadDatePicker>
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
                            MasterTableView-NoDetailRecordsText="No activee clients available" BorderColor="#D7D7D7">
                            <PagerStyle Mode="NumericPages" PagerTextFormat="Navigate pages {4} Page {0} of {1}, Clients {2} to {3} of {5}" />
                            <ExportSettings HideStructureColumns="true" />
                            <MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" AllowFilteringByColumn="False"
                                ShowFooter="false" TableLayout="Auto" Width="100%">
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToWordButton="true"
                                    ShowRefreshButton="false" ShowExportToExcelButton="true" ShowExportToCsvButton="true"
                                    ShowExportToPdfButton="true" />
                                <NoRecordsTemplate>
                                    <center>
                                        <br />
                                        No record to display<br />
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
                                    <telerik:GridTemplateColumn HeaderText="Client Name" UniqueName="ClientName">
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
                                    <telerik:GridBoundColumn DataField="NoOfWebinars" HeaderText="Webinars">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Client As Of" UniqueName="ClientAsOf">
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
            <table width="97%" align="center">
                <tr>
                    <td align="right">
                        <asp:LinkButton ID="lbtnBack" runat="server" Text="Back to Theme Builder" OnClick="lbtnBack_Click" CssClass="lnkBtn1"></asp:LinkButton>
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
                                        <asp:HiddenField ID="hClientID" runat="server" />
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
                    <td colspan="2">
                        <div class="ui-widget" runat="server" id="dvAlert" visible="false">
                            <div class="ui-state-error ui-corner-all" style="padding: 0 .7em; height: 21px;">
                                <p>
                                    <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
                                    <strong>Alert:</strong>
                                    <asp:Label ID="lblInactiveMsg" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="Steps" runat="server" id="Div1" visible="true">
                            <ul>
                                <li id="li1" runat="server" class="One1 Current">
                                    <asp:LinkButton ID="lbtnAudiInter" runat="server" Text="Audience Interface" CausesValidation="false"></asp:LinkButton>
                                </li>
                                <li id="li2" runat="server" class="Two">
                                    <asp:LinkButton ID="lnkConfig" runat="server" Text="Registration Themes" CausesValidation="false"></asp:LinkButton>
                                </li>
                                <li id="li3" runat="server" class="Three">
                                    <asp:LinkButton ID="lnkSnapsite" runat="server" Text="SnapSite Themes" CausesValidation="false"></asp:LinkButton>
                                </li>
                                <li id="li4" runat="server" class="Four">
                                    <asp:LinkButton ID="lnkInvite" runat="server" Text="Invitation Themes" CausesValidation="false"></asp:LinkButton>
                                </li>
                            </ul>
                            <div class="Clr">
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    <table width="100%" style="min-height:300px">
                    <tr><td>&nbsp;</td></tr>
                    </table>
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
    </div>
    <div class="BottBg">
    </div>
    <div class="Clr">
    </div>
</div>
