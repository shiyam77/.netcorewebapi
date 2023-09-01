using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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
            return BCrypt.Net.BCrypt.Verify(providedPassword, savedHash);
        }

        private string GenerateJwtToken(CreateUserAndRolesRequest user)
        {
            var claims = new List<Claim>
            {
             new Claim("name", user.Username),
            };

            var userRoles = _context.UserRoles
            .Where(ur => ur.UserId == user.UserId)
            .Select(ur => ur.RoleId)
            .ToList();


            foreach (var roleId in userRoles)
            {
                var role = _context.Roles.FirstOrDefault(r => r.RoleId == roleId);
                if (role != null)
                {
                    claims.Add(new Claim("role", role.RoleName));

                    var userMenuComponents = _context.MenuComponents
                   .Where(mc => mc.RoleId == roleId)
                   .ToList();

                    var menuComponentDetails = new List<MenuComponents>();

                    foreach (var menuComponent in userMenuComponents)
                    {
                        // Add menu component details to the list
                        menuComponentDetails.Add(menuComponent);

                        // Add individual claims for menu component permissions if needed
                        claims.Add(new Claim("menuComponent", menuComponent.ComponentID.ToString()));
                    }

                    // Serialize the menu component details to JSON
                    var menuComponentJson = JsonConvert.SerializeObject(menuComponentDetails);

                    // Add a claim with the JSON string to the token
                    claims.Add(new Claim("menuComponents", menuComponentJson));
                }
            }

           


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
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
