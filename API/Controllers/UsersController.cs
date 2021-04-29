using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IPhotoService _photoService;
        private readonly IFileService _fileService;

        public UsersController(IUserRepository userRepository, IAuthRepository authRepository,
        IPhotoService photoService, IFileService fileService)
        {
            _fileService = fileService;
            _photoService = photoService;
            _authRepository = authRepository;
            _userRepository = userRepository;
        }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        return Ok(await _userRepository.GetUsers());
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<MemberDto>> GetUser(int id)
    {
        return await _userRepository.GetUser(id);
    }

    [Authorize]
    [HttpGet("pending")]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsersWithPending()
    {
        return Ok(await _userRepository.GetUsersWithPending());
    }

    [Authorize]
    [HttpGet("role/{id}")]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsersWithRole(int id)
    {
        return Ok(await _userRepository.GetUsersWithRole(id));
    }

    [Authorize]
    [HttpGet("department/{id}")]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsersWithDepartment(int id)
    {
        return Ok(await _userRepository.GetUsersWithDepartment(id));
    }

    [Authorize]
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<MemberDto>>> SearchUsers([FromQuery] UserFilterDto filter)
    {
        return Ok(await _userRepository.GetUsersWithParameters(filter));
    }

    [Authorize]
    [HttpGet("search/{searchParam}")]
    public async Task<ActionResult<IEnumerable<MemberDto>>> WideSearchUsers(string searchParam)
    {
        return Ok(await _userRepository.GetUsersWithSingleParameters(searchParam));
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateUser(int id, UserEditDto userEdit)
    {
        int uid = RetrieveUserId();

        if (id == uid || await _authRepository.IsAdmin(uid))
            return await _userRepository.UpdateUser(id, userEdit);

        return Unauthorized();
    }

    [Authorize]
    [HttpPut("department/{id}/{departmentId}")]
    public async Task<ActionResult<bool>> ChangeDepartment(int id, int departmentId)
    {
        int uid = RetrieveUserId();

        if (await _authRepository.IsAdmin(uid))
            return await _userRepository.ChangeUserDepartment(id, departmentId);

        return Unauthorized();
    }

    [Authorize]
    [HttpPut("role/{id}/{roleId}")]
    public async Task<ActionResult<bool>> ChangeRole(int id, int roleId)
    {
        int uid = RetrieveUserId();

        if (await _authRepository.IsAdmin(uid))
            return await _userRepository.ChangeUserRole(id, roleId);

        return Unauthorized();
    }

    [Authorize]
    [HttpPost("image")]
    public async Task<ActionResult<bool>> UploadPhoto([FromForm] IFormFile image)
    {
        int uid = RetrieveUserId();

        var user = await _userRepository.GetUser(uid);
        if (!string.IsNullOrEmpty(user.PictureId))
            await _photoService.DeletePhotoAsync(user.PictureId);

        var result = await _photoService.AddPhotoAsync(image);
        await _userRepository.ChangeImage(uid, result.Url.ToString(), result.PublicId);
        
        return Ok(true);
    }

    [Authorize]
    [HttpPost("file")]
    public async Task<ActionResult<bool>> UploadFile([FromForm] IFormFile file)
    {
        int uid = RetrieveUserId();
        string email = (await _userRepository.GetUser(uid)).Email;
        var result = await _fileService.AddFileAsync(file, email);
        await _userRepository.UploadFile(uid, result, file.FileName, file.ContentType);
        return true;
    }
    
    [Authorize]
    [HttpGet("file")]
    public async Task<ActionResult<IEnumerable<PersonalFilesDto>>> GetFiles()
    {
        int uid = RetrieveUserId();
        return Ok(await _userRepository.GetFiles(uid));
    }

    [Authorize]
    [HttpDelete("file/{fileId}")]
    public async Task<IActionResult> DeleteFile(int fileId)
    {
        int uid = RetrieveUserId();
        var file = await _userRepository.GetFile(fileId);
        if (file == null) return Ok("Not found");
        ResourceType type;
        if (file.FileType.Contains("image")) type = ResourceType.Image;
        else if (file.FileType.Contains("video")) type = ResourceType.Video;
        else type = ResourceType.Raw;
        var result = await _fileService.DeleteFileAsync(file.FileId, type);
        await _userRepository.DeleteFileAsync(fileId);
        return Ok(result);
    }

    [Authorize]
    [HttpPut("file")]

    public async Task<ActionResult<bool>> RenameFile(PersonalFilesDto personalFilesDto)
    {
            int uid = RetrieveUserId();
        
        if (uid == personalFilesDto.FileOwnerId)
        {
            return await _userRepository.RenameFileAsync(personalFilesDto);
        }

        return Unauthorized();
    }
}
}