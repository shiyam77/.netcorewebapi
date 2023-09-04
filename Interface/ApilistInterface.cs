using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApidotnetcore.Models;

namespace WebApidotnetcore.Interface
{
    public interface ApilistInterface
    {
        Task<IEnumerable<ApiEndpointInfo>> GetApiListAsync();
    }
}
