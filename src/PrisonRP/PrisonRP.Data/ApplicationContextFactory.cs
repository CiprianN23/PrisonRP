using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PrisonRP.Data;

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        var mariadbVersion = new MariaDbServerVersion(new Version(10, 6, 5));
        optionsBuilder.UseMySql("", mariadbVersion);

        return new ApplicationContext(optionsBuilder.Options);
    }
}
