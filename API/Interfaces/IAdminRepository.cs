using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IAdminRepository
    {
        Task<bool> DepartmentExists(DepartmentDto department);
        Task<Department> CreateDepartment(DepartmentDto department);
        Task<Department> UpdateDepartment(int departmentId, DepartmentDto department);
        Task<bool> DeleteDepartment(int id);

        Task<bool> DeleteUser(int id);
    }
}