using PatchManager.Models;

namespace PatchManager.DTOs
{
    public class PatchStatusDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int PatchId { get; set; }
        public PatchStatusEnum Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
