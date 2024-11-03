using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPManagementSystem.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? TwoLetterCode { get; set; }
        public string? ThreeLetterCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<IpAddressEntity> IPAddresses { get; set; }
    }
}
