using PatchManager.DTOs;

namespace PatchManager.Services
{
    public interface IPatchStatusService
    {
        Task<IEnumerable<PatchStatusDto>> GetAllAsync();
        Task<PatchStatusDto> GetByIdAsync(int id);
        Task<PatchStatusDto> CreateAsync(PatchStatusDto statusDto);
        Task<bool> UpdateAsync(int id, PatchStatusDto statusDto);
        Task<bool> DeleteAsync(int id);

        public Task<bool> UpdateStatusAsync(int customerId, int patchId, StatusUpdateDto dto);
    }
}
