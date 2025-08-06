namespace PatchAgentService.Models
{
    public class AgentQueryDto
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public bool HasUpdate { get; set; }
        public int? PatchId { get; set; }

        public int PatchStatusId { get; set; }
        public string? Version { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
