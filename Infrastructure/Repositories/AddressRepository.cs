using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _context;

    public AddressRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Address?> GetByIdAsync(Guid id)
    {
        return await _context.Addresses
            .Include(a => a.Client)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Address>> GetByClientIdAsync(Guid clientId)
    {
        return await _context.Addresses
            .Where(a => a.ClientId == clientId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Address>> GetAllAsync()
    {
        return await _context.Addresses
            .Include(a => a.Client)
            .ToListAsync();
    }

    public async Task AddAsync(Address address)
    {
        await _context.Addresses.AddAsync(address);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Address address)
    {
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Address address)
    {
        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();
    }
}