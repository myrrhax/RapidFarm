using Microsoft.EntityFrameworkCore;
using RapidFarmApi.Abstractions;
using RapidFarmApi.Database.Repository;

namespace RapidFarmApi.Database
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services) 
        {
            string dbUser = Environment.GetEnvironmentVariable("POSTGRES_USER");
            string dbPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
            string dbName = Environment.GetEnvironmentVariable("POSTGRES_DB");

            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseNpgsql($"Host=db;Port=5432;Database={dbName};Username={dbUser};Password={dbPassword}");
            });

            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}