using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApidotnetcore.Models;
using WebApidotnetcore.Models.data;


namespace WebApidotnetcore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class login : ControllerBase
    {

        private readonly CollegeDbContext _context; // Declare _context here

        public login(CollegeDbContext context)
        {
            _context = context; // Inject CollegeDbContext through constructor
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] CreateUserAndRolesRequest model)
        {
            // Authenticate user (validate credentials)
            var user = AuthenticateUser(model.Username, model.PasswordHash);
            if (user == null)
            {
                return Unauthorized();
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }


        private CreateUserAndRolesRequest AuthenticateUser(string Username, string PasswordHash)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == Username);

            if (user != null && VerifyPassword(user.PasswordHash, PasswordHash))
            {
                return user;
            }

            return null;
        }

        private bool VerifyPassword(string savedHash, string providedPassword)
        {
            // Implement password verification logic using a secure library like BCrypt
            return BCrypt.Net.BCrypt.Verify(providedPassword, savedHash);
        }

        private string GenerateJwtToken(CreateUserAndRolesRequest user)
        {
            var claims = new List<Claim>
    {
        //new Claim(ClaimTypes.NameIdentifier, user.Userid.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        // Add any additional claims here (e.g., roles, permissions)
    };


            var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

            if (string.IsNullOrEmpty(secretKey))
            {
                var keyBytes = new byte[32]; // 256 bits
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(keyBytes);
                }

                secretKey = Convert.ToBase64String(keyBytes);
                Environment.SetEnvironmentVariable("JWT_SECRET_KEY", secretKey);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token expiration time
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
