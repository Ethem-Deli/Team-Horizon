window.reportsCharts = window.reportsCharts || {};

window.reportsCharts.renderMonthly = function (labels, values, colors) {
    var pieCanvas = document.getElementById('monthly-category-pie');
    var barCanvas = document.getElementById('monthly-category-bar');

    if (!pieCanvas || !barCanvas || !window.Chart) {
        return;
    }

    if (window.reportsCharts._pie) {
        window.reportsCharts._pie.destroy();
    }

    if (window.reportsCharts._bar) {
        window.reportsCharts._bar.destroy();
    }

    window.reportsCharts._pie = new Chart(pieCanvas, {
        type: 'doughnut',
        data: {
            labels: labels,
            datasets: [{
                data: values,
                backgroundColor: colors,
                borderWidth: 0
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'bottom'
                }
            }
        }
    });

    window.reportsCharts._bar = new Chart(barCanvas, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Total Spent',
                data: values,
                backgroundColor: colors,
                borderRadius: 6
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    ticks: {
                        callback: function (value) {
                            return '$' + value;
                        }
                    }
                }
            },
            plugins: {
                legend: {
                    display: false
                }
            }
        }
    });
};
