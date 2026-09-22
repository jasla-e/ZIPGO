using Microsoft.EntityFrameworkCore;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Domain.Entities;
using ZIPGO.Infrastructure.Data;

namespace ZIPGO.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;

        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Address>> GetByUserId(int userId)
        {
            return await _context.Addresses
                .Where(a => a.UserId == userId && !a.IsDeleted)
                .ToListAsync();
        }

        public async Task<Address?> GetById(int id)
        {
            return await _context.Addresses
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task Add(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Address address)
        {
            var existingAddress = await _context.Addresses
                .FindAsync(address.Id);

            if (existingAddress != null)
            {
                existingAddress.FullName = address.FullName;
                existingAddress.Phone = address.Phone;
                existingAddress.HouseArea = address.HouseArea;
                existingAddress.City = address.City;
                existingAddress.State = address.State;
                existingAddress.Pincode = address.Pincode;
                existingAddress.IsDeleted = address.IsDeleted;

                await _context.SaveChangesAsync();
            }
        }

      
    }
}