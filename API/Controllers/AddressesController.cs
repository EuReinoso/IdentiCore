using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressesController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressesController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UpdateAddressDto>>> GetAll()
    {
        var addresses = await _addressService.GetAllAsync();
        return Ok(addresses);
    }

    [HttpGet("client/{clientId:guid}")]
    public async Task<ActionResult<IEnumerable<DetailAddressDto>>> GetByClientId(Guid clientId)
    {
        var addresses = await _addressService.GetByClientIdAsync(clientId);
        return Ok(addresses);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DetailAddressDto>> GetById(Guid id)
    {
        var address = await _addressService.GetByIdAsync(id);
        if (address == null)
            return NotFound();

        return Ok(address);
    }

    [HttpPost]
    public async Task<ActionResult<DetailAddressDto>> Create([FromBody] CreateAddressDto dto)
    {
        var address = new Address()
        {
            Street = dto.Street,
            ClientId = dto.ClientId
        };

        var created = await _addressService.CreateAsync(address);

        var result = new DetailAddressDto()
        {
            Id = created.Id,
            ClientId = created.ClientId,
            Street = created.Street
        };

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<DetailAddressDto>> Update(Guid id, [FromBody] UpdateAddressDto request)
    {
        try
        {
            var existing = await _addressService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            var address = new Address()
            {
                Id = id,
                Street = request.Street,
                ClientId = existing.ClientId
            };

            var updated = await _addressService.UpdateAsync(address);

            var result = new DetailAddressDto()
            {
                Id = updated.Id,
                ClientId = updated.ClientId,
                Street = updated.Street
            };

            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _addressService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}