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
    public class Registeruser : ControllerBase
    {
        private readonly IRegisterInterface _registerInterface;

        public Registeruser(IRegisterInterface _Registeruserdata)
        {
            _registerInterface = _Registeruserdata;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestModel model)
        {
            var userRoles = new List<UserRoles>(); // Populate user roles

            var result = await _registerInterface.CreateUserAndAssignRoleAsync(model);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully." });
            }
            else
            {
                var errors = result.Errors.Select(error => error.Description).ToList();
                return BadRequest(new { Message = "User registration failed.", Errors = errors });
            }
        }




    }
}
