using MediatR;
using Microsoft.AspNetCore.Mvc;
using P7WebApp.Application.User.Commands;
using P7WebApp.Application.User.Queries;

namespace P7WebApp.API.Controllers
{
    [Route("api/users")]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok("User was created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{userId}/sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand request)
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

        [HttpPost("{userId}/sign-out")]
        public async Task<IActionResult> SignOut([FromBody] SignOutCommand request)
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

        [HttpPost("api/{userId}/profile")]
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

        [HttpGet("{userId}/profile")]
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
    }
}
