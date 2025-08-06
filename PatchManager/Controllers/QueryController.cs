using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatchManager.DTOs;
using PatchManager.Services;

namespace PatchManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryController : Controller
    {
        private readonly QueryService _queryService;
        private readonly IMapper _mapper;

        public QueryController(QueryService queryService, IMapper mapper)
        {
            _queryService = queryService;
            _mapper = mapper;
        }

        [HttpGet("{id}/patch-details")]
        public async Task<ActionResult<List<CustomerPatchQueryDto>>> GetPatchDetails(int id)
        {
            var result = await _queryService.GetCustomerPatchDetailsAsync(id);
            return Ok(result);
        }

        [HttpGet("admin/patches")]
        public ActionResult<List<AdminPatchQueryDto>> GetAdminPatchDetails()
        {
            var result = _queryService.GetAdminPatchDetails();
            return Ok(result);
        }

        [HttpGet("notification/{customerId}")]
        public ActionResult<List<PatchNotificationQueryDto>> GetPendingPatches(int customerId)
        {
            var result = _queryService.GetPendingPatchesForCustomer(customerId);

            if (result == null || result.Count == 0)
            {
                return NotFound("No pending patches found for this customer.");
            }

            return Ok(result);
        }

        [HttpGet("latest-per-product/{customerId}")]
        public async Task<IActionResult> GetLatestPatchPerProduct(int customerId)
        {
            var updates = await _queryService.GetLatestPatchStatusesForCustomerAsync(customerId);
            return Ok(updates);
        }
    }
}
