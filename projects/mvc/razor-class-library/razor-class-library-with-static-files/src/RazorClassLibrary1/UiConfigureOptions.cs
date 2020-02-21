using RazorClassLibraries.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace RazorClassLibrary1
{
    public sealed class UiConfigureOptions : BaseModuleUiConfigureOptions
    {
        public UiConfigureOptions(IWebHostEnvironment environment)
            : base(environment)
        {
        }
    }
}