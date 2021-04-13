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
            CreateMap<DepartmentDto, Departments>().ForMember(m => m.Department, opt => opt.MapFrom(src => src.Name));
        }
    }
}