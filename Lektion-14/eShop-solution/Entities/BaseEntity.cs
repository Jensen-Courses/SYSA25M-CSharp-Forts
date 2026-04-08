using System.ComponentModel.DataAnnotations;

namespace eShop.Entities;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}
