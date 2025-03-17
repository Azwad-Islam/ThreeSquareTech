using System.ComponentModel.DataAnnotations;

namespace EmployeeLeave.Models
{
    public class EmployeeViewModel
    {
        public string Id { get; set; } //(Guid as String)

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; } 

        public string? Department { get; set; } 
    }
}
