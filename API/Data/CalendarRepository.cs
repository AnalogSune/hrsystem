using System;
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
    public class CalendarRepository : ICalendarRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CalendarRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CalendarEntry>> GetEntries(CalendarSearchDto calendarEntry)
        {
            var entries = await _context.Calendar
                .Where(c => calendarEntry.EmployeeId == null? true: c.EmployeeId == calendarEntry.EmployeeId)
                .Where(c => calendarEntry.Type == null? true : c.Type == calendarEntry.Type )
                .Where(c => 
                    (c.StartDate.Date < calendarEntry.StartDate.Date && c.EndDate.Date >= calendarEntry.StartDate.Date) ||
                    (c.StartDate.Date <= calendarEntry.EndDate.Date && c.EndDate.Date > calendarEntry.EndDate.Date) ||
                    (c.StartDate.Date >= calendarEntry.StartDate.Date && c.EndDate.Date <= calendarEntry.EndDate.Date))
                .Include(c => c.Shift)
                .ToListAsync();

            return entries;
        }

        public async Task<bool> AddEntry(CalendarEntryDto calendarEntry)
        {
            await CorrectDates(calendarEntry);

            var entries = await _context.Calendar
                .Where(c => c.EmployeeId == calendarEntry.EmployeeId)
                .Where(c => 
                    (c.StartDate.Date < calendarEntry.StartDate.Date && c.EndDate.Date >= calendarEntry.StartDate.Date) ||
                    (c.StartDate.Date <= calendarEntry.EndDate.Date && c.EndDate.Date > calendarEntry.EndDate.Date))
                .ToListAsync();

            var entriesToDelete = await _context.Calendar
                    .Where(c => c.EmployeeId == calendarEntry.EmployeeId)
                    .Where(c => 
                    (c.StartDate.Date >= calendarEntry.StartDate.Date && c.EndDate.Date <= calendarEntry.EndDate.Date) )
                    .ToListAsync();


            _context.Calendar.RemoveRange(entriesToDelete);
            
            for (int i = 0; i < entries.Count; i++)
            {
                await CalculateEntries(calendarEntry, entries[i]);
            }
            
            if (calendarEntry.CreateNewEntry)
                await _context.Calendar.AddAsync(_mapper.Map<CalendarEntry>(calendarEntry));

            return await _context.SaveChangesAsync() > 0;
        }
        
        private async Task CorrectDates(CalendarEntryDto calendarEntry)
        {
            var prev = await _context.Calendar
                .Where(c => c.EmployeeId == calendarEntry.EmployeeId)
                .Where(c => c.Type == calendarEntry.Type)
                .Where(c => c.ShiftId == calendarEntry.ShiftId)
                .Where(c => c.EndDate.Date == calendarEntry.StartDate.AddDays(-1).Date)
                .FirstOrDefaultAsync();
            if (prev != null) calendarEntry.StartDate = prev.StartDate;

            var next = await _context.Calendar
                .Where(c => c.EmployeeId == calendarEntry.EmployeeId)
                .Where(c => c.Type == calendarEntry.Type)
                .Where(c => c.ShiftId == calendarEntry.ShiftId)
                .Where(c => c.StartDate.Date == calendarEntry.EndDate.AddDays(1).Date)
                .FirstOrDefaultAsync();
            if (next != null) calendarEntry.EndDate = next.EndDate;
        }
        
        private async Task CalculateEntries(CalendarEntryDto entry1, CalendarEntry entry2)
        {
            int startOffset = (entry1.StartDate.Date - entry2.StartDate.Date).Days;
            int endOffset = (entry2.EndDate.Date - entry1.EndDate.Date ).Days;
            
            if (startOffset > 0 && endOffset > 0)
            {
                var newEntry = new CalendarEntryDto
                {
                    EmployeeId = entry2.EmployeeId,
                    EndDate =  entry2.EndDate,
                    StartDate = entry1.EndDate.AddDays(1),
                    Type = entry2.Type,
                    ShiftId = entry2.ShiftId
                };

                await _context.Calendar.AddAsync(_mapper.Map<CalendarEntry>(newEntry));
                entry2.EndDate = entry1.StartDate.AddDays(-1);
            }
            else if (startOffset > 0 && endOffset <= 0)
            {
                if (entry2.Type != entry1.Type || entry2.ShiftId != entry1.ShiftId)
                    entry2.EndDate = entry1.StartDate.AddDays(-1);
                else
                {
                    entry1.StartDate = entry2.StartDate;
                    _context.Calendar.Remove(entry2);
                }
            }
            else if (startOffset <= 0 && endOffset > 0)
            {
                if (entry2.Type != entry1.Type || entry2.ShiftId != entry1.ShiftId)
                    entry2.StartDate = entry1.EndDate.AddDays(1);
                else
                {
                    entry1.EndDate = entry2.EndDate;
                    _context.Calendar.Remove(entry2);
                }
            }
        }
    }
}