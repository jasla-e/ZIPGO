using ZIPGO.Application.DTOs;

namespace ZIPGO.Application.Interfaces.Services
{
    public interface IAddressService
    {
        Task<List<AddressDto>> GetMyAddresses(int userId);
        Task AddAddress(int userId, AddressDto addressDto);
        Task<bool> UpdateAddress(int userId, int addressId, AddressDto addressDto);
        Task DeleteAddress(int userId, int addressId);
    }
}