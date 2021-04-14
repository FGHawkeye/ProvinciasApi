using Microsoft.Extensions.Logging;
using ProvinciasApi.Data;
using ProvinciasApi.Models;
using ProvinciasApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvinciasApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ProvinceContext _context;
        private readonly ILogger<UserService> _logger;
        public UserService(ILogger<UserService> logger, ProvinceContext context)
        {
            _logger = logger;
            _context = context;
        }

        public UserDto Login(LoginRequest loginRequest)
        {

            try
            {
                var user = _context.User.Where(x => x.Username == loginRequest.Username && x.Password == loginRequest.Password).FirstOrDefault();
                return user != null ? new UserDto { Username = user.Username, Fullname = user.Fullname, Email = user.Email } : null;
            }
            catch(Exception e)
            {
                _logger.LogError("Error in Login Method error message = " + e.Message);
                return null;
            }
        }
    }
}
