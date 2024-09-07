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
    }
}
