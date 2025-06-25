using Domain.Entities;

namespace Domain.Interfaces;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(Guid id);
    Task<Client?> GetByEmailAsync(string email);
    Task<IEnumerable<Client>> GetAllAsync();
    Task AddAsync(Client client);
    Task UpdateAsync(Client client);
    Task DeleteAsync(Client client);
    Task<bool> EmailExistsAsync(string email, Guid? ignoreClientId = null);
}
