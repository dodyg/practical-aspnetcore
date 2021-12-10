using System.Net;
using System.Text.Json;
var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<ErrorHandlingMiddleware>();

var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.Run(context => throw new ExampleApiException("oops"));
app.Run();

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
            await next(context);
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
