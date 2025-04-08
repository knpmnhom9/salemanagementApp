using salemanagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace salemanagementApp.Data
{
x    public class AppDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=localhost;Database=TestDB;User Id=sa;Password=123456;");
        }
    }
}