using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Channel-binding tokens are only available over TLS.
builder.WebHost.UseUrls("https://localhost:5001");

var app = builder.Build();

// Read the TLS channel-binding token (CBT) and expose a hash of it so you can
// see it change per connection. Use the token to defend against relay attacks.
app.Use(async (context, next) =>
{
    var tls = context.Features.Get<ITlsConnectionFeature>();
    if (tls is not null)
    {
        // Which binding kinds a TLS session supports depends on the stack
        // (TLS version, cipher, OS). Try the standard kinds in order.
        foreach (var kind in new[] { ChannelBindingKind.Endpoint, ChannelBindingKind.Unique })
        {
            if (tls.TryGetChannelBindingBytes(kind, out var cbt))
            {
                var hash = Convert.ToHexString(SHA256.HashData(cbt.Span));
                context.Response.Headers["X-Channel-Binding-Kind"] = kind.ToString();
                context.Response.Headers["X-Channel-Binding-Hash"] = hash;
                context.Response.Headers["X-Channel-Binding-Length"] = cbt.Length.ToString();
                break;
            }
        }

        if (!context.Response.Headers.ContainsKey("X-Channel-Binding-Hash"))
        {
            context.Response.Headers["X-Channel-Binding"] = "unavailable for this TLS session";
        }
    }

    await next(context);
});

app.MapGet("/", () => Results.Text("""
    <html><body>
        <h1>TLS channel-binding token</h1>
        <p>
            Every HTTPS request to this server carries
            <code>X-Channel-Binding-Kind</code>, <code>X-Channel-Binding-Hash</code>
            (SHA-256 of the CBT) and <code>X-Channel-Binding-Length</code> headers.
        </p>
        <p>
            <code>curl -sk https://localhost:5001/ -i | grep -i channel</code>
        </p>
        <p>The hash is unique per TLS connection (a fresh connection gets a new value).</p>
    </body></html>
    """, "text/html")).ExcludeFromDescription();

app.Run();
