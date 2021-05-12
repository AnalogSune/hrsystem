using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        
#nullable enable
        
        public int? RoleId { get; set; }
        public Role? Role { get; set; }

        public int? DepartmentId { get; set; }
        public Department? InDepartment { get; set; }
#nullable disable
        
        public string PictureUrl { get; set; }
        
        public string PictureId { get; set; }

        public double DaysOffLeft { get; set; }

        public int WorkedFromHome { get; set; }

        public bool IsAdmin { get; set; }

        [Column(TypeName="Date")]
        public DateTime DateStarted { get; set; }

        [Column(TypeName="Date")]
        public DateTime DaysOffLastUpdated { get; set; }
        
        public ICollection<PersonalFile> PersonalFiles { get; set; }

        public AppUser()
        {
            DateStarted = DateTime.Today;
            DaysOffLastUpdated = DateStarted;
        }
    }
}