using Domain.Entities.ListGarbagemanTask;
using Domain.Entities.ListSidewalkCleanerTask;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class User_scheduleCleanSidewalk_mapEntityConfiguration : IEntityTypeConfiguration<User_scheduleCleanSidewalk_maps>
    {
        public void Configure(EntityTypeBuilder<User_scheduleCleanSidewalk_maps> builder)
        {
            ConfigruationListSidewalkCleanerTasksTable(builder);
        }

        private void ConfigruationListSidewalkCleanerTasksTable(EntityTypeBuilder<User_scheduleCleanSidewalk_maps> builder)
        {
            builder.ToTable("User_scheduleCleanSidewalk_maps")
                   .HasKey(user_scheduleCleanSidewalk_map => new { user_scheduleCleanSidewalk_map.UserId , user_scheduleCleanSidewalk_map.ScheduleCleanSidewalkId });

            builder.Property(listSidewalkCleanerTasks => listSidewalkCleanerTasks.CreateBy)
                   .HasMaxLength(50);
            builder.Property(listSidewalkCleanerTasks => listSidewalkCleanerTasks.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new User_scheduleCleanSidewalk_maps { UserId = Guid.Parse("b2b1e0ce-0187-4285-8cce-60fdff665f46"), ScheduleCleanSidewalkId = Guid.Parse("7a866c85-b013-4fab-80c7-15d21d0c686c"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            builder.HasData(new User_scheduleCleanSidewalk_maps { UserId = Guid.Parse("b2b1e0ce-0187-4285-8cce-60fdff666f46"), ScheduleCleanSidewalkId = Guid.Parse("7a866c85-b013-4fab-80c7-15d21d0c686c"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });

        }
    }
}
