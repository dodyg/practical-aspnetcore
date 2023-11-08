# Accessing WASM Pages in mixed environment host

`.AddAdditionalAssemblies` allows the host project to discover Page components in a WASM project. 

``` csharp
app.MapRazorComponents<RazorComponentThirteen.App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Wasm.Pages.Index).Assembly);
`

The Web Assembly components **need to be hosted in a separate project**. 