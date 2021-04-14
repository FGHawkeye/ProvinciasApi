using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using ProvinciasApi.Models;
using ProvinciasApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProvinciasApi.Services.Implementations
{
    public class ProvinceService : IProvinceService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<ProvinceService> _logger;
        public ProvinceService(ILogger<ProvinceService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public ProvinceResponse GetProvinceInfo(string provinceName)
        {
            _logger.LogInformation("Enter GetProvinceInfo method with provinceName = " + provinceName);
            try
            {
                var uri = _config.GetValue<string>("ApiUrl");
                var parameters = String.Format("?nombre={0}", provinceName);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uri);
                    var responseTask = client.GetAsync(parameters);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        dynamic response = JObject.Parse(readTask.Result);
                        if(response.cantidad > 0)
                        {
                            var provinceResponse = new ProvinceResponse
                            {
                                Latitude = response.provincias[0].centroide.lat,
                                Longitude = response.provincias[0].centroide.lon
                            };
                            return provinceResponse;
                        }
                        else
                            _logger.LogError("Error in GetProvinceInfo Method - PROVINCE NAME NOT FOUND");

                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error in GetProvinceInfo Method error message = " + e.Message);
            }
            return null;
        }
    }
}
