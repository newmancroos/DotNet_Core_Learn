using System.ComponentModel.DataAnnotations;

namespace DotNetCore_Concepts.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public int Department { get; set; }
    }
}
