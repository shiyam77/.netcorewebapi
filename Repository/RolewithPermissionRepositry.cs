using Microsoft.AspNetCore.Http;
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
    public class RolewithPermissionRepositry : RoleInterface
    {
        private readonly CollegeDbContext _context;

        public RolewithPermissionRepositry(CollegeDbContext context)
        {
            _context = context;
        }

        public async Task<IdentityResult> Createpermission(MenuComponents model)
        {
            try
            {
                _context.MenuComponents.Add(model);
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
    }
}
