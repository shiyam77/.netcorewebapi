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
        Task<IdentityResult> Createpermission(MenuComponents model);
    }
}
