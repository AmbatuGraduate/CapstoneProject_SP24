using Domain.Entities.BucketTruck;
using Domain.Entities.ScheduleCleanSidewalk_street_map;
using Domain.Entities.Street;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class ScheduleCleanSidewalk_street_mapEntityConfiguration : IEntityTypeConfiguration<ScheduleCleanSidewalk_street_maps>
    {
        public void Configure(EntityTypeBuilder<ScheduleCleanSidewalk_street_maps> builder)
        {
            ConfigurationScheduleCleanSidewalk_street_mapsTable(builder);
        }

        void ConfigurationScheduleCleanSidewalk_street_mapsTable(EntityTypeBuilder<ScheduleCleanSidewalk_street_maps> builder)
        {
            builder.ToTable("ScheduleCleanSidewalk_street_maps");
            builder.HasKey(scheduleCleanSidewalk_street_maps => new { scheduleCleanSidewalk_street_maps.StreetId, scheduleCleanSidewalk_street_maps.ScheduleCleanSidewalksId});

            builder.Property(bucketTruck => bucketTruck.CreateBy)
                .HasMaxLength(50);
            builder.Property(bucketTruck => bucketTruck.UpdateBy)
            .HasMaxLength(50);

            builder.HasData(new ScheduleCleanSidewalk_street_maps { 
                StreetId = Guid.Parse("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), 
                ScheduleCleanSidewalksId = Guid.Parse("7a866c85-b013-4fab-80c7-15d21d0c686c"), 
                CreateDate = DateTime.Now, 
                CreateBy = "Admin", 
                UpdateBy = "Admin", 
                UpdateDate = DateTime.Now 
            });
        }
    }
}
