using System;
using LitterService.Application.Contracts.Persistence;
using LitterService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LitterService.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var host = Environment.GetEnvironmentVariable("LITTERDB_HOST") is not null ? Environment.GetEnvironmentVariable("LITTERDB_HOST") : "localhost";
            var database = Environment.GetEnvironmentVariable("LITTERDB_DATABASE") is not null ? Environment.GetEnvironmentVariable("LITTERDB_DATABASE") : "LitterDb";
            var user = Environment.GetEnvironmentVariable("LITTERDB_USER") is not null ? Environment.GetEnvironmentVariable("LITTERDB_USER") : "test-user";
            var password = Environment.GetEnvironmentVariable("LITTERDB_PASSWORD") is not null ? Environment.GetEnvironmentVariable("LITTERDB_PASSWORD") : "test-password";

            var dbConnectionString = $"host={host};database={database};username={user};password={password};";

            services.AddDbContext<LitterDbContext>(option =>
            {
                option.UseNpgsql(dbConnectionString);
            });

            var runMigrations = Environment.GetEnvironmentVariable("RUN_MIGRATIONS");
            if (runMigrations is not null)
            {
                Console.WriteLine("Running migrations");
                var optionBuilder = new DbContextOptionsBuilder<LitterDbContext>();
                optionBuilder.UseNpgsql(dbConnectionString);
                using var dbContext = new LitterDbContext(optionBuilder.Options);
                dbContext.Database.Migrate();
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}