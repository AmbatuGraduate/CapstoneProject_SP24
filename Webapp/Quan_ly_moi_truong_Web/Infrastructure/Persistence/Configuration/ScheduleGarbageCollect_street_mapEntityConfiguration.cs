using Domain.Entities.ScheduleCleanSidewalk_street_map;
using Domain.Entities.ScheduleGarbageCollect_street_map;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class ScheduleGarbageCollect_street_mapEntityConfiguration : IEntityTypeConfiguration<ScheduleGarbageCollect_street_maps>
    {
        public void Configure(EntityTypeBuilder<ScheduleGarbageCollect_street_maps> builder)
        {
            ConfigurationScheduleGarbageCollect_street_mapsTable(builder);
        }

        void ConfigurationScheduleGarbageCollect_street_mapsTable(EntityTypeBuilder<ScheduleGarbageCollect_street_maps> builder)
        {
            builder.ToTable("ScheduleGarbageCollect_street_maps");
            builder.HasKey(scheduleGarbageCollect_street_maps => new { scheduleGarbageCollect_street_maps.StreetId, scheduleGarbageCollect_street_maps.ScheduleGarbageCollectId });

            builder.Property(bucketTruck => bucketTruck.CreateBy)
                .HasMaxLength(50);
            builder.Property(bucketTruck => bucketTruck.UpdateBy)
            .HasMaxLength(50);

            builder.HasData(new ScheduleGarbageCollect_street_maps
            {
                StreetId = Guid.Parse("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"),
                ScheduleGarbageCollectId = Guid.Parse("26397b2b-ca94-4af4-bf0d-f7aaa7510698"),
                CreateDate = DateTime.Now,
                CreateBy = "Admin",
                UpdateBy = "Admin",
                UpdateDate = DateTime.Now
            });

            builder.HasData(new ScheduleGarbageCollect_street_maps
            {
                StreetId = Guid.Parse("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"),
                ScheduleGarbageCollectId = Guid.Parse("e3c19a06-7f84-4c4d-8d83-71264a5cf176"),
                CreateDate = DateTime.Now,
                CreateBy = "Admin",
                UpdateBy = "Admin",
                UpdateDate = DateTime.Now
            });
        }
    }
}
