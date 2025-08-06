using PatchManager.DTOs;
using PatchManager.Models;
using PatchManager.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace PatchManager.Services
{
    public class PatchService : IPatchService
    {
        private readonly PatchDbContext _context;
        private readonly IMapper _mapper;

        public PatchService(PatchDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatchDto>> GetAllAsync()
        {
            var patches = await _context.Patches.ToListAsync();
            return _mapper.Map<List<PatchDto>>(patches);
        }

        public async Task<PatchDto> GetByIdAsync(int id)
        {
            var patch = await _context.Patches.FindAsync(id);
            return patch == null ? null : _mapper.Map<PatchDto>(patch);
        }

        public async Task<PatchDto> CreateAsync(PatchDto patchDto)
        {
            var product = await _context.Products
            .Include(p => p.Customer)
            .FirstOrDefaultAsync(p => p.Id == patchDto.ProductId);

            if (product == null)
            {
                throw new Exception("Product not found.");
            }
            var patch = _mapper.Map<Patch>(patchDto);
            _context.Patches.Add(patch);
            await _context.SaveChangesAsync();

            var patchStatus = new PatchStatus
            {
                PatchId = patch.Id,
                CustomerId = product.CustomerId,
                Status = PatchStatusEnum.Pending,
                UpdatedAt = DateTime.UtcNow
            };

            _context.PatchStatuses.Add(patchStatus);
            await _context.SaveChangesAsync();

            return _mapper.Map<PatchDto>(patch);
        }

        public async Task<bool> UpdateAsync(int id, PatchDto patchDto)
        {
            var patch = await _context.Patches.FindAsync(id);
            if (patch == null) return false;

            _mapper.Map(patchDto, patch);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patch = await _context.Patches.FindAsync(id);
            if (patch == null) return false;

            _context.Patches.Remove(patch);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
