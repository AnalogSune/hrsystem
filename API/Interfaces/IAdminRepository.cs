using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface IAdminRepository
    {
        Task<bool> CreateDepartment(DepartmentDto department);
    }
}