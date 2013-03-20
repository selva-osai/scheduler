function qtipPassPolicy(element, contentString, wd, closebtn) {
    $(element).qtip({
        overwrite: false, content: contentString,
        position: { at: 'center', my: 'center', target: $(window) },
        //show: { event: 'click', modal: { on: true, blur: false, escape: true }, solo: true },
        show: { event: 'click', ready: true, modal: { on: true} },
        hide: 'unfocus',
        style: { classes: 'ui-tooltip-shadow', width: wd },
        events: {
            show: function (event, api) {
                api.elements.content.find(closebtn).click(function () {
                    $(element).qtip('hide');
                });
            }
        }
    });
}