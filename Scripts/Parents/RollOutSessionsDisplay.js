/// <reference path="../jquery-1.6.4-vsdoc.js" />


function Layout() {
    $(document).ready(function () {
        var Loads = $('#ctl00_MainContent_LoadsHiddenField').val();
        if (Loads == 1)
            $('#ctl00_MainContent_DisplayPanel').show(500);
    });
}