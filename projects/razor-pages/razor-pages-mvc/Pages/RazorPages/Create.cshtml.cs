using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracticalAspNetCore.Data;

namespace PracticalAspNetCore.Pages
{
    public class CreateModel : PageModel
    {
        private readonly GuestbookContext _db;

        public CreateModel(GuestbookContext db) => _db = db;

        [BindProperty]
        public Entry Entry { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _db.Entries.Add(Entry);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
