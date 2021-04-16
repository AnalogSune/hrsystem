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
            CreateMap<PersonalFiles, PersonalFilesDto>();
        }
    }
}