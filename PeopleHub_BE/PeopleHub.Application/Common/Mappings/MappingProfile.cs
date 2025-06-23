using AutoMapper;
using PeopleHub.Application.Features.Colleagues.Dtos.Responses;
using PeopleHub.Application.Features.Users.Dtos;
using PeopleHub.Domain.Entities;

namespace PeopleHub.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // for get all user
            //CreateMap<AppUser, UserResponseDto>()
            //    .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.Profile.FullName))
            //    .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Profile.Position))
            //    .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Profile.Department))
            //    .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Profile.Location))
            //    .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Profile.Photos.FirstOrDefault(p => p.IsMain)!.Url))
            //    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));
            CreateMap<UserProfile, ColleaguesResponseDto>()
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain)!.Url));
        }
    }
}
