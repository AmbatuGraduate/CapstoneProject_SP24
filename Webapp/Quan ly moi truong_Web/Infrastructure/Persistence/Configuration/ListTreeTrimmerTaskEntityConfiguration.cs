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
    public class ListTreeTrimmerTaskEntityConfiguration : IEntityTypeConfiguration<ListTreeTrimmerTasks>
    {
        public void Configure(EntityTypeBuilder<ListTreeTrimmerTasks> builder)
        {
            ConfigruationListTreeTrimmerTasksTable(builder);
        }

        private void ConfigruationListTreeTrimmerTasksTable(EntityTypeBuilder<ListTreeTrimmerTasks> builder)
        {
            builder.ToTable("ListTreeTrimmerTasks")
                   .HasKey(listTreeTrimmerTasks => listTreeTrimmerTasks.ListTreeTrimmerTaskId);

            builder.Property(listTreeTrimmerTasks => listTreeTrimmerTasks.CreateBy)
                   .HasMaxLength(50);
            builder.Property(listTreeTrimmerTasks => listTreeTrimmerTasks.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new ListTreeTrimmerTasks { ListTreeTrimmerTaskId = Guid.Parse("25f83ff6-39d4-461d-82d3-3814cb57fa9c"), UserId = Guid.Parse("b2b1e0ce-0187-4285-8cce-60fdff665f46"), ScheduleTreeTrimId = Guid.Parse("04dc28f5-94c4-4565-93a2-934d6fee53fd"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            builder.HasData(new ListTreeTrimmerTasks { ListTreeTrimmerTaskId = Guid.Parse("e13c54c5-1923-49ef-99ab-54a60fed579c"), UserId = Guid.Parse("b2b1e0ce-0187-4285-8cce-60fdff666f46"), ScheduleTreeTrimId = Guid.Parse("04dc28f5-94c4-4565-93a2-934d6fee53fd"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });

        }
    }
}
