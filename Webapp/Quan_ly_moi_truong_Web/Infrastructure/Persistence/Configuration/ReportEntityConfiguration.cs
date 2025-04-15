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

            builder.Property(reports => reports.IssuerGmail)
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}