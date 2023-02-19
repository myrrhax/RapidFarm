using Microsoft.EntityFrameworkCore;
using RapidFarmApi.Abstractions;
using RapidFarmApi.Database.Entities;

namespace RapidFarmApi.Database.Repository
{
    public class ScriptsRepository : IScriptsRepository
    {
        private readonly ApplicationDbContext _db;
        public ScriptsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task AddScriptAsync(PlantScript script)
        {
            await _db.Scripts.AddAsync(script);
            await _db.SaveChangesAsync();
        }

        public async Task ChangeCurrentAsync(Guid newCurrentId)
        {
            PlantScript currentScript = await GetCurrentScriptAsync();
            PlantScript? newCurrent = await GetScriptByIdAsync(newCurrentId);
            if (newCurrent != null) 
            {
                currentScript.IsCurrent = false;
                currentScript.currentInterval = null;
                newCurrent.IsCurrent = true;
                newCurrent.currentInterval = 0;
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteScriptAsync(Guid id)
        {
            PlantScript? script = await GetScriptByIdAsync(id);
            if (script != null)
            {
                _db.Scripts.Remove(script);
                await _db.SaveChangesAsync();
            }
        }

        public Task<PlantScript> GetCurrentScriptAsync()
        {
            return _db.Scripts.FirstAsync(s => s.IsCurrent);
        }

        public Task<PlantScript?> GetScriptByIdAsync(Guid id)
        {
            return _db.Scripts.FirstOrDefaultAsync(s => s.Id == id);
        }


        public Task<List<PlantScript>> GetScriptsAsync()
        {
            return _db.Scripts.ToListAsync();
        }
    }
}