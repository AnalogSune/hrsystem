using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        [MaxLength(30)]
        public string FName { get; set; }

        [MaxLength(30)]
        public string LName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [MaxLength(12)]
        public string PhoneNumber { get; set; }

        [MaxLength(30)]
        public string Address { get; set; }

        [MaxLength(20)]
        public string Country { get; set; }

        [MaxLength(20)]
        public string Nationality { get; set; }
        
#nullable enable
        public int? RoleId { get; set; }
        public Role? Role { get; set; }

        public int? DepartmentId { get; set; }
        public Department? InDepartment { get; set; }
#nullable disable
        
        [MaxLength(100)]
        public string PictureUrl { get; set; }
        
        [MaxLength(100)]
        public string PictureId { get; set; }

        public int DaysOffLeft { get; set; }

        public int WorkedFromHome { get; set; }

        public bool IsAdmin { get; set; }
        
        public ICollection<PersonalFiles> PersonalFiles { get; set; }
    }
}