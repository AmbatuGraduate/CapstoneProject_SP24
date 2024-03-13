using Domain.Entities.Deparment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class DepartmentEntityConfiguration : IEntityTypeConfiguration<Departments>
    {
        public void Configure(EntityTypeBuilder<Departments> builder)
        {
            ConfigurateDepartmentsTable(builder);
        }

        private void ConfigurateDepartmentsTable(EntityTypeBuilder<Departments> builder)
        {
            builder.ToTable("Departments")
                   .HasKey(department => department.DepartmentId);
            builder.Property(department => department.DepartmentName)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(department => department.DepartmentEmail)
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}