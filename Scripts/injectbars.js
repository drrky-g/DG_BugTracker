//this script will inject relevant data to set the widths of the progress bars on the home dashboard
$(document).ready(function () {
    //initialize tooltips for bars 
    $('[data-toggle="tooltip"]').tooltip();

    var allBars = $('.inject').get();
    var loopStop = allBars.length + 1;

    for (var bar = 1; bar < loopStop; bar++) {
        var thisBar = allBars.pop();
        var barValue = Number($(thisBar).attr('value'));
        var totalValue = Number($(thisBar).attr('max'));
        var calculate = (barValue / totalValue) * 100;
        var round = Math.round(calculate);
        var percent = round + "%";
        $(thisBar).css("width", percent);
    };

});