using Domain.Entities.ScheduleGarbageCollect;
using Domain.Entities.StreetType;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class StreetTypeEntityConfiguration : IEntityTypeConfiguration<StreetTypes>
    {
        public void Configure(EntityTypeBuilder<StreetTypes> builder)
        {
            ConfigurationStreetTypesTable(builder);
        }

        public void ConfigurationStreetTypesTable(EntityTypeBuilder<StreetTypes> builder)
        {
            builder.ToTable("StreetTypes")
                   .HasKey(streetType => streetType.StreetTypeId);

            builder.Property(streetType => streetType.CreateBy)
                   .HasMaxLength(50);
            builder.Property(streetType => streetType.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new StreetTypes
            {
                StreetTypeId = Guid.Parse("1be73957-b7e9-4304-9242-00e8b92a86f0"),
                StreetTypeName = "Duong Kinh Doanh",
                CreateDate = DateTime.Now,
                CreateBy = "Admin",
                UpdateBy = "Admin",
                UpdateDate = DateTime.Now
            });

            builder.HasData(new StreetTypes
            {
                StreetTypeId = Guid.Parse("e3d44b7e-8ebe-434f-88ef-054a81951be1"),
                StreetTypeName = "Duong Dan Sinh",
                CreateDate = DateTime.Now,
                CreateBy = "Admin",
                UpdateBy = "Admin",
                UpdateDate = DateTime.Now
            });
        }
    }
}
