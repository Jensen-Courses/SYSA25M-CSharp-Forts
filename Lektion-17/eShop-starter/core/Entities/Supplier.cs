namespace core.Entities;

public class Supplier : BaseEntity
{
    public required string Name { get; set; }
    public required string Phone { get; set; }
    public int CustomerNumber { get; set; }
    public string? WebSite { get; set; }
}
