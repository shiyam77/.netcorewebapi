using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApidotnetcore.Models
{

    //public class RegisterRequestModel
    //{
    //    public CreateUserAndRolesRequest User { get; set; }
    //    public UserRoles Role { get; set; }
    //}


    //public class CreateUserAndRolesRequest
    //{
    //    [Key]
    //    public int UserId { get; set; }
    //    public string Username { get; set; }
    //    public string PasswordHash { get; set; }
    //    public List<UserRoles> Roles { get; set; } = new List<UserRoles>();

    //    //[NotMapped]
    //    //public List<int> MenuComponentIds { get; set; } = new List<int>();
    //}

    //public class UserRoles
    //{
    //    [Key]
    //    public int UserRoleId { get; set; }
    //    public int UserId { get; set; }
    //    public int RoleId { get; set; }
    //    public List<UserRoles> Roles { get; set; } = new List<UserRoles>();

    //}


    //public class Roles
    //{
    //    [Key]
    //    public int RoleId { get; set; }
    //    public string RoleName { get; set; }
    //    //public ICollection<MenuComponents> RolePermissions { get; set; }
    //}
    //public class MenuComponents
    //{
    //    [Key]
    //    public int ComponentID { get; set; }
    //    public int RoleId { get; set; }
    //    public string Name { get; set; }
    //    public bool ReadPermission { get; set; }
    //    public bool WritePermission { get; set; }
    //    public bool DeletePermission { get; set; }
    //    public bool UpdatePermission { get; set; }
    //    public bool AddPermission { get; set; }

    //}


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
        public List<UserRoles> Roles { get; set; } = new List<UserRoles>();
    }

    public class UserRoles
    {
        [Key]
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }

    public class Roles
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class MenuComponents
    {
        [Key]
        public int ComponentID { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool ReadPermission { get; set; }
        public bool WritePermission { get; set; }
        public bool DeletePermission { get; set; }
        public bool UpdatePermission { get; set; }
        public bool AddPermission { get; set; }
    }
}
