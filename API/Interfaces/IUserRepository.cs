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

        Task<IEnumerable<MemberDto>> GetUsersWithDepartment(int departmentId);
        Task<IEnumerable<MemberDto>> GetUsersWithParameters(UserFilterDto filters);
        Task<bool> UpdateUser(int id, UserEditDto userEdit);
        Task<bool> ChangeImage(int id, string url, string publicId);
        Task<bool> UploadFile(int id, UploadResult file, string originalFilename);

        Task<bool> RenameFileAsync(PersonalFilesDto personalFilesDto);
        Task<IEnumerable<PersonalFilesDto>> GetFiles(int id);
        Task<PersonalFiles> GetFile(int fileId);
        Task<bool> DeleteFileAsync(int fileId);
    }
}