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
            builder.Property(tree => tree.TreeLocation)
                   .HasMaxLength(200)
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
        }
    }
}