using Application.DTOs;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class AddressViewModel
{
    public Guid? Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Street { get; set; } = null!;

    [Required]
    public Guid ClientId { get; set; }

    public async Task<UpdateAddressDto> ToUpdateAddressDtoAsync()
    {
        return new UpdateAddressDto()
        {
            Street = Street,
        };
    }

    public async Task<CreateAddressDto> ToCreateAddressDtoAsync()
    {
        return new CreateAddressDto()
        {
            Street = Street,
            ClientId = ClientId,
        };
    }
}
