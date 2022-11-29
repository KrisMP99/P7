using MediatR;
using Microsoft.AspNetCore.Mvc;
using P7WebApp.Application.ProfileCQRS.Commands.SignIn;
using P7WebApp.Application.ProfileCQRS.Commands;
using P7WebApp.Application.ProfileCQRS.Commands.CreateProfile;
using P7WebApp.Application.ProfileCQRS.Commands.UpdateProfile;
using P7WebApp.Application.ProfileCQRS.Queries;
using P7WebApp.Application.CourseCQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace P7WebApp.API.Controllers
{
    [Route("api/profiles")]
    public class ProfileController : BaseController
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //-----------------------ACCOUNT SETTINGS-----------------------//
        //-----------------------NO AUTHORIZATION-----------------------//
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateProfileCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.Succeeded)
                {
                    return Ok("Profile was created successfully");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (result is not null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Invalid login");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand request)
        {
            try
            {
                var result = _mediator.Send(request);
                return Ok("User was logged out");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //-----------------------ACCOUNT SETTINGS END-----------------------//




        //-------------------------PROFILE SETTINGS-------------------------//
        //----------------------AUTHORIZATION REQUIRED----------------------//
        [HttpPost]
        [Route("{userId}/profile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateProfileCommand request)
        {
            try
            {
                var result = _mediator.Send(request);
                return Ok("User was updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{userId}/profile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserProfile([FromBody] GetProfileQuery request)
        {
            try
            {
                var result = _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("courses/created")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUsersCreatedCourses()
        {
            try
            {
                var result = await _mediator.Send(new GetCreatedCoursesQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("courses/attends")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUsersAttendedCourses()
        {
            try
            {
                var result = await _mediator.Send(new GetProfileAttendedCoursesQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}