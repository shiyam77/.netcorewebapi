using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApidotnetcore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class roles : ControllerBase
    {
        private readonly Interface.getRole _RoleInterface;

        public roles(Interface.getRole approledata)
        {
            _RoleInterface = approledata;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<roles>>> GetRoles()
        {
            var role = await _RoleInterface.GetAllRoleAsync();
            return Ok(role);
        }
    }
}
