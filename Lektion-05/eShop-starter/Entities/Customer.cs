using System.Diagnostics.CodeAnalysis;

namespace eShop.Entities;

public record Customer
{
    public int CustomerId { get; set; }
    [NotNull]
    public string FirstName { get; set; }
    [NotNull]
    public string LastName { get; set; }
    [NotNull]
    public string Email { get; set; }
}
