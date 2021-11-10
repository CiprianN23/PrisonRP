using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrisonRP.Data.Models;

namespace PrisonRP.Data.EntityConfiguration;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.
            Property(b => b.Password)
            .IsRequired()
            .HasMaxLength(61);

        builder.
            Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(25);

        builder.
            HasIndex(b => b.Name);

        builder
            .Property(b => b.ImprisonmentReason)
            .HasDefaultValue("None")
            .HasMaxLength(129)
            .IsRequired();

        builder
            .Property(b => b.LastPositionX)
            .HasDefaultValue(416.6970);

        builder
            .Property(b => b.LastPositionY)
            .HasDefaultValue(1567.0526);

        builder
            .Property(b => b.LastPositionZ)
            .HasDefaultValue(1001.0000);

        builder
            .Property(b => b.LastPositionAngle)
            .HasDefaultValue(270.7746);

        builder
            .Property(b => b.Health)
            .HasDefaultValue(100);

        builder
            .Property(b => b.Armour)
            .HasDefaultValue(0);

        builder
            .Property(b => b.LastInterior)
            .HasDefaultValue(0);

        builder
            .Property(b => b.LastWorld)
            .HasDefaultValue(0);

        builder
            .Property(b => b.Money)
            .HasDefaultValue(350);

        builder
            .Property(b => b.FightStyle)
            .HasDefaultValue(4);
    }
}
