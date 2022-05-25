var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOrchardCms();
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseOrchardCore(x => x.UsePoweredByOrchardCore(enabled: false));

app.Run();

