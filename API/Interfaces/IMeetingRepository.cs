using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IMeetingRepository
    {
        Task<bool> AddMeeting(Meeting meeting);

        Task<IEnumerable<Meeting>> GetMeetings(int departmentId, MeetingType meetingType);

        Task<bool> DeleteMeeting(int meetingID);
    }
}