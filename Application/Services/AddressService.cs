using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IClientRepository _clientRepository;

    public AddressService(IAddressRepository addressRepository, IClientRepository clientRepository)
    {
        _addressRepository = addressRepository;
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<Address>> GetAllAsync()
    {
        return await _addressRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Address>> GetByClientIdAsync(Guid clientId)
    {
        return await _addressRepository.GetByClientIdAsync(clientId);
    }

    public async Task<Address?> GetByIdAsync(Guid id)
    {
        return await _addressRepository.GetByIdAsync(id);
    }

    public async Task<Address> CreateAsync(Address address)
    {
        var client = await _clientRepository.GetByIdAsync(address.ClientId);
        if (client == null)
            throw new InvalidOperationException("Client not found.");

        await _addressRepository.AddAsync(address);
        return address;
    }

    public async Task<Address> UpdateAsync(Address address)
    {
        var existing = await _addressRepository.GetByIdAsync(address.Id);
        if (existing == null)
            throw new KeyNotFoundException("Address not found.");

        existing.Street = address.Street;

        await _addressRepository.UpdateAsync(existing);
        return address;
    }

    public async Task DeleteAsync(Guid id)
    {
        var address = await _addressRepository.GetByIdAsync(id);
        if (address == null)
            throw new KeyNotFoundException("Address not found.");

        await _addressRepository.DeleteAsync(address);
    }
}