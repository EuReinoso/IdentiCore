using Domain.Entities;

namespace Domain.Interfaces;

public interface IAddressRepository
{
    Task<Address?> GetByIdAsync(Guid id);
    Task<IEnumerable<Address>> GetByClientIdAsync(Guid clientId);
    Task<IEnumerable<Address>> GetAllAsync();
    Task AddAsync(Address address);
    Task UpdateAsync(Address address);
    Task DeleteAsync(Address address);
}