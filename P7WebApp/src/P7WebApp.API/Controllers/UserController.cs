﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7WebApp.Application.User.Commands;
using P7WebApp.Application.User.Queries;

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