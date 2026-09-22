using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ZIPGO.Application.DTOs.Auth
{
    public class UpdateProfileDto
    {
        [Required]
        public string Name { get; set; }

        [Phone]
        public string? Phone { get; set; }

        public string? Gender { get; set; }

        public DateTime? Dob { get; set; }
    }
}