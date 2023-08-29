using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApidotnetcore.Models
{
    [Table("collegeapplicationsdata")]
    public class StudentdataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email  {get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Degree { get; set; }
        public string Qualification { get; set; }
        public string Gender { get; set; }
        
    }
}
