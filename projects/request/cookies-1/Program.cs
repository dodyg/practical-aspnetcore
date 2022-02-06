var app = WebApplication.Create();

app.Run(context =>
{
    var cookie = context.Request.Cookies["MyCookie"];

    if (string.IsNullOrWhiteSpace(cookie))
    {
        context.Response.Cookies.Append
        (
            "MyCookie",
            "Hello World",
            new CookieOptions
            {
                Path = "/",
                HttpOnly = false,
                Secure = false
            }
        );
    }

    return context.Response.WriteAsync($"Hello World Cookie: {cookie}. Refresh page to see cookie value.");
});

app.Run();