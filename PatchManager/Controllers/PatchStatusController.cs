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
    public class PatchStatusController : ControllerBase
    {
        private readonly IPatchStatusService _statusService;

        public PatchStatusController(IPatchStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatchStatusDto>>> GetAll()
        {
            return Ok(await _statusService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatchStatusDto>> GetById(int id)
        {
            var status = await _statusService.GetByIdAsync(id);
            return status == null ? NotFound() : Ok(status);
        }

        [HttpPost]
        public async Task<ActionResult<PatchStatusDto>> Create(PatchStatusDto dto)
        {
            var created = await _statusService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PatchStatusDto dto)
        {
            var result = await _statusService.UpdateAsync(id, dto);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _statusService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus(
            [FromQuery] int customerId,
            [FromQuery] int patchId,
            [FromBody] StatusUpdateDto dto)
        {
            try
            {
                var result = await _statusService.UpdateStatusAsync(customerId, patchId, dto);
                return result ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
