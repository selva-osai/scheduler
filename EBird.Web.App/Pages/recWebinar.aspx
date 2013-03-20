<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/masterWithNav.Master"
    AutoEventWireup="true" CodeBehind="recWebinar.aspx.cs" Inherits="EBird.Web.App.Pages.recWebinar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Begin Main Container -->
    <div class="MainCont">
        <div class="Temp1">
            <div class="LPart">
                <div class="widgets">
                    <h2>
                        Record a Webinars</h2>
                    <ul>
                        <li><a href="#">Recorded Webinars</a></li>
                        <li><a href="#">Record a Webinars</a></li>
                    </ul>
                </div>
                <div class="widgets">
                    <h2>
                        View Page Demo</h2>
                    <div class="PageDemo">
                        <img src="../Images/PageDemo.jpg" alt="View Page Demo" />
                    </div>
                </div>
                <div class="LinkBtn">
                    <ul>
                        <li><a href="#">Beginner's Guide </a></li>
                        <li><a href="#">How-to Tutorials</a></li>
                        <li><a href="#">Best Practices </a></li>
                        <li><a href="#">FAQ and Support</a></li>
                    </ul>
                </div>
            </div>
            <div class="RPart">
                <div class="Bredcrumb">
                    <ul>
                        <li><a href="#">Record a Webinar </a>&gt;</li>
                        <li>Recorded Webinars </li>
                    </ul>
                    <div class="Clr">
                    </div>
                </div>
                <div class="TopBg">
                </div>
                <div class="widgets">
                    <div class="Steps">
                        <div class="FormCont">
                            <div class="SectionTitle">
                                Search Recorded Webinar</div>
                            <div class="Row">
                                <input name="input" type="text" class="SearchRecord" value="Search Recorded Webinar Title" />
                            </div>
                            <div class="Row">
                                <select class="DaysLarge" name="">
                                    <option>ALL</option>
                                    <option>Past 30 Days</option>
                                    <option>Past 60 Days</option>
                                    <option>Past 90 Days</option>
                                </select>
                            </div>
                            <div class="Row CheckBox">
                                <label>
                                    From</label>
                                <input name="" type="text" class="SmallText" />
                                <div class="NoiseTxt">
                                    To</div>
                                <input type="text" class="SmallText" name="">
                                <div class="Clr">
                                </div>
                            </div>
                            <input type="submit" value="Search" class="SubBtn">
                        </div>
                        <div class="Clr">
                        </div>
                    </div>
                    <div class="DataGrid">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvWeb" runat="server" BorderColor="#e5e5e5" CellPadding="3" 
                                        EmptyDataText="No record found..." AutoGenerateColumns="false" Width="100%" 
                                        PageSize="20" onrowcommand="gvWeb_RowCommand">
                                    <HeaderStyle BackColor="#e5e5e5" Height="23" />
                                    <RowStyle Height="23" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Recorded Webinar Title">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnTitle" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Title")%>'
                                                        CommandName="Title" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"WebcastID")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Recorded Date and Time" DataField="SessionDateAndTime" />
                                            <asp:BoundField HeaderText="Registered" DataField="Registrants" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Viewed" DataField="Registrants" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <div class="PageNavCont">
                            <ul class="pagination">
                                <li class="previous-off">«Previous</li>
                                <li class="active">1</li>
                                <li><a href="#">2</a></li>
                                <li><a href="#">3</a></li>
                                <li><a href="#">4</a></li>
                                <li><a href="#">5</a></li>
                                <li><a href="#">6</a></li>
                                <li><a href="#">7</a></li>
                                <li class="next"><a href="#">Next »</a></li>
                            </ul>
                            <div class="Clr">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="BottBg">
                </div>
                <div class="Clr">
                </div>
            </div>
            <div class="Clr">
            </div>
        </div>
        <div class="Clr">
        </div>
    </div>
    <!--End Main Container -->
    <div class="Clr">
    </div>
</asp:Content>
