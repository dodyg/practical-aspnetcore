  using Microsoft.EntityFrameworkCore;
  using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
  
  public class ApplicationDBContext:IdentityDbContext<ApplicationUser>
  {
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
     : base(options)
    {
    }
  }