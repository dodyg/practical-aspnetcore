var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.MapGet("/", (HttpContext context) => Results.Content($$"""
<html>
<head>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-0evHe/X+R7YkIZDRvuzKMRqM+OrBnVFBL6DOitfPri4tjfHxaWutUpFmBp4vmVor" crossorigin="anonymous">
</head>
<body>
    <div class="container">
        <h1>Upload File</h1>
        <div class="alert alert-info mb-3">You can find the uploaded file in 'uploaded' directory</div>
        <form action="upload" method="post" enctype="multipart/form-data">
            <input type="file" name="file" />
            <br/>
            <button type="submit" class="btn btn-primary mt-3">Upload</button>
        </form>
    </div>
</body>
</html>
""", "text/html"));

app.MapPost("/upload", async (IFormFile file, IWebHostEnvironment env) =>
{
    if (file is not null && file.Length > 0)
    {
        var folder = Path.Combine(env.ContentRootPath, "uploaded");
        if (Directory.Exists(folder) is false)
            Directory.CreateDirectory(folder);

        var path = Path.Combine(folder, file.FileName);
        Console.WriteLine(path);
        using var stream = System.IO.File.OpenWrite(path);
        await file.CopyToAsync(stream); 
    }

    return Results.LocalRedirect("/");
});

app.Run();