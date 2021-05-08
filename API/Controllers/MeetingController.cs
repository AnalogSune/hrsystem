using System.Threading.Tasks;
using API.Data;
using API.Entities;
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
        public async Task<IActionResult> AddMeeting(Meeting meeting)
        {
            int uid = RetrieveUserId();
            if (!await _authRepository.IsAdmin(uid))
                return Unauthorized("You need administrative rights!");

            if (await _meetingRepository.AddMeeting(meeting))
                return Ok();
            return BadRequest("Unable to create the request!");
        }

        [HttpGet("{id}/{type}")]
        public async Task<IActionResult> GetMeetings(int id, MeetingType type)
        {
            return Ok(await _meetingRepository.GetMeetings(id, type));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMeeting(int id)
        {
            int uid = RetrieveUserId();
            if (!await _authRepository.IsAdmin(uid))
                return Unauthorized("You need administrative rights!");

            return Ok(await _meetingRepository.DeleteMeeting(id));
        }
    }
}