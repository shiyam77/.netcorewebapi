using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


namespace WebApidotnetcore.Models.data
{
    public class CollegeDbContext : DbContext
    {
        public CollegeDbContext(DbContextOptions<CollegeDbContext> options) : base(options) { }
        public DbSet<StudentdataModel> Students { get; set; }
        public DbSet<CreateUserAndRolesRequest> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Register>().HasNoKey(); // Configure Register as keyless
        //}
    }
}
