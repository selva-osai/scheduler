//$(document).ready(function () {
jQuery(function ($) {

    // Content/Briefcase
    $("#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig8").live('click', function (e) {
        var isChk = 0;
        var ht = 110;
        if ($('#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig8').is(':checked')) {
            isChk = 1;
            ht = 365;
        }
        var URL = "/Pages/popup/audiFeature_Content.aspx?flg=" + isChk + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig8",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='audFrame' src=" + URL + " height='" + ht + "' width='680' scrolling='no' class='bgfill'></iframe></div>",
                    680, '.modalClose', '/Pages/Schedule', '#audFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    $("#ContentPlaceHolder1_schMeeting1_webAudience1_lbtnConfig8").live('click', function (e) {
        var URL = "/Pages/popup/audiFeature_Content.aspx?flg=1&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webAudience1_lbtnConfig8",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='audFrame' src=" + URL + " height='365' width='680' scrolling='no' class='bgfill'></iframe></div>",
                    680, '.modalClose', '/Pages/Schedule', '#audFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    // Load dialog on click - FB
    $("#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig15").live('click', function (e) {
        var isChk = 0;
        var ht = 110;
        if ($('#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig15').is(':checked')) {
            isChk = 1;
            ht = 438;
        }
        var URL = "/Pages/popup/audiFeature_FB.aspx?flg=" + isChk + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig15",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='audFrame' src=" + URL + " height='" + ht + "' width='510' scrolling='no' class='bgfill'></iframe></div>",
                    510, '.modalClose', '/Pages/Schedule', '#audFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    $("#ContentPlaceHolder1_schMeeting1_webAudience1_lbtnConfig15").live('click', function (e) {
        var URL = "/Pages/popup/audiFeature_FB.aspx?flg=1&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webAudience1_lbtnConfig15",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='audFrame' src=" + URL + " height='438' width='510' scrolling='no' class='bgfill'></iframe></div>",
                    510, '.modalClose', '/Pages/Schedule', '#audFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    // Load dialog on click - Search
    $("#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig19").live('click', function (e) {
        var isChk = 0;
        var ht = 110;
        var wd = 450;
        if ($('#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig19').is(':checked')) {
            isChk = 1;
            ht = 208;
            wd = 350;
        }
        var URL = "/Pages/popup/audiFeature_Search.aspx?flg=" + isChk + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig19",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='audFrame' src=" + URL + " height='" + ht + "' width='" + wd + "' scrolling='no' class='bgfill'></iframe></div>",
                    wd, '.modalClose', '/Pages/Schedule', '#audFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    $("#ContentPlaceHolder1_schMeeting1_webAudience1_lbtnConfig19").live('click', function (e) {
        var URL = "/Pages/popup/audiFeature_Search.aspx?flg=1&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webAudience1_lbtnConfig19",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='audFrame' src=" + URL + " height='208' width='350' scrolling='no' class='bgfill'></iframe></div>",
                    350, '.modalClose', '/Pages/Schedule', '#audFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    // Load dialog on click - Twitter
    $("#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig16").live('click', function (e) {
        var isChk = 0;
        var ht = 110;
        if ($('#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig16').is(':checked')) {
            isChk = 1;
            ht = 420;
        }
        var URL = "/Pages/popup/audiFeature_Twitter.aspx?flg=" + isChk + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig16",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='audFrame' src=" + URL + " height='" + ht + "' width='540' scrolling='no' class='bgfill'></iframe></div>",
                    540, '.modalClose', '/Pages/Schedule', '#audFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    $("#ContentPlaceHolder1_schMeeting1_webAudience1_lbtnConfig16").live('click', function (e) {
        var URL = "/Pages/popup/audiFeature_Twitter.aspx?flg=1&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webAudience1_lbtnConfig16",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='audFrame' src=" + URL + " height='420' width='540' scrolling='no' class='bgfill'></iframe></div>",
                    540, '.modalClose', '/Pages/Schedule', '#audFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    // Load dialog on click - LinkedIn
    $("#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig17").live('click', function (e) {
        var isChk = 0;
        var ht = 110;
        if ($('#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig17').is(':checked')) {
            isChk = 1;
            ht = 490;
        }
        var URL = "/Pages/popup/audiFeature_LinkedIn.aspx?flg=" + isChk + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig17",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='audFrame' src=" + URL + " height='" + ht + "' width='550' scrolling='no' class='bgfill'></iframe></div>",
                    550, '.modalClose', '/Pages/Schedule', '#audFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    $("#ContentPlaceHolder1_schMeeting1_webAudience1_lbtnConfig17").live('click', function (e) {
        var URL = "/Pages/popup/audiFeature_LinkedIn.aspx?flg=1&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webAudience1_lbtnConfig17",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='audFrame' src=" + URL + " height='490' width='550' scrolling='no' class='bgfill'></iframe></div>",
                    550, '.modalClose', '/Pages/Schedule', '#audFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    // Load dialog on click - Bio
    $("#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig13").live('click', function (e) {
        var isChk = 0;
        if ($('#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig13').is(':checked'))
            isChk = 1;
        var URL = "/Pages/popup/audiFeature_Bio.aspx?flg=" + isChk + "&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webAudience1_chkConfig13",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='audFrame' src=" + URL + " height='380' width='660' scrolling='no' class='bgfill'></iframe></div>",
                    660, '.modalClose', '/Pages/Schedule', '#audFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });

    $("#ContentPlaceHolder1_schMeeting1_webAudience1_lbtnConfig13").live('click', function (e) {
        var URL = "/Pages/popup/audiFeature_Bio.aspx?flg=1&ID=" + hWebID.value;
        qtipPopup2("#ContentPlaceHolder1_schMeeting1_webAudience1_lbtnConfig13",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='audFrame' src=" + URL + " height='380' width='660' scrolling='no' class='bgfill'></iframe></div>",
                    660, '.modalClose', '/Pages/Schedule', '#audFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
    });
});

