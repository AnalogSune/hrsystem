using System.Collections.Generic;

namespace API.Entities
{
    public class Departments
    {
        public int Id { get; set; }

        public string Department { get; set; }

        public ICollection<AppUser> Employees { get; set; }

        public ICollection<Role> DepartmentRoles { get; set; }
    }
}