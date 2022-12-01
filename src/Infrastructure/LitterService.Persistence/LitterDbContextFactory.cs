using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LitterService.Persistence
{
    public class SentinelDbContextFactory : IDesignTimeDbContextFactory<LitterDbContext>
    {
        public LitterDbContext CreateDbContext(string[] args)
        {
            var sentinelDbConnectionString = "host=localhost;port=5433;database=TestDb;username=test-user;password=test-user;";
            var optionsBuilder = new DbContextOptionsBuilder<LitterDbContext>()
                 .UseNpgsql(sentinelDbConnectionString);

            return new LitterDbContext(optionsBuilder.Options);
        }
    }
}
