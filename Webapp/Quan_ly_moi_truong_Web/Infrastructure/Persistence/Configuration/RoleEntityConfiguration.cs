using Domain.Entities.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            ConfigurationRolesTable(builder);
        }

        private void ConfigurationRolesTable(EntityTypeBuilder<Roles> builder) 
        {
            builder.ToTable("Roles")
                   .HasKey(role => role.RoleId);

            builder.Property(role => role.RoleName)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(role => role.CreateBy)
                   .HasMaxLength(50);
            builder.Property(role => role.UpdateBy)
                   .HasMaxLength(50);

            //For testing
            builder.HasData(new Roles { RoleId = Guid.Parse("abccde85-c7dc-4f78-9e4e-b1b3e7abee84"), RoleName = "Manager", CreateDate = DateTime.Now, CreateBy = "Admin", UpdateDate = DateTime.Now, UpdateBy = "Admin" });
            builder.HasData(new Roles { RoleId = Guid.Parse("cacd4b3a-8afe-43e9-b757-f57f5c61f8d8"), RoleName = "Leader", CreateDate = DateTime.Now, CreateBy = "Admin", UpdateDate = DateTime.Now, UpdateBy = "Admin" });
            builder.HasData(new Roles { RoleId = Guid.Parse("8977ef77-e554-4ef3-8353-3e01161f84d0"), RoleName = "Employee", CreateDate = DateTime.Now, CreateBy = "Admin", UpdateDate = DateTime.Now, UpdateBy = "Admin" });
        }
    }
}
