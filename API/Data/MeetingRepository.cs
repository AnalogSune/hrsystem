using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MeetingRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> AddMeeting(MeetingDto meeting)
        {
            await _context.Meetings.AddAsync(_mapper.Map<Meeting>(meeting));

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteMeeting(int meetingID)
        {
            var entry = await _context.Meetings.Where(m => m.Id == meetingID).FirstOrDefaultAsync();

            _context.Remove(entry);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Meeting>> GetMeetings(MeetingSearchDto meetingSearch)
        {
            return await _context.Meetings
                .Where(m => meetingSearch.DepartmentId == null? true: m.DepartmentId == meetingSearch.DepartmentId )
                .Where(m => meetingSearch.MeetingType == null? true: m.MeetingType == (MeetingType)meetingSearch.MeetingType )
                .ToListAsync();
        }
    }
}