using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<MemberDto>> GetUsers();

        Task<MemberDto> GetUser(int id);
        Task<IEnumerable<MemberDto>> GetUsersWithRole(int roleId);
        Task<IEnumerable<MemberDto>> GetUsersWithPending();
        Task<bool> ChangeUserDepartment(int userId, int departmentId);
        Task<bool> ChangeUserRole(int userId, int roleId);
        Task<IEnumerable<MemberDto>> GetUsersWithDepartment(int departmentId);
        Task<IEnumerable<MemberDto>> GetUsersWithParameters(UserFilterDto filters);
        Task<IEnumerable<MemberDto>> GetUsersWithSingleParameters(string searchParam);
        Task<MemberDto> UpdateUser(int id, UserUpdateDto userEdit);
        Task<bool> ChangeImage(int id, string url, string publicId);
        Task<bool> UploadFile(int id, UploadResult file, string originalFilename, string contentType);

        Task<bool> RenameFileAsync(PersonalFilesDto personalFilesDto);
        Task<IEnumerable<PersonalFilesDto>> GetFiles(int id);
        Task<PersonalFile> GetFile(int fileId);
        Task<bool> DeleteFileAsync(int fileId);
    }
}