namespace RazorClassLibrary2
{
    using Microsoft.AspNetCore.Hosting;
    using RazorClassLibraries.Mvc;

    public sealed class UiConfigureOptions : BaseModuleUiConfigureOptions
    {
        public UiConfigureOptions(IHostingEnvironment environment)
            : base(environment)
        {
        }
    }
}