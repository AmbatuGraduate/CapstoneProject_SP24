using Domain.Entities.ScheduleGarbageCollect;
using Domain.Entities.ScheduleTreeTrim;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class ScheduleTreeTrimEntityConfiguration : IEntityTypeConfiguration<ScheduleTreeTrims>
    {
        public void Configure(EntityTypeBuilder<ScheduleTreeTrims> builder)
        {
            ConfigurationScheduleTreeTrimsTable(builder);
        }

        public void ConfigurationScheduleTreeTrimsTable(EntityTypeBuilder<ScheduleTreeTrims> builder)
        {
            builder.ToTable("ScheduleTreeTrims")
                   .HasKey(scheduleTreeTrim => scheduleTreeTrim.ScheduleTreeTrimId);

            builder.Property(scheduleTreeTrim => scheduleTreeTrim.CreateBy)
                   .HasMaxLength(50);
            builder.Property(scheduleTreeTrim => scheduleTreeTrim.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new ScheduleTreeTrims
            {
                ScheduleTreeTrimId = Guid.Parse("04dc28f5-94c4-4565-93a2-934d6fee53fd"),
                BucketTruckId = Guid.Parse("f9257e9f-6d30-45fd-8afc-3e3266d7adc6"),
                EstimatedPruningTime = DateTime.Now.AddMonths(3),
                ActualTrimmingTime = DateTime.Now.AddMonths(3).AddDays(1),
                CreateDate = DateTime.Now,
                CreateBy = "Admin",
                UpdateBy = "Admin",
                UpdateDate = DateTime.Now
            });
        }
    }
}
