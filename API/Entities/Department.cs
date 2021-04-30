using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Role> DepartmentRoles { get; set; }
    }
}