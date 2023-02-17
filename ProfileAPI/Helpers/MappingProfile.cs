using AutoMapper;
using ProfileAPI.DTOs;
using ProfileAPI.Models;

namespace ProfileAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Info, LoginRequest>();
            CreateMap<LoginRequest, Info>();
        }
    }
}
