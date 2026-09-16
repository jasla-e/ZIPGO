using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs
{
    public class UserDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }

    }
}
