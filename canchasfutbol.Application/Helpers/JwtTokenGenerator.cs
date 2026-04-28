using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace canchasfutbol.Application.Helpers
{
    public class JwtTokenGenerator
    {

        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string email, string username, string role)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var secretKey = jwtSettings["Secret"];
            var expirationMinutes = Convert.ToDouble(jwtSettings["ExpirationMinutes"]);

            var claims = new[]
            {
               new Claim(ClaimTypes.Email, email),
               new Claim(ClaimTypes.Name, username),
               new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirationMinutes),
                signingCredentials: creds); 

            return new JwtSecurityTokenHandler().WriteToken(token);



        }

    }
}
