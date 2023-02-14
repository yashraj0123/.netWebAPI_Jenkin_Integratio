using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public class JwtServices:IJwtServices
    {
        private readonly JwtSecurityTokenHandler tokenService = new JwtSecurityTokenHandler();
        public string Key { get; set; }
        public int TokenDuration { get; set; }

        private readonly IConfiguration config;
        public JwtServices(IConfiguration _config)
        {
            config = _config;
            this.Key = config.GetSection("jwtConfig").GetSection("Key").Value;
            this.TokenDuration = Int32.Parse(config.GetSection("jwtConfig").GetSection("Duration").Value);     
        }       
        public string GenerateToken(string id, string email, string name)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Key));
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var payload = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim("id", id),
                new Claim("email",email),
                 new Claim("name",name)
            };
            var tokendes = new JwtSecurityToken(
                issuer:"localhost",
                audience: "localhost",
                claims: payload,
                expires: DateTime.Now.AddMinutes(TokenDuration),
                signingCredentials: signature
            );

            return new JwtSecurityTokenHandler().WriteToken(tokendes);
        }
        public JwtSecurityToken Decode(string token)
        {
            var t= tokenService.ReadJwtToken(token);
            Console.WriteLine(t);
            return t;
            
        }
    }
}
