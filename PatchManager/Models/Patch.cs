using PatchManager.Models;

public class Patch
{
    public int Id { get; set; }
    public string Version { get; set; }
    public string FileName { get; set; }
    public string Description { get; set; }
    public DateTime ReleasedOn { get; set; }

    public int ProductId { get; set; } // <-- Add this if missing
    public Product Product { get; set; }

    public ICollection<PatchStatus> PatchStatuses { get; set; }
}
