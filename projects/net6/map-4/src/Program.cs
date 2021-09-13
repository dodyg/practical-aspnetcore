using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

string Plaintext() => "Hello, World!";
app.MapGet("/hello", Plaintext);

Greeting Json() => new Greeting("Hello, World!");
app.MapGet("/json", Json);

app.MapGet("/hello/{name}", (string name) => new Greeting($"Hello, {name}!"));

app.Run();

public record Greeting(string Message);