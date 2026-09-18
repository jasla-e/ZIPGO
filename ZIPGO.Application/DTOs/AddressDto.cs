using System;
using System.Collections.Generic;
using System.Text;

namespace ZIPGO.Application.DTOs
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string HouseArea { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }

    }
}
