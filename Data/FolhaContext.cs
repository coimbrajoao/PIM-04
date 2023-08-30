using Course.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Course.Data
{
    public class FolhaContext : IdentityDbContext<User>
    {
        public FolhaContext(DbContextOptions<FolhaContext> opts) : base(opts) { }

       

        public DbSet<User> Users { get; set; }
        

    }


}
