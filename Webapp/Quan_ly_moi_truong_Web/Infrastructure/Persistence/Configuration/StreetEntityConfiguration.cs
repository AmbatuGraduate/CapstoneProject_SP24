using Domain.Entities.Street;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class StreetEntityConfiguration : IEntityTypeConfiguration<Streets>
    {
        public void Configure(EntityTypeBuilder<Streets> builder)
        {
            ConfigurationStreetstable(builder);
        }

        private void ConfigurationStreetstable(EntityTypeBuilder<Streets> builder)
        {
            builder.ToTable("Streets")
                   .HasKey(street => street.StreetId);

            builder.Property(street => street.StreetName)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(street => street.StreetLength)
                   .IsRequired();
            builder.Property(street => street.NumberOfHouses)
                   .IsRequired();

            builder.Property(street => street.CreateBy)
                   .HasMaxLength(50);
            builder.Property(street => street.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new Streets
            {
                StreetId = Guid.Parse("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"),
                StreetTypeId = Guid.Parse("1be73957-b7e9-4304-9242-00e8b92a86f0"),
                StreetName = "Duong Huynh Lam",
                StreetLength = 10000,
                NumberOfHouses = 20,
                ResidentialGroupId = Guid.Parse("0a0e931d-d055-48a9-b8a4-2cf57ac2f6f5"),
                CreateDate = DateTime.Now,
                CreateBy = "Admin",
                UpdateBy = "Admin",
                UpdateDate = DateTime.Now
            });
        }
    }
}