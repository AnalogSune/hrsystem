using System.Collections.Generic;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        
#nullable enable
        public int? RoleId { get; set; }
        public Role? EmployeeRole { get; set; }
#nullable disable

        public int DepartmentId { get; set; }
        
        public string ProfilePicture { get; set; }

        public string Address { get; set; }

        public int DaysOffLeft { get; set; }

        public int WorkedFromHome { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Dashboard> Posts { get; set; }

        public ICollection<DaysOffRequest> DaysOffRequests { get; set; }
        
    }
}