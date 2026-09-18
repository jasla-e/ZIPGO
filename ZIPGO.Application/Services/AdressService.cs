using ZIPGO.Application.DTOs;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Application.Interfaces.Services;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<List<AddressDto>> GetMyAddresses(int userId)
        {
            var addresses = await _addressRepository.GetByUserId(userId);

            return addresses.Select(a => new AddressDto
            {
                Id=a.Id,
                FullName = a.FullName,
                Phone = a.Phone,
                HouseArea = a.HouseArea,
                City = a.City,
                State = a.State,
                Pincode = a.Pincode
            }).ToList();
        }

        public async Task AddAddress(int userId, AddressDto addressDto)
        {
            var address = new Address
            {
                UserId = userId,
                FullName = addressDto.FullName,
                Phone = addressDto.Phone,
                HouseArea = addressDto.HouseArea,
                City = addressDto.City,
                State = addressDto.State,
                Pincode = addressDto.Pincode
            };

            await _addressRepository.Add(address);
        }

        public async Task<bool> UpdateAddress(
        int userId,
        int addressId,
        AddressDto addressDto)
        {
            var existingAddress = await _addressRepository.GetById(addressId);

            if (existingAddress == null || existingAddress.UserId != userId)
                return false;

            existingAddress.FullName = addressDto.FullName;
            existingAddress.Phone = addressDto.Phone;
            existingAddress.HouseArea = addressDto.HouseArea;
            existingAddress.City = addressDto.City;
            existingAddress.State = addressDto.State;
            existingAddress.Pincode = addressDto.Pincode;

            await _addressRepository.Update(existingAddress);

            return true;
        }

        public async Task DeleteAddress(int userId, int addressId)
        {
            var address = await _addressRepository.GetById(addressId);

            if (address == null || address.UserId != userId)
                return;

            await _addressRepository.Delete(addressId);
        }
    }
}