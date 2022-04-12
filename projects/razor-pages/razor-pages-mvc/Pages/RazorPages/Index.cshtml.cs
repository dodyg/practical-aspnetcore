using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracticalAspNetCore.Data;
using Microsoft.EntityFrameworkCore;

namespace PracticalAspNetCore.Pages
{
    public class IndexRazorPagesModel : PageModel
    {
        private readonly GuestbookContext _db;

        public IndexRazorPagesModel(GuestbookContext db) => _db = db;

        public IList<Entry> Entries { get; private set; }

        public async Task OnGetAsync()
        {
            Entries = await _db.Entries.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostLikeAsync(int id)
        {
            var entry = await _db.Entries.FindAsync(id);
            entry.Likes += 1;
            await _db.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var entry = await _db.Entries.FindAsync(id);
            _db.Entries.Remove(entry);
            await _db.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
