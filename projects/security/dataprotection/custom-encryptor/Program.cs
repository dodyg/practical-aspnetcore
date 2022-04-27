using System.IO;
using CustomEncryptor.Extensions;
using CustomEncryptor.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder();
var keysFolder = Path.Combine(Directory.GetCurrentDirectory(), "temp-keys");
builder.Services.AddDataProtection()
        .PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
        .UseXmlEncryptor(s => new CustomXmlEncryptor(s));
                    
builder.Services.AddRazorPages();

var app = builder.Build();
app.UseStaticFiles();
app.MapRazorPages();

app.Run();