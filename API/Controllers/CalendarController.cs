using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CalendarController : BaseApiController
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly IAuthRepository _authRepository;
        public CalendarController(ICalendarRepository calendarRepository, IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            _calendarRepository = calendarRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<bool>> AddEntries(CalendarEntryDto calendarEntry)
        {
            int uid = RetrieveUserId();
            if(await _authRepository.IsAdmin(uid))
                return Ok(await _calendarRepository.AddEntry(calendarEntry));
            
            return Unauthorized("You dont have rights to do this!");
        }

    }
}