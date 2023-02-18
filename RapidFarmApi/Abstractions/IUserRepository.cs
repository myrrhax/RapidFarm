using RapidFarmApi.Database.Entities;
using RapidFarmApi.Models;

namespace RapidFarmApi.Abstractions
{
    public interface IUserRepository
    {
        Task<User?> AddUserAsync(RegisterRequest req);
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<User?> GetUserByName(string userName);
        string HashPassword(string password);
        string GenerateJwtToken(User user);
    }
}