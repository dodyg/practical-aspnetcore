# TLS channel-binding token from `ITlsConnectionFeature`

Preview 7 exposes the TLS channel-binding token (CBT) via
`ITlsConnectionFeature.TryGetChannelBindingBytes`. A CBT is a per-connection
cryptographic binding between a TLS session and higher-level authentication
(e.g. HTTP authentication); it can be used to defend against relay attacks.

```csharp
using System.Security.Authentication.ExtendedProtection;

var tls = context.Features.Get<ITlsConnectionFeature>();
if (tls is not null && tls.TryGetChannelBindingBytes(ChannelBindingKind.Endpoint, out var cbt))
{
    // cbt is a ReadOnlyMemory<byte> — hash it, sign it, include it in
    // an authentication token, etc.
}
```

Which binding kinds a TLS session exposes depends on the stack (TLS version,
cipher, OS). This sample tries `ChannelBindingKind.Endpoint` first and falls
back to `ChannelBindingKind.Unique`.

This sample hashes the CBT and returns it in
`X-Channel-Binding-Kind` / `X-Channel-Binding-Hash` /
`X-Channel-Binding-Length` response headers:

```bash
dotnet dev-certs https   # first time only
dotnet watch run         # Development environment; binds https://localhost:5001
curl -sk https://localhost:5001/ -i | grep -i channel
```

Notes:

- The app must use HTTPS — Kestrel returns the binding from
  `SslStream.TransportContext.GetChannelBinding`.
- IIS / HTTP.sys have their own story, controlled by
  `HttpSysOptions.HttpAuthenticationHardeningLevel`
  (`Legacy` / `Medium` / `Strict`).

See the [Preview 7 release notes](https://github.com/dotnet/core/blob/main/release-notes/11.0/preview/preview7/aspnetcore.md#tls-channel-binding-token-access-from-itlsconnectionfeature).
