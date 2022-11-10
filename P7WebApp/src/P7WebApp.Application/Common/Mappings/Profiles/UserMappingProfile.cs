using AutoMapper;
using P7WebApp.Application.Common.Models;
using P7WebApp.Application.User.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.Common.Mappings.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<GetUserProfileQuery, UserDto>();
        }
    }
}
