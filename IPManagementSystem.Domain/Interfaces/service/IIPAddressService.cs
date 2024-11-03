using IPManagementSystem.Domain.DTO;
using IPManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPManagementSystem.Domain.Interfaces.service
{
    public interface IIPAddressService
    {
        Task<IpAddressEntity> GetIPAddressInfoAsync(string ip);  
        Task UpdateIPAddressesAsync();
        Task<IEnumerable<CountryReport>> GetIPReportAsync(IEnumerable<string> countryCodes = null);
    }
}
