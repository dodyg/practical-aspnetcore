public class Startup
{
    public void Configure(IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            // Duplicate the code below and write more messages. Save and refresh your browser to see the result.
            await context.Response.WriteAsync("Hello world. Make sure you run this app using 'dotnet watch run'.");
        });
    }
}

public class Program
{
    public static void Main(string[] args) =>
        CreateHostBuilder(args).Build().Run();

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.UseStartup<Startup>()
            );
}