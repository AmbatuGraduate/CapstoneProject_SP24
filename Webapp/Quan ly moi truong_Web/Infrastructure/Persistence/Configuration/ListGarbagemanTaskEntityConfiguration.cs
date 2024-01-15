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
    public class ListGarbagemanTaskEntityConfiguration : IEntityTypeConfiguration<ListGarbagemanTasks>
    {
        public void Configure(EntityTypeBuilder<ListGarbagemanTasks> builder)
        {
            ConfigruationListGarbagemanTasksTable(builder);
        }

        private void ConfigruationListGarbagemanTasksTable(EntityTypeBuilder<ListGarbagemanTasks> builder)
        {
            builder.ToTable("ListGarbagemanTasks")
                   .HasKey(listGarbagemanTask => listGarbagemanTask.ListGarbagemanTaskId);

            builder.Property(listGarbagemanTask => listGarbagemanTask.CreateBy)
                   .HasMaxLength(50);
            builder.Property(listGarbagemanTask => listGarbagemanTask.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new ListGarbagemanTasks { ListGarbagemanTaskId = Guid.Parse("f348026b-3f20-4197-865f-076f47c4cbc7"), UserId = Guid.Parse("b2b1e0ce-0187-4285-8cce-60fdff665f46"), ScheduleGarbageCollectId = Guid.Parse("26397b2b-ca94-4af4-bf0d-f7aaa7510698"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            builder.HasData(new ListGarbagemanTasks { ListGarbagemanTaskId = Guid.Parse("f348026b-3f20-4197-865f-076f47c4cbd7"), UserId = Guid.Parse("b2b1e0ce-0187-4285-8cce-60fdff666f46"), ScheduleGarbageCollectId = Guid.Parse("26397b2b-ca94-4af4-bf0d-f7aaa7510698"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
        }
    }
}

