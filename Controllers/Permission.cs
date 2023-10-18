using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        //private readonly DbContext _context;
             
        public Permission(RoleInterface _Roleuserdata)
        {
            _RoleInterface = _Roleuserdata;

            //_context = context;
        }

        //[HttpPost]
        //public async Task<IActionResult> Createpermission([FromBody] MenuComponents model) 
        //{
        //    var result = await _RoleInterface.Createpermission(model);

        //    if (result.Succeeded)
        //    {
        //        return Ok(new { Message = "Permission registered successfully." });
        //    }
        //    else
        //    {
        //        var errors = result.Errors.Select(error => error.Description).ToList();
        //        return BadRequest(new { Message = "Permission registration failed.", Errors = errors });
        //    }
        //}

        //[HttpGet]

        //public async Task<ActionResult<IEnumerable<Permission>>> Getpermission()
        //{
        //    var getpermission = await _RoleInterface.GetAllPermissionAsync();
        //    return Ok(getpermission);
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuComponents>>> GetMenuComponents()
        {
            var menuComponents = await _RoleInterface.GetMenuComponentsAsync();
            return Ok(menuComponents);

            //var menuComponents = await _RoleInterface.GetMenuComponentsWithPermissionsAsync();
            //return Ok(menuComponents);
        }
    
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuComponents>> GetMenuComponent(int id)
        {
            var menuComponent = await _RoleInterface.GetMenuComponentAsync(id);
            if (menuComponent == null)
            {
                return NotFound();
            }
            return Ok(menuComponent);
        }

        [HttpPost]
        public async Task<ActionResult<MenuComponents>> CreateMenuComponent(MenuComponents menuComponent)
        {
            await _RoleInterface.CreateMenuComponentAsync(menuComponent);
            return CreatedAtAction(nameof(GetMenuComponent), new { id = menuComponent.ComponentID }, menuComponent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuComponent(int id, MenuComponents menuComponent)
        {
            if (id != menuComponent.ComponentID)
            {
                return BadRequest();
            }

            await _RoleInterface.UpdateMenuComponentAsync(menuComponent);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuComponent(int id)
        {
            await _RoleInterface.DeleteMenuComponentAsync(id);
            return NoContent();
        }
    }
}

