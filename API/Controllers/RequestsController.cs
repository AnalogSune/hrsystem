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
    public class RequestsController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IRequestsRepository _requestsRepository;
        private readonly IAdminRepository _adminRepository;
        public RequestsController(IUserRepository userRepository, IAuthRepository authRepository, IRequestsRepository requestsRepository, IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
            _requestsRepository = requestsRepository;
            _authRepository = authRepository;
            _userRepository = userRepository;

        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateRequest(RequestsDto requestsDto)
        {
            int uid = int.Parse(User.Claims.FirstOrDefault().Value);
            if (requestsDto.EmployeeId == uid || await _authRepository.IsAdmin(uid))
                return await _requestsRepository.CreateRequest(requestsDto);
            return Unauthorized("You don't have the rights to do this!");
        }

        [Authorize]
        [HttpPost("{id}/{status}")]
        public async Task<ActionResult<bool>> UpdateRequestStatus(int id, RequestStatus status)
        {
            int uid = int.Parse(User.Claims.FirstOrDefault().Value);
            if (await _authRepository.IsAdmin(uid))
                return await _requestsRepository.UpdateRequestStatus(id, status);
            return Unauthorized("You don't have the rights to do this!");
        }

        [Authorize]
        [HttpGet("history/{id}/{type?}/{status?}")]
        public async Task<ActionResult<ICollection<RequestsDto>>> GetRequests(int id, RequestType? type, RequestStatus? status)
        {
            int uid = int.Parse(User.Claims.FirstOrDefault().Value);
            if (await _authRepository.IsAdmin(uid) || uid == id)
                return Ok(await _requestsRepository.GetRequests(id, type, status));
            else
                return Unauthorized("You don't have the rights to do this!");
        }
    }
}