using Microsoft.AspNetCore.Mvc;
using ProvinciasApi.Models;
using ProvinciasApi.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace ProvinciasApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceService _provinceService;
        private readonly ILogger<ProvinceController> _logger;
        public ProvinceController(ILogger<ProvinceController> logger, IProvinceService provinceService)
        {
            _logger = logger;
            _provinceService = provinceService;
        }

        /// <summary>
        /// return the latitude and longitude from a province
        /// </summary>
        /// <param name="provinceName">examples: Santiago del Estero | Stgo.del Estero | S.del Estero | Sgo. del Estero</param>
        [HttpGet]
        [Authorize]
        public IActionResult ProvinceInfo(string provinceName)
        {
            _logger.LogInformation("Enter ProvinceInfo method with provinceName = " +provinceName);
            var provinceInfo = _provinceService.GetProvinceInfo(provinceName);
            if (provinceInfo != null)
            {
                _logger.LogInformation(string.Format("Province info obtained, lat = {0} | lon = {1}", provinceInfo.Latitude, provinceInfo.Longitude));
                return Ok(provinceInfo);
            }
            else
            {
                _logger.LogError("Info not found");
                return NotFound("Info not found, try another name");
            }
        }
    }
}
