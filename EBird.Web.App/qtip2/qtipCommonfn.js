﻿function qtipPopup(element, contentString, frmID, targetWinEle, wd, ht, closebtn, refreshURL) {
    $(element).qtip({
        overwrite: false, content: contentString,
        position: { at: 'center', my: 'center', target: $(window) },
        //show: { event: 'click', modal: { on: true, blur: false, escape: true }, solo: true },
        show: { ready: true, modal: { on: true} },
        hide: 'unfocus',
        style: { classes: 'ui-tooltip-shadow', width: wd, height: ht },
        events: {
            render: function (event, api) {
                $(frmID).load(function () {
                    var updatedMsg = $(frmID).contents().find(targetWinEle).val();
                    if (updatedMsg == '1') {
                        $(element).trigger('unfocus');
                        if (refreshURL != '')
                            window.location = refreshURL;
                        $(element).qtip('destroy');
                    }
                });
            },
            show: function (event, api) {
                api.elements.content.find(closebtn).click(function () {
                    $(element).qtip('destroy');
                });
            }
        }
    });
}

function qtipPopup2(element, contentString, wd, closebtn, refreshURL, frameID, modFlag) {
    $(element).qtip({
        overwrite: false, content: contentString,
        position: { at: 'center', my: 'center', target: $(window) },
        //show: { event: 'click', modal: { on: true, blur: false, escape: true }, solo: true },
        show: { event: 'click', ready: true, modal: { on: true} },
        hide: 'unfocus',
        style: { classes: 'ui-tooltip-shadow', width: wd },
        events: {
            render: function (event, api) {
                $(frameID).load(function () {
                    var updatedMsg = $(frameID).contents().find(modFlag).val();
                    if (updatedMsg == '1') {
                        $(element).qtip('hide');
                        if (refreshURL != "") {
                            window.location = refreshURL;
                        }
                        $(element).qtip('destroy');
                    }
                });
            },
            show: function (event, api) {
                api.elements.content.find(closebtn).click(function () {
                    if (refreshURL != '')
                        window.location = refreshURL;
                    $(element).qtip('destroy');
                });
            }
        }
    });
}

function qtipPopup3(element, contentString, wd, closebtn, refreshURL, frameID, modFlag) {
    $(element).qtip({
        overwrite: false, content: contentString,
        position: { at: 'center', my: 'center', target: $(window) },
        //show: { event: 'click', modal: { on: true, blur: false, escape: true }, solo: true },
        show: { event: 'click', modal: { on: true} },
        hide: 'unfocus',
        style: { classes: 'ui-tooltip-shadow', width: wd },
        events: {
            render: function (event, api) {
                $(frameID).load(function () {
                    var updatedMsg = $(frameID).contents().find(modFlag).val();
                    if (updatedMsg == '1') {
                        $(element).qtip('hide');
                        if (refreshURL != "") {
                            window.location = refreshURL;
                        }
                        $(element).qtip('destroy');
                    }
                });
            },
            show: function (event, api) {
                api.elements.content.find(closebtn).click(function () {
                    $(element).qtip('destroy');
                });
            }
        }
    });
}