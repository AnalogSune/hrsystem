using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>();
            CreateMap<RegisterDto, AppUser>()
                .ForMember(r => r.Email, opt => opt.MapFrom(src => src.Email.ToLower()));
            CreateMap<DepartmentDto, Department>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<UserEditDto, AppUser>();
            CreateMap<RequestsDto, Request>();
            CreateMap<Role, RoleDto>();
            CreateMap<WorkShift, WorkShiftCreationDto>();
            CreateMap<WorkShiftCreationDto, WorkShift>();
            CreateMap<RoleDto, Role>();
            CreateMap<Request, RequestsDto>();
            CreateMap<CalendarEntryDto, CalendarEntry>();
            CreateMap<PersonalFile, PersonalFilesDto>();
            CreateMap<DashboardDto, Dashboard>();
            CreateMap<TaskDto, Tasks>();
            CreateMap<Tasks, TaskDto>();
            CreateMap<EmployeesTasks, TaskReturnDto>();
            CreateMap<Dashboard, DashboardReturnDto>()
            .ForMember(m => m.PublisherName, opt => opt.MapFrom(a => a.Publisher.FName));
            CreateMap<CVDto, CV>();
            CreateMap<RequestsDto, CalendarEntryDto>()
                .ForMember(m => m.StartDate, opt => opt.MapFrom(src => src.Date))
                .ForMember(m => m.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(m => m.Type, opt => opt.MapFrom(src => src.requestType))
                .ForMember(m => m.CreateNewEntry, opt => opt.MapFrom(src => true));
        }
    }
}