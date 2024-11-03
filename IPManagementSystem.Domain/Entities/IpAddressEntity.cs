using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPManagementSystem.Domain.Entities
{
    public class IpAddressEntity
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string? IP { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Country? Country { get; set; }
    }
}

