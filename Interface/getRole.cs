using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApidotnetcore.Models;

namespace WebApidotnetcore.Interface
{
  public  interface getRole
    {
        Task<IEnumerable<Roles>> GetAllRoleAsync();
    }
}
