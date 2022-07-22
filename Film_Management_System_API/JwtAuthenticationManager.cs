using Film_Management_System_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Film_Management_System_API
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly string key;
        public JwtAuthenticationManager(string key)
        {
            this.key = key;
        }
        public string Authenticate(string AdminUsername, string AdminPassword)
        {
             Admin a = new Admin();
            if(!(a.AdminUsername == AdminUsername&& a.AdminPassword == AdminPassword))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, AdminUsername)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
