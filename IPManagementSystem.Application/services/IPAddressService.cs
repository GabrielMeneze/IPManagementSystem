using IPManagementSystem.Domain.DTO;
using IPManagementSystem.Domain.Entities;
using IPManagementSystem.Domain.Interfaces.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPManagementSystem.Application.services
{
    public class IPAddressService : IIPAddressService
    {
        public Task<IpAddressEntity> GetIPAddressInfoAsync(string ip)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CountryReport>> GetIPReportAsync(IEnumerable<string> countryCodes = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateIPAddressesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
