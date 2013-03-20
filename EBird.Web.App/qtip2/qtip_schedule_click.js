$(document).ready(function () {

    $("#ContentPlaceHolder1_schMeeting1_webRegistration1_chkEnableReg").live('click', function (e) {
        var isChk = document.getElementById('<%=hInitStatus.ClientID %>').value
        var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
        var URL = "/Pages/popup/ConfirmWebinarStatus.aspx?ID=" + hWebID.value + "&s=" + isChk;

        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webRegistration1_chkEnableReg",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='popFrame4' src=" + URL + " height='125' width='350' scrolling='no' class='bgfill'></iframe></div>",
                    350, ".modalClose", "", "#popFrame4", "body #ContentPlaceHolder1_hModalStatusFlg");
    });

    // Advance registration feature
    $("#ContentPlaceHolder1_schMeeting1_webRegistration1_chkAPI").live('click', function (e) {
        if ($('#ContentPlaceHolder1_schMeeting1_webRegistration1_chkAPI').is(':checked')) {
            var hAPIReg = document.getElementById('<%=hAPIReg.ClientID %>');
            document.getElementById('<%=txtAPIReg.ClientID %>').value = hAPIReg.value;
        }
        else {
            document.getElementById('<%=txtAPIReg.ClientID %>').value = "";
        }
    });

    $("#ContentPlaceHolder1_schMeeting1_webRegistration1_chkCamp").live('click', function (e) {
        if ($('#ContentPlaceHolder1_schMeeting1_webRegistration1_chkCamp').is(':checked')) {
            var hEmailCamp = document.getElementById('<%=hEmailCampaign.ClientID %>');
            document.getElementById('<%=txtEmailCampaign.ClientID %>').value = hEmailCamp.value;
        }
        else {
            document.getElementById('<%=txtEmailCampaign.ClientID %>').value = "";
        }
    });

});
