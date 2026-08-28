var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// -------------------------------
// CASE 1: Primitive Obsession
// -------------------------------
// We rely on primitive types (string) to represent a domain concept.
// This forces validation logic to be repeated everywhere.

app.MapPost("/orders-bad", (string email) =>
{
if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
return Results.BadRequest("Invalid email address");

return Results.Ok($"Order placed for {email}");
});


// -------------------------------
// CASE 2: Value Object
// -------------------------------
// We represent the concept using a dedicated type.
// ASP.NET Core will use TryParse during model binding.

app.MapPost("/orders-good", (EmailAddress email) =>
{
// If execution reaches here, the email is guaranteed to be valid
return Results.Ok($"Order placed for {email.Value}");
});


app.Run();


// -------------------------------
// Value Object
// -------------------------------
public sealed record EmailAddress
{
    public string Value { get; }

    private EmailAddress(string value)
    {
        Value = value;
    }

    // ASP.NET Core Minimal APIs automatically use TryParse
    // during parameter binding.
    public static bool TryParse(string? value, out EmailAddress? result)
    {
        if (!string.IsNullOrWhiteSpace(value) && value.Contains("@"))
        {
            result = new EmailAddress(value);
            return true;
        }

        result = null;
        return false;
    }
}