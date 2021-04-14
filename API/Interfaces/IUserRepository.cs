using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<MemberDto>> GetUsers();

        Task<MemberDto> GetUser(int id);
        Task<IEnumerable<MemberDto>> GetUsersWithRole(int roleId);

        Task<IEnumerable<MemberDto>> GetUsersWithDepartment(int departmentId);
        Task<IEnumerable<MemberDto>> GetUsersWithParameters(UserFilterDto filters);
        Task<bool> UpdateUser(int id, UserEditDto userEdit);
    }
}