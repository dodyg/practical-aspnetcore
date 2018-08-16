using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMvc.Data;
using Microsoft.EntityFrameworkCore;

namespace RazorPagesMvcCrud.Pages
{
    public class EditModel : PageModel
    {
        private readonly GuestbookContext _db;

        public EditModel(GuestbookContext db) => _db = db;

        [BindProperty]
        public Entry Entry { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Entry = await _db.Entries.FindAsync(id);

            if (Entry == null) return RedirectToPage("/Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _db.Attach(Entry).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
