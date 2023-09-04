using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApidotnetcore.Interface;
using WebApidotnetcore.Models;
using WebApidotnetcore.Models.data;

namespace WebApidotnetcore.Repository
{
    public class RoleRepositry : getRole
    {
        private readonly CollegeDbContext _context;
        public RoleRepositry(CollegeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Roles>> GetAllRoleAsync()
        {
            return await _context.Roles.ToListAsync();
        }

    }
}
