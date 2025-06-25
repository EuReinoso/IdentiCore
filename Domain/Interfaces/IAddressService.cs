using Domain.Entities;

namespace Domain.Interfaces;

public interface IAddressService
{
    Task<IEnumerable<Address>> GetAllAsync();
    Task<IEnumerable<Address>> GetByClientIdAsync(Guid clientId);
    Task<Address?> GetByIdAsync(Guid id);
    Task<Address> CreateAsync(Address address);
    Task<Address> UpdateAsync(Address address);
    Task DeleteAsync(Guid id);
}