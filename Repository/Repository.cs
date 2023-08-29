using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApidotnetcore.Controllers;
using WebApidotnetcore.Interface;
using WebApidotnetcore.Models;
using WebApidotnetcore.Models.data;

namespace WebApidotnetcore.Repository
{
    public class Repository : StudentInterface
    {
        private readonly CollegeDbContext _context;

        public Repository(CollegeDbContext context) 
        {
            _context = context;
        }

        //public List<Studentdata> _studentdatas = new List<Studentdata>();
    
        public async Task<IEnumerable<StudentdataModel>> GetAllStudentAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task AddStudentAsync(StudentdataModel Addstudentdata)
        {
            _context.Students.Add(Addstudentdata);
            await _context.SaveChangesAsync();
        }

        public async Task<StudentdataModel> GetStudentByIdAsync(int Id)
        {
            return await _context.Students.FindAsync(Id);
        }

        public async Task DeleteStudentAsync(int Id)
        {
            var student =  await _context.Students.FindAsync(Id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateStudentAsync(StudentdataModel Updatestudentdata)
        {
            _context.Students.Update(Updatestudentdata);
            await _context.SaveChangesAsync();
        }

    }

}
