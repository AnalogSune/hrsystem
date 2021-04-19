using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 6)]
        public string Password { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public int? DepartmentId { get; set; }

        public int? RoleId { get; set; }

        [Required]
        public bool isAdmin { get; set; }
    }
}