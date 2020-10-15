using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace middleware_13
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
    
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }

    public interface ICustomApiException
    {
        HttpStatusCode HttpStatus { get; }
        string HttpMessage { get; }
    }
    
    public class ExampleApiException : Exception, ICustomApiException
    {
        public HttpStatusCode HttpStatus => HttpStatusCode.Forbidden;
        public string HttpMessage => "Resource cannot be accessed.";

        public ExampleApiException(string message) : base(message)
        {
        }
    }

    public class ErrorDto
    {
        public int Status { get; set; }
        public string Message { get; set; }
    }

    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var httpStatus = HttpStatusCode.InternalServerError;
                var httpMessage = "Internal Server Error";
                
                if (ex is ICustomApiException customException)
                {
                    httpStatus = customException.HttpStatus;
                    httpMessage = customException.HttpMessage;
                }
                
                context.Response.StatusCode = (int)httpStatus;
                context.Response.ContentType = "application/json";

                var error = new ErrorDto()
                {
                    Status = (int)httpStatus,
                    Message = httpMessage
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            }
        }
    }
}
