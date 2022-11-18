using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7WebApp.Application.UserCQRS.Commands;
using P7WebApp.Application.UserCQRS.Queries;

namespace P7WebApp.API.Controllers
{
    [Route("api/users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("{userId}/profile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileCommand request)
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
        public async Task<IActionResult> GetUserProfile([FromBody] GetUserProfileQuery request)
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
        public async Task<IActionResult> GetUsersCreatedCourses()
        {
            try
            {
                var result = await _mediator.Send(new GetUserCreatedCoursesQuery());
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("courses/attended")]
        public async Task<IActionResult> GetUsersAttendedCourses()
        {
            try
            {
                var result = await _mediator.Send(new GetUserAttendedCoursesQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
