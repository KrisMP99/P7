using AutoMapper;
using P7WebApp.Application.Common.Models;
using P7WebApp.Application.UserCQRS.Commands.SignIn;

namespace P7WebApp.Application.Common.Mappings.Profiles
{
    public class AuthenticateMappingProfile : Profile
    {
        public AuthenticateMappingProfile()
        {
            CreateMap<AuthenticateCommand, TokenRequest>();
        }
    }
}
