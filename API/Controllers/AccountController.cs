using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helper;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class AccountController : BaseApiController
    {
        private IAuthRepository _authRepository;
        private IMapper _mapper;
        private readonly ILogService _logService;
        private readonly IMailService _mailService;

        public AccountController(IAuthRepository authRepository, IMapper mapper, ILogService LogService,
            IMailService mailService)
        {
            _logService = LogService;
            _mailService = mailService;
            _authRepository = authRepository;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (await _authRepository.UserExists(registerDto.Email)) return BadRequest("Username is taken");

            string adminEmail = await _authRepository.GetEmailById(User.GetId());
            await _logService.RegisterLogFile(registerDto, adminEmail);

            await _mailService.SendMessage("Registration", 
                "<!DOCTYPE html><html><head><meta charset='UTF-8'></head> " +
                $"<body><h1>Welcome to our hr system, {registerDto.FName}! &#128512</h1>" +
                "<p><a href='https://localhost:5001'>Click here to start</a></p></body></html>"
                , registerDto.Email, registerDto.FName);
            
            return Ok(await _authRepository.Register(registerDto));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!await _authRepository.UserExists(loginDto.Email)) return Unauthorized("Wrong Username");

            UserDto user = await _authRepository.Login(loginDto.Email, loginDto.Password);
            await _logService.LoginLogFile(loginDto);

            if (user == null) return BadRequest("Wrong Password");

            return Ok(user);
        }

        [Authorize]
        [HttpPost("password/{id}/{password}")]
        public async Task<IActionResult> ChangePassword(int id, string password)
        {
            if (User.IsAdmin() || User.GetId() == id)
            {
                return Ok(await _authRepository.ChangePassword(id, password));
            }

            return Unauthorized("You need administrative rights to do this!");
        }

        [HttpPost("password/mail/{email}")]
        public async Task<IActionResult> ChangePasswordEmail(string email)
        {
            return Ok(await _authRepository.ChangePasswordEmail(email));
        }

    }
}