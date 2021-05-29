using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs
{
    public class UserUpdateDto
    {

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(30)]
        public string FName { get; set; }

        [MaxLength(30)]
        public string LName { get; set; }

        [Column(TypeName="Date")]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(12)]
        public string PhoneNumber { get; set; }

        [MaxLength(30)]
        public string Address { get; set; }

        [MaxLength(20)]
        public string Country { get; set; }

        [MaxLength(20)]
        public string Nationality { get; set; }
        
        public int? RoleId { get; set; }

        public int? DepartmentId { get; set; }
    }
}