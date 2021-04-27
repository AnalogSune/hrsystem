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

        return Unauthorized("You don't have the rights to edit this user!");
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
    public async Task<ActionResult<string>> UploadFile(IFormFile file)
    {
        int uid = RetrieveUserId();
        string email = (await _userRepository.GetUser(uid)).Email;
        var result = await _fileService.AddFileAsync(file, email);
        await _userRepository.UploadFile(uid, result, file.FileName);
        return Ok(result.Url.ToString());
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
    public async Task<ActionResult<IEnumerable<DeletionResult>>> DeleteFilei(int fileId)
    {
        int uid = RetrieveUserId();
        var file = await _userRepository.GetFile(fileId);
        if (file == null) return Ok("Not found");
        var result = await _fileService.DeleteFileAsync(file.FileId, ResourceType.Raw);
        await _userRepository.DeleteFileAsync(fileId);
        return Ok(result.Result);
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

        return Unauthorized("You don't have the rights to do this!");
    }
}
}