<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="audit.ascx.cs" Inherits="EBird.Web.App.UserControls.audit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<div class="RPart">
    <div class="TopBg">
    </div>
    <div class="widgets">
        <table width="97%" align="center" style="min-height: 475px;">
            <tr>
                <td valign="top">
                    <telerik:RadGrid ID="tgrdauditList" runat="server" GridLines="None" AllowPaging="True"
                        AutoGenerateColumns="false" PageSize="15" MasterTableView-EditFormSettings-EditFormType="Template"
                        AllowSorting="True" BorderColor="#D7D7D7" OnItemDataBound="tgrdauditList_ItemDataBound" OnItemCommand="tgrdauditList_ItemCommand">
                        <PagerStyle Mode="NumericPages" PagerTextFormat="Navigate pages {4} Page {0} of {1}, Audit actions {2} to {3} of {5}" />
                        <ExportSettings HideStructureColumns="true" />
                        <MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" AllowFilteringByColumn="False"
                            ShowFooter="false" TableLayout="Auto" Width="100%">
                            <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToWordButton="true"
                                ShowRefreshButton="true" ShowExportToExcelButton="true" ShowExportToCsvButton="true"
                                ShowExportToPdfButton="true" />
                            <NoRecordsTemplate>
                                <center><br />No audit record available<br />&nbsp;</center>
                            </NoRecordsTemplate>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="ActionBy" HeaderText="Administrator" UniqueName="ActionBy" SortExpression="ActionBy">
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Date and Time" UniqueName="ActionDate" SortExpression="ActionDate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActionOn" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="200" Wrap="false" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ActionDetail" HeaderText="Action">
                                    <ItemStyle HorizontalAlign="Left" Wrap="true" />
                                </telerik:GridBoundColumn>
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
    </div>
    <div class="BottBg">
    </div>
    <img src="../Images/blank.gif" height="10" />
    <div class="Clr">
    </div>
</div>
