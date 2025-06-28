using Domain.Entities;

namespace Domain.Interfaces;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(Guid id);
    Task<Client?> GetByEmailAsync(string Email);
    Task<Client> CreateAsync(Client client);
    Task<Client> UpdateAsync(Client client);
    Task DeleteAsync(Guid id);
}
