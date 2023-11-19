using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    /// <summary>
    /// Config the property of table when creating in database
    /// </summary>
    public class UserEntityConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            ConfigureUsersTable(builder);
        }

        private void ConfigureUsersTable(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(user => user.Id);

            // This one is for aggregate root if have
            //builder.Property(user => user.Id)
            //    .ValueGeneratedNeve()
            //    .HasConversion(
            //        id => id.Value,
            //        value => DefaultUserIdProvider.Create(value)
            //    );

            builder.Property(user => user.Name)
                .HasMaxLength(50);
            builder.Property(user => user.Address)
                .HasMaxLength(50);
            builder.Property(user => user.PhoneNumber)
                .HasMaxLength(11);
        }
    }
}
