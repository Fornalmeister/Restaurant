using Microsoft.EntityFrameworkCore;
using Services.Product.Models;

namespace Services.Product.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Products>().HasData(new Products
            {
                ProductId = 1,
                Name = "Kotlet Schabowy",
                Price = 20,
                Description = "Najlepszy kotlet schabowy na swiecie.",
                Category = "Mieso"
            });

            modelBuilder.Entity<Products>().HasData(new Products
            {
                ProductId = 2,
                Name = "Kurczak słodko-kwasny",
                Price = 20,
                Description = "Najlepszy kurczak z ryzem.",
                Category = "Mieso"
            });
        }
    }
}
