using System.Collections.Generic;
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
        Task<IEnumerable<Department>> GetDepartments();
        Task<bool> DeleteDepartment(int id);

        Task<bool> DeleteUser(int id);

        Task<bool> AddPost(DashboardDto dashboardDto);
        Task<IEnumerable<DashboardReturnDto>> GetPosts();
        Task<bool> DeletePost(int id);
    }
}