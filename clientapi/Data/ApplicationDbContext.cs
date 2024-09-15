using clientapi.Models;
using Microsoft.EntityFrameworkCore;

namespace clientapi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> client { get; set; }

        public DbSet<ServiceDetails> servicehistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure primary key for ServiceDetails
            modelBuilder.Entity<ServiceDetails>()
                .HasKey(s => s.servicehistoryid);

            // Configure any other model configurations here
        }

    }
}
