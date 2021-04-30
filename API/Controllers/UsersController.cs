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
    [Authorize]
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
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await _userRepository.GetUsers());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        return Ok(await _userRepository.GetUser(id));
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetUsersWithPending()
    {
        return Ok(await _userRepository.GetUsersWithPending());
    }

    [HttpGet("role/{id}")]
    public async Task<IActionResult> GetUsersWithRole(int id)
    {
        return Ok(await _userRepository.GetUsersWithRole(id));
    }

    [HttpGet("department/{id}")]
    public async Task<IActionResult> GetUsersWithDepartment(int id)
    {
        return Ok(await _userRepository.GetUsersWithDepartment(id));
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchUsers([FromQuery] UserFilterDto filter)
    {
        return Ok(await _userRepository.GetUsersWithParameters(filter));
    }

    [HttpGet("search/{searchParam}")]
    public async Task<IActionResult> WideSearchUsers(string searchParam)
    {
        return Ok(await _userRepository.GetUsersWithSingleParameters(searchParam));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserEditDto userEdit)
    {
        int uid = RetrieveUserId();

        if (id == uid || await _authRepository.IsAdmin(uid))
            return Ok(await _userRepository.UpdateUser(id, userEdit));

        return Unauthorized();
    }

    [HttpPut("department/{id}/{departmentId}")]
    public async Task<IActionResult> ChangeDepartment(int id, int departmentId)
    {
        int uid = RetrieveUserId();

        if (await _authRepository.IsAdmin(uid))
        {
            if (await _userRepository.ChangeUserDepartment(id, departmentId))
                return Ok();
            return BadRequest("Unable to change department!");
        }

        return Unauthorized();
    }

    [HttpPut("role/{id}/{roleId}")]
    public async Task<IActionResult> ChangeRole(int id, int roleId)
    {
        int uid = RetrieveUserId();

        if (await _authRepository.IsAdmin(uid))
        {
            if (await _userRepository.ChangeUserRole(id, roleId))
                return Ok();
            return BadRequest("Unable to change role!");
        }

        return Unauthorized();
    }

    [HttpPost("image")]
    public async Task<IActionResult> UploadPhoto([FromForm] IFormFile image)
    {
        int uid = RetrieveUserId();
        var user = await _userRepository.GetUser(uid);

        if (!string.IsNullOrEmpty(user.PictureId))
            if ((await _photoService.DeletePhotoAsync(user.PictureId)).Error != null)
                return BadRequest("Unable to delete the previous photo!");

        var result = await _photoService.AddPhotoAsync(image);
        if (await _userRepository.ChangeImage(uid, result.Url.ToString(), result.PublicId))
            return Ok();
        
        return BadRequest("Unable to upload the photo!");
    }

    [HttpPost("file")]
    public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
    {
        int uid = RetrieveUserId();
        string email = (await _userRepository.GetUser(uid)).Email;
        var result = await _fileService.AddFileAsync(file, email);
        await _userRepository.UploadFile(uid, result, file.FileName, file.ContentType);
        return Ok();
    }
    
    [HttpGet("file")]
    public async Task<IActionResult> GetFiles()
    {
        int uid = RetrieveUserId();
        return Ok(await _userRepository.GetFiles(uid));
    }

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
    
    [HttpPut("file")]
    public async Task<IActionResult> RenameFile(PersonalFilesDto personalFilesDto)
    {
            int uid = RetrieveUserId();
        
        if (uid == personalFilesDto.FileOwnerId)
        {
            return Ok(await _userRepository.RenameFileAsync(personalFilesDto));
        }

        return Unauthorized();
    }
}
}