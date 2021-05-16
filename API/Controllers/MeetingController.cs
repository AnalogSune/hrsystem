using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class MeetingController : BaseApiController
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IAuthRepository _authRepository;

        public MeetingController(IMeetingRepository meetingRepository, IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            _meetingRepository = meetingRepository;

        }

        [HttpPost]
        public async Task<IActionResult> AddMeeting(MeetingDto meeting)
        {
            if (!User.IsAdmin())
                return Unauthorized("You need administrative rights!");
            var meet = await _meetingRepository.AddMeeting(meeting);
            if (meet != null)
                return Ok(meet);
            return BadRequest("Unable to create the request!");
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetMeetings(MeetingSearchDto meetingSearch)
        {
            return Ok(await _meetingRepository.GetMeetings(meetingSearch));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMeeting(int id)
        {
            if (!User.IsAdmin())
                return Unauthorized("You need administrative rights!");

            return Ok(await _meetingRepository.DeleteMeeting(id));
        }
    }
}