using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string Nationality { get; set; }
        
#nullable enable
        public int? RoleId { get; set; }
        public Role? Role { get; set; }

        public int? DepartmentId { get; set; }
        public Department? InDepartment { get; set; }
#nullable disable
        
        public string PictureUrl { get; set; }
        public string PictureId { get; set; }

        public int DaysOffLeft { get; set; }

        public int WorkedFromHome { get; set; }

        public bool IsAdmin { get; set; }
        
        public ICollection<PersonalFiles> PersonalFiles { get; set; }
    }
}