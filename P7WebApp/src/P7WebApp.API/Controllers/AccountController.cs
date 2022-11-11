using MediatR;
using Microsoft.AspNetCore.Mvc;
using P7WebApp.Application.User.Commands.CreateUser;
using P7WebApp.Application.User.Commands.SignIn;
using P7WebApp.Application.User.Commands;

namespace P7WebApp.API.Controllers
{
    [Route("api/accounts")]
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.Succeeded)
                {
                    return Ok("User was created");
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
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (result.Succeeded)
                {
                    return Ok("Signed in");
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
                return Ok("User was updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}