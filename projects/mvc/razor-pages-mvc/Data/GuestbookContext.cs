using Microsoft.EntityFrameworkCore;

namespace RazorPagesMvc.Data
{
    public class GuestbookContext : DbContext
    {
        public GuestbookContext(DbContextOptions options) : base(options) { }

        public DbSet<Entry> Entries { get; set; }
    }
}
