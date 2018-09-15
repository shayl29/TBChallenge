var ChartBuilder = function (chartId, chartType) {
    //-----Single Bar --------------
    var c = document.getElementById(chartId);
    var ctx = c.getContext("2d");
    var chart = $("#" + chartId).data("chart") || {};

    var myBarChart = new Chart(
        ctx,
        {
            type: chartType,
            data: chart,
            options: {
                tooltips: {
                    callbacks: {
                        /* Will set the chart tooltip inner text */
                        label: function (tooltipItem, data) {
                            var label = data.datasets[tooltipItem.datasetIndex].label || '';

                            if (label) {
                                label += ': ';
                            }
                            label += parseFloat(tooltipItem.yLabel).toFixed(2);
                            return label;
                        }
                    }
                }
            }
        }
    );
}