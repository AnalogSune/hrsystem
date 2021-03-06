using System.Threading.Tasks;
using API.DTOs;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class CalendarController : BaseApiController
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly IAuthRepository _authRepository;
        public CalendarController(ICalendarRepository calendarRepository, IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            _calendarRepository = calendarRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddEntry(CalendarEntryDto calendarEntry)
        {
            if (!User.IsAdmin()) return Unauthorized("You dont have rights to do this!");
            
            if (await _calendarRepository.AddEntry(calendarEntry))
                return Ok();
            return BadRequest("Unable to to add the entry to the calendar!");

        }

        [HttpPost("get")]
        public async Task<IActionResult> GetEntries(CalendarSearchDto calendarEntry)
        {
            return Ok(await _calendarRepository.GetEntries(calendarEntry));
        }
    }
}