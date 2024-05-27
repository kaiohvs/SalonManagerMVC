using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalonManager.Domain.Entities;
namespace SalonManager.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}


