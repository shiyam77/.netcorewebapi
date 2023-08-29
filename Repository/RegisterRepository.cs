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
       

        public RegisterRepository(CollegeDbContext context)
        {
            _context = context;
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
