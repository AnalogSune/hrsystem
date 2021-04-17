using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ICalendarRepository
    {
        Task<bool> AddEntry(CalendarEntryDto calendarEntry);
    }
}