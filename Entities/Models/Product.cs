using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Product
{
    [Column("ID")]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string  Name { get; set; } = string.Empty;

    public string?  Description { get; set; }
}
