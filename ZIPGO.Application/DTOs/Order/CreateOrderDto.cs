using System.ComponentModel.DataAnnotations;

namespace ZIPGO.Application.DTOs
{
    public class CreateOrderDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "AddressId must be greater than 0")]
        public int AddressId { get; set; }

        [Required]
        public string PaymentMethod { get; set; } = null!;
    }
}