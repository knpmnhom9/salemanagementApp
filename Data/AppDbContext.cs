using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using salemanagementApp.Models;

namespace salemanagementApp.Data
{
    public class AnotherAppDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=HUY-HOANG22;Database=YourDatabaseName;Trusted_Connection=True;");
            }
        }
    }
}
