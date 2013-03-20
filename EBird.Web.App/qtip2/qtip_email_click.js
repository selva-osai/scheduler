$(document).ready(function () {

    // EMAIL CONFIRMATION - Preview
    $('#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnEmailPreviewConf').live('click', function (e) {
        var themeUrl = $(this).attr('data-typ');
        //var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
        var URL = "/Pages/popup/EmailPreview.aspx?typ=" + themeUrl + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnEmailPreviewConf",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='emlFrame' src=" + URL + " height='525' width='670' scrolling='no' class='bgfill'></iframe></div>",
                    750, '.modalClose', '', '#emlFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    // EMAIL CONFIRMATION - Edit
    $('#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnEmailEditConf').live('click', function (e) {
        var themeUrl = $(this).attr('data-typ');
        //var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
        var URL = "/Pages/popup/Notification_EmailTpl.aspx?typ=" + themeUrl + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnEmailEditConf",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='emlFrame' src=" + URL + " height='465' width='670' scrolling='no' class='bgfill'></iframe></div>",
                    670, '.modalClose', '', '#emlFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    // EMAIL CONFIRMATION - Review
    $('#ContentPlaceHolder1_schMeeting1_webEmail1_lblEmailReviewConf').live('click', function (e) {
        var themeUrl = $(this).attr('data-typ');
        //var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
        var URL = "/Pages/popup/Notification_EmailTpl.aspx?typ=" + themeUrl + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webEmail1_lblEmailReviewConf",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='emlFrame' src=" + URL + " height='600' width='670' scrolling='no' class='bgfill'></iframe></div>",
                    670, '.modalClose', '', '#emlFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    // EMAIL REMINDER - Preview
    $('#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnRemPreview').live('click', function (e) {
        var themeUrl = $(this).attr('data-typ');
        //var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
        var URL = "/Pages/popup/EmailPreview.aspx?typ=" + themeUrl + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnRemPreview",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='emlFrame' src=" + URL + " height='525' width='750' scrolling='no' class='bgfill'></iframe></div>",
                    750, '.modalClose', '', '#emlFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    // EMAIL REMINDER - Edit
    $('#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnRemEdit').live('click', function (e) {
        var themeUrl = $(this).attr('data-typ');
        //var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
        var URL = "/Pages/popup/Notification_EmailTpl.aspx?typ=" + themeUrl + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnRemEdit",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='emlFrame' src=" + URL + " height='465' width='670' scrolling='no' class='bgfill'></iframe></div>",
                    670, '.modalClose', '', '#emlFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    // EMAIL REMINDER - Review
    $('#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnRemReviewSend').live('click', function (e) {
        var themeUrl = $(this).attr('data-typ');
        //var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
        var URL = "/Pages/popup/Notification_EmailTpl.aspx?typ=" + themeUrl + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnRemReviewSend",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='emlFrame' src=" + URL + " height='600' width='670' scrolling='no' class='bgfill'></iframe></div>",
                    670, '.modalClose', '', '#emlFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    // EMAIL ATTENDEE FOLLOW-UP - Preview
    $('#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnFollowAttendPreview').live('click', function (e) {
        var themeUrl = $(this).attr('data-typ');
        //var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
        var URL = "/Pages/popup/EmailPreview.aspx?typ=" + themeUrl + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnFollowAttendPreview",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='emlFrame' src=" + URL + " height='525' width='750' scrolling='no' class='bgfill'></iframe></div>",
                    750, '.modalClose', '', '#emlFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    // EMAIL ATTENDEE FOLLOW-UP - Edit
    $('#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnFollowAttendEdit').live('click', function (e) {
        var themeUrl = $(this).attr('data-typ');
        //var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
        var URL = "/Pages/popup/Notification_EmailTpl.aspx?typ=" + themeUrl + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnFollowAttendEdit",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='emlFrame' src=" + URL + " height='465' width='670' scrolling='no' class='bgfill'></iframe></div>",
                    670, '.modalClose', '', '#emlFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    // EMAIL ATTENDEE FOLLOW-UP - Review
    $('#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnFollowAttendReview').live('click', function (e) {
        var themeUrl = $(this).attr('data-typ');
        //var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
        var URL = "/Pages/popup/Notification_EmailTpl.aspx?typ=" + themeUrl + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnFollowAttendReview",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='emlFrame' src=" + URL + " height='600' width='670' scrolling='no' class='bgfill'></iframe></div>",
                    670, '.modalClose', '', '#emlFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    // EMAIL NON-ATTENDEE FOLLOW-UP - Preview
    $('#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnUnFollowAttendPreview').live('click', function (e) {
        var themeUrl = $(this).attr('data-typ');
        //var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
        var URL = "/Pages/popup/EmailPreview.aspx?typ=" + themeUrl + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnUnFollowAttendPreview",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='emlFrame' src=" + URL + " height='525' width='750' scrolling='no' class='bgfill'></iframe></div>",
                    750, '.modalClose', '', '#emlFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    // EMAIL NON-ATTENDEE FOLLOW-UP - Edit
    $('#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnUnFollowAttendEdit').live('click', function (e) {
        var themeUrl = $(this).attr('data-typ');
        //var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
        var URL = "/Pages/popup/Notification_EmailTpl.aspx?typ=" + themeUrl + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnUnFollowAttendEdit",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='emlFrame' src=" + URL + " height='465' width='670' scrolling='no' class='bgfill'></iframe></div>",
                    670, '.modalClose', '', '#emlFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    // EMAIL NON-ATTENDEE FOLLOW-UP - Review
    $('#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnUnFollowAttendReview').live('click', function (e) {
        var themeUrl = $(this).attr('data-typ');
        //var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
        var URL = "/Pages/popup/Notification_EmailTpl.aspx?typ=" + themeUrl + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webEmail1_lbtnUnFollowAttendReview",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='emlFrame' src=" + URL + " height='600' width='670' scrolling='no' class='bgfill'></iframe></div>",
                    670, '.modalClose', '', '#emlFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

});
