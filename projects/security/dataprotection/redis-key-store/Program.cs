using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder();

var configuration = builder.Configuration;

var redisServer = configuration["DataProtection:RedisServer"];
var redisPassword = configuration["DataProtection:RedisPassword"];
var redisKey = configuration["DataProtection:RedisKey"];
var redis = ConnectionMultiplexer.Connect($"{redisServer},password={redisPassword}");

builder.Services.AddDataProtection().PersistKeysToStackExchangeRedis(redis, redisKey);
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();
app.UseAuthorization();
app.MapRazorPages();

app.Run();