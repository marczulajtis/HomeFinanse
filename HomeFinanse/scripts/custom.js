function ChangeBodyColor() {
    $('body').addClass('whiteBackground');
};

function LoadSummaryOnAppStart() {
    $.get('/Home/Summary', { selectedPeriodID: $("#periodsDropdown").val() }, function (data) {
        $("#myContentView").html(data);
    });
}


function LoadSummary() {

    $.post('/Home/Summary', function (data) {
        $("#myContentView").html(data);
    });
};

function SetCurrentPeriod() {
    $.post("/Home/SetSelectedPeriodID", {
        selectedPeriodID: $("#periodsDropdown").val()
    });

    if ($("#overviewLink").hasClass("active")) {
        LoadSummaryOnAppStart();
    }
    else if ($('#incomesLink').hasClass("active")) {
        $('#incomesLink').trigger("click");
    }
    else if ($('#outcomesLink').hasClass("active")) {
        $('#outcomesLink').trigger("click");
    }
    else if ($('#periodsLink').hasClass("active")) {
        $('#periodsLink').trigger("click");
    }
};

function OnChangeEvent() {
    var a = $('#ShouldBe').val();
    var b = $('#OnAccount').val();
    var result = b - a;

    $('#Difference').val(result);

    var totalIncome = parseInt($('#TotalIncome').val());
    var totalOutcome = parseInt($('#TotalOutcome').val());
    var difference = parseInt($('#Difference').val());

    if (difference > 0) { 

        var bilans = totalIncome - totalOutcome;
        bilans = bilans + difference;

        $('#differenceLabel').text = "Za dużo o :";
        $('#PerMonth').val(bilans);
    }
    else {
        var abs_difference = Math.abs(difference);
        var bilans1 = (totalIncome - totalOutcome) - abs_difference;

        $('#differenceLabel').text = "Brakuje :";

        if (bilans < 0) {
            $('#PerMonth').addClass('label-danger');
            $('#PerMonth').removedClass('label-success');
        }
        else {
            $('#PerMonth').addClass('label-success');
            $('#PerMonth').removeClass('label-danger');
        }
        
        $('#PerMonth').val(bilans1.toString());
    }

};


$(function () {
    $('#overviewLink').on('click', function () {
        $.post('/Home/Summary', function (data) {
            $("#myContentView").html(data);

            // set current link to active
            $('#overviewLink').addClass('active');

            // set rest of link unactive
            $('#incomesLink').removeClass('active');
            $('#outcomesLink').removeClass('active');
            $('#categoriesLink').removeClass('active');
            $('#periodsLink').removeClass('active');
        });
    });

    $('#incomesLink').on("click", function () {
        $.get('/Incomes/Income/ShowIncomes', function (data) {
            $("#myContentView").html(data);

            // set current link to active
            $('#incomesLink').addClass('active');

            // set rest of link unactive
            $('#overviewLink').removeClass('active');
            $('#outcomesLink').removeClass('active');
            $('#categoriesLink').removeClass('active');
            $('#periodsLink').removeClass('active');
        });
    });
    $('#outcomesLink').on("click", function () {
        $.get('/Outcomes/Outcome/ShowOutcomes', function (data) {
            $("#myContentView").html(data);

            // set current link to active
            $('#outcomesLink').addClass('active');

            // set rst of link unactive
            $('#overviewLink').removeClass('active');
            $('#incomesLink').removeClass('active');
            $('#categoriesLink').removeClass('active');
            $('#periodsLink').removeClass('active');
        });
    });
    $('#categoriesLink').on("click", function () {
        $.get('/Categories/Category/ShowCategories', function (data) {
            $("#myContentView").html(data);

            // set current link to active
            $('#categoriesLink').addClass('active');

            // set rst of link unactive
            $('#overviewLink').removeClass('active');
            $('#incomesLink').removeClass('active');
            $('#outcomesLink').removeClass('active');
            $('#periodsLink').removeClass('active');
        });
    });
    $('#periodsLink').on("click", function () {
        $.get('/Periods/Period/ShowPeriods', function (data) {
            $("#myContentView").html(data);

            // set current link to active
            $('#periodsLink').addClass('active');

            // set rst of link unactive
            $('#overviewLink').removeClass('active');
            $('#incomesLink').removeClass('active');
            $('#outcomesLink').removeClass('active');
            $('#categoriesLink').removeClass('active');
        });
    });
});

(function () {
    $('body').on('click', '.editOutcome',  function (e) {
        e.preventDefault();
        $(this).attr('data-target', '#modal-container');
        $(this).attr('data-toggle', 'modal');
    });

    $('body').on('click', '.modal-close-btn', function () {
        $('#modal-container').modal('hide');
    });
});

function RefreshPeriods() {
    $.ajax({
        url: '/Home/RefreshPeriods',
        type: 'GET',
        dataType: 'json',
        cache: false,
        success: function (result) {
            $('#periodsDropdown').html("");

            for (var i = 0; i < result.periods.length; i++) {
                $('#periodsDropdown').append($('<option></option>').val(result.periods[i].Value).html(result.periods[i].Text));
            }
        },
        error: function () {
            alert('Error occured');
        }
    });
}

function editOutcome(outcomeId) {
    var data = {
        outcomeId: outcomeId
    };

    $.ajax({
        url: '/Outcomes/Outcome/EditOutcome/',
        type: 'GET',
        data: data, 
        contentType: 'application/html',
        success: function (content) {
            $('#modalWrapper').html(content);
            $('#editModal').modal("show");
        },
        error: function (ex) {
            console.log(ex);
        }
    });
}