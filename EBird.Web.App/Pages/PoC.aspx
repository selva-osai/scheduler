<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/masterFullScreen.Master"
    AutoEventWireup="true" CodeBehind="PoC.aspx.cs" Inherits="EBird.Web.App.Pages.PoC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Begin Main Container -->
    <div class="MainCont">
        <div class="Temp1">
            <div class="widgets">
                <div class="FormCont Steps">
                    <table width="95%" cellpadding="15" align="center">
                        <tr>
                            <td colspan="2" height="30">
                                <b>Title:</b> Audio / Vedio Capture & Streaming&nbsp;&nbsp;<asp:LinkButton ID="lbtnSessionStart"
                                    runat="server" Text="Start" onclick="lbtnSessionStart_Click"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td height="30">
                                <b>Presenter:</b> Colin May
                            </td>
                            <td>
                                <b>Date & Time:</b> May 5th, 2012 between 3 pm and 5 pm EST
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="30">
                                <b>Summary:</b><br />
                                &nbsp;
                                <p align="justify">
                                    Since the explosion of interest in the Internet in 1993, people have experimented
                                    with transmitting sound and video over the Net. As we saw in Chapter 7, for the
                                    most part, this was a disappointing experience, because of the time it takes to
                                    transfer an entire multimedia file over slow links. An audio file might take more
                                    real time to download than the length of the clip being played – that is, you might
                                    spend 10 or 30 minutes downloading an audio clip whose elapsed playback time might
                                    be only two minutes. Video, which carries much more information than audio, entailed
                                    even longer download times, just to experience a 1/8 screen, slow-frame-rate, blurry
                                    movie.</p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="30">
                                <br />
                                <b>Presentation documents:</b>&nbsp;&nbsp;<asp:LinkButton ID="lblUpload" runat="server"
                                    Text="Upload"></asp:LinkButton><br />
                                &nbsp;
                                <p>
                                </p>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;
                </div>
                <div class="FormCont">
                </div>
            </div>
        </div>
    </div>
    <div class="Clr">
    </div>
    <!--End Main Container -->
</asp:Content>
