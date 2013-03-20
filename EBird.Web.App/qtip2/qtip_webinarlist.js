$(document).ready(function () {
    $(".dummycss").each(function (event) {
        var hWebID = $(this).attr('data-theme');
        var URL = "/Pages/popup/DeleteWebinar.aspx?ID=" + hWebID;
        qtipPopup2(".dummycss",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='popFrame1' src=" + URL + " height='120' scrolling='no' class='bgfill'></iframe></div>",
                    320, ".modalClose", "/Pages/Webinar", "#popFrame1", "body #ContentPlaceHolder1_hModalStatusFlg");
    });
    $('.gridIco').each(function (event) {
        var webinarID = $(this).attr('data-webid');
        var URL = "/pages/popup/PresenterContact.aspx?cmd=" + webinarID;
        qtipPopup2(".gridIco",
                "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='popFrame2' src=" + URL + " height='235' width='716' scrolling='no' class='bgfill'></iframe></div>",
                716, ".modalClose", "", "#popFrame2", "body #ContentPlaceHolder1_hModalStatusFlg");
    });
    $('.gridIco1').each(function (event) {
        var webinarID = $(this).attr('data-webid');
        //var typ = $(this).attr('data-typ');
        var URL = "ViewWebinarURLs.aspx?cmd=" + webinarID;
        qtipPopup2(".gridIco1",
                "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='popFrame3' src=" + URL + " height='310' width='720' scrolling='no' class='bgfill'></iframe></div>",
                720, ".modalClose", "", "#popFrame3", "body #ContentPlaceHolder1_webaction1_hModalStatusFlg");
    });
    $("#ContentPlaceHolder1_webinarlist1_AdvancedSearch").each(function (event) {
        var URL = "/Pages/AdSearchWebinar";
        qtipPopup2("#ContentPlaceHolder1_webinarlist1_AdvancedSearch",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='popFrame4' src=" + URL + " height='237' width='550' scrolling='no' class='bgfill'></iframe></div>",
                    550, ".modalClose", "/Pages/Webinar", "#popFrame4", "body #ContentPlaceHolder1_hModalStatusFlg");
    });
});
        