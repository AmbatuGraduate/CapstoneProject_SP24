using Domain.Entities.GarbageTruckType;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class GarbageTruckTypeEntityConfiguration : IEntityTypeConfiguration<GarbageTruckTypes>
    {
        public void Configure(EntityTypeBuilder<GarbageTruckTypes> builder)
        {
            ConfigurationGarbageTruckTypesTable(builder);
        }

        private void ConfigurationGarbageTruckTypesTable(EntityTypeBuilder<GarbageTruckTypes> builder)
        {
            builder.ToTable("GarbageTruckTypes")
                   .HasKey(garbageTruckTypes => garbageTruckTypes.GarbageTruckTypeId);

            builder.Property(garbageTruckTypes => garbageTruckTypes.GarbageTruckTypeName)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(garbageTruckTypes => garbageTruckTypes.CreateBy)
                   .HasMaxLength(50);
            builder.Property(garbageTruckTypes => garbageTruckTypes.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new GarbageTruckTypes { GarbageTruckTypeId = Guid.Parse("12e42a48-f991-4733-bd7c-2e536f931b22"), GarbageTruckTypeName = "Xe thu gom rac nho", CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            builder.HasData(new GarbageTruckTypes { GarbageTruckTypeId = Guid.Parse("12e42a48-f991-4733-bd7c-2e536f921b22"), GarbageTruckTypeName = "Xe thu gom rac to", CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
        }
    }
}