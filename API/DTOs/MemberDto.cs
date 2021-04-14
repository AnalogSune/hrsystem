using System;
using API.Entities;

namespace API.DTOs
{
    public class MemberDto
    {
        public string Email { get; set; }

        public string ProfilePicture { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string Nationality { get; set; }

        public string PhoneNumber { get; set; }

        public Department InDepartment { get; set; }

        public Role Role { get; set; }
    }
}