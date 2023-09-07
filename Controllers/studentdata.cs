using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApidotnetcore.Models.data;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;
using WebApidotnetcore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApidotnetcore.Controllers

{

    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
   
    public class studentdata : Controller
    {
        private readonly Interface.StudentInterface _productRepository;
        private readonly ILogger<studentdata> _logger;

        public studentdata(Interface.StudentInterface appStudentdata, ILogger<studentdata> logger)
        {
            _productRepository = appStudentdata;
            _logger = logger;
        }

       
        [HttpGet]

      
        public async Task<ActionResult<IEnumerable<studentdata>>> GetStudentData()
        {
            try
            {
                var user = HttpContext.User;
                var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Substring("Bearer ".Length);

                // Log the received token
                _logger.LogInformation($"Received token: {token}");

                var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                //var roles = user.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).ToList();
                _logger.LogInformation($"User roles: {string.Join(", ", roles)}");
            
                if (roles.Contains("Admin"))
                {
                    var students = await _productRepository.GetAllStudentAsync();
                _logger.LogInformation("Retrieved all student data."); // Log an information message
                return Ok(students);
                     }
                else
                {
                    // User is not authorized
                    return StatusCode(403, "Access denied. You do not have the 'Admin' role.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving student data."); // Log an error message with the exception
                return StatusCode(500, "An error occurred while processing your request.");
            }
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
