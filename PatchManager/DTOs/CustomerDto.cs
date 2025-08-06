namespace PatchManager.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string ServerIp { get; set; }
        public DateTime LastCheckIn { get; set; }
    }
}
