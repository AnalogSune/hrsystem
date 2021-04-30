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
    [Authorize]
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
        public async Task<IActionResult> AddDepartment(DepartmentDto department)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
                return Ok(await _adminRepository.CreateDepartment(department));
            else
                return Unauthorized("You need administrative rights!");
        }

        [HttpPost("role/{id}/{rolename}")]
        public async Task<IActionResult> AddRole(int id, string rolename)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return Ok(await _adminRepository.AddRole(id, rolename));
            }
            
            return Unauthorized("You need administrative rights!");
        }

        [HttpDelete("role/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return Ok(await _adminRepository.DeleteRole(id));
            }
            
            return Unauthorized("You need administrative rights!");
        }

        [HttpGet("departments")]
        public async Task<IActionResult> GetDepartments()
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return Ok(await _adminRepository.GetDepartments());
            }
            
            return Unauthorized("You need administrative rights!");
        }

        [HttpDelete("department/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return Ok(await _adminRepository.DeleteDepartment(id));
            }
            
            return Unauthorized("You need administrative rights!");
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return Ok(await _adminRepository.DeleteUser(id));
            }
            
            return Unauthorized("You need administrative rights!");
        }

        [HttpPost("dashboard")]
        public async Task<IActionResult> AddPost(DashboardDto dashboardDto)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid))
            {
                return Ok(await _adminRepository.AddPost(dashboardDto));
            }

            return Unauthorized("Only admin can do that you fucking asshole!");
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetPosts()
        {
            return Ok(await _adminRepository.GetPosts());
        }

        [HttpDelete("dashboard/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            return Ok(await _adminRepository.DeletePost(id));
        }

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

        [HttpGet("shift")]
        public async Task<IActionResult> GetShift()
        {
            return Ok(await _adminRepository.GetShifts());
        }

        [HttpDelete("shift/{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            return Ok(await _adminRepository.DeleteShift(id));
        }
    }

}