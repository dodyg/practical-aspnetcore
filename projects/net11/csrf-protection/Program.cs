using Microsoft.AspNetCore.Mvc;

var app = WebApplication.Create(args);

// Form-binding endpoints are automatically protected (Preview 7): the CSRF
// middleware validates only endpoints with
// IAntiforgeryMetadata { RequiresValidation: true }, which form binding adds.
// A cross-origin browser request (Sec-Fetch-Site: cross-site / fake Origin)
// is rejected with 403; same-origin requests pass.
app.MapPost("/contact", ([FromForm] ContactForm form) =>
        Results.Ok($"Thanks, {form.Name}! Your message was received."));

// Opt out on a specific endpoint.
app.MapPost("/contact-unprotected", ([FromForm] ContactForm form) =>
        Results.Ok($"Thanks, {form.Name}! (this endpoint opted out)"))
   .DisableAntiforgery();

app.MapGet("/", () => Results.Content("""
    <html><body>
        <h1>Automatic cross-origin (CSRF) protection</h1>
        <form method="post" action="/contact" enctype="application/x-www-form-urlencoded">
            <label>Name: <input name="name" value="Ada" /></label><br/>
            <label>Message: <input name="message" value="Hello from a same-origin form" /></label><br/>
            <button type="submit">Submit (same-origin — allowed)</button>
        </form>
        <p>
            Try simulating a cross-site request:
            <code>curl -X POST http://localhost:5000/contact -d "name=X" -H "Sec-Fetch-Site: cross-site"</code>
            → 403. Repeat without the header (or with <code>same-origin</code>) → 200.
        </p>
    </body></html>
    """, "text/html")).ExcludeFromDescription();

app.Run();

public record ContactForm(string Name, string Message);
