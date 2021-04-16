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
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateUser(int id, UserEditDto userEdit)
    {
        int uid = int.Parse(User.Claims.FirstOrDefault().Value);

        if (id == uid || await _authRepository.IsAdmin(uid))
            return await _userRepository.UpdateUser(id, userEdit);

        return Unauthorized("You don't have the rights to edit this user!");
    }

    [Authorize]
    [HttpPost("add-image")]
    public async Task<ActionResult<string>> UploadPhoto(IFormFile image)
    {
        int uid = int.Parse(User.Claims.FirstOrDefault().Value);

        var user = await _userRepository.GetUser(uid);
        if (!string.IsNullOrEmpty(user.PictureId))
            await _photoService.DeletePhotoAsync(user.PictureId);

        var result = await _photoService.AddPhotoAsync(image);
        await _userRepository.ChangeImage(uid, result.Url.ToString(), result.PublicId);
        
        return Ok(result.Url.ToString());
    }

    [Authorize]
    [HttpPost("add-file")]
    public async Task<ActionResult<string>> UploadFile(IFormFile file)
    {
        int uid = int.Parse(User.Claims.FirstOrDefault().Value);
        string email = (await _userRepository.GetUser(uid)).Email;
        var result = await _fileService.AddFileAsync(file, email);
        await _userRepository.UploadFile(uid, result, file.FileName);
        return Ok(result.Url.ToString());
    }
    
    [Authorize]
    [HttpGet("file")]
    public async Task<ActionResult<IEnumerable<PersonalFilesDto>>> GetFiles()
    {
        int uid = int.Parse(User.Claims.FirstOrDefault().Value);
        return Ok(await _userRepository.GetFiles(uid));
    }

    [Authorize]
    [HttpDelete("file/{fileId}")]
    public async Task<ActionResult<IEnumerable<DeletionResult>>> DeleteFilei(int fileId)
    {
        int uid = int.Parse(User.Claims.FirstOrDefault().Value);
        var file = await _userRepository.GetFile(fileId);
        if (file == null) return Ok("Not found");
        var result = await _fileService.DeleteFileAsync(file.FileId, ResourceType.Raw);
        await _userRepository.DeleteFileAsync(fileId);
        return Ok(result.Result);
    }

    [Authorize]
    [HttpPut("rename-file")]

    public async Task<ActionResult<bool>> RenameFile(PersonalFilesDto personalFilesDto)
    {
        int uid = int.Parse(User.Claims.FirstOrDefault().Value);
        
        if (uid == personalFilesDto.FileOwnerId)
        {
            return await _userRepository.RenameFileAsync(personalFilesDto);
        }

        return Unauthorized("You don't have the rights to do this!");
    }
}
}