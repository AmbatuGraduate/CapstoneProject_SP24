using Domain.Entities;
using Infrastructure.Persistence.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class WebDbContext : DbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> opts) : base(opts) { }

        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Trees> Trees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ignore create table IdentityUser
            modelBuilder.Ignore<IdentityUser<Guid>>();

            // Config primary key is not auto generate
            modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties())
                .Where(key => key.IsPrimaryKey())
                .ToList()
                .ForEach(e => e.ValueGenerated = ValueGenerated.Never);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebDbContext).Assembly);
        }
    }
}
