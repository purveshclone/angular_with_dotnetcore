using Microsoft.EntityFrameworkCore;
using Testfirst.API.Models;

namespace Testfirst.API.Models.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) {}

        public DbSet<Value> MyProperty { get; set; }
    }
}