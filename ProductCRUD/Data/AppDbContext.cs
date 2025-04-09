using Microsoft.EntityFrameworkCore;
using ProductCRUD.Models;

namespace ProductCRUD.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }

    }
}
