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
            CreateMap<DepartmentDto, Department>().ForMember(m => m.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<UserEditDto, AppUser>();
            CreateMap<RequestsDto, Request>();
            CreateMap<Request, RequestsDto>();
            CreateMap<CalendarEntryDto, CalendarEntry>();
            CreateMap<PersonalFiles, PersonalFilesDto>();
            CreateMap<DashboardDto, Dashboard>();
            CreateMap<CVDto, CV>();
            CreateMap<RequestsDto, CalendarEntryDto>()
                .ForMember(m => m.StartDate, opt => opt.MapFrom(src => src.Date))
                .ForMember(m => m.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(m => m.Type, opt => opt.MapFrom(src => src.requestType))
                .ForMember(m => m.CreateNewEntry, opt => opt.MapFrom(src => true));
        }
    }
}