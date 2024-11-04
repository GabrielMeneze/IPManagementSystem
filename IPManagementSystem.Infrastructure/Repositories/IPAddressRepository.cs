using IPManagementSystem.Domain.DTO;
using IPManagementSystem.Domain.Entities;
using IPManagementSystem.Domain.Interfaces.repository;
using IPManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPManagementSystem.Infrastructure.Repositories
{
    public class IPAddressRepository : IIPAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public IPAddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IpAddressEntity> GetIPAddressAsync(string ip)
        {
            return await _context.IPAddresses.FirstOrDefaultAsync(i => i.IP == ip);
        }

        public async Task SaveIPAddressAsync(IpAddressEntity ipEntity)
        {
            await _context.IPAddresses.AddAsync(ipEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIPAddressAsync(IpAddressEntity ipEntity)
        {
            _context.IPAddresses.Update(ipEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IpAddressEntity>> GetAllIPAddressesAsync()
        {
            return await _context.IPAddresses.ToListAsync();
        }

        public async Task<IEnumerable<CountryReport>> GetIPReportAsync(IEnumerable<string> countryCodes = null)
        {
            var sqlQuery = @"
            SELECT c.Name AS CountryName, COUNT(ip.Id) AS AddressesCount, MAX(ip.UpdatedAt) AS LastAddressUpdated
            FROM Countries c
            INNER JOIN IPAddresses ip ON c.Id = ip.CountryId
            WHERE (@CountryCodes IS NULL OR c.TwoLetterCode IN (@CountryCodes))
            GROUP BY c.Name";

            var countryCodesParameter = countryCodes != null ? string.Join(",", countryCodes) : null;

            return await _context.Set<CountryReport>().FromSqlRaw(sqlQuery, countryCodesParameter).ToListAsync();
        }
    }
}
