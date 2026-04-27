using System.ComponentModel.DataAnnotations;

namespace core.Entities;

public abstract class BaseEntity
{
    [Key]
    public string Id { get; set; } =
        Guid.NewGuid().ToString().Replace("-", "");
}
