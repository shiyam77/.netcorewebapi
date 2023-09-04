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
    public class ApiListRepositry : ApilistInterface
    {
        private readonly CollegeDbContext _context;
        public ApiListRepositry(CollegeDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ApiEndpointInfo>> GetApiListAsync()
        {
            return await _context.ApiEndpoints.ToListAsync();
        }
    }
}
