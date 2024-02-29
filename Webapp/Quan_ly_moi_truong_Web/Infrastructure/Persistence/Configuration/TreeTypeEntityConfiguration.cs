using Domain.Entities.TreeType;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class TreeTypeEntityConfiguration : IEntityTypeConfiguration<TreeTypes>
    {
        public void Configure(EntityTypeBuilder<TreeTypes> builder)
        {
            ConfigurationTreeTypesTable(builder);
        }

        private void ConfigurationTreeTypesTable(EntityTypeBuilder<TreeTypes> builder)
        {
            builder.ToTable("TreeTypes")
                   .HasKey(type => type.TreeTypeId);

            builder.Property(type => type.TreeTypeName)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(type => type.CreateBy)
                   .HasMaxLength(50);
            builder.Property(type => type.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new TreeTypes
            {
                TreeTypeId = Guid.Parse("ad98e780-ce3b-401b-a2ec-dd7ba8027642"),
                TreeTypeName = "Cay than go",
                CreateDate = DateTime.Now,
                CreateBy = "Admin",
                UpdateBy = "Admin",
                UpdateDate = DateTime.Now
            });
        }
    }
}