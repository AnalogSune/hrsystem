using System.Threading.Tasks;
using API.DTOs;
using API.Services;

namespace API.Interfaces
{
    public interface IlogService
    {
        Task LoginLogFile(LoginDto loginDto);

        Task RegisterLogFile(RegisterDto registerDto, string adminEmail);

        Task AcceptRequestLogFile(string user, RequestsDto requestsDto, string adminEmail);

        Task RequestMadeLogFile(string user, RequestsDto requestsDto);

        Task UserDeletedLogFile(string user, string admin);

        Task DepartmentsLogFile(DepartmentDto departmentDto, string admin, DepartmentActionType actionType);
    }
}