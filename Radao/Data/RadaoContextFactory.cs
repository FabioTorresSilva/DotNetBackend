using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Radao.Data
{
    public class RadaoContextFactory : IDesignTimeDbContextFactory<RadaoContext>
    {
        public RadaoContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<RadaoContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("RadaoDB"));

            return new RadaoContext(optionsBuilder.Options);
        }
    }
}
