
function OnChangeEvent() {
    var a = $('#ShouldBe').val();
    var b = $('#OnAccount').val();
    var result = b - a;

    $('#Difference').val(result.toString());

    var totalIncome = $('#TotalIncome').val();
    var totalOutcome = $('#TotalOutcome').val();
    var difference = $('#Difference').val();
    var bilans = totalIncome - totalOutcome - difference;

    $('#PerMonth').val(bilans.toString());
};