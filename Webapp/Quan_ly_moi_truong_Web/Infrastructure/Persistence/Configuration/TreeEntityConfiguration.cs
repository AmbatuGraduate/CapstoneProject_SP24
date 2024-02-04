using Domain.Entities.Tree;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class TreeEntityConfiguration : IEntityTypeConfiguration<Trees>
    {
        public void Configure(EntityTypeBuilder<Trees> builder)
        {
            ConfigurationTreesTable(builder);
        }

        public void ConfigurationTreesTable(EntityTypeBuilder<Trees> builder)
        {
            builder.ToTable("Trees")
                   .HasKey(tree => tree.TreeId);

            builder.Property(tree => tree.TreeCode)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(tree => tree.BodyDiameter)
                   .IsRequired();
            builder.Property(tree => tree.LeafLength)
                   .IsRequired();
            builder.Property(tree => tree.PlantTime)
                   .IsRequired();
            builder.Property(tree => tree.CutTime)
                   .IsRequired();
            builder.Property(tree => tree.IntervalCutTime)
                   .IsRequired();
            builder.Property(tree => tree.Note)
                   .HasMaxLength(180)
                   .IsRequired(false);
            builder.Property(tree => tree.CreateBy)
                   .HasMaxLength(50);
            builder.Property(tree => tree.CreateBy)
                   .HasMaxLength(50);

            builder.HasData(new Trees
            {
                TreeId = Guid.Parse("24b2ee45-d7c3-4cc7-9fac-406b4bac1d82"),
                TreeCode = "12_HL_HH_NHS",
                StreetId = Guid.Parse("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"),
                BodyDiameter = 30,
                LeafLength = 50,
                PlantTime = DateTime.Now,
                CutTime = DateTime.Now.AddMonths(3),
                IntervalCutTime = 3,
                CultivarId = Guid.Parse("136514ac-99a2-421a-80e1-5351d9a9c4af"),
                CreateDate = DateTime.Now,
                CreateBy = "Admin",
                UpdateBy = "Admin",
                UpdateDate = DateTime.Now
            });
        }
    }
}