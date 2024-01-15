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
    public class ListSidewalkCleanerTaskEntityConfiguration : IEntityTypeConfiguration<ListSidewalkCleanerTasks>
    {
        public void Configure(EntityTypeBuilder<ListSidewalkCleanerTasks> builder)
        {
            ConfigruationListSidewalkCleanerTasksTable(builder);
        }

        private void ConfigruationListSidewalkCleanerTasksTable(EntityTypeBuilder<ListSidewalkCleanerTasks> builder)
        {
            builder.ToTable("ListSidewalkCleanerTasks")
                   .HasKey(listSidewalkCleanerTasks => listSidewalkCleanerTasks.ListSidewalkCleanerTaskId);

            builder.Property(listSidewalkCleanerTasks => listSidewalkCleanerTasks.CreateBy)
                   .HasMaxLength(50);
            builder.Property(listSidewalkCleanerTasks => listSidewalkCleanerTasks.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new ListSidewalkCleanerTasks { ListSidewalkCleanerTaskId = Guid.Parse("3c30019e-05f6-4f43-8bd5-3e29ef9004cf"), UserId = Guid.Parse("b2b1e0ce-0187-4285-8cce-60fdff665f46"), ScheduleCleanSidewalkId = Guid.Parse("7a866c85-b013-4fab-80c7-15d21d0c686c"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            builder.HasData(new ListSidewalkCleanerTasks { ListSidewalkCleanerTaskId = Guid.Parse("3c30019e-05f6-4f43-8bd5-3e29ef90031a"), UserId = Guid.Parse("b2b1e0ce-0187-4285-8cce-60fdff666f46"), ScheduleCleanSidewalkId = Guid.Parse("7a866c85-b013-4fab-80c7-15d21d0c686c"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });

        }
    }
}
