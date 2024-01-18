using Domain.Entities.ScheduleGarbageCollect;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ScheduleGarbageCollectEntityConfiguration : IEntityTypeConfiguration<ScheduleGarbageCollects>
    {
        public void Configure(EntityTypeBuilder<ScheduleGarbageCollects> builder)
        {
            ConfigurationScheduleGarbageCollectsTable(builder);
        }

        private void ConfigurationScheduleGarbageCollectsTable(EntityTypeBuilder<ScheduleGarbageCollects> builder)
        {
            builder.ToTable("ScheduleGarbageCollects")
                   .HasKey(scheduleGarbageCollects => scheduleGarbageCollects.ScheduleGarbageCollectId);

            builder.Property(scheduleGarbageCollects => scheduleGarbageCollects.GabageMass)
                   .IsRequired();
            builder.Property(scheduleGarbageCollects => scheduleGarbageCollects.CreateBy)
                   .HasMaxLength(50);
            builder.Property(scheduleGarbageCollects => scheduleGarbageCollects.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new ScheduleGarbageCollects
            {
                ScheduleGarbageCollectId = Guid.Parse("26397b2b-ca94-4af4-bf0d-f7aaa7510698"),
                GarbageTruckId = Guid.Parse("fc34e805-4550-4037-a273-17a0b1639bbe"),
                GabageMass = 10,
                StartTime = DateTime.Now,
                TransitTime = DateTime.Now.AddHours(3),
                WorkingMonth = DateTime.Now,
                CreateDate = DateTime.Now,
                CreateBy = "Admin",
                UpdateBy = "Admin",
                UpdateDate = DateTime.Now
            });

            builder.HasData(new ScheduleGarbageCollects
            {
                ScheduleGarbageCollectId = Guid.Parse("e3c19a06-7f84-4c4d-8d83-71264a5cf176"),
                GarbageTruckId = Guid.Parse("fc34e805-4550-4037-a273-17a0b1639bbe"),
                GabageMass = 10,
                StartTime = DateTime.Now,
                TransitTime = DateTime.Now.AddHours(3),
                WorkingMonth = DateTime.Now,
                CreateDate = DateTime.Now,
                CreateBy = "Admin",
                UpdateBy = "Admin",
                UpdateDate = DateTime.Now
            });
        }
    }
}