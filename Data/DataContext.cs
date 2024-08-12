using Akij_Bashir_It.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Akij_Bashir_It.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options):base(options)
        {
   
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProduct>()
                .HasKey(pc => new { pc.UserId, pc.ProductId });

            modelBuilder.Entity<UserProduct>()
                .HasOne(p => p.User)
                .WithMany(pc => pc.UserProducts)
                .HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<UserProduct>()
               .HasOne(p => p.Product)
               .WithMany(pc => pc.UserProducts)
               .HasForeignKey(c => c.UserId);
        }
    }
}
