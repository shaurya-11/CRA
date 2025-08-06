using PatchManager.Models;

namespace PatchManager.DTOs
{
    public class CustomerPatchQueryDto
    {
        public string CustomerName { get; set; } = "";
        public string ProductName { get; set; } = "";
        public string CustomerPatchVersion { get; set; } = "";
        public string LatestPatchVersion { get; set; } = "";
        public string LatestPatchDescription { get; set; } = "";
        public DateTime LatestPatchReleaseDate { get; set; }
        public PatchStatusEnum PatchStatus { get; set; }
        public string StatusText{ get; set; }
    }
}
