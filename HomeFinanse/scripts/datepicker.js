jQuery(function($) {
    //$(document.body).on("focus", "#datepicker", function () {
    //    $(this).datetimepicker({
    //        format: "dd-MM-yyyy"
    //    });
    //});
    $('#datepicker1').datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        yearRange: "-100:+0",
        dateFormat: 'dd/mm/yy'
    });
});