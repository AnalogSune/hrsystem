using System.Collections.Generic;

namespace API.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public ICollection<AppUser> Employees { get; set; }
    }
}