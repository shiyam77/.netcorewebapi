using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApidotnetcore.Models;

namespace WebApidotnetcore.Interface
{
    public interface IRegisterInterface
    {
        Task<IdentityResult> CreateUserAndAssignRoleAsync(RegisterRequestModel model);
    }

}