using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrisonRP.Data.Models;

namespace PrisonRP.Data.EntityConfiguration;

public class FactionEntityTypeConfiguration : IEntityTypeConfiguration<Faction>
{
    public void Configure(EntityTypeBuilder<Faction> builder)
    {
        builder.HasData(new { Id = 1, Name = "Prisoners" }, new { Id = 2, Name = "Guards" }, new { Id = 3, Name = "Medics" });

        builder
            .HasMany(c => c.Players)
            .WithOne(e => e.Faction)
            .IsRequired();
    }
}
