using Microsoft.EntityFrameworkCore;
using PrisonRP.Data.EntityConfiguration;
using PrisonRP.Data.Models;

namespace PrisonRP.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Faction> Factions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FactionEntityTypeConfiguration).Assembly);
    }
}
