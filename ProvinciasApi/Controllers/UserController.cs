using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ProvinciasApi.Models;
using ProvinciasApi.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProvinciasApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(ILogger<UserController> logger, IUserService userService, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
        }

        /// <summary>
        /// Validate if the user exist in db
        /// </summary>
        /// <param name="loginRequest">username and password, valid username = test | password = 123456</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginRequest loginRequest)
        {
            _logger.LogInformation(string.Format("Enter Login method with username = {0} password = {1}",loginRequest.Username, loginRequest.Password));
            var user = _userService.Login(loginRequest);

            if (user != null)
            {
                _logger.LogInformation("Successfully login");
                user.Token = GenerateToken(user);
                return Ok(user);
            }
            else
            {
                _logger.LogError("Invalid username / password in Login method");
                return NotFound("Invalid username / password");
            }
        }

        private string GenerateToken(UserDto user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretKey"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);

        }
    }
}
