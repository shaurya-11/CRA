namespace PatchManager.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string ServerIp { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<PatchStatus> PatchStatuses { get; set; } 
    }
}