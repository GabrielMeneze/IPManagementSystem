using IPManagementSystem.Domain.DTO;
using IPManagementSystem.Domain.Entities;
using IPManagementSystem.Domain.Interfaces.repository;
using IPManagementSystem.Domain.Interfaces.service;
using Microsoft.Extensions.Caching.Distributed;
using System.Net.Http;
using System.Text.Json;

public class IPAddressService : IIPAddressService
{
    private readonly IIPAddressRepository _ipAddressRepository;
    private readonly IDistributedCache _cache;  
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string IP2CUrl = "https://free.ip2c.org";  

    public IPAddressService(
        IIPAddressRepository ipAddressRepository,
        IDistributedCache cache,
        IHttpClientFactory httpClientFactory)
    {
        _ipAddressRepository = ipAddressRepository;
        _cache = cache;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IpAddressEntity> GetIPAddressInfoAsync(string ip)
    {
        // Step 1: Check cache
        var cachedData = await _cache.GetStringAsync(ip);
        if (cachedData != null)
        {
            return JsonSerializer.Deserialize<IpAddressEntity>(cachedData);
        }

        // Step 2: Check database
        var ipAddressInfo = await _ipAddressRepository.GetIPAddressAsync(ip);
        if (ipAddressInfo != null)
        {
            await _cache.SetStringAsync(ip, JsonSerializer.Serialize(ipAddressInfo));
            return ipAddressInfo;
        }

        // Step 3: Call IP2C external API
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"{IP2CUrl}?ip={ip}");

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var ipInfo = ParseIP2CResponse(jsonString, ip);

            // Save to database
            await _ipAddressRepository.SaveIPAddressAsync(ipInfo);

            // Save to cache
            await _cache.SetStringAsync(ip, JsonSerializer.Serialize(ipInfo));

            return ipInfo;
        }

        return null;
    }

    public async Task UpdateIPAddressesAsync()
    {
        // Obtenha todos os IPs do repositório
        var allIPs = await _ipAddressRepository.GetAllIPAddressesAsync();

        foreach (var ipEntity in allIPs)
        {
            // Obtenha a informação atualizada do IP
            var updatedInfo = await GetIPAddressInfoAsync(ipEntity.IP);
            if (updatedInfo != null && updatedInfo.UpdatedAt > ipEntity.UpdatedAt)
            {
                // Atualize as informações e o timestamp
                ipEntity.Country = updatedInfo.Country;
                ipEntity.UpdatedAt = DateTime.UtcNow;
                await _ipAddressRepository.UpdateIPAddressAsync(ipEntity);

                // Atualize o cache
                await _cache.SetStringAsync(ipEntity.IP, JsonSerializer.Serialize(ipEntity));
            }
        }
    }

    public async Task<IEnumerable<CountryReport>> GetIPReportAsync(IEnumerable<string> countryCodes = null)
    {
        return await _ipAddressRepository.GetIPReportAsync(countryCodes);
    }

    private IpAddressEntity ParseIP2CResponse(string response, string ip)
    {
        var parsedData = JsonSerializer.Deserialize<CountryInfoDto>(response);
        return new IpAddressEntity
        {
            IP = ip,  // Utilize o parâmetro ip aqui
            Country = new Country
            {
                Name = parsedData.CountryName,
                TwoLetterCode = parsedData.TwoLetterCode,
                ThreeLetterCode = parsedData.ThreeLetterCode
            },
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
