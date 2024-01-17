using Domain.Entities.ScheduleCleanSidewalk_street_map;
using Domain.Entities.ScheduleTreeTrim_street_map;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class ScheduleTreeTrim_street_mapEntityConfiguration : IEntityTypeConfiguration<ScheduleTreeTrim_street_maps>
    {
        public void Configure(EntityTypeBuilder<ScheduleTreeTrim_street_maps> builder)
        {
            ConfigurationScheduleTreeTrim_street_mapsTable(builder);
        }

        void ConfigurationScheduleTreeTrim_street_mapsTable(EntityTypeBuilder<ScheduleTreeTrim_street_maps> builder)
        {
            builder.ToTable("ScheduleTreeTrim_street_maps");
            builder.HasKey(scheduleTreeTrim_street_maps => new { scheduleTreeTrim_street_maps.StreetId, scheduleTreeTrim_street_maps.ScheduleTreeTrimId });

            builder.Property(bucketTruck => bucketTruck.CreateBy)
                .HasMaxLength(50);
            builder.Property(bucketTruck => bucketTruck.UpdateBy)
            .HasMaxLength(50);

            builder.HasData(new ScheduleTreeTrim_street_maps
            {
                StreetId = Guid.Parse("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"),
                ScheduleTreeTrimId = Guid.Parse("04dc28f5-94c4-4565-93a2-934d6fee53fd"),
                CreateDate = DateTime.Now,
                CreateBy = "Admin",
                UpdateBy = "Admin",
                UpdateDate = DateTime.Now
            });
        }
    }
}
