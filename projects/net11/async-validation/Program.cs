using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Validation;

const string IndexHtml = """
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>Async validation sample</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
</head>
<body class="container">
    <h1>Async validation</h1>
    <p>Submit the form below to POST to <code>/register</code>. Try
       <code>taken@example.com</code> (fails the async unique-email rule) or
       a username equal to the email (fails the async object rule).</p>
    <form id="form">
        <fieldset>
            <label>Email <input name="email" value="new@example.com"></label>
            <label>Password <input name="password" type="password" value="a-strong-password"></label>
            <label>Username <input name="username" value="bob"></label>
            <button type="submit">Submit</button>
        </fieldset>
    </form>
    <p id="status" role="status"></p>
    <pre id="output" style="min-height:200px;white-space:pre-wrap"></pre>

    <script>
        document.getElementById('form').addEventListener('submit', async (event) => {
            event.preventDefault();
            const form = new FormData(event.target);
            const body = Object.fromEntries(form.entries());
            const status = document.getElementById('status');
            const output = document.getElementById('output');
            status.textContent = '';
            output.textContent = 'Sending...';
            try {
                const response = await fetch('/register', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(body)
                });
                const text = await response.text();
                output.textContent = text;
                status.textContent = `HTTP ${response.status} ${response.ok ? 'OK' : 'Invalid'}`;
                status.className = response.ok ? 'ok' : 'bad';
            } catch (error) {
                status.textContent = `Error: ${error}`;
                status.className = 'bad';
            }
        });
    </script>
</body>
</html>
""";

var builder = WebApplication.CreateBuilder(args);

// Enables validation of [ValidatableType] bodies for minimal API endpoints.
builder.Services.AddValidation();

var app = builder.Build();

app.MapPost("/register", (RegisterRequest request) => Results.Ok(request));

app.MapGet("/", () => Results.Content(IndexHtml, "text/html"));

app.Run();

// All application code below this point.

[ValidatableType]
public partial class RegisterRequest
{
    [Required, EmailAddress, UniqueEmail]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(100, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string Username { get; set; } = string.Empty;
}

/// <summary>
/// Async validation attribute: validates against a remote/database source
/// without blocking a thread.
/// </summary>
public sealed class UniqueEmailAttribute : AsyncValidationAttribute
{
    // This attribute only knows how to validate asynchronously.
    protected override ValidationResult? IsValid(object? value, ValidationContext context) =>
        throw new InvalidOperationException("Validate this attribute with IsValidAsync.");

    protected override async Task<ValidationResult?> IsValidAsync(
        object? value, ValidationContext context, CancellationToken cancellationToken)
    {
        // Simulate a remote/database lookup.
        await Task.Delay(50, cancellationToken);

        return value as string == "taken@example.com"
            ? new ValidationResult("That email is already registered.")
            : ValidationResult.Success;
    }
}

// Marker attribute so we can demo IAsyncValidatableObject on a real member.
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class IAsyncValidatableObjectSampleAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        // The actual validation lives in RegisterRequest.IAsyncValidate.
        return ValidationResult.Success;
    }
}

public partial class RegisterRequest : IAsyncValidatableObject
{
    // IValidatableObject.Validate is still abstract; this type only knows how
    // to validate asynchronously.
    public IEnumerable<ValidationResult> Validate(ValidationContext context) =>
        throw new InvalidOperationException("Validate this object with ValidateAsync.");

    // Cross-property async rule: the framework runs these after member
    // attributes, so Email is guaranteed to be set by the time this runs.
    public async IAsyncEnumerable<ValidationResult> ValidateAsync(ValidationContext context,
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await Task.Delay(10, cancellationToken);

        if (Username == Email)
        {
            yield return new ValidationResult("Username must not match Email.",
                new[] { nameof(Username) });
        }
    }
}
