using Domain.Entities.GarbageTruck;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class GarbageTruckEntityConfiguration : IEntityTypeConfiguration<GarbageTrucks>
    {
        public void Configure(EntityTypeBuilder<GarbageTrucks> builder)
        {
            ConfigurationGarbageTrucksTable(builder);
        }

        private void ConfigurationGarbageTrucksTable(EntityTypeBuilder<GarbageTrucks> builder)
        {
            builder.ToTable("GarbageTrucks")
                   .HasKey(garbageTruck => garbageTruck.GarbageTruckId);

            builder.Property(garbageTruck => garbageTruck.GarbageTruckLicensePlates)
                   .IsRequired();
            builder.Property(garbageTruck => garbageTruck.CreateBy)
                   .HasMaxLength(50);
            builder.Property(garbageTruck => garbageTruck.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new GarbageTrucks { GarbageTruckId = Guid.Parse("fc34e805-4550-4037-a273-17a0b1639bbe"), GarbageTruckLicensePlates = "123123Aa", GarbageTruckWeight = 450, GarbageTruckTypeId = Guid.Parse("12e42a48-f991-4733-bd7c-2e536f931b22"), GarbageDumpId = Guid.Parse("be5d01ee-b15c-4ced-aa0c-165c47dac9f9"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            builder.HasData(new GarbageTrucks { GarbageTruckId = Guid.Parse("fc34e805-4550-4037-a273-17a0b1639bbc"), GarbageTruckLicensePlates = "123456Aa", GarbageTruckWeight = 450, GarbageTruckTypeId = Guid.Parse("12e42a48-f991-4733-bd7c-2e536f931b22"), GarbageDumpId = Guid.Parse("be5d01ee-b15c-4ced-aa0c-165c47dac9f9"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
        }
    }
}