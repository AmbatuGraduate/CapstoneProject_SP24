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
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(department => department.DepartmentEmail)
                    .HasMaxLength(100);
            builder.Property(department => department.Description)
                    .HasMaxLength(100);

            //     builder.HasData(new Departments { DepartmentId = "01egqt2p26jkcil", DepartmentName = "Quan ly cay xanh", CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            //     builder.HasData(new Departments { DepartmentId = "01gf8i83494yr09", DepartmentName = "Quan ly quet don", CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            //     builder.HasData(new Departments { DepartmentId = "03bj1y382j5l78b", DepartmentName = "Quan ly thu gom rac", CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            builder.HasData(new Departments { DepartmentId = "01egqt2p26jkcil", DepartmentName = "Quan ly cay xanh", DepartmentEmail = "dev@gmail.com", Description = "string", AdminCreated = true, DirectMembersCount = 3});
        }
    }
}