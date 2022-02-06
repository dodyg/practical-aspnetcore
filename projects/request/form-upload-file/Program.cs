var app = WebApplication.Create();

app.MapGet("", async context =>
{
    context.Response.Headers.Add("content-type", "text/html");

    var body = $@"
                <html><body>
                <h1>Upload File</h1>
                <form action=""Upload"" method=""post"" enctype=""multipart/form-data"">
                        <input type=""file"" name=""file"" />
                        <input type=""submit"" value=""Upload"" />
                </form>
                </body></html>
                ";

    await context.Response.WriteAsync(body);
});

app.MapPost("Upload", async context =>
{
    if (context.Request.HasFormContentType)
    {
        var form = await context.Request.ReadFormAsync();

        foreach (var f in form.Files)
        {
            using (var body = f.OpenReadStream())
            {
                var fileName = Path.Combine(app.Environment.ContentRootPath, f.FileName);
                File.WriteAllBytes(fileName, ReadFully(body));
                await context.Response.WriteAsync($"Uploaded file written to {fileName}");
            }
        }
    }
    await context.Response.WriteAsync("");
});

app.Run();

static byte[] ReadFully(Stream input)
{
    byte[] buffer = new byte[16 * 1024];
    using var ms = new MemoryStream();
    int read;
    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
    {
        ms.Write(buffer, 0, read);
    }
    return ms.ToArray();
}