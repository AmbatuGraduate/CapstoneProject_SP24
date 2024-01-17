using Domain.Entities.BucketTruck;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class BucketTruckEntityConfiguration : IEntityTypeConfiguration<BucketTrucks>
    {
        public void Configure(EntityTypeBuilder<BucketTrucks> builder)
        {
            ConfigurationBucketTrucksTable(builder);
        }

        void ConfigurationBucketTrucksTable(EntityTypeBuilder<BucketTrucks> builder)
        {
            builder.ToTable("Bucket Trucks");
            builder.HasKey(bucketTruck => bucketTruck.BucketTruckId);

            builder.Property(bucketTruck => bucketTruck.BucketTruckLicensePlates)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(bucketTruck => bucketTruck.CreateBy)
                .HasMaxLength(50);
            builder.Property(bucketTruck => bucketTruck.UpdateBy)
                .HasMaxLength(50);

            builder.HasData(new BucketTrucks {
                BucketTruckId = Guid.Parse("f9257e9f-6d30-45fd-8afc-3e3266d7adc6"), 
                BucketTruckLicensePlates= "123123123Aa", 
                CraneArmLength= 12, 
                CreateDate= DateTime.Now, 
                CreateBy= "Admin", 
                UpdateBy = "Admin", 
                UpdateDate= DateTime.Now 
            });
            builder.HasData(new BucketTrucks { 
                BucketTruckId = Guid.Parse("f9257e9f-6d31-45fd-8afc-3e3266d7adc6"), 
                BucketTruckLicensePlates = "123123123Aa", 
                CraneArmLength = 12, 
                CreateDate = DateTime.Now, 
                CreateBy = "Admin", 
                UpdateBy = "Admin", 
                UpdateDate = DateTime.Now 
            });
        }
    }
}
