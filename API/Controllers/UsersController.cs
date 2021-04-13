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
        public UsersController(IUserRepository userRepository)
        {
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
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery]UserFilterDto filter)
        {
            return Ok(await _userRepository.GetUsersWithParameters(filter));
        }
    }
}