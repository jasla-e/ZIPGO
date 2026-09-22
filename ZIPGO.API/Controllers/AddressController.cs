using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZIPGO.Application.DTOs;
using ZIPGO.Application.Interfaces.Services;

namespace ZIPGO.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        private int GetUserId()
        {
            return int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetMyAddresses()
        {
            var userId = GetUserId();

            var addresses = await _addressService.GetMyAddresses(userId);

            return Ok(addresses);
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(AddressDto addressDto)
        {
            var userId = GetUserId();

            await _addressService.AddAddress(userId, addressDto);

            return Ok("Address added successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(
        int id,
        AddressDto addressDto)
        {
            var userId = GetUserId();

            var updated = await _addressService.UpdateAddress(
                userId,
                id,
                addressDto
            );

            if (!updated)
                return NotFound("Address not found.");

            return Ok("Address updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var userId = GetUserId();
            
            var deleted = await _addressService.DeleteAddress(userId, id);

            if (!deleted)
                return NotFound("Address not found.");

            return Ok("Address deleted successfully");
        }
    }
}