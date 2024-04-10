using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApidotnetcore.Interface;
using WebApidotnetcore.Models;
using WebApidotnetcore.Models.data;

namespace WebApidotnetcore.Repository
{

    public class RegisterRepository : IRegisterInterface
    {
        private readonly CollegeDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public RegisterRepository(CollegeDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateUserAndAssignRoleAsync(RegisterRequestModel model)
        {
            try
            {
                string hashedPassword = HashPassword(model.User.PasswordHash);

                var user = new CreateUserAndRolesRequest
                {
                    Username = model.User.Username,
                    PasswordHash = hashedPassword
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                var userss = await _userManager.FindByNameAsync(model.Username);
                // Check if the "Admin" role exists, and create it if not
                if (userss != null)
                {
                    // Check if the "Admin" role exists and create it if not
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    }

                    // Assign the "Admin" role to the user
                    await _userManager.AddToRoleAsync(userss ,"Admin");

                    // Rest of your code
                }

                var role = new UserRoles
                {
                    RoleId = model.Role.RoleId,
                    UserId = user.UserId // Use the provided UserId
                };
                _context.UserRoles.Add(role);

                await _context.SaveChangesAsync();                                                                                              

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException;
                var errorMessage = innerException != null ? innerException.Message : ex.Message;
                return IdentityResult.Failed(new IdentityError { Description = errorMessage });
            }
        }
        private string HashPassword(string password)
        {
            // Use a proper password hashing library like BCrypt
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
