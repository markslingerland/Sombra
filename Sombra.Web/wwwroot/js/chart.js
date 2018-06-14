var ctx = document.getElementById("myChart").getContext('2d');

var gradientStroke = ctx.createLinearGradient(100, 0, 500, -100);
gradientStroke.addColorStop(0, "#fda23f");
gradientStroke.addColorStop(1, "#f3646f");

var myChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: ["jan", "feb", "mar", "apr", "mei", "jun", "jul", "aug", "sep", "okt", "nov" , "dec"],
        datasets: [{
            borderColor:               gradientStroke,
            pointBorderColor:          gradientStroke,
            pointBackgroundColor:      gradientStroke,
            pointHoverBackgroundColor: gradientStroke,
            pointHoverBorderColor:     gradientStroke,
            data: [500,750,860,1114,1600,1156],
            borderWidth: 9,
            fill: false, 
            pointBackgroundColor: gradientStroke,
            pointBorderColor: gradientStroke,
            pointHoverBackgroundColor: '#ffffff',
            pointHoverBorderColor: gradientStroke,
            pointRadius: [0,0,0,0,0,6],
            pointHoverRadius: 14,
            hitRadius: 50,
            pointBorderWidth: 7,
        }]
    },
    options: {
        tooltips: {
            backgroundColor: 'rgba(248,131,86,1)',
            borderColor: 'rgba(248,131,86,1)',
            caretPadding: 28,
            yAlign: 'bottom',
            xAlign: 'center',

            caretSize: 10,
            titleFontSize: 0,
            titleSpacing: 0,
            titleMarginBottom: 0,
            displayColors: false,
            bodyFontSize: 16,
            xPadding: 20,
            yPadding: 15,
            callbacks: {
                label: function(tooltipItem, data) {
                    return '€' + tooltipItem.yLabel + ',00';
                }
            }
        },
        title: {
            display: true,
            position: 'top',
            fontSize: 18,
            fontColor: '#838383',
            fontFamily: 'Source Sans Pro',
            padding: 25,
            text: 'Opgehaalde bedragen in het jaar 2018                                                                                                                                                                      '
        },
        layout: {
            padding: {
                left: 70,
                top: 34,
                bottom: 45,
                right: 95
            }
        },  
        legend: {
            display: false
        },
        scales: {
            yAxes: [{
                gridLines: {
                    display: true,
                    drawOnChartArea: false,
                    color: '#979797',
                    zeroLineColor: '#979797'
                },
                ticks: { 
                    maxTicksLimit: 5,
                    fontSize: 18,
                    fontFamily: 'Source Sans Pro',
                    fontColor: '#838383',
                    beginAtZero:true,
                    padding: 8
                }
            }],
            xAxes: [{
                gridLines: {
                    offset: true,
                    display: true,
                    drawOnChartArea: false,
                    color: '#979797',
                    zeroLineColor: '#979797'
                },
                ticks: {
                    fontSize: 18,
                    fontFamily: 'Source Sans Pro',
                    fontColor: '#838383',
                    padding: 3

                }
            }]
        }
    }
});


