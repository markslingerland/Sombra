var ctx = document.getElementById("myChart").getContext('2d');

var gradientStroke = ctx.createLinearGradient(500, 0, 100, 0);
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
            label: 'Opgehaald bedrag',
            data: [500,750,1000,1250,1500,2000,1500,1800,1300,1000,750,1500],
            borderWidth: 9,
            fill: false, 
            pointBackgroundColor: '#ffffff',
            pointBorderColor: '#ffffff',
            pointHoverBackgroundColor: '#ffffff',
            pointHoverBorderColor: '#ffffff',
            pointRadius: [0,0,0,0,0,0,0,0,0,0,0,16],
            pointHoverRadius: [0,0,0,0,0,0,0,0,0,0,0,16]
        }]
    },
    options: {
        title: {
            display: true,
            position: 'top',
            fontSize: 18,
            fontColor: '#838383',
            padding: 25,
            text: 'Opgehaalde bedragen in het jaar 2018                                                                                                                                                                      '
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
                    fontColor: '#838383',
                    beginAtZero:true,
                    padding: 8
                }
            }],
            xAxes: [{
                gridLines: {
                    display: true,
                    drawOnChartArea: false,
                    color: '#979797',
                    zeroLineColor: '#979797'
                },
                ticks: {
                    fontSize: 18,
                    fontColor: '#838383',
                    padding: 3

                }
            }]
        }
    }
});
