using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs.Auth
{
    public class UpdateProfileDto
    {

        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public DateTime? Dob { get; set; }



    }
}
