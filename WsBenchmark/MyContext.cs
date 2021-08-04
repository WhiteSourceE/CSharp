using Microsoft.EntityFrameworkCore;

namespace WsBenchmark
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {
        }

        protected MyContext()
        {
        }
        
        public DbSet<string> Name { get; set; }
    }
}