using Domain.Entities.Cultivar;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class CultivarEntityConfiguration : IEntityTypeConfiguration<Cultivars>
    {
        public void Configure(EntityTypeBuilder<Cultivars> builder)
        {
            ConfigureCultivarsTable(builder);
        }

        private void ConfigureCultivarsTable(EntityTypeBuilder<Cultivars> builder)
        {
            builder.ToTable("Cultivars")
                   .HasKey(cultivar => cultivar.CultivarId);

            builder.Property(cultivar => cultivar.CultivarName)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(cultivar => cultivar.CreateBy)
                   .HasMaxLength(50);
            builder.Property(cultivar => cultivar.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new Cultivars { CultivarId = Guid.Parse("136514ac-99a2-421a-80e1-5351d9a9c4af"), CultivarName = "Giong cay bang", TreeTypeId = Guid.Parse("ad98e780-ce3b-401b-a2ec-dd7ba8027642"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            builder.HasData(new Cultivars { CultivarId = Guid.Parse("136514ac-99a2-221a-80e1-5351d9a9c4af"), CultivarName = "Giong cay phuong", TreeTypeId = Guid.Parse("ad98e780-ce3b-401b-a2ec-dd7ba8027642"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
        }
    }
}