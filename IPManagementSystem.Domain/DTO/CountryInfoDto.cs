using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPManagementSystem.Domain.DTO
{
    public class CountryInfoDto
    {
        public string? CountryName { get; set; }
        public string? TwoLetterCode { get; set; }
        public string? ThreeLetterCode { get; set; }
    }
}
