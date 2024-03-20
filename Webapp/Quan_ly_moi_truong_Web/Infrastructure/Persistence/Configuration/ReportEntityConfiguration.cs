using Domain.Entities.Report;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

            builder.Property(reports => reports.IssuerGmail)
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
