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
            builder.Property(department => department.CreateBy)
                   .HasMaxLength(50);
            builder.Property(department => department.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new Departments { DepartmentId = Guid.Parse("bc2f24de-2b9b-489a-a108-64a114d2b9be"), DepartmentName = "Quet don via he", CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            builder.HasData(new Departments { DepartmentId = Guid.Parse("bc2f24de-2b9b-429a-a108-64a114d2b9be"), DepartmentName = "Thu gom rac", CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            builder.HasData(new Departments { DepartmentId = Guid.Parse("bc2f24de-1b9b-489a-a108-64a114d2b9be"), DepartmentName = "Cat tia cay", CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
        }
    }
}