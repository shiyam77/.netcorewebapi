using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApidotnetcore.Models
{

    public class RegisterRequestModel
    {
        public CreateUserAndRolesRequest User { get; set; }
        public UserRoles Role { get; set; }
    }


    public class CreateUserAndRolesRequest
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }

    public class UserRoles
    {
        [Key]
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
