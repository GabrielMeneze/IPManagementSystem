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

        public async Task<IEnumerable<CountryReport>> GetIPReportAsync(IEnumerable<string> countryCodes = null)
        {
            // Implemente a consulta bruta aqui para o relatório
            return new List<CountryReport>();
        }
    }
}
