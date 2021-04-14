using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;
        public UsersController(IUserRepository userRepository, IAuthRepository authRepository)
        {
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

    }
}