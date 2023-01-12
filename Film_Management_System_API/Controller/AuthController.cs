using AutoMapper;
using Film_Management_System_API.DataModels;
using Film_Management_System_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Film_Management_System_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        public static Admin admin=new Admin();
        private readonly MoviesContext _moviesContext;
        private readonly IMapper mapper;
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration, MoviesContext moviesContext, IMapper mapper)
        {
            this.mapper = mapper;
            _configuration = configuration;
            _moviesContext = moviesContext;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<AdminDTO>> Register(AdminDTO request)
        {
            var user = mapper.Map<Admin>(request);
            await _moviesContext.Admins.AddAsync(user);
          
            
            admin.AdminUsernameEmail = request.AdminUsernameEmail;
            admin.AdminPassword = request.AdminPassword;

            await _moviesContext.SaveChangesAsync();
            return Ok(admin);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(AdminDTO request)
        {
            if (admin.AdminUsernameEmail != request.AdminUsernameEmail && admin.AdminPassword!=admin.AdminPassword)
            {
                return BadRequest("Admin not found.");
            }
           
            string token = CreateToken(request);
            Token t = new Token();
            t.token = token;
            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);
            return Ok(t);
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!admin.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (admin.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }
            AdminDTO ad     = new AdminDTO();
            string token = CreateToken(ad);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            return Ok(token);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            admin.RefreshToken = newRefreshToken.Token;
            admin.TokenCreated = newRefreshToken.Created;
            admin.TokenExpires = newRefreshToken.Expires;
        }

        private string CreateToken(AdminDTO request)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,request.AdminUsernameEmail)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt=new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
       /* private void CreatePasswordHash(string password,out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac=new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }
*/
       /* private bool VerifyPasswordHash(string password)
        {
            using (var hmac = new HMACSHA512(admin.AdminPasswordSalt))
            {
                var computedHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }*/
    }
}
