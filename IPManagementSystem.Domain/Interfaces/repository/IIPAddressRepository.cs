using IPManagementSystem.Domain.DTO;
using IPManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPManagementSystem.Domain.Interfaces.repository
{
    public interface IIPAddressRepository
    {
        Task<IpAddressEntity> GetIPAddressAsync(string ip);
        Task SaveIPAddressAsync(IpAddressEntity ipEntity);
        Task UpdateIPAddressAsync(IpAddressEntity ipEntity);
        Task<IEnumerable<CountryReport>> GetIPReportAsync(IEnumerable<string> countryCodes = null);
    }
}
