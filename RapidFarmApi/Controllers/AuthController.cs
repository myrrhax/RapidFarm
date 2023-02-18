using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidFarmApi.Abstractions;
using RapidFarmApi.Database.Entities;
using RapidFarmApi.Models;

namespace RapidFarmApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public AuthController(IUserRepository repo)
        {
            _repo = repo;
        }
        
        [HttpPost]
        [Route("/login")]
        public async Task<ActionResult<string>> Login(LoginRequest req) 
        {
            User? user = await _repo.GetUserByName(req.UserName);
            if (user == null || user?.PasswordHash != _repo.HashPassword(req.Password))
                return NotFound();
            
            return Ok(_repo.GenerateJwtToken(user));
        }

        [HttpPost]
        [Route("/register")]
        public async Task<ActionResult<string>> Register(RegisterRequest req) 
        {
            User? user = await _repo.AddUserAsync(req);
            if (user == null)
                return BadRequest();
            
            return Ok(_repo.GenerateJwtToken(user));
        }
    }
}