using Domain.Entities.Street;
using Domain.Entities.Ward;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class WardEntityConfiguration : IEntityTypeConfiguration<Wards>
    {
        public void Configure(EntityTypeBuilder<Wards> builder)
        {
            ConfigurationWardsTable(builder);
        }

        private void ConfigurationWardsTable(EntityTypeBuilder<Wards> builder)
        {
            builder.ToTable("Wards")
                   .HasKey(ward => ward.WardId);

            builder.Property(ward => ward.WardName)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasData(new Wards
            {
                WardId = Guid.Parse("996c63bc-5f0a-44f6-8c9a-aad741b3beac"),
                WardName = "Hoa Hai",
                DistrictId = Guid.Parse("be7d62da-53ea-46b0-b294-bb109eca92fc")
            });

            builder.HasData(new Wards
            {
                WardId = Guid.Parse("faa64719-904b-4844-9ec9-d8f2620ffb51"),
                WardName = "Khue My",
                DistrictId = Guid.Parse("be7d62da-53ea-46b0-b294-bb109eca92fc")
            });

            builder.HasData(new Wards
            {
                WardId = Guid.Parse("79bea4b4-23ce-41ad-b585-4dfc835d607a"),
                WardName = "Hoa Quy",
                DistrictId = Guid.Parse("be7d62da-53ea-46b0-b294-bb109eca92fc")
            });

            builder.HasData(new Wards
            {
                WardId = Guid.Parse("38af1dbf-b83f-4899-8389-743021c463a0"),
                WardName = "My An",
                DistrictId = Guid.Parse("be7d62da-53ea-46b0-b294-bb109eca92fc")
            });

            builder.HasData(new Wards
            {
                WardId = Guid.Parse("c088acde-18ea-48ca-ae03-bdd4e610e039"),
                WardName = "Hai Chau I",
                DistrictId = Guid.Parse("be7d62da-53ea-46b0-b294-bb109eca92fc")
            });

            builder.HasData(new Wards
            {
                WardId = Guid.Parse("3097108f-15fe-4ac8-aab4-187b56841c81"),
                WardName = "Hai Chau II",
                DistrictId = Guid.Parse("be7d62da-53ea-46b0-b294-bb109eca92fc")
            });

            builder.HasData(new Wards
            {
                WardId = Guid.Parse("f4e93702-9dc2-4288-8f23-4c3812ed50cc"),
                WardName = "Thuan Phuoc",
                DistrictId = Guid.Parse("be7d62da-53ea-46b0-b294-bb109eca92fc")
            });
        }
    }
}
