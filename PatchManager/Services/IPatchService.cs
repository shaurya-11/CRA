using PatchManager.DTOs;

namespace PatchManager.Services
{
    public interface IPatchService
    {
        Task<IEnumerable<PatchDto>> GetAllAsync();
        Task<PatchDto> GetByIdAsync(int id);
        Task<PatchDto> CreateAsync(PatchDto patchDto);
        Task<bool> UpdateAsync(int id, PatchDto patchDto);
        Task<bool> DeleteAsync(int id);
    }
}
