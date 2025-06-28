using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetailClientDto>>> GetAll()
    {
        var clients = await _clientService.GetAllAsync();
        return Ok(clients);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DetailClientDto>> GetById(Guid id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client == null)
            return NotFound();

        return Ok(client);
    }

    [HttpPost]
    public async Task<ActionResult<DetailClientDto>> Create([FromBody] UpdateClientDto request)
    {
        try
        {
            var client = new Client
            {
                Name = request.Name,
                Email = request.Email,
                Logo = request.Logo
            };

            var created = await _clientService.CreateAsync(client);

            var result = new DetailClientDto
            {
                Id = created.Id,
                Name = created.Name,
                Email = created.Email,
                Logo = created.Logo
            };

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<DetailClientDto>> Update(Guid id, [FromBody] UpdateClientDto request)
    {
        var client = new Client
        {
            Id = id,
            Name = request.Name,
            Email = request.Email,
            Logo = request.Logo
        };

        if (id != client.Id)
            return BadRequest("ID mismatch");

        try
        {
            var updated = await _clientService.UpdateAsync(client);

            var result = new DetailClientDto
            {
                Id = updated.Id,
                Name = updated.Name,
                Email = updated.Email,
                Logo = updated.Logo
            };

            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _clientService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}