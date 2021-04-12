// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function DrawGraph(detailsArr) {
    const chart = Highcharts.chart('graph-container', {
        chart: {
            type: 'line'
        },
        title: {
            text: 'Investment Growth'
        },
        xAxis: {
            title: { text: 'Time (in years)' },
            categories: Array(detailsArr.length).fill().map((_, idx) => idx + 1)
        },
        yAxis: {
            title: {
                text: 'Total Amount (in USD)'
            }
        },
        series: [{
            name: 'Investment Value',
            data: detailsArr
        }]
    });
}
