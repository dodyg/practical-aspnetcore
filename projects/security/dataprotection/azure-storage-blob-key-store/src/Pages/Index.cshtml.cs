using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace PANC.DataProtection.AzStorageBlobKeyStore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IDataProtector _protector;
        private readonly ILogger<IndexModel> _logger;

        
        public string EncryptedString { get; private set; }
        public string DeCryptedString { get; private set; }
        public IndexModel(ILogger<IndexModel> logger, IDataProtectionProvider dataProtectionProvider)
        {
            _protector = dataProtectionProvider.CreateProtector("DP.DefaultSettings.V1");
            _logger = logger;
        }

        public void OnGet()
        {

        }

        [Required(ErrorMessage = "Enter string to encrypt.")]
        [BindProperty]
        public string StringToEncrypt { get; set; }
        public void OnPost()
        {
            EncryptedString = _protector.Protect(StringToEncrypt);
            DeCryptedString = _protector.Unprotect(EncryptedString);
        }
    }
}
