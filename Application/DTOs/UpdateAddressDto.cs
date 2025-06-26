using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs;

public class UpdateAddressDto
{
    [Required]
    [MaxLength(255)]
    public string Street { get; set; } = null!;
}
