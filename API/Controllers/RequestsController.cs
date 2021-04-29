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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateRequest(RequestsDto requestsDto)
        {
            int uid = RetrieveUserId();
            if (requestsDto.EmployeeId == uid || await _authRepository.IsAdmin(uid))
                return await _requestsRepository.CreateRequest(requestsDto);
            return Unauthorized("You don't have the rights to do this!");
        }

        [Authorize]
        [HttpPost("{id}/{status}")]
        public async Task<ActionResult<bool>> UpdateRequestStatus(int id, RequestStatus status)
        {
            int uid = RetrieveUserId();
            if (!await _authRepository.IsAdmin(uid))
                return Unauthorized();

            await _requestsRepository.UpdateRequestStatus(id, status);
            if (status == RequestStatus.Accepted)
            {
                var newRequest = (await _requestsRepository.GetRequest(id));
                await _calendarRepository.AddEntry(_mapper.Map<CalendarEntryDto>(newRequest));
            }

            return Ok();
        }

        [Authorize]
        [HttpGet("history/{id}/{type?}/{status?}")]
        public async Task<ActionResult<ICollection<RequestsDto>>> GetRequests(int id, RequestType? type, RequestStatus? status)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid) || uid == id)
                return Ok(await _requestsRepository.GetRequests(id, type, status));
            else
                return Unauthorized("You don't have the rights to do this!");
        }

        [Authorize]
        [HttpPost("search")]
        public async Task<ActionResult<ICollection<RequestsDto>>> GetRequests(RequestSearchDto searchDto)
        {
            int uid = RetrieveUserId();
            if (await _authRepository.IsAdmin(uid) || uid == searchDto.EmployeeId)
                return Ok(await _requestsRepository.GetRequests(searchDto));
            else
                return Unauthorized();
        }
    }
}