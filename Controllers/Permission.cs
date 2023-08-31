using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApidotnetcore.Interface;
using WebApidotnetcore.Models;

namespace WebApidotnetcore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Permission : ControllerBase
    {
        private readonly RoleInterface _RoleInterface;

        public Permission(RoleInterface _Roleuserdata)
        {
            _RoleInterface = _Roleuserdata;
        }

        [HttpPost]
        public async Task<IActionResult> Createpermission([FromBody] MenuComponents model) 
        {
            var result = await _RoleInterface.Createpermission(model);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Permission registered successfully." });
            }
            else
            {
                var errors = result.Errors.Select(error => error.Description).ToList();
                return BadRequest(new { Message = "Permission registration failed.", Errors = errors });
            }
        }
    }
}
