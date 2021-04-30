using System;
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
        public DateTime DateOfBirth { get; set; }
        
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public int? DepartmentId { get; set; }

        public int? RoleId { get; set; }

        public bool? isAdmin { get; set; }

        public string Country { get; set; }

        public string Nationality { get; set; }
    }
}