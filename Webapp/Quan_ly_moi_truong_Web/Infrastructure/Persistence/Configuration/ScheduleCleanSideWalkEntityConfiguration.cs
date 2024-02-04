using Domain.Entities.ScheduleCleanSidewalk;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class ScheduleCleanSideWalkEntityConfiguration : IEntityTypeConfiguration<ScheduleCleanSidewalks>
    {
        public void Configure(EntityTypeBuilder<ScheduleCleanSidewalks> builder)
        {
            ConfigurationScheduleCleanSidewalksTable(builder);
        }

        private void ConfigurationScheduleCleanSidewalksTable(EntityTypeBuilder<ScheduleCleanSidewalks> builder)
        {
            builder.ToTable("ScheduleCleanSidewalks")
                   .HasKey(scheduleCleanSidewalks => scheduleCleanSidewalks.ScheduleCleanSidewalksId);

            builder.Property(scheduleCleanSidewalks => scheduleCleanSidewalks.CreateBy)
                   .HasMaxLength(50);
            builder.Property(scheduleCleanSidewalks => scheduleCleanSidewalks.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new ScheduleCleanSidewalks 
            {
                ScheduleCleanSidewalksId = Guid.Parse("7a866c85-b013-4fab-80c7-15d21d0c686c"), 
                StartTime = DateTime.Now,
                WorkingMonth = DateTime.Now,
                CreateDate = DateTime.Now, 
                CreateBy = "Admin", 
                UpdateBy = "Admin", 
                UpdateDate = DateTime.Now 
            });
        }
    }
}
