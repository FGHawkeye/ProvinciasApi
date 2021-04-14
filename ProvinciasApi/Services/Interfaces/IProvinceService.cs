using ProvinciasApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvinciasApi.Services.Interfaces
{
    public interface IProvinceService
    {
        public ProvinceResponse GetProvinceInfo(string provinceName);
    }
}
