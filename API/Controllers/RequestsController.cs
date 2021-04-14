using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
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
        public RequestsController(IUserRepository userRepository, IAuthRepository authRepository, IRequestsRepository requestsRepository)
        {
            _requestsRepository = requestsRepository;
            _authRepository = authRepository;
            _userRepository = userRepository;

        }

        [Authorize]
        [HttpPost("workhome")]
        public async Task<ActionResult<bool>> CreateWorkFromHomeRequest(RequestsDto requestsDto)
        {
            int uid = int.Parse(User.Claims.FirstOrDefault().Value);
            if (requestsDto.EmployeeId == uid || await _authRepository.IsAdmin(uid))
                return await _requestsRepository.CreateWorkFromHomeRequest(requestsDto);
            return Unauthorized("You don't have the rights to do this!");
        }

        [Authorize]
        [HttpPost("dayoff")]
        public async Task<ActionResult<bool>> CreateDayOffRequest(RequestsDto requestsDto)
        {
            int uid = int.Parse(User.Claims.FirstOrDefault().Value);
            if (requestsDto.EmployeeId == uid || await _authRepository.IsAdmin(uid))
                return await _requestsRepository.CreateDayOffRequest(requestsDto);
            return Unauthorized("You don't have the rights to do this!");
        }
        
        [Authorize]
        [HttpPost("dayoff/{id}/{status}")]
        public async Task<ActionResult<bool>> StatusDayOffRequest(int id, int status)
        {
            int uid = int.Parse(User.Claims.FirstOrDefault().Value);
            if (await _authRepository.IsAdmin(uid))
                return await _requestsRepository.StatusDayOffRequest(id, status);
            return Unauthorized("You don't have the rights to do this!");
        }

        [Authorize]
        [HttpPost("workhome/{id}/{status}")]
        public async Task<ActionResult<bool>> StatusWorkHomeRequest(int id, int status)
        {
            int uid = int.Parse(User.Claims.FirstOrDefault().Value);
            if (await _authRepository.IsAdmin(uid))
                return await _requestsRepository.StatusWorkHomeRequest(id, status);
            return Unauthorized("You don't have the rights to do this!");
        }
    }
}