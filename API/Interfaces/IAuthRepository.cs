using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces

{
    public interface IAuthRepository
    {
        Task<UserDto> Register(RegisterDto registerDto);
        Task<UserDto> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<bool> IsAdmin(int id);
        Task<bool> ChangePassword(int id, string newPassword);
    }
}