using Domain.Entities.ResidentialGroup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ResidentialGroupEntityConfiguration : IEntityTypeConfiguration<ResidentialGroups>
    {
        public void Configure(EntityTypeBuilder<ResidentialGroups> builder)
        {
            ConfigureResidentialGroupsTable(builder);
        }

        private void ConfigureResidentialGroupsTable(EntityTypeBuilder<ResidentialGroups> builder)
        {
            builder.ToTable("ResidentialGroups")
                   .HasKey(residentialGroups => residentialGroups.ResidentialGroupId);

            builder.Property(residentialGroups => residentialGroups.ResidentialGroupName)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(residentialGroups => residentialGroups.CreateBy)
                   .HasMaxLength(50);
            builder.Property(residentialGroups => residentialGroups.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new ResidentialGroups
            {
                ResidentialGroupId = Guid.Parse("0a0e931d-d055-48a9-b8a4-2cf57ac2f6f5"),
                ResidentialGroupName = "Dong Tra",
                WardId = Guid.Parse("996c63bc-5f0a-44f6-8c9a-aad741b3beac"),
                CreateDate = DateTime.Now,
                CreateBy = "Admin",
                UpdateBy = "Admin",
                UpdateDate = DateTime.Now
            });
        }
    }
}