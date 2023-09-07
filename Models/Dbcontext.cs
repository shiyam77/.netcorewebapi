using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace WebApidotnetcore.Models.data
{

    //: DbContext
    
    public class CollegeDbContext :IdentityDbContext<ApplicationUser>
    { 
        public CollegeDbContext(DbContextOptions<CollegeDbContext> options) : base(options) { }
        public DbSet<StudentdataModel> Students { get; set; }
        public DbSet<CreateUserAndRolesRequest> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<MenuComponents> MenuComponents { get; set; }
        //public DbSet<apiendpointconfig> ApiEndpoints { get; set; }
        public DbSet<ApiEndpointInfo> ApiEndpoints { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Register>().HasNoKey(); // Configure Register as keyless
        //}

    }
}
