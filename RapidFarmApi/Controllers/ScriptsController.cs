using Microsoft.AspNetCore.Mvc;
using RapidFarmApi.Abstractions;
using RapidFarmApi.Database.Entities;

namespace RapidFarmApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScriptsController : ControllerBase
    {
        private readonly IScriptsRepository _repo;

        public ScriptsController(IScriptsRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> AddScript(PlantScript script) 
        {
            await _repo.AddScriptAsync(script);
            return script != null ? Ok(script) : BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetScript(string id) 
        {
            PlantScript? script = await _repo.GetScriptByIdAsync(Guid.Parse(id));

            return script != null ? Ok(script) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetScripts() 
        {
            return Ok(await _repo.GetScriptsAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScript(string id) 
        {
            await _repo.DeleteScriptAsync(Guid.Parse(id));
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ChangeCurrent(string id) 
        {
            await _repo.ChangeCurrentAsync(Guid.Parse(id));

            return Ok();
        }
    }
}