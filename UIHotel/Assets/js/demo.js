'use strict';

$(document).ready(function(){

    // Chart Data
    var lineChartData = [
        {
            label: 'Green',
            data: [[1,60], [2,30], [3,50], [4,100], [5,10], [6,90], [7,85]],
            color: '#32c787'
        },
        {
            label: 'Blue',
            data: [[1,20], [2,90], [3,60], [4,40], [5,100], [6,25], [7,65]],
            color: '#03A9F4'
        },
        {
            label: 'Amber',
            data: [[1,100], [2,20], [3,60], [4,90], [5,80], [6,10], [7,5]],
            color: '#f5c942'
        }
    ];

    // Chart Options
    var lineChartOptions = {
        series: {
            lines: {
                show: true,
                barWidth: 0.05,
                fill: 0
            }
        },
        shadowSize: 0.1,
        grid : {
            borderWidth: 1,
            borderColor: '#edf9fc',
            show : true,
            hoverable : true,
            clickable : true
        },

        yaxis: {
            tickColor: '#edf9fc',
            tickDecimals: 0,
            font :{
                lineHeight: 13,
                style: 'normal',
                color: '#9f9f9f',
            },
            shadowSize: 0
        },

        xaxis: {
            tickColor: '#fff',
            tickDecimals: 0,
            font :{
                lineHeight: 13,
                style: 'normal',
                color: '#9f9f9f'
            },
            shadowSize: 0,
        },
        legend:{
            container: '.flot-chart-legends--line',
            backgroundOpacity: 0.5,
            noColumns: 0,
            backgroundColor: '#fff',
            lineWidth: 0,
            labelBoxBorderColor: '#fff'
        }
    };

    // Create chart
    if ($('.flot-line')[0]) {
        $.plot($('.flot-line'), lineChartData, lineChartOptions);
    }
});

'use strict';

$(document).ready(function() {

    // Tooltips for Flot Charts
    if ($('.flot-chart')[0]) {
        $('.flot-chart').bind('plothover', function (event, pos, item) {
            if (item) {
                var x = item.datapoint[0].toFixed(2),
                    y = item.datapoint[1].toFixed(2);
                $('.flot-tooltip').html(item.series.label + ' of ' + x + ' = ' + y).css({top: item.pageY+5, left: item.pageX+5}).show();
            }
            else {
                $('.flot-tooltip').hide();
            }
        });

        $('<div class="flot-tooltip"></div>').appendTo('body');
    }
});

'use strict';

$(document).ready(function () {
    // Sign up widget line chart
    if($('.sparkline-line-signups')[0]) {
        $('.sparkline-line-signups').sparkline('html', {
            type: 'line',
            width: '100%',
            height: 50,
            lineColor: 'rgba(255, 255, 255, 0.8)',
            fillColor: 'rgba(0,0,0,0)',
            lineWidth: 1,
            maxSpotColor: '#fff',
            minSpotColor: '#fff',
            spotColor: '#fff',
            spotRadius: 4,
            highlightSpotColor: '#fff',
            highlightLineColor: '#fff'
        });
    }
});

'use strict';

$(document).ready(function () {
    $('.select2.select').select2();
});
