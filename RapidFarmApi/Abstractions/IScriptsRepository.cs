using RapidFarmApi.Database.Entities;

namespace RapidFarmApi.Abstractions
{
    public interface IScriptsRepository
    {
        Task<PlantScript> GetCurrentScriptAsync();
        Task<List<PlantScript>> GetScriptsAsync();
        Task<PlantScript?> GetScriptByIdAsync(Guid id);
        Task AddScriptAsync(PlantScript script);
        Task ChangeCurrentAsync(Guid newCurrentId);
        Task DeleteScriptAsync(Guid id);
    }
}