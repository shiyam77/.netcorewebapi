using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public login(CollegeDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context; // Inject CollegeDbContext through constructor
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
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
        new Claim(ClaimTypes.Name, user.Username),
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
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleName)); // Use ClaimTypes.Role

                    var userMenuComponents = _context.MenuComponents
                        .Where(mc => mc.RoleId == roleId)
                        .ToList();

                    var menuComponentDetails = new List<MenuComponents>();

                    foreach (var menuComponent in userMenuComponents)
                    {
                        menuComponentDetails.Add(menuComponent);
                    }

                    var menuComponentJson = JsonConvert.SerializeObject(menuComponentDetails, new JsonSerializerSettings
                    {
                        StringEscapeHandling = StringEscapeHandling.Default // This setting omits backslashes
                    });

                    menuComponentJson = menuComponentJson.Replace("\\", "");

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
