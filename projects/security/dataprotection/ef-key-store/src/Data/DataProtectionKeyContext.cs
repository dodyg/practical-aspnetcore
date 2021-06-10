using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PANC.DataProtection.EFCoreKeyStore.Data
{
    class DataProtectionKeyContext : DbContext, IDataProtectionKeyContext
    {
        public DataProtectionKeyContext(DbContextOptions<DataProtectionKeyContext> options)
            : base(options){ }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    }
}
