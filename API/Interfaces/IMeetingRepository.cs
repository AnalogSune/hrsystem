using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IMeetingRepository
    {
        Task<bool> AddMeeting(MeetingDto meeting);

        Task<IEnumerable<Meeting>> GetMeetings(MeetingSearchDto meetingSearch);

        Task<bool> DeleteMeeting(int meetingID);
    }
}