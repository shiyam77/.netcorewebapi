using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApidotnetcore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class apilist : Controller
    {
        private readonly Interface.ApilistInterface _ApiInterface;

        public apilist(Interface.ApilistInterface appapidata)
        {
            _ApiInterface = appapidata;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<roles>>> GetRoles()
        {
            var apiList = await _ApiInterface.GetApiListAsync();
            return Ok(apiList);
        }
    }
}
