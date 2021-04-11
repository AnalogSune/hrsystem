using System.Collections.Generic;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        
#nullable enable
        public int? RoleId { get; set; }
        public Role? Role { get; set; }

        public int? DepartmentId { get; set; }
        public Departments? Department { get; set; }
#nullable disable
        
        public string ProfilePicture { get; set; }

        public int DaysOffLeft { get; set; }

        public int WorkedFromHome { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Dashboard> Posts { get; set; }

        public ICollection<DaysOffRequest> DaysOffRequests { get; set; }
        
    }
}