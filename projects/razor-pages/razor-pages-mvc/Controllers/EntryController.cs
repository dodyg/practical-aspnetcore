using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticalAspNetCore.Data;

namespace PracticalAspNetCore.Controllers
{
    [Route("Mvc")]
    public class EntryController : Controller
    {
        private GuestbookContext _db;

        public EntryController(GuestbookContext db) => _db = db;

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var entries = await _db.Entries.AsNoTracking().ToListAsync();
            return View(entries);
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(Entry entry)
        {
            if (!ModelState.IsValid) return View("Edit", entry);

            _db.Attach(entry).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Route("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var entry = await _db.Entries.SingleOrDefaultAsync(e => e.Id == id);
            return View(entry);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Entry entry)
        {
            if (!ModelState.IsValid) return View(entry);

            await _db.Entries.AddAsync(entry);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Route("Create")]
        public IActionResult Create() => View();

        [HttpPost]
        [Route("Like")]
        public async Task<IActionResult> Like(int id)
        {
            var entry = await _db.Entries.SingleOrDefaultAsync(e => e.Id == id);
            entry.Likes += 1;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
