using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ZIPGO.Application.DTOs
{
    public class AddressDto
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string HouseArea { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        [RegularExpression(@"^\d{6}$",
            ErrorMessage = "Pincode must be exactly 6 digits")]
        public string Pincode { get; set; }
    }
}