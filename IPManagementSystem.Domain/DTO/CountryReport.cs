using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPManagementSystem.Domain.DTO
{
    public class CountryReport
    {
        public string? CountryName { get; set; }
        public int AddressesCount { get; set; }
        public DateTime LastAddressUpdated { get; set; }
    }
}
