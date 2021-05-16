using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
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
        private readonly ILogService _logService;
        public AdminController(IAdminRepository adminRepository, IAuthRepository authRepository, ILogService logService)
        {
            _logService = logService;
            _authRepository = authRepository;
            _adminRepository = adminRepository;
        }

        [HttpPost("department")]
        public async Task<IActionResult> AddDepartment(DepartmentDto department)
        {
            if (User.IsAdmin())
            {
                if (await _adminRepository.DepartmentExists(department))
                    return BadRequest("Department already exists!");

                string adminEmail = await _authRepository.GetEmailById(User.GetId());
                await _logService.DepartmentsLogFile(department, adminEmail, Services.DepartmentActionType.Create);
                
                return Ok(await _adminRepository.CreateDepartment(department));
            }
            else
                return Unauthorized("You need administrative rights!");
        }

        [HttpPost("role/{id}/{rolename}")]
        public async Task<IActionResult> AddRole(int id, string rolename)
        {
            if (User.IsAdmin())
            {
                var dep = await _adminRepository.AddRole(id, rolename);
                if (dep != null)
                    return Ok(dep);
                return BadRequest("Unable to add role, check if role already exists in department!");
            }

            return Unauthorized("You need administrative rights!");
        }

        [HttpDelete("role/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            if (User.IsAdmin())
            {
                
                return Ok(await _adminRepository.DeleteRole(id));
            }

            return Unauthorized("You need administrative rights!");
        }

        [HttpGet("departments")]
        public async Task<IActionResult> GetDepartments()
        {
            return Ok(await _adminRepository.GetDepartments());
        }

        [HttpDelete("department/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (User.IsAdmin())
            {
                string adminEmail = await _authRepository.GetEmailById(User.GetId());
                var department = await _adminRepository.getDepartmentNameById(id);
                await _logService.DepartmentsLogFile(department, adminEmail, Services.DepartmentActionType.Delete);

                return Ok(await _adminRepository.DeleteDepartment(id));
            }

            return Unauthorized("You need administrative rights!");
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (User.IsAdmin())
            {
                string userEmail = await _authRepository.GetEmailById(id);
                string adminEmail = await _authRepository.GetEmailById(User.GetId());
                await _logService.UserDeletedLogFile(userEmail, adminEmail);

                return Ok(await _adminRepository.DeleteUser(id));
            }

            return Unauthorized("You need administrative rights!");
        }

        [HttpPost("dashboard")]
        public async Task<IActionResult> AddPost(DashboardDto dashboardDto)
        {
            if (User.IsAdmin())
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