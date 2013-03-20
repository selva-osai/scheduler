
//Mutual exclusive check box

$(function () {
    // get all the checkboxes    
    var $tblChkBox = $("table.cbl input:checkbox");
    // add a click handler to each checkbox    
    $tblChkBox.click(function () {
        // get the id of the selected checkbox        
        var selectedId = this.id;
        // uncheck all checkboxes except the selected one        
        $tblChkBox.each(function () {
            if (this.id != selectedId) this.checked = false;
        });
    });
});