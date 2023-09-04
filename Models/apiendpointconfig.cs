using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApidotnetcore.Models
{
    public class ApiEndpointInfo
    {
        [Key]
        public int EndpointId { get; set; } // Remove [Key] attribute
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
