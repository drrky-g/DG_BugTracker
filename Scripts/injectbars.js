//this script will inject relevant data to set the widths of the progress bars on the home dashboard
$(document).ready(function () {
    //initialize tooltips for bars 
    $('[data-toggle="tooltip"]').tooltip();

    var allBars = $('.inject').get();
    var loopStop = allBars.length + 1;

    for (var bar = 1; bar < loopStop; bar++) {
        //get element
        var thisBar = allBars.pop();
        //get attribute 'value'
        var barValue = Number($(thisBar).attr('value'));
        //get attribute 'max'
        var totalValue = Number($(thisBar).attr('max'));
        //calculate the percentage and round it
        var calculate = (barValue / totalValue) * 100;
        var round = Math.round(calculate);
        //add percent symbol so that its css friendly
        var percent = round + "%";
        //set width to be equal to rounded, calculated percent
        $(thisBar).css("width", percent);
    };

});