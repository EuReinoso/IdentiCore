using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Client
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; }

    public byte[]? Logo { get; set; }

    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}
