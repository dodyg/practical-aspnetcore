using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Entity> Entities { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=mydb;Trusted_Connection=True;MultipleActiveResultSets=True");
    }
}