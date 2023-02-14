using Microsoft.EntityFrameworkCore;
using RapidFarmApi.Database.Entities;

namespace RapidFarmApi.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Profile> Profiles {get; set;}
        public DbSet<State> StateList {get; set;}

        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
    }
}