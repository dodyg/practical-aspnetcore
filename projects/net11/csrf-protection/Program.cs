using Microsoft.AspNetCore.Mvc;

var app = WebApplication.Create(args);

app.MapPost("/contact", ([FromForm] ContactForm form) => Results.Ok($"Thanks, {form.Name}! Your message was received."));

// Opt out on a specific endpoint.
app.MapPost("/contact-unprotected", ([FromForm] ContactForm form) =>
        Results.Ok($"Thanks, {form.Name}! (this endpoint opted out)"))
   .DisableAntiforgery();

app.MapGet("/", () => Results.Content("""
    <html>
    <head>
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        <style>
            .curl-command {
                display: flex;
                align-items: flex-start;
                gap: 0.5rem;
            }

            .curl-command pre {
                flex: 1;
            }

            .copy-button {
                width: auto;
                margin: 0;
                padding: 0.25rem 0.5rem;
                font-size: 0.75rem;
            }
        </style>
        <script>
            async function copyCurlCommand(button) {
                const command = button.previousElementSibling.textContent.trim();
                const originalText = button.textContent;

                try {
                    await navigator.clipboard.writeText(command);
                    button.textContent = "Copied!";
                } catch {
                    button.textContent = "Copy failed";
                }

                setTimeout(() => button.textContent = originalText, 1500);
            }
        </script>
    </head>
    <body class="container">
        <h1>Automatic cross-origin (CSRF) protection</h1>
        <form method="post" action="/contact" enctype="application/x-www-form-urlencoded">
            <label>Name: <input name="name" value="Ada" /></label><br/>
            <label>Message: <input name="message" value="Hello from a same-origin form" /></label><br/>
            <button type="submit">Submit (same-origin allowed)</button>
        </form>
        <h2>Test with curl</h2>
        <p>Run these commands while the app is running. The protected endpoint accepts a normal request, but rejects one that claims to be cross-site.</p>
        <p>Normal request: HTTP 200</p>
        <div class="curl-command">
            <pre><code>curl -i -X POST http://localhost:5000/contact -d "name=X" -d "message=Hello from curl"</code></pre>
            <button class="copy-button" type="button" onclick="copyCurlCommand(this)">Copy</button>
        </div>
        <p>Simulated cross-site request: HTTP 400</p>
        <div class="curl-command">
            <pre><code>curl -i -X POST http://localhost:5000/contact -d "name=X" -d "message=Hello from curl" -H "Sec-Fetch-Site: cross-site" -H "Origin: https://evil.example"</code></pre>
            <button class="copy-button" type="button" onclick="copyCurlCommand(this)">Copy</button>
        </div>
        <p>Same-origin request: HTTP 200</p>
        <div class="curl-command">
            <pre><code>curl -i -X POST http://localhost:5000/contact -d "name=X" -d "message=Hello from curl" -H "Sec-Fetch-Site: same-origin"</code></pre>
            <button class="copy-button" type="button" onclick="copyCurlCommand(this)">Copy</button>
        </div>
        <p>Endpoint that opted out of antiforgery validation: HTTP 200, even cross-site</p>
        <div class="curl-command">
            <pre><code>curl -i -X POST http://localhost:5000/contact-unprotected -d "name=X" -d "message=Hello from curl" -H "Sec-Fetch-Site: cross-site" -H "Origin: https://evil.example"</code></pre>
            <button class="copy-button" type="button" onclick="copyCurlCommand(this)">Copy</button>
        </div>
    </body></html>
    """, "text/html")).ExcludeFromDescription();

app.Run();

public record ContactForm(string Name, string Message);
