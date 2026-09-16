using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.DTOs.Auth;

namespace ZIPGO.Application.Interfaces.Services
{
    public interface IAuthService
    {

        Task Register(RegisterDto registerDto);

        Task<string?> Login(LoginDto loginDto);


    }
}
