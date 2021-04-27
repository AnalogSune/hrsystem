using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class DepartmentDto
    {
        public string Name { get; set; }

        public ICollection<RoleDto> DepartmentRoles { get; set; }
    }
}