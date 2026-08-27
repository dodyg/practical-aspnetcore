using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCompression();
builder.Services.AddRequestDecompression();

// Zstandard is enabled by default; tune the compression quality (1-22).
builder.Services.Configure<ZstandardCompressionProviderOptions>(options =>
{
    options.CompressionOptions = new ZstandardCompressionOptions { Quality = 6 };
});

var app = builder.Build();
app.UseResponseCompression();
app.UseRequestDecompression();

app.MapGet("/", () => string.Concat(Enumerable.Repeat("Hello, zstd! ", 1000)));

app.MapPost("/echo", async (HttpRequest request) =>
{
    using var reader = new StreamReader(request.Body);
    return "echo: " + await reader.ReadToEndAsync();
});

app.Run();
