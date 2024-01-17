
using Domain.Entities.ListGarbagemanTask;
using Domain.Entities.Report;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class ReportEntityConfiguration : IEntityTypeConfiguration<Reports>
    {
        public void Configure(EntityTypeBuilder<Reports> builder)
        {
            ConfigruationReportsTable(builder);
        }

        private void ConfigruationReportsTable(EntityTypeBuilder<Reports> builder)
        {
            builder.ToTable("Reports")
                   .HasKey(reports => reports.ReportId);

            builder.Property(reports => reports.Title)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(reports => reports.Content)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(report => report.Feedback)
                   .IsRequired(false);
            builder.Property(reports => reports.FeedbackBy)
                   .HasMaxLength(50)
                   .IsRequired(false);

            builder.HasData(new Reports { ReportId = Guid.Parse("6e4ba4c3-6edf-45ca-8b60-54caa256c725"), UserId = Guid.Parse("b2b1e0ce-0187-4285-8cce-60fdff665f46"), Title = "DEMO", Content = "Demo", Image = "string", Status = false });
        }
    }
}
