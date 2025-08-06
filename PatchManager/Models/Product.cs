using PatchManager.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } 

    public ICollection<Patch> Patches { get; set; } 
}
