using Domain.Entities.Report;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        }
    }
}