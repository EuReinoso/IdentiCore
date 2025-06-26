using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Address
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Street { get; set; } = null!;

    [Required]
    public Guid ClientId { get; set; }

    public Client Client { get; set; } = null!;
}
