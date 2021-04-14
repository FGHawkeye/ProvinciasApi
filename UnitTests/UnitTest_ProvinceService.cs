using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProvinciasApi.Data;
using ProvinciasApi.Models;
using ProvinciasApi.Services.Implementations;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UnitTest_ProvinceService
    {
        private Dictionary<string, string> inMemorySettings = new Dictionary<string, string>
            {
                {"ApiUrl", "https://apis.datos.gob.ar/georef/api/provincias"}
            };

        [TestMethod]
        public void GetProvinceInfo_ValidName()
        {
            var logger = new Mock<ILogger<ProvinceService>>();
            IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            var userService = new ProvinceService(logger.Object, configuration);
            var provinceInfo = userService.GetProvinceInfo("Santiago del Estero");
            Assert.IsNotNull(provinceInfo);
        }

        [TestMethod]
        public void GetProvinceInfo_InvalidName()
        {
            var logger = new Mock<ILogger<ProvinceService>>();
            IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
            var userService = new ProvinceService(logger.Object,configuration);
            var provinceInfo = userService.GetProvinceInfo("afsasf");
            Assert.IsNull(provinceInfo);
        }
    }
}
