using Microsoft.EntityFrameworkCore;
using RapidFarmApi.Database.Entities;

namespace RapidFarmApi.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<PlantScript> Scripts {get; set;}
        public DbSet<State> StateList {get; set;}
        public DbSet<User> Users {get; set;}

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        public async Task Ping() 
        {
            await Database.ExecuteSqlRawAsync("SELECT 1;").ConfigureAwait(false); 
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { Database.EnsureCreated(); }
    }
}