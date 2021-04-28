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

        [HttpPost("role/{id}/{rolename}")]
        [Authorize]
        public async Task<ActionResult<Department>> AddRole(int id, string rolename)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return Ok(await _adminRepository.AddRole(id, rolename));
            }
            
            return Unauthorized();
        }

        [HttpDelete("role/{id}")]
        [Authorize]
        public async Task<ActionResult<Department>> DeleteRole(int id)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return Ok(await _adminRepository.DeleteRole(id));
            }
            
            return Unauthorized();
        }

        [HttpGet("departments")]
        [Authorize]
        public async Task<ActionResult<Department>> GetDepartments()
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return Ok(await _adminRepository.GetDepartments());
            }
            
            return Unauthorized();
        }

        [HttpDelete("department/{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteDepartment(int id)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return (await _adminRepository.DeleteDepartment(id));
            }
            
            return Unauthorized();
        }

        [HttpDelete("users/{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return (await _adminRepository.DeleteUser(id));
            }
            
            return Unauthorized();
        }

        [Authorize]
        [HttpPost("dashboard")]

        public async Task<ActionResult<bool>> AddPost(DashboardDto dashboardDto)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return await _adminRepository.AddPost(dashboardDto);
            }

            return Unauthorized("Only admin can do that you fucking asshole!");
        }

        [Authorize]
        [HttpGet("dashboard")]

        public async Task<ActionResult<IEnumerable<Dashboard>>> GetPosts()
        {
            return Ok(await _adminRepository.GetPosts());
        }

        [Authorize]
        [HttpDelete("dashboard/{id}")]

        public async Task<ActionResult<IEnumerable<Dashboard>>> DeletePost(int id)
        {
            return Ok(await _adminRepository.DeletePost(id));
        }

    }

}