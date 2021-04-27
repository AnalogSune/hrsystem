using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Department
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        public ICollection<AppUser> Employees { get; set; }

        public ICollection<Role> DepartmentRoles { get; set; }
    }
}