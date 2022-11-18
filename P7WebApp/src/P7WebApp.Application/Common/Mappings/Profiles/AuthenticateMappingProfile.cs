using AutoMapper;
using P7WebApp.Application.AccountCQRS.Commands.UpdateAccountProfile;
using P7WebApp.Application.Responses;
using P7WebApp.Domain.Aggregates.AccountAggregate;

namespace P7WebApp.Application.Common.Mappings.Profiles
{
    public class AuthenticateMappingProfile : Profile
    {
        public AuthenticateMappingProfile()
        {
            CreateMap<Account, TokenResponse>()
                .ForMember(dest => dest.Id, src => src.MapFrom(a => a.UserId))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(a => a.Profile.FirstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(a => a.Profile.LastName))
                .ForMember(dest => dest.Email, src => src.MapFrom(a => a.Profile.Email));

            CreateMap<UpdateAccountProfileCommand, Profile>()
                .ForPath(dest => dest.Profile.FirstName, src => src.MapFrom(a => a.FirstName))
                .ForPath(dest => dest.Profile.LastName, src => src.MapFrom(a => a.LastName))
                .ForPath(dest => dest.Profile.Email, src => src.MapFrom(a => a.Email))
                .ForPath(dest => dest.Profile.Password, src => src.MapFrom(uap => uap.Password));
        }
    }
}