using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApidotnetcore.Models.data;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;
using WebApidotnetcore.Models;

namespace WebApidotnetcore.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class studentdata : Controller
    {
        private readonly Interface.StudentInterface _productRepository;

        public studentdata(Interface.StudentInterface appStudentdata)
        {
            _productRepository = appStudentdata;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<studentdata>>> GetStudentData()
        {
            var students = await _productRepository.GetAllStudentAsync();
            return Ok(students);
        }


        [HttpGet ("{Id}")]
        public async Task<ActionResult<studentdata>> GetStudent(int Id)
        {
            var student = await _productRepository.GetStudentByIdAsync(Id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<StudentdataModel>> AddStudent(StudentdataModel student)
        {
            await _productRepository.AddStudentAsync(student);
            var createdStudent = CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
            createdStudent.Value = student; 
            return createdStudent;
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateStudent(int id, StudentdataModel student)
        {
            await _productRepository.UpdateStudentAsync(student);
            return Ok("Student updated successfully.");
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            await _productRepository.DeleteStudentAsync(id);
            return Ok("Student Deleted successfully.");
        }


    }
}
