using Microsoft.EntityFrameworkCore;
using OrdersApi.Models;

namespace OrdersApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders => Set<Order>();
    }

    public class ReadAppDbContext : DbContext
    {
        public ReadAppDbContext(DbContextOptions<ReadAppDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders => Set<Order>();
    }

    public class WriteAppDbContext : DbContext
    {
        public WriteAppDbContext(DbContextOptions<WriteAppDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders => Set<Order>();
    }
}
