using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly DataContext _context;

        public MeetingRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddMeeting(Meeting meeting)
        {
            await _context.Meetings.AddAsync(meeting);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteMeeting(int meetingID)
        {
            var entry = await _context.Meetings.Where(m => m.Id == meetingID).FirstOrDefaultAsync();

            _context.Remove(entry);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Meeting>> GetMeetings(int departmentId, MeetingType meetingType)
        {
            return await _context.Meetings.Where(m => m.DepartmentId == departmentId && m.MeetingType == meetingType).ToListAsync();
        }
    }
}