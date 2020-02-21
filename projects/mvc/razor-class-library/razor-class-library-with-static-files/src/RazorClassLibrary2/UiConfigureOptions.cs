using Microsoft.AspNetCore.Hosting;
using RazorClassLibraries.Mvc;

namespace RazorClassLibrary2
{
    public sealed class UiConfigureOptions : BaseModuleUiConfigureOptions
    {
        public UiConfigureOptions(IWebHostEnvironment environment)
            : base(environment)
        {
        }
    }
}