using System.Collections.Generic;

namespace API.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<AppUser> Employees { get; set; }

        public ICollection<Role> DepartmentRoles { get; set; }
    }
}