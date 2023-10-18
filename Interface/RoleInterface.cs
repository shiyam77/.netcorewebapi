using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApidotnetcore.Models;

namespace WebApidotnetcore.Interface
{
   public interface RoleInterface
    {
    Task<IEnumerable<MenuComponents>> GetMenuComponentsAsync();
    Task<MenuComponents> GetMenuComponentAsync(int id);
    Task CreateMenuComponentAsync(MenuComponents menuComponent);
    Task UpdateMenuComponentAsync(MenuComponents menuComponent);
    Task DeleteMenuComponentAsync(int id);

    //Task<IEnumerable<MenuComponents>> GetMenuComponentsWithPermissionsAsync();

        //Task<IEnumerable<EndpointPermission>> GetEndpointPermissionsAsync();
        //Task<EndpointPermission> GetEndpointPermissionAsync(int id);
        //Task CreateEndpointPermissionAsync(EndpointPermission endpointPermission);
        //Task UpdateEndpointPermissionAsync(EndpointPermission endpointPermission);
        //Task DeleteEndpointPermissionAsync(int id);
    }

}
