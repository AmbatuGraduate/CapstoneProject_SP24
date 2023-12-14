using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class TreeDbContext : DbContext
    {
        public TreeDbContext(DbContextOptions<TreeDbContext> options) : base(options)
        {
        }

        public DbSet<Tree> Trees { get; set; }
    }
}
