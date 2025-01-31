using Microsoft.EntityFrameworkCore;
using PARKE_Landing_Page.Models.Entities;

namespace PARKE_Landing_Page.Data.Services
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<New> News { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientDetail> ClientDetails { get; set; }

        public DbSet<Machine> Machines { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientDetail>()
                .HasKey(cd => new { cd.ClientId, cd.MachineId });
            modelBuilder.Entity<ClientDetail>()
                .HasOne(cd => cd.Client)
                .WithMany(c => c.ClientDetails)
                .HasForeignKey(cd => cd.ClientId);
            modelBuilder.Entity<ClientDetail>()
                .HasOne(cd => cd.Machine)
                .WithMany(m => m.ClientDetails )
                .HasForeignKey(cd => cd.MachineId);
            
        }
    }
}