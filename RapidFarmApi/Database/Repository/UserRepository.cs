using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RapidFarmApi.Abstractions;
using RapidFarmApi.Database.Entities;
using RapidFarmApi.Models;

namespace RapidFarmApi.Database.Repository
{
    public class UserRepository : IUserRepository
    {
        public ApplicationDbContext _db;
        ILogger<UserRepository> _logger;
        public UserRepository(ApplicationDbContext db, ILogger<UserRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        public Task<User?> GetUserByIdAsync(Guid userId)
        {
            return _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public string HashPassword(string password)
        {
            var a = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SALT")),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            ));
            _logger.LogInformation(a);
            return a;
        }

        public async Task<User?> AddUserAsync(RegisterRequest req)
        {
            User user = new User() { Id = Guid.NewGuid(), Name = req.UserName,
                                     TelegramId = req.TelegramId,
                                     UseTelegramNotification = req.UseTelegramNotification,
                                     PasswordHash = HashPassword(req.Password),
                                     AddTime = DateTime.UtcNow };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public Task<User?> GetUserByName(string userName)
        {
            return _db.Users.FirstOrDefaultAsync(u => u.Name == userName);
        }

        public string GenerateJwtToken(User user) 
        {
            string issuer = Environment.GetEnvironmentVariable("TOKEN_ISSUER");
            string audience = Environment.GetEnvironmentVariable("TOKEN_AUDIENCE");
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("KEY"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new [] {
                    new Claim("id", user.Id.ToString()),
                    new Claim("user_name", user.Name),
                }),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}