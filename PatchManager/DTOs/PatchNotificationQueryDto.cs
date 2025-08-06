using PatchManager.Models;

namespace PatchManager.DTOs
{
    public class PatchNotificationQueryDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string PatchName { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public PatchStatusEnum Status { get; set; } 
    }
}
