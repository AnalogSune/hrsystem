using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
    public class AdminController : BaseApiController
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAuthRepository _authRepository;
        public AdminController(IAdminRepository adminRepository, IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            _adminRepository = adminRepository;
        }

        [HttpPost("department")]
        [Authorize]
        public async Task<ActionResult<Department>> AddDepartment(DepartmentDto department)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
                return Ok(await _adminRepository.CreateDepartment(department));
            else
                return Unauthorized();
        }

        [HttpPut("department/{id}")]
        [Authorize]
        public async Task<ActionResult<Department>> UpdateDepartment(int id, DepartmentDto department)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return Ok(await _adminRepository.UpdateDepartment(id, department));
            }
            else
                return Unauthorized();
        }

        [HttpDelete("department/{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteDepartment(int id)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                if (await _adminRepository.DeleteDepartment(id))
                    return Ok("Department deleted!");
                return BadRequest("Couldn't delete department!");
            }
            else
                return Unauthorized("Only admin can do that you fucking asshole!");
        }

        [HttpDelete("users/{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                if (await _adminRepository.DeleteUser(id))
                    return Ok("User deleted!");
                return BadRequest("Couldn't delete user!");
            }
            else
                return Unauthorized("Only admin can do that you fucking asshole!");
        }

    }
}