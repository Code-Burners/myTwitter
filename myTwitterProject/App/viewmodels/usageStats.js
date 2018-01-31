define(['durandal/app', 'knockout', 'plugins/router', 'durandal/system'], function (app, ko, router, system) {


(function () {



    Highcharts.chart('chart1', {


        chart: {
            type: 'column'
        },
        title: {
            text: "Top 10 Users of myTwitter"
        },
        subtitle: {
            text: 'Heart beats, Your Tweets'
        },
        xAxis: {
            categories: [
                'Sam'
                , 'Sabi'
            ],
            crosshair: true
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Rainfall (mm)'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y} Actions</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: [{
            name: "Usage",
            data: [42, 43]
        }]
    });

});
    return {


    }
});