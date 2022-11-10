using MediatR;
using Microsoft.AspNetCore.Mvc;
using P7WebApp.Application.User.Commands;

namespace P7WebApp.API.Controllers
{
    [Route("api/home")]
    public class HomeController : BaseController
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] CreateUserCommand request)
        {
            try
            {
                var result = _mediator.Send(request);
                return Ok("User was created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sign-in")]
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

        [HttpPost("sign-out")]
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
    }
}
