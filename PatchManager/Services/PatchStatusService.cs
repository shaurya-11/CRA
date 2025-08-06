using PatchManager.DTOs;
using PatchManager.Models;
using PatchManager.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace PatchManager.Services
{
    public class PatchStatusService : IPatchStatusService
    {
        private readonly PatchDbContext _context;
        private readonly IMapper _mapper;

        public PatchStatusService(PatchDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatchStatusDto>> GetAllAsync()
        {
            var statuses = await _context.PatchStatuses
                .Include(ps => ps.Customer)
                .Include(ps => ps.Patch)
                .ToListAsync();

            return _mapper.Map<List<PatchStatusDto>>(statuses);
        }

        public async Task<PatchStatusDto> GetByIdAsync(int id)
        {
            var status = await _context.PatchStatuses
                .Include(ps => ps.Customer)
                .Include(ps => ps.Patch)
                .FirstOrDefaultAsync(ps => ps.Id == id);

            return status == null ? null : _mapper.Map<PatchStatusDto>(status);
        }

        public async Task<PatchStatusDto> CreateAsync(PatchStatusDto dto)
        {
            var entity = _mapper.Map<PatchStatus>(dto);
            _context.PatchStatuses.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<PatchStatusDto>(entity);
        }

        public async Task<bool> UpdateAsync(int id, PatchStatusDto dto)
        {
            var entity = await _context.PatchStatuses.FindAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PatchStatuses.FindAsync(id);
            if (entity == null) return false;

            _context.PatchStatuses.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(int customerId, int patchId, StatusUpdateDto dto)
        {
            var status = await _context.PatchStatuses
                .FirstOrDefaultAsync(ps =>
                    ps.CustomerId == customerId &&
                    ps.PatchId == patchId);

            if (status == null)
                return false;

            status.Status = (PatchStatusEnum)dto.Status;
            status.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<AgentQueryDto>> GetLatestPatchStatusesForCustomerAsync(int customerId)
        {
            var products = await _context.Products
                .Where(p => p.CustomerId == customerId)
                .ToListAsync();

            var result = new List<AgentQueryDto>();

            foreach (var product in products)
            {
                var latestPending = await (
                    from ps in _context.PatchStatuses
                    join p in _context.Patches on ps.PatchId equals p.Id
                    where ps.CustomerId == customerId
                        && ps.Status == PatchStatusEnum.Pending
                        && p.ProductId == product.Id
                    orderby p.ReleasedOn descending
                    select new AgentQueryDto
                    {
                        CustomerId = customerId,
                        ProductId = product.Id,
                        ProductName = product.Name,
                        PatchId = p.Id,
                        Version = p.Version,
                        HasUpdate = true,
                        Message = "New update available"
                    })
                    .FirstOrDefaultAsync();

                result.Add(latestPending ?? new AgentQueryDto
                {
                    CustomerId = customerId,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    HasUpdate = false,
                    Message = "No updates available"
                });
            }

            return result;
        }
    }
}

