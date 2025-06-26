using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;

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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetAll()
    {
        var clients = await _clientService.GetAllAsync();
        return Ok(clients);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Client>> GetById(Guid id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client == null)
            return NotFound();

        return Ok(client);
    }

    [HttpPost]
    public async Task<ActionResult<Client>> Create(Client client)
    {
        try
        {
            var created = await _clientService.CreateAsync(client);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Client>> Update(Guid id, Client client)
    {
        if (id != client.Id)
            return BadRequest("ID mismatch");

        try
        {
            var updated = await _clientService.UpdateAsync(client);
            return Ok(updated);
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