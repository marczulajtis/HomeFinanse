$(document.body).ready(function () {
    $("#incomesLink").on('click', (function () {
        var url = $("#body").data('incomes-url');
        $("#body").load(url);
    }))
});

$(document.body).ready(function () {
    $("#outcomesLink").on('click', (function () {
        var url = $('#body').data('outcomes-url');
        $("#body").load(url);
    }))
});

$(document.body).ready(function () {
    $("#categoriesLink").on('click', (function () {
        var url = $("#body").data('categories-url');
        $("#body").load(url);
    }))
});

$(document.body).ready(function () {
    $("#periodsLink").on('click', (function () {
        var url = $("#body").data('periods-url');
        $("#body").load(url);
    }))
});