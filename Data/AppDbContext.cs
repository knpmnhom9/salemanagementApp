using Microsoft.EntityFrameworkCore;
using salemanagementApp.Models;

namespace salemanagementApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=SaleManagementDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
    }
}
