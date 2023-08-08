using Microsoft.EntityFrameworkCore;
using Ssl.Certificate.Data.Models;

namespace Ssl.Certificate.Data
{
    public class MonitorDbContext : DbContext
    {
        public MonitorDbContext(DbContextOptions<MonitorDbContext> options)
        : base(options)
        { 
        }

        public DbSet<SslControlTable> SslControlTable { get; set; }
        public DbSet<SslActivityLog> SslActivityLog { get; set; }
    }
}