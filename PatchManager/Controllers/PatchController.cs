using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatchManager.DTOs;
using PatchManager.Services;
using Microsoft.AspNetCore.Authorization;

namespace PatchManager.Controllers
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class PatchController : ControllerBase
    {
        private readonly IPatchService _patchService;

        public PatchController(IPatchService patchService)
        {
            _patchService = patchService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatchDto>>> GetAll()
        {
            return Ok(await _patchService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatchDto>> GetById(int id)
        {
            var patch = await _patchService.GetByIdAsync(id);
            return patch == null ? NotFound() : Ok(patch);
        }

        [HttpPost]
        public async Task<ActionResult<PatchDto>> Create(PatchDto patchDto)
        {
            var created = await _patchService.CreateAsync(patchDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PatchDto patchDto)
        {
            var result = await _patchService.UpdateAsync(id, patchDto);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _patchService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
