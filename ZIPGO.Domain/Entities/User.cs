using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string? Phone { get; set; }

        public string? Gender { get; set; }

        public DateTime? Dob { get; set; }

        public string Role { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
