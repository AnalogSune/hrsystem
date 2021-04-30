using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize]
        [HttpPost("department")]
        public async Task<ActionResult<Department>> AddDepartment(DepartmentDto department)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
                return Ok(await _adminRepository.CreateDepartment(department));
            else
                return Unauthorized();
        }

        [Authorize]
        [HttpPost("role/{id}/{rolename}")]
        public async Task<ActionResult<Department>> AddRole(int id, string rolename)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return Ok(await _adminRepository.AddRole(id, rolename));
            }
            
            return Unauthorized();
        }

        [Authorize]
        [HttpDelete("role/{id}")]
        public async Task<ActionResult<Department>> DeleteRole(int id)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return Ok(await _adminRepository.DeleteRole(id));
            }
            
            return Unauthorized();
        }

        [Authorize]
        [HttpGet("departments")]
        public async Task<ActionResult<Department>> GetDepartments()
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return Ok(await _adminRepository.GetDepartments());
            }
            
            return Unauthorized();
        }

        [Authorize]
        [HttpDelete("department/{id}")]
        public async Task<ActionResult<bool>> DeleteDepartment(int id)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return (await _adminRepository.DeleteDepartment(id));
            }
            
            return Unauthorized();
        }

        [Authorize]
        [HttpDelete("users/{id}")]
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
        public async Task<ActionResult<bool>> DeletePost(int id)
        {
            return Ok(await _adminRepository.DeletePost(id));
        }

        [Authorize]
        [HttpPost("shift")]
        public async Task<IActionResult> CreateShift(WorkShiftCreationDto shiftCreationDto)
        {
            if (shiftCreationDto.Duration <= 0 || string.IsNullOrEmpty(shiftCreationDto.Name))
                return BadRequest("Invalid Shift!");
            var newShift = await _adminRepository.CreateShift(shiftCreationDto);
            if (newShift != null)
                return Ok(newShift);
            
            return BadRequest("Couldn't create the work shift!");
        }

        [Authorize]
        [HttpGet("shift")]
        public async Task<IActionResult> GetShift()
        {
            return Ok(await _adminRepository.GetShifts());
        }

        [Authorize]
        [HttpDelete("shift/{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            return Ok(await _adminRepository.DeleteShift(id));
        }
    }

}