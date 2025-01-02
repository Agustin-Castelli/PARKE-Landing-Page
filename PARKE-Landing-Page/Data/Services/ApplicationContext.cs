using Microsoft.EntityFrameworkCore;
using PARKE_Landing_Page.Models.Entities;

namespace PARKE_Landing_Page.Data.Services
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<New> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}