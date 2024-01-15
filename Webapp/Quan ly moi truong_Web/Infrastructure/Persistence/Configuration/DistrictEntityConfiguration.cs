using Domain.Entities.Deparment;
using Domain.Entities.District;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class DistrictEntityConfiguration : IEntityTypeConfiguration<Districts>
    {
        public void Configure(EntityTypeBuilder<Districts> builder)
        {
            ConfigurationDistrictsTable(builder);
        }

        private void ConfigurationDistrictsTable(EntityTypeBuilder<Districts> builder)
        {
            builder.ToTable("Districts")
                   .HasKey(district => district.DistrictId);

            builder.Property(district => district.DistrictName)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(district => district.CreateBy)
                   .HasMaxLength(50);
            builder.Property(district => district.UpdateBy)
                   .HasMaxLength(50);


            builder.HasData(new Districts { DistrictId = Guid.Parse("be7d62da-53ea-46b0-b294-bb109eca92fc"), DistrictName = "Ngu Hanh Son", CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            builder.HasData(new Districts { DistrictId = Guid.Parse("be7d62da-33ea-46b0-b294-bb109eca92fc"), DistrictName = "Thanh Khe", CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            builder.HasData(new Districts { DistrictId = Guid.Parse("be7d62da-51ea-46b0-b294-bb109eca92fc"), DistrictName = "Hai Chau", CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
        }
    }
}
