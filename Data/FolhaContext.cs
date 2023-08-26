using Course.Models;
using Microsoft.EntityFrameworkCore;

namespace Course.Data
{
    public class FolhaContext : DbContext
    {
        public FolhaContext(DbContextOptions<FolhaContext> opts) : base(opts) { }

        public DbSet<User> Users { get; set; }

    }


}
