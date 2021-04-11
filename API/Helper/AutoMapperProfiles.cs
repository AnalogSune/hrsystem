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
                .ForMember(r => r.UserName, opt => opt.MapFrom(src => src.Username.ToLower()));
        }
    }
}