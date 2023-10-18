using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class RolewithPermissionRepositry : RoleInterface
    {
        private readonly CollegeDbContext _context;

        public RolewithPermissionRepositry(CollegeDbContext context)
        {
            _context = context;

        }

        //public async Task<IdentityResult> Createpermission(MenuComponents model)
        //{
        //    try
        //    {
        //        _context.MenuComponents.Add(model);
        //        await _context.SaveChangesAsync();
        //        return IdentityResult.Success;
        //    }
        //    catch (Exception ex)
        //    {
        //        var innerException = ex.InnerException;
        //        var errorMessage = innerException != null ? innerException.Message : ex.Message;
        //        return IdentityResult.Failed(new IdentityError { Description = errorMessage });
        //    }
        //}


        //public async Task<IEnumerable<MenuComponents>> GetAllPermissionAsync()
        //{
        //    return await _context.MenuComponents.ToListAsync();
        //}



        public async Task<IEnumerable<MenuComponents>> GetMenuComponentsAsync()
        {
            //return await _context.MenuComponents.ToListAsync();


            return await _context.MenuComponents
              .Include(mc => mc.Permissions)
              .ToListAsync();
        }

        public async Task<MenuComponents> GetMenuComponentAsync(int id)
        {
            return await _context.MenuComponents.FindAsync(id);
        }

        public async Task CreateMenuComponentAsync(MenuComponents menuComponent)
        {
            _context.MenuComponents.Add(menuComponent);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMenuComponentAsync(MenuComponents menuComponent)
        {
            _context.Entry(menuComponent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMenuComponentAsync(int id)
        {
            var menuComponent = await _context.MenuComponents.FindAsync(id);
            if (menuComponent != null)
            {
                _context.MenuComponents.Remove(menuComponent);
                await _context.SaveChangesAsync();
            }
        }


        //public async Task<IEnumerable<MenuComponents>> GetMenuComponentsWithPermissionsAsync()
        //{
        //    return await _context.MenuComponents
        //        .Include(mc => mc.Permissions) // Load related permissions
        //        .ToListAsync();
        //}


        //public async Task<IEnumerable<EndpointPermission>> GetEndpointPermissionsAsync()
        //{
        //    return await _context.EndpointPermissions.ToListAsync();
        //}
        //public async Task<EndpointPermission> GetEndpointPermissionAsync(int id)
        //{
        //    return await _context.EndpointPermissions.FindAsync(id);
        //}
        //public async Task CreateEndpointPermissionAsync(EndpointPermission endpointPermission)
        //{
        //    _context.EndpointPermissions.Add(endpointPermission);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task UpdateEndpointPermissionAsync(EndpointPermission endpointPermission)
        //{
        //    _context.Entry(endpointPermission).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //}


        //public async Task DeleteEndpointPermissionAsync(int id)
        //{
        //    var endpointPermission = await _context.EndpointPermissions.FindAsync(id);
        //    if (endpointPermission != null)
        //    {
        //        _context.EndpointPermissions.Remove(endpointPermission);
        //        await _context.SaveChangesAsync();
        //    }
        //}


        //public async Task<IEnumerable<EndpointPermission>> GetAllPermissionAsync()
        //{
        //    return await _context.EndpointPermissions.ToListAsync();
        //}


    }
}
