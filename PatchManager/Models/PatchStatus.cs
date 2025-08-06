namespace PatchManager.Models
{
    public class PatchStatus
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int PatchId { get; set; }
        public Patch Patch { get; set; }

        public PatchStatusEnum Status { get; set; } // Changed from string to enum
        public DateTime UpdatedAt { get; set; }
    }
}
