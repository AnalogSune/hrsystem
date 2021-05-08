using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class RequestsController : BaseApiController
    {
        private readonly IAuthRepository _authRepository;
        private readonly IRequestsRepository _requestsRepository;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IMapper _mapper;

        public RequestsController(IAuthRepository authRepository, IRequestsRepository requestsRepository,
            ICalendarRepository calendarRepository, IMapper mapper)
        {
            _mapper = mapper;
            _requestsRepository = requestsRepository;
            _authRepository = authRepository;
            _calendarRepository = calendarRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest(RequestsDto requestsDto)
        {
            int uid = RetrieveUserId();
            if (requestsDto.EmployeeId == uid || await _authRepository.IsAdmin(uid))
            {
                if (await _requestsRepository.CreateRequest(requestsDto))
                    return Ok();
                return BadRequest("Unable to create the request!");
            }
            
            return Unauthorized("You don't have the rights to do this!");
        }

        [HttpPost("{id}/{status}")]
        public async Task<IActionResult> UpdateRequestStatus(int id, RequestStatus status)
        {
            int uid = RetrieveUserId();
            if (!await _authRepository.IsAdmin(uid))
                return Unauthorized("You need administrative rights!");

            if (!await _requestsRepository.UpdateRequestStatus(id, status))
                return BadRequest("Unable to update the request");

            if (status == RequestStatus.Accepted)
            {
                var newRequest = (await _requestsRepository.GetRequest(id));
                if (await _calendarRepository.AddEntry(_mapper.Map<CalendarEntryDto>(newRequest)))
                    return Ok();
            }

            return Ok();
        }

        [HttpGet("history/{id}/{type?}/{status?}")]
        public async Task<IActionResult> GetRequests(int id, RequestType? type, RequestStatus? status)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid) || uid == id)
                return Ok(await _requestsRepository.GetRequests(id, type, status));
            else
                return Unauthorized("You need administrative rights!");
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetRequests(RequestSearchDto searchDto)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid) || uid == searchDto.EmployeeId)
                return Ok(await _requestsRepository.GetRequests(searchDto));
            else
                return Unauthorized("You need administrative rights!");
        }
    }
}