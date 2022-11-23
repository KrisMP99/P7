using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7WebApp.Application.AccountCQRS.Commands.UpdateAccountProfile;
using P7WebApp.Application.UserCQRS.Queries;

namespace P7WebApp.API.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("{userId}/profile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateAccountProfileCommand request)
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
        [Route("courses")]
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
    }
}
