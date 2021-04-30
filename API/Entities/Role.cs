using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public int DepartmentId { get; set; }

    }
}