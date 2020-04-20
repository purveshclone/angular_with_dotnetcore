using Microsoft.EntityFrameworkCore;
using Testfirst.API.Models;

namespace Testfirst.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) {}

        public DbSet<Value> MyProperty  { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}