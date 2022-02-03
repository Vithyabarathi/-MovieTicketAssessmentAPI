using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MovieTicketAPI.Models;

namespace MovieTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenAPIController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly MovieAPIDbContext _context;

        public TokenAPIController(IConfiguration config, MovieAPIDbContext context)
        {
            _configuration = config;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post(UserCredentials _userData)
        {

            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Email, _userData.Password);

                if (user != null)
                {
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("Email", user.Email),
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var Login = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: Login);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
        private async Task<UserCredentials> GetUser(string email, string password)
        {
            UserCredentials usercredential = null;
            var result = await _context.usercredentials.Where(u => u.Email == email && u.Password == password).ToListAsync();
            if (result.Count > 0)
                usercredential = result[0];
            return usercredential;

        }

    }
}
