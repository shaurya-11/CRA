namespace PatchManager.DTOs
{
    public class PatchDto
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public DateTime ReleasedOn { get; set; }
        public int ProductId { get; set; }
    }
}
