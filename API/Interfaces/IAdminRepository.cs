using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IAdminRepository
    {
        Task<bool> CreateDepartment(DepartmentDto department);
        Task<bool> UpdateDepartment(int departmentId, DepartmentDto department);
        Task<bool> DeleteDepartment(int id);

        Task<bool> DeleteUser(int id);
    }
}