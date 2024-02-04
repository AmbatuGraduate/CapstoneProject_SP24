using Domain.Entities.ListGarbagemanTask;
using Domain.Entities.ListTreeTrimmerTask;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class User_scheduleTreeTrim_mapEntityConfiguration : IEntityTypeConfiguration<User_scheduleTreeTrim_maps>
    {
        public void Configure(EntityTypeBuilder<User_scheduleTreeTrim_maps> builder)
        {
            ConfigruationListTreeTrimmerTasksTable(builder);
        }

        private void ConfigruationListTreeTrimmerTasksTable(EntityTypeBuilder<User_scheduleTreeTrim_maps> builder)
        {
            builder.ToTable("User_scheduleTreeTrim_maps")
                   .HasKey(user_scheduleTreeTrim_maps => new { user_scheduleTreeTrim_maps.UserId, user_scheduleTreeTrim_maps .ScheduleTreeTrimId});

            builder.Property(listTreeTrimmerTasks => listTreeTrimmerTasks.CreateBy)
                   .HasMaxLength(50);
            builder.Property(listTreeTrimmerTasks => listTreeTrimmerTasks.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new User_scheduleTreeTrim_maps { UserId = Guid.Parse("b2b1e0ce-0187-4285-8cce-60fdff665f46"), ScheduleTreeTrimId = Guid.Parse("04dc28f5-94c4-4565-93a2-934d6fee53fd"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            builder.HasData(new User_scheduleTreeTrim_maps { UserId = Guid.Parse("b2b1e0ce-0187-4285-8cce-60fdff666f46"), ScheduleTreeTrimId = Guid.Parse("04dc28f5-94c4-4565-93a2-934d6fee53fd"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });

        }
    }
}
