using Microsoft.AspNetCore.Mvc;
using Web.Integration;
using Application.DTOs;
using System.Reflection;
using Web.Models;

namespace Web.Controllers;

public class ClientsController : Controller
{
    private readonly IdentiCoreIntegration _integration;

    public ClientsController(IdentiCoreIntegration integration)
    {
        _integration = integration;
    }

    public async Task<IActionResult> Index()
    {
        var clients = await _integration.GetAllClientsAsync();
        return View(clients);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        if (id == Guid.Empty)
            return View(new ClientViewModel());

        var client = await _integration.GetClientByIdAsync(id);
        if (client == null)
            return NotFound();

        var model = new ClientViewModel
        {
            Id = id,
            Name = client.Name,
            Email = client.Email,
            Logo = client.Logo,
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid? id, ClientViewModel model)
    {
        if (!ModelState.IsValid)
            View(nameof(Edit), model);

        var dto = await model.ToUpdateClientDtoAsync();

        try
        {
            await _integration.CreateClientAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        catch (ApplicationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(nameof(Edit), model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, ClientViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var dto = await model.ToUpdateClientDtoAsync();

        try
        {
            await _integration.UpdateClientAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }
        catch (ApplicationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _integration.DeleteClientAsync(id);
        return RedirectToAction(nameof(Index));
    }
}