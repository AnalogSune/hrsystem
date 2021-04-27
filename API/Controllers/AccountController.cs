using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private IAuthRepository _authRepository;
        private IMapper _mapper;

        public AccountController(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _authRepository.UserExists(registerDto.Email)) return BadRequest("Username is taken");
            
            return await _authRepository.Register(registerDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            if (!await _authRepository.UserExists(loginDto.Email)) return Unauthorized("Wrong Username");
            
            UserDto user =  await _authRepository.Login(loginDto.Email, loginDto.Password);

            if (user == null) return Unauthorized("Wrong Password");
            
            return user;
        }
        
    }
}