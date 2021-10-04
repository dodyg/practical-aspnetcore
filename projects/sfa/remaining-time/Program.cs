using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var app = WebApplication.Create();

app.MapGet("/", () =>
{
    var header = @"<!doctype html>
        <html lang=""en"">
        <head>
            <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css"" rel=""stylesheet"" integrity=""sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU"" crossorigin=""anonymous"">
        </head>
        <body>
    ";

    var footer = @"</body></html>";

    var script = @"
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"" integrity=""sha512-894YE6QWD5I59HgZOGReFYm4dnWc1Qt5NtvYSaNcOP+u1T9qYdvdihz0PPSiiqn/+/3e7Jo4EaG7TubfWGUrMQ=="" crossorigin=""anonymous"" referrerpolicy=""no-referrer""></script>
    <script src=""https://cdn.jsdelivr.net/npm/chart.js@3.5.1/dist/chart.min.js"" integrity=""sha256-bC3LCZCwKeehY6T4fFi9VfOU0gztUa+S4cnkIhVPZ5E="" crossorigin=""anonymous""></script>
    <script src=""https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@2.0.0""></script>
    <script>
        $(function()
        {
            $('#render').click(renderChart);
        });

        function renderChart(){
            var progress = $('#progress').val();
            var remaining = $('#total').val() - progress;
            var ctx = document.getElementById('chart').getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'doughnut',
                plugins: [ChartDataLabels],
                data: {
                labels: [
                    'So Far',
                    'Remaining',
                ],
                datasets: [{
                    label: 'Time Remaining',
                    data: [progress, remaining],
                    backgroundColor: [
                    'rgb(255, 99, 132)',
                    'rgb(54, 162, 235)',
                    ],
                    hoverOffset: 4
                }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    },
                    plugins: {
                        title: {
                            display: true,
                            text: 'Remaining Project Time (weeks)'
                        },
                        datalabels: {
                            color: 'white',
                            font: {
                                size : '40px'
                            },
                        }
                    },
                    responsive : false
                }
            });
        }
    </script>
    ";

    var page = header + @"
        <div class=""container"">
        <h1>Remaining Time</h1>

        <canvas id=""chart"" width=""600"" height=""500""></canvas>
        <form>
            <div class=""row col-md-6"">
                <div class=""col"">
                    <input type=""number"" class=""form-control"" step=""1"" min=""1"" placeholder=""Progress"" id=""progress"" value=""0"">
                </div>
                <div class=""col"">
                    <input type=""number"" class=""form-control"" step=""1"" placeholder=""End"" id=""total"" value=""0"">
                </div>
                <div class=""col"">
                    <button type=""button"" class=""btn btn-primary"" id=""render"">Render</button>
                </div>
            </div>
        </form>
        </div>
        " + script + footer;

    return Results.Text(page, "text/html");
});

app.Run();