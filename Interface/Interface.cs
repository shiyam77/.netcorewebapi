using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApidotnetcore.Controllers;
using WebApidotnetcore.Models;

namespace WebApidotnetcore.Interface
{
    public interface StudentInterface  
    {
        Task<IEnumerable<StudentdataModel>> GetAllStudentAsync();
        Task<StudentdataModel> GetStudentByIdAsync(int id);
      
        Task UpdateStudentAsync(StudentdataModel Updatestudentdata);
        Task DeleteStudentAsync(int id);
        Task AddStudentAsync(StudentdataModel Addstudentdata);

        Task<IEnumerable<StudentdataModel>> GetStudentsByLastName(string nameFilter);

    }

   
}
