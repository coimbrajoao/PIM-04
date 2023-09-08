using Course.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Course.Data
{
    public class FolhaContext : IdentityDbContext<User,IdentityRole<int>, int>
    {
        public FolhaContext(DbContextOptions<FolhaContext> opts) : base(opts) { }

       

        public DbSet<User> Users { get; set; }
        
        public DbSet<Payroll> Payrolls { get; set; }

        public DbSet<TimeClock> TimeClocks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Remover o prefixo "AspNet" das tabelas
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole<int>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
            modelBuilder.Entity<TimeClock>().ToTable("TimeClock");
            // Definir outros relacionamentos ou configurações do modelo, se necessário
        }


    }


}
