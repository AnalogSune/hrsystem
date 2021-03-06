using System;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using HrSystemTests;
using Xunit;

public class CalendarTest
{
    private readonly CalendarRepository _repo;
    
    public CalendarTest()
    {
        var mock = MockDependenciesFactory.CreateMySqlDb();
        _repo = new CalendarRepository(mock.DataContext, mock.Mapper);
    }

    [Fact]
    public async Task GetEntries()
    {
        var startDate = DateTime.Now;
        var endDate = DateTime.Now.AddDays(2);
        var added = await _repo.AddEntry(new CalendarEntryDto()
        {
            CreateNewEntry = true,
            EmployeeId = 2,
            EndDate = endDate,
            ShiftId = null,
            StartDate = startDate,
            Type = CalendarType.DayOff
        });
        Assert.True(added);
        
        var entries = await _repo.GetEntries(new CalendarSearchDto()
        {
            EmployeeId = 2,
            EndDate = endDate,
            StartDate = startDate,
            Type = CalendarType.DayOff
        });
        
        var calendarEntries =  entries.ToList();
        
        Assert.NotEmpty(calendarEntries);
        Assert.Equal(2, calendarEntries[0].EmployeeId);
        Assert.Equal(CalendarType.DayOff, calendarEntries[0].Type);
    }
}
