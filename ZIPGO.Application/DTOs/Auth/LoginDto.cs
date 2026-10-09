using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ZIPGO.Application.DTOs.Auth
{
    public class LoginDto
    {
        
        public string Email { get; set; }

       
        public string Password { get; set; }
    }
}