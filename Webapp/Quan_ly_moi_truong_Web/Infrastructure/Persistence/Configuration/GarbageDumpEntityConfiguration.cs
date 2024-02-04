using Domain.Entities.District;
using Domain.Entities.GarbageDump;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class GarbageDumpEntityConfiguration : IEntityTypeConfiguration<GarbageDumps>
    {
        public void Configure(EntityTypeBuilder<GarbageDumps> builder)
        {
            ConfigurationGarbageDumpsTable(builder);
        }

        private void ConfigurationGarbageDumpsTable(EntityTypeBuilder<GarbageDumps> builder)
        {
            builder.ToTable("GarbageDumps")
                   .HasKey(garbageDump => garbageDump.GarbageDumpId);

            builder.Property(garbageDump => garbageDump.GarbageDumpName)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(garbageDump => garbageDump.CreateBy)
                   .HasMaxLength(50);
            builder.Property(garbageDump => garbageDump.UpdateBy)
                   .HasMaxLength(50);

            builder.HasData(new GarbageDumps { GarbageDumpId = Guid.Parse("be5d01ee-b15c-4ced-aa0c-165c47dac9f9"), GarbageDumpName = "HL-HH-NHS_1", StreetId= Guid.Parse("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });
            builder.HasData(new GarbageDumps { GarbageDumpId = Guid.Parse("be5d01ee-b15d-4ced-aa0c-165c47dac9f9"), GarbageDumpName = "HL-HH-NHS_2", StreetId = Guid.Parse("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), CreateDate = DateTime.Now, CreateBy = "Admin", UpdateBy = "Admin", UpdateDate = DateTime.Now });

        }
    }
}
