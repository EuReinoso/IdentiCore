using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs;

public class DetailAddressDto
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Street { get; set; } = null!;

    [Required]
    public Guid ClientId { get; set; }
}