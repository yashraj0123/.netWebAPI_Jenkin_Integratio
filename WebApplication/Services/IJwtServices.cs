using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public interface IJwtServices
    {
        public JwtSecurityToken Decode(string token);
        
        public string GenerateToken(string id, string email, string name);
    }
}
