using AutoMapper;
using ProfileAPI.DTOs;
using ProfileAPI.Models;

namespace ProfileAPI.Helpers
{
    /// <summary>
    /// Maps the Data Model of the application to the DTOs
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Info, LoginRequest>();
            CreateMap<LoginRequest, Info>();

            CreateMap<Info, LoginResponse>();
            CreateMap<LoginResponse, Info>();
        }
    }
}
