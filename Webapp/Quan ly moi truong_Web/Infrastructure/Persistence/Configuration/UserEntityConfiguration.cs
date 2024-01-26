using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.Property(user => user.UserCode)
                   .IsRequired();
            builder.Property(user => user.Name)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(user => user.Address)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(user => user.PhoneNumber)
                   .HasMaxLength(11)
                   .IsRequired();

            builder.HasData(new Users
            {
                Id = Guid.Parse("56b77536-6c85-4e7d-910b-964e906c7cf2"),
                UserCode = "admin",
                Name = "Admin",
                Address = "Admin",
                PhoneNumber = "0947346127",
                Password = "123123Aa!",
                RoleId = Guid.Parse("abccde85-c7dc-4f78-9e4e-b1b3e7abee84"),
                DepartmentId = Guid.Parse("bc2f24de-2b9b-489a-a108-64a114d2b9be"),
                Image = "string"
            });

            builder.HasData(new Users
            {
                Id = Guid.Parse("b2b1e0ce-0187-4285-8cce-60fdff665f46"),
                UserCode = "NHS_HH_NKKN_123",
                Name = "Nguyen Van A",
                Address = "30 Nam Ky Khoi Nghia",
                PhoneNumber = "0947123244",
                Password = "123123Aa!",
                RoleId = Guid.Parse("8977ef77-e554-4ef3-8353-3e01161f84d0"),
                DepartmentId = Guid.Parse("bc2f24de-2b9b-489a-a108-64a114d2b9be"),
                Image = "string"
            });

            builder.HasData(new Users
            {
                Id = Guid.Parse("b2b1e0ce-0187-4285-8cce-60fdff666f46"),
                UserCode = "NHS_HH_NKKN_456",
                Name = "Nguyen Van B",
                Address = "45 Huynh Lam",
                PhoneNumber = "0947133244",
                Password = "123123Aa!",
                RoleId = Guid.Parse("8977ef77-e554-4ef3-8353-3e01161f84d0"),
                DepartmentId = Guid.Parse("bc2f24de-2b9b-429a-a108-64a114d2b9be"),
                Image = "string"
            });
        }
    }
}