using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7WebApp.Application.CourseCQRS.Queries;
using P7WebApp.Application.ProfileCQRS.Commands.UpdateProfile;
using P7WebApp.Application.ProfileCQRS.Queries;

namespace P7WebApp.API.Controllers
{
    [Route("api/profiles")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
    }
}
