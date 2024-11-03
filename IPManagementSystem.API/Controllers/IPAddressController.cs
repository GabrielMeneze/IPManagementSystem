using IPManagementSystem.Domain.Interfaces.service;
using Microsoft.AspNetCore.Mvc;

namespace IPManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IPAddressController : ControllerBase
    {
        private readonly IIPAddressService _ipAddressService;

        public IPAddressController(IIPAddressService ipAddressService)
        {
            _ipAddressService = ipAddressService;
        }

        [HttpGet("{ip}")]
        public async Task<IActionResult> GetIPAddressInfo(string ip)
        {
            var ipInfo = await _ipAddressService.GetIPAddressInfoAsync(ip);
            return Ok(ipInfo);
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetIPReport([FromQuery] string[] countryCodes)
        {
            var report = await _ipAddressService.GetIPReportAsync(countryCodes);
            return Ok(report);
        }
    }
}
