using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;
using System.Linq;

namespace API.Helpers
{
    public class AutoMappersProfile : Profile
    {
        public AutoMappersProfile()
        {
            CreateMap<AppUser, MembersDto>()
                .ForMember(desc => desc.PhotoUrl,
                opt => opt.MapFrom(src => src.Photos.FirstOrDefault().Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotosDto>();
        }
    }
}
