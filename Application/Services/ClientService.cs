using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;
public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _clientRepository.GetAllAsync();
    }

    public async Task<Client?> GetByIdAsync(Guid id)
    {
        return await _clientRepository.GetByIdAsync(id);
    }

    public async Task<Client> CreateAsync(Client client)
    {
        var emailExists = await _clientRepository.EmailExistsAsync(client.Email);
        if (emailExists)
            throw new InvalidOperationException("Email is already in use.");

        await _clientRepository.AddAsync(client);
        return client;
    }

    public async Task<Client> UpdateAsync(Client client)
    {
        var existing = await _clientRepository.GetByIdAsync(client.Id);
        if (existing == null)
            throw new KeyNotFoundException("Client not found.");

        var emailInUse = await _clientRepository.EmailExistsAsync(client.Email, client.Id);
        if (emailInUse)
            throw new InvalidOperationException("Email is already in use by another client.");

        existing.Name = client.Name;
        existing.Email = client.Email;
        existing.Logo = client.Logo;

        await _clientRepository.UpdateAsync(existing);
        return existing;
    }

    public async Task DeleteAsync(Guid id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client == null)
            throw new KeyNotFoundException("Client not found.");

        await _clientRepository.DeleteAsync(client);
    }

    public Task<Client?> GetByEmailAsync(string email)
    {
        return _clientRepository.GetByEmailAsync(email);
    }
}