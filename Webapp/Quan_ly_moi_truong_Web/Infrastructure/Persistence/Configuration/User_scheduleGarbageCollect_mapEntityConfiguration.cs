using Domain.Entities.GarbageTruckType;
using Domain.Entities.ListGarbagemanTask;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class User_scheduleGarbageCollect_mapEntityConfiguration : IEntityTypeConfiguration<User_scheduleGarbageCollect_maps>
    {
        public void Configure(EntityTypeBuilder<User_scheduleGarbageCollect_maps> builder)
        {
            ConfigruationListGarbagemanTasksTable(builder);
        }

        private void ConfigruationListGarbagemanTasksTable(EntityTypeBuilder<User_scheduleGarbageCollect_maps> builder)
        {
            builder.ToTable("User_scheduleGarbageCollect_maps")
                   .HasKey(user_scheduleGarbageCollect_maps => new { user_scheduleGarbageCollect_maps.UserId, user_scheduleGarbageCollect_maps .ScheduleGarbageCollectId});

            builder.Property(listGarbagemanTask => listGarbagemanTask.CreateBy)
                   .HasMaxLength(50);
            builder.Property(listGarbagemanTask => listGarbagemanTask.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new User_scheduleGarbageCollect_maps { UserId = Guid.Parse("b2b1e0ce-0187-4285-8cce-60fdff665f46"), ScheduleGarbageCollectId = Guid.Parse("26397b2b-ca94-4af4-bf0d-f7aaa7510698"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            builder.HasData(new User_scheduleGarbageCollect_maps { UserId = Guid.Parse("b2b1e0ce-0187-4285-8cce-60fdff666f46"), ScheduleGarbageCollectId = Guid.Parse("26397b2b-ca94-4af4-bf0d-f7aaa7510698"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
        }
    }
}

